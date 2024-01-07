/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace AIO.Net
{
    /// <summary>
    /// UDP server is used to send or multicast datagrams to UDP endpoints
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public partial class UdpServer : NetServer, INetSession
    {
        /// <inheritdoc />
        public UdpServer(IPAddress address, int port) : this(new IPEndPoint(address, port))
        {
        }

        /// <inheritdoc />
        public UdpServer(string address, int port) : this(new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <inheritdoc />
        public UdpServer(DnsEndPoint endpoint) : base(endpoint, endpoint.Host, endpoint.Port)
        {
        }

        /// <inheritdoc />
        public UdpServer(IPEndPoint endpoint) : base(endpoint, endpoint.Address.ToString(), endpoint.Port)
        {
        }

        /// <summary>
        /// Multicast endpoint
        /// </summary>
        public EndPoint MulticastEndpoint { get; private set; }

        /// <summary>
        /// Socket
        /// </summary>
        public Socket Socket { get; private set; }

        /// <summary>
        /// Number of bytes pending sent by the server
        /// </summary>
        public long BytesPending { get; private set; }

        /// <summary>
        /// Number of bytes sending by the server
        /// </summary>
        public long BytesSending { get; private set; }

        /// <summary>
        /// Number of bytes sent by the server
        /// </summary>
        public long BytesSent { get; private set; }

        /// <summary>
        /// Number of bytes received by the server
        /// </summary>
        public long BytesReceived { get; private set; }

        /// <summary>
        /// Number of datagrams sent by the server
        /// </summary>
        public long DatagramsSent { get; private set; }

        /// <summary>
        /// Number of datagrams received by the server
        /// </summary>
        public long DatagramsReceived { get; private set; }

        /// <summary>
        /// Server option
        /// </summary>
        public UdpSettingServer Option { get; } = new UdpSettingServer();

        #region Connect / Disconnect client

        /// <inheritdoc />
        protected override Socket CreateSocket()
        {
            return new Socket(Endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <summary>
        /// Start the server (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public override bool Start()
        {
            Debug.Assert(!IsStarted, "UDP server is already started!");
            if (IsStarted) return false;

            // Setup buffers
            _receiveNetBuffer = new NetBuffer();
            _sendNetBuffer = new NetBuffer();

            // Setup event args
            ReceiveEventArg = new SocketAsyncEventArgs();
            ReceiveEventArg.Completed += OnAsyncCompleted;
            SendEventArg = new SocketAsyncEventArgs();
            SendEventArg.Completed += OnAsyncCompleted;

            // Create a new server socket
            Socket = CreateSocket();

            // Update the server socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: reuse address
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, Option.ReuseAddress);
            // Apply the option: exclusive address use
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse,
                Option.ExclusiveAddressUse);
            // Apply the option: dual mode (this option must be applied before receiving)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6)
                Socket.DualMode = Option.DualMode;

            // Bind the server socket to the endpoint
            Socket.Bind(Endpoint);
            // Refresh the endpoint property based on the actual endpoint created
            Endpoint = Socket.LocalEndPoint;

            // Call the server starting handler
            OnStarting();

            // Prepare receive endpoint
            ReceiveEndpoint =
                new IPEndPoint(
                    Endpoint.AddressFamily == AddressFamily.InterNetworkV6 ? IPAddress.IPv6Any : IPAddress.Any, 0);

            // Prepare receive & send buffers
            _receiveNetBuffer.Reserve(Option.ReceiveBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;
            DatagramsSent = 0;
            DatagramsReceived = 0;

            // Update the started flag
            IsStarted = true;

            // Call the server started handler
            OnStarted();

            return true;
        }

        /// <summary>
        /// Stop the server (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully stopped, 'false' if the server is already stopped</returns>
        public override bool Stop()
        {
            Debug.Assert(IsStarted, "UDP server is not started!");
            if (!IsStarted) return false;

            // Reset event args
            ReceiveEventArg.Completed -= OnAsyncCompleted;
            SendEventArg.Completed -= OnAsyncCompleted;

            // Call the server stopping handler
            OnStopping();

            try
            {
                // Close the server socket
                Socket.Close();

                // Dispose the server socket
                Socket.Dispose();

                // Dispose event arguments
                ReceiveEventArg.Dispose();
                SendEventArg.Dispose();

                // Update the server socket disposed flag
                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException)
            {
            }

            // Update the started flag
            IsStarted = false;

            // Update sending/receiving flags
            Receiving = false;
            Sending = false;

            // Clear send/receive buffers
            ClearBuffers();

            // Call the server stopped handler
            OnStopped();

            return true;
        }

        /// <summary>
        /// Start the server with a given multicast IP address and port number (synchronous)
        /// </summary>
        /// <param name="multicastAddress">Multicast IP address</param>
        /// <param name="multicastPort">Multicast port number</param>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public virtual bool Start(IPAddress multicastAddress, int multicastPort) =>
            Start(new IPEndPoint(multicastAddress, multicastPort));

        /// <summary>
        /// Start the server with a given multicast IP address and port number (synchronous)
        /// </summary>
        /// <param name="multicastAddress">Multicast IP address</param>
        /// <param name="multicastPort">Multicast port number</param>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public virtual bool Start(string multicastAddress, int multicastPort) =>
            Start(new IPEndPoint(IPAddress.Parse(multicastAddress), multicastPort));

        /// <summary>
        /// Start the server with a given multicast endpoint (synchronous)
        /// </summary>
        /// <param name="multicastEndpoint">Multicast endpoint</param>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public virtual bool Start(EndPoint multicastEndpoint)
        {
            MulticastEndpoint = multicastEndpoint;
            return Start();
        }

        #endregion

        /// <summary>
        /// Clear send/receive buffers
        /// </summary>
        protected override void ClearBuffers()
        {
            // Clear send buffers
            _sendNetBuffer.Clear();

            // Update statistic
            BytesPending = 0;
            BytesSending = 0;
        }

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
                case SocketAsyncOperation.ReceiveFrom:
                    ProcessReceiveFrom(e);
                    break;
                case SocketAsyncOperation.SendTo:
                    ProcessSendTo(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }
    }
}