/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-06
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net.Sockets;
using System.Text;

namespace AIO.Net
{
    public partial class TcpClient
    {
        // Receive buffer
        private bool _receiving;
        private Buffer _receiveBuffer;

        private SocketAsyncEventArgs _receiveEventArg;

        /// <summary>
        /// Receive data from the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <returns>Size of received data</returns>
        public long Receive(byte[] buffer)
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
        public virtual long Receive(byte[] buffer, int offset, int size)
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
        /// Receive text from the server (synchronous)
        /// </summary>
        /// <param name="size">Text size to receive</param>
        /// <returns>Received text</returns>
        public string Receive(long size)
        {
            var buffer = new byte[size];
            var length = Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, (int)length);
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
            if (_receiving)
                return;

            if (!IsConnected)
                return;

            var process = true;

            while (process)
            {
                process = false;

                try
                {
                    // Async receive with the receive handler
                    _receiving = true;
                    _receiveEventArg.SetBuffer(_receiveBuffer.Data, 0,
                        _receiveBuffer.Capacity);

                    if (!Socket.ReceiveAsync(_receiveEventArg))
                        process = ProcessReceive(_receiveEventArg);
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
            if (!IsConnected)
                return false;

            var size = e.BytesTransferred;
            // Received some data from the server
            if (size > 0)
            {
                // Update statistic
                BytesReceived += size;

                // Call the buffer received handler
                OnReceived(_receiveBuffer.Data, 0, size);
                // If the receive buffer is full increase its size
                if (_receiveBuffer.Capacity == size)
                {
                    // Check the receive buffer limit
                    if (2 * size > OptionReceiveBufferLimit && OptionReceiveBufferLimit > 0)
                    {
                        SendError(SocketError.NoBufferSpaceAvailable);
                        DisconnectAsync();
                        return false;
                    }

                    _receiveBuffer.Reserve(2 * size);
                }
            }

            _receiving = false;

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