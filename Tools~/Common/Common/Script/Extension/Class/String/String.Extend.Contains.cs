#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    public partial class ExtendString
    {
        /// <summary>
        ///     判断集合中 是否有重复
        /// </summary>
        /// <param name="chars">集合</param>
        /// <param name="targetChars">匹配集合</param>
        /// <returns>Ture:存在 False:不存在</returns>
        public static bool Contains(this string chars, in string targetChars)
        {
            if (chars == null || targetChars == null) return false;
            return chars.Intersect(targetChars).Any();
        }

        /// <summary>
        ///     判断集合中 是否有重复
        /// </summary>
        /// <param name="chars">集合</param>
        /// <param name="targetChars">匹配集合</param>
        /// <returns>Ture:存在 False:不存在</returns>
        public static bool Contains(this IEnumerable<string> chars, IEnumerable<string> targetChars)
        {
            if (chars == null || targetChars == null) return false;
            return chars.Intersect(targetChars).Any();
        }

        /// <summary>
        ///     判断集合中 是否有重复
        /// </summary>
        /// <param name="chars">集合</param>
        /// <param name="targetChars">匹配集合</param>
        /// <returns>Ture:存在 False:不存在</returns>
        public static bool Contains(this IEnumerable<string> chars, string targetChars)
        {
            if (chars == null || targetChars == null) return false;
            foreach (var charx in chars)
                if (charx == targetChars)
                    return true;

            return false;
        }

        /// <summary>
        ///     检查字符串是否包含指定的子字符串，忽略大小写
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="needle">要在 haystack 中搜索的子字符串</param>
        /// <returns>如果 haystack 包含 needle，则为 true；否则为 false</returns>
        public static bool ContainsOrdinalIgnoreCase(this string str, in string needle)
        {
            return str.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        ///     检查字符串是否包含指定的子字符串，忽略大小写
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="needle">要在 haystack 中搜索的子字符串</param>
        /// <param name="comparison">搜索模式</param>
        /// <returns>如果 haystack 包含 needle，则为 true；否则为 false</returns>
        public static bool Contains(this string str, in string needle, StringComparison comparison)
        {
            return str.IndexOf(needle, comparison) >= 0;
        }

        /// <summary>
        ///     判断路径是是否包含指定字符
        /// </summary>
        /// <returns>Ture:符合 False:不符合</returns>
        public static bool Contains(this string str, ICollection<int> list)
        {
            if (list == null || list.Count == 0) return false;
            return str.All(item => list.Contains(item));
        }

        /// <summary>
        ///     是否包含指定字符
        /// </summary>
        /// <returns>Ture:符合 False:不符合</returns>
        public static bool Contains(this string str, int start, int end)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (start > end) (start, end) = (end, start);
            return str.All(c => c.IsInRange(start, end));
        }
    }
}