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
        /// <see cref="PrGit"/> <see cref="Pull"/> 拉取
        /// </summary>
        public static class Pull
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
                return Create(work, "pull {0}", args);
            }


            /// <summary>
            /// 拉取
            /// </summary>
            /// <param name="work">工作文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Update(in string work)
            {
                return Create(work, "pull --verbose --ff-only --progress --autostash --recurse-submodules");
            }

            /// <summary>
            /// 拉取指定分支
            /// </summary>
            /// <param name="work">工作文件夹</param>
            /// <param name="branch">远端分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor UpdateWithALL(in string work, in string branch)
            {
                if (string.IsNullOrEmpty(branch)) throw new ArgumentNullException(nameof(branch));
                return Create(work, "pull --all --verbose --ff-only --progress --autostash --recurse-submodules", branch);
            }

            /// <summary>
            /// 拉取指定分支
            /// </summary>
            /// <param name="work">工作文件夹</param>
            /// <param name="branch">远端分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor UpdateWithBranch(in string work, in string branch)
            {
                if (string.IsNullOrEmpty(branch)) throw new ArgumentNullException(nameof(branch));
                return Create(work, "pull origin {0} --verbose --ff-only --progress --autostash --recurse-submodules", branch);
            }
        }
    }
}