#region

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 设置指定下标元素
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<T>(this IList<T> array, in int index, in T value)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Count) throw new IndexOutOfRangeException();
            array[index] = value;
        }

        /// <summary>
        /// 设置最后一个元素
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLast<T>(this IList<T> array, in T value)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count == 0) array.Add(value);
            else array[array.Count - 1] = value;
        }

        /// <summary>
        /// 设置第一个元素
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFirst<T>(this IList<T> array, in T value)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count == 0) array.Add(value);
            else array[0] = value;
        }
    }
}