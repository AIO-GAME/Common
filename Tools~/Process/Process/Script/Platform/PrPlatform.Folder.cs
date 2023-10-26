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
        /// 文件夹命令
        /// </summary>
        public static class Folder
        {
            /// <summary>
            /// 链接文件夹
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="source">源路径</param>
            /// <param name="args">参数</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Link(string target, string source, in string args)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrCmd.MkLink.Execute(target, source, args);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Ln.Execute(source, target, args);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// 软链接文件夹
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="source">源路径</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Symbolic(string target, string source)
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
                        return PrMac.Ln.Symbolic(source, target);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// 硬链接文件夹
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="source">源路径</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Hard(string target, string source)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrCmd.MkLink.HardDirectory(target, source);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Ln.Hard(source, target);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
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
            /// 创建文件夹
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
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

            /// <summary>
            /// 移动文件夹
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="source">源路径</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Move(string target, string source)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrCmd.Move.Execute(target, source);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Move.Execute(target, source);
                    default: throw new NotImplementedException();
                }
            }

            /// <summary>
            /// 移动文件夹
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <param name="source">源路径</param>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Copy(string target, string source)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        return PrWin.XCopy.Directory(target, source);
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        return PrMac.Cp.Execute(target, source);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}