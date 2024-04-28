#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        /// <summary>
        ///     选择排序 数据量:100以下适用
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">索引</param>
        /// <param name="end">数量</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        private static IList<T> SortSelect<T>(IList<T> array, int start, int end, in IComparer<T> comparer)
        {
            if (end - start < 2) return array;
            for (var a = start; a < end; a++)
            {
                var minIndex = a;
                for (var j = a + 1; j <= end; j++)
                {
                    if (comparer.Compare(array[j], array[minIndex]) < 0)
                        minIndex = j;
                }

                if (minIndex != a) (array[a], array[minIndex]) = (array[minIndex], array[a]);
            }

            return array;
        }

        /// <summary>
        ///     选择排序 数据量:100以下适用
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">索引</param>
        /// <param name="end">数量</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortSelect<T>(IList<T> array, int start, int end) where T : IComparable<T>
        {
            if (end - start < 2) return array;
            for (var a = start; a < end; a++)
            {
                var minIndex = a;
                for (var j = a + 1; j <= end; j++)
                {
                    if (array[j].CompareTo(array[minIndex]) < 0) minIndex = j;
                }

                if (minIndex != a) (array[a], array[minIndex]) = (array[minIndex], array[a]);
            }

            return array;
        }
    }
}