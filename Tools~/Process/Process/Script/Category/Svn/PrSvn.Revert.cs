/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-05-21               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

namespace AIO
{
    public partial class PrSvn
    {
        /// <summary>
        /// 还原 命令
        /// </summary>
        public sealed class Revert
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="args">参数</param>
            /// <returns>执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new System.ArgumentNullException(nameof(args));
                return Create(work, "revert {0}", args);
            }

            /// <summary>
            /// 还原指定文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="path">资源路径</param>
            /// <returns>执行器</returns>
            public static IExecutor Specify(in string work, in string path)
            {
                return Create(work, $"revert {path}");
            }
            
            /// <summary>
            /// 还原指定文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="extension">扩展名</param>
            /// <returns>执行器</returns>
            public static IExecutor SpecifyWithExtension(in string work, in string extension)
            {
                return Create(work, $"revert *.{extension}");
            }
        }
    }
}