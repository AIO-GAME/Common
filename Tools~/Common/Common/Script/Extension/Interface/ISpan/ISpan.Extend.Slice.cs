#region

using System;

#endregion

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 对数组进行切片
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Slice(1, 3)
        /// </remarks>
        public static T[] Slice<T>(this T[] array, in int start, in int end)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (end < 0 && end >= array.Length) throw new ArgumentOutOfRangeException(nameof(end));
            if (start < 0 && start >= end) throw new ArgumentOutOfRangeException(nameof(start));
            var length = end - start;
            var result = new T[length];
            Array.Copy(array, start, result, 0, length);
            return result;
        }
    }
}