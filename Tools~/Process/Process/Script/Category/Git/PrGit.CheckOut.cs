/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// <see cref="PrGit"/> <see cref="Checkout"/> 切换检出
        /// </summary>
        public static class Checkout
        {
            /// <summary>
            /// Checkout 参数
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "checkout {0}", args);
            }

            /// <summary>
            /// 切换分支
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="branchName">分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Branch(in string work, in string branchName)
            {
                return Create(work, "checkout {0}", branchName);
            }
        }
    }
}