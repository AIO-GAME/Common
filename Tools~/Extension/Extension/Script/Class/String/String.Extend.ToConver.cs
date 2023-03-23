/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIO
{
    public static partial class StringExtend
    {
      
        /// <summary>
        /// 解析字符串为 ulong
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToConverULong(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default;
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// 解析字符串为 UInt
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ToConverUInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default;
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// 解析字符串为 UShort
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ToConverUshort(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default;
            return Convert.ToUInt16(value);
        }

        /// <summary>
        /// 解析字符串为 Double
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToConverDouble(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default;
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// 解析字符串为 Decimal
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal ToConverDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default;
            return Convert.ToDecimal(value);
        }

        /// <summary>
        /// 解析字符串为 DateTime
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToConverDateTime(this string value)
        {
            return Convert.ToDateTime(value);
        }

        /// <summary>
        /// 解析字符串为 Char
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char ToConverChar(this string value)
        {
            return Convert.ToChar(value);
        }

        /// <summary>
        /// 解析字符串为 Byte
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToConverBytes(this string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        /// <summary>
        /// 解析字符串为 Boolean
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ToConverBoolean(this string value)
        {
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// 解析字符串为 SByte
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ToConverSByte(this string value)
        {
            return Convert.ToSByte(value);
        }

        /// <summary>
        /// 解析字符串为 Short
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ToConverShort(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return Convert.ToInt16(value);
        }

        /// <summary>
        /// 解析字符串为 Int
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToConverInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// 解析字符串为 Long
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToConverLong(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0L;
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// 解析字符串为 Float
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToConverFloat(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0F;
            return Convert.ToSingle(value);
        }

        /// <summary>
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] ToConverInts(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<int>();
            var strs = value.Split(new char[] { ',' });
            var returns = new int[strs.Length];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverInt(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] ToConverInts(this string value, char split)
        {
            if (string.IsNullOrEmpty(value)) return Array.Empty<int>();
            var strs = value.Split(new char[] { split });
            var returns = new int[strs.Length];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverInt(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 解析字符串(以,|分割)为二维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[][] ToConverIntss(this string value)
        {
            if (string.IsNullOrEmpty(value)) return Array.Empty<int[]>();
            var strs = value.Split('|');
            var returns = new int[strs.Length][];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverInts(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 解析字符串(以,|,:分割)为三维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[][][] ToConverIntsss(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<int[][]>();
            var strs = value.Split(':');
            var returns = new int[strs.Length][][];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverIntss(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 转化为Int数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] ToConverInts(this IList<string> value)
        {
            var array = new int[value.Count];
            for (var i = 0; i < value.Count; i++)
            {
                array[i] = ToConverInt(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 转化为Int数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[][] ToConverInts(this string[][] value)
        {
            var array = new int[value.Length][];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverInts(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 转化为Int数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[][][] ToConverInts(this string[][][] value)
        {
            var array = new int[value.Length][][];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverInts(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[] ToConverLongs(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<long>();
            var strs = value.Split(new char[] { ',' });
            var returns = new long[strs.Length];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i].ToConverLong();
            }

            return returns;
        }

        /// <summary>
        /// 解析字符串(以,|分割)为二维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[][] ToConverLongss(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<long[]>();
            var strs = value.Split(new char[] { '|' });
            var returns = new long[strs.Length][];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverLongs(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 解析字符串(以,|分割)为一维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[] ToConverLongs(this IList<string> value)
        {
            var array = new long[value.Count];
            for (var i = 0; i < value.Count; i++)
            {
                array[i] = ToConverLong(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[][] ToConverLongss(this string[][] value)
        {
            var array = new long[value.Length][];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverLongs(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[][][] ToConverLongsss(this string[][][] value)
        {
            var array = new long[value.Length][][];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverLongss(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] ToConverStrings(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<string>();
            var strs = value.Split(new char[] { ',' });
            var returns = new string[strs.Length];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i];
            }

            return returns;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] ToConverStrings(this string value, char split)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<string>();
            var strs = value.Split(new char[] { split });
            var returns = new string[strs.Length];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i];
            }

            return returns;
        }

        /// <summary>
        /// 解析字符串(以,|分割)为二维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[][] ToConverStringss(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<string[]>();
            var strs = value.Split(new char[] { '|' });
            var returns = new string[strs.Length][];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverStrings(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 解析字符串(以,|分割)为三维数字数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[][][] ToConverStringsss(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<string[][]>();
            var strs = value.Split(new char[] { ':' });
            var returns = new string[strs.Length][][];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverStringss(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 转换为Bool 一维数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool[] ToConverBools(this string[] value)
        {
            var array = new bool[value.Length];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = value[i].ToConverBoolean();
            }

            return array;
        }

        /// <summary>
        /// 转换为Bool 二维数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool[][] ToConverBoolss(this string[][] value)
        {
            var array = new bool[value.Length][];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverBools(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 转换为Bool 三维数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool[][][] ToConverBoolsss(this string[][][] value)
        {
            var array = new bool[value.Length][][];
            for (var i = 0; i < value.Length; i++)
                array[i] = ToConverBoolss(value[i]);
            return array;
        }

        /// <summary>
        /// 转换Base64
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(value));
        }

        /// <summary>
        /// Base64转化为Str
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FromBase64ToStr(this string value)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(value));
        }
    }
}