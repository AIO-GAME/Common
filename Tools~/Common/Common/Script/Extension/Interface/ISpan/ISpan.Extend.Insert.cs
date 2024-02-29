/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 插入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] array, in int index, params T[] values)
        {
            if (index < 0 || index > array.Length) throw new ArgumentOutOfRangeException(nameof(index));
            var copy = new T[array.Length - index];
            array = AutoMaticExpansion(array, index, values.Length);
            Array.ConstrainedCopy(array, index, copy, 0, copy.Length);
            Array.ConstrainedCopy(values, 0, array, index, values.Length);
            Array.ConstrainedCopy(copy, 0, array, index + values.Length, copy.Length);
            return array;
        }

        /// <summary>
        /// 插入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] arrays, in int index, in Array values)
        {
            var copy = new T[arrays.Length - index];
            arrays = AutoMaticExpansion(arrays, index, values.Length);
            Array.ConstrainedCopy(arrays, index, copy, 0, copy.Length);
            Array.ConstrainedCopy(values, 0, arrays, index, values.Length);
            Array.ConstrainedCopy(copy, 0, arrays, index + values.Length, copy.Length);
            return arrays;
        }

        /// <summary>
        /// 插入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] arrays, in int index, in T values)
        {
            // 将数组转换为 List<T>
            var list = arrays.ToList();

            // 插入元素
            list.Insert(index, values);

            // 将 List<T> 转换回数组
            return list.ToArray();
        }

        /// <summary>
        /// 插入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] arrays, int index, in ICollection<T> values)
        {
            var copy = new T[arrays.Length - index];
            arrays = AutoMaticExpansion(arrays, index, values.Count);
            Array.ConstrainedCopy(arrays, index, copy, 0, copy.Length);
            foreach (var item in values) arrays[index++] = item;
            Array.ConstrainedCopy(copy, 0, arrays, index + 1, copy.Length);
            return arrays;
        }
    }
}