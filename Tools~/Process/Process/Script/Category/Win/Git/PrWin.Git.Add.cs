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
            #region Nested type: Add

            /// <summary>
            /// 添加命令
            /// </summary>
            public static class Add
            {
                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Add"/>
                /// </summary>
                /// <param name="targets">文件路径列表</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(ICollection<string> targets, string args, bool quit = true)
                {
                    var STR = GetExecute(targets, string.Concat("add ", args));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Add"/>
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(string target, string args, bool quit = true)
                {
                    var STR = GetExecute(target, string.Concat("add ", args));
                    return Git.Execute(STR, quit);
                }


                /// <summary>
                /// 添加全部修改文件
                /// </summary>
                /// <param name="targets">文件路径列表</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor ALL(ICollection<string> targets, bool quit = true)
                {
                    return Execute(targets, ".", quit);
                }

                /// <summary>
                /// 添加全部修改文件
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor ALL(string target, bool quit = true)
                {
                    return Execute(target, ".", quit);
                }
            }

            #endregion
        }

        #endregion
    }
}