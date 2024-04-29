#region

using System;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// Long 扩展
    /// </summary>
    public static class ExtendLong
    {
        /// <summary>
        /// 单位
        /// </summary>
        private static readonly string[] unitStr = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

        /// <summary>
        /// 将文件大小(字节)转换为最适合的显示方式
        /// Generate data size string. Will return a pretty string of bytes, KiB, MiB, GiB, TiB based on the given bytes.
        /// </summary>
        /// <param name="size">Data size in bytes</param>
        /// <param name="retain">保留小数点后几位</param>
        /// <returns>String with data size representation</returns>
        public static string ToConverseStringFileSize(this long size, uint retain = 2)
        {
            if (size < 0) return "Unknown";
            if (size == 0) return "0 B";

            double bytes                                         = size;
            ushort suffixIndex                                   = 0;
            int    limit                                         = unitStr.Length - 1;
            while (bytes >= 1024 && suffixIndex++ < limit) bytes /= 1024;
            return $"{bytes.ToString($"N{retain}").TrimEnd('0').TrimEnd('.')} {unitStr[suffixIndex]}";
        }

        /// <summary>
        /// 将文件大小(字节)转换为最适合的显示方式
        /// Generate data size string. Will return a pretty string of bytes, KiB, MiB, GiB, TiB based on the given bytes.
        /// </summary>
        /// <param name="size">Data size in bytes</param>
        /// <param name="retain">保留小数点后几位</param>
        /// <returns>String with data size representation</returns>
        public static string ToConverseStringFileSize(this ulong size, uint retain = 2)
        {
            if (size == 0) return "0 B";

            double bytes                                         = size;
            ushort suffixIndex                                   = 0;
            var    limit                                         = unitStr.Length - 1;
            while (bytes >= 1024 && suffixIndex++ < limit) bytes /= 1024;
            return $"{bytes.ToString($"N{retain}").TrimEnd('0', '.')} {unitStr[suffixIndex]}";
        }

        /// <summary>
        /// 转换为格式:00,000,000
        /// </summary>
        public static string ToConverseStringMoney(this long value)
        {
            var tmp = value % 1000;
            var str = tmp.ToString();
            value /= 1000;
            while (value > 0)
            {
                if (tmp >= 0 && tmp < 10) str.AppendToLast("00");
                else if (tmp >= 10 && tmp < 100) str = "0" + str;
                tmp   =  value % 1000;
                str   =  tmp + "," + str;
                value /= 1000;
            }

            return str;
        }

        /// <summary>
        /// Generate time period string. Will return a pretty string of ns, mcs, ms, s, m, h based on the given nanoseconds.
        /// </summary>
        /// <param name="ms">Milliseconds</param>
        /// <returns>String with time period representation</returns>
        public static string ToConverseTimePeriod(this long ms) { return ToConverseTimePeriod((double)ms); }

        /// <summary>
        /// Generate time period string. Will return a pretty string of ns, mcs, ms, s, m, h based on the given nanoseconds.
        /// </summary>
        /// <param name="ms">Milliseconds</param>
        /// <returns>String with time period representation</returns>
        public static string ToConverseTimePeriod(this double ms)
        {
            var sb = new StringBuilder();

            var nanoseconds    = (long)(ms * 1000.0 * 1000.0);
            var absNanoseconds = Math.Abs(nanoseconds);

            if (absNanoseconds >= 60 * 60 * 1000000000L)
            {
                var hours        = nanoseconds / (60 * 60 * 1000000000L);
                var minutes      = nanoseconds % (60 * 60 * 1000000000L) / 1000000000 / 60;
                var seconds      = nanoseconds % (60 * 60 * 1000000000L) / 1000000000 % 60;
                var milliseconds = nanoseconds % (60 * 60 * 1000000000L) % 1000000000 / 1000000;
                sb.Append(hours);
                sb.Append(':');
                sb.Append(minutes < 10 ? "0" : "");
                sb.Append(minutes);
                sb.Append(':');
                sb.Append(seconds < 10 ? "0" : "");
                sb.Append(seconds);
                sb.Append('.');
                sb.Append(milliseconds < 100 ? "0" : "");
                sb.Append(milliseconds < 10 ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" h");
            }
            else if (absNanoseconds >= 60 * 1000000000L)
            {
                var minutes      = nanoseconds / (60 * 1000000000L);
                var seconds      = nanoseconds % (60 * 1000000000L) / 1000000000;
                var milliseconds = nanoseconds % (60 * 1000000000L) % 1000000000 / 1000000;
                sb.Append(minutes);
                sb.Append(':');
                sb.Append(seconds < 10 ? "0" : "");
                sb.Append(seconds);
                sb.Append('.');
                sb.Append(milliseconds < 100 ? "0" : "");
                sb.Append(milliseconds < 10 ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" m");
            }
            else if (absNanoseconds >= 1000000000)
            {
                var seconds      = nanoseconds / 1000000000;
                var milliseconds = nanoseconds % 1000000000 / 1000000;
                sb.Append(seconds);
                sb.Append('.');
                sb.Append(milliseconds < 100 ? "0" : "");
                sb.Append(milliseconds < 10 ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" s");
            }
            else if (absNanoseconds >= 1000000)
            {
                var milliseconds = nanoseconds / 1000000;
                var microseconds = nanoseconds % 1000000 / 1000;
                sb.Append(milliseconds);
                sb.Append('.');
                sb.Append(microseconds < 100 ? "0" : "");
                sb.Append(microseconds < 10 ? "0" : "");
                sb.Append(microseconds);
                sb.Append(" ms");
            }
            else if (absNanoseconds >= 1000)
            {
                var microseconds = nanoseconds / 1000;
                nanoseconds = nanoseconds % 1000;
                sb.Append(microseconds);
                sb.Append('.');
                sb.Append(nanoseconds < 100 ? "0" : "");
                sb.Append(nanoseconds < 10 ? "0" : "");
                sb.Append(nanoseconds);
                sb.Append(" mcs");
            }
            else
            {
                sb.Append(nanoseconds);
                sb.Append(" ns");
            }

            return sb.ToString();
        }
    }
}