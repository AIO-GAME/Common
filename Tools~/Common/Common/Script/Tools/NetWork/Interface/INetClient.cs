#region

using System;
using System.Net;

#endregion

namespace AIO.Net
{
    /// <summary>
    /// INetClient interface
    /// </summary>
    public interface INetClient : IDisposable
    {
        /// <summary>
        /// Connect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        bool Connect();

        /// <summary>
        /// Disconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        bool Disconnect();

        /// <summary>
        /// Reconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully reconnected, 'false' if the client is already reconnected</returns>
        bool Reconnect();

        /// <summary>
        /// Receive datagram from the server (asynchronous)
        /// </summary>
        void ReceiveAsync();

        #region Send

        /// <summary>
        /// Send datagram to the connected server (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of sent datagram</returns>
        int Send(byte[] buffer, int offset, int size);

        /// <summary>
        /// Send datagram to the connected server (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send as a span of bytes</param>
        /// <returns>Size of sent datagram</returns>
        int Send(byte[] buffer);

        /// <summary>
        /// Send text to the connected server (synchronous)
        /// </summary>
        /// <param name="data">Data to send</param>
        /// <returns>Size of sent datagram</returns>
        int Send<T>(T data);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of sent datagram</returns>
        int Send(EndPoint endpoint, byte[] buffer, int offset, int size);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="data">Datagram buffer to send</param>
        /// <returns>Size of sent datagram</returns>
        int Send<T>(EndPoint endpoint, T data);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <returns>Size of sent datagram</returns>
        int Send(EndPoint endpoint, byte[] buffer);

        #endregion

        #region SendAsync

        /// <summary>
        /// Send datagram to the connected server (asynchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>'true' if the datagram was successfully sent, 'false' if the datagram was not sent</returns>
        bool SendAsync(byte[] buffer, int offset, int size);

        /// <summary>
        /// Send datagram to the connected server (asynchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send as a span of bytes</param>
        /// <returns>'true' if the datagram was successfully sent, 'false' if the datagram was not sent</returns>
        bool SendAsync(byte[] buffer);

        /// <summary>
        /// Send text to the connected server (asynchronous)
        /// </summary>
        /// <param name="data">Data to send</param>
        /// <returns>Size of sent datagram</returns>
        bool SendAsync<T>(T data);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of sent datagram</returns>
        bool SendAsync(EndPoint endpoint, byte[] buffer, int offset, int size);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="data">Datagram buffer to send</param>
        /// <returns>Size of sent datagram</returns>
        bool SendAsync<T>(EndPoint endpoint, T data);

        /// <summary>
        /// Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <returns>Size of sent datagram</returns>
        bool SendAsync(EndPoint endpoint, byte[] buffer);

        #endregion
    }
}