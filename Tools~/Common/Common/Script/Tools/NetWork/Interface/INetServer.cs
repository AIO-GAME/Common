/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace AIO.Net
{
    /// <summary>
    /// Net Server Extend
    /// </summary>
    public static class ExtendNetServer
    {
        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="server">Server</param>
        /// <param name="endpoint">Endpoint to receive from</param>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <returns>Size of received datagram</returns>
        public static int Receive(this INetServer server, INetSession endpoint, byte[] buffer)
        {
            return server.Receive(endpoint, buffer, 0, buffer.Length);
        }
    }

    /// <summary>
    /// INetServer interface
    /// </summary>
    public interface INetServer : IDisposable
    {
        /// <summary>
        /// Send datagram to the given endpoint (asynchronous)
        /// </summary>
        /// <param name="session">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send as a span of bytes</param>
        /// <returns>'true' if the datagram was successfully sent, 'false' if the datagram was not sent</returns>
        bool SendAsync(INetSession session, byte[] buffer);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="session">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send as a span of bytes</param>
        /// <returns>Size of sent datagram</returns>
        int Send(INetSession session, byte[] buffer);

        /// <summary>
        /// Multicast datagram to the prepared multicast endpoint (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <returns>Size of multicasted datagram</returns>
        int Multicast(byte[] buffer);

        /// <summary>
        /// Multicast datagram to the prepared multicast endpoint (asynchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <returns>'true' if the datagram was successfully multicasted, 'false' if the datagram was not multicasted</returns>
        bool MulticastAsync(byte[] buffer);

        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="session">Endpoint to receive from</param>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of received datagram</returns>
        int Receive(INetSession session, byte[] buffer, int offset, int size);

        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="session">Endpoint to receive from</param>
        /// <returns>Size of received datagram</returns>
        void ReceiveAsync(INetSession session);
    }
}