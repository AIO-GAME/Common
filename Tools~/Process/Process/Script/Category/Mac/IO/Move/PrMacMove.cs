using System.IO;

namespace AIO
{
    public sealed partial class PrMac
    {
        /// <summary>
        /// 改变文件名 或 所在目录
        /// </summary>
        public static class Move
        {
            /// <summary>
            /// 改变文件名 或 所在目录
            /// </summary>
            public static IExecutor Execute(string source, string target)
            {
                if (!System.IO.File.Exists(source)) throw new FileNotFoundException($"[PrMac Error] The Current File Does Not Exist : {target}");
                var cmd = string.Format("'{0}' '{1}'", source.Replace('\\', '/'), target.Replace('\\', '/'));
                return Chmod.Set777(target).Link(Create(CMD_Mv, cmd));
            }

            /// <summary>
            /// 改变文件名 或 所在目录
            /// </summary>
            public static IExecutor Execute(string source, string target, string command)
            {
                if (!System.IO.File.Exists(source)) throw new FileNotFoundException($"[PrMac Error] The Current File Does Not Exist : {target}");
                var cmd = string.Format("{0} '{1}' '{2}'", command, source.Replace('\\', '/'), target.Replace('\\', '/'));
                return Chmod.Set777(target).Link(Create(CMD_Mv, cmd));
            }

            /// <summary>
            /// 改变文件名 或 所在目录
            /// </summary>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_Mv, args);
            }
        }
    }
}