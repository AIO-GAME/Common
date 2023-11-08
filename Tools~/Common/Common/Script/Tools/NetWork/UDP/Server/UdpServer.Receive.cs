/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Net.Sockets;

namespace AIO.Net
{
    public partial class UdpServer
    {
        private EndPoint ReceiveEndpoint;

        private bool Receiving;

        private Buffer ReceiveBuffer;

        private SocketAsyncEventArgs ReceiveEventArg;

        /// <summary>
        /// This method is invoked when an asynchronous receive from operation completes
        /// </summary>
        private void ProcessReceiveFrom(SocketAsyncEventArgs e)
        {
            Receiving = false;

            if (!IsStarted)
                return;

            // Check for error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);

                // Call the datagram received zero handler
                OnReceived(e.RemoteEndPoint, ReceiveBuffer.Arrays, 0, 0);

                return;
            }

            // Received some data from the client
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
                if (2 * size > Option.ReceiveBufferLimit && (Option.ReceiveBufferLimit > 0))
                {
                    SendError(SocketError.NoBufferSpaceAvailable);

                    // Call the datagram received zero handler
                    OnReceived(e.RemoteEndPoint, ReceiveBuffer.Arrays, 0, 0);

                    return;
                }

                ReceiveBuffer.Reserve(2 * size);
            }
        }

        /// <inheritdoc />
        public void ReceiveAsync()
        {
            if (Receiving) return;
            if (!IsStarted) return;

            try
            {
                // Async receive with the receive handler
                Receiving = true;
                ReceiveEventArg.RemoteEndPoint = ReceiveEndpoint;
                ReceiveEventArg.SetBuffer(ReceiveBuffer.Arrays, 0, ReceiveBuffer.Capacity);
                if (!Socket.ReceiveFromAsync(ReceiveEventArg))
                    ProcessReceiveFrom(ReceiveEventArg);
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <inheritdoc />
        public int Receive(byte[] buffer, int offset, int size)
        {
            if (!IsStarted) return 0;
            if (size == 0 || buffer is null) return 0;

            try
            {
                // Receive datagram from the client
                var sessionEndpoint = Endpoint;
                var received = Socket.ReceiveFrom(buffer, offset, size, SocketFlags.None, ref sessionEndpoint);
                Endpoint = sessionEndpoint;

                // Update statistic
                DatagramsReceived++;
                BytesReceived += received;

                // Call the datagram received handler
                OnReceived(Endpoint, buffer, offset, size);

                return received;
            }
            catch (ObjectDisposedException)
            {
                return 0;
            }
            catch (SocketException ex)
            {
                SendError(ex.SocketErrorCode);
                return 0;
            }
        }
    }
}