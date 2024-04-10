namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Sudo

        /// <summary>
        /// 最高权限运行
        /// </summary>
        public static class Sudo
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Sudo, command);
            }
        }

        #endregion
    }
}