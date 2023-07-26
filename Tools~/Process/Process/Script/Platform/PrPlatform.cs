/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.IO;

namespace AIO
{
    /// <summary>
    /// 命令 无需判断平台
    /// </summary>
    public static partial class PrPlatform
    {
        /// <summary>
        /// 打开路径
        /// </summary>
        public static class Open
        {
            /// <summary>
            /// 目标路径
            /// </summary>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Path(string target)
            {
                if (Directory.Exists(target) || System.IO.File.Exists(target))
                {
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                            return PrWin.Open.Path(target);
                        case PlatformID.MacOSX:
                        case PlatformID.Unix:
                            return PrMac.Open.Path(target);
                        default: throw new NotImplementedException();
                    }
                }

                throw new FileNotFoundException("The current path does not exist");
            }
        }
    }
}