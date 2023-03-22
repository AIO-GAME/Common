/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    // float    类型:System.Single    说明:32位单精度浮点数    位数:7
    // double   类型:System.Double    说明:64位双精度浮点数    位数:15/16
    // decimal  类型:System.Decimal   说明:128位双精度浮点数   位数:28/29   范围:(-7.9 x 1028 - 7.9 x 1028) / (100 - 28)

    //  sbyte   表示有符号的8位整数      -128~127
    //  byte    表示无符号的8位整数      0~255
    //  short   表示有符号的16位整数     -32768~32767
    //  ushort  表示无符号的16位整数     0~65535
    //  int     表示有符号的32位整数     -2147483648~2147483647
    //  uint    表示无符号的32位整数     0~4294967295
    //  long    表示有符号的64位整数     -9223372036854775808~9223372036854775807
    //  ulong   表示无符号的64位整数     0~18446744073709551615
    //  char    表示无符号的16位整数     0~65535

    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 数组方法
    /// </summary>
    public static partial class ArrayExtend
    {
        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] AutoMaticExpansion<T>(this T[] arrays, in int offset, in int len)
        {
            var c = arrays.Length;
            var v = offset + len;
            if (v >= c) Array.Resize(ref arrays, c + len);
            return arrays;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] CopyTo<T>(this T[] arrays, in int Offset)
        {
            var copy = new T[Offset];
            Array.ConstrainedCopy(arrays, 0, copy, 0, arrays.Length);
            return copy;
        }

        #region Add

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Add<T>(this T[] arrays, in T value)
        {
            var result = new T[arrays.Length + 1];
            arrays.CopyTo(result, 0);
            result[arrays.Length] = value;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Add<T>(this T[] arrays, params T[] values)
        {
            var result = new T[arrays.Length + values.Length];
            arrays.CopyTo(result, 0);
            var index = arrays.Length;
            foreach (var item in values) result[index++] = item;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Add<T>(this T[] arrays, in ICollection<T> values)
        {
            var result = new T[arrays.Length + values.Count];
            arrays.CopyTo(result, 0);
            var index = arrays.Length;
            foreach (var item in values) result[index++] = item;
            return result;
        }

        #endregion

        #region Insert

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] arrays, in int offset, params T[] values)
        {
            var copy = new T[arrays.Length - offset];
            arrays = AutoMaticExpansion(arrays, offset, values.Length);
            Array.ConstrainedCopy(arrays, offset, copy, 0, copy.Length);
            Array.ConstrainedCopy(values, 0, arrays, offset, values.Length);
            Array.ConstrainedCopy(copy, 0, arrays, offset + values.Length, copy.Length);
            return arrays;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] arrays, in int offset, in Array values)
        {
            var copy = new T[arrays.Length - offset];
            arrays = AutoMaticExpansion(arrays, offset, values.Length);
            Array.ConstrainedCopy(arrays, offset, copy, 0, copy.Length);
            Array.ConstrainedCopy(values, 0, arrays, offset, values.Length);
            Array.ConstrainedCopy(copy, 0, arrays, offset + values.Length, copy.Length);
            return arrays;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] arrays, in int offset, in T values)
        {
            var copy = new T[arrays.Length - offset];
            arrays = AutoMaticExpansion(arrays, offset, 1);
            Array.ConstrainedCopy(arrays, offset, copy, 0, copy.Length);
            arrays[offset] = values;
            Array.ConstrainedCopy(copy, 0, arrays, offset + 1, copy.Length);
            return arrays;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Insert<T>(this T[] arrays, int offset, in ICollection<T> values)
        {
            var copy = new T[arrays.Length - offset];
            arrays = AutoMaticExpansion(arrays, offset, values.Count);
            Array.ConstrainedCopy(arrays, offset, copy, 0, copy.Length);
            foreach (var item in values) arrays[offset++] = item;
            Array.ConstrainedCopy(copy, 0, arrays, offset + 1, copy.Length);
            return arrays;
        }

        #endregion

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

        /// <summary>
        /// 移除
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Remove<T>(this T[] arrays, in T value)
        {
            var copy = new T[arrays.Length - 1];
            for (var i = 0; i < arrays.Length; i++)
            {
                if (arrays[i].Equals(value)) continue;
                if (i == copy.Length) throw new KeyNotFoundException(nameof(value));
                copy[i] = arrays[i];
            }

            return copy;
        }
    }
}