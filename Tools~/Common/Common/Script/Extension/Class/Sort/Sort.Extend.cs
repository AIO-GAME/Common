/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// Sort 扩展
    /// </summary>
    public static partial class ExtendSort
    {
        /// <summary>
        /// 自动扩容，并保存数据
        /// </summary>
        private static T[] AutomaticArray<T>(in Array array, in T value)
        {
            if (array == null) return new T[] { value };
            var a = new T[array.Length + 1];
            Array.ConstrainedCopy(array, 0, a, 0, array.Length);
            a[array.Length] = value;
            return a;
        }

        /// <summary>  
        /// 按升序插入 linklist   
        /// </summary>  
        /// <param name="linkedList"> 要排序的链表 </param>  
        /// <param name="num"> 要插入排序的数字 </param>  
        private static void InsertIntoLinkList<T>(LinkedList<T> linkedList, T num) where T : IComparable
        {
            // 链表为空时，插入到第一位  
            if (linkedList.Count <= 0) linkedList.AddFirst(num);
            else
            {
                foreach (var node in
                         from i in linkedList
                         where i.CompareTo(num) > 0
                         select linkedList.Find(i))
                {
                    linkedList.AddBefore(node, num);
                    return;
                }

                linkedList.AddLast(num);
            }
        }

        #region 希尔排序

        /// <summary>
        /// 希尔排序
        /// </summary>
        public static IList<T> SortShell<T>(this IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            var gap = (array.Count / 2);
            while (gap > 0)
            {
                gap /= 2;
                for (int i = 0; i < array.Count - gap; i++)
                {
                    if (array[i].CompareTo(array[i + gap]) > 0)
                    {
                        array.Swap(i, i + gap);
                        int j = i;
                        do
                        {
                            if (j - gap < 0) break;
                            if (array[j].CompareTo(array[j - gap]) >= 0) break;
                            else array.Swap(j, j - gap);
                            j = j - gap;
                        } while (j > 0);
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// 希尔排序
        /// </summary>
        public static IList<T> SortShell<T>(this IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            var gap = (array.Count / 2);
            while (gap > 0)
            {
                gap /= 2;
                for (int i = 0; i < array.Count - gap; i++)
                {
                    if (Comparer(array[i], array[i + gap]) > 0)
                    {
                        array.Swap(i, i + gap);
                        int j = i;
                        do
                        {
                            if (j - gap < 0) break;
                            if (Comparer(array[j], array[j - gap]) >= 0) break;
                            else array.Swap(j, j - gap);
                            j = j - gap;
                        } while (j > 0);
                    }
                }
            }

            return array;
        }

        #endregion

        #region 选择排序

        /// <summary>
        /// 选择排序 数据量:100以下适用
        /// </summary>
        public static IList<T> SortSelect<T>(this IList<T> array) where T : IComparable
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
        public static IList<T> SortSelect<T>(this IList<T> array, in Func<T, T, int> Comparer) where T : IComparable
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

        #region 堆排序

        /// <summary>
        /// 堆排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> SortHeap<T>(this IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            for (var i = (array.Count / 2) - 1; i >= 0; i--)
            {
                HeapSort_MaxHeaping(array, i, array.Count);
            }

            for (var i = array.Count - 1; i > 0; i--)
            {
                array.Swap(0, i);
                HeapSort_MaxHeaping(array, 0, i);
            }

            return array;
        }

        /// <summary>
        /// 将指定的结点调整为堆。
        /// </summary>
        private static void HeapSort_MaxHeaping<T>(in IList<T> array, in int index, in int size)
            where T : IComparable
        {
            var left = (2 * index) + 1;
            var right = 2 * (index + 1);
            var large = index;
            if (left < size && array[left].CompareTo(array[large]) > 0) large = left;
            if (right < size && array[right].CompareTo(array[large]) > 0) large = right;
            if (index != large)
            {
                array.Swap(index, large);
                HeapSort_MaxHeaping(array, large, size);
            }
        }

        /// <summary>
        /// 堆排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> SortHeap<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            for (int i = (array.Count / 2) - 1; i >= 0; i--)
            {
                HeapSort_MaxHeaping(array, i, array.Count, Comparer);
            }

            for (int i = array.Count - 1; i > 0; i--)
            {
                array.Swap(0, i);
                HeapSort_MaxHeaping(array, 0, i, Comparer);
            }

            return array;
        }

        /// <summary>
        /// 将指定的结点调整为堆。
        /// </summary>
        private static void HeapSort_MaxHeaping<T>(in IList<T> array, in int index, in int size,
            in Func<T, T, int> Comparer)
        {
            var left = (2 * index) + 1;
            var right = 2 * (index + 1);
            var large = index;
            if (left < size && Comparer(array[left], array[large]) > 0) large = left;
            if (right < size && Comparer(array[right], array[large]) > 0) large = right;
            if (index != large)
            {
                array.Swap(index, large);
                HeapSort_MaxHeaping(array, large, size, Comparer);
            }
        }

        #endregion

        #region 归并排序

        /// <summary>
        /// 归并排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> SortMerge<T>(this IList<T> array) where T : IComparable
        {
            return array.Count < 2 ? array : MergeSort(array, 0, array.Count - 1);
        }

        /// <summary>
        /// 归并排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> SortMerge<T>(this IList<T> array, in Func<T, T, int> Comparer)
        {
            return array.Count < 2 ? array : MergeSort(array, 0, array.Count - 1, Comparer);
        }

        private static IList<T> MergeSort<T>(in IList<T> array, in int left, in int right) where T : IComparable
        {
            if (left >= right) return array;
            var median = (left + right) / 2; //计算出中间值
            MergeSort(array, left, median);
            MergeSort(array, median + 1, right);
            return Merge(array, left, median, right);
        }

        private static IList<T> Merge<T>(in IList<T> array, in int left, in int median, in int right)
            where T : IComparable
        {
            var LAL = median - left + 1; //左数组长度
            var RAL = right - median; //右数组长度
            var LA = new T[LAL];
            var RA = new T[RAL];

            // 给左右两边数组 初始化内容
            for (int i = 0; i < LAL; i++) LA[i] = array[left + i];
            for (int i = 0; i < RAL; i++) RA[i] = array[median + 1 + i];

            for (int i = 0, j = 0, k = 0; i < right - left + 1; i++)
            {
                // 遍历 从左边0开始 到右边截止长度-左边起始开始位置+1
                if (j < LAL)
                {
                    // 如果 J小于左边数组长度
                    if (k < RAL)
                    {
                        // K小于右边数组长度
                        if (LA[j].CompareTo(RA[k]) <= 0)
                            array[left + i] = LA[j++];
                        else array[left + i] = RA[k++];
                    }
                    else
                    {
                        // K大于等于右边数组长度
                        for (int m = j; m < LAL; m++)
                            array[left + i + m - j] = LA[m];
                        return array;
                    }
                }
                else if (k < RAL)
                {
                    // 如果 K小于右边数组长度
                    for (int n = k; n < RAL; n++)
                        array[left + i + n - k] = RA[n];
                    return array;
                }
            }

            return array;
        }


        private static IList<T> MergeSort<T>(in IList<T> array, in int left, in int right,
            in Func<T, T, int> Comparer)
        {
            if (left < right)
            {
                int median = (left + right) / 2; //计算出中间值
                MergeSort(array, left, median, Comparer);
                MergeSort(array, median + 1, right, Comparer);
                return Merge(array, left, median, right, Comparer);
            }

            return array;
        }


        private static IList<T> Merge<T>(in IList<T> array, in int left, in int median, in int right,
            in Func<T, T, int> Comparer)
        {
            var LAL = median - left + 1; //左数组长度
            var RAL = right - median; //右数组长度
            var LA = new T[LAL];
            var RA = new T[RAL];

            // 给左右两边数组 初始化内容
            for (int i = 0; i < LAL; i++) LA[i] = array[left + i];
            for (int i = 0; i < RAL; i++) RA[i] = array[median + 1 + i];

            for (int i = 0, j = 0, k = 0; i < right - left + 1; i++)
            {
                // 遍历 从左边0开始 到右边截止长度-左边起始开始位置+1
                if (j < LAL)
                {
                    // 如果 J小于左边数组长度
                    if (k < RAL)
                    {
                        // K小于右边数组长度
                        if (Comparer(LA[j], RA[k]) <= 0)
                            array[left + i] = LA[j++];
                        else array[left + i] = RA[k++];
                    }
                    else
                    {
                        // K大于等于右边数组长度
                        for (int m = j; m < LAL; m++)
                            array[left + i + m - j] = LA[m];
                        return array;
                    }
                }
                else if (k < RAL)
                {
                    // 如果 K小于右边数组长度
                    for (int n = k; n < RAL; n++)
                        array[left + i + n - k] = RA[n];
                    return array;
                }
            }

            return array;
        }

        #endregion

        #region 插入排序

        /// <summary>
        /// 插入排序
        /// </summary>
        public static IList<T> SortInsert<T>(this IList<T> array) where T : IComparable
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
        /// 插入排序
        /// </summary>
        public static IList<T> SortInsert<T>(this IList<T> array, in Func<T, T, int> Comparer)
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

        #region 快速排序

        /// <summary>
        /// 快速排序
        /// </summary>
        public static IList<T> SortQuick<T>(this IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            Quick(array, 0, array.Count - 1);
            return array;
        }

        private static void Quick<T>(in IList<T> a, in int l, in int r) where T : IComparable
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
        /// 快速排序
        /// </summary>
        public static IList<T> SortQuick<T>(this IList<T> array, in Func<T, T, int> Comparer)
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

        #region 单向冒泡排序

        /// <summary>
        /// 单向冒泡排序
        /// </summary>
        public static IList<T> SortBubble<T>(this IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            var len = array.Count - 1;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0) array.Swap(j, j + 1);
                }
            }

            return array;
        }

        /// <summary>
        /// 单向冒泡排序
        /// </summary>
        public static IList<T> SortBubble<T>(this IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            var len = array.Count - 1;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (Comparer(array[j], array[j + 1]) > 0) array.Swap(j, j + 1);
                }
            }

            return array;
        }

        #endregion

        #region 双向冒泡排序

        /// <summary>
        /// 双向冒泡排序
        /// </summary>
        public static IList<T> SortBubbleTwoWay<T>(this IList<T> array) where T : IComparable
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
                    {
                        array.Swap(i, i - 1);
                    }

                L++;
            }

            return array;
        }

        /// <summary>
        /// 双向冒泡排序
        /// </summary>
        public static IList<T> SortBubbleTwoWay<T>(this IList<T> array, in Func<T, T, int> Comparer)
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
                    {
                        array.Swap(i, i - 1);
                    }

                L++;
            }

            return array;
        }

        #endregion
    }
}