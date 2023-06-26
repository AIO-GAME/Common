using System;
using System.IO;
using System.Security.Cryptography;

public partial class UtilsGen
{
    /// <summary>
    /// 计算MD5
    /// </summary>
    public static partial class MD5
    {
        /// <summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="bufferSize">自定义缓冲区大小16K</param>
        /// <returns>MD5Hash</returns>
        public static string GetMD5(Stream stream, in long bufferSize = 1024 * 16)
        {
            return stream == null ? null : GetMD5ByHashAlgorithm(stream, bufferSize);
        }

        /// <summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bufferSize">自定义缓冲区大小16K</param>
        public static string GetMD5ByHashAlgorithm(Stream stream, in long bufferSize = 1024 * 16)
        {
            var buffer = new byte[bufferSize];
            using (var inputStream = stream)
            {
                var hashAlgorithm = new MD5CryptoServiceProvider();
                int readLength; //每次读取长度
                var output = new byte[bufferSize];
                while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //计算MD5
                    hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
                }

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                var md5 = BitConverter.ToString(hashAlgorithm.Hash);
                hashAlgorithm.Clear();
                inputStream.Close();
                hashAlgorithm.Dispose();
                inputStream.Dispose();
                md5 = md5.Replace("-", "").ToLower();
                return md5;
            }
        }
    }
}