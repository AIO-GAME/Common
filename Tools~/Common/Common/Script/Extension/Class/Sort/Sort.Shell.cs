#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        /// <summary>
        ///     希尔排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">索引</param>
        /// <param name="end">数量</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortShell<T>(IList<T> array, int start, int end)
        where T : IComparable<T>
        {
            if (array.Count < 2) return array;
            var gap = array.Count / 2;
            while (gap > 0)
            {
                gap /= 2;
                for (var i = start; i < end - gap; i++)
                {
                    var b = i + gap;
                    if (array[i].CompareTo(array[b]) <= 0) continue;
                    (array[i], array[b]) = (array[b], array[i]);
                    var j = i;
                    do
                    {
                        b = j - gap;
                        if (b < 0) break;
                        if (array[j].CompareTo(array[b]) >= 0) break;
                        (array[j], array[b]) = (array[b], array[j]);
                        j                    = b;
                    } while (j > 0);
                }
            }

            return array;
        }

        /// <summary>
        ///     希尔排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">索引</param>
        /// <param name="end">数量</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortShell<T>(IList<T> array, int start, int end, in IComparer<T> comparer)
        {
            if (array.Count < 2) return array;
            var gap = array.Count / 2;
            while (gap > 0)
            {
                gap /= 2;
                for (var i = start; i < end - gap; i++)
                {
                    var b = i + gap;
                    if (comparer.Compare(array[i], array[b]) <= 0) continue;
                    (array[i], array[b]) = (array[b], array[i]);
                    var j = i;
                    do
                    {
                        b = j - gap;
                        if (b < 0) break;
                        if (comparer.Compare(array[j], array[b]) >= 0) break;
                        (array[j], array[b]) = (array[b], array[j]);
                        j                    = b;
                    } while (j > 0);
                }
            }

            return array;
        }
    }
}