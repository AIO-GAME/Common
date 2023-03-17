namespace AIO
{
    public sealed partial class PrMac
    {
        /// <summary>
        /// 文件夹比较相关命令
        /// </summary>
        public static class DirCmp
        {
            /// <summary>
            /// 创建比较两个目录的命令行执行器
            /// </summary>
            /// <param name="path1">第一个目录的路径</param>
            /// <param name="path2">第二个目录的路径</param>
            /// <returns>执行器实例</returns>
            public static IExecutor CompareDirectories(string path1, string path2)
            {
                return Create(CMD_DirCmp, $"-r \"{path1.Replace('\\', '/')}\" \"{path2.Replace('\\', '/')}\"");
            }

            /// <summary>
            /// 比较两个目录的不同之处
            /// </summary>
            /// <param name="path1">第一个目录的路径</param>
            /// <param name="path2">第二个目录的路径</param>
            /// <returns>执行器实例</returns>
            public static IExecutor ShowDifferences(string path1, string path2)
            {
                return Create(CMD_DirCmp, $"-s \"{path1.Replace('\\', '/')}\" \"{path2.Replace('\\', '/')}\"");
            }

            /// <summary>
            /// 比较两个目录，输出详细的比较结果
            /// </summary>
            /// <param name="path1">第一个目录的路径</param>
            /// <param name="path2">第二个目录的路径</param>
            /// <returns>执行器实例</returns>
            public static IExecutor ShowDetailedDifferences(string path1, string path2)
            {
                return Create(CMD_DirCmp, $"-S \"{path1.Replace('\\', '/')}\" \"{path2.Replace('\\', '/')}\"");
            }
        }
    }
}