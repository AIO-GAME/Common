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

        public partial class Git
        {
            #region Nested type: Clean

            /// <summary>
            /// 清理
            /// </summary>
            public static class Clean
            {
                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Clean"/>
                /// </summary>
                /// <param name="targets">文件路径列表</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(ICollection<string> targets, string args, bool quit = true)
                {
                    var STR = GetExecute(targets, string.Concat("clean ", args));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Clean"/>
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(string target, string args, bool quit = true)
                {
                    var STR = GetExecute(target, string.Concat("clean ", args));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 强制清理文件夹 并不使用用.gitignore 中忽略的文件
                /// </summary>
                /// <param name="targets">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDirX(ICollection<string> targets, bool quit = true)
                {
                    return Execute(targets, "-fd -x", quit);
                }

                /// <summary>
                /// 强制清理文件夹
                /// </summary>
                /// <param name="targets">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDir(ICollection<string> targets, bool quit = true)
                {
                    return Execute(targets, "-fd", quit);
                }


                /// <summary>
                /// 强制清理文件夹 并不使用用.gitignore 中忽略的文件
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDirX(string target, bool quit = true)
                {
                    return Execute(target, "-fd -x", quit);
                }

                /// <summary>
                /// 强制清理文件夹
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDir(string target, bool quit = true)
                {
                    return Execute(target, "-fd", quit);
                }
            }

            #endregion
        }

        #endregion
    }
}