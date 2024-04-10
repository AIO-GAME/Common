#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        #region 快速排序

        /// <summary>
        ///     快速排序
        /// </summary>
        private static IList<T> SortQuick<T>(IList<T> array)
        where T : IComparable
        {
            if (array.Count < 2) return array;
            Quick(array, 0, array.Count - 1);
            return array;
        }

        private static void Quick<T>(in IList<T> a, in int l, in int r)
        where T : IComparable
        {
            if (l < r)
            {
                var i = l;
                var j = r;
                var x = a[i];
                while (i < j)
                {
                    while (i < j && a[j].CompareTo(x) > 0) j--;
                    if (i < j) a[i++] = a[j];
                    while (i < j && a[i].CompareTo(x) < 0) i++;
                    if (i < j) a[j--] = a[i];
                }

                a[i] = x;
                Quick(a, l, i - 1);
                Quick(a, i + 1, r);
            }
        }

        /// <summary>
        ///     快速排序
        /// </summary>
        private static IList<T> SortQuick<T>(IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            Quick(array, 0, array.Count - 1, Comparer);
            return array;
        }

        private static void Quick<T>(in IList<T> a, in int l, in int r, in Func<T, T, int> Comparer)
        {
            if (l < r)
            {
                var i = l;
                var j = r;
                var x = a[i];
                while (i < j)
                {
                    while (i < j && Comparer(x, a[j]) > 0) j--;
                    if (i < j) a[i++] = a[j];
                    while (i < j && Comparer(x, a[i]) < 0) i++;
                    if (i < j) a[j--] = a[i];
                }

                a[i] = x;
                Quick(a, l, i - 1, Comparer);
                Quick(a, i + 1, r, Comparer);
            }
        }

        #endregion
    }
}