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
        #region Nested type: Branch

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Branch"/>
        /// </summary>
        public static class Branch
        {
            /// <summary>
            /// 获取目标GIT库的分支列表
            /// </summary>
            /// <param name="work">Git文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor GetList(in string work)
            {
                return PrGit.Create(work, "branch -l --column");
            }

            /// <summary>
            /// 删除指定本地分支
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="branch">分支</param>
            /// <param name="isForce">是否强制删除 Ture:强制 False:不强制</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor DelLocal(in string work, in string branch, in bool isForce = false)
            {
                if (string.IsNullOrEmpty(branch)) throw new ArgumentNullException();
                return PrGit.Create(work, "branch {0} {1}", isForce ? "-D" : "-d", branch);
            }

            /// <summary>
            /// 删除指定远端分支
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="branch">分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor DelOrigin(in string work, in string branch)
            {
                if (string.IsNullOrEmpty(branch)) throw new ArgumentNullException();
                return PrGit.Create(work, "push origin --delete {0}", branch);
            }

            /// <summary>
            /// 创建指定分支
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="branch">分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Create(in string work, in string branch)
            {
                if (string.IsNullOrEmpty(branch)) throw new ArgumentNullException();
                return PrGit.Create(work, "branch {0}", branch);
            }

            /// <summary>
            /// 对当前分支重命名
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="branchName">分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor CurReName(in string work, in string branchName)
            {
                return PrGit.Create(work, "branch -m {0}", branchName);
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
                return PrGit.Create(work, "branch {0}", args);
            }
        }

        #endregion
    }
}