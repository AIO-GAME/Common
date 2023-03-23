using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 迭代器扩展
    /// </summary>
    public static partial class IEnumerableExtend
    {
        /// <summary>
        /// 查找 -1:未找到
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Find<T>(this IEnumerable<T> array, in T value) where T : IEquatable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var index = 0;
            foreach (var item in array)
            {
                if (value.Equals(item))
                    return index;
                index++;
            }

            return -1;
        }

        /// <summary>
        /// 查找 -1:未找到
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Find<T>(this IEnumerable<T> array, in T value, int number) where T : IEquatable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (value == null) throw new ArgumentNullException(nameof(value));
            var index = 0;
            foreach (var item in array)
            {
                if (value.Equals(item))
                {
                    if (--number <= 0) return index;
                }

                index++;
            }

            return -1;
        }
    }
}