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
        /// Client Id/标识ID
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// TCP server address/地址
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// TCP server port/端口
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Endpoint/端点
        /// </summary>
        public EndPoint Endpoint { get; private set; }

        /// <summary>
        /// Socket/套接字
        /// </summary>
        public Socket Socket { get; private set; }

        /// <summary>
        /// Number of bytes pending sent by the client/客户端待发送的字节数
        /// </summary>
        public long BytesPending { get; private set; }

        /// <summary>
        /// Number of bytes sending by the client/客户端正在发送的字节数
        /// </summary>
        public long BytesSending { get; private set; }

        /// <summary>
        /// Number of bytes sent by the client/客户端已发送的字节数
        /// </summary>
        public long BytesSent { get; private set; }

        /// <summary>
        /// Number of bytes received by the client/客户端已接收的字节数
        /// </summary>
        public long BytesReceived { get; private set; }

        /// <summary>
        /// Option: dual mode socket/双模式套接字
        /// </summary>
        /// <remarks>
        /// Specifies whether the Socket is a dual-mode socket used for both IPv4 and IPv6.
        /// Will work only if socket is bound on IPv6 address.
        /// </remarks>
        /// <remarks>
        /// 指定套接字是否为用于IPv4和IPv6的双模式套接字。
        /// </remarks>
        public bool OptionDualMode { get; set; }

        /// <summary>
        /// Option: keep alive/保持活动
        /// </summary>
        /// <remarks>
        /// This option will setup SO_KEEPALIVE if the OS support this feature
        /// </remarks>
        /// <remarks>
        /// 此选项将设置SO_KEEPALIVE，如果操作系统支持此功能
        /// </remarks>
        public bool OptionKeepAlive { get; set; }

        /// <summary>
        /// Option: TCP keep alive time/选项：TCP保持活动时间
        /// </summary>
        /// <remarks>
        /// The number of seconds a TCP connection will remain alive/idle before keepalive probes are sent to the remote
        /// </remarks>
        /// <remarks>
        /// 在发送保持活动探测到远程之前，TCP连接将保持活动/空闲的秒数
        /// </remarks>
        public int OptionTcpKeepAliveTime { get; set; } = -1;

        /// <summary>
        /// Option: TCP keep alive interval/选项：TCP保持活动间隔
        /// </summary>
        /// <remarks>
        /// The number of seconds a TCP connection will wait for a keepalive response before sending another keepalive probe
        /// </remarks>
        /// <remarks>
        /// 在发送另一个保持活动探测之前，TCP连接将等待保持活动响应的秒数
        /// </remarks>
        public int OptionTcpKeepAliveInterval { get; set; } = -1;

        /// <summary>
        /// Option: TCP keep alive retry count/选项：TCP保持活动重试计数
        /// </summary>
        /// <remarks>
        /// The number of TCP keep alive probes that will be sent before the connection is terminated
        /// </remarks>
        /// <remarks>
        /// 在连接终止之前将发送的TCP保持活动探测次数
        /// </remarks>
        public int OptionTcpKeepAliveRetryCount { get; set; } = -1;

        /// <summary>
        /// Option: no delay/
        /// </summary>
        /// <remarks>
        /// This option will enable/disable Nagle's algorithm for TCP protocol
        /// </remarks>
        public bool OptionNoDelay { get; set; }

        /// <summary>
        /// Option: receive buffer limit
        /// </summary>
        public int OptionReceiveBufferLimit { get; set; } = 0;

        /// <summary>
        /// Option: receive buffer size
        /// </summary>
        public int OptionReceiveBufferSize { get; set; } = 8192;

        /// <summary>
        /// Option: send buffer limit
        /// </summary>
        public int OptionSendBufferLimit { get; set; } = 0;

        /// <summary>
        /// Option: send buffer size
        /// </summary>
        public int OptionSendBufferSize { get; set; } = 8192;

        /// <summary>
        /// Clear send/receive buffers
        /// </summary>
        private void ClearBuffers()
        {
            lock (_sendLock)
            {
                // Clear send buffers
                _sendBufferMain.Clear();
                _sendBufferFlush.Clear();
                _sendBufferFlushOffset = 0;

                // Update statistic
                BytesPending = 0;
                BytesSending = 0;
            }
        }

        /// <summary>
        /// This method is called whenever a receive or send operation is completed on a socket
        /// </summary>
        private void OnAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (IsSocketDisposed) return;

            // Determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
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
        /// Handle buffer received notification
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
        /// Handle buffer sent notification
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
        /// Handle empty send buffer notification
        /// </summary>
        /// <remarks>
        /// Notification is called when the send buffer is empty and ready for a new data to send.
        /// This handler could be used to send another buffer to the server.
        /// </remarks>
        protected virtual void OnEmpty()
        {
        }

        /// <summary>
        /// Handle error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void OnError(SocketError error)
        {
            Console.WriteLine($"TCP client caught an error with code {error}");
        }

        #endregion

        #region Error handling

        /// <summary>
        /// Send error notification
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
        /// Disposed flag
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Client socket disposed flag
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

            if (!IsDisposed)
            {
                if (disposingManagedResources)
                {
                    // Dispose managed resources here...
                    DisconnectAsync();
                }

                // Dispose unmanaged resources here...

                // Set large fields to null here...

                // Mark as disposed.
                IsDisposed = true;
            }
        }

        #endregion
    }
}