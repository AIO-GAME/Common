/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-07-26

|||✩ - - - - - |*/


using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class PrPlatform
    {
        public partial class Git
        {
            /// <summary>
            /// <see cref="PrPlatform"/> Git Push
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
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
                        return PrMac.Git.Push(target, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// <see cref="PrPlatform"/> Git Push
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
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
                        return PrMac.Git.Push(targets, quit);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
