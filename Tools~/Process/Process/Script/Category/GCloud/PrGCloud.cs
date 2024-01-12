/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.ComponentModel;
using System.Runtime.Remoting.Activation;

namespace AIO
{
    /// <summary>
    /// Google PrGsutil PrGCloud Platform
    /// </summary>
    [Url("https://github.com/AIO-GAME/Common/blob/main/Tools~/Process/Process/Script/Category/GCloud/PrGCloud.cs")]
    [Description("Google PrGsutil PrGCloud Platform")]
    public static partial class PrGCloud
    {
        /// <summary>
        /// gsutil 工具路径 
        /// </summary>
        /// <remarks>
        /// 如果找不到 请设置自定义路径
        /// </remarks>
        public static string Gsutil = "gsutil";


        /// <summary>
        /// gcloud 工具路径
        /// </summary>
        /// <remarks>
        /// 如果找不到 请设置自定义路径
        /// </remarks>
        public static string Gcloud = "gcloud";

        /// <summary>
        /// Create a new instance of PrGCloud
        /// </summary>
        /// <param name="cmd">工具</param>
        /// <param name="args">参数</param>
        /// <returns><see cref="PrGCloud"/></returns>
        private static IExecutor Create(string cmd, string args)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return PrCmd.Create().Input($"{cmd} {args}");
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    return PrMac.Create(cmd, args);
                default: throw new NotImplementedException();
            }
        }
    }
}