#region

using System;
using System.Net;
using System.Net.Sockets;

#endregion

namespace AIO.Net
{
    public partial class UdpServer
    {
        private NetBuffer _sendNetBuffer;
        private EndPoint  SendEndpoint;

        private SocketAsyncEventArgs SendEventArg;

        private bool Sending;

        #region INetSession Members

        /// <inheritdoc />
        public bool SendAsync(byte[] buffer)
        {
            if (Sending) return false;
            if (!IsStarted) return false;
            if (buffer is null || buffer.Length == 0) return true;

            // Check the send buffer limit
            if (_sendNetBuffer.Count + buffer.Length > Option.SendBufferLimit && Option.SendBufferLimit > 0)
            {
                SendError(SocketError.NoBufferSpaceAvailable);
                return false;
            }

            // Fill the main send buffer
            _sendNetBuffer.Write(buffer);

            // Update statistic
            BytesSending = _sendNetBuffer.Count;

            // Update send endpoint
            SendEndpoint = Endpoint;

            // Try to send the main buffer
            TrySend();

            return true;
        }

        /// <inheritdoc />
        public int Send(byte[] buffer)
        {
            if (!IsStarted)
                return 0;

            if (buffer is null || buffer.Length == 0)
                return 0;

            try
            {
                // Sent datagram to the client
                var sent = Socket.SendTo(buffer, SocketFlags.None, Endpoint);
                if (sent > 0)
                {
                    // Update statistic
                    DatagramsSent++;
                    BytesSent += sent;

                    // Call the datagram sent handler
                    OnSent(Endpoint, sent);
                }

                return sent;
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

        #endregion

        /// <inheritdoc />
        public override bool MulticastAsync(byte[] buffer)
        {
            return SendAsync(buffer);
        }

        /// <inheritdoc />
        public override int Multicast(byte[] buffer)
        {
            return Send(buffer);
        }

        /// <summary>
        /// This method is invoked when an asynchronous send to operation completes
        /// </summary>
        private void ProcessSendTo(SocketAsyncEventArgs e)
        {
            Sending = false;

            if (!IsStarted)
                return;

            // Check for error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);

                // Call the buffer sent zero handler
                OnSent(SendEndpoint, 0);

                return;
            }

            var sent = e.BytesTransferred;

            // Send some data to the client
            if (sent > 0)
            {
                // Update statistic
                BytesSending =  0;
                BytesSent    += sent;

                // Clear the send buffer
                _sendNetBuffer.Clear();

                // Call the buffer sent handler
                OnSent(SendEndpoint, sent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void TrySend()
        {
            if (SendEndpoint is null) return;

            if (Sending) return;

            if (!IsStarted) return;

            try
            {
                // Async write with the write handler
                Sending                     = true;
                SendEventArg.RemoteEndPoint = SendEndpoint;
                SendEventArg.SetBuffer(_sendNetBuffer.Arrays, 0, _sendNetBuffer.Count);
                if (!Socket.SendToAsync(SendEventArg))
                    ProcessSendTo(SendEventArg);
            }
            catch (ObjectDisposedException) { }
        }
    }
}