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
        ///     数据量:1000以下适用
        /// </summary>
        private static IList<T> SortMerge<T>(IList<T> array)
        where T : IComparable<T>, IComparable
        {
            return array.Count < 2 ? array : MergeSort(array, 0, array.Count - 1);
        }

        /// <summary>
        ///     归并排序
        ///     数据量:1000以下适用
        /// </summary>
        private static IList<T> SortMerge<T>(IList<T> array, in Func<T, T, int> Comparer)
        {
            return array.Count < 2 ? array : MergeSort(array, 0, array.Count - 1, Comparer);
        }

        private static IList<T> MergeSort<T>(in IList<T> array, in int left, in int right)
        where T : IComparable<T>, IComparable
        {
            if (left >= right) return array;
            var median = (left + right) / 2; //计算出中间值
            MergeSort(array, left, median);
            MergeSort(array, median + 1, right);
            return Merge(array, left, median, right);
        }

        private static IList<T> Merge<T>(in IList<T> array, in int left, in int median, in int right)
        where T : IComparable<T>, IComparable
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
                {
                    // 如果 J小于左边数组长度
                    if (k < RAL)
                    {
                        // K小于右边数组长度
                        if (LA[j].CompareTo(RA[k]) <= 0)
                            array[left + i]  = LA[j++];
                        else array[left + i] = RA[k++];
                    }
                    else
                    {
                        // K大于等于右边数组长度
                        for (var m = j; m < LAL; m++)
                            array[left + i + m - j] = LA[m];
                        return array;
                    }
                }
                else if (k < RAL)
                {
                    // 如果 K小于右边数组长度
                    for (var n = k; n < RAL; n++)
                        array[left + i + n - k] = RA[n];
                    return array;
                }

            return array;
        }


        private static IList<T> MergeSort<T>(in IList<T>        array, in int left, in int right,
                                             in Func<T, T, int> Comparer)
        {
            if (left < right)
            {
                var median = (left + right) / 2; //计算出中间值
                MergeSort(array, left, median, Comparer);
                MergeSort(array, median + 1, right, Comparer);
                return Merge(array, left, median, right, Comparer);
            }

            return array;
        }


        private static IList<T> Merge<T>(in IList<T>        array, in int left, in int median, in int right,
                                         in Func<T, T, int> Comparer)
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
                {
                    // 如果 J小于左边数组长度
                    if (k < RAL)
                    {
                        // K小于右边数组长度
                        if (Comparer(LA[j], RA[k]) <= 0)
                            array[left + i]  = LA[j++];
                        else array[left + i] = RA[k++];
                    }
                    else
                    {
                        // K大于等于右边数组长度
                        for (var m = j; m < LAL; m++)
                            array[left + i + m - j] = LA[m];
                        return array;
                    }
                }
                else if (k < RAL)
                {
                    // 如果 K小于右边数组长度
                    for (var n = k; n < RAL; n++)
                        array[left + i + n - k] = RA[n];
                    return array;
                }

            return array;
        }

        #endregion
    }
}