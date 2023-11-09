/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Net.Sockets;

namespace AIO.Net
{
    /// <summary>
    /// TCP客户端用于向连接的TCP服务器读写数据
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public partial class TcpClient : IDisposable
    {
        /// <summary>
        /// Initialize TCP client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public TcpClient(IPAddress address, int port) : this(new IPEndPoint(address, port))
        {
        }

        /// <summary>
        /// Initialize TCP client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public TcpClient(string address, int port) : this(new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <summary>
        /// Initialize TCP client with a given DNS endpoint
        /// </summary>
        /// <param name="endpoint">DNS endpoint</param>
        public TcpClient(DnsEndPoint endpoint) : this(endpoint, endpoint.Host, endpoint.Port)
        {
        }

        /// <summary>
        /// Initialize TCP client with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        public TcpClient(IPEndPoint endpoint) : this(endpoint, endpoint.Address.ToString(), endpoint.Port)
        {
        }

        /// <summary>
        /// Initialize TCP client with a given endpoint, address and port
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="address">Server address</param>
        /// <param name="port">Server port</param>
        private TcpClient(EndPoint endpoint, string address, int port)
        {
            Id = Guid.NewGuid();
            Address = address;
            Port = port;
            Endpoint = endpoint;
        }

        /// <summary>
        /// Client Id / 标识ID
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// TCP server address / 地址
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// TCP server port / 端口
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Endpoint / 端点
        /// </summary>
        public EndPoint Endpoint { get; private set; }

        /// <summary>
        /// Socket/套接字
        /// </summary>
        public Socket Socket { get; private set; }

        /// <summary>
        /// Number of bytes pending sent by the client / 客户端待发送的字节数
        /// </summary>
        public long BytesPending { get; private set; }

        /// <summary>
        /// Number of bytes sending by the client / 客户端正在发送的字节数
        /// </summary>
        public long BytesSending { get; private set; }

        /// <summary>
        /// Number of bytes sent by the client / 客户端已发送的字节数
        /// </summary>
        public long BytesSent { get; private set; }

        /// <summary>
        /// Number of bytes received by the client / 客户端已接收的字节数
        /// </summary>
        public long BytesReceived { get; private set; }

        /// <summary>
        /// Client option / 客户端选项
        /// </summary>
        public NetSettingClient Option { get; } = new NetSettingClient();

        /// <summary>
        /// Clear send/receive buffers / 清除发送/接收缓冲区
        /// </summary>
        private void ClearBuffers()
        {
            lock (SendLock)
            {
                // Clear send buffers
                SendBufferMain.Clear();
                SendBufferFlush.Clear();
                SendBufferFlushOffset = 0;

                // Update statistic
                BytesPending = 0;
                BytesSending = 0;
            }
        }

        /// <summary>
        /// This method is called whenever a receive or send operation is completed on a socket/每当套接字上的接收或发送操作完成时，都会调用此方法
        /// </summary>
        private void OnAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (IsSocketDisposed) return;
            switch (e.LastOperation) // Determine which type of operation just completed and call the associated handler
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
                case SocketAsyncOperation.Receive:
                    if (ProcessReceive(e))
                        TryReceive();
                    break;
                case SocketAsyncOperation.Send:
                    if (ProcessSend(e))
                        TrySend();
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        #region Session handlers

        /// <summary>
        /// Handle client connecting notification/处理客户端连接中通知
        /// </summary>
        protected virtual void OnConnecting()
        {
        }

        /// <summary>
        /// Handle client connected notification/处理客户端连接通知
        /// </summary>
        protected virtual void OnConnected()
        {
        }

        /// <summary>
        /// Handle client disconnecting notification / 处理客户端断开连接中通知
        /// </summary>
        protected virtual void OnDisconnecting()
        {
        }

        /// <summary>
        /// Handle client disconnected notification / 处理客户端断开连接通知
        /// </summary>
        protected virtual void OnDisconnected()
        {
        }

        /// <summary>
        /// Handle buffer received notification/处理缓冲区接收通知
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        /// <remarks>
        /// Notification is called when another chunk of buffer was received from the server
        /// </remarks>
        protected virtual void OnReceived(byte[] buffer, int offset, int size)
        {
        }

        /// <summary>
        /// Handle buffer sent notification/处理缓冲区发送通知
        /// </summary>
        /// <param name="sent">Size of sent buffer</param>
        /// <param name="pending">Size of pending buffer</param>
        /// <remarks>
        /// Notification is called when another chunk of buffer was sent to the server.
        /// This handler could be used to send another buffer to the server for instance when the pending size is zero.
        /// </remarks>
        protected virtual void OnSent(long sent, long pending)
        {
        }

        /// <summary>
        /// Handle empty send buffer notification/处理空发送缓冲区通知
        /// </summary>
        /// <remarks>
        /// Notification is called when the send buffer is empty and ready for a new data to send.
        /// This handler could be used to send another buffer to the server.
        /// </remarks>
        protected virtual void OnEmpty()
        {
        }

        /// <summary>
        /// Handle error notification/处理错误通知
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void OnError(SocketError error)
        {
            Console.WriteLine($"TCP client caught an error with code {error}");
        }

        #endregion

        #region Error handling

        /// <summary>
        /// Send error notification/发送错误通知
        /// </summary>
        /// <param name="error">Socket error code</param>
        private void SendError(SocketError error)
        {
            // Skip disconnect errors
            if (error == SocketError.ConnectionAborted ||
                error == SocketError.ConnectionRefused ||
                error == SocketError.ConnectionReset ||
                error == SocketError.OperationAborted ||
                error == SocketError.Shutdown)
                return;

            OnError(error);
        }

        #endregion

        #region IDisposable implementation

        /// <summary>
        /// Disposed flag/已处理标志
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Client socket disposed flag/客户端套接字已处理标志
        /// </summary>
        public bool IsSocketDisposed { get; private set; } = true;

        // Implement IDisposable.
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose client resources/处理客户端资源
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
            if (disposingManagedResources) DisconnectAsync();

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }

        #endregion
    }
}