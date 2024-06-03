namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Mkdir

        /// <summary>
        /// 目录相关命令
        /// </summary>
        public static class Mkdir
        {
            private const string CMD_Mkdir = "mkdir";

            /// <summary>
            /// 创建目录
            /// </summary>
            /// <param name="target">目标路径</param>
            public static IExecutor Directory(string target)
            {
                return Create(CMD_Mkdir, $"-p '{target}'");
            }

            /// <summary>
            /// 创建目录并设置权限
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="permissions">目录权限 chmod中的权限</param>
            public static IExecutor Directory(string target, string permissions)
            {
                return Create(CMD_Mkdir, $"-p -m {permissions} '{target}'");
            }

            /// <summary>
            /// 移动目录或重命名目录
            /// </summary>
            /// <param name="source">源目录路径</param>
            /// <param name="target">目标目录路径</param>
            public static IExecutor Move(string source, string target)
            {
                return Create(CMD_Mkdir, $"'{source}' '{target}'");
            }

            /// <summary>
            /// 创建目录并设置权限
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="permissions">目录权限 chmod中的权限</param>
            public static IExecutor Permissions(string target, string permissions)
            {
                return Create(CMD_Mkdir, $"-m {permissions} '{target}'");
            }
        }

        #endregion
    }
}