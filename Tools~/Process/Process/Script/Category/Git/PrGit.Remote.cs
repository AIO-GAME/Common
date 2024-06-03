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
        #region Nested type: Remote

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Remote"/> 远端
        /// </summary>
        public static class Remote
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
                return Create(work, "remote {0}", args);
            }

            /// <summary>
            /// 获取远端库
            /// </summary>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ListName(in string work)
            {
                return Create(work, "remote");
            }

            /// <summary>
            /// 获取远端库
            /// </summary>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ListNameAll(in string work)
            {
                return Create(work, "remote -v show");
            }

            /// <summary>
            /// 添加远端库
            /// </summary>
            /// <param name="work">Git 库</param>
            /// <param name="remoteName">远端名</param>
            /// <param name="url">远端路径</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Add(in string work, in string remoteName, in string url)
            {
                if (string.IsNullOrEmpty(remoteName)) throw new ArgumentNullException();
                if (string.IsNullOrEmpty(url)) throw new ArgumentNullException();
                return Create(work, "remote -add {0} {1}", remoteName, url);
            }

            /// <summary>
            /// 远端库 添加指定远端关联库
            /// </summary>
            /// <param name="work">Git 库</param>
            /// <param name="remoteName">远端名</param>
            /// <param name="url">远端路径</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor SetUrlAdd(in string work, in string remoteName, in string url)
            {
                if (string.IsNullOrEmpty(remoteName)) throw new ArgumentNullException();
                if (string.IsNullOrEmpty(url)) throw new ArgumentNullException();
                return Create(work, "remote set-url --add {0} {1}", remoteName, url);
            }

            /// <summary>
            /// 远端库 删除指定远端关联库
            /// </summary>
            /// <param name="work">Git 库</param>
            /// <param name="remoteName">远端名</param>
            /// <param name="url">远端路径</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor SetUrlDel(in string work, in string remoteName, in string url)
            {
                if (string.IsNullOrEmpty(remoteName)) throw new ArgumentNullException();
                if (string.IsNullOrEmpty(url)) throw new ArgumentNullException();
                return Create(work, "remote set-url --delete {0} {1}", remoteName, url);
            }

            /// <summary>
            /// 远端库 删除远端库
            /// </summary>
            /// <param name="work">Git 库</param>
            /// <param name="remoteName">远端名</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Remove(in string work, in string remoteName)
            {
                if (string.IsNullOrEmpty(remoteName)) throw new ArgumentNullException();
                return Create(work, "remote remove {0}", remoteName);
            }

            /// <summary>
            /// 远端库 重命名远端库
            /// </summary>
            /// <param name="work">Git 库</param>
            /// <param name="oldRemoteName">老</param>
            /// <param name="newRemoteName">新</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Remove(in string work, in string oldRemoteName, in string newRemoteName)
            {
                if (string.IsNullOrEmpty(oldRemoteName)) throw new ArgumentNullException();
                if (string.IsNullOrEmpty(newRemoteName)) throw new ArgumentNullException();
                return Create(work, new[] { "remote rename {0} {1}", oldRemoteName, newRemoteName });
            }
        }

        #endregion
    }
}