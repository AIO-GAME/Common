#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        #region 堆排序

        /// <summary>
        ///     堆排序
        ///     数据量:1000以下适用
        /// </summary>
        private static IList<T> SortHeap<T>(IList<T> array)
        where T : IComparable
        {
            if (array.Count < 2) return array;
            for (var i = array.Count / 2 - 1; i >= 0; i--) HeapSort_MaxHeaping(array, i, array.Count);

            for (var i = array.Count - 1; i > 0; i--)
            {
                array.Swap(0, i);
                HeapSort_MaxHeaping(array, 0, i);
            }

            return array;
        }

        /// <summary>
        ///     将指定的结点调整为堆。
        /// </summary>
        private static void HeapSort_MaxHeaping<T>(in IList<T> array, in int index, in int size)
        where T : IComparable
        {
            var left = 2 * index + 1;
            var right = 2 * (index + 1);
            var large = index;
            if (left < size && array[left].CompareTo(array[large]) > 0) large   = left;
            if (right < size && array[right].CompareTo(array[large]) > 0) large = right;
            if (index != large)
            {
                array.Swap(index, large);
                HeapSort_MaxHeaping(array, large, size);
            }
        }

        /// <summary>
        ///     堆排序
        ///     数据量:1000以下适用
        /// </summary>
        private static IList<T> SortHeap<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            for (var i = array.Count / 2 - 1; i >= 0; i--) HeapSort_MaxHeaping(array, i, array.Count, Comparer);

            for (var i = array.Count - 1; i > 0; i--)
            {
                array.Swap(0, i);
                HeapSort_MaxHeaping(array, 0, i, Comparer);
            }

            return array;
        }

        /// <summary>
        ///     将指定的结点调整为堆。
        /// </summary>
        private static void HeapSort_MaxHeaping<T>(in IList<T>        array, in int index, in int size,
                                                   in Func<T, T, int> Comparer)
        {
            var left = 2 * index + 1;
            var right = 2 * (index + 1);
            var large = index;
            if (left < size && Comparer(array[left], array[large]) > 0) large   = left;
            if (right < size && Comparer(array[right], array[large]) > 0) large = right;
            if (index != large)
            {
                array.Swap(index, large);
                HeapSort_MaxHeaping(array, large, size, Comparer);
            }
        }

        #endregion
    }
}