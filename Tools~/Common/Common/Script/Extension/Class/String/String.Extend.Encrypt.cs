using System;
using System.Text;

namespace AIO
{
    public partial class ExtendString
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后的数据</returns>
        public static string EncryptBase64(this string str, Encoding encoding = null)
        {
            var bytes = (encoding ?? Encoding.UTF8).GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解密后的数据</returns>
        public static string DecryptBase64(this string str, Encoding encoding = null)
        {
            try
            {
                var bytes = Convert.FromBase64String(str);
                return (encoding ?? Encoding.UTF8).GetString(bytes);
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>SHA256结果(返回长度为44字节的字符串)</returns>
        public static string GetSHA256(this string str, Encoding encoding = null)
        {
            var       sha256Data = (encoding ?? Encoding.UTF8).GetBytes(str);
            using var sha256     = System.Security.Cryptography.SHA256.Create();
            var       result     = sha256.ComputeHash(sha256Data);
            return Convert.ToBase64String(result); //返回长度为44字节的字符串
        }
    }
}