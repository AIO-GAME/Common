using System;
using System.IO;
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
        public static string MD5String(this string message)
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
        public static string MD5String2(this string message) => MD5String(MD5String(message));

        /// <summary>
        /// MD5 三次摘要算法
        /// </summary>
        /// <param name="s">需要摘要的字符串</param>
        /// <returns>MD5摘要字符串</returns>
        public static string MD5String3(this string s)
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
        public static string MD5String(this string message, string salt) => MD5String(string.Concat(message, salt));

        /// <summary>
        ///     对字符串进行MD5二次加盐摘要
        /// </summary>
        /// <param name="message">需要摘要的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5摘要字符串</returns>
        public static string MD5String2(this string message, string salt) => MD5String(MD5String(string.Concat(message, salt)), salt);

        /// <summary>
        /// MD5 三次摘要算法
        /// </summary>
        /// <param name="s">需要摘要的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5摘要字符串</returns>
        public static string MD5String3(this string s, string salt)
        {
            using var md5    = MD5.Create();
            var       bytes  = Encoding.ASCII.GetBytes(string.Concat(s, salt));
            var       bytes1 = md5.ComputeHash(bytes);
            var       bytes2 = md5.ComputeHash(bytes1);
            var       bytes3 = md5.ComputeHash(bytes2);
            return GetHexString(bytes3);
        }

        #endregion

        #region 获取文件的MD5值

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="fileName">需要求MD5值的文件的文件名及路径</param>
        /// <returns>MD5摘要字符串</returns>
        public static string MDFile(this string fileName)
        {
            using var fs    = new BufferedStream(File.Open(fileName, FileMode.Open, FileAccess.Read), 1048576);
            using var md5   = MD5.Create();
            var       bytes = md5.ComputeHash(fs);
            return GetHexString(bytes);
        }

        /// <summary>
        /// 计算文件的sha256
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string SHA256(this Stream stream)
        {
            using var fs       = new BufferedStream(stream, 1048576);
            using var sha      = System.Security.Cryptography.SHA256.Create();
            var       checksum = sha.ComputeHash(fs);
            return BitConverter.ToString(checksum).Replace("-", string.Empty);
        }

        /// <summary>
        /// 获取数据流的MD5摘要值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>MD5摘要字符串</returns>
        public static string MD5String(this Stream stream)
        {
            using var fs    = new BufferedStream(stream, 1048576);
            using var md5   = MD5.Create();
            var       bytes = md5.ComputeHash(fs);
            var       mdStr = GetHexString(bytes);
            stream.Position = 0;
            return mdStr;
        }

        public static string GetHexString(byte[] bytes)
        {
            var hexArray = new char[bytes.Length << 1];
            for (var i = 0; i < hexArray.Length; i += 2)
            {
                var b = bytes[i >> 1];
                hexArray[i]     = GetHexValue(b >> 4);  // b / 16
                hexArray[i + 1] = GetHexValue(b & 0xF); // b % 16
            }

            return new string(hexArray, 0, hexArray.Length);

            static char GetHexValue(int i)
            {
                if (i < 10) return (char)(i + '0');
                return (char)(i - 10 + 'a');
            }
        }

        #endregion
    }
}