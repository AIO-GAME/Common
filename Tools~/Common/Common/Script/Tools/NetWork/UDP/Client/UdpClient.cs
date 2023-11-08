/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Net.Sockets;

namespace AIO.Net
{
    /// <summary>
    /// UDP client is used to read/write data from/into the connected UDP server
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public partial class UdpClient : NetClient
    {
        /// <summary>
        /// Client option
        /// </summary>
        public UdpSettingClient Option { get; set; } = new UdpSettingClient();

        /// <inheritdoc />
        protected override Socket CreateSocket()
        {
            return new Socket(Endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        }

        #region Multicast group

        /// <summary>
        /// Setup multicast: bind the socket to the multicast UDP server
        /// </summary>
        /// <param name="enable">Enable/disable multicast</param>
        public virtual void SetupMulticast(bool enable)
        {
            Option.ReuseAddress = enable;
            Option.Multicast = enable;
        }

        /// <summary>
        /// Join multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void JoinMulticastGroup(IPAddress address)
        {
            if (Endpoint.AddressFamily == AddressFamily.InterNetworkV6)
                Socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership,
                    new IPv6MulticastOption(address));
            else
                Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                    new MulticastOption(address));

            // Call the client joined multicast group notification
            OnJoinedMulticastGroup(address);
        }

        /// <summary>
        /// Join multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void JoinMulticastGroup(string address)
        {
            JoinMulticastGroup(IPAddress.Parse(address));
        }

        /// <summary>
        /// Leave multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void LeaveMulticastGroup(IPAddress address)
        {
            if (Endpoint.AddressFamily == AddressFamily.InterNetworkV6)
                Socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership,
                    new IPv6MulticastOption(address));
            else
                Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership,
                    new MulticastOption(address));

            // Call the client left multicast group notification
            OnLeftMulticastGroup(address);
        }

        /// <summary>
        /// Leave multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void LeaveMulticastGroup(string address)
        {
            LeaveMulticastGroup(IPAddress.Parse(address));
        }

        #endregion

        #region Send/Receive data

        /// <inheritdoc />
        protected override void ClearBuffers()
        {
            // Clear send buffers
            SendBuffer.Clear();

            // Update statistic
            BytesPending = 0;
            BytesSending = 0;
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

        /// <summary>
        /// This method is invoked when an asynchronous receive from operation completes
        /// </summary>
        private void ProcessReceiveFrom(SocketAsyncEventArgs e)
        {
            Receiving = false;

            if (!IsConnected)
                return;

            // Disconnect on error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);
                Disconnect();
                return;
            }

            // Received some data from the server
            var size = e.BytesTransferred;

            // Update statistic
            DatagramsReceived++;
            BytesReceived += size;

            // Call the datagram received handler
            OnReceived(e.RemoteEndPoint, ReceiveBuffer.Arrays, 0, size);

            // If the receive buffer is full increase its size
            if (ReceiveBuffer.Capacity == size)
            {
                // Check the receive buffer limit
                if (((2 * size) > Option.ReceiveBufferLimit) && (Option.ReceiveBufferLimit > 0))
                {
                    SendError(SocketError.NoBufferSpaceAvailable);
                    Disconnect();
                    return;
                }

                ReceiveBuffer.Reserve(2 * size);
            }
        }

        /// <summary>
        /// This method is invoked when an asynchronous send to operation completes
        /// </summary>
        private void ProcessSendTo(SocketAsyncEventArgs e)
        {
            Sending = false;

            if (!IsConnected)
                return;

            // Disconnect on error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);
                Disconnect();
                return;
            }

            var sent = e.BytesTransferred;

            // Send some data to the server
            if (sent > 0)
            {
                // Update statistic
                BytesSending = 0;
                BytesSent += sent;

                // Clear the send buffer
                SendBuffer.Clear();

                // Call the buffer sent handler
                OnSent(SendEndpoint, sent);
            }
        }

        #endregion

        #region Session handlers

        /// <summary>
        /// Handle client joined multicast group notification
        /// </summary>
        /// <param name="address">IP address</param>
        protected virtual void OnJoinedMulticastGroup(IPAddress address)
        {
        }

        /// <summary>
        /// Handle client left multicast group notification
        /// </summary>
        /// <param name="address">IP address</param>
        protected virtual void OnLeftMulticastGroup(IPAddress address)
        {
        }

        /// <summary>
        /// Handle datagram received notification
        /// </summary>
        /// <param name="endpoint">Received endpoint</param>
        /// <param name="buffer">Received datagram buffer</param>
        /// <param name="offset">Received datagram buffer offset</param>
        /// <param name="size">Received datagram buffer size</param>
        /// <remarks>
        /// Notification is called when another datagram was received from some endpoint
        /// </remarks>
        protected virtual void OnReceived(EndPoint endpoint, byte[] buffer, int offset, int size)
        {
        }

        /// <summary>
        /// Handle datagram sent notification
        /// </summary>
        /// <param name="endpoint">Endpoint of sent datagram</param>
        /// <param name="sent">Size of sent datagram buffer</param>
        /// <remarks>
        /// Notification is called when a datagram was sent to the server.
        /// This handler could be used to send another datagram to the server for instance when the pending size is zero.
        /// </remarks>
        protected virtual void OnSent(EndPoint endpoint, int sent)
        {
        }

        #endregion

        /// <inheritdoc />
        protected override void SendError(SocketError error)
        {
            // Skip disconnect errors
            if (error == SocketError.ConnectionAborted ||
                error == SocketError.ConnectionRefused ||
                error == SocketError.ConnectionReset ||
                error == SocketError.OperationAborted ||
                error == SocketError.Shutdown)
                return;

            OnError(error);
        }

        /// <inheritdoc />
        public UdpClient(EndPoint endpoint, string address, int port) : base(endpoint, address, port)
        {
        }

        /// <inheritdoc />
        public UdpClient(IPAddress address, int port) : base(address, port)
        {
        }

        /// <inheritdoc />
        public UdpClient(string address, int port) : base(address, port)
        {
        }

        /// <inheritdoc />
        public UdpClient(DnsEndPoint endpoint) : base(endpoint)
        {
        }

        /// <inheritdoc />
        public UdpClient(IPEndPoint endpoint) : base(endpoint)
        {
        }
    }
}