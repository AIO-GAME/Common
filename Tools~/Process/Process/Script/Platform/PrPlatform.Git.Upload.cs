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
            /// <see cref="PrPlatform"/> Git Upload
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="inputCommit">输入提交信息</param>
            /// <param name="inputOrigin">输入上传分支</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
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
            /// <see cref="PrPlatform"/> Git Upload
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="inputCommit">输入提交信息</param>
            /// <param name="inputOrigin">输入上传分支</param>
            /// <param name="quit">静默退出</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
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
        }
    }
}
