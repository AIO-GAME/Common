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
        private readonly object _sendLock = new object();
        private bool _sending;
        private Buffer _sendBufferMain;
        private Buffer _sendBufferFlush;
        private SocketAsyncEventArgs _sendEventArg;
        private long _sendBufferFlushOffset;

        /// <summary>
        /// Send data to the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send as a span of bytes</param>
        /// <returns>Size of sent data</returns>
        public virtual long Send(byte[] buffer)
        {
            if (!IsConnected)
                return 0;

            if (buffer is null || buffer.Length == 0)
                return 0;

            // Sent data to the client
            long sent = Socket.Send(buffer, 0, buffer.Length, SocketFlags.None, out var ec);
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
        /// Send text to the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of sent data</returns>
        public long Send(byte[] buffer, int offset, int size)
        {
            var target = new byte[size - offset];
            Array.ConstrainedCopy(buffer, offset, target, 0, size);
            return Send(target);
        }

        /// <summary>
        /// Send text to the client (synchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>Size of sent data</returns>
        public virtual long Send(string text)
        {
            var buffer = new BufferByte();
            buffer.WriteStringUTF8(text);
            return Send(buffer.ToArray());
        }

        /// <summary>
        /// Send data to the client (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the session is not connected</returns>
        public bool SendAsync(byte[] buffer)
        {
            if (!IsConnected)
                return false;

            if (buffer is null || buffer.Length == 0)
                return true;

            lock (_sendLock)
            {
                // Check the send buffer limit
                if (((_sendBufferMain.Size + buffer.Length) > OptionSendBufferLimit) && (OptionSendBufferLimit > 0))
                {
                    SendError(SocketError.NoBufferSpaceAvailable);
                    return false;
                }

                // Fill the main send buffer
                _sendBufferMain.Append(buffer);

                // Update statistic
                BytesPending = _sendBufferMain.Size;

                // Avoid multiple send handlers
                if (_sending)
                    return true;
                else
                    _sending = true;

                // Try to send the main buffer
                TrySend();
            }

            return true;
        }

        /// <summary>
        /// Send data to the client (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the session is not connected</returns>
        public bool SendAsync(byte[] buffer, int offset, int size)
        {
            var target = new byte[size - offset];
            Array.ConstrainedCopy(buffer, offset, target, 0, size);
            return SendAsync(target);
        }

        /// <summary>
        /// Send text to the client (asynchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>'true' if the text was successfully sent, 'false' if the session is not connected</returns>
        public bool SendAsync(string text)
        {
            var buffer = new BufferByte();
            buffer.WriteStringUTF8(text);
            return SendAsync(buffer);
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

                lock (_sendLock)
                {
                    // Is previous socket send in progress?
                    if (_sendBufferFlush.IsEmpty)
                    {
                        // Swap flush and main buffers
                        _sendBufferFlush = Interlocked.Exchange(ref _sendBufferMain, _sendBufferFlush);
                        _sendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += _sendBufferFlush.Size;

                        // Check if the flush buffer is empty
                        if (_sendBufferFlush.IsEmpty)
                        {
                            // Need to call empty send buffer handler
                            empty = true;

                            // End sending process
                            _sending = false;
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
                    _sendEventArg.SetBuffer(_sendBufferFlush.Data, (int)_sendBufferFlushOffset,
                        (int)(_sendBufferFlush.Size - _sendBufferFlushOffset));
                    if (!Socket.SendAsync(_sendEventArg))
                        process = ProcessSend(_sendEventArg);
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }
    }
}