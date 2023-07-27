using System.IO;
using System.Linq;

namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 文件查看相关命令
        /// </summary>
        public static class Cat
        {
            /// <summary>
            /// 执行cat命令
            /// </summary>
            /// <param name="args">cat命令参数</param>
            /// <returns>命令执行器</returns>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_Cat, args);
            }

            /// <summary>
            /// 查看文件内容
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor ViewFile(params string[] paths)
            {
                return Create(CMD_Cat, string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'")));
            }

            /// <summary>
            /// 显示不可打印字符
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor ShowNonprinting(params string[] paths)
            {
                return Create(CMD_Cat, $"-v {string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'"))}");
            }

            /// <summary>
            /// 显示所有字符（包括行尾符）
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor ShowAll(params string[] paths)
            {
                return Create(CMD_Cat, $"-A {string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'"))}");
            }

            /// <summary>
            /// 显示行号
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor NumberLines(params string[] paths)
            {
                return Create(CMD_Cat, $"-n {string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'"))}");
            }

            /// <summary>
            /// 只显示非空行的行号
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor NumberNonblankLines(params string[] paths)
            {
                return Create(CMD_Cat, $"-b {string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'"))}");
            }

            /// <summary>
            /// 在行尾显示$符号
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor ShowEnds(params string[] paths)
            {
                return Create(CMD_Cat, $"-E {string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'"))}");
            }

            /// <summary>
            /// 将制表符显示为^I
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor ShowTabs(params string[] paths)
            {
                return Create(CMD_Cat, $"-T {string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'"))}");
            }

            /// <summary>
            /// 显示行尾符为$
            /// </summary>
            /// <param name="paths">文件路径</param>
            /// <returns>命令执行器</returns>
            public static IExecutor ShowLineEndings(params string[] paths)
            {
                return Create(CMD_Cat, $"-e {string.Join(" ", paths.Select(path => $"'{path.Replace('\\', '/')}'"))}");
            }
        }
    }
}