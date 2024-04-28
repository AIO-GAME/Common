#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        /// <summary>
        /// 单向冒泡排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortBubble<T>(IList<T> array, in int start, in int end)
        where T : IComparable<T>
        {
            if (array.Count < 2) return array;
            for (var i = start; i < end; i++)
            for (var a = start; a < end - i; a++)
            {
                var b = a + 1;
                if (array[a].CompareTo(array[b]) > 0)
                    (array[a], array[b]) = (array[b], array[a]);
            }

            return array;
        }

        /// <summary>
        ///     单向冒泡排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortBubble<T>(IList<T> array, in int start, in int end, in IComparer<T> comparer)
        {
            if (array.Count < 2) return array;
            for (var i = start; i < end; i++)
            for (var a = start; a < end - i; a++)
            {
                var b = a + 1;
                if (comparer.Compare(array[a], array[b]) > 0)
                    (array[a], array[b]) = (array[b], array[a]);
            }

            return array;
        }
    }
}