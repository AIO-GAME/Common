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
        // Send buffer
        private readonly object SendLock = new object();

        private NetBuffer _sendNetBufferMain;

        private NetBuffer _sendNetBufferFlush;

        private bool Sending;

        private SocketAsyncEventArgs SendEventArg;

        private long SendBufferFlushOffset;

        /// <summary>
        /// Send data to the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send as a span of bytes</param>
        /// <returns>Size of sent data</returns>
        public virtual int Send(byte[] buffer)
        {
            if (!IsConnected)
                return 0;

            if (buffer is null || buffer.Length == 0)
                return 0;

            // Sent data to the client
            var sent = Socket.Send(buffer, 0, buffer.Length, SocketFlags.None, out var ec);
            if (sent > 0)
            {
                // Update statistic
                BytesSent += sent;
                Interlocked.Add(ref Server._bytesSent, sent);

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
        /// Send data to the client (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the session is not connected</returns>
        public bool SendAsync(byte[] buffer)
        {
            if (!IsConnected) return false;

            if (buffer is null || buffer.Length == 0) return true;

            lock (SendLock)
            {
                // Check the send buffer limit
                if (((_sendNetBufferMain.Count + buffer.Length) > OptionSendBufferLimit) && (OptionSendBufferLimit > 0))
                {
                    SendError(SocketError.NoBufferSpaceAvailable);
                    return false;
                }

                // Fill the main send buffer
                _sendNetBufferMain.Write(buffer);

                // Update statistic
                BytesPending = _sendNetBufferMain.Count;

                // Avoid multiple send handlers
                if (Sending) return true;
                Sending = true;

                // Try to send the main buffer
                TrySend();
            }

            return true;
        }

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
                    if (_sendNetBufferFlush.IsEmpty)
                    {
                        // Swap flush and main buffers
                        _sendNetBufferFlush = Interlocked.Exchange(ref _sendNetBufferMain, _sendNetBufferFlush);
                        SendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += _sendNetBufferFlush.Count;

                        // Check if the flush buffer is empty
                        if (_sendNetBufferFlush.IsEmpty)
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
                    SendEventArg.SetBuffer(_sendNetBufferFlush.Arrays, (int)SendBufferFlushOffset,
                        (int)(_sendNetBufferFlush.Count - SendBufferFlushOffset));
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
            if (!IsConnected)
                return false;

            long size = e.BytesTransferred;

            // Send some data to the client
            if (size > 0)
            {
                // Update statistic
                BytesSending -= size;
                BytesSent += size;
                Interlocked.Add(ref Server._bytesSent, size);

                // Increase the flush buffer offset
                SendBufferFlushOffset += size;

                // Successfully send the whole flush buffer
                if (SendBufferFlushOffset == _sendNetBufferFlush.Count)
                {
                    // Clear the flush buffer
                    _sendNetBufferFlush.Clear();
                    SendBufferFlushOffset = 0;
                }

                // Call the buffer sent handler
                OnSent(size, BytesPending + BytesSending);
            }

            // Try to send again if the session is valid
            if (e.SocketError == SocketError.Success)
                return true;
            else
            {
                SendError(e.SocketError);
                Disconnect();
                return false;
            }
        }
    }
}