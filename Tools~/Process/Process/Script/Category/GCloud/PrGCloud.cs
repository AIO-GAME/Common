/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    /// <summary>
    /// Google Cloud Platform
    /// </summary>
    public sealed partial class PrGCloud : PrCourse
    {
        [DebuggerHidden, DebuggerNonUserCode]
        private PrGCloud()
        {
        }

        /// <summary>
        /// 命令
        /// </summary>
        public const string CMD = "gcloud";

        /// <summary>
        /// 用法
        /// </summary>
        public static class Usage
        {
            /// <summary>
            /// 存储
            /// </summary>
            public const string Storage = "storage";

            /// <summary>
            /// 帮助
            /// </summary>
            public const string Help = "help";
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns><see cref="IExecutor"/></returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public override IExecutor Execute()
        {
            try
            {
                SetFileName("cmd");
                SetInCreateNoWindow(true);
                SetInUseShellExecute(false);
                SetRedirectError(true);
                SetRedirectOutput(true);

                if (string.IsNullOrEmpty(Info.WorkingDirectory))
                    Info.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                encoding = encoding ?? Encoding.UTF8;

#if (NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1)
                if (p.StartInfo.RedirectStandardInput) p.StartInfo.StandardInputEncoding = encoding;
#endif

                if (Info.RedirectStandardOutput) Info.StandardOutputEncoding = encoding;
                if (Info.RedirectStandardError) Info.StandardErrorEncoding = encoding;
            }
            catch (Exception ex)
            {
                return new ExecutorException(Info, ex);
            }

            return new Executor(Info, EnableOutput);
        }

        private static IExecutor Create()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return PrCmd.Create();
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    return new PrMac().Execute();
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Create a new instance of PrGCloud
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns><see cref="PrGCloud"/></returns>
        public static IExecutor Create(string args)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return PrCmd.Create().Input($"{CMD} {args}");
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    return new PrMac().Execute().Input($"{CMD} {args}");
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Create a new instance of PrGCloud
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns><see cref="PrGCloud"/></returns>
        public static IExecutor Create(IEnumerable<string> args)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return PrCmd.Create().Input($"{CMD} {string.Join(" ", args)}".Trim());
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    return new PrMac().Execute().Input($"{CMD} {string.Join(" ", args)}".Trim());
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Create a new instance of PrGCloud
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns><see cref="PrGCloud"/></returns>
        public static IExecutor Create(params string[] args)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return PrCmd.Create().Input($"{CMD} {string.Join(" ", args)}".Trim());
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    return new PrMac().Execute().Input($"{CMD} {string.Join(" ", args)}".Trim());
                default: throw new NotImplementedException();
            }
        }
    }
}