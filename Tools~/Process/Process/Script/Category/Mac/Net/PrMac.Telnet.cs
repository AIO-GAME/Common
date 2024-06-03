namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Telnet

        /// <summary>
        /// 远程登录
        /// </summary>
        public static class Telnet
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Telnet, command);
            }
        }

        #endregion
    }
}