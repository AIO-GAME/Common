#region

using System.IO;

#endregion

namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: Touch

        /// <summary>
        /// 更新文件的访问和修改时间
        /// </summary>
        public sealed class Touch
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string target, string command = null)
            {
                if (!File.Exists(target)) throw new FileNotFoundException($"[PrMac Error] The Current File Does Not Exist : {target}");
                var cmd = string.Format("{0} '{1}'", command, target.Replace('\\', '/'));
                return Chmod.Set777(target).Link(Create(CMD_Touch, cmd));
            }
        }

        #endregion
    }
}