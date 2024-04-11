#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        #region 单向冒泡排序

        /// <summary>
        ///     单向冒泡排序
        /// </summary>
        private static IList<T> SortBubble<T>(IList<T> array)
        where T : IComparable<T>, IComparable
        {
            if (array.Count < 2) return array;
            var len = array.Count - 1;
            for (var i = 0; i < len; i++)
            for (var A = 0; A < len - i; A++)
            {
                var B = A + 1;
                if (array[A].CompareTo(array[B]) > 0)
                    (array[A], array[B]) = (array[B], array[A]);
            }

            return array;
        }

        /// <summary>
        ///     单向冒泡排序
        /// </summary>
        private static IList<T> SortBubble<T>(IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            var len = array.Count - 1;
            for (var i = 0; i < len; i++)
            for (var j = 0; j < len - i; j++)
                if (Comparer(array[j], array[j + 1]) > 0)
                    array.Swap(j, j + 1);

            return array;
        }

        #endregion
    }
}