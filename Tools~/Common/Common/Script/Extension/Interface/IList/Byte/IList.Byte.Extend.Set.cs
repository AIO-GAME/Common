/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIO
{
    partial class ExtendIList
    {
        #region Array

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>
        public static void SetCharArray(this IList<byte> array, ref int index, in ICollection<char> value, in bool reverse = false)
        {
            var str = new StringBuilder();
            foreach (var c in value) str.Append(c);
            SetString(array, ref index, str, Encoding.UTF8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>
        public static void SetByteArray(this IList<byte> array, ref int index, in ICollection<byte> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            unchecked
            {
                if (reverse)
                {
                    var j = index;
                    foreach (var item in value) array[j++] = item;
                }
                else
                {
                    var j = index + value.Count - 1;
                    foreach (var item in value) array[j--] = item;
                }
                index += value.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetSByteArray(this IList<byte> array, ref int index, in ICollection<sbyte> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            if (reverse)
            {
                var j = index;
                foreach (var item in value) array[j++] = (byte)item;
            }
            else
            {
                var j = index + value.Count - 1;
                foreach (var item in value) array[j--] = (byte)item;
            }

            index += value.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetBoolArray(this IList<byte> array, ref int index, in ICollection<bool> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            if (reverse)
            {
                var j = index;
                foreach (var item in value) array[j++] = (byte)(item ? 1 : 0);
            }
            else
            {
                var j = index + value.Count - 1;
                foreach (var item in value) array[j--] = (byte)(item ? 1 : 0);
            }

            index += value.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetInt16Array(this IList<byte> array, ref int index, in ICollection<short> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetNumber(array, ref index, item, 2, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetInt32Array(this IList<byte> array, ref int index, in ICollection<int> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetNumber(array, ref index, item, 4, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetInt64Array(this IList<byte> array, ref int index, in ICollection<long> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetNumber(array, ref index, item, 8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetUInt16Array(this IList<byte> array, ref int index, in ICollection<ushort> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetNumber(array, ref index, item, 2, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetUInt32Array(this IList<byte> array, ref int index, in ICollection<uint> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetNumber(array, ref index, item, 4, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetUInt64Array(this IList<byte> array, ref int index, in ICollection<ulong> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetNumber(array, ref index, item, 8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>

        public static void SetLenArray(this IList<byte> array, ref int index, in ICollection<int> value)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetLen(array, ref index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>

        public static void SetFloatArray(this IList<byte> array, ref int index, in ICollection<float> value, bool all = false, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetFloat(array, ref index, item, all, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>

        public static void SetDoubleArray(this IList<byte> array, ref int index, in ICollection<double> value, bool all = false, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetDouble(array, ref index, item, all, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetDecimalArray(this IList<byte> array, ref int index, in ICollection<decimal> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetDecimal(array, ref index, item, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <param name="reverse"></param>

        public static void SetStringArray(this IList<byte> array, ref int index, in ICollection<string> value, Encoding encoding = null, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, encoding, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <param name="reverse"></param>

        public static void SetStringArray(this IList<byte> array, ref int index, in ICollection<StringBuilder> value, Encoding encoding = null, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, encoding, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8Array(this IList<byte> array, ref int index, in ICollection<string> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, Encoding.UTF8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8ASCII(this IList<byte> array, ref int index, in ICollection<string> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, Encoding.ASCII, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8Unicode(this IList<byte> array, ref int index, in ICollection<string> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, Encoding.Unicode, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8Array(this IList<byte> array, ref int index, in ICollection<StringBuilder> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, Encoding.UTF8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8ASCII(this IList<byte> array, ref int index, in ICollection<StringBuilder> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, Encoding.ASCII, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8Unicode(this IList<byte> array, ref int index, in ICollection<StringBuilder> value, in bool reverse = false)
        {
            SetLen(array, ref index, value.Count);
            foreach (var item in value) SetString(array, ref index, item, Encoding.Unicode, reverse);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>

        public static void SetBool(this IList<byte> array, ref int index, in bool value)
        {
            array[index++] = (byte)(value ? 1 : 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetChar(this IList<byte> array, ref int index, in char value, in bool reverse = false)
        {
            var bytes = BitConverter.GetBytes(value);
            SetByteArray(array, ref index, bytes, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="SystemException"></exception>

        public static void SetLen(this IList<byte> array, ref int index, in int value)
        {
            if (value >= 0x20000000 || value < 0) throw new SystemException("value overflow , current max overflow = (2^29-1) ! invalid len:" + value);
            if (value < 0x80)
            {
                unchecked { SetSByte(array, ref index, (sbyte)(value | 0x80)); }
                return;
            }
            if (value < 0x4000)
            {
                unchecked { SetUInt16(array, ref index, (ushort)(value | 0x4000), true); }
                return;
            }

            unchecked { SetInt32(array, ref index, value | 0x20000000, true); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>

        public static void SetEnum<T>(this IList<byte> array, in T value, ref int index, in bool reverse = false) where T : Enum
        {
            SetInt32(array, ref index, value.GetHashCode(), reverse);
        }

        #region SetNumber


        private static void SetNumber(this IList<byte> array, ref int index, in long value, in byte place, in bool reverse = false)
        {
            if (reverse)
                for (byte i = 0, j = (byte)(place - 1); i < place; i++, j--)
                    array[index++] = (byte)(value >> (j * 8));
            else
                for (byte i = 0; i < place; i++)
                    array[index++] = (byte)(value >> (i * 8));
        }


        private static void SetNumber(this IList<byte> array, ref int index, in int value, in byte place, in bool reverse = false)
        {
            unchecked
            {
                if (reverse)
                    for (byte i = 0, j = (byte)(place - 1); i < place; i++, j--)
                        array[index++] = (byte)(value >> (j * 8));
                else
                    for (byte i = 0; i < place; i++)
                        array[index++] = (byte)(value >> (i * 8));
            }
        }


        private static void SetNumber(this IList<byte> array, ref int index, in uint value, in byte place, in bool reverse = false)
        {
            unchecked
            {
                if (reverse)
                    for (byte i = 0, j = (byte)(place - 1); i < place; i++, j--)
                        array[index++] = (byte)(value >> (j * 8));
                else
                    for (byte i = 0; i < place; i++)
                        array[index++] = (byte)(value >> (i * 8));
            }
        }


        private static void SetNumber(this IList<byte> array, ref int index, in ulong value, in byte place, in bool reverse = false)
        {
            unchecked
            {
                if (reverse)
                    for (byte i = 0, j = (byte)(place - 1); i < place; i++, j--)
                        array[index++] = (byte)(value >> (j * 8));
                else
                    for (byte i = 0; i < place; i++)
                        array[index++] = (byte)(value >> (i * 8));
            }
        }

        #endregion

        #region Number

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>

        public static void SetByte(this IList<byte> array, ref int index, in byte value)
        {
            unchecked
            {
                array[index++] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>

        public static void SetSByte(this IList<byte> array, ref int index, in sbyte value)
        {
            unchecked { array[index++] = (byte)value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetInt16(this IList<byte> array, ref int index, in short value, in bool reverse = false)
        {
            SetNumber(array, ref index, value, 2, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetInt32(this IList<byte> array, ref int index, in int value, in bool reverse = false)
        {
            SetNumber(array, ref index, value, 4, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetInt64(this IList<byte> array, ref int index, in long value, in bool reverse = false)
        {
            SetNumber(array, ref index, value, 8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetUInt16(this IList<byte> array, ref int index, in ushort value, in bool reverse = false)
        {
            SetNumber(array, ref index, value, 2, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetUInt32(this IList<byte> array, ref int index, in uint value, in bool reverse = false)
        {
            SetNumber(array, ref index, value, 4, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetUInt64(this IList<byte> array, ref int index, in ulong value, in bool reverse = false)
        {
            SetNumber(array, ref index, value, 8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>

        public static void SetFloat(this IList<byte> array, ref int index, in float value, in bool all = false, in bool reverse = false)
        {
            byte[] bytes;
            if (all) bytes = Encoding.UTF8.GetBytes(value.ToString(CultureInfo.InvariantCulture));
            else bytes = BitConverter.GetBytes(value);
            SetByteArray(array, ref index, bytes, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="all"></param>
        /// <param name="reverse"></param>

        public static void SetDouble(this IList<byte> array, ref int index, in double value, in bool all = false, in bool reverse = false)
        {
            byte[] bytes;
            if (all) bytes = Encoding.UTF8.GetBytes(value.ToString(CultureInfo.InvariantCulture));
            else bytes = BitConverter.GetBytes(value);
            SetByteArray(array, ref index, bytes, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetDecimal(this IList<byte> array, ref int index, in decimal value, in bool reverse = false)
        {
            var list = decimal.GetBits(value);
            SetLen(array, ref index, list.Length);
            foreach (var item in list) SetInt32(array, ref index, item, reverse);
        }

        #endregion

        #region String

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <param name="reverse"></param>

        public static void SetString(this IList<byte> array, ref int index, in string value, in Encoding encoding = null, in bool reverse = false)
        {
            if (value == null)
            {
                SetLen(array, ref index, -1);
                return;
            }

            if (value.Length <= 0)
            {
                SetLen(array, ref index, 0);
                return;
            }

            var bytes = (encoding ?? Encoding.Default).GetBytes(value);
            SetByteArray(array, ref index, bytes, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <param name="reverse"></param>

        public static void SetString(this IList<byte> array, ref int index, in StringBuilder value, in Encoding encoding = null, in bool reverse = false)
        {
            if (value == null)
            {
                SetLen(array, ref index, -1);
                return;
            }

            if (value.Length <= 0)
            {
                SetLen(array, ref index, 0);
                return;
            }

            var bytes = (encoding ?? Encoding.Default).GetBytes(value.ToString());
            SetByteArray(array, ref index, bytes, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8(this IList<byte> array, ref int index, in string value, in bool reverse = false)
        {
            SetString(array, ref index, value, Encoding.UTF8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringASCII(this IList<byte> array, ref int index, in string value, in bool reverse = false)
        {
            SetString(array, ref index, value, Encoding.ASCII, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUnicode(this IList<byte> array, ref int index, in string value, in bool reverse = false)
        {
            SetString(array, ref index, value, Encoding.Unicode, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUTF8(this IList<byte> array, ref int index, in StringBuilder value, in bool reverse = false)
        {
            SetString(array, ref index, value, Encoding.UTF8, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringASCII(this IList<byte> array, ref int index, in StringBuilder value, in bool reverse = false)
        {
            SetString(array, ref index, value, Encoding.ASCII, reverse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="reverse"></param>

        public static void SetStringUnicode(this IList<byte> array, ref int index, in StringBuilder value, in bool reverse = false)
        {
            SetString(array, ref index, value, Encoding.Unicode, reverse);
        }

        #endregion
    }
}