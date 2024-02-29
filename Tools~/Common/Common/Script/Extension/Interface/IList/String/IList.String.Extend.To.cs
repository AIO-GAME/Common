using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 转换为小写形式的副本。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> ToLower(this IList<string> value)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].ToLower();
            return value;
        }

        /// <summary>
        /// 对象的转换为大写形式的副本。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> ToUpper(this IList<string> value)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].ToUpper();
            return value;
        }

        /// <summary>
        /// 转换为小写形式的副本。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> ToLowerInvariant(this IList<string> value)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].ToLowerInvariant();
            return value;
        }

        /// <summary>
        /// 对象的转换为大写形式的副本。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> ToUpperInvariant(this IList<string> value)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].ToUpperInvariant();
            return value;
        }

    }
}