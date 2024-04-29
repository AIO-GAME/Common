namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Ftp

        /// <summary>
        /// 在本地主机与远程主机之间传输文件
        /// </summary>
        public static class Ftp
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Ftp, command);
            }
        }

        #endregion
    }
}