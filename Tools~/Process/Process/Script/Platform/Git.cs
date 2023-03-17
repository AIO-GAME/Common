/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.IO;

namespace AIO
{
    /// <summary>
    /// 命令 无需判断平台
    /// </summary>
    public static partial class PrPlatform
    {
        /// <summary>
        /// 
        /// </summary>
        public static partial class Git
        {
            /// <summary>
            /// 获取有效Git路径
            /// </summary>
            private static List<string> GetValidUrl(string target, ICollection<string> urls)
            {
                var list = new List<string>();
                foreach (var item in urls)
                {
                    var name = Path.GetFileName(item).Replace(".git", "").Replace(".ssh", "");
                    var path = Path.Combine(target, name);
                    if (!Directory.Exists(path)) list.Add(item);
                }

                return list;
            }

            #region Remote

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target"></param>
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
            /// <param name="targets"></param>
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
            /// <param name="target"></param>
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
            /// <param name="target"></param>
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
            /// <param name="targets"></param>
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
            /// <param name="target"></param>
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
            /// 
            /// </summary>
            /// <param name="target"></param>
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
            /// 
            /// </summary>
            /// <param name="target"></param>
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
            /// 
            /// </summary>
            /// <param name="targets"></param>
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
            /// 
            /// </summary>
            /// <param name="targets"></param>
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
            /// 
            /// </summary>
            /// <param name="targets"></param>
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
            /// 
            /// </summary>
            /// <param name="target"></param>
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
            /// 
            /// </summary>
            /// <param name="target"></param>
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
            /// 
            /// </summary>
            /// <param name="targets"></param>
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
            /// 
            /// </summary>
            /// <param name="target"></param>
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
            /// 
            /// </summary>
            /// <param name="targets"></param>
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
        }
    }
}