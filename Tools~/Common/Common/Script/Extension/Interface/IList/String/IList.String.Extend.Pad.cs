using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class ExtendIList
    {
        /// <summary>
        /// 字符串通过在此实例中的字符左侧填充空格来达到指定的总长度，从而实现右对齐。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> PadLeft(this IList<string> value, in int pad)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].PadLeft(pad);
            return value;
        }

        /// <summary>
        /// 字符右侧填充空格来达到指定的总长度，从而使这些字符左对齐
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> PadRight(this IList<string> value, in int pad)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].PadRight(pad);
            return value;
        }

        /// <summary>
        /// 字符右侧填充空格来达到指定的总长度，从而使这些字符左对齐
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> Normalize(this IList<string> value)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].Normalize();
            return value;
        }
    }
}