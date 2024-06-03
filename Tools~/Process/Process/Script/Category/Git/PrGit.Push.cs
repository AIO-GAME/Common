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
        #region Nested type: Push

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Push"/> 拉取
        /// </summary>
        public static class Push
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "push {0}", args);
            }

            /// <summary>
            /// push 参数
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Update(in string work)
            {
                return Create(work, "push --verbose --progress");
            }
        }

        #endregion
    }
}