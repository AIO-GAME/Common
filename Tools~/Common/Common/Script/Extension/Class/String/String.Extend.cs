#region

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace AIO
{
    /*
    1)、Length：获得当前字符串中字符的个数
    2)、ToUpper():将字符转换成大写形式
    3)、ToLower():将字符串转换成小写形式
    4)、Equals(lessonTwo,StringComparison.OrdinalIgnoreCase):比较两个字符串，可以忽略大小写
    5)、Split()：分割字符串，返回字符串类型的数组。注：第二个参数为：StringSplitOptions.RemoveEmptyEntries 时表示移除空格。
    6)、Substring()：截取字符串。在截取的时候包含要截取的那个位置。
    7)、IndexOf():判断某个字符串在字符串中第一次出现的位置，如果没有返回-1、值类型和引用类型在内存上存储的地方不一样。
    8)、LastIndexOf()：判断某个字符串在字符串中最后一次出现的位置，如果没有同样返回-1
    9)、StartsWith():判断是否以....开始
    10)、EndsWith():判断是否以...结束.
    11)、Replace():将字符串中某个字符串替换成一个新的字符串
    12)、Contains():判断某个字符串是否包含指定的字符串
    13)、Trim():去掉字符串中前后的空格
    14)、TrimEnd()：去掉字符串中结尾的空格
    15)、TrimStart()：去掉字符串中前面的空格
    16)、string.IsNullOrEmpty():判断一个字符串是否为空或者为null
    17)、string.Join()：将数组按照指定的字符串连接，返回一个字符串。
    */

    /// <summary>
    ///     字符工具类
    /// </summary>
    public static partial class ExtendString
    {
        /// <summary>
        ///     获取字符串中某个字符左右两侧的部分。
        /// </summary>
        /// <param name="str">指定的字符串。</param>
        /// <param name="c">要查找的字符。</param>
        /// <param name="before">输出参数，表示 c 左侧的字符串。</param>
        /// <param name="after">输出参数，表示 c 右侧的字符串。</param>
        /// <remarks>
        ///     如果字符串中不包含指定的字符，则 before 为整个字符串，after 为 null。
        /// </remarks>
        public static void PartsAround(
            this string str,
            in   char   c,
            out  string before,
            out  string after)
        {
            var index = str.IndexOf(c);

            if (index > 0)
            {
                before = str.Substring(0, index);
                after  = str.Substring(index + 1);
            }
            else
            {
                before = str;
                after  = null;
            }
        }

        /// <summary>
        ///     获取字符串中某个字符右侧的部分。
        /// </summary>
        /// <param name="str">指定的字符串。</param>
        /// <param name="c">要查找的字符。</param>
        /// <returns>包含 c 右侧内容的字符串；如果字符串中不包含指定的字符，则返回整个字符串。</returns>
        public static string PartAfter(this string str, in char c)
        {
            var index = str.IndexOf(c);

            if (index > 0) return str.Substring(index + 1);
            return str;
        }

        /// <summary>
        ///     将字符串中多个指定字符替换为另一个字符。
        /// </summary>
        /// <param name="str">指定的字符串。</param>
        /// <param name="haystacks">要替换的字符集合。</param>
        /// <param name="replacement">替换为此字符。</param>
        /// <returns>替换后的字符串。</returns>
        public static string ReplaceMultiple(this string str, in HashSet<char> haystacks, in char replacement)
        {
            var sb = new StringBuilder();

            foreach (var current in str) sb.Append(haystacks.Contains(current) ? replacement : current);

            return sb.ToString();
        }

        /// <summary>
        ///     转化为小写
        /// </summary>
        public static void XToLower(this string str)
        {
            if (string.IsNullOrEmpty(str)) return;

            unsafe
            {
                fixed (char* pstr = str)
                {
                    for (var i = 0; i < str.Length; ++i)
                    {
                        var c                             = pstr[i];
                        if (c >= 'A' && c <= 'Z') pstr[i] = (char)('a' + (c - 'A'));
                    }
                }
            }
        }

        /// <summary>
        ///     转化为大写
        /// </summary>
        public static void XToUpper(this string str)
        {
            if (string.IsNullOrEmpty(str)) return;

            unsafe
            {
                fixed (char* pstr = str)
                {
                    for (var i = 0; i < str.Length; ++i)
                    {
                        var c                             = pstr[i];
                        if (c >= 'a' && c <= 'z') pstr[i] = (char)('A' + (c - 'a'));
                    }
                }
            }
        }

        /// <summary>
        ///     转化为小写
        /// </summary>
        public static void XToLower(this string str, in int idx)
        {
            if (str == null || str.Length <= idx || idx < 0) return;
            unsafe
            {
                fixed (char* pstr = str)
                {
                    var c                               = pstr[idx];
                    if (c >= 'A' && c <= 'Z') pstr[idx] = (char)('a' + (c - 'a'));
                }
            }
        }

        /// <summary>
        ///     转化为大写
        /// </summary>
        public static void XToUpper(this string str, in int idx)
        {
            if (str == null || str.Length <= idx || idx < 0) return;
            unsafe
            {
                fixed (char* pstr = str)
                {
                    var c                               = pstr[idx];
                    if (c >= 'a' && c <= 'z') pstr[idx] = (char)('a' + (c - 'a'));
                }
            }
        }

        /// <summary>
        ///     重复
        /// </summary>
        public static string Repeat(this string str, in int repeat)
        {
            var builder = new StringBuilder(repeat * str.Length);
            for (var i = 0; i < repeat; i++) builder.Append(str);
            return builder.ToString();
        }

        /// <summary>
        ///     比较Null值
        /// </summary>
        public static string EqualsNull(this string str) { return string.IsNullOrWhiteSpace(str) ? "null" : str; }

        /// <summary>
        ///     字符串反转
        /// </summary>
        public static string Reverse(this string str)
        {
            var a                                                = str.ToCharArray();
            var l                                                = a.Length;
            for (var i = 0; i < l / 2; i++) (a[i], a[l - 1 - i]) = (a[l - 1 - i], a[i]);

            return new string(a);
        }

        /// <summary>
        ///     重复N此 复制传入数据
        /// </summary>
        public static string Clone(this string str, in uint num)
        {
            if (num <= 0) return str;
            var builder = new StringBuilder();
            for (var i = 0; i < num; i++) builder.Append(str);
            return builder.ToString();
        }

        /// <summary>
        ///     重复N此 复制传入数据
        /// </summary>
        public static string Clone(this char str, in uint num)
        {
            if (num <= 0) return string.Empty;
            var builder = new StringBuilder();
            for (var i = 0; i < num; i++) builder.Append(str);
            return builder.ToString();
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="input"> 输入字符串 </param>
        /// <param name="pattern"> 正则表达式 </param>
        /// <param name="replacement"> 替换字符串 </param>
        /// <returns> 替换后的字符串 </returns>
        public static string RegexReplace(this string input, string pattern, string replacement) { return Regex.Replace(input, pattern, replacement, RegexOptions.IgnoreCase); }
    }
}