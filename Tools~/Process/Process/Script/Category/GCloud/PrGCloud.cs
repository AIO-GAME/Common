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
    public sealed partial class PrGCloud
    {
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
                    return PrMac.Create(CMD);
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
                    return PrMac.Create(CMD, args);
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
            return Create(string.Join(" ", args));
        }

        /// <summary>
        /// Create a new instance of PrGCloud
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns><see cref="PrGCloud"/></returns>
        public static IExecutor Create(params string[] args)
        {
            return Create(string.Join(" ", args));
        }
    }
}