using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AIO.Net
{
    /// <summary>
    /// TCP server is used to connect, disconnect and manage TCP sessions
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public partial class TcpServer : NetServer
    {
        /// <summary>
        /// Initialize TCP server with a given IP address and port number / 使用给定的IP地址和端口号初始化TCP服务器
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public TcpServer(IPAddress address, int port) : this(new IPEndPoint(address, port))
        {
        }

        /// <summary>
        /// Initialize TCP server with a given IP address and port number / 使用给定的IP地址和端口号初始化TCP服务器
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public TcpServer(string address, int port) : this(new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <summary>
        /// Initialize TCP server with a given DNS endpoint / 使用给定的DNS端点初始化TCP服务器
        /// </summary>
        /// <param name="endpoint">DNS endpoint</param>
        public TcpServer(DnsEndPoint endpoint) : base(endpoint, endpoint.Host, endpoint.Port)
        {
        }

        /// <summary>
        /// Initialize TCP server with a given IP endpoint / 使用给定的IP端点初始化TCP服务器
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        public TcpServer(IPEndPoint endpoint) : base(endpoint, endpoint.Address.ToString(), endpoint.Port)
        {
        }

        /// <summary>
        /// Number of sessions connected to the server / 连接到服务器的会话数
        /// </summary>
        public long ConnectedSessions => Sessions.Count;

        /// <summary>
        /// Number of bytes pending sent by the server / 服务器待发送的字节数
        /// </summary>
        public long BytesPending => _bytesPending;

        /// <summary>
        /// Number of bytes sent by the server / 服务器发送的字节数
        /// </summary>
        public long BytesSent => _bytesSent;

        /// <summary>
        /// Number of bytes received by the server / 服务器接收的字节数
        /// </summary>
        public long BytesReceived => _bytesReceived;

        /// <summary>
        /// Server option / 服务器选项
        /// </summary>
        public NetSettingServer Option { get; } = new NetSettingServer();

        #region Start/Stop server

        // Server acceptor
        private Socket AcceptorSocket;
        private SocketAsyncEventArgs AcceptorEventArg;

        // Server statistic
        /// <summary>
        /// Number of bytes pending sent by the server / 服务器待发送的字节数
        /// </summary>
        internal long _bytesPending;

        /// <summary>
        /// Number of bytes sent by the server / 服务器发送的字节数
        /// </summary>
        internal long _bytesSent;

        /// <summary>
        /// Number of bytes received by the server / 服务器接收的字节数
        /// </summary>
        internal long _bytesReceived;

        /// <summary>
        /// Is the server accepting new clients?
        /// </summary>
        public bool IsAccepting { get; private set; }

        /// <inheritdoc />
        protected override Socket CreateSocket()
        {
            return new Socket(Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <inheritdoc />
        public override bool Start()
        {
            Debug.Assert(!IsStarted, "TCP server is already started!");
            if (IsStarted) return false;

            // Setup acceptor event arg
            AcceptorEventArg = new SocketAsyncEventArgs();
            AcceptorEventArg.Completed += OnAsyncCompleted;

            // Create a new acceptor socket
            AcceptorSocket = CreateSocket();

            // Update the acceptor socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: reuse address
            AcceptorSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress,
                Option.ReuseAddress);
            // Apply the option: exclusive address use
            AcceptorSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse,
                Option.ExclusiveAddressUse);
            // Apply the option: dual mode (this option must be applied before listening)
            if (AcceptorSocket.AddressFamily == AddressFamily.InterNetworkV6)
                AcceptorSocket.DualMode = Option.DualMode;

            // Bind the acceptor socket to the endpoint
            AcceptorSocket.Bind(Endpoint);
            // Refresh the endpoint property based on the actual endpoint created
            Endpoint = AcceptorSocket.LocalEndPoint;

            // Call the server starting handler
            OnStarting();

            // Start listen to the acceptor socket with the given accepting backlog size
            AcceptorSocket.Listen(Option.AcceptorBacklog);

            // Reset statistic
            _bytesPending = 0;
            _bytesSent = 0;
            _bytesReceived = 0;

            // Update the started flag
            IsStarted = true;

            // Call the server started handler
            OnStarted();

            // Perform the first server accept
            IsAccepting = true;
            StartAccept(AcceptorEventArg);

            return true;
        }

        /// <inheritdoc />
        public override bool Stop()
        {
            Debug.Assert(IsStarted, "TCP server is not started!");
            if (!IsStarted)
                return false;

            // Stop accepting new clients
            IsAccepting = false;

            // Reset acceptor event arg
            AcceptorEventArg.Completed -= OnAsyncCompleted;

            // Call the server stopping handler
            OnStopping();

            try
            {
                // Close the acceptor socket
                AcceptorSocket.Close();

                // Dispose the acceptor socket
                AcceptorSocket.Dispose();

                // Dispose event arguments
                AcceptorEventArg.Dispose();

                // Update the acceptor socket disposed flag
                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException)
            {
            }

            // Disconnect all sessions
            DisconnectAll();

            // Update the started flag
            IsStarted = false;

            // Call the server stopped handler
            OnStopped();

            return true;
        }

        /// <inheritdoc />
        public override bool Restart()
        {
            if (!Stop()) return false;
            while (IsStarted) Thread.Yield();
            return Start();
        }

        #endregion

        #region Accepting clients

        /// <summary>
        /// Start accept a new client connection
        /// </summary>
        private void StartAccept(SocketAsyncEventArgs e)
        {
            // Socket must be cleared since the context object is being reused
            e.AcceptSocket = null;

            // Async accept a new client connection
            if (!AcceptorSocket.AcceptAsync(e))
                ProcessAccept(e);
        }

        /// <summary>
        /// Process accepted client connection
        /// </summary>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Create a new session to register
                var session = CreateSession();

                // Register the session
                RegisterSession(session);

                // Connect new session
                session.Connect(e.AcceptSocket);
            }
            else
                SendError(e.SocketError);

            // Accept the next client connection
            if (IsAccepting)
                StartAccept(e);
        }

        /// <summary>
        /// This method is the callback method associated with Socket.AcceptAsync()
        /// operations and is invoked when an accept operation is complete
        /// </summary>
        private void OnAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (IsSocketDisposed)
                return;

            ProcessAccept(e);
        }

        #endregion

        #region Session factory

        /// <summary>
        /// Create TCP session factory method
        /// </summary>
        /// <returns>TCP session</returns>
        protected virtual TcpSession CreateSession()
        {
            return new TcpSession(this);
        }

        #endregion

        #region Session management

        /// <summary>
        /// Server sessions
        /// </summary>
        protected readonly ConcurrentDictionary<Guid, TcpSession> Sessions =
            new ConcurrentDictionary<Guid, TcpSession>();

        /// <summary>
        /// Disconnect all connected sessions
        /// </summary>
        /// <returns>'true' if all sessions were successfully disconnected, 'false' if the server is not started</returns>
        public virtual bool DisconnectAll()
        {
            if (!IsStarted)
                return false;

            // Disconnect all sessions
            foreach (var session in Sessions.Values)
                session.Disconnect();

            return true;
        }

        /// <summary>
        /// Find a session with a given Id
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <returns>Session with a given Id or null if the session it not connected</returns>
        public TcpSession FindSession(Guid id)
        {
            // Try to find the required session
            return Sessions.TryGetValue(id, out var result) ? result : null;
        }

        /// <summary>
        /// Register a new session
        /// </summary>
        /// <param name="session">Session to register</param>
        internal void RegisterSession(TcpSession session)
        {
            // Register a new session
            Sessions.TryAdd(session.Id, session);
        }

        /// <summary>
        /// Unregister session by Id
        /// </summary>
        /// <param name="id">Session Id</param>
        internal void UnregisterSession(Guid id)
        {
            // Unregister session by Id
            Sessions.TryRemove(id, out _);
        }

        #endregion

        #region Multicasting

        /// <summary>
        /// Multicast data to all connected clients
        /// </summary>
        /// <param name="buffer">Buffer to send as a span of bytes</param>
        /// <returns>'true' if the data was successfully multicasted, 'false' if the data was not multicasted</returns>
        public bool Multicast(BufferByte buffer)
        {
            if (!IsStarted)
                return false;

            if (buffer is null || buffer.Count == 0)
                return true;

            // Multicast data to all sessions
            foreach (var session in Sessions.Values)
                session.SendAsync(buffer);

            return true;
        }

        /// <summary>
        /// Multicast text to all connected clients
        /// </summary>
        /// <param name="text">Text string to multicast</param>
        /// <returns>'true' if the text was successfully multicasted, 'false' if the text was not multicasted</returns>
        public bool Multicast(string text)
        {
            var data = new BufferByte();
            data.WriteStringUTF8(text);
            return Multicast(data);
        }

        #endregion

        #region Server handlers

        /// <summary>
        /// Handle session connecting notification
        /// </summary>
        /// <param name="session">Connecting session</param>
        protected virtual void OnConnecting(TcpSession session)
        {
        }

        /// <summary>
        /// Handle session connected notification
        /// </summary>
        /// <param name="session">Connected session</param>
        protected virtual void OnConnected(TcpSession session)
        {
        }

        /// <summary>
        /// Handle session disconnecting notification
        /// </summary>
        /// <param name="session">Disconnecting session</param>
        protected virtual void OnDisconnecting(TcpSession session)
        {
        }

        /// <summary>
        /// Handle session disconnected notification
        /// </summary>
        /// <param name="session">Disconnected session</param>
        protected virtual void OnDisconnected(TcpSession session)
        {
        }

        internal void OnConnectingInternal(TcpSession session)
        {
            OnConnecting(session);
        }

        internal void OnConnectedInternal(TcpSession session)
        {
            OnConnected(session);
        }

        internal void OnDisconnectingInternal(TcpSession session)
        {
            OnDisconnecting(session);
        }

        internal void OnDisconnectedInternal(TcpSession session)
        {
            OnDisconnected(session);
        }

        #endregion

        /// <summary>
        /// Send error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected override void SendError(SocketError error)
        {
            // Skip disconnect errors
            if (error == SocketError.ConnectionAborted ||
                error == SocketError.ConnectionRefused ||
                error == SocketError.ConnectionReset ||
                error == SocketError.OperationAborted ||
                error == SocketError.Shutdown
               ) return;
            OnError(error);
        }
    }
}