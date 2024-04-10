#region

using System;
using System.Collections.Generic;

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
        where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
            return sort switch
            {
                ESort.Bubble       => SortBubble(array),
                ESort.BubbleTwoWay => SortBubbleTwoWay(array),
                ESort.Select       => SortSelect(array),
                ESort.Insert       => SortInsert(array),
                ESort.Shell        => SortShell(array),
                ESort.Heap         => SortHeap(array),
                ESort.Merge        => SortMerge(array),
                ESort.Quick        => SortQuick(array),
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
            this IList<T>        array,
            in   Func<T, T, int> comparer,
            in   ESort           sort = ESort.Quick)
        where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
            return sort switch
            {
                ESort.Bubble       => SortBubble(array, comparer),
                ESort.BubbleTwoWay => SortBubbleTwoWay(array, comparer),
                ESort.Select       => SortSelect(array, comparer),
                ESort.Insert       => SortInsert(array, comparer),
                ESort.Shell        => SortShell(array, comparer),
                ESort.Heap         => SortHeap(array, comparer),
                ESort.Merge        => SortMerge(array, comparer),
                ESort.Quick        => SortQuick(array, comparer),
                _                  => throw new ArgumentOutOfRangeException(nameof(sort), sort, "传入的排序方式不支持")
            };
        }

        /// <summary>
        ///     数组排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="sort">排序方式</param>
        /// <returns>排序后的数组 可无需赋值</returns>
        /// <remarks>支持快速排序、冒泡排序、双向冒泡排序、选择排序、插入排序、希尔排序、堆排序、归并排序、桶排序、基数排序</remarks>
        /// <exception cref="ArgumentOutOfRangeException">传入的排序方式不在枚举内</exception>
        /// <exception cref="ArgumentNullException">传入的数组为空</exception>
        public static T[] Sort<T>(this T[] array, in ESort sort = ESort.Quick)
        where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Length <= 1) return array;
            _ = sort switch
            {
                ESort.Bubble       => SortBubble(array),
                ESort.BubbleTwoWay => SortBubbleTwoWay(array),
                ESort.Select       => SortSelect(array),
                ESort.Insert       => SortInsert(array),
                ESort.Shell        => SortShell(array),
                ESort.Heap         => SortHeap(array),
                ESort.Merge        => SortMerge(array),
                ESort.Quick        => SortQuick(array),
                _                  => throw new ArgumentOutOfRangeException(nameof(sort), sort, "传入的排序方式不支持")
            };
            return array;
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
        public static T[] Sort<T>(this T[]             array,
                                  in   Func<T, T, int> comparer,
                                  in   ESort           sort = ESort.Quick)
        where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Length <= 1) return array;
            _ = sort switch
            {
                ESort.Bubble       => SortBubble(array, comparer),
                ESort.BubbleTwoWay => SortBubbleTwoWay(array, comparer),
                ESort.Select       => SortSelect(array, comparer),
                ESort.Insert       => SortInsert(array, comparer),
                ESort.Shell        => SortShell(array, comparer),
                ESort.Heap         => SortHeap(array, comparer),
                ESort.Merge        => SortMerge(array, comparer),
                ESort.Quick        => SortQuick(array, comparer),
                _                  => throw new ArgumentOutOfRangeException(nameof(sort), sort, "传入的排序方式不支持")
            };
            return array;
        }
    }
}