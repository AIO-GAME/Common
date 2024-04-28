#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        /// <summary>
        ///     数组排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="sort">排序方式</param>
        /// <returns>排序后的数组 可无需赋值</returns>
        /// <remarks>支持快速排序、冒泡排序、双向冒泡排序、选择排序、插入排序、希尔排序、堆排序、归并排序、桶排序、基数排序</remarks>
        /// <exception cref="ArgumentOutOfRangeException">传入的排序方式不在枚举内</exception>
        /// <exception cref="ArgumentNullException">传入的数组为空</exception>
        public static IList<T> Sort<T>(this IList<T> array, in ESort sort = ESort.Quick)
        where T : IComparable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count < 2) return array;
            return sort switch
            {
                ESort.Bubble       => SortBubble(array, 0, array.Count - 1),
                ESort.BubbleTwoWay => SortBubbleTwoWay(array, 0, array.Count - 1),
                ESort.Select       => SortSelect(array, 0, array.Count - 1),
                ESort.Insert       => SortInsert(array, 0, array.Count - 1),
                ESort.Shell        => SortShell(array, 0, array.Count - 1),
                ESort.Heap         => SortHeap(array, 0, array.Count - 1),
                ESort.Merge        => SortMerge(array, 0, array.Count - 1),
                ESort.Quick        => SortQuick(array, 0, array.Count - 1),
                _                  => throw new ArgumentOutOfRangeException(nameof(sort), sort, "传入的排序方式不支持")
            };
        }

        /// <summary>
        ///     数组排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="func">比较器</param>
        /// <param name="sort">排序方式</param>
        /// <returns>排序后的数组 可无需赋值</returns>
        /// <remarks>支持快速排序、冒泡排序、双向冒泡排序、选择排序、插入排序、希尔排序、堆排序、归并排序、桶排序、基数排序</remarks>
        /// <exception cref="ArgumentOutOfRangeException">传入的排序方式不在枚举内</exception>
        /// <exception cref="ArgumentNullException">传入的数组为空</exception>
        public static IList<T> Sort<T>(
            this IList<T>        array,
            in   Func<T, T, int> func,
            in   ESort           sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count < 2) return array;
            var comparer = Comparer(func);
            return sort switch
            {
                ESort.Bubble       => SortBubble(array, 0, array.Count - 1, comparer),
                ESort.BubbleTwoWay => SortBubbleTwoWay(array, 0, array.Count - 1, comparer),
                ESort.Select       => SortSelect(array, 0, array.Count - 1, comparer),
                ESort.Insert       => SortInsert(array, 0, array.Count - 1, comparer),
                ESort.Shell        => SortShell(array, 0, array.Count - 1, comparer),
                ESort.Heap         => SortHeap(array, 0, array.Count - 1, comparer),
                ESort.Merge        => SortMerge(array, 0, array.Count - 1, comparer),
                ESort.Quick        => SortQuick(array, 0, array.Count - 1, comparer),
                _                  => throw new ArgumentOutOfRangeException(nameof(sort), sort, "传入的排序方式不支持")
            };
        }

        /// <summary>
        ///     数组排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="comparer">比较器</param>
        /// <param name="sort">排序方式</param>
        /// <returns>排序后的数组 可无需赋值</returns>
        /// <remarks>支持快速排序、冒泡排序、双向冒泡排序、选择排序、插入排序、希尔排序、堆排序、归并排序、桶排序、基数排序</remarks>
        /// <exception cref="ArgumentOutOfRangeException">传入的排序方式不在枚举内</exception>
        /// <exception cref="ArgumentNullException">传入的数组为空</exception>
        public static IList<T> Sort<T>(
            this IList<T>     array,
            in   IComparer<T> comparer,
            in   ESort        sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
            return sort switch
            {
                ESort.Bubble       => SortBubble(array, 0, array.Count - 1, comparer),
                ESort.BubbleTwoWay => SortBubbleTwoWay(array, 0, array.Count - 1, comparer),
                ESort.Select       => SortSelect(array, 0, array.Count - 1, comparer),
                ESort.Insert       => SortInsert(array, 0, array.Count - 1, comparer),
                ESort.Shell        => SortShell(array, 0, array.Count - 1, comparer),
                ESort.Heap         => SortHeap(array, 0, array.Count - 1, comparer),
                ESort.Merge        => SortMerge(array, 0, array.Count - 1, comparer),
                ESort.Quick        => SortQuick(array, 0, array.Count - 1, comparer),
                _                  => throw new ArgumentOutOfRangeException(nameof(sort), sort, "传入的排序方式不支持")
            };
        }

        /// <summary>
        ///     数组排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="comparer">比较器</param>
        /// <param name="sort">排序方式</param>
        /// <returns>排序后的数组 可无需赋值</returns>
        /// <remarks>支持快速排序、冒泡排序、双向冒泡排序、选择排序、插入排序、希尔排序、堆排序、归并排序、桶排序、基数排序</remarks>
        /// <exception cref="ArgumentOutOfRangeException">传入的排序方式不在枚举内</exception>
        /// <exception cref="ArgumentNullException">传入的数组为空</exception>
        public static T[] Sort<T>(
            this T[]          array,
            in   IComparer<T> comparer,
            in   ESort        sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Length <= 1) return array;
            Array.Sort(array, 0, array.Length - 1, comparer);
            return array;
        }

        /// <summary>
        ///     数组排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="func">比较器</param>
        /// <param name="sort">排序方式</param>
        /// <returns>排序后的数组 可无需赋值</returns>
        /// <remarks>支持快速排序、冒泡排序、双向冒泡排序、选择排序、插入排序、希尔排序、堆排序、归并排序、桶排序、基数排序</remarks>
        /// <exception cref="ArgumentOutOfRangeException">传入的排序方式不在枚举内</exception>
        /// <exception cref="ArgumentNullException">传入的数组为空</exception>
        public static T[] Sort<T>(
            this T[]             array,
            in   Func<T, T, int> func,
            in   ESort           sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Length <= 1) return array;
            var comparer = Comparer(func);
            Array.Sort(array, 0, array.Length - 1, comparer);
            return array;
        }
    }
}