#region

using System;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: TimeStamp

        /// <summary>
        /// TimeStamp 时间戳
        /// </summary>
        public class TimeStamp
        {
            private TimeStamp() { }

            /// <summary>
            /// Get current timestamp / 当前时间戳，单位是毫秒
            /// </summary>
            public static long NowMillisecond => (DateTime.UtcNow.ToUniversalTime().Ticks - 621355968000000000) / 10000;

            /// <summary>
            /// Get current timestamp / 当前时间戳，单位是秒
            /// </summary>
            public static long NowSecond => (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;

            /// <summary>
            /// 毫秒级时间戳转为时间
            /// </summary>
            /// <param name="timestamp">毫秒级时间磋</param>
            public static DateTime MillisecondToDateTime(long timestamp)
            {
                return timestamp.Equals(0)
                    ? DateTime.Now
                    : TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddMilliseconds(timestamp);
            }

            /// <summary>
            /// 秒级时间戳转为时间
            /// </summary>
            /// <param name="timestamp">秒级时间磋</param>
            public static DateTime SecondToDateTime(long timestamp)
            {
                return timestamp.Equals(0)
                    ? DateTime.Now
                    : TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(timestamp);
            }

            /// <summary>
            ///  DateTime时间格式转换为Unix时间戳格式
            /// </summary>
            public static long ConvertDateTime(DateTime time)
            {
                return (time.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            }
        }

        #endregion
    }
}