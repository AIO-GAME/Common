#region

using System.IO;

#endregion

namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Nl

        /// <summary>
        /// 给文件加上行号
        /// </summary>
        public static class Nl
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string source, string target)
            {
                if (!File.Exists(source)) throw new FileNotFoundException($"[PrMac Error] The Current File Does Not Exist : {target}");
                var cmd = $"'{source.Replace('\\', '/')}' {">"} '{target.Replace('\\', '/')}'";
                return Chmod.Set777(target).Link(Create(CMD_Nl, cmd));
            }
        }

        #endregion
    }
}