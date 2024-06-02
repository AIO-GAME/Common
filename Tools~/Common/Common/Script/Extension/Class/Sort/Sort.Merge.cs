#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        #region 归并排序

        /// <summary>
        ///     归并排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        internal static IList<T> SortMerge<T>(in IList<T> array, int start, int end, in IComparer<T> comparer)
        {
            if (start >= end) return array;
            var median = (start + end) / 2; //计算出中间值
            SortMerge(array, start, median, comparer);
            SortMerge(array, median + 1, end, comparer);
            return Merge(array, start, median, end, comparer);
        }

        /// <summary>
        ///     归并排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">右边界</param>
        /// <param name="end">结束边界</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        private static IList<T> SortMerge<T>(in IList<T> array, in int start, in int end)
        where T : IComparable<T>
        {
            if (start >= end) return array;
            var median = (start + end) / 2; //计算出中间值
            SortMerge(array, start, median);
            SortMerge(array, median + 1, end);
            return Merge(array, start, median, end);
        }

        private static IList<T> Merge<T>(in IList<T> array, in int left, in int median, in int right)
        where T : IComparable<T>
        {
            var lal = median - left + 1; //左数组长度
            var ral = right - median;    //右数组长度
            var la = new T[lal];
            var ra = new T[ral];

            // 给左右两边数组 初始化内容
            for (var i = 0; i < lal; i++) la[i] = array[left + i];
            for (var i = 0; i < ral; i++) ra[i] = array[median + 1 + i];

            for (int i = 0, j = 0, k = 0; i < right - left + 1; i++)
                // 遍历 从左边0开始 到右边截止长度-左边起始开始位置+1
                if (j < lal)
                {
                    // 如果 J小于左边数组长度
                    if (k < ral)
                    {
                        // K小于右边数组长度
                        if (la[j].CompareTo(ra[k]) <= 0)
                            array[left + i]  = la[j++];
                        else array[left + i] = ra[k++];
                    }
                    else
                    {
                        // K大于等于右边数组长度
                        for (var m = j; m < lal; m++)
                            array[left + i + m - j] = la[m];
                        return array;
                    }
                }
                else if (k < ral)
                {
                    // 如果 K小于右边数组长度
                    for (var n = k; n < ral; n++)
                        array[left + i + n - k] = ra[n];
                    return array;
                }

            return array;
        }

        private static IList<T> Merge<T>(in IList<T> array, in int left, in int median, in int right, in IComparer<T> comparer)
        {
            var LAL = median - left + 1; //左数组长度
            var RAL = right - median;    //右数组长度
            var LA = new T[LAL];
            var RA = new T[RAL];

            // 给左右两边数组 初始化内容
            for (var i = 0; i < LAL; i++) LA[i] = array[left + i];
            for (var i = 0; i < RAL; i++) RA[i] = array[median + 1 + i];

            for (int i = 0, j = 0, k = 0; i < right - left + 1; i++)
                // 遍历 从左边0开始 到右边截止长度-左边起始开始位置+1
                if (j < LAL)
                { // 如果 J小于左边数组长度
                    if (k < RAL)
                    { // K小于右边数组长度
                        if (comparer.Compare(LA[j], RA[k]) <= 0)
                            array[left + i]  = LA[j++];
                        else array[left + i] = RA[k++];
                    }
                    else
                    { // K大于等于右边数组长度
                        for (var m = j; m < LAL; m++)
                            array[left + i + m - j] = LA[m];
                        return array;
                    }
                }
                else if (k < RAL)
                { // 如果 K小于右边数组长度
                    for (var n = k; n < RAL; n++)
                        array[left + i + n - k] = RA[n];
                    return array;
                }

            return array;
        }

        #endregion
    }
}