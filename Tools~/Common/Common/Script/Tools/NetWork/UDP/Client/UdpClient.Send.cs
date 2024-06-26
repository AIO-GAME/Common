﻿#region

using System;
using System.Net;
using System.Net.Sockets;

#endregion

namespace AIO.Net
{
    public partial class UdpClient
    {
        /// <inheritdoc />
        protected override void TrySend()
        {
            if (Sending) return;
            if (!IsConnected) return;

            try
            {
                // Async write with the write handler
                Sending                     = true;
                SendEventArg.RemoteEndPoint = SendEndpoint;
                SendEventArg.SetBuffer(SendNetBuffer.Arrays, 0, SendNetBuffer.Count);
                if (!Socket.SendToAsync(SendEventArg))
                    ProcessSendTo(SendEventArg);
            }
            catch (ObjectDisposedException) { }
        }

        /// <inheritdoc />
        public override int Send(EndPoint endpoint, byte[] buffer)
        {
            if (!IsConnected) return 0;
            if (buffer is null || buffer.Length == 0) return 0;

            try
            {
                // Sent datagram to the server
                var sent = Socket.SendTo(buffer, SocketFlags.None, endpoint);
                if (sent > 0)
                {
                    // Update statistic
                    DatagramsSent++;
                    BytesSent += sent;

                    // Call the datagram sent handler
                    OnSent(endpoint, sent);
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
                Disconnect();
                return 0;
            }
        }

        /// <inheritdoc />
        public override bool SendAsync(EndPoint endpoint, byte[] buffer)
        {
            if (Sending) return false;
            if (!IsConnected) return false;
            if (buffer is null || buffer.Length == 0) return true;

            // Check the send buffer limit
            if (SendNetBuffer.Count + buffer.Length > Option.SendBufferLimit && Option.SendBufferLimit > 0)
            {
                SendError(SocketError.NoBufferSpaceAvailable);
                return false;
            }

            // Fill the main send buffer
            SendNetBuffer.Write(buffer);

            // Update statistic
            BytesSending = SendNetBuffer.Count;

            // Update send endpoint
            SendEndpoint = endpoint;

            // Try to send the main buffer
            TrySend();

            return true;
        }
    }
}