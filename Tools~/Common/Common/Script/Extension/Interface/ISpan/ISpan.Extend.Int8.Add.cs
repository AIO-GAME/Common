using System;
using System.Collections.Generic;

namespace AIO
{
    partial class ExtendISpan
    {
        #region sbyte

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks> 
        public static sbyte[] Add(this sbyte[] arrays, in sbyte item)
        {
            var oldLength = arrays.Length;
            var newArray = new sbyte[oldLength + 1];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, oldLength);
            newArray[oldLength] = (sbyte)(object)item;
            return newArray;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.Add(1)
        /// </remarks>
        public static sbyte[] Add(this sbyte[] arrays, params sbyte[] items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Length == 0) return arrays;

            var result = new sbyte[arrays.Length + items.Length];
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
        public static sbyte[] Add(this sbyte[] arrays, in ICollection<sbyte> items)
        {
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));
            if (items == null || items.Count == 0) return arrays;

            var result = new sbyte[arrays.Length + items.Count];
            Array.ConstrainedCopy(arrays, 0, result, 0, arrays.Length);
            var index = arrays.Length;
            foreach (var item in items) arrays[index++] = item;
            return result;
        }

        #endregion
    }
}