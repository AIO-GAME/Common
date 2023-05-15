using System.Runtime.CompilerServices;

namespace AIO
{
    using System;
    using System.Collections.Generic;

    public partial class StringExtend
    {
        /// <summary>
        /// 在字符串中查找所有出现指定子字符串的位置，忽略大小写
        /// </summary>
        /// <param name="haystack">要搜索的字符串</param>
        /// <param name="needle">要在 haystack 中搜索的子字符串</param>
        /// <returns>包含 needle 的所有索引的 IEnumerable(int) 对象</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> IndexesOf(this string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle)) yield break;

            for (var index = 0; ; index += needle.Length)
            {
                index = haystack.IndexOf(needle, index, StringComparison.OrdinalIgnoreCase);

                if (index == -1) break;

                yield return index;
            }
        }
    }
}