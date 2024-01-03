using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class ExtendIList
    {
        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this IList<T> array, in int A, in int B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this IList<T> array, in short A, in short B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this IList<T> array, in ushort A, in ushort B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this IList<T> array, in byte A, in byte B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this IList<T> array, in sbyte A, in sbyte B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }
    }
}