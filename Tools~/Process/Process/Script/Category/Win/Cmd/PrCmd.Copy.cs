namespace AIO
{
    public partial class PrCmd
    {
        #region Nested type: Copy

        /// <summary>
        /// 复制
        /// </summary>
        public static class Copy
        {
            /// <summary>
            /// 复制
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="arg1">源路径参数</param>
            /// <param name="arg2">目标路径参数</param>
            public static IExecutor Execute(in string source, in string target, string arg1, string arg2)
            {
                var messages = string.Format("{0} \"{1}\" {3} \"{2}\" {4}", CMD_Copy, source.Replace('/', '\\'), target.Replace('/', '\\'), arg1, arg2);
                return Create().Input(messages);
            }

            /// <summary>
            /// 复制
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            public static IExecutor ALL(in string source, in string target)
            {
                var messages = string.Format("{0} \"{1}\" /D \"{2}\" /V /Y /L", CMD_Copy, source.Replace('/', '\\'), target.Replace('/', '\\'));
                return Create().Input(messages);
            }

            /// <summary>
            /// 二进制文件
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            public static IExecutor Binary(in string source, in string target)
            {
                return Create().Input(string.Format("{0} \"{1}\" /B /D \"{2}\" /V /Y /L", CMD_Copy, source.Replace('/', '\\'), target.Replace('/', '\\')));
            }

            /// <summary>
            /// 文本文件
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            public static IExecutor ASCII(in string source, in string target)
            {
                return Create().Input(string.Format("{0} \"{1}\" /A /D \"{2}\" /V /Y /L", CMD_Copy, source.Replace('/', '\\'), target.Replace('/', '\\')));
            }

            /// <summary>
            /// 复制
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            public static IExecutor NetALL(in string source, in string target)
            {
                var messages = string.Format("{0} \"{1}\" /D \"{2}\" /V /Y /L /Z", CMD_Copy, source.Replace('/', '\\'), target.Replace('/', '\\'));
                return Create().Input(messages);
            }

            /// <summary>
            /// 二进制文件
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            public static IExecutor NetBinary(in string source, in string target)
            {
                return Create().Input(string.Format("{0} \"{1}\" /B /D \"{2}\" /V /Y /L /Z", CMD_Copy, source.Replace('/', '\\'), target.Replace('/', '\\')));
            }

            /// <summary>
            /// 复制网络文件
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            public static IExecutor NetASCII(in string source, in string target)
            {
                return Create().Input(string.Format("{0} \"{1}\" /A /D \"{2}\" /V /Y /L /Z", CMD_Copy, source.Replace('/', '\\'), target.Replace('/', '\\')));
            }
        }

        #endregion
    }
}