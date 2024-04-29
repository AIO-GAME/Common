/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-05-21
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

#region

using System;

#endregion

namespace AIO
{
    public partial class PrSvn
    {
        #region Nested type: Delete

        /// <summary>
        /// 删除 命令
        /// </summary>
        public sealed class Delete
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
            /// 删除所有文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns>执行器</returns>
            public static IExecutor ALL(in string work)
            {
                return Create(work, "status | awk '{if ($1 == \"!\") {print $2}}' | xargs svn delete");
            }

            /// <summary>
            /// 删除指定类型的所有文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="ext">扩展类型 不包含. 例:(.php)->(php)</param>
            /// <returns>执行器</returns>
            public static IExecutor ALLWithExtension(in string work, in string ext)
            {
                return Create(work, $"delete *.{ext}");
            }

            /// <summary>
            /// 删除指定文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="path">被添加资源路径</param>
            /// <returns>执行器</returns>
            public static IExecutor Specify(in string work, in string path)
            {
                return Create(work, $"delete {path}");
            }

            /// <summary>
            /// 删除指定URL
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="url">URL地址</param>
            /// <returns>执行器</returns>
            public static IExecutor URL(in string work, in string url)
            {
                return Create(work, $"delete -m {url}");
            }


            /// <summary>
            /// 删除已修改的全部文件
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns>执行器</returns>
            public static IExecutor ALLWithChnage(in string work)
            {
                return Create(work, "delete -force over-there");
            }
        }

        #endregion
    }
}