#region

using System;
using System.Net;
using System.Net.Sockets;

#endregion

namespace AIO.Net
{
    /// <summary>
    /// Net Server class
    /// </summary>
    public abstract class NetServer : INetServer
    {
        private NetServer() { }

        /// <summary>
        /// Initialize Net server with a given endpoint, address and port
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="address">Server address</param>
        /// <param name="port">Server port</param>
        protected NetServer(EndPoint endpoint, string address, int port)
        {
            Id       = Guid.NewGuid();
            Address  = address;
            Port     = port;
            Endpoint = endpoint;
        }

        /// <summary>
        /// Initialize Net server with a given endpoint, address and port
        /// </summary>
        /// <param name="address">Server address</param>
        /// <param name="port">Server port</param>
        protected NetServer(IPAddress address, int port) : this(new IPEndPoint(address, port)) { }

        /// <summary>
        /// Initialize Net server with a given endpoint, address and port
        /// </summary>
        /// <param name="address">Server address</param>
        /// <param name="port">Server port</param>
        protected NetServer(string address, int port) : this(new IPEndPoint(IPAddress.Parse(address), port)) { }

        /// <summary>
        /// Initialize Net server with a given endpoint, address and port
        /// </summary>
        /// <param name="endpoint">Server endpoint</param>
        protected NetServer(DnsEndPoint endpoint) : this(endpoint, endpoint.Host, endpoint.Port) { }

        /// <summary>
        /// Initialize Net server with a given endpoint, address and port
        /// </summary>
        /// <param name="endpoint">Server endpoint</param>
        protected NetServer(IPEndPoint endpoint) : this(endpoint, endpoint.Address.ToString(), endpoint.Port) { }

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
        public EndPoint Endpoint { get; protected set; }

        /// <summary>
        /// Is the server started?
        /// </summary>
        public bool IsStarted { get; protected set; }

        #region INetServer Members

        /// <inheritdoc />
        public virtual bool MulticastAsync(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual int Multicast(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool SendAsync(INetSession session, byte[] buffer)
        {
            return session.SendAsync(buffer);
        }

        /// <inheritdoc />
        public int Send(INetSession session, byte[] buffer)
        {
            return session.Send(buffer);
        }

        /// <inheritdoc />
        public int Receive(INetSession session, byte[] buffer, int offset, int size)
        {
            return session.Receive(buffer, offset, size);
        }

        /// <inheritdoc />
        public void ReceiveAsync(INetSession session)
        {
            session.ReceiveAsync();
        }

        #endregion

        /// <summary>
        /// Create a new socket object
        /// </summary>
        /// <remarks>
        /// Method may be override if you need to prepare some specific socket object in your implementation.
        /// </remarks>
        /// <returns>Socket object</returns>
        protected abstract Socket CreateSocket();

        /// <summary>
        /// Stop the server (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully stopped, 'false' if the server is already stopped</returns>
        public abstract bool Stop();

        /// <summary>
        /// Start the server with a given multicast endpoint (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public abstract bool Start();

        /// <inheritdoc />
        public sealed override string ToString()
        {
            return base.ToString();
        }

        /// <inheritdoc />
        public sealed override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IDisposable implementation

        /// <summary>
        /// Disposed flag
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Server socket disposed flag
        /// </summary>
        public bool IsSocketDisposed { get; protected set; } = true;

        /// <inheritdoc />
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Restart the server (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully restarted, 'false' if the server failed to restart</returns>
        public virtual bool Restart()
        {
            return Stop() && Start();
        }

        /// <summary>
        /// Clear send/receive buffers
        /// </summary>
        protected virtual void ClearBuffers() { }

        /// <summary>
        /// Send error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void SendError(SocketError error)
        {
            OnError(error);
        }

        /// <summary>
        /// Dispose the server
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
            if (disposingManagedResources) Stop();

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }

        #endregion

        #region MyRegion

        /// <summary>
        /// Handle error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void OnError(SocketError error) { }

        /// <summary>
        /// Handle server starting notification
        /// </summary>
        protected virtual void OnStarting() { }

        /// <summary>
        /// Handle server started notification
        /// </summary>
        protected virtual void OnStarted() { }

        /// <summary>
        /// Handle server stopping notification
        /// </summary>
        protected virtual void OnStopping() { }

        /// <summary>
        /// Handle server stopped notification
        /// </summary>
        protected virtual void OnStopped() { }

        /// <summary>
        /// Handle datagram received notification
        /// </summary>
        /// <param name="endpoint">Received endpoint</param>
        /// <param name="buffer">Received datagram buffer</param>
        /// <param name="offset">Received datagram buffer offset</param>
        /// <param name="size">Received datagram buffer size</param>
        /// <remarks>
        /// Notification is called when another datagram was received from some endpoint
        /// </remarks>
        protected virtual void OnReceived(EndPoint endpoint, byte[] buffer, int offset, int size) { }

        /// <summary>
        /// Handle datagram sent notification
        /// </summary>
        /// <param name="endpoint">Endpoint of sent datagram</param>
        /// <param name="sent">Size of sent datagram buffer</param>
        /// <remarks>
        /// Notification is called when a datagram was sent to the client.
        /// This handler could be used to send another datagram to the client for instance when the pending size is zero.
        /// </remarks>
        protected virtual void OnSent(EndPoint endpoint, int sent) { }

        #endregion
    }
}