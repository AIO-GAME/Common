namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 给一个网络主机发送回应请求
        /// </summary>
        public static class Ping
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Ping, command);
            }
        }
    }
}