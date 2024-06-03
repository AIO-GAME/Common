namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Rsh

        /// <summary>
        /// 远程登录
        /// </summary>
        public static class Rsh
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Rsh, command);
            }
        }

        #endregion
    }
}