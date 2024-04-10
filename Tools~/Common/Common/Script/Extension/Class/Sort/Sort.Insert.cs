#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        #region 插入排序

        /// <summary>
        ///     插入排序
        /// </summary>
        private static IList<T> SortInsert<T>(IList<T> array)
        where T : IComparable
        {
            if (array.Count < 2) return array;
            for (int i = 1, insertions = i - 1; i < array.Count; i++, insertions = i - 1)
            {
                var undervalue = array[i];
                while (insertions >= 0 && undervalue.CompareTo(array[insertions]) < 0)
                    array[insertions + 1] = array[insertions--];
                array[insertions + 1] = undervalue;
            }

            return array;
        }

        /// <summary>
        ///     插入排序
        /// </summary>
        private static IList<T> SortInsert<T>(IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            for (int i = 1, insertions = i - 1; i < array.Count; i++, insertions = i - 1)
            {
                var undervalue = array[i];
                while (insertions >= 0 && Comparer(undervalue, array[insertions]) < 0)
                    array[insertions + 1] = array[insertions--];
                array[insertions + 1] = undervalue;
            }

            return array;
        }

        #endregion
    }
}