namespace AIO
{
    public partial class PrWin
    {
        #region Nested type: XCopy

        /// <summary>
        /// 复制文件夹
        /// </summary>
        public static class XCopy
        {
            /// <summary>
            /// 复制文件夹
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="args">启动参数</param>
            public static IExecutor Execute(string target, string source, string args)
            {
                return Create(CMD_Xcopy, "\"{0}\" {2} \"{1}\"", source.Replace('/', '\\'), target.Replace('/', '\\'),
                              args);
            }

            /// <summary>
            /// 复制文件夹 包括空目录
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="isAll">Ture:包含空目录</param>
            public static IExecutor Directory(string target, string source, bool isAll = true)
            {
                return Create(CMD_Xcopy, "\"{0}\" {2} {3} \"{1}\"",
                              source.Replace('/', '\\'), target.Replace('/', '\\'),
                              isAll ? "/s /e" : "/s", "/y /i /v /f /c /g /h /r");
            }

            /// <summary>
            /// 网络 复制文件夹 包括空目录
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="isAll">Ture:包含空目录</param>
            public static IExecutor NetDirectory(string target, string source, bool isAll = true)
            {
                return Create(CMD_Xcopy, "\"{0}\" {2} {3} \"{1}\"",
                              source.Replace('/', '\\'), target.Replace('/', '\\'),
                              isAll ? "/e" : "s", "/y /i /v /f /c /g /h /r /z /COMPRESS");
            }

            /// <summary>
            /// 复制大文件夹 包括空目录
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="isAll">Ture:包含空目录</param>
            public static IExecutor BigFile(string target, string source, bool isAll = true)
            {
                return Create(CMD_Xcopy, "\"{0}\" {2} {3} \"{1}\"",
                              source.Replace('/', '\\'), target.Replace('/', '\\'),
                              isAll ? "/e" : "s", "/y /-i /v /f /c /g /h /r /j");
            }

            /// <summary>
            /// 复制文件夹结构 不包含文件
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="isAll">Ture:包含空目录</param>
            public static IExecutor DirectoryTree(string target, string source, bool isAll = true)
            {
                return Create(CMD_Xcopy, "\"{0}\" {2} {3} \"{1}\"",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'), isAll ? "/t /e" : "/t", "/y /-i /v /f /c /g /h /r");
            }

            /// <summary>
            /// 复制文件夹结构 不包含文件
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="isAll">Ture:包含空目录</param>
            public static IExecutor NetDirectoryTree(string target, string source, bool isAll = true)
            {
                return Create(CMD_Xcopy, "\"{0}\" {2} {3} \"{1}\"",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'), isAll ? "/t /e" : "/t", "/y /-i /v /f /c /g /h /r /z");
            }
        }

        #endregion
    }
}