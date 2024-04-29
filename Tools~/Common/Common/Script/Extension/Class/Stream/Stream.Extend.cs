#region

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    /// <summary>
    ///     nameof(StreamExtend)
    /// </summary>
    public static class StreamExtend
    {
        /// <summary>
        ///     缓冲区大小
        /// </summary>
        public const int BUFFER_SIZE = 1024 * 1024;

        /// <summary>
        ///     获取MD5
        /// </summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>MD5</returns>
        public static string GetMD5(this Stream stream, long bufferSize = BUFFER_SIZE)
        {
            using var hashAlgorithm = new MD5CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = stream.Read(buffer, 0, buffer.Length)) > 0) hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(hashAlgorithm.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取MD5
        /// </summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>MD5</returns>
        public static async Task<string> GetMD5Async(this Stream       stream,
                                                     long              bufferSize        = BUFFER_SIZE,
                                                     CancellationToken cancellationToken = default)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default) cancellationToken = CancellationToken.None;
            using var crypt = new MD5CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取SHA1
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>SHA1</returns>
        public static async Task<string> GetSHA1Async(this Stream       stream,
                                                      long              bufferSize        = BUFFER_SIZE,
                                                      CancellationToken cancellationToken = default)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default) cancellationToken = CancellationToken.None;
            using var crypt = new SHA1CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取SHA1
        /// </summary>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="stream">流数据</param>
        /// <returns>SHA1</returns>
        public static string GetSHA1(this Stream stream, long bufferSize = BUFFER_SIZE)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            using var crypt = new SHA1CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = stream.Read(buffer, 0, buffer.Length)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }


        /// <summary>
        ///     获取SHA256
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>SHA1</returns>
        public static async Task<string> GetSHA256Async(this Stream       stream,
                                                        long              bufferSize        = BUFFER_SIZE,
                                                        CancellationToken cancellationToken = default)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default) cancellationToken = CancellationToken.None;
            using var crypt = new SHA256CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取SHA256
        /// </summary>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="stream">流数据</param>
        /// <returns>SHA1</returns>
        public static string GetSHA256(this Stream stream, long bufferSize = BUFFER_SIZE)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            using var crypt = new SHA256CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = stream.Read(buffer, 0, buffer.Length)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取SHA384
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>SHA1</returns>
        public static async Task<string> GetSHA384Async(this Stream       stream,
                                                        long              bufferSize        = BUFFER_SIZE,
                                                        CancellationToken cancellationToken = default)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default) cancellationToken = CancellationToken.None;
            using var crypt = new SHA384CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取SHA384
        /// </summary>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="stream">流数据</param>
        /// <returns>SHA1</returns>
        public static string GetSHA384(this Stream stream, long bufferSize = BUFFER_SIZE)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            using var crypt = new SHA384CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = stream.Read(buffer, 0, buffer.Length)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取SHA512
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>SHA1</returns>
        public static async Task<string> GetSHA512Async(this Stream       stream,
                                                        long              bufferSize        = BUFFER_SIZE,
                                                        CancellationToken cancellationToken = default)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default) cancellationToken = CancellationToken.None;
            using var crypt = new SHA512CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        ///     获取SHA512
        /// </summary>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="stream">流数据</param>
        /// <returns>SHA1</returns>
        public static string GetSHA512(this Stream stream, long bufferSize = BUFFER_SIZE)
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            using var crypt = new SHA512CryptoServiceProvider();
            {
                int readLength; //每次读取长度
                var buffer = new byte[bufferSize];
                var output = new byte[bufferSize]; //计算MD5
                while ((readLength = stream.Read(buffer, 0, buffer.Length)) > 0) crypt.TransformBlock(buffer, 0, readLength, output, 0);

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                crypt.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
            }
        }
    }
}