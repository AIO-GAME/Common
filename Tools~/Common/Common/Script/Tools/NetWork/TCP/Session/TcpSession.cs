using System;
using System.Net.Sockets;

namespace AIO.Net
{
    /// <summary>
    /// TCP session is used to read and write data from the connected TCP client
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public partial class TcpSession : INetSession
    {
        /// <summary>
        /// Initialize the session with a given server
        /// </summary>
        /// <param name="server">TCP server</param>
        public TcpSession(TcpServer server)
        {
            Id = Guid.NewGuid();
            Server = server;
            OptionReceiveBufferSize = server.Option.ReceiveBufferSize;
            OptionSendBufferSize = server.Option.SendBufferSize;
        }

        /// <summary>
        /// Session Id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Server
        /// </summary>
        public TcpServer Server { get; }

        /// <summary>
        /// Socket
        /// </summary>
        public Socket Socket { get; private set; }

        /// <summary>
        /// Number of bytes pending sent by the session // 会话发送的待处理字节数
        /// </summary>
        public long BytesPending { get; private set; }

        /// <summary>
        /// Number of bytes sending by the session // 会话发送的字节数
        /// </summary>
        public long BytesSending { get; private set; }

        /// <summary>
        /// Number of bytes sent by the session // 会话发送的字节数
        /// </summary>
        public long BytesSent { get; private set; }

        /// <summary>
        /// Number of bytes received by the session // 会话接收的字节数
        /// </summary>
        public long BytesReceived { get; private set; }

        /// <summary>
        /// Option: receive buffer limit
        /// </summary>
        public int OptionReceiveBufferLimit { get; set; } = 0;

        /// <summary>
        /// Option: receive buffer size
        /// </summary>
        public int OptionReceiveBufferSize { get; set; } = 8192;

        /// <summary>
        /// Option: send buffer limit
        /// </summary>
        public int OptionSendBufferLimit { get; set; } = 0;

        /// <summary>
        /// Option: send buffer size
        /// </summary>
        public int OptionSendBufferSize { get; set; } = 8192;

        #region Connect/Disconnect session

        /// <summary>
        /// Is the session connected?
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Connect the session
        /// </summary>
        /// <param name="socket">Session socket</param>
        internal void Connect(Socket socket)
        {
            Socket = socket;

            // Update the session socket disposed flag
            IsSocketDisposed = false;

            // Setup buffers
            _receiveNetBuffer = new NetBuffer();
            _sendNetBufferMain = new NetBuffer();
            _sendNetBufferFlush = new NetBuffer();

            // Setup event args
            ReceiveEventArg = new SocketAsyncEventArgs();
            ReceiveEventArg.Completed += OnAsyncCompleted;
            SendEventArg = new SocketAsyncEventArgs();
            SendEventArg.Completed += OnAsyncCompleted;

            // Apply the option: keep alive
            if (Server.Option.KeepAlive)
                Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            // if (Server.OptionTcpKeepAliveTime >= 0)
            //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime, Server.OptionTcpKeepAliveTime);
            // if (Server.OptionTcpKeepAliveInterval >= 0)
            //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval, Server.OptionTcpKeepAliveInterval);
            // if (Server.OptionTcpKeepAliveRetryCount >= 0)
            //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveRetryCount, Server.OptionTcpKeepAliveRetryCount);
            // Apply the option: no delay
            if (Server.Option.NoDelay)
                Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

            // Prepare receive & send buffers
            _receiveNetBuffer.Reserve(OptionReceiveBufferSize);
            _sendNetBufferMain.Reserve(OptionSendBufferSize);
            _sendNetBufferFlush.Reserve(OptionSendBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;

            // Call the session connecting handler
            OnConnecting();

            // Call the session connecting handler in the server
            Server.OnConnectingInternal(this);

            // Update the connected flag
            IsConnected = true;

            // Try to receive something from the client
            ReceiveAsync();

            // Check the socket disposed state: in some rare cases it might be disconnected while receiving!
            if (IsSocketDisposed) return;

            // Call the session connected handler
            OnConnected();

            // Call the session connected handler in the server
            Server.OnConnectedInternal(this);

            // Call the empty send buffer handler
            if (_sendNetBufferMain.IsEmpty)
                OnEmpty();
        }

        /// <summary>
        /// Disconnect the session
        /// </summary>
        /// <returns>'true' if the section was successfully disconnected, 'false' if the section is already disconnected</returns>
        public virtual bool Disconnect()
        {
            if (!IsConnected)
                return false;

            // Reset event args
            ReceiveEventArg.Completed -= OnAsyncCompleted;
            SendEventArg.Completed -= OnAsyncCompleted;

            // Call the session disconnecting handler
            OnDisconnecting();

            // Call the session disconnecting handler in the server
            Server.OnDisconnectingInternal(this);

            try
            {
                try
                {
                    // Shutdown the socket associated with the client
                    Socket.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException)
                {
                }

                // Close the session socket
                Socket.Close();

                // Dispose the session socket
                Socket.Dispose();

                // Dispose event arguments
                ReceiveEventArg.Dispose();
                SendEventArg.Dispose();

                // Update the session socket disposed flag
                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException)
            {
            }

            // Update the connected flag
            IsConnected = false;

            // Update sending/receiving flags
            Receiving = false;
            Sending = false;

            // Clear send/receive buffers
            ClearBuffers();

            // Call the session disconnected handler
            OnDisconnected();

            // Call the session disconnected handler in the server
            Server.OnDisconnectedInternal(this);

            // Unregister session
            Server.UnregisterSession(Id);

            return true;
        }

        #endregion

        #region Send/Receive data

        /// <summary>
        /// Clear send/receive buffers
        /// </summary>
        private void ClearBuffers()
        {
            lock (SendLock)
            {
                // Clear send buffers
                _sendNetBufferMain.Clear();
                _sendNetBufferFlush.Clear();
                SendBufferFlushOffset = 0;

                // Update statistic
                BytesPending = 0;
                BytesSending = 0;
            }
        }

        #endregion

        #region IO processing

        /// <summary>
        /// This method is called whenever a receive or send operation is completed on a socket
        /// </summary>
        private void OnAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (IsSocketDisposed)
                return;

            // Determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    if (ProcessReceive(e))
                        ReceiveAsync();
                    break;
                case SocketAsyncOperation.Send:
                    if (ProcessSend(e))
                        TrySend();
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        #endregion

        #region Session handlers

        /// <summary>
        /// Handle client connecting notification
        /// </summary>
        protected virtual void OnConnecting()
        {
        }

        /// <summary>
        /// Handle client connected notification
        /// </summary>
        protected virtual void OnConnected()
        {
        }

        /// <summary>
        /// Handle client disconnecting notification
        /// </summary>
        protected virtual void OnDisconnecting()
        {
        }

        /// <summary>
        /// Handle client disconnected notification
        /// </summary>
        protected virtual void OnDisconnected()
        {
        }

        /// <summary>
        /// Handle buffer received notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        /// <remarks>
        /// Notification is called when another chunk of buffer was received from the client
        /// </remarks>
        protected virtual void OnReceived(byte[] buffer, int offset, int size)
        {
        }

        /// <summary>
        /// Handle buffer sent notification
        /// </summary>
        /// <param name="sent">Size of sent buffer</param>
        /// <param name="pending">Size of pending buffer</param>
        /// <remarks>
        /// Notification is called when another chunk of buffer was sent to the client.
        /// This handler could be used to send another buffer to the client for instance when the pending size is zero.
        /// </remarks>
        protected virtual void OnSent(long sent, long pending)
        {
        }

        /// <summary>
        /// Handle empty send buffer notification
        /// </summary>
        /// <remarks>
        /// Notification is called when the send buffer is empty and ready for a new data to send.
        /// This handler could be used to send another buffer to the client.
        /// </remarks>
        protected virtual void OnEmpty()
        {
        }

        /// <summary>
        /// Handle error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void OnError(SocketError error)
        {
        }

        #endregion

        #region Error handling

        /// <summary>
        /// Send error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        private void SendError(SocketError error)
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

        #endregion

        #region IDisposable implementation

        /// <summary>
        /// Disposed flag
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Session socket disposed flag
        /// </summary>
        public bool IsSocketDisposed { get; private set; } = true;

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposingManagedResources"></param>
        protected virtual void Dispose(bool disposingManagedResources)
        {
            // The idea here is that Dispose(Boolean) knows whether it is
            // being called to do explicit cleanup (the Boolean is true)
            // versus being called due to a garbage collection (the Boolean
            // is false). This distinction is useful because, when being
            // disposed explicitly, the Dispose(Boolean) method can safely
            // execute code using reference type fields that refer to other
            // objects knowing for sure that these other objects have not been
            // finalized or disposed of yet. When the Boolean is false,
            // the Dispose(Boolean) method should not execute code that
            // refer to reference type fields because those objects may
            // have already been finalized."

            if (IsDisposed) return;

            // Dispose managed resources here...
            if (disposingManagedResources) Disconnect();

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }

        #endregion
    }
}