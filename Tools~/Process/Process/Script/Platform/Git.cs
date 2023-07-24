/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// 命令 无需判断平台
    /// </summary>
    public static partial class PrPlatform
    {
        /// <summary>
        /// Git命令
        /// </summary>
        public static partial class Git
        {
            /// <summary>
            /// 获取有效Git路径
            /// </summary>
            private static List<string> GetValidUrl(string target, IEnumerable<string> urls)
            {
                return (from item in urls
                    let name = Path.GetFileName(item).Replace(".git", "").Replace(".ssh", "")
                    let path = Path.Combine(target, name)
                    where !Directory.Exists(path)
                    select item).ToList();
            }

            #region Remote

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor RemoteSetUrl(string target, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.RemoteSetUrl(target, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.RemoteSetUrl(target, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor RemoteSetUrl(ICollection<string> targets, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.RemoteSetUrl(targets, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.RemoteSetUrl(targets, quit);
                    default: throw new NotImplementedException();
                }
            }

            #endregion

            #region Clone

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="urls"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Clone(string target, ICollection<string> urls, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clone(target, GetValidUrl(target, urls), quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clone(target, GetValidUrl(target, urls), quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="url"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Clone(string target, string url, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clone(target, GetValidUrl(target, new string[] { url }), quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clone(target, GetValidUrl(target, new string[] { url }), quit);
                    default: throw new NotImplementedException();
                }
            }

            #endregion

            #region Pull

            /// <summary>
            /// 
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Pull(ICollection<string> targets, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Pull(targets, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Pull(targets, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Pull(string target, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Pull(target, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Pull(target, quit);
                    default: throw new NotImplementedException();
                }
            }

            #endregion

            /// <summary>
            /// Git上传
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="inputCommit"></param>
            /// <param name="inputOrigin"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Upload(string target, bool inputCommit = false, bool inputOrigin = false,
                bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Upload(target, inputCommit, inputOrigin, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Upload(target, inputCommit, inputOrigin, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git上传
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="inputCommit"></param>
            /// <param name="inputOrigin"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Upload(ICollection<string> target, bool inputCommit = false, bool inputOrigin = false,
                bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Upload(target, inputCommit, inputOrigin, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Upload(target, inputCommit, inputOrigin, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 提交
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Commit(ICollection<(string, string)> targets,
                bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Commit(targets, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Commit(targets, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 提交
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Commit(ICollection<string> targets, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Commit(targets, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Commit(targets, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 提交
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Commit(string targets, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Commit(targets, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Commit(targets, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 提交
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Commit((string, string) target, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Commit(target, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Commit(target, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 推送
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Push((string, string) target, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Push(target, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Commit(target, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 推送
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Push(ICollection<(string, string)> targets, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Push(targets, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Commit(targets, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 添加
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Add(string target, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Add(target, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Add(target, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 添加
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static IExecutor Add(ICollection<string> targets, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Add(targets, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Add(targets, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 添加
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="args">参数</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            public static IExecutor Clean(ICollection<string> targets, string args = "-fd -x", bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clean(targets, args, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clean(targets, args, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// Git 添加
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="args">参数</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            public static IExecutor Clean(string targets, string args = "-fd -x", bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clean(targets, args, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clean(targets, args, quit);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}