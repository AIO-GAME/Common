namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Rlogin

        /// <summary>
        /// 远程登录
        /// </summary>
        public static class Rlogin
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Rlogin, command);
            }
        }

        #endregion
    }
}