namespace AIO
{
    public sealed partial class PrMac
    {
        /// <summary>
        /// 显示进程当前状态
        /// </summary>
        public static class Ps
        {
            /// <summary>
            /// 显示进程当前状态
            /// </summary>
            public static IExecutor Execute(string args = "u")
            {
                return Create(CMD_Ps, args);
            }
        }
    }
}