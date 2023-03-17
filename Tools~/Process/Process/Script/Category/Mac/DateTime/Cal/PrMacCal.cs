namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 显示日历
        /// </summary>
        public static class Cal
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command = "")
            {
                return Create(CMD_Cal, command);
            }
        }
    }
}