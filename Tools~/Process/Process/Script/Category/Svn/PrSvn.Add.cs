/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-05-21               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;

namespace AIO
{
    public partial class PrSvn
    {
        /// <summary>
        /// 添加命令
        /// </summary>
        public sealed class Add
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="args">参数</param>
            /// <returns>执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "add {0}", args);
            }

            /// <summary>
            /// 添加所有文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns></returns>
            public static IExecutor ALL(in string work)
            {
                return Create(work, "svn status | awk '{if ($1 == \"?\") {print $2} }' | xargs svn add");
            }

            /// <summary>
            /// 添加所有文件 并显示详细信息
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="ext">扩展类型 不包含. 例:(.php)->(php)</param>
            /// <returns></returns>
            public static IExecutor ALLWithExtension(in string work, in string ext)
            {
                return Create(work, $"add *.{ext} --no-ignore --force");
            }

            /// <summary>
            /// 添加指定文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="path">被添加资源路径</param>
            /// <returns></returns>
            public static IExecutor Specify(in string work, in string path)
            {
                return Create(work, $"add {path}");
            }
        }
    }
}