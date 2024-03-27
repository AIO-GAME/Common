using System;
using System.Collections.Generic;

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Insert(1, new[] { 1, 2, 3 })
        /// </remarks>
        public static T[] Insert<T>(this T[] arrays, in int index, in T[] values)
        {
            if (values is null) return arrays;
            var valueCount = values.Length;
            if (valueCount == 0) return arrays;
            var oldLength = arrays.Length;
            if (index < 0 || index > oldLength) throw new ArgumentOutOfRangeException(nameof(index));

            var newArray = new T[oldLength + valueCount];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, index);
            Array.ConstrainedCopy(values, 0, newArray, index, valueCount);
            Array.ConstrainedCopy(arrays, index, newArray, index + valueCount, oldLength - index);
            return newArray;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Insert(1, new[] { 1, 2, 3 })
        /// </remarks>
        public static T[] Insert<T>(this T[] arrays, in int index, in Array values)
        {
            if (values is null) return arrays;
            var valueCount = values.Length;
            if (valueCount == 0) return arrays;
            var oldLength = arrays.Length;
            if (index < 0 || index > oldLength) throw new ArgumentOutOfRangeException(nameof(index));

            var newArray = new T[oldLength + values.Length];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, index);
            Array.ConstrainedCopy(arrays, index, newArray, index + 1, oldLength - index);
            Array.ConstrainedCopy(values, 0, newArray, index, values.Length);
            return newArray;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Insert(1, new[] { 1, 2, 3 })
        /// </remarks>
        public static T[] Insert<T>(this T[] arrays, in int index, in T value)
        {
            var oldLength = arrays.Length;
            if (index < 0 || index > oldLength) throw new ArgumentOutOfRangeException(nameof(index));

            var newArray = new T[oldLength + 1];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, index);
            Array.ConstrainedCopy(arrays, index, newArray, index + 1, oldLength - index);
            newArray[index] = value;
            return newArray;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Insert(1, new[] { 1, 2, 3 })
        /// </remarks>
        public static T[] Insert<T>(this T[] arrays, int index, in ICollection<T> values)
        {
            if (values is null) return arrays;
            var valueCount = values.Count;
            if (valueCount == 0) return arrays;
            var oldLength = arrays.Length;
            if (index < 0 || index > oldLength) throw new ArgumentOutOfRangeException(nameof(index));

            var newArray = new T[oldLength + valueCount];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, index);
            Array.ConstrainedCopy(arrays, index, newArray, index + valueCount, oldLength - index);
            foreach (var item in values) newArray[index++] = item;
            return newArray;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Insert(1, new[] { 1, 2, 3 })
        /// </remarks>
        public static T[] Insert<T>(this T[] arrays, int index, in IList<T> values)
        {
            if (values is null) return arrays;
            var valueCount = values.Count;
            if (valueCount == 0) return arrays;
            var oldLength = arrays.Length;
            if (index < 0 || index > oldLength) throw new ArgumentOutOfRangeException(nameof(index));

            var newArray = new T[oldLength + valueCount];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, index);
            Array.ConstrainedCopy(arrays, index, newArray, index + valueCount, oldLength - index);
            foreach (var item in values) newArray[index++] = item;
            return newArray;
        }
    }
}