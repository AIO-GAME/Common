/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Security.Cryptography;

namespace AIO
{
    using System;
    using System.Collections.Generic;
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
        public static string ToConvert(StringBlock Unicode, params string[] context)
        {
            return (Unicode ?? SpaceTemplate).Convert(context);
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
            if (len * 3 > gLen)
            {
                len = gLen / 3;
            }

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
        public static string GetMD5(in string sDataIn, Encoding encoding = null)
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
        public static string[] GetMD5(in ICollection<string> sDataIn, Encoding encoding = null)
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
    }
}