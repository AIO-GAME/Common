using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class IListExtend
    {
        /// <summary>
        /// 添加相同元素
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddUnion<T>(this IList<T> array, in IEnumerable<T> others)
        {
            if (array is null) throw new ArgumentNullException(nameof(array), "The input array is null.");
            if (others is null) return;
            foreach (var t in others)
            {
                if (!array.Contains(t)) array.Add(t);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this IList<T> array, in T a1, in T a2)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            array.Add(a1);
            array.Add(a2);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this IList<T> array, in IEnumerable<T> arrys)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arrys) array.Add(item);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddDicValue<T>(this IList<T> array, in IDictionary<object, T> arrys)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arrys) array.Add(item.Value);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddDicKey<T>(this IList<T> array, in IDictionary<T, object> arrys)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arrys) array.Add(item.Key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this IList<T> array, in IList<T> others, int start, int end)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (others == null || others.Count == 0) return;

            if (start < 0) start = 0;
            if (start > others.Count) start = others.Count;

            if (end < start) end = start;
            if (end > others.Count) end = others.Count;

            if (start < others.Count - 1 && end > start)
            {
                for (var i = start; i < end; ++i) array.Add(others[i]);
            }
        }
    }
}