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
    public class UdpSettingServer : NetSetting
    {
        /// <summary>
        /// option: reuse address
        /// </summary>
        /// <remarks>
        /// This option will enable/disable SO_REUSEADDR if the OS support this feature
        /// </remarks>
        public bool ReuseAddress { get; set; }

        /// <summary>
        /// option: enables a socket to be bound for exclusive access
        /// </summary>
        /// <remarks>
        /// This option will enable/disable SO_EXCLUSIVEADDRUSE if the OS support this feature
        /// </remarks>
        public bool ExclusiveAddressUse { get; set; }

        /// <summary>
        /// option: receive buffer limit
        /// </summary>
        public int ReceiveBufferLimit { get; set; } = 0;


        /// <summary>
        /// option: send buffer limit
        /// </summary>
        public int SendBufferLimit { get; set; } = 0;
    }
}