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
            /// <see cref="PrPlatform"/> Git Add
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="args">默认参数 .</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Add(string target, string args = ".", bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Add.Execute(target, args, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Add.Execute(target, args, quit);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// <see cref="PrPlatform"/> Git Add
            /// </summary>
            /// <param name="targets">目标路径</param>
            /// <param name="args">默认参数 .</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Add(ICollection<string> targets, string args = ".", bool quit = false)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.Git.Add.Execute(targets, args, quit);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Git.Add.Execute(targets, args, quit);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
