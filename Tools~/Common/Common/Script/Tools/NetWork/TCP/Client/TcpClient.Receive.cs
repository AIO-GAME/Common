/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-06
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net.Sockets;

namespace AIO.Net
{
    public partial class TcpClient
    {
        /// <summary>
        /// Receiving flag
        /// </summary>
        private bool Receiving;

        /// <summary>
        /// Receive buffer
        /// </summary>
        private NetBuffer _receiveNetBuffer;

        /// <summary>
        /// Receive event args
        /// </summary>
        private SocketAsyncEventArgs ReceiveEventArg;

        /// <summary>
        /// Receive data from the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <returns>Size of received data</returns>
        public int Receive(byte[] buffer)
        {
            return Receive(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Receive data from the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of received data</returns>
        public virtual int Receive(byte[] buffer, int offset, int size)
        {
            if (!IsConnected)
                return 0;

            if (buffer is null || buffer.Length == 0 || size == 0 || size - offset <= 0)
                return 0;

            // Receive data from the server
            var received = Socket.Receive(buffer, offset, size, SocketFlags.None, out var ec);
            if (received > 0)
            {
                // Update statistic
                BytesReceived += received;
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
        /// Receive data from the server (asynchronous)
        /// </summary>
        public virtual void ReceiveAsync()
        {
            // Try to receive data from the server
            TryReceive();
        }

        /// <summary>
        /// Try to receive new data
        /// </summary>
        private void TryReceive()
        {
            if (Receiving) return;
            if (!IsConnected) return;

            var process = true;
            while (process)
            {
                process = false;

                try // Async receive with the receive handler
                {
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
            if (size > 0) // Received some data from the server
            {
                // Update statistic
                BytesReceived += size;

                // Call the buffer received handler
                OnReceived(_receiveNetBuffer.Arrays, 0, _receiveNetBuffer.Capacity);
                // OnReceived(ReceiveBuffer.Arrays, 0, size);
                // If the receive buffer is full increase its size
                if (_receiveNetBuffer.Capacity == size)
                {
                    // Check the receive buffer limit
                    if (2 * size > Option.ReceiveBufferLimit && Option.ReceiveBufferLimit > 0)
                    {
                        SendError(SocketError.NoBufferSpaceAvailable);
                        DisconnectAsync();
                        return false;
                    }

                    _receiveNetBuffer.Reserve(2 * size);
                }
            }

            Receiving = false;

            // Try to receive again if the client is valid
            if (e.SocketError == SocketError.Success)
            {
                // If zero is returned from a read operation, the remote end has closed the connection
                if (size > 0) return true;
                DisconnectAsync();
            }
            else
            {
                SendError(e.SocketError);
                DisconnectAsync();
            }

            return false;
        }
    }
}