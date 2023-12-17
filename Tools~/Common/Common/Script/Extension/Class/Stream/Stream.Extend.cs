/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.IO;
using System.Security.Cryptography;

namespace AIO
{
    /// <summary>
    /// nameof(StreamExtend)
    /// </summary>
    public static partial class StreamExtend
    {
        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <returns>MD5</returns>
        public static string GetMD5(this Stream stream)
        {
            using var crypt = MD5.Create();
            return BitConverter.ToString(crypt.ComputeHash(stream)).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 获取SHA1
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <returns>SHA1</returns>
        public static string GetSHA1(this Stream stream)
        {
            using var crypt = SHA1.Create();
            return BitConverter.ToString(crypt.ComputeHash(stream)).Replace("-", "").ToLower();
        }


        /// <summary>
        /// 获取SHA256
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <returns>SHA256</returns>
        public static string GetSHA256(this Stream stream)
        {
            using var crypt = SHA256.Create();
            return BitConverter.ToString(crypt.ComputeHash(stream)).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 获取SHA512
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <returns>SHA512</returns>
        public static string GetSHA512(this Stream stream)
        {
            using var crypt = SHA512.Create();
            return BitConverter.ToString(crypt.ComputeHash(stream)).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 获取SHA384
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <returns>SHA384</returns>
        public static string GetSHA384(this Stream stream)
        {
            using var crypt = SHA384.Create();
            return BitConverter.ToString(crypt.ComputeHash(stream)).Replace("-", "").ToLower();
        }
    }
}