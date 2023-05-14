using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class StringExtend
    {
        /// <summary>
        /// 检查字符串是否包含指定的子字符串，忽略大小写
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="needle">要在 haystack 中搜索的子字符串</param>
        /// <returns>如果 haystack 包含 needle，则为 true；否则为 false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsOrdinalIgnoreCase(this string str, in string needle)
        {
            return str.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// 检查字符串是否包含指定的子字符串，忽略大小写
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="needle">要在 haystack 中搜索的子字符串</param>
        /// <param name="comparison">搜索模式</param>
        /// <returns>如果 haystack 包含 needle，则为 true；否则为 false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string str, in string needle, StringComparison comparison)
        {
            return str.IndexOf(needle, comparison) >= 0;
        }

        /// <summary>
        /// 判断路径是是否包含指定字符
        /// </summary>
        /// <returns>Ture:符合 False:不符合</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string str, ICollection<int> list)
        {
            if (list == null || list.Count == 0) return false;
            return str.All(item => list.Contains(item));
        }

        /// <summary>
        /// 是否包含指定字符
        /// </summary>
        /// <returns>Ture:符合 False:不符合</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string str, int start, int end)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (start > end) (start, end) = (end, start);
            return str.All(c => c.IsInRange(start, end));
        }
    }
}