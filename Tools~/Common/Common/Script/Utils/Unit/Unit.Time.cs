using System;

public partial class Unit
{
    /// <summary>
    /// 时间
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// 
        /// </summary>
        public enum SencondUnit
        {
            /// <summary> 秒 </summary>
            SECOND = 0,
            /// <summary> 毫秒 </summary>
            MILLISCOND,
            /// <summary> 微秒 </summary>
            MICROSECOND,
            /// <summary> 纳秒 </summary>
            NANOSECOND,
            /// <summary> 100纳秒计数单位 </summary>
            NANOSECOND_100,
        }

        /// <summary>
        /// 获取秒 计数单位 秒
        /// </summary>
        public static long GetSencondUnit(SencondUnit unit)
        {
            switch (unit)
            {
                case SencondUnit.MILLISCOND:
                    return MS_SECOND;
                case SencondUnit.MICROSECOND:
                    return μS_SECOND;
                case SencondUnit.NANOSECOND:
                    return NS_SECOND;
                case SencondUnit.NANOSECOND_100:
                    return NS_SECOND / 100;
                case SencondUnit.SECOND:
                default: return SECOND;
            }
        }

        /// <summary>
        /// 获取秒 计数单位 分
        /// </summary>
        public static long GetMinUnit(SencondUnit unit)
        {
            switch (unit)
            {
                case SencondUnit.MILLISCOND:
                    return MS_MIN;
                case SencondUnit.MICROSECOND:
                    return μS_MIN;
                case SencondUnit.NANOSECOND:
                    return NS_MIN;
                case SencondUnit.NANOSECOND_100:
                    return NS_MIN / 100;
                case SencondUnit.SECOND:
                default: return SECOND_MIN;
            }
        }

        /// <summary>
        /// 获取秒 计数单位 时
        /// </summary>
        public static long GetHourUnit(SencondUnit unit)
        {
            switch (unit)
            {
                case SencondUnit.MILLISCOND:
                    return MS_HOUR;
                case SencondUnit.MICROSECOND:
                    return μS_HOUR;
                case SencondUnit.NANOSECOND:
                    return NS_HOUR;
                case SencondUnit.NANOSECOND_100:
                    return NS_HOUR / 100;
                case SencondUnit.SECOND:
                default: return SECOND_HOUR;
            }
        }

        /// <summary>
        /// 获取秒 计数单位 时
        /// </summary>
        public static long GetDayUnit(SencondUnit unit)
        {
            switch (unit)
            {
                case SencondUnit.MILLISCOND:
                    return MS_DAY;
                case SencondUnit.MICROSECOND:
                    return μS_DAY;
                case SencondUnit.NANOSECOND:
                    return NS_DAY;
                case SencondUnit.NANOSECOND_100:
                    return NS_DAY / 100;
                case SencondUnit.SECOND:
                default: return SECOND_DAY;
            }
        }

        /// <summary>
        /// 获取秒 计数单位 时
        /// </summary>
        public static long GetWeekUnit(SencondUnit unit)
        {
            switch (unit)
            {
                case SencondUnit.MILLISCOND:
                    return MS_WEEK;
                case SencondUnit.MICROSECOND:
                    return μS_WEEK;
                case SencondUnit.NANOSECOND:
                    return NS_WEEK;
                case SencondUnit.NANOSECOND_100:
                    return NS_WEEK / 100;
                case SencondUnit.SECOND:
                default: return SECOND_WEEK;
            }
        }

        /// <summary>
        /// 时间间隔
        /// </summary>
        public enum DateTimeUnit
        {
            /// <summary> 日 </summary>
            Day = 0,
            /// <summary> 周 </summary>
            Week,
            /// <summary> 月 </summary>
            Month,
            /// <summary> 季 </summary>
            Season,
            /// <summary> 年 </summary>
            Year,
        }

        /// <summary> 一周7天 </summary>
        public const int UNIT_WEEK_DAY = 7;

        /// <summary> 一天24时 </summary>
        public const int UNIT_DAY_HOUR = 24;

        /// <summary> 一时60分 </summary>
        public const int UNIT_HOUR_MIN = 60;

        /// <summary> 一分60秒 </summary>
        public const int UNIT_MIN_SECOND = 60;

