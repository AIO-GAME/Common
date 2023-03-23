/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// 字符串工具库
    /// </summary>
    public static partial class StringUtils
    {
        private static StringBlock BlockTemplate = new StringBlock("╔╗║╚╝══║", 75);
        private static StringBlock SpaceTemplate = new StringBlock("        ", 75);

        /// <summary>
        /// 转化为区块
        /// </summary>
        public static string ToConvertBlock(params string[] context)
        {
            return BlockTemplate.Convert(context);
        }

        /// <summary>
        /// 转化为区块
        /// </summary>
        public static string ToConvertSpace(params string[] context)
        {
            return SpaceTemplate.Convert(context);
        }

        /// <summary>
        /// 转化为区块
        /// </summary>
        public static string ToConvert(in StringBlock Unicode, params string[] context)
        {
            return (Unicode ?? SpaceTemplate).Convert(context);
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

            for (var i = intK; i > 0; i--)
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

        private static char[] gHex = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static char[] gChars;
        private const int gLen = 1 * 1024 * 3;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string StringToHexView(in byte[] buff, int len)
        {
            if (len * 3 > gLen) len = gLen / 3;

            if (gChars == null)
            {
                gChars = new char[gLen];
            }

            gChars[len * 3 - 1] = ' ';
            for (var i = 0; i < len; ++i)
            {
                var b = buff[i];
                gChars[i * 3] = gHex[b >> 4];
                gChars[i * 3 + 1] = gHex[b & 0xF];
                gChars[i * 3 + 2] = (i + 1) % 10 == 0 ? '\n' : ' ';
            }

            return new string(gChars, 0, len * 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string StringToHex(in byte[] buff, int len = 0)
        {
            len = len <= 0 ? buff.Length : len;
            var r = new char[len * 2];
            for (var i = 0; i < len; ++i)
            {
                var b = buff[i];
                r[i * 2] = gHex[b >> 4];
                r[i * 2 + 1] = gHex[b & 0xF];
            }

            return new string(r);
        }

        private static byte CharToByte(in char c)
        {
            return (byte)(c >= 0x41 ? c - 0x41 : c - 0x30);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(in string hexStr)
        {
            var r = new byte[hexStr.Length / 2];
            for (var i = 0; i < r.Length; ++i)
            {
                r[i] = (byte)(CharToByte(hexStr[i * 2]) << 4 | CharToByte(hexStr[i * 2 + 1]));
            }

            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static string BtsToBase64(in byte[] buff)
        {
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Base64ToBts(in string str)
        {
            return Convert.FromBase64String(str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDataIn"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetMD5(in string sDataIn, in Encoding encoding = null)
        {
            var str = "";
            var data = (encoding ?? Encoding.UTF8).GetBytes(sDataIn);
            var md5 = new MD5CryptoServiceProvider();
            var bytes = md5.ComputeHash(data);
            foreach (var t in bytes) str = string.Concat(str, t.ToString("x2"));
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDataIn"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string[] GetMD5(in ICollection<string> sDataIn, in Encoding encoding = null)
        {
            var md5str = new string[sDataIn.Count];
            var builder = new StringBuilder();
            var index = 0;
            var md5 = new MD5CryptoServiceProvider();
            foreach (var item in sDataIn)
            {
                if (string.IsNullOrEmpty(item)) continue;
                builder.Clear();

                var data = (encoding ?? Encoding.UTF8).GetBytes(item);
                var bytes = md5.ComputeHash(data);

                foreach (var t in bytes) builder.Append(t.ToString("x2"));
                md5str[index++] = builder.ToString();
            }

            return md5str;
        }

        /// <summary>
        /// 是空还是空白
        /// </summary>
        public static bool IsNullOrWhiteSpace(in string s)
        {
            return s == null || s.Trim() == string.Empty;
        }

        /// <summary>
        /// 如果指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// 则回退
        /// </summary>
        public static string FallbackEmpty(string s, in string fallback)
        {
            if (string.IsNullOrEmpty(s)) s = fallback;
            return s;
        }

        /// <summary>
        /// 如果所有字符串为空白则回退
        /// </summary>
        public static string FallbackWhitespace(string s, in string fallback)
        {
            if (IsNullOrWhiteSpace(s)) s = fallback;
            return s;
        }
    }
}