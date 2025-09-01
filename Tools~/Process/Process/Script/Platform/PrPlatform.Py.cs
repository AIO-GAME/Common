#region

using System;
using System.IO;

#endregion

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
        public static class Python
        {
            /// <summary>
            /// 目标路径
            /// </summary>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Run(FileSystemInfo target, DirectoryInfo workdir, params string[] args)
            {
                if (!target.Exists) throw new FileNotFoundException("Python脚本文件不存在", target.FullName);
                PrCourse cmd = null;
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        cmd = new PrWin();
                        cmd.SetFileName("python");
                        break;
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        cmd = new PrMac();
                        cmd.SetFileName("python3");
                        break;
                    default:
                        cmd = new PrEmpty();
                        break;
                }

                if (workdir.Exists) cmd.SetWorkingDir(workdir.FullName);
                else throw new DirectoryNotFoundException($"工作目录不存在: {workdir.FullName}");
                cmd.SetInArgs($"{target} {string.Join(" ", args)}");
                return cmd.Execute();
            }

            /// <summary>
            /// 目标路径
            /// </summary>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Run(string target, DirectoryInfo workdir, params string[] args)
            {
                var file = new FileInfo(target);
                return Run(file, workdir, args);
            }

            /// <summary>
            /// 目标路径
            /// </summary>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Run(FileInfo target, params string[] args)
            {
                if (!target.Exists) throw new FileNotFoundException("Python脚本文件不存在", target.FullName);
                PrCourse cmd = null;
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        cmd = new PrWin();
                        cmd.SetFileName("python");
                        break;
                    case PlatformID.MacOSX:
                    case PlatformID.Unix:
                        cmd = new PrMac();
                        cmd.SetFileName("python3");
                        break;
                    default:
                        cmd = new PrEmpty();
                        break;
                }

                cmd.SetInArgs($"{target.FullName} {string.Join(" ", args)}");
                return cmd.Execute();
            }

            /// <summary>
            /// 目标路径
            /// </summary>
            /// <exception cref="NotImplementedException">未实现</exception>
            /// <returns><see cref="IExecutor"/>执行器</returns>
            public static IExecutor Run(string target, params string[] args) { return Run(new FileInfo(target), args); }
        }
    }
}