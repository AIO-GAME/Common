/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Text;

namespace AIO
{
    public static partial class StringExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<string> ToLower(this IList<string> value)
        {
            if (value == null) return null;
            for (var i = 0; i < value.Count; i++)
                value[i] = string.IsNullOrEmpty(value[i]) ? value[i] : value[i].ToLower();
            return value;
        }

        /// <summary>
        /// 将文件大小(字节)转换为最适合的显示方式
        /// </summary>
        public static string ToConverStringFileSize(this long size)
        {
            var result = "0KB";
            var filelength = size.ToString().Length;
            if (filelength < 4)
                result = size + "byte";
            else if (filelength < 7)
                result = Math.Round(Convert.ToDouble(size / 1024d), 2) + "KB";
            else if (filelength < 10)
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024), 2) + "MB";
            else if (filelength < 13)
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024), 2) + "GB";
            else if (filelength < 17)
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024 / 1024), 2) + "TB";
            return result;
        }

        /// <summary>
        /// 转换为格式:00,000,000
        /// </summary>
        public static string ToConverStringMoney(this long value)
        {
            var tmp = value % 1000;
            var str = tmp.ToString();
            value /= 1000;
            while (value > 0)
            {
                if (tmp >= 0 && tmp < 10) str.AppendLast("00");
                else if (tmp >= 10 && tmp < 100) str = "0" + str;
                tmp = value % 1000;
                str = tmp + "," + str;
                value /= 1000;
            }

            return str;
        }

        /// <summary>
        /// 解析字符串为 ulong
        /// </summary>
        public static ulong ToConverULong(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// 解析字符串为 UInt
        /// </summary>
        public static uint ToConverUInt(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// 解析字符串为 UShort
        /// </summary>
        public static ushort ToConverUshort(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToUInt16(value);
        }

        /// <summary>
        /// 解析字符串为 Double
        /// </summary>
        public static double ToConverDouble(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// 解析字符串为 Decimal
        /// </summary>
        public static decimal ToConverDecimal(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToDecimal(value);
        }

        /// <summary>
        /// 解析字符串为 DateTime
        /// </summary>
        public static DateTime ToConverDateTime(this string value)
        {
            return Convert.ToDateTime(value);
        }

        /// <summary>
        /// 解析字符串为 Char
        /// </summary>
        public static char ToConverChar(this string value)
        {
            return Convert.ToChar(value);
        }

        /// <summary>
        /// 解析字符串为 Byte
        /// </summary>
        public static byte[] ToConverBytes(this string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        /// <summary>
        /// 解析字符串为 Boolean
        /// </summary>
        public static bool ToConverBoolean(this string value)
        {
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// 解析字符串为 SByte
        /// </summary>
        public static short ToConverSByte(this string value)
        {
            return Convert.ToSByte(value);
        }

        /// <summary>
        /// 解析字符串为 Short
        /// </summary>
        public static short ToConverShort(this string value)
        {
            if (value.IsNullOrEmpty()) return 0;
            return Convert.ToInt16(value);
        }

        /// <summary>
        /// 解析字符串为 Int
        /// </summary>
        public static int ToConverInt(this string value)
        {
            if (value.IsNullOrEmpty()) return 0;
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// 解析字符串为 Long
        /// </summary>
        public static long ToConverLong(this string value)
        {
            if (value.IsNullOrEmpty()) return 0L;
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// 解析字符串为 Float
        /// </summary>
        public static float ToConverFloat(this string value)
        {
            if (value.IsNullOrEmpty()) return 0F;
            return Convert.ToSingle(value);
        }

        /// <summary>
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        public static int[] ToConverInts(this string value)
        {
            if (value.IsNullOrEmpty())
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
        public static int[] ToConverInts(this string value, char split)
        {
            if (value.IsNullOrEmpty()) return Array.Empty<int>();
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
        public static int[][] ToConverIntss(this string value)
        {
            if (value.IsNullOrEmpty())
                return Array.Empty<int[]>();
            var strs = value.Split(new char[] { '|' });
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
        public static int[][][] ToConverIntsss(this string value)
        {
            if (value.IsNullOrEmpty())
                return Array.Empty<int[][]>();
            var strs = value.Split(new char[] { ':' });
            var returns = new int[strs.Length][][];
            for (var i = 0; i < returns.Length; i++)
            {
                returns[i] = ToConverIntss(strs[i]);
            }

            return returns;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] ToConverInts(this string[] value)
        {
            var array = new int[value.Length];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverInt(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[][] ToConverIntss(this string[][] value)
        {
            var array = new int[value.Length][];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverInts(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[][][] ToConverIntsss(this string[][][] value)
        {
            var array = new int[value.Length][][];
            for (var i = 0; i < value.Length; i++)
            {
                array[i] = ToConverIntss(value[i]);
            }

            return array;
        }

        /// <summary>
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        public static long[] ToConverLongs(this string value)
        {
            if (value.IsNullOrEmpty())
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
        public static long[][] ToConverLongss(this string value)
        {
            if (value.IsNullOrEmpty())
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
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long[] ToConverLongs(this string[] value)
        {
            var array = new long[value.Length];
            for (var i = 0; i < value.Length; i++)
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
        public static string[] ToConverStrings(this string value)
        {
            if (value.IsNullOrEmpty())
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
        public static string[] ToConverStrings(this string value, char split)
        {
            if (value.IsNullOrEmpty())
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
        public static string[][] ToConverStringss(this string value)
        {
            if (value.IsNullOrEmpty())
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
        public static string[][][] ToConverStringsss(this string value)
        {
            if (value.IsNullOrEmpty())
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
        public static string ToConverBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(value));
        }

        /// <summary>
        /// Base64转化为Str
        /// </summary>
        public static string FromBase64ToStr(this string value)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        /// 阿拉伯数字全部转化为中文数字 有单位 传入需全部为数字字符 简体中文
        /// </summary>
        /// <param name="num"></param>
        /// <param name="unitNum">单位截止下标,默认0,1:万后,2:亿后,3:万亿</param>
        public static string ToConverUnitsCNS(this string num, in int unitNum = 0)
        {
            if (num.Length == 0) return num;
            var str = new StringBuilder();
            var index = 0; //下标
            var intM = num.Length % 4;
            var intK = intM > 0 ? num.Length / 4 + 1 : num.Length / 4;

            for (var i = intK; i > unitNum; i--)
            {
                var intL = (i == intK && intM != 0) ? intM : 4;
                var four = num.Substring(index, intL); //得到一组四位数
                for (int j = 0, n; j < four.Length; j++) //内层循环在该组中的每一位数上循环
                {
                    n = Convert.ToInt32(four.Substring(j, 1)); //处理组中的每一位数加上所在的位
                    if (n == 0)
                    {
                        if (j < four.Length - 1
                            && Convert.ToInt32(four.Substring(j + 1, 1)) > 0
                            && !str.ToString().EndsWith(ChineseUnit.CNSNum[n]))
                            str.Append(ChineseUnit.CNSNum[n]);
                    }
                    else
                    {
                        if (!(n == 1
                              && (str.ToString().EndsWith(ChineseUnit.CNSNum[0]) | str.Length == 0)
                              && j == four.Length - 2))
                            str.Append(ChineseUnit.CNSNum[n]);
                        str.Append(ChineseUnit.CNSDigit[four.Length - j - 1]);
                    }
                }

                index += intL;
                if (i < intK) //如果不是最高位的一组,每组最后加上一个单位:",万,",",亿," 等
                {
                    if (Convert.ToInt32(four) != 0)
                        str.Append(ChineseUnit.CNSUnits[i - 1]); //如果所有4位不全是0则加上单位",万,",",亿,"等
                }
                else str.Append(ChineseUnit.CNSUnits[i - 1]); //处理最高位的一组,最后必须加上单位
            }

            return str.ToString();
        }

        /// <summary>
        /// 阿拉伯数字全部转化为中文数字 无单位 传入需全部为数字字符 简体中文
        /// </summary>
        public static string ToConverNoUnitsCNS(this string num)
        {
            if (num.Length == 0) return num;
            var str = new StringBuilder();
            var index = 0; //下标
            var intM = num.Length % 4;
            var intK = intM > 0 ? num.Length / 4 + 1 : num.Length / 4;

            for (int i = intK; i > 0; i--)
            {
                var intL = (i == intK && intM != 0) ? intM : 4;
                var four = num.Substring(index, intL); //得到一组四位数
                for (int j = 0, n; j < four.Length; j++) //内层循环在该组中的每一位数上循环
                {
                    n = Convert.ToInt32(four.Substring(j, 1)); //处理组中的每一位数加上所在的位
                    if (n == 0)
                    {
                        if (j < four.Length - 1
                            && Convert.ToInt32(four.Substring(j + 1, 1)) > 0
                            && !str.ToString().EndsWith(ChineseUnit.CNSNum[n]))
                            str.Append(ChineseUnit.CNSNum[n]);
                    }
                    else
                    {
                        if (!(n == 1
                              && (str.ToString().EndsWith(ChineseUnit.CNSNum[0]) | str.Length == 0)
                              && j == four.Length - 2))
                            str.Append(ChineseUnit.CNSNum[n]);
                        str.Append(ChineseUnit.CNSDigit[four.Length - j - 1]);
                    }
                }

                index += intL;
            }

            return str.ToString();
        }
    }
}