/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIO
{
    public static partial class ExtendString
    {
        /// <summary>
        /// 分组
        /// </summary>
        public static string[] XSplit(this string src, in char ch)
        {
            if (string.IsNullOrEmpty(src)) return Array.Empty<string>();
            var result = new List<string>();

            var startIndex = 0;
            while (true)
            {
                var index = src.IndexOf(ch, startIndex);
                if (index < 0)
                {
                    result.Add(src.Substring(startIndex));
                    break;
                }

                result.Add(src.Substring(startIndex, index - startIndex));
                startIndex = index + 1;
            }

            return result.ToArray();
        }

        /// <summary>
        /// 分组一次
        /// </summary>
        public static (string, string) SplitOnce(this string src, in char ch)
        {
            if (string.IsNullOrWhiteSpace(src)) return (string.Empty, string.Empty);

            var index = src.IndexOf(ch);
            if (index == -1) return (src, string.Empty);

            var array1 = src.Substring(0, index);
            var array2 = src.Substring(index + 1);

            return (array1, array2);
        }

        /// <summary>
        /// 将字符串以行拆分为数组
        /// </summary>
        public static string[] SplitLine(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return Array.Empty<string>();

            return XSplit(str, '\n')
                .Select(line => line.Trim('\r', '\n'))
                .Where(trimmedLine => !string.IsNullOrEmpty(trimmedLine)).ToArray();
        }

        /// <summary>
        /// 单词分割
        /// </summary>
        public static string SplitWords(this string s, in char separator)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;

            var result = new StringBuilder(s.Length + s.Length / 10);
            result.Append(s[0]);

            for (var i = 1; i < s.Length; i++)
            {
                var c = s[i];
                if (char.IsLetterOrDigit(c) && !char.IsLetterOrDigit(s[i - 1]))
                    result.Append(separator);
                result.Append(c);
            }

            return result.ToString();
        }
    }
}