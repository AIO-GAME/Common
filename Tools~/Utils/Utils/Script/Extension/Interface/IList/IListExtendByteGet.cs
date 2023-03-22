using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace AIO
{
    public static partial class IListExtend
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T[] GetLenArrayEmtpy<T>(this IList<byte> array, ref int index)
        {
            var len = GetLen(array, ref index);
            if (len < 0) return null;
            if (len == 0) return Array.Empty<T>();
            if (len > array.Count) throw new SystemException("data overflow:" + len);
            return new T[len];
        }

        #region Array

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char[] GetCharArray(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var str = GetString(array, ref index, Encoding.UTF8, reverse);
            if (str == null) return null;
            if (str.Length == 0) return Array.Empty<char>();
            return str.ToCharArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool[] GetBoolArray(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<bool>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            if (reverse)
                for (int i = 0, j = index; i < list.Length; i++, j++)
                    list[i] = array[j] != 0;
            else
                for (int i = 0, j = index + list.Length - 1; i < list.Length; i++, j--)
                    list[i] = array[j] != 0;
            index += list.Length;
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte[] GetSByteArray(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<sbyte>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            if (reverse)
                for (int i = 0, j = index; i < list.Length; i++, j++)
                    list[i] = (sbyte)array[j];
            else
                for (int i = 0, j = index + list.Length - 1; i < list.Length; i++, j--)
                    list[i] = (sbyte)array[j];
            index += list.Length;
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetByteArray(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<byte>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            if (reverse)
                for (var i = 0; i < list.Length; i++)
                    list[i] = array[index + i];
            else
                for (int i = 0, j = index + list.Length - 1; i < list.Length; i++, --j)
                    list[i] = array[j];
            index += list.Length;
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short[] GetInt16Array(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<short>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetInt16(array, ref index, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort[] GetUInt16Array(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<ushort>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetUInt16(array, ref index, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] GetUInt32Array(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<uint>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetUInt32(array, ref index, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] GetInt32Array(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<int>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetInt32(array, ref index, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong[] GetUInt64Array(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<ulong>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetUInt64(array, ref index, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[] GetInt64Array(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<long>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetInt64(array, ref index, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] GetFloatArray(this IList<byte> array, ref int index, bool all = false, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<float>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetFloat(array, ref index, all, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] GetDoubleArray(this IList<byte> array, ref int index, bool all = false, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<double>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetDouble(array, ref index, all, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal[] GetDecimalArray(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<decimal>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetDecimal(array, ref index, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] GetLenArray(this IList<byte> array, ref int index)
        {
            var list = GetLenArrayEmtpy<int>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetLen(array, ref index);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="encoding"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] GetStringArray(this IList<byte> array, ref int index, Encoding encoding = null, in bool reverse = false)
        {
            var list = GetLenArrayEmtpy<string>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetString(array, ref index, encoding, reverse);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] GetEnumArray<T>(this IList<byte> array, ref int index, in bool reverse = false) where T : Enum
        {
            var list = GetLenArrayEmtpy<T>(array, ref index);
            if (list == null || list.Length <= 0) return list;
            for (var i = 0; i < list.Length; i++) list[i] = GetEnum<T>(array, ref index, reverse);
            return list;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBool(this IList<byte> array, ref int index)
        {
            return array[index++] != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetEnum<T>(this IList<byte> array, ref int index, in bool reverse = false) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), GetInt32(array, ref index, reverse).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char GetChar(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var bytes = GetByteArray(array, ref index, reverse);
            return BitConverter.ToChar(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetLen(this IList<byte> array, ref int index)
        {
            var n = array[index] & 0xFF;
            if (n < 0x20) return -1;
            if (n < 0x40) return GetInt32(array, ref index, true) & 0x1FFFFFFF; //0x1FFFFFFF
            if (n < 0x80) return GetUInt16(array, ref index, true) & 0x3FFF;
            return GetSByte(array, ref index) & 0x7F;
        }

        #region Number

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long GetNumber(this IList<byte> array, ref int index, int place, long unit, in bool reverse = false)
        {
            var value = 0L;
            if (reverse)
                for (int i = 0, j = place - 1; i < place; i++, j--)
                    value |= (array[index++] & unit) << (j * 8);
            else
                for (var i = 0; i < place; i++)
                    value |= (array[index++] & unit) << (i * 8);

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte GetByte(this IList<byte> array, ref int index)
        {
            return array[index++];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte GetSByte(this IList<byte> array, ref int index)
        {
            return (sbyte)array[index++];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short GetInt16(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return (short)GetNumber(array, ref index, 2, 0xFF, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort GetUInt16(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return (ushort)GetNumber(array, ref index, 2, 0xFFU, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetInt32(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return (int)GetNumber(array, ref index, 4, 0xFF, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetUInt32(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return (uint)GetNumber(array, ref index, 4, 0xFFU, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetInt64(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return GetNumber(array, ref index, 8, 0xFFFL, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetUInt64(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var value = 0UL;
            const ulong unit = 0xFFFUL;
            if (reverse)
                for (int i = 0, j = 7; i < 8; i++, j--)
                    value |= (array[index++] & unit) << (j * 8);
            else
                for (var i = 0; i < 8; i++)
                    value |= (array[index++] & unit) << (i * 8);
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetFloat(this IList<byte> array, ref int index, in bool all = false, in bool reverse = false)
        {
            var bytes = GetByteArray(array, ref index, reverse);
            if (all)
            {
                var value = Encoding.UTF8.GetString(bytes);
                if (string.IsNullOrEmpty(value)) throw new NullReferenceException();
                return float.Parse(value, NumberStyles.Float | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
            }
            else return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDouble(this IList<byte> array, ref int index, in bool all = false, in bool reverse = false)
        {
            var bytes = GetByteArray(array, ref index, reverse);
            if (all)
            {
                var value = Encoding.UTF8.GetString(bytes);
                if (string.IsNullOrEmpty(value)) throw new NullReferenceException();
                return float.Parse(value, NumberStyles.Float | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
            }
            else return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal GetDecimal(this IList<byte> array, ref int index, in bool reverse = false)
        {
            var list = new int[GetLen(array, ref index)];
            for (var i = 0; i < list.Length; i++) list[i] = GetInt32(array, ref index, reverse);
            return new decimal(list);
        }

        #endregion

        #region String

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="encoding"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        /// <exception cref="SystemException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(this IList<byte> array, ref int index, Encoding encoding = null, in bool reverse = false)
        {
            var len = GetLen(array, ref index);
            if (len < 0) return null;
            if (len == 0) return "";
            if (len > array.Count) throw new SystemException("data overflow:" + len);

            var value = new byte[len];
            if (reverse)
            {
                for (var i = 0; i < len; i++)
                {
                    value[i] = array[index + i];
                }
            }
            else
            {
                for (int i = 0, j = index + len - 1; i < len; i++, j--)
                {
                    value[i] = array[j];
                }
            }

            index += len;

            return (encoding ?? Encoding.Default).GetString(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetStringUTF8(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return GetString(array, ref index, Encoding.UTF8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetStringASCII(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return GetString(array, ref index, Encoding.ASCII, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetStringUnicode(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return GetString(array, ref index, Encoding.Unicode, reverse);
        }

        #endregion

        #region Json

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="settings"></param>
        /// <param name="encoding"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetJson<T>(this IList<byte> array, ref int index, JsonSerializerSettings settings = null, Encoding encoding = null, in bool reverse = false)
        {
            var data = GetString(array, ref index, encoding, reverse);
            return ParseUtils.JsonDeserialize<T>(data, settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="settings"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetJsonUTF8<T>(this IList<byte> array, ref int index, JsonSerializerSettings settings = null, in bool reverse = false)
        {
            var data = GetString(array, ref index, Encoding.UTF8, reverse);
            return ParseUtils.JsonDeserialize<T>(data, settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="settings"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetJsonASCII<T>(this IList<byte> array, ref int index, JsonSerializerSettings settings = null, in bool reverse = false)
        {
            var data = GetString(array, ref index, Encoding.ASCII, reverse);
            return ParseUtils.JsonDeserialize<T>(data, settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="settings"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetJsonUnicode<T>(this IList<byte> array, ref int index, JsonSerializerSettings settings = null, in bool reverse = false)
        {
            var data = GetString(array, ref index, Encoding.Unicode, reverse);
            return ParseUtils.JsonDeserialize<T>(data, settings);
        }

        #endregion
    }
}