/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;

#pragma warning disable CS1591
namespace AIO.Net
{
    public static class ExtendNetSession
    {
        public static int Send<C>(this C session, byte[] buffer, int offset, int size) where C : INetSession
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return session.Send(data);
        }

        public static int Send<C>(this C session, byte[] buffer) where C : INetSession
        {
            return session.Send(buffer);
        }

        public static int Send<C, T>(this C session, T data) where C : INetSession
        {
            return session.Send(AHelper.Binary.Serialize(data));
        }

        public static bool SendAsync<C>(this C session, byte[] buffer) where C : INetSession
        {
            return session.SendAsync(buffer);
        }

        public static bool SendAsync<C>(this C session, byte[] buffer, int offset, int size) where C : INetSession
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return session.SendAsync(data);
        }

        public static bool SendAsync<C, T>(this C session, T data) where C : INetSession
        {
            return session.SendAsync(AHelper.Binary.Serialize(data));
        }

        /// <summary>
        /// Multicast datagram to the prepared multicast endpoint (synchronous)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of multicasted datagram</returns>
        public static int Multicast(this INetSession session, byte[] buffer, long offset, long size)
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return session.Multicast(data);
        }

        /// <summary>
        /// Multicast text to the prepared multicast endpoint (synchronous)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data">data to multicast</param>
        /// <returns>Size of multicasted datagram</returns>
        public static int Multicast<T>(this INetSession session, T data) =>
            session.Multicast(AHelper.Binary.Serialize(data));

        /// <summary>
        /// Multicast datagram to the prepared multicast endpoint (asynchronous)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>'true' if the datagram was successfully multicasted, 'false' if the datagram was not multicasted</returns>
        public static bool MulticastAsync(this INetSession session, byte[] buffer, long offset, long size)
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return session.MulticastAsync(data);
        }

        /// <summary>
        /// Multicast text to the prepared multicast endpoint (asynchronous)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data">data to multicast</param>
        /// <returns>'true' if the text was successfully multicasted, 'false' if the text was not multicasted</returns>
        public static bool MulticastAsync<C, T>(this C session, T data) where C : INetSession =>
            session.MulticastAsync(AHelper.Binary.Serialize(data));
    }

    /// <summary>
    /// nameof(NetSession)
    /// </summary>
    public interface INetSession : IDisposable
    {
        /// <summary>
        /// Server Id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Send datagram to the given endpoint (asynchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send as a span of bytes</param>
        /// <returns>'true' if the datagram was successfully sent, 'false' if the datagram was not sent</returns>
        int Send(byte[] buffer);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send as a span of bytes</param>
        /// <returns>Size of sent datagram</returns>
        bool SendAsync(byte[] buffer);

        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of received datagram</returns>
        int Receive(byte[] buffer, int offset, int size);

        /// <summary>
        /// Receive datagram from the client (asynchronous)
        /// </summary>
        void ReceiveAsync();
    }
}