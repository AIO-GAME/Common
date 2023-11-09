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
    public class NetSettingClient : NetSetting
    {
        /// <summary>
        /// Option: receive buffer limit/选项：接收缓冲区限制
        /// </summary>
        public int ReceiveBufferLimit { get; set; } = 0;

        /// <summary>
        /// Option: send buffer limit/选项：发送缓冲区限制
        /// </summary>
        public int SendBufferLimit { get; set; } = 0;
    }
}