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
        /// 
        /// </summary>
        public static class Open
        {
            /// <summary>
            /// 
            /// </summary>
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