/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

namespace AIO.Net
{
    /// <summary>
    /// TCP server setting 
    /// </summary>
    public class TcpSettingServer : TcpSetting
    {
        /// <summary>
        /// Option: reuse address
        /// </summary>
        /// <remarks>
        /// This option will enable/disable SO_REUSEADDR if the OS support this feature
        /// </remarks>
        public bool ReuseAddress { get; set; }

        /// <summary>
        /// Option: enables a socket to be bound for exclusive access
        /// </summary>
        /// <remarks>
        /// This option will enable/disable SO_EXCLUSIVEADDRUSE if the OS support this feature
        /// </remarks>
        public bool ExclusiveAddressUse { get; set; }

        /// <summary>
        /// Option: acceptor backlog size /选项：接收器后备大小
        /// </summary>
        /// <remarks>
        /// This option will set the listening socket's backlog size
        /// </remarks>
        public int AcceptorBacklog { get; set; } = 1024;
    }
}