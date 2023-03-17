namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 联接文件
        /// </summary>
        public static class In
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string source, string target, string command = "-s")
            {
                return Create(CMD_In, "{0} '{1}' '{2}'", command, source.Replace('\\', '/'), target.Replace('\\', '/'));
            }

            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_In, args);
            }
        }
    }
}