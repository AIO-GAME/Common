/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-24
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// <see cref="PrGit"/> <see cref="Reset"/> 重置
        /// </summary>
        public static class Reset
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
                return Create(work, "reset {0}", args);
            }

            /// <summary>
            /// 重置分支的引用以外，还会重置暂存区和工作区。
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
            public static IExecutor Hard(in string work)
            {
                if (string.IsNullOrEmpty(work)) throw new ArgumentNullException(nameof(work));
                return Create(work, "reset --hard");
            }

            /// <summary>
            /// 重置分支的引用，不会修改暂存区和工作区
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
            public static IExecutor Soft(in string work)
            {
                if (string.IsNullOrEmpty(work)) throw new ArgumentNullException(nameof(work));
                return Create(work, "reset --soft");
            }

            /// <summary>
            /// 重置分支的引用以外，还会重置暂存区
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
            public static IExecutor Mixed(in string work)
            {
                if (string.IsNullOrEmpty(work)) throw new ArgumentNullException(nameof(work));
                return Create(work, "reset --mixed");
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
            public static IExecutor Merge(in string work)
            {
                if (string.IsNullOrEmpty(work)) throw new ArgumentNullException(nameof(work));
                return Create(work, "reset --merge");
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
            public static IExecutor Keep(in string work)
            {
                if (string.IsNullOrEmpty(work)) throw new ArgumentNullException(nameof(work));
                return Create(work, "reset --keep");
            }
        }
    }
}