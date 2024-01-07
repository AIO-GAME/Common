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
    public partial class TcpSession
    {
        private bool Receiving;

        private NetBuffer _receiveNetBuffer;

        private SocketAsyncEventArgs ReceiveEventArg;

        /// <summary>
        /// Receive data from the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of received data</returns>
        public virtual int Receive(byte[] buffer, int offset, int size)
        {
            if (!IsConnected) return 0;

            if (size == 0) return 0;

            // Receive data from the client
            var received = Socket.Receive(buffer, offset, size, SocketFlags.None, out var ec);
            if (received > 0)
            {
                // Update statistic
                BytesReceived += received;
                Interlocked.Add(ref Server._bytesReceived, received);

                // Call the buffer received handler
                OnReceived(buffer, 0, received);
            }

            // Check for socket error
            if (ec != SocketError.Success)
            {
                SendError(ec);
                Disconnect();
            }

            return received;
        }

        /// <summary>
        /// Receive data from the client (asynchronous)
        /// </summary>
        public virtual void ReceiveAsync()
        {
            if (Receiving) return;
            if (!IsConnected) return;

            var process = true;
            while (process)
            {
                process = false;

                try
                {
                    // Async receive with the receive handler
                    Receiving = true;
                    ReceiveEventArg.SetBuffer(_receiveNetBuffer.Arrays, 0, _receiveNetBuffer.Capacity);
                    if (Socket.ReceiveAsync(ReceiveEventArg)) continue;
                    process = ProcessReceive(ReceiveEventArg);
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /// <summary>
        /// This method is invoked when an asynchronous receive operation completes
        /// </summary>
        private bool ProcessReceive(SocketAsyncEventArgs e)
        {
            if (!IsConnected) return false;

            var size = e.BytesTransferred;
            if (size > 0) // Received some data from the client
            {
                // Update statistic
                BytesReceived += size;
                Interlocked.Add(ref Server._bytesReceived, size);

                // Call the buffer received handler
                OnReceived(_receiveNetBuffer.Arrays, 0, _receiveNetBuffer.Capacity);

                // If the receive buffer is full increase its size
                if (_receiveNetBuffer.Capacity == size)
                {
                    // Check the receive buffer limit
                    if (2 * size > OptionReceiveBufferLimit && OptionReceiveBufferLimit > 0)
                    {
                        SendError(SocketError.NoBufferSpaceAvailable);
                        Disconnect();
                        return false;
                    }

                    _receiveNetBuffer.Reserve(2 * size);
                }
            }

            Receiving = false;

            // Try to receive again if the session is valid
            if (e.SocketError == SocketError.Success)
            {
                // If zero is returned from a read operation, the remote end has closed the connection
                if (size > 0) return true;
                Disconnect();
            }
            else
            {
                SendError(e.SocketError);
                Disconnect();
            }

            return false;
        }
    }
}