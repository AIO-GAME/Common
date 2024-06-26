﻿#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 添加相同元素
        /// </summary>
        public static void AddUnion<T>(this IList<T> array, in IEnumerable<T> others)
        {
            if (array is null) throw new ArgumentNullException(nameof(array), "The input array is null.");
            if (others is null) return;
            foreach (var t in others)
                if (!array.Contains(t))
                    array.Add(t);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void Add<T>(this IList<T> array, in T a1, in T a2)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            array.Add(a1);
            array.Add(a2);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void Add<T>(this IList<T> array, in T a1, in T a2, in T a3)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            array.Add(a1);
            array.Add(a2);
            array.Add(a3);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void Add<T>(this IList<T> array, in T a1, in T a2, in T a3, in T a4)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            array.Add(a1);
            array.Add(a2);
            array.Add(a3);
            array.Add(a4);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void Add<T>(this IList<T> array, in T a1, in T a2, in T a3, in T a4, in T a5)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            array.Add(a1);
            array.Add(a2);
            array.Add(a3);
            array.Add(a4);
            array.Add(a5);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void Add<T>(this IList<T> array, IEnumerable<T> arr)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arr) array.Add(item);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void AddDicValue<T>(this IList<T> array, IDictionary<object, T> arr)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arr) array.Add(item.Value);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void AddDicKey<T>(this IList<T> array, IDictionary<T, object> arr)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arr) array.Add(item.Key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void AddRange<T>(this IList<T> array, IList<T> others, int start, int end)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (others == null || others.Count == 0) return;

            if (start < 0) start            = 0;
            if (start > others.Count) start = others.Count;

            if (end < start) end        = start;
            if (end > others.Count) end = others.Count;

            if (start < others.Count - 1 && end > start)
                for (var i = start; i < end; ++i)
                    array.Add(others[i]);
        }
    }
}