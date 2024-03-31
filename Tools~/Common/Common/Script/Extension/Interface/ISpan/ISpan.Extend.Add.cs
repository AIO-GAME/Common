using System;
using System.Collections.Generic;

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static T[] Add<T, TA>(this T[] arrays, in TA item) where TA : T
        {
            var oldLength = arrays.Length;
            var newArray = new T[oldLength + 1];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, oldLength);
            newArray[oldLength] = item;
            return newArray;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static T[] Add<T, TA>(this T[] array, params TA[] items) where TA : T
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (items == null || items.Length == 0) return array;

            var result = new T[array.Length + items.Length];
            Array.ConstrainedCopy(array, 0, result, 0, array.Length);
            Array.ConstrainedCopy(items, 0, result, array.Length, items.Length);
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static T[] Add<T, TA>(this T[] array, in ICollection<TA> items) where TA : T
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (items == null || items.Count == 0) return array;

            var result = new T[array.Length + items.Count];
            Array.ConstrainedCopy(array, 0, result, 0, array.Length);
            var index = array.Length;
            foreach (var item in items) array[index++] = item;
            return result;
        }
    }
}