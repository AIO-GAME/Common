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
        private static IList<T> SortBubbleTwoWay<T>(IList<T> array)
        where T : IComparable<T>, IComparable
        {
            if (array.Count < 2) return array;
            int L = 0, R = array.Count - 1;
            while (L < R)
            {
                var flag = 0;
                int i;
                for (i = L; i < R; i++)
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        array.Swap(i, i + 1);
                        flag = 1;
                    }

                if (flag == 0) break;
                for (i = --R; i > L; i--)
                    if (array[i].CompareTo(array[i - 1]) < 0)
                        array.Swap(i, i - 1);

                L++;
            }

            return array;
        }

        /// <summary>
        ///     双向冒泡排序
        /// </summary>
        private static IList<T> SortBubbleTwoWay<T>(IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            int L = 0, R = array.Count - 1;
            while (L < R)
            {
                var flag = 0;
                int i;
                for (i = L; i < R; i++)
                    if (Comparer(array[i], array[i + 1]) > 0)
                    {
                        array.Swap(i, i + 1);
                        flag = 1;
                    }

                if (flag == 0) break;
                for (i = --R; i > L; i--)
                    if (Comparer(array[i], array[i - 1]) < 0)
                        array.Swap(i, i - 1);

                L++;
            }

            return array;
        }

        #endregion
    }
}