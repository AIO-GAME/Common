#region

using System;
using System.Net;
using System.Net.Sockets;

#endregion

namespace AIO.Net
{
    public partial class UdpClient
    {
        /// <inheritdoc />
        public override bool Connect()
        {
            if (IsConnected)
                return false;

            // Setup buffers
            ReceiveNetBuffer = new NetBuffer();
            SendNetBuffer    = new NetBuffer();

            // Setup event args
            ReceiveEventArg           =  new SocketAsyncEventArgs();
            ReceiveEventArg.Completed += OnAsyncCompleted;
            SendEventArg              =  new SocketAsyncEventArgs();
            SendEventArg.Completed    += OnAsyncCompleted;

            // Create a new client socket
            Socket = CreateSocket();

            // Update the client socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: reuse address
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, Option.ReuseAddress);
            // Apply the option: exclusive address use
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse,
                                   Option.ExclusiveAddressUse);
            // Apply the option: dual mode (this option must be applied before receiving/sending)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6)
                Socket.DualMode = Option.DualMode;

            // Call the client connecting handler
            OnConnecting();

            try
            {
                // Bind the acceptor socket to the endpoint
                if (Option.Multicast)
                {
                    Socket.Bind(Endpoint);
                }
                else
                {
                    var endpoint =
                        new IPEndPoint(
                            Endpoint.AddressFamily == AddressFamily.InterNetworkV6
                                ? IPAddress.IPv6Any
                                : IPAddress.Any, 0);
                    Socket.Bind(endpoint);
                }
            }
            catch (SocketException ex)
            {
                // Call the client error handler
                SendError(ex.SocketErrorCode);

                // Reset event args
                ReceiveEventArg.Completed -= OnAsyncCompleted;
                SendEventArg.Completed    -= OnAsyncCompleted;

                // Call the client disconnecting handler
                OnDisconnecting();

                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
                ReceiveEventArg.Dispose();
                SendEventArg.Dispose();

                // Call the client disconnected handler
                OnDisconnected();

                return false;
            }

            // Prepare receive endpoint
            ReceiveEndpoint =
                new IPEndPoint(
                    Endpoint.AddressFamily == AddressFamily.InterNetworkV6 ? IPAddress.IPv6Any : IPAddress.Any, 0);

            // Prepare receive & send buffers
            ReceiveNetBuffer.Reserve(Option.ReceiveBufferSize);

            // Reset statistic
            BytesPending      = 0;
            BytesSending      = 0;
            BytesSent         = 0;
            BytesReceived     = 0;
            DatagramsSent     = 0;
            DatagramsReceived = 0;

            // Update the connected flag
            IsConnected = true;

            // Call the client connected handler
            OnConnected();

            return true;
        }

        /// <inheritdoc />
        public override bool Disconnect()
        {
            if (!IsConnected)
                return false;

            // Reset event args
            ReceiveEventArg.Completed -= OnAsyncCompleted;
            SendEventArg.Completed    -= OnAsyncCompleted;

            // Call the client disconnecting handler
            OnDisconnecting();

            try
            {
                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
                ReceiveEventArg.Dispose();
                SendEventArg.Dispose();

                // Update the client socket disposed flag
                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException) { }

            // Update the connected flag
            IsConnected = false;

            // Update sending/receiving flags
            Receiving = false;
            Sending   = false;

            // Clear send/receive buffers
            ClearBuffers();

            // Call the client disconnected handler
            OnDisconnected();

            return true;
        }
    }
}