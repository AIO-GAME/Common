/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;

#endregion

namespace AIO
{
    public partial class PrGit
    {
        #region Nested type: Tag

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Tag"/>
        /// </summary>
        public static class Tag
        {
            /// <summary>
            /// 添加标签
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="tagName">标签名</param>
            /// <param name="commit">提交信息</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Add(in string work, in string tagName, in string commit = null)
            {
                return Create(work, "tag -a {0} -m \"{1}\"",
                              tagName, string.IsNullOrEmpty(commit) ? tagName : commit);
            }

            /// <summary>
            /// Branch 参数
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "tag {0}", args);
            }
        }

        #endregion
    }
}