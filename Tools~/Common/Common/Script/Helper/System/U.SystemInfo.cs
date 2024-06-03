#region

using System;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: SystemInfo

        /// <summary>
        /// 设备信息 .NET API
        /// </summary>
        public class SystemInfo
        {
            /// <summary>
            /// 获取用户设备名
            /// </summary>
            public static string GetEquipmentName()
            {
                return Environment.MachineName;
            }

            /// <summary>
            /// 获取平台名称
            /// </summary>
            public static PlatformID GetPlatform()
            {
                return Environment.OSVersion.Platform;
            }

            /// <summary>
            /// 获取平台版本号
            /// </summary>
            public static System.Version GetNowVersion()
            {
                return Environment.OSVersion.Version;
            }

            /// <summary>
            /// 获取当前设备 连接网络名称
            /// </summary>
            public static string GetUserDomainName()
            {
                return Environment.UserDomainName;
            }

            /// <summary>
            /// 获取系统跟目录
            /// </summary>
            public static string GetSystemDirectory()
            {
                return Environment.SystemDirectory;
            }

            /// <summary>
            /// 获取系统当前时间
            /// </summary>
            public static string GetNowTime()
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            /// <summary>
            /// 获取系统启动后 经过的毫秒数
            /// </summary>
            public static int GetStartSystemTickCount()
            {
                return Environment.TickCount;
            }
        }

        #endregion
    }
}