﻿#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    /// <summary>
    ///     排序枚举
    /// </summary>
    public enum ESort : byte
    {
        /// <summary>
        ///     非比较排序: 计数排序 适用于数据量大但数据范围小的情况
        /// </summary>
        Counting,

        /// <summary>
        ///     非比较排序: 基数排序 适用于数据量大但数据范围小的情况
        /// </summary>
        Radix,

        /// <summary>
        ///     非比较排序: 桶排序 适用于数据量大但数据范围小的情况
        /// </summary>
        Bucket,

        /// <summary>
        ///     比较排序: 冒泡排序
        /// </summary>
        Bubble,

        /// <summary>
        ///     比较排序: 双向冒泡排序
        /// </summary>
        BubbleTwoWay,

        /// <summary>
        ///     比较排序: 选择排序
        /// </summary>
        Select,

        /// <summary>
        ///     比较排序: 插入排序
        /// </summary>
        Insert,

        /// <summary>
        ///     比较排序: 希尔排序
        /// </summary>
        Shell,

        /// <summary>
        ///     比较排序: 堆排序
        /// </summary>
        Heap,

        /// <summary>
        ///     比较排序: 归并排序
        /// </summary>
        Merge,

        /// <summary>
        ///     比较排序: 快速排序
        /// </summary>
        Quick
    }

    /// <summary>
    ///     Sort 扩展
    /// </summary>
    public static partial class ExtendSort
    {
        /// <summary>
        ///     比较器
        /// </summary>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回比较器</returns>
        internal static IComparer<T> Comparer<T>(Func<T, T, int> comparer) => new GeneraComparer<T>(comparer);

        /// <summary>
        ///     比较器
        /// </summary>
        /// <param name="comparer">比较器</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回比较器</returns>
        internal static IComparer<T> Comparer<T>(Comparison<T> comparer) => new ComparisonComparer<T>(comparer);

        /// <summary>
        ///     自动扩容，并保存数据
        /// </summary>
        /// <remarks>
        ///     如果数组为空，则创建一个新数组并保存数据
        ///     重新分配内存并保存数据
        /// </remarks>
        private static T[] AutomaticArray<T>(in Array array, in T value)
        {
            if (array is null) return new[] { value };
            var a = new T[array.Length + 1];
            Array.ConstrainedCopy(array, 0, a, 0, array.Length);
            a[array.Length] = value;
            return a;
        }

        /// <summary>
        ///     按升序插入 linklist
        /// </summary>
        /// <param name="linkedList"> 要排序的链表 </param>
        /// <param name="number"> 要插入排序的数字 </param>
        private static void InsertIntoLinkList<T>(LinkedList<T> linkedList, T number)
        where T : IComparable
        {
            if (linkedList.Count > 0)
                foreach (var node in
                         from i in linkedList
                         where i.CompareTo(number) > 0
                         select linkedList.Find(i))
                {
                    linkedList.AddBefore(node, number);
                    return;
                }

            linkedList.AddLast(number); // 链表为空时，插入到第一位  
        }

        #region Nested type: ComparisonComparer

        internal class ComparisonComparer<T> : IComparer<T>
        {
            private readonly Comparison<T> Comparer;

            public ComparisonComparer(Comparison<T> func)
            {
                Comparer = func;
            }

            #region IComparer<T> Members

            public int Compare(T x, T y) => Comparer(x, y);

            #endregion
        }

        #endregion

        #region Nested type: GeneraComparer

        internal class GeneraComparer<T> : IComparer<T>
        {
            private readonly Func<T, T, int> Comparer;

            public GeneraComparer(Func<T, T, int> func)
            {
                Comparer = func;
            }

            #region IComparer<T> Members

            public int Compare(T x, T y) => Comparer(x, y);

            #endregion
        }

        #endregion
    }
}