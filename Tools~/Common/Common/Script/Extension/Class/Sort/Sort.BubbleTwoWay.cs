#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        #region 双向冒泡排序

        /// <summary>
        ///     双向冒泡排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortBubbleTwoWay<T>(IList<T> array, int start, int end)
        where T : IComparable<T>
        {
            if (array.Count < 2) return array;
            while (start < end)
            {
                var flag = 0;
                int a;
                for (a = start; a < end; a++)
                {
                    var b = a + 1;
                    if (array[a].CompareTo(array[b]) <= 0) continue;
                    (array[a], array[b]) = (array[b], array[a]);
                    flag                 = 1;
                }

                if (flag == 0) break;
                for (a = --end; a > start; a--)
                {
                    var b = a - 1;
                    if (array[a].CompareTo(array[b]) < 0)
                        (array[a], array[b]) = (array[b], array[a]);
                }

                start++;
            }

            return array;
        }

        /// <summary>
        ///     双向冒泡排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortBubbleTwoWay<T>(IList<T> array, int start, int end, in IComparer<T> comparer)
        {
            if (array.Count < 2) return array;
            while (start < end)
            {
                var flag = 0;
                int a;
                for (a = start; a < end; a++)
                {
                    var b = a + 1;
                    if (comparer.Compare(array[a], array[b]) <= 0) continue;
                    (array[a], array[b]) = (array[b], array[a]);
                    flag                 = 1;
                }

                if (flag == 0) break;
                for (a = --end; a > start; a--)
                {
                    var b = a - 1;
                    if (comparer.Compare(array[a], array[b]) < 0)
                        (array[a], array[b]) = (array[b], array[a]);
                }

                start++;
            }

            return array;
        }

        #endregion
    }
}