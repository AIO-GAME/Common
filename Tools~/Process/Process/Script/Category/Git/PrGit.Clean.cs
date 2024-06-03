#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class PrGit
    {
        #region Nested type: Clean

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Clean"/> 清理命令
        /// usage: git clean [-d] [-f] [-i] [-n] [-q] [-e (pattern)] [-x | -X] [--] (paths)...
        /// -q, --quiet           do not print names of files removed
        /// -n, --dry-run         dry run
        /// -f, --force           force
        /// -i, --interactive     interactive cleaning
        /// -d                    remove whole directories
        /// -e, --exclude pattern()
        /// add (pattern) to ignore rules
        /// -x                    remove ignored files, too  被忽略的文件也删除
        /// -X                    remove only ignored files  只删被忽略的文件
        /// </summary>
        public static class Clean
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                return Create(work, "clean {0}", args);
            }

            /// <summary>
            /// 显示被清理的文件
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor DryRun(in string work)
            {
                return Create(work, "clean -n");
            }

            /// <summary>
            /// 显示被清理的文件
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor DryRunDirectory(in string work)
            {
                return Create(work, "clean -dn");
            }

            /// <summary>
            /// 删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Directory(in string work)
            {
                return Create(work, "clean -d");
            }

            /// <summary>
            /// 删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="exclude">排除文件列表</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor DirectoryWithExclude(in string work, in ICollection<string> exclude)
            {
                return Create(work, "clean -de {0}", string.Join(" ", exclude));
            }

            /// <summary>
            /// 删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="exclude">排除文件列表</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor DirectoryWithExclude(in string work, in string exclude)
            {
                return Create(work, "clean -de {0}", exclude);
            }

            /// <summary>
            /// 强制删除
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="path">路径</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Force(in string work, in string path)
            {
                return Create(work, "clean -f {0}", path);
            }

            /// <summary>
            /// 强制删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ForceDirectory(in string work)
            {
                return Create(work, "clean -fd");
            }

            /// <summary>
            /// 强制删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="exclude">排除文件列表</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ForceDirectoryWithExclude(in string work, in string exclude)
            {
                return Create(work, "clean -fde {0}", exclude);
            }

            /// <summary>
            /// 强制删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="exclude">排除文件列表</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ForceDirectoryWithExclude(in string work, in ICollection<string> exclude)
            {
                return Create(work, "clean -fde {0}", string.Join(" ", exclude));
            }

            /// <summary>
            /// 强制删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ForceDirectoryX(in string work)
            {
                return Create(work, "clean -fd -X");
            }

            /// <summary>
            /// 强制删除整个目录
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ForceDirectoryx(in string work)
            {
                return Create(work, "clean -fd -x");
            }
        }

        #endregion
    }
}