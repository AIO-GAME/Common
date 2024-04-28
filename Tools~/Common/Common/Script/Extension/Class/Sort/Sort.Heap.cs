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
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        public static IList<T> SortHeap<T>(IList<T> array, int start, int end, in IComparer<T> comparer)
        {
            if (end - start < 2) return array;
            for (var i = end / 2; i >= start; i--) HeapSort_MaxHeaping(array, i, end, comparer);
            for (var i = end; i > start; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                HeapSort_MaxHeaping(array, 0, i - 1, comparer);
            }

            return array;
        }

        /// <summary>
        ///     堆排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortHeap<T>(IList<T> array, int start, int end)
        where T : IComparable<T>
        {
            if (end - start < 2) return array;
            for (var i = end / 2 - 1; i >= start; i--) HeapSort_MaxHeaping(array, i, end);

            for (var i = end - 1; i > start; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                HeapSort_MaxHeaping(array, 0, i - 1);
            }

            return array;
        }

        private static void HeapSort_MaxHeaping<T>(IList<T> array, int index, in int size)
        where T : IComparable<T>
        {
            int left, right, large;
            while (true)
            {
                left  = (index << 1) + 1;
                right = (index + 1) << 1;
                large = index;

                if (left < size && array[left].CompareTo(array[large]) > 0) large   = left;
                if (right < size && array[right].CompareTo(array[large]) > 0) large = right;
                if (index == large) return;

                (array[index], array[large]) = (array[large], array[index]);
                index                        = large;
            }
        }

        private static void HeapSort_MaxHeaping<T>(IList<T> array, int index, int size, in IComparer<T> comparer)
        {
            int left, right, large;
            while (true)
            {
                left  = (index << 1) + 1;
                right = (index + 1) << 1;
                large = index;

                if (left < size && comparer.Compare(array[left], array[large]) > 0) large   = left;
                if (right < size && comparer.Compare(array[right], array[large]) > 0) large = right;
                if (index == large) return;

                (array[index], array[large]) = (array[large], array[index]);
                index                        = large;
            }
        }

        #endregion
    }
}