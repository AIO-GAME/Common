using System.Buffers;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AIO
{
    public partial class ExtendString
    {
        #region 对字符串进行MD5

        /// <summary>
        ///     对字符串进行MD5摘要
        /// </summary>
        /// <param name="message">需要摘要的字符串</param>
        /// <returns>MD5摘要字符串</returns>
        public static string GetMD5Digest(this string message)
        {
            var md5    = MD5.Create();
            var buffer = Encoding.Default.GetBytes(message);
            var bytes  = md5.ComputeHash(buffer);
            return GetHexString(bytes);
        }

        /// <summary>
        ///     对字符串进行MD5二次摘要
        /// </summary>
        /// <param name="message">需要摘要的字符串</param>
        /// <returns>MD5摘要字符串</returns>
        public static string GetMD5Digest2(this string message) => GetMD5Digest(GetMD5Digest(message));

        /// <summary>
        /// MD5 三次摘要算法
        /// </summary>
        /// <param name="s">需要摘要的字符串</param>
        /// <returns>MD5摘要字符串</returns>
        public static string GetMD5Digest3(this string s)
        {
            using var md5    = MD5.Create();
            var       bytes  = Encoding.ASCII.GetBytes(s);
            var       bytes1 = md5.ComputeHash(bytes);
            var       bytes2 = md5.ComputeHash(bytes1);
            var       bytes3 = md5.ComputeHash(bytes2);
            return GetHexString(bytes3);
        }

        /// <summary>
        ///     对字符串进行MD5加盐摘要
        /// </summary>
        /// <param name="message">需要摘要的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5摘要字符串</returns>
        public static string GetMD5Digest(this string message, string salt) => GetMD5Digest(string.Concat(message, salt));

        /// <summary>
        ///     对字符串进行MD5二次加盐摘要
        /// </summary>
        /// <param name="message">需要摘要的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5摘要字符串</returns>
        public static string GetMD5Digest2(this string message, string salt) => GetMD5Digest(GetMD5Digest(string.Concat(message, salt)), salt);

        /// <summary>
        /// MD5 三次摘要算法
        /// </summary>
        /// <param name="s">需要摘要的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5摘要字符串</returns>
        public static string GetMD5Digest3(this string s, string salt)
        {
            using var md5    = MD5.Create();
            var       bytes  = Encoding.ASCII.GetBytes(string.Concat(s, salt));
            var       bytes1 = md5.ComputeHash(bytes);
            var       bytes2 = md5.ComputeHash(bytes1);
            var       bytes3 = md5.ComputeHash(bytes2);
            return GetHexString(bytes3);
        }

        #endregion

        private static string GetHexString(in IList<byte> bytes)
        {
            var hexArray = ArrayPool<char>.Create().Rent(bytes.Count << 1);
            for (var i = 0; i < hexArray.Length; i += 2)
            {
                var b = bytes[i >> 1];
                hexArray[i]     = GetHexValue(b >> 4);  // b / 16
                hexArray[i + 1] = GetHexValue(b & 0xF); // b % 16
            }

            var str = new string(hexArray, 0, hexArray.Length);
            ArrayPool<char>.Shared.Return(hexArray, true);
            return str;
            char GetHexValue(int i) => i < 10 ? (char)(i + '0') : (char)(i - 10 + 'a');
        }
    }
}