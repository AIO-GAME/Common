namespace AIO
{
    public partial class PrCmd
    {
        /// <summary>
        /// 符号链接
        /// </summary>
        public static class MkLink
        {
            /// <summary>
            /// 符号链接 目录
            /// </summary>
            /// <param name="target">链接目标路径</param>
            /// <param name="source">链接源路径</param>
            /// <returns>执行器</returns>
            public static IExecutor Directory(in string target, in string source)
            {
                var messages = string.Format("{0} /D /J \"{1}\" \"{2}\"", CMD_Mklink, target.Replace('/', '\\'), source.Replace('/', '\\'));
                return Create().Input(messages);
            }

            /// <summary>
            /// 硬链接 目录
            /// </summary>
            /// <param name="target">链接目标路径</param>
            /// <param name="source">链接源路径</param>
            /// <returns>执行器</returns>
            public static IExecutor HardDirectory(in string target, in string source)
            {
                var messages = string.Format("{0} /H /J \"{1}\" \"{2}\"", CMD_Mklink, target.Replace('/', '\\'), source.Replace('/', '\\'));
                return Create().Input(messages);
            }

            /// <summary>
            /// 硬链接 目录或文件
            /// </summary>
            /// <param name="target">链接目标路径</param>
            /// <param name="source">链接源路径</param>
            /// <returns>执行器</returns>
            public static IExecutor Symbolic(in string target, in string source)
            {
                var messages = string.Format("{0} /D \"{1}\" \"{2}\"", CMD_Mklink, target.Replace('/', '\\'), source.Replace('/', '\\'));
                return Create().Input(messages);
            }

            /// <summary>
            /// 符号链接 目录或文件
            /// </summary>
            /// <param name="target">链接目标路径</param>
            /// <param name="source">链接源路径</param>
            /// <returns>执行器</returns>
            public static IExecutor Hard(in string target, in string source)
            {
                var messages = string.Format("{0} /H \"{1}\" \"{2}\"", CMD_Mklink, target.Replace('/', '\\'), source.Replace('/', '\\'));
                return Create().Input(messages);
            }
        }
    }
}