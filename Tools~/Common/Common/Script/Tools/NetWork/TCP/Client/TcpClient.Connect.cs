/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-06
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net.Sockets;
using System.Threading;

namespace AIO.Net
{
    public partial class TcpClient
    {
        /// <summary>
        /// Connect Event Arg
        /// </summary>
        private SocketAsyncEventArgs ConnectEventArg;

        /// <summary>
        /// Is the client connecting?
        /// </summary>
        public bool IsConnecting { get; private set; }

        /// <summary>
        /// Is the client connected?
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Create a new socket object
        /// </summary>
        /// <remarks>
        /// Method may be override if you need to prepare some specific socket object in your implementation.
        /// </remarks>
        /// <returns>Socket object</returns>
        protected virtual Socket CreateSocket()
        {
            return new Socket(Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Connect the client (synchronous)
        /// </summary>
        /// <remarks>
        /// Please note that synchronous connect will not receive data automatically!
        /// You should use Receive() or ReceiveAsync() method manually after successful connection.
        /// </remarks>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public virtual bool Connect()
        {
            if (IsConnected || IsConnecting) return false;

            // Setup buffers
            _receiveNetBuffer = new NetBuffer();
            _sendNetBufferMain = new NetBuffer();
            _sendNetBufferFlush = new NetBuffer();

            // Setup event args
            ConnectEventArg = new SocketAsyncEventArgs();
            ConnectEventArg.RemoteEndPoint = Endpoint;
            ConnectEventArg.Completed += OnAsyncCompleted;

            ReceiveEventArg = new SocketAsyncEventArgs();
            ReceiveEventArg.Completed += OnAsyncCompleted;

            SendEventArg = new SocketAsyncEventArgs();
            SendEventArg.Completed += OnAsyncCompleted;

            // Create a new client socket
            Socket = CreateSocket();

            // Update the client socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: dual mode (this option must be applied before connecting)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6) Socket.DualMode = Option.DualMode;

            // Call the client connecting handler
            OnConnecting();

            try
            {
                // Connect to the server
                Socket.Connect(Endpoint);
            }
            catch (SocketException ex)
            {
                // Call the client error handler
                SendError(ex.SocketErrorCode);

                // Reset event args
                ConnectEventArg.Completed -= OnAsyncCompleted;
                ReceiveEventArg.Completed -= OnAsyncCompleted;
                SendEventArg.Completed -= OnAsyncCompleted;

                // Call the client disconnecting handler
                OnDisconnecting();

                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
                ConnectEventArg.Dispose();
                ReceiveEventArg.Dispose();
                SendEventArg.Dispose();

                // Call the client disconnected handler
                OnDisconnected();

                return false;
            }

            // Apply the option: keep alive
            if (Option.KeepAlive)
                Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            // if (OptionTcpKeepAliveTime >= 0)
            //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime,
            //         OptionTcpKeepAliveTime);
            // if (OptionTcpKeepAliveInterval >= 0)
            //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval,
            //         OptionTcpKeepAliveInterval);
            // if (OptionTcpKeepAliveRetryCount >= 0)
            //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveRetryCount,
            //         OptionTcpKeepAliveRetryCount);
            // Apply the option: no delay
            if (Option.NoDelay)
                Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

            // Prepare receive & send buffers
            _receiveNetBuffer.Reserve(Option.SendBufferSize);
            _sendNetBufferMain.Reserve(Option.SendBufferSize);
            _sendNetBufferFlush.Reserve(Option.SendBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;

            // Update the connected flag
            IsConnected = true;

            // Call the client connected handler
            OnConnected();
            // Call the empty send buffer handler
            if (_sendNetBufferMain.IsEmpty)
                OnEmpty();

            return true;
        }

        /// <summary>
        /// Disconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        public virtual bool Disconnect()
        {
            if (!IsConnected && !IsConnecting) return false;

            // Cancel connecting operation
            if (IsConnecting) Socket.CancelConnectAsync(ConnectEventArg);

            // Reset event args
            ConnectEventArg.Completed -= OnAsyncCompleted;
            ReceiveEventArg.Completed -= OnAsyncCompleted;
            SendEventArg.Completed -= OnAsyncCompleted;

            // Call the client disconnecting handler
            OnDisconnecting();

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

                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
                ConnectEventArg.Dispose();
                ReceiveEventArg.Dispose();
                SendEventArg.Dispose();

                // Update the client socket disposed flag
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

            // Call the client disconnected handler
            OnDisconnected();

            return true;
        }

        /// <summary>
        /// Reconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully reconnected, 'false' if the client is already reconnected</returns>
        public virtual bool Reconnect()
        {
            return Disconnect() && Connect();
        }

        /// <summary>
        /// Connect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public virtual bool ConnectAsync()
        {
            if (IsConnected || IsConnecting)
                return false;

            // Setup buffers
            _receiveNetBuffer = new NetBuffer();
            _sendNetBufferMain = new NetBuffer();
            _sendNetBufferFlush = new NetBuffer();

            // Setup event args
            ConnectEventArg = new SocketAsyncEventArgs();
            ConnectEventArg.RemoteEndPoint = Endpoint;
            ConnectEventArg.Completed += OnAsyncCompleted;
            ReceiveEventArg = new SocketAsyncEventArgs();
            ReceiveEventArg.Completed += OnAsyncCompleted;
            SendEventArg = new SocketAsyncEventArgs();
            SendEventArg.Completed += OnAsyncCompleted;

            // Create a new client socket
            Socket = CreateSocket();

            // Update the client socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: dual mode (this option must be applied before connecting)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6)
                Socket.DualMode = Option.DualMode;

            // Update the connecting flag
            IsConnecting = true;

            // Call the client connecting handler
            OnConnecting();

            // Async connect to the server
            if (!Socket.ConnectAsync(ConnectEventArg))
                ProcessConnect(ConnectEventArg);

            return true;
        }

        /// <summary>
        /// Disconnect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        public virtual bool DisconnectAsync() => Disconnect();

        /// <summary>
        /// Reconnect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully reconnected, 'false' if the client is already reconnected</returns>
        public virtual bool ReconnectAsync()
        {
            if (!DisconnectAsync())
                return false;

            while (IsConnected)
                Thread.Yield();

            return ConnectAsync();
        }

        /// <summary>
        /// This method is invoked when an asynchronous connect operation completes
        /// </summary>
        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            IsConnecting = false;

            if (e.SocketError == SocketError.Success)
            {
                // Apply the option: keep alive
                if (Option.KeepAlive)
                    Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                // if (OptionTcpKeepAliveTime >= 0)
                //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime,
                //         OptionTcpKeepAliveTime);
                // if (OptionTcpKeepAliveInterval >= 0)
                //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval,
                //         OptionTcpKeepAliveInterval);
                // if (OptionTcpKeepAliveRetryCount >= 0)
                //     Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveRetryCount,
                //         OptionTcpKeepAliveRetryCount);
                // Apply the option: no delay
                if (Option.NoDelay)
                    Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                // Prepare receive & send buffers
                _receiveNetBuffer.Reserve(Option.ReceiveBufferSize);
                _sendNetBufferMain.Reserve(Option.SendBufferSize);
                _sendNetBufferFlush.Reserve(Option.SendBufferSize);

                // Reset statistic
                BytesPending = 0;
                BytesSending = 0;
                BytesSent = 0;
                BytesReceived = 0;

                // Update the connected flag
                IsConnected = true;

                // Try to receive something from the server
                TryReceive();

                // Check the socket disposed state: in some rare cases it might be disconnected while receiving!
                if (IsSocketDisposed)
                    return;

                // Call the client connected handler
                OnConnected();

                // Call the empty send buffer handler
                if (_sendNetBufferMain.IsEmpty)
                    OnEmpty();
            }
            else
            {
                // Call the client disconnected handler
                SendError(e.SocketError);
                OnDisconnected();
            }
        }
    }
}