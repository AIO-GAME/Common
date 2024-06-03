#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    partial class ExtendIEnumerable
    {
        /// <summary>
        /// 转化为HashSet
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }

        /// <summary>
        /// 将 IEnumerable 中的元素转换为以指定分隔符分隔的字符串
        /// </summary>
        /// <param name="enumerable">IEnumerable 对象，包含要转换的元素</param>
        /// <param name="separator">用于间隔每个元素的字符串</param>
        /// <returns>以分隔符分隔的字符串</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToSeparatedString(this IEnumerable enumerable, in string separator)
        {
            return string.Join(separator, enumerable.Cast<object>().Select(o => o?.ToString() ?? "(null)").ToArray());
        }

        /// <summary>
        /// 将 IEnumerable 中的元素转换为逗号分隔的字符串
        /// </summary>
        /// <param name="enumerable">IEnumerable 对象，包含要转换的元素</param>
        /// <returns>逗号分隔的字符串</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToCommaSeparatedString(this IEnumerable enumerable)
        {
            return ToSeparatedString(enumerable, ", ");
        }

        /// <summary>
        /// 将 IEnumerable 中的元素转换为以行结束符分隔的字符串
        /// </summary>
        /// <param name="enumerable">IEnumerable 对象，包含要转换的元素</param>
        /// <returns>以行结束符分隔的字符串</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToLineSeparatedString(this IEnumerable enumerable)
        {
            return ToSeparatedString(enumerable, Environment.NewLine);
        }
    }
}