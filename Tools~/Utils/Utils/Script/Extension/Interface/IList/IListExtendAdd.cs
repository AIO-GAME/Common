using System;
using System.Collections.Generic;

namespace AIO
{
    public static partial class IListExtend
    {
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
        public static void Add<T>(this IList<T> array, in IEnumerable<T> arrys)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arrys) array.Add(item);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void AddRange<T>(this IList<T> array, in IList<T> others, int start, int end)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (others == null || others.Count == 0) return;

            start = MathUtils.Clamp(start, 0, others.Count);
            end = MathUtils.Clamp(end, start, others.Count);
            if (start < others.Count - 1 && end > start)
            {
                for (var i = start; i < end; ++i) array.Add(others[i]);
            }
        }
    }
}