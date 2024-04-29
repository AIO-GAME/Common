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
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortInsert<T>(IList<T> array, int start, int end)
        where T : IComparable<T>
        {
            if (end - start < 2) return array;
            for (int i = start + 1, j = start; i <= end; j = i++)
            {
                var key = array[i];
                while (j >= 0 && array[j].CompareTo(key) > 0)
                    array[j + 1] = array[j--];

                array[j + 1] = key;
            }

            return array;
        }

        /// <summary>
        ///     插入排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortInsert<T>(IList<T> array, int start, int end, in IComparer<T> comparer)
        {
            if (end - start < 2) return array;
            for (int i = start + 1, j = start; i <= end; j = i++)
            {
                var key = array[i];
                while (j >= 0 && comparer.Compare(array[j], key) > 0)
                    array[j + 1] = array[j--];

                array[j + 1] = key;
            }

            return array;
        }

        #endregion
    }
}