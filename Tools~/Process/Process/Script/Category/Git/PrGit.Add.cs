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
        #region Nested type: Add

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Add"/> 添加命令
        /// usage: git add [(options)] [--] (pathspec)...
        /// -n, --dry-run         dry run
        /// -v, --verbose         be verbose
        /// -i, --interactive     interactive picking
        /// -p, --patch           select hunks interactively
        /// -e, --edit            edit current diff and apply
        /// -f, --force           allow adding otherwise ignored files
        /// -u, --update          update tracked files
        /// --renormalize         renormalize EOL of tracked files (implies -u)
        /// -N, --intent-to-add   record only the fact that the path will be added later
        /// -A, --all             add changes from all tracked and untracked files
        /// --ignore-removal      ignore paths removed in the working tree (same as --no-all)
        /// --refresh             don't add, only refresh the index
        /// --ignore-errors       just skip files which cannot be added because of errors
        /// --ignore-missing      check if - even missing - files are ignored in dry run
        /// --sparse              allow updating entries outside of the sparse-checkout cone
        /// --chmod (+|-)x        override the executable bit of the listed files
        /// --pathspec-from-file (file)
        ///                       read pathspec from file
        /// --pathspec-file-nul   with --pathspec-from-file, pathspec elements are separated with NUL character
        /// </summary>
        public static class Add
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
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
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ALL(in string work)
            {
                return Create(work, "add --all");
            }

            /// <summary>
            /// 添加所有文件 并显示详细信息
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ALLWithV(in string work)
            {
                return Create(work, "add --all --verbose");
            }

            /// <summary>
            /// 列出文件
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ALLWithN(in string work)
            {
                return Create(work, "add --all --dry-run");
            }

            /// <summary>
            /// 添加所有文件 并显示详细信息 并显示详细进度
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ALLWithVP(in string work)
            {
                return Create(work, "add --all --verbose --progress");
            }

            /// <summary>
            /// 添加所有文件 并更新
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ALLWithU(in string work)
            {
                return Create(work, "add --all --update");
            }
        }

        #endregion
    }
}