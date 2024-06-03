namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Shell

        /// <summary>
        /// Shell 运行
        /// </summary>
        public static class Shell
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_Sh, args);
            }
        }

        #endregion
    }
}