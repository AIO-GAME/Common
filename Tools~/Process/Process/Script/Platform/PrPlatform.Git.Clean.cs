/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-26
|||✩ Document: ||| -> 
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
            /// <see cref="PrPlatform"/> Git Clean
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="args">默认参数-fdx</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Clean(ICollection<string> targets, string args = "-fdx", bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clean.Execute(targets, args, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clean.Execute(targets, args, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// <see cref="PrPlatform"/> Git Clean
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="args">默认参数-fdx</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Clean(string target, string args = "-fdx", bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Clean.Execute(target, args, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Clean.Execute(target, args, quit);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}