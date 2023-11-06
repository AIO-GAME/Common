using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIO
{
    /// <summary>
    /// Long 扩展
    /// </summary>
    public static class LongExtend
    {
        /// <summary>
        /// 将文件大小(字节)转换为最适合的显示方式
        /// Generate data size string. Will return a pretty string of bytes, KiB, MiB, GiB, TiB based on the given bytes.
        /// </summary>
        /// <param name="size">Data size in bytes</param>
        /// <returns>String with data size representation</returns>
        public static string ToConverseStringFileSize(this long size)
        {
            var sb = new StringBuilder();
            var bytes = size;
            var absBytes = Math.Abs(bytes);

            if (absBytes >= 1024L * 1024L * 1024L * 1024L)
            {
                var tb = bytes / (1024L * 1024L * 1024L * 1024L);
                var gb = bytes % (1024L * 1024L * 1024L * 1024L) / (1024 * 1024 * 1024);
                sb.Append(tb);
                sb.Append('.');
                sb.Append((gb < 100) ? "0" : "");
                sb.Append((gb < 10) ? "0" : "");
                sb.Append(gb);
                sb.Append(" TB");
            }
            else if (absBytes >= 1024 * 1024 * 1024)
            {
                var gb = bytes / (1024 * 1024 * 1024);
                var mb = bytes % (1024 * 1024 * 1024) / (1024 * 1024);
                sb.Append(gb);
                sb.Append('.');
                sb.Append((mb < 100) ? "0" : "");
                sb.Append((mb < 10) ? "0" : "");
                sb.Append(mb);
                sb.Append(" GB");
            }
            else if (absBytes >= (1024 * 1024))
            {
                var mb = bytes / (1024 * 1024);
                var kb = bytes % (1024 * 1024) / 1024;
                sb.Append(mb);
                sb.Append('.');
                sb.Append((kb < 100) ? "0" : "");
                sb.Append((kb < 10) ? "0" : "");
                sb.Append(kb);
                sb.Append(" MB");
            }
            else if (absBytes >= 1024)
            {
                var kb = bytes / 1024;
                bytes %= 1024;
                sb.Append(kb);
                sb.Append('.');
                sb.Append(bytes < 100 ? "0" : "");
                sb.Append(bytes < 10 ? "0" : "");
                sb.Append(bytes);
                sb.Append(" KB");
            }
            else
            {
                sb.Append(bytes);
                sb.Append(" bytes");
            }

            return sb.ToString();
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
                tmp = value % 1000;
                str = tmp + "," + str;
                value /= 1000;
            }

            return str;
        }


        /// <summary>
        /// Generate time period string. Will return a pretty string of ns, mcs, ms, s, m, h based on the given nanoseconds.
        /// </summary>
        /// <param name="ms">Milliseconds</param>
        /// <returns>String with time period representation</returns>
        public static string ToConverseTimePeriod(this long ms)
        {
            var sb = new StringBuilder();

            var nanoseconds = (long)(ms * 1000.0 * 1000.0);
            var absNanoseconds = Math.Abs(nanoseconds);

            if (absNanoseconds >= (60 * 60 * 1000000000L))
            {
                var hours = nanoseconds / (60 * 60 * 1000000000L);
                var minutes = nanoseconds % (60 * 60 * 1000000000L) / 1000000000 / 60;
                var seconds = nanoseconds % (60 * 60 * 1000000000L) / 1000000000 % 60;
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
            else if (absNanoseconds >= (60 * 1000000000L))
            {
                var minutes = nanoseconds / (60 * 1000000000L);
                var seconds = (nanoseconds % (60 * 1000000000L)) / 1000000000;
                var milliseconds = ((nanoseconds % (60 * 1000000000L)) % 1000000000) / 1000000;
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
                var seconds = nanoseconds / 1000000000;
                var milliseconds = (nanoseconds % 1000000000) / 1000000;
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
                var microseconds = (nanoseconds % 1000000) / 1000;
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
                sb.Append((nanoseconds < 100) ? "0" : "");
                sb.Append((nanoseconds < 10) ? "0" : "");
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