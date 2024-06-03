using System.Threading.Tasks;

namespace AIO
{
    partial class PrWin
    {
        #region Nested type: Certutil

        /// <summary>
        /// <see cref="Certutil"/> 命令
        /// </summary>
        public static partial class Certutil
        {
            private const string cmd = "certutil";

            /// <summary>
            ///  <see cref="MD5"/> 算法 计算 <see cref="target"/>
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <param name="type"> 类型 </param>
            /// <returns> 执行器 </returns>
            public static IExecutor Custom(string target, string type) => Create(cmd, "-hashfile \"{0}\" {1}", target.Replace('/', '\\'), type);

            /// <summary>
            /// 获取 <see cref="target"/> 的 自定义算法 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <param name="type"> 类型 </param>
            /// <returns> 自定义算法 值 </returns>
            public static string GetCustom(string target, string type)
            {
                var executor = Custom(target, type);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var value = executor.Sync().StdOut.ToString().Split('\n')[1].TrimStart();
                executor.EnableOutput = temp;
                return value;
            }

            /// <summary>
            /// 获取 <see cref="target"/> 的 自定义算法 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <param name="type"> 类型 </param>
            /// <returns> 自定义算法 值 </returns>
            public static async Task<string> GetCustomAsync(string target, string type)
            {
                var executor = Custom(target, type);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var result = await executor.Async();
                var value  = result.StdOut.ToString().Split('\n')[1].TrimStart();
                executor.EnableOutput = temp;
                return value;
            }
        }

        #endregion
    }
}