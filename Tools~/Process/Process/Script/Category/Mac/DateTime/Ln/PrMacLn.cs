namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 显示系统的当前日期和时间
        /// </summary>
        public static class Ln
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_Ln, args);
            }

            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute()
            {
                return Create(CMD_Ln);
            }
        }
    }
}