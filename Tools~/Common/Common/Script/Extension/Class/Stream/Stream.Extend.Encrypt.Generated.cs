
using System;
using System.Buffers;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace AIO
{
    partial class StreamExtend
    {    
        #region MD5

        /// <summary>
        ///     获取 MD5
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>MD5 value</returns>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        public static string GetMD5(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new MD5CryptoServiceProvider();
            while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);
       
            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        ///     获取MD5
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        /// <returns>MD5</returns>
        public static async Task<string> GetMD5Async(
            this Stream       stream,
            int               bufferSize        = BUFFER_SIZE,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default(CancellationToken)) cancellationToken = CancellationToken.None;

            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new MD5CryptoServiceProvider();
            while ((length = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);

            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算文件的sha256 摘要值
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>sha256摘要字符串</returns>
        public static string GetMD5Digest(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            using var fs  = new BufferedStream(stream, BUFFER_SIZE);
            using var sha = MD5.Create();
            return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", string.Empty);
        }

        #endregion

        #region SHA1

        /// <summary>
        ///     获取 SHA1
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>SHA1 value</returns>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA1
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA1值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        public static string GetSHA1(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA1CryptoServiceProvider();
            while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);
       
            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        ///     获取SHA1
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA1
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA1值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        /// <returns>SHA1</returns>
        public static async Task<string> GetSHA1Async(
            this Stream       stream,
            int               bufferSize        = BUFFER_SIZE,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default(CancellationToken)) cancellationToken = CancellationToken.None;

            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA1CryptoServiceProvider();
            while ((length = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);

            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算文件的sha256 摘要值
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>sha256摘要字符串</returns>
        public static string GetSHA1Digest(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            using var fs  = new BufferedStream(stream, BUFFER_SIZE);
            using var sha = SHA1.Create();
            return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", string.Empty);
        }

        #endregion

        #region SHA256

        /// <summary>
        ///     获取 SHA256
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>SHA256 value</returns>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA256
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA256值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        public static string GetSHA256(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA256CryptoServiceProvider();
            while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);
       
            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        ///     获取SHA256
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA256
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA256值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        /// <returns>SHA256</returns>
        public static async Task<string> GetSHA256Async(
            this Stream       stream,
            int               bufferSize        = BUFFER_SIZE,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default(CancellationToken)) cancellationToken = CancellationToken.None;

            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA256CryptoServiceProvider();
            while ((length = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);

            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算文件的sha256 摘要值
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>sha256摘要字符串</returns>
        public static string GetSHA256Digest(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            using var fs  = new BufferedStream(stream, BUFFER_SIZE);
            using var sha = SHA256.Create();
            return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", string.Empty);
        }

        #endregion

        #region SHA384

        /// <summary>
        ///     获取 SHA384
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>SHA384 value</returns>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA384
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA384值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        public static string GetSHA384(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA384CryptoServiceProvider();
            while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);
       
            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        ///     获取SHA384
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA384
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA384值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        /// <returns>SHA384</returns>
        public static async Task<string> GetSHA384Async(
            this Stream       stream,
            int               bufferSize        = BUFFER_SIZE,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default(CancellationToken)) cancellationToken = CancellationToken.None;

            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA384CryptoServiceProvider();
            while ((length = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);

            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算文件的sha256 摘要值
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>sha256摘要字符串</returns>
        public static string GetSHA384Digest(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            using var fs  = new BufferedStream(stream, BUFFER_SIZE);
            using var sha = SHA384.Create();
            return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", string.Empty);
        }

        #endregion

        #region SHA512

        /// <summary>
        ///     获取 SHA512
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>SHA512 value</returns>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA512
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA512值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        public static string GetSHA512(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA512CryptoServiceProvider();
            while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);
       
            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        ///     获取SHA512
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="bufferSize">容器大小</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <remarks>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得SHA512
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算SHA512值
        /// 可自定义缓冲区大小，计算速度较快
        /// </remarks>
        /// <returns>SHA512</returns>
        public static async Task<string> GetSHA512Async(
            this Stream       stream,
            int               bufferSize        = BUFFER_SIZE,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!stream.CanRead) throw new NotSupportedException("流不支持读取操作");
            if (cancellationToken == default(CancellationToken)) cancellationToken = CancellationToken.None;

            var buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
            var output = ArrayPool<byte>.Shared.Rent(bufferSize);
            int length;

            using var crypt = new SHA512CryptoServiceProvider();
            while ((length = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                crypt.TransformBlock(buffer, 0, length, output, 0);

            crypt.TransformFinalBlock(buffer, 0, 0); //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            ArrayPool<byte>.Shared.Return(output, true);
            ArrayPool<byte>.Shared.Return(buffer, true);
            return BitConverter.ToString(crypt.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算文件的sha256 摘要值
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="bufferSize">容器大小</param>
        /// <returns>sha256摘要字符串</returns>
        public static string GetSHA512Digest(this Stream stream, int bufferSize = BUFFER_SIZE)
        {
            using var fs  = new BufferedStream(stream, BUFFER_SIZE);
            using var sha = SHA512.Create();
            return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", string.Empty);
        }

        #endregion

    }
}