using System;

namespace AIO
{
    public partial class StringExtend
    {
        /// <summary>
        /// 从当前字符串的开头移除指定字符串。如果当前字符串不是以指定字符串开头，则返回原始字符串。
        /// </summary>
        /// <param name="source">要进行操作的字符串。</param>
        /// <param name="value">要移除的字符串。</param>
        /// <returns>移除指定字符串后的新字符串，或者如果当前字符串不是以指定字符串开头，则返回原始字符串。</returns>
        public static string TrimStart(
            this string source,
            in string value)
        {
            if (!source.StartsWith(value)) return source;
            return source.Substring(value.Length);
        }

        /// <summary>
        /// 将当前字符串截断到指定的最大长度，并在末尾添加可选的后缀。如果当前字符串长度小于等于指定的最大长度，则返回原始字符串。
        /// </summary>
        /// <param name="value">要进行操作的字符串。</param>
        /// <param name="maxLength">要截断的最大长度。</param>
        /// <param name="suffix">可选的后缀，默认值为 "..."。</param>
        /// <returns>截断后的新字符串，或者如果当前字符串长度小于等于指定的最大长度，则返回原始字符串。</returns>
        public static string Truncate(
            this string value,
            in int maxLength,
            in string suffix = "...")
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + suffix;
        }

        /// <summary>
        /// 从当前字符串的结尾移除指定字符串。如果当前字符串不是以指定字符串结尾，则返回原始字符串。
        /// </summary>
        /// <param name="source">要进行操作的字符串。</param>
        /// <param name="value">要移除的字符串。</param>
        /// <returns>移除指定字符串后的新字符串，或者如果当前字符串不是以指定字符串结尾，则返回原始字符串。</returns>
        public static string TrimEnd(
            this string source,
            in string value)
        {
            if (!source.EndsWith(value)) return source;
            return source.Remove(source.LastIndexOf(value, StringComparison.CurrentCulture));
        }
    }
}