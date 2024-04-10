#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        #region 希尔排序

        /// <summary>
        ///     希尔排序
        /// </summary>
        private static IList<T> SortShell<T>(IList<T> array)
        where T : IComparable
        {
            if (array.Count < 2) return array;
            var gap = array.Count / 2;
            while (gap > 0)
            {
                gap /= 2;
                for (var i = 0; i < array.Count - gap; i++)
                    if (array[i].CompareTo(array[i + gap]) > 0)
                    {
                        array.Swap(i, i + gap);
                        var j = i;
                        do
                        {
                            if (j - gap < 0) break;
                            if (array[j].CompareTo(array[j - gap]) >= 0) break;
                            array.Swap(j, j - gap);
                            j = j - gap;
                        } while (j > 0);
                    }
            }

            return array;
        }

        /// <summary>
        ///     希尔排序
        /// </summary>
        private static IList<T> SortShell<T>(IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            var gap = array.Count / 2;
            while (gap > 0)
            {
                gap /= 2;
                for (var i = 0; i < array.Count - gap; i++)
                    if (Comparer(array[i], array[i + gap]) > 0)
                    {
                        array.Swap(i, i + gap);
                        var j = i;
                        do
                        {
                            if (j - gap < 0) break;
                            if (Comparer(array[j], array[j - gap]) >= 0) break;
                            array.Swap(j, j - gap);
                            j = j - gap;
                        } while (j > 0);
                    }
            }

            return array;
        }

        #endregion
    }
}