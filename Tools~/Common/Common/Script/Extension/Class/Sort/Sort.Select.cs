using System;
using System.Collections.Generic;

namespace AIO
{
    partial class ExtendSort
    {
        #region 选择排序

        /// <summary>
        /// 选择排序 数据量:100以下适用
        /// </summary>
        private static IList<T> SortSelect<T>(IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            for (int i = 0, minIndex = i; i < array.Count - 1; i++, minIndex = i)
            {
                var minVal = array[i];
                for (var j = i + 1; j < array.Count; j++)
                    if (minVal.CompareTo(array[j]) > 0)
                    {
                        minVal = array[j];
                        minIndex = j;
                    }

                array.Swap(i, minIndex);
            }

            return array;
        }

        /// <summary>
        /// 选择排序 数据量:100以下适用
        /// </summary>
        private static IList<T> SortSelect<T>(IList<T> array, in Func<T, T, int> Comparer) where T : IComparable
        {
            if (array.Count < 2) return array;
            for (int i = 0, minIndex = i; i < array.Count - 1; i++, minIndex = i)
            {
                var minVal = array[i];
                for (var j = i + 1; j < array.Count; j++)
                    if (Comparer(minVal, array[j]) > 0)
                    {
                        minVal = array[j];
                        minIndex = j;
                    }

                array.Swap(i, minIndex);
            }

            return array;
        }

        #endregion
    }
}