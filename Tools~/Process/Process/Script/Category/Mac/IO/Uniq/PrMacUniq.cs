using System.IO;

namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 去掉文件中的重复行
        /// </summary>
        public static class Uniq
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string source, string target, string command)
            {
                if (!System.IO.File.Exists(source) || !System.IO.File.Exists(target))
                    throw new FileNotFoundException($"[PrMac Error] The Current File Does Not Exist : {target}");
                var cmd = string.Format("{0} '{1}' '{2}'", command, source.Replace('\\', '/'), target.Replace('\\', '/'));
                return Chmod.Set777(target).Link(Create(CMD_Uniq, cmd));
            }

            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string source, string target)
            {
                if (!System.IO.File.Exists(source) || !System.IO.File.Exists(target))
                    throw new FileNotFoundException($"[PrMac Error] The Current File Does Not Exist : {target}");
                var cmd = string.Format("'{0}' '{1}'", source.Replace('\\', '/'), target.Replace('\\', '/'));
                return Chmod.Set777(target).Link(Create(CMD_Uniq, cmd));
            }
        }
    }
}