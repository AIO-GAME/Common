/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Net.Sockets;

namespace AIO.Net
{
    /// <summary>
    /// Net client interface
    /// </summary>
    public abstract class NetClient : INetClient
    {
        /// <summary>
        /// Client socket
        /// </summary>
        private NetClient()
        {
        }

        /// <summary>
        /// Create a new Net client with a given server address and port
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="address">Server address</param>
        /// <param name="port">Server port</param>
        protected NetClient(EndPoint endpoint, string address, int port)
        {
            Id = Guid.NewGuid();
            Address = address;
            Port = port;
            Endpoint = endpoint;
        }

        /// <summary>
        /// Initialize Net client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        protected NetClient(IPAddress address, int port) : this(new IPEndPoint(address, port))
        {
        }

        /// <summary>
        /// Initialize Net client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        protected NetClient(string address, int port) : this(new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <summary>
        /// Initialize Net client with a given DNS endpoint
        /// </summary>
        /// <param name="endpoint">DNS endpoint</param>
        protected NetClient(DnsEndPoint endpoint) : this(endpoint, endpoint.Host, endpoint.Port)
        {
        }

        /// <summary>
        /// Initialize Net client with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        protected NetClient(IPEndPoint endpoint) : this(endpoint, endpoint.Address.ToString(), endpoint.Port)
        {
        }

        /// <summary>
        /// Client Id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// UDP server address
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// UDP server port
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Endpoint
        /// </summary>
        public EndPoint Endpoint { get; private set; }

        /// <summary>
        /// Socket
        /// </summary>
        public Socket Socket { get; protected set; }

        /// <summary>
        /// Number of bytes pending sent by the client
        /// </summary>
        public long BytesPending { get; protected set; }

        /// <summary>
        /// Number of bytes sending by the client
        /// </summary>
        public long BytesSending { get; protected set; }

        /// <summary>
        /// Number of bytes sent by the client
        /// </summary>
        public long BytesSent { get; protected set; }

        /// <summary>
        /// Number of bytes received by the client
        /// </summary>
        public long BytesReceived { get; protected set; }

        /// <summary>
        /// Number of datagrams sent by the client
        /// </summary>
        public long DatagramsSent { get; protected set; }

        /// <summary>
        /// Number of datagrams received by the client
        /// </summary>
        public long DatagramsReceived { get; protected set; }

        #region Handle Events

        /// <summary>
        /// Handle client connecting notification
        /// </summary>
        protected virtual void OnConnecting()
        {
        }

        /// <summary>
        /// Handle client connected notification
        /// </summary>
        protected virtual void OnConnected()
        {
        }

        /// <summary>
        /// Handle client disconnecting notification
        /// </summary>
        protected virtual void OnDisconnecting()
        {
        }

        /// <summary>
        /// Handle client disconnected notification
        /// </summary>
        protected virtual void OnDisconnected()
        {
        }

        /// <summary>
        /// Handle error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void OnError(SocketError error)
        {
        }

        #endregion

        /// <summary>
        /// Send error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void SendError(SocketError error)
        {
            OnError(error);
        }

        #region Connect/Disconnect client

        /// <summary>
        /// Is the client connected?
        /// </summary>
        public bool IsConnected { get; protected set; }

        /// <summary>
        /// Disconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        public abstract bool Disconnect();

        /// <summary>
        /// Connect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public abstract bool Connect();

        /// <summary>
        /// Reconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully reconnected, 'false' if the client is already reconnected</returns>
        public bool Reconnect()
        {
            return Disconnect() && Connect();
        }

        /// <inheritdoc />
        public void ReceiveAsync()
        {
            // Try to receive datagram
            TryReceive();
        }

        /// <summary>
        /// Try to receive new data
        /// </summary>
        protected virtual void TryReceive()
        {
        }

        /// <summary>
        /// Clear send/receive buffers
        /// </summary>
        protected virtual void ClearBuffers()
        {
        }

        /// <summary>
        /// Create a new socket object
        /// </summary>
        /// <remarks>
        /// Method may be override if you need to prepare some specific socket object in your implementation.
        /// </remarks>
        /// <returns>Socket object</returns>
        protected abstract Socket CreateSocket();

        #endregion

        #region Receive

