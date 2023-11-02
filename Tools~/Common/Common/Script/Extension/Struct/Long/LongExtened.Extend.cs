using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// Long 扩展
    /// </summary>
    public static class LongExtend
    {
        /// <summary>
        /// 将文件大小(字节)转换为最适合的显示方式
        /// </summary>
        public static string ToConverseStringFileSize(this long size)
        {
            var result = "0KB";
            var length = size.ToString().Length;
            if (length < 4)
                result = string.Concat(size, "byte");
            else if (length < 7)
                result = string.Concat(Math.Round(Convert.ToDouble(size / 1024d), 2), "KB");
            else if (length < 10)
                result = string.Concat(Math.Round(Convert.ToDouble(size / 1024d / 1024), 2), "MB");
            else if (length < 13)
                result = string.Concat(Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024), 2), "GB");
            else if (length < 17)
                result = string.Concat(Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024 / 1024), 2), "TB");
            return result;
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
    }
}