namespace AIO
{
    public sealed partial class PrMac
    {
        /// <summary>
        /// 进程相关类
        /// </summary>
        public static class Kills
        {
            /// <summary>
            /// 关闭指定进程
            /// </summary>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_Kill, args);
            }
        }
    }
}