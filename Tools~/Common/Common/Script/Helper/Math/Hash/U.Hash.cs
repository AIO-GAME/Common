/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AIO;

public partial class AHelper
{
    /// <summary>
    /// hash工具
    /// </summary>
    public partial class Hash
    {
        private static string ToString(byte[] hashBytes)
        {
            return BitConverter.ToString(hashBytes).ToLower().Replace("-", "");
        }

        #region SHA1

        /// <summary>
        /// 获取字符串的Hash值
        /// </summary>
        public static string StringSHA1(in string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            return BytesSHA1(buffer);
        }

        /// <summary>
        /// 获取文件的Hash值
        /// </summary>
        public static string FileSHA1(in string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return StreamSHA1(fs);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取数据流的Hash值
        /// </summary>
        public static string StreamSHA1(Stream stream)
        {
            // 说明：创建的是SHA1类的实例，生成的是160位的散列码
            var hash = HashAlgorithm.Create();
            var hashBytes = hash.ComputeHash(stream);
            return ToString(hashBytes);
        }

        /// <summary>
        /// 获取字节数组的Hash值
        /// </summary>
        public static string BytesSHA1(byte[] buffer)
        {
            // 说明：创建的是SHA1类的实例，生成的是160位的散列码
            var hash = HashAlgorithm.Create();
            var hashBytes = hash.ComputeHash(buffer);
            return ToString(hashBytes);
        }

        #endregion

        #region MD5

        /// <summary>
        /// 获取字符串的MD5
        /// </summary>
        public static string StringMD5(in string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            return BytesMD5(buffer);
        }

        /// <summary>
        /// 获取文件的MD5
        /// </summary>
        public static string FileMD5(in string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return StreamMD5(fs);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取数据流的MD5
        /// </summary>
        public static string StreamMD5(Stream stream)
        {
            var provider = new MD5CryptoServiceProvider();
            var hashBytes = provider.ComputeHash(stream);
            return ToString(hashBytes);
        }

        /// <summary>
        /// 获取字节数组的MD5
        /// </summary>
        public static string BytesMD5(byte[] buffer)
        {
            var provider = new MD5CryptoServiceProvider();
            var hashBytes = provider.ComputeHash(buffer);
            return ToString(hashBytes);
        }

        #endregion

        #region CRC32

        /// <summary>
        /// 获取字符串的CRC32
        /// </summary>
        public static string StringCRC32(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            return BytesCRC32(buffer);
        }

        /// <summary>
        /// 获取文件的CRC32
        /// </summary>
        public static string FileCRC32(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return StreamCRC32(fs);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取数据流的CRC32
        /// </summary>
        public static string StreamCRC32(Stream stream)
        {
            var hash = new CRC32Algorithm();
            var hashBytes = hash.ComputeHash(stream);
            return ToString(hashBytes);
        }

        /// <summary>
        /// 获取字节数组的CRC32
        /// </summary>
        public static string BytesCRC32(byte[] buffer)
        {
            var hash = new CRC32Algorithm();
            var hashBytes = hash.ComputeHash(buffer);
            return ToString(hashBytes);
        }

        #endregion
    }
}