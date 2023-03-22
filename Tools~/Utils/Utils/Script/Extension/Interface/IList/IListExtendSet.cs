using System;
using System.Collections.Generic;

namespace AIO
{
    public static partial class IListExtend
    {
        /// <summary>
        /// 设置指定下标元素
        /// </summary>
        public static void Set<T>(this IList<T> array, in int idx, in T value)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (idx < 0 || idx >= array.Count) throw new IndexOutOfRangeException();
            array[idx] = value;
        }

        /// <summary>
        /// 设置最后一个元素
        /// </summary>
        public static void SetLast<T>(this IList<T> array, in T value)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count == 0) array.Add(value);
            else array[array.Count - 1] = value;
        }

        /// <summary>
        /// 设置第一个元素
        /// </summary>
        public static void SetFirst<T>(this IList<T> array, in T value)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count == 0) array.Add(value);
            else array[0] = value;
        }
    }
}