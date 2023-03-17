/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System;

    /// <summary>
    /// 命令 无需判断平台
    /// </summary>
    public partial class PrPlatform
    {
        /// <summary>
        ///
        /// </summary>
        public static class Folder
        {
            /// <summary>
            ///
            /// </summary>
            public static IExecutor Link(string target, string source)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrCmd.MkLink.Directory(target, source);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.In.Execute(source, target);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            ///
            /// </summary>
            public static IExecutor Del(string target)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrCmd.Rmdir.Execute(target);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Rm.Directory(target);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            ///
            /// </summary>
            public static IExecutor Create(string target)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrCmd.Mkdir.Directory(target);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Mkdir.Directory(target);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}