#region

using System.IO;

#endregion

namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: Rm

        /// <summary>
        /// 提供文件和目录操作的静态方法，仅在 Mac 和 Unix 上运行。
        /// </summary>
        public static class Rm
        {
            /// <summary>
            /// 删除指定文件，谨慎使用，使用前请先测试。
            /// </summary>
            /// <param name="target">要删除的文件路径。</param>
            /// <returns>如果成功删除文件，返回空的 IExecutor 对象；如果文件不存在，返回包含异常信息的 IExecutor 对象。</returns>
            public static IExecutor File(in string target)
            {
                if (!System.IO.File.Exists(target)) return new PrException<FileNotFoundException>($"[PrMac Error] The Current Path Does Not Exist : {target}").Execute();

                // 设置文件权限为 777，并删除文件。
                return Chmod.Set777(target).Link(Create(CMD_Rm, "-f '{0}'", target.Replace('\\', '/')));
            }

            /// <summary>
            /// 删除指定目录及其下所有文件和子目录。
            /// </summary>
            /// <param name="target">要删除的目录路径。</param>
            /// <returns>如果成功删除目录，返回空的 IExecutor 对象；如果目录不存在，返回包含异常信息的 IExecutor 对象。</returns>
            public static IExecutor Directory(in string target)
            {
                if (!System.IO.Directory.Exists(target)) return new PrException<FileNotFoundException>($"[PrMac Error] The Current Path Does Not Exist : {target}").Execute();

                // 设置目录权限为 777，并删除目录。
                return Chmod.Set777(target).Link(Create(CMD_Rm, "-rf '{0}'", target.Replace('\\', '/')));
            }

            /// <summary>
            /// 在当前进程中执行指定的命令行。
            /// </summary>
            /// <param name="args">要执行的命令行参数。</param>
            /// <returns>返回包含执行结果的 IExecutor 对象。</returns>
            public static IExecutor Execute(in string args)
            {
                // 执行指定的命令行。
                return Create(CMD_Rm, args);
            }
        }

        #endregion
    }
}