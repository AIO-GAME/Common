/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;

namespace AIO
{
    /// <summary>
    /// 命令 无需判断平台
    /// </summary>
    public partial class PrPlatform
    {
        /// <summary>
        ///
        /// </summary>
        public static class File
        {
            #region Link

            /// <summary>
            /// 符号链接
            /// </summary>
            public static IExecutor Link(string target, string source)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrCmd.MkLink.Symbolic(target, source);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.In.Execute(source, target, "-s");
                    default: throw new NotImplementedException();
                }
            }

            #endregion

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
                        return PrCmd.Del.ALL(target);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Rm.File(target);
                    case PlatformID.Xbox:
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}