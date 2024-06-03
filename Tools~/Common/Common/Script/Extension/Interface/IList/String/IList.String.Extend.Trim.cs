#region

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 移除所有前导空白字符和尾部空白字符。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> Trim(this IList<string> value)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].Trim();
            return value;
        }

        /// <summary>
        /// 移除数组中指定的一组字符的所有尾部匹配项。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> TrimEnd(this IList<string> value, params char[] trimChars)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].TrimEnd(trimChars);
            return value;
        }

        /// <summary>
        /// 移除数组中指定的一组字符的所有前导匹配项。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> TrimStart(this IList<string> value, params char[] trimChars)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].TrimStart(trimChars);
            return value;
        }
    }
}