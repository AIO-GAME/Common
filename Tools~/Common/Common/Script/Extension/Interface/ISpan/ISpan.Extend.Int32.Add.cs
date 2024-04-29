#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendISpan
    {
        #region byte

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks> 
        public static int[] Add(this int[] arrays, in byte item)
        {
            var oldLength = arrays.Length;
            var newArray = new int[oldLength + 1];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, oldLength);
            newArray[oldLength] = item;
            return newArray;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, params byte[] items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Length == 0) return arrays;

            var result = new int[arrays.Length + items.Length];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            for (var i = 0; i < items.Length; i++) result[arrays.Length + i] = items[i];
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, in ICollection<byte> items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Count == 0) return arrays;

            var result = new int[arrays.Length + items.Count];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            var index = arrays.Length;
            foreach (var item in items) arrays[index++] = item;
            return result;
        }

        #endregion

        #region sbyte

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks> 
        public static int[] Add(this int[] arrays, in sbyte item)
        {
            var oldLength = arrays.Length;
            var newArray = new int[oldLength + 1];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, oldLength);
            newArray[oldLength] = item;
            return newArray;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, params sbyte[] items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Length == 0) return arrays;

            var result = new int[arrays.Length + items.Length];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            for (var i = 0; i < items.Length; i++) result[arrays.Length + i] = items[i];
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, in ICollection<sbyte> items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Count == 0) return arrays;

            var result = new int[arrays.Length + items.Count];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            var index = arrays.Length;
            foreach (var item in items) arrays[index++] = item;
            return result;
        }

        #endregion

        #region short

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks> 
        public static int[] Add(this int[] arrays, in short item)
        {
            var oldLength = arrays.Length;
            var newArray = new int[oldLength + 1];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, oldLength);
            newArray[oldLength] = item;
            return newArray;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, params short[] items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Length == 0) return arrays;

            var result = new int[arrays.Length + items.Length];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            for (var i = 0; i < items.Length; i++) result[arrays.Length + i] = items[i];
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, in ICollection<short> items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Count == 0) return arrays;

            var result = new int[arrays.Length + items.Count];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            var index = arrays.Length;
            foreach (var item in items) arrays[index++] = item;
            return result;
        }

        #endregion

        #region ushort

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks> 
        public static int[] Add(this int[] arrays, in ushort item)
        {
            var oldLength = arrays.Length;
            var newArray = new int[oldLength + 1];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, oldLength);
            newArray[oldLength] = item;
            return newArray;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, params ushort[] items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Length == 0) return arrays;

            var result = new int[arrays.Length + items.Length];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            for (var i = 0; i < items.Length; i++) result[arrays.Length + i] = items[i];
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static int[] Add(this int[] arrays, in ICollection<ushort> items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Count == 0) return arrays;

            var result = new int[arrays.Length + items.Count];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            var index = arrays.Length;
            foreach (var item in items) arrays[index++] = item;
            return result;
        }

        #endregion
    }
}