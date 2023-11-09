/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

namespace AIO.Net
{
    /// <summary>
    /// TCP server/client setting 
    /// </summary>
    public class NetSetting
    {
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
        public bool DualMode { get; set; }

        /// <summary>
        /// Option: keep alive/保持活动
        /// </summary>
        /// <remarks>
        /// This option will setup SO_KEEPALIVE if the OS support this feature
        /// </remarks>
        /// <remarks>
        /// 此选项将设置SO_KEEPALIVE，如果操作系统支持此功能
        /// </remarks>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// Option: TCP keep alive time/选项：TCP保持活动时间
        /// </summary>
        /// <remarks>
        /// The number of seconds a TCP connection will remain alive/idle before keepalive probes are sent to the remote
        /// </remarks>
        /// <remarks>
        /// 在发送保持活动探测到远程之前，TCP连接将保持活动/空闲的秒数
        /// </remarks>
        public int TcpKeepAliveTime { get; set; } = -1;

        /// <summary>
        /// Option: TCP keep alive interval/选项：TCP保持活动间隔
        /// </summary>
        /// <remarks>
        /// The number of seconds a TCP connection will wait for a keepalive response before sending another keepalive probe
        /// </remarks>
        /// <remarks>
        /// 在发送另一个保持活动探测之前，TCP连接将等待保持活动响应的秒数
        /// </remarks>
        public int TcpKeepAliveInterval { get; set; } = -1;

        /// <summary>
        /// Option: TCP keep alive retry count/选项：TCP保持活动重试计数
        /// </summary>
        /// <remarks>
        /// The number of TCP keep alive probes that will be sent before the connection is terminated
        /// </remarks>
        /// <remarks>
        /// 在连接终止之前将发送的TCP保持活动探测次数
        /// </remarks>
        public int TcpKeepAliveRetryCount { get; set; } = -1;

        /// <summary>
        /// Option: no delay
        /// </summary>
        /// <remarks>
        /// This option will enable/disable Nagle's algorithm for TCP protocol
        /// </remarks>
        public bool NoDelay { get; set; }

        /// <summary>
        /// Option: receive buffer size /选项：接收缓冲区大小
        /// </summary>
        public int ReceiveBufferSize { get; set; } = 8192;

        /// <summary>
        /// Option: send buffer size/选项：发送缓冲区大小
        /// </summary>
        public int SendBufferSize { get; set; } = 8192;
    }
}