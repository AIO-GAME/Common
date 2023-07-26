/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-26
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AIO
{
    public partial class PrPlatform
    {
        public partial class Git
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


            /// <summary>
            /// <see cref="PrPlatform"/> Git Clone
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="urls"></param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Clone(string target, ICollection<string> urls, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clone.Execute(target, GetValidUrl(target, urls), quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clone.Execute(target, GetValidUrl(target, urls), quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// <see cref="PrPlatform"/> Git Clone
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="url"></param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Clone(string target, string url, bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clone.Execute(target, GetValidUrl(target, new string[] { url }), quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clone.Execute(target, GetValidUrl(target, new string[] { url }), quit);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}