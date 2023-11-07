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
        private readonly object SendLock = new object();
        private bool Sending;
        private Buffer SendBufferMain;
        private Buffer SendBufferFlush;
        private SocketAsyncEventArgs SendEventArg;
        private long SendBufferFlushOffset;

        #region Send Synchronous

        /// <summary>
        /// Send data to the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of sent data</returns>
        public virtual long Send(byte[] buffer, int offset, int size)
        {
            var data = new byte[size - offset];
            Array.Copy(data, offset, data, 0, size);
            return Send(data);
        }

        /// <summary>
        /// Send data to the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>Size of sent data</returns>
        public long Send(byte[] buffer)
        {
            if (!IsConnected)
                return 0;

            if (buffer is null || buffer.Length == 0)
                return 0;

            // Sent data to the server
            long sent = Socket.Send(buffer, 0, buffer.Length, SocketFlags.None, out var ec);
            if (sent > 0)
            {
                // Update statistic
                BytesSent += sent;

                // Call the buffer sent handler
                OnSent(sent, BytesPending + BytesSending);
            }

            // Check for socket error
            if (ec != SocketError.Success)
            {
                SendError(ec);
                Disconnect();
            }

            return sent;
        }

        /// <summary>
        /// Send text to the server (synchronous)
        /// </summary>
        /// <param name="text">Text to send as a span of characters</param>
        /// <returns>Size of sent text</returns>
        public long Send(string text)
        {
            var data = new BufferByte();
            data.WriteStringUTF8(text);
            return Send(data.ToArray());
        }

        /// <summary>
        /// Send text to the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>Size of sent text</returns>
        public long Send(BufferByte buffer)
        {
            return Send(buffer.ToArray());
        }

        #endregion

        #region Send Asynchronous

        /// <summary>
        /// Send data to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
        public bool SendAsync(byte[] buffer)
        {
            if (!IsConnected) return false;

            if (buffer is null || buffer.Length == 0) return true;

            lock (SendLock)
            {
                // Check the send buffer limit
                if (SendBufferMain.Count + buffer.Length > Option.SendBufferLimit && Option.SendBufferLimit > 0)
                {
                    SendError(SocketError.NoBufferSpaceAvailable);
                    return false;
                }

                // Fill the main send buffer
                // SendBufferMain.Append(buffer);
                SendBufferMain.Write(buffer);

                // Update statistic
                BytesPending = SendBufferMain.Count;

                // Avoid multiple send handlers
                if (Sending) return true;

                Sending = true;

                // Try to send the main buffer
                TrySend();
            }

            return true;
        }

        /// <summary>
        /// Send data to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
        public bool SendAsync(byte[] buffer, int offset, int size)
        {
            var data = new byte[size - offset];
            Array.Copy(data, offset, data, 0, size);
            return SendAsync(data);
        }

        /// <summary>
        /// Send text to the server (asynchronous)
        /// </summary>
        /// <param name="text">Text to send as a span of characters</param>
        /// <returns>'true' if the text was successfully sent, 'false' if the client is not connected</returns>
        public bool SendAsync(string text)
        {
            var buffer = new BufferByte();
            buffer.WriteStringUTF8(text);
            return SendAsync(buffer.ToArray());
        }

        /// <summary>
        /// Send text to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>'true' if the text was successfully sent, 'false' if the client is not connected</returns>
        public bool SendAsync(BufferByte buffer)
        {
            return SendAsync(buffer.ToArray());
        }

        #endregion


        /// <summary>
        /// Try to send pending data
        /// </summary>
        private void TrySend()
        {
            if (!IsConnected) return;

            var empty = false;
            var process = true;

            while (process)
            {
                process = false;

                lock (SendLock)
                {
                    // Is previous socket send in progress?
                    if (SendBufferFlush.IsEmpty)
                    {
                        // Swap flush and main buffers
                        SendBufferFlush = Interlocked.Exchange(ref SendBufferMain, SendBufferFlush);
                        SendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += SendBufferFlush.Count;

                        // Check if the flush buffer is empty
                        if (SendBufferFlush.IsEmpty)
                        {
                            // Need to call empty send buffer handler
                            empty = true;

                            // End sending process
                            Sending = false;
                        }
                    }
                    else
                        return;
                }

                // Call the empty send buffer handler
                if (empty)
                {
                    OnEmpty();
                    return;
                }

                try
                {
                    // Async write with the write handler
                    SendEventArg.SetBuffer(SendBufferFlush.Arrays, (int)SendBufferFlushOffset,
                        (int)(SendBufferFlush.Count - SendBufferFlushOffset));
                    if (!Socket.SendAsync(SendEventArg))
                        process = ProcessSend(SendEventArg);
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /// <summary>
        /// This method is invoked when an asynchronous send operation completes
        /// </summary>
        private bool ProcessSend(SocketAsyncEventArgs e)
        {
            if (!IsConnected) return false;

            var size = e.BytesTransferred;

            // Send some data to the server
            if (size > 0)
            {
                // Update statistic
                BytesSending -= size;
                BytesSent += size;

                // Increase the flush buffer offset
                SendBufferFlushOffset += size;

                // Successfully send the whole flush buffer
                if (SendBufferFlushOffset == SendBufferFlush.Count)
                {
                    // Clear the flush buffer
                    SendBufferFlush.Clear();
                    SendBufferFlushOffset = 0;
                }

                // Call the buffer sent handler
                OnSent(size, BytesPending + BytesSending);
            }

            // Try to send again if the client is valid
            if (e.SocketError == SocketError.Success)
                return true;
            else
            {
                SendError(e.SocketError);
                DisconnectAsync();
                return false;
            }
        }
    }
}