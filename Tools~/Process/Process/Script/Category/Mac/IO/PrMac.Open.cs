namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: Open

        /// <summary>
        /// 打开指令
        /// </summary>
        public sealed class Open
        {
            /// <summary>
            /// 打开文件
            /// </summary>
            /// <param name="target">目标路径</param>
            public static IExecutor Path(in string target)
            {
                return Chmod.Set777(target).Link(Create(CMD_Open, target.Replace('\\', '/')));
            }

            /// <summary>
            /// 用shell默认程序打开文件
            /// </summary>
            public static IExecutor Shell(in string target)
            {
                return Duti.DefaultPrograms("com.apple.Terminal", "public.shell-script").Link(Path(target));
            }

            /// <summary>
            /// 打开网页
            /// </summary>
            public static IExecutor URL(in string target, in string command = null)
            {
                return Create(CMD_Open, "{0} '{1}'", command, target.Replace('\\', '/'));
            }
        }

        #endregion
    }
}