/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Runtime.CompilerServices;

public partial class Utils
{
    /// <summary>
    /// 设备信息 .NET API
    /// </summary>
    public static partial class SystemInfoX
    {
        /// <summary>
        /// 获取用户设备名
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEquipmentName()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// 获取平台名称
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlatformID GetPlatform()
        {
            return Environment.OSVersion.Platform;
        }

        /// <summary>
        /// 获取平台版本号
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Version GetNowVersion()
        {
            return Environment.OSVersion.Version;
        }

        /// <summary>
        /// 获取当前设备 连接网络名称
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetUserDomainName()
        {
            return Environment.UserDomainName;
        }

        /// <summary>
        /// 获取系统跟目录
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetSystemDirectory()
        {
            return Environment.SystemDirectory;
        }

        /// <summary>
        /// 获取系统当前时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetNowTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取系统启动后 经过的毫秒数
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetStartSystemTickCount()
        {
            return Environment.TickCount;
        }
    }
}
