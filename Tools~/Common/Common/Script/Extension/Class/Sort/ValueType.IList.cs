﻿#region

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
        public static IList<ushort> Sort(this IList<ushort> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
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
        public static IList<byte> Sort(this IList<byte> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
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
        public static IList<long> Sort(this IList<long> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
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
        public static IList<int> Sort(this IList<int> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
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
        public static IList<short> Sort(this IList<short> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
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
        public static IList<sbyte> Sort(this IList<sbyte> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
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
        public static IList<uint> Sort(this IList<uint> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
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
        public static IList<ulong> Sort(this IList<ulong> array, in ESort sort = ESort.Quick)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
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
                ESort.Bucket       => SortBucket(array),
                ESort.Radix        => SortRadix(array),
                ESort.Counting     => SortCounting(array),
                _                  => throw new ArgumentOutOfRangeException(nameof(sort), sort, "传入的排序方式不支持")
            };
        }
    }
}