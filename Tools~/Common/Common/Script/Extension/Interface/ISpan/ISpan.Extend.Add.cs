using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class ExtendISpan
    {

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Add<T>(this T[] array, in T item)
        {
            var oldLength = array.Length;
            var newArray = new T[oldLength + 1];
            Array.Copy(array, newArray, oldLength);
            newArray[oldLength] = item;
            return newArray;
        }

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Add<T>(this T[] array, params T[] items)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (items == null || items.Length == 0) return array;

            var result = new T[array.Length + items.Length];
            Array.Copy(array, result, array.Length);
            Array.Copy(items, 0, result, array.Length, items.Length);
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Add<T>(this T[] array, in ICollection<T> items)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (items == null || items.Count == 0) return array;

            var result = new T[array.Length + items.Count];
            Array.Copy(array, result, array.Length);
            items.CopyTo(result, array.Length);
            return result;
        }
    }
}
