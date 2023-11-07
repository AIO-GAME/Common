using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class ExtendISpan
    {
        /// <summary>
        /// 移除
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] RemoveALL<T>(this T[] array, in T value) where T : IEqualityComparer<T>
        {
            var count = 0;
            foreach (var item in array)
            {
                if (value.Equals(item)) count++;
            }

            var result = new T[array.Length - count];
            var index = 0;
            foreach (var item in array)
            {
                if (!value.Equals(item))
                {
                    result[index++] = item;
                }
            }

            if (index != result.Length) throw new KeyNotFoundException(nameof(value));

            return result;
        }

        /// <summary>
        /// 移除
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Remove<T>(this T[] array, in T value) where T : IEqualityComparer<T>
        {
            var result = new T[array.Length - 1];
            var index = 0;
            var found = false;

            foreach (var item in array)
            {
                if (value.Equals(item) && !found) found = true;
                else result[index++] = item;
            }

            if (!found) throw new KeyNotFoundException(nameof(value));
            return result;
        }

        /// <summary>
        /// 移除
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] RemoveAt<T>(this T[] arrays, in int index)
        {
            var newLength = arrays.Length - 1;
            var copy = new T[newLength];
            Array.ConstrainedCopy(arrays, 0, copy, 0, index);
            Array.ConstrainedCopy(arrays, index + 1, copy, index, newLength - index);
            return copy;
        }
    }
}