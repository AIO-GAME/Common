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
    public partial class UdpClient
    {
        /// <inheritdoc />
        public override int Receive(ref EndPoint endpoint, byte[] buffer, int offset, int size)
        {
            if (!IsConnected)
                return 0;

            if (size == 0)
                return 0;

            try
            {
                // Receive datagram from the server
                var received = Socket.ReceiveFrom(buffer, offset, size, SocketFlags.None, ref endpoint);

                // Update statistic
                DatagramsReceived++;
                BytesReceived += received;

                // Call the datagram received handler
                OnReceived(endpoint, buffer, offset, size);

                return received;
            }
            catch (ObjectDisposedException)
            {
                return 0;
            }
            catch (SocketException ex)
            {
                SendError(ex.SocketErrorCode);
                Disconnect();
                return 0;
            }
        }

        /// <inheritdoc />
        protected override void TryReceive()
        {
            if (Receiving) return;

            if (!IsConnected) return;

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
    }
}