        /*--------------------------------------------------------------------------------------------------------------*/

        /// <summary> 秒 单位时间（秒） s </summary>
        public const int SECOND = 1;

        /// <summary> 分 单位时间（秒） s </summary>
        public const int SECOND_MIN = SECOND * UNIT_MIN_SECOND;

        /// <summary> 时 单位时间（秒） s </summary>
        public const int SECOND_HOUR = SECOND_MIN * UNIT_HOUR_MIN;

        /// <summary> 日 单位时间（秒） s </summary>
        public const int SECOND_DAY = SECOND_HOUR * UNIT_DAY_HOUR;

        /// <summary> 周 单位时间（秒） s </summary>
        public const int SECOND_WEEK = SECOND_DAY * UNIT_WEEK_DAY;

        /*--------------------------------------------------------------------------------------------------------------*/

        /// <summary> 秒 单位时间（毫秒） ms </summary>
        public const long MS_SECOND = SECOND * 1000;

        /// <summary> 分 单位时间（毫秒） ms  </summary>
        public const long MS_MIN = MS_SECOND * UNIT_MIN_SECOND;

        /// <summary> 时 单位时间（毫秒） ms  </summary>
        public const long MS_HOUR = MS_MIN * UNIT_HOUR_MIN;

        /// <summary> 日 单位时间（毫秒） ms  </summary>
        public const long MS_DAY = SECOND_DAY * UNIT_DAY_HOUR;

        /// <summary> 周 单位时间（毫秒） ms  </summary>
        public const long MS_WEEK = MS_DAY * UNIT_WEEK_DAY;

        /*--------------------------------------------------------------------------------------------------------------*/

        /// <summary> 秒 单位时间（微秒） μs </summary>
        public const long μS_SECOND = MS_SECOND * 1000;

        /// <summary> 分 单位时间（微秒） μs </summary>
        public const long μS_MIN = μS_SECOND * UNIT_MIN_SECOND;

        /// <summary> 时 单位时间（微秒） μs </summary>
        public const long μS_HOUR = μS_MIN * UNIT_HOUR_MIN;

        /// <summary> 天 单位时间（微秒） μs </summary>
        public const long μS_DAY = μS_HOUR * UNIT_DAY_HOUR;

        /// <summary> 周 单位时间（微秒） μs </summary>
        public const long μS_WEEK = μS_DAY * UNIT_WEEK_DAY;

        /*--------------------------------------------------------------------------------------------------------------*/

        /// <summary> 秒 单位时间（纳秒） ns </summary>
        public const long NS_SECOND = μS_SECOND * 1000;

        /// <summary> 分 单位时间（纳秒） ns </summary>
        public const long NS_MIN = NS_SECOND * UNIT_MIN_SECOND;

        /// <summary> 时 单位时间（纳秒） ns </summary>
        public const long NS_HOUR = NS_MIN * UNIT_HOUR_MIN;

        /// <summary> 天 单位时间（纳秒） ns </summary>
        public const long NS_DAY = NS_HOUR * UNIT_DAY_HOUR;

        /// <summary> 周 单位时间（纳秒） ns </summary>
        public const long NS_WEEK = NS_DAY * UNIT_WEEK_DAY;

        /*--------------------------------------------------------------------------------------------------------------*/

        /// <summary> 1毫秒 </summary>
        public static TimeSpan Milliseconds { get; } = new TimeSpan(0, 0, 0, 0, 1);

        /// <summary> 1秒钟 </summary>
        public static TimeSpan Second { get; } = new TimeSpan(0, 0, 1);

        /// <summary> 1分钟 </summary>
        public static TimeSpan Minute { get; } = new TimeSpan(0, 1, 0);

        /// <summary> 1小时 </summary>
        public static TimeSpan Hour { get; } = new TimeSpan(1, 0, 0);

        /// <summary> 1天 </summary>
        public static TimeSpan Day { get; } = new TimeSpan(1, 0, 0, 0, 0);

        /*--------------------------------------------------------------------------------------------------------------*/

        /// <summary> 格林威治时间UTC参照点：1970年1月1日0时0分0秒 </summary>
        public static readonly DateTime GREENWICH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

    }
}
