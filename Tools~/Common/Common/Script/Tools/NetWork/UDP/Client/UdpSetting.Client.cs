/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

namespace AIO.Net
{
    /// <summary>
    /// TCP client setting 
    /// </summary>
    public class UdpSettingClient : NetSetting
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
        /// This option will enable/disable SO_EXCLUSIVENESS if the OS support this feature
        /// </remarks>
        public bool ExclusiveAddressUse { get; set; }

        /// <summary>
        /// Option: bind the socket to the multicast UDP server
        /// </summary>
        public bool Multicast { get; set; }

        /// <summary>
        /// Option: receive buffer limit
        /// </summary>
        public int ReceiveBufferLimit { get; set; } = 0;

        /// <summary>
        /// Option: send buffer limit
        /// </summary>
        public int SendBufferLimit { get; set; } = 0;
    }
}