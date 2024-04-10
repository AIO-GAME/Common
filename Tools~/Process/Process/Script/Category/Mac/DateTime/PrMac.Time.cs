namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Time

        /// <summary>
        /// 获取程序的执行时间
        /// </summary>
        public static class Time
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command = "a.out")
            {
                return Create(CMD_Time, command);
            }
        }

        #endregion
    }
}