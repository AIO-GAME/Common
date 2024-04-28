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
        ///    快速排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">索引</param>
        /// <param name="end">数量</param>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        internal static IList<T> SortQuick<T>(IList<T> array, in int start, in int end, in IComparer<T> comparer)
        {
            if (array.Count >= 2) Quick(array, start, end, comparer);
            return array;
        }

        /// <summary>
        ///    快速排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="start">索引</param>
        /// <param name="end">数量</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回排序后的数组</returns>
        internal static IList<T> SortQuick<T>(IList<T> array, in int start, in int end) where T : IComparable<T>
        {
            if (array.Count >= 2) Quick(array, start, end);
            return array;
        }

        private static void Quick<T>(in IList<T> a, in int l, in int r, in IComparer<T> comparer)
        {
            if (l >= r) return;
            var i = l;
            var j = r;
            var x = a[i];
            while (i < j)
            {
                while (i < j && comparer.Compare(x, a[j]) < 0) j--;
                if (i < j) a[i++] = a[j];
                while (i < j && comparer.Compare(x, a[i]) > 0) i++;
                if (i < j) a[j--] = a[i];
            }

            a[i] = x;
            Quick(a, l, i - 1, comparer);
            Quick(a, i + 1, r, comparer);
        }

        private static void Quick<T>(in IList<T> a, in int l, in int r)
        where T : IComparable<T>
        {
            if (l >= r) return;
            var i = l;
            var j = r;
            var x = a[i];
            while (i < j)
            {
                while (i < j && a[j].CompareTo(x) < 0) j--;
                if (i < j) a[i++] = a[j];
                while (i < j && a[i].CompareTo(x) > 0) i++;
                if (i < j) a[j--] = a[i];
            }

            a[i] = x;
            Quick(a, l, i - 1);
            Quick(a, i + 1, r);
        }

        #endregion
    }
}