        /// <summary>
        /// Endpoint to receive from
        /// </summary>
        protected EndPoint ReceiveEndpoint;

        /// <summary>
        /// Receiving flag
        /// </summary>
        protected bool Receiving;

        /// <summary>
        /// Receive event arguments
        /// </summary>
        protected SocketAsyncEventArgs ReceiveEventArg;

        /// <summary>
        /// Receive buffer
        /// </summary>
        internal Buffer ReceiveBuffer;

        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <returns>Size of received datagram</returns>
        public long Receive(byte[] buffer)
        {
            return Receive(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of received datagram</returns>
        public virtual int Receive(byte[] buffer, int offset, int size)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to receive from</param>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <returns>Size of received datagram</returns>
        public int Receive(ref EndPoint endpoint, byte[] buffer)
        {
            return Receive(ref endpoint, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to receive from</param>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of received datagram</returns>
        public virtual int Receive(ref EndPoint endpoint, byte[] buffer, int offset, int size)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Send

        /// <summary>
        /// Endpoint to send to
        /// </summary>
        protected EndPoint SendEndpoint;

        /// <summary>
        /// Sending flag
        /// </summary>
        protected bool Sending;

        /// <summary>
        /// Send event arguments
        /// </summary>
        protected SocketAsyncEventArgs SendEventArg;

        /// <summary>
        /// Send buffer
        /// </summary>
        internal Buffer SendBuffer;

        /// <summary>
        /// Try to send pending data
        /// </summary>
        protected virtual void TrySend()
        {
        }

        /// <inheritdoc />
        public virtual bool SendAsync(EndPoint endpoint, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual int Send(EndPoint endpoint, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Send(byte[] buffer, int offset, int size)
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return Send(Endpoint, data);
        }

        /// <inheritdoc />
        public int Send(byte[] buffer)
        {
            return Send(Endpoint, buffer);
        }

        /// <inheritdoc />
        public int Send(EndPoint endpoint, byte[] buffer, int offset, int size)
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return Send(endpoint, data);
        }

        /// <inheritdoc />
        public int Send<T>(EndPoint endpoint, T data)
        {
            return Send(endpoint, AHelper.Binary.Serialize(data));
        }

        /// <inheritdoc />
        public int Send<T>(T data)
        {
            return Send(Endpoint, AHelper.Binary.Serialize(data));
        }

        /// <inheritdoc />
        public bool SendAsync(byte[] buffer, int offset, int size)
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return SendAsync(Endpoint, data);
        }

        /// <inheritdoc />
        public bool SendAsync(byte[] buffer)
        {
            return SendAsync(Endpoint, buffer);
        }

        /// <inheritdoc />
        public bool SendAsync<T>(T data)
        {
            return SendAsync(Endpoint, AHelper.Binary.Serialize(data));
        }

        /// <inheritdoc />
        public bool SendAsync(EndPoint endpoint, byte[] buffer, int offset, int size)
        {
            var data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            return SendAsync(endpoint, data);
        }

        /// <inheritdoc />
        public bool SendAsync<T>(EndPoint endpoint, T data)
        {
            return SendAsync(endpoint, AHelper.Binary.Serialize(data));
        }

        #endregion

        #region IDisposable implementation

        /// <summary>
        /// Disposed flag
        /// </summary>
        public bool IsDisposed { get; protected set; }

        /// <summary>
        /// Client socket disposed flag
        /// </summary>
        public bool IsSocketDisposed { get; protected set; } = true;

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose client resources
        /// </summary>
        /// <param name="disposingManagedResources"></param>
        protected virtual void Dispose(bool disposingManagedResources)
        {
            // The idea here is that Dispose(Boolean) knows whether it is
            // being called to do explicit cleanup (the Boolean is true)
            // versus being called due to a garbage collection (the Boolean
            // is false). This distinction is useful because, when being
            // disposed explicitly, the Dispose(Boolean) method can safely
            // execute code using reference type fields that refer to other
            // objects knowing for sure that these other objects have not been
            // finalized or disposed of yet. When the Boolean is false,
            // the Dispose(Boolean) method should not execute code that
            // refer to reference type fields because those objects may
            // have already been finalized."

            if (IsDisposed) return;
            // Dispose managed resources here...
            if (disposingManagedResources) Disconnect();

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }

        #endregion
    }
}