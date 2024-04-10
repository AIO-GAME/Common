/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class PrWin
    {
        #region Nested type: Git

        public static partial class Git
        {
            #region Nested type: Reset

            /// <summary>
            /// 重置
            /// </summary>
            public static class Reset
            {
                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Reset"/>
                /// </summary>
                /// <param name="targets">文件路径列表</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(ICollection<string> targets, string args, bool quit = true)
                {
                    var STR = GetExecute(targets, string.Concat("reset ", args));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Reset"/>
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(string target, string args, bool quit = true)
                {
                    var STR = GetExecute(target, string.Concat("reset ", args));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 重置分支的引用以外，还会重置暂存区和工作区。
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Hard(in string work, bool quit = true)
                {
                    return Execute(work, "--hard", quit);
                }

                /// <summary>
                /// 重置分支的引用，不会修改暂存区和工作区
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Soft(in string work, bool quit = true)
                {
                    return Execute(work, "--soft", quit);
                }

                /// <summary>
                /// 重置分支的引用以外，还会重置暂存区
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Mixed(in string work, bool quit = true)
                {
                    return Execute(work, "--mixed", quit);
                }

                /// <summary>
                /// 重置索引并更新工作树中的提交和Head之间不同文件 单保留索引和工作树之间不同的文件(即有未添加的更改) 如果一个不同于提交和索引的文件有未分级的更改 重置将被终止
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Merge(in string work, bool quit = true)
                {
                    return Execute(work, "--merge", quit);
                }

                /// <summary>
                /// 重置索引并更新 如果提交和HEAD之间的文件与HEAD不同，则重置将中止。
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Keep(in string work, bool quit = true)
                {
                    return Execute(work, "--keep", quit);
                }


                /// <summary>
                /// 重置分支的引用以外，还会重置暂存区和工作区。
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Hard(in ICollection<string> work, bool quit = true)
                {
                    return Execute(work, "--hard", quit);
                }

                /// <summary>
                /// 重置分支的引用，不会修改暂存区和工作区
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Soft(in ICollection<string> work, bool quit = true)
                {
                    return Execute(work, "--soft", quit);
                }

                /// <summary>
                /// 重置分支的引用以外，还会重置暂存区
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Mixed(in ICollection<string> work, bool quit = true)
                {
                    return Execute(work, "--mixed", quit);
                }


                /// <summary>
                /// 重置索引并更新工作树中的提交和Head之间不同文件 单保留索引和工作树之间不同的文件(即有未添加的更改) 如果一个不同于提交和索引的文件有未分级的更改 重置将被终止
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Merge(in ICollection<string> work, bool quit = true)
                {
                    return Execute(work, "--merge", quit);
                }

                /// <summary>
                /// 重置索引并更新 如果提交和HEAD之间的文件与HEAD不同，则重置将中止。
                /// </summary>
                /// <param name="work">文件夹</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">work is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Keep(in ICollection<string> work, bool quit = true)
                {
                    return Execute(work, "--keep", quit);
                }
            }

            #endregion
        }

        #endregion
    }
}