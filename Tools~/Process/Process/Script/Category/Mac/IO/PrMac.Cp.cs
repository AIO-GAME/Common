#region

using System.IO;

#endregion

namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Cp

        /// <summary>
        /// 复制
        /// </summary>
        public static class Cp
        {
            /// <summary>
            /// 复制
            /// </summary>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_Cp, args);
            }

            /// <summary>
            /// 复制
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="command">参数</param>
            public static IExecutor Execute(string target, string source, string command = "-R")
            {
                if (!Directory.Exists(source)) throw new FileNotFoundException($"not found folder : {source}");
                if (Directory.Exists(target)) throw new IOException($"folder is already exist : {target}");
                var executor = Chmod.Set777(source);
                if (!Directory.Exists(Path.GetDirectoryName(target)))
                    executor.Link(Mkdir.Directory(Path.GetDirectoryName(target)));
                return executor.Link(Create(CMD_Cp, "{0} '{1}' '{2}'", command, source.Replace('\\', '/'),
                                            target.Replace('\\', '/')));
            }
        }

        #endregion
    }
}