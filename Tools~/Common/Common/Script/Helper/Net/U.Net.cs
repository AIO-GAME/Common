﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class AHelper
{
    /// <summary>
    /// Http 工具类
    /// </summary>
    public partial class Net
    {
        internal Net()
        {
        }

        /// <summary>
        /// Model对象转换为uri网址参数形式
        /// </summary>
        /// <param name="obj">Model对象</param>
        public static string UriParamSerialize(in object obj)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var sb = new StringBuilder();
            sb.Append("?");
            foreach (var p in properties)
            {
                var v = p.GetValue(obj, null) ?? "";

                sb.Append(p.Name);
                sb.Append("=");
                sb.Append(Uri.EscapeDataString(v.ToString())); //将字符串转换为它的转义表示形式，HttpUtility.UrlEncode是小写
                sb.Append("&");
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private static readonly byte[] CODE = new byte[] { 1, 3, 9, 3, 1, 3, 9, 3, 1 };

        internal const ushort TIMEOUT = 3000;

        /// <summary>
        /// 移除文件头
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="bufferSize">容量大小</param>
        internal static void RemoveFileHeader(Stream stream, int bufferSize = BUFFER_SIZE)
        {
            var headerSize = CODE.Length;

            var buffer = new byte[bufferSize];
            stream.Seek(headerSize, SeekOrigin.Begin);
            var size = stream.Read(buffer, 0, buffer.Length);
            var offset = 0;
            while (size > 0)
            {
                stream.Seek(offset, SeekOrigin.Begin);
                stream.Write(buffer, 0, size);
                offset += size;
                stream.Seek(offset + headerSize, SeekOrigin.Begin);
                size = stream.Read(buffer, 0, buffer.Length);
            }

            stream.SetLength(offset);
            stream.Flush();
        }

        /// <summary>
        /// 移除文件头
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="bufferSize">容量大小</param>
        internal static async Task RemoveFileHeaderAsync(Stream stream, int bufferSize = BUFFER_SIZE)
        {
            var headerSize = CODE.Length;

            var buffer = new byte[bufferSize];
            stream.Seek(headerSize, SeekOrigin.Begin);
            var size = await stream.ReadAsync(buffer, 0, buffer.Length);
            var offset = 0;
            while (size > 0)
            {
                stream.Seek(offset, SeekOrigin.Begin);
                await stream.WriteAsync(buffer, 0, size);
                offset += size;
                stream.Seek(offset + headerSize, SeekOrigin.Begin);
                size = await stream.ReadAsync(buffer, 0, buffer.Length);
            }

            stream.SetLength(offset);
            await stream.FlushAsync();
        }

        internal static FileStream AddFileHeader(string localPath, string remotePath, bool isOverWrite = false)
        {
            FileStream outputStream;
            if (File.Exists(localPath))
            {
                outputStream = new FileStream(localPath, FileMode.Open);
                var header = new byte[CODE.Length];
                _ = outputStream.Read(header, 0, header.Length);
                var resume = !CODE.Where((t, i) => t != header[i]).Any();
                if (resume) return outputStream; // 断点续传

                outputStream.Seek(0, SeekOrigin.Begin);
                using var md5 = System.Security.Cryptography.MD5.Create(); // 判断MD5 是否一致
                var localMD5 = BitConverter.ToString(md5.ComputeHash(outputStream));
                var remoteMD5 = HTTPGetMD5(remotePath);

                if (localMD5 == remoteMD5)
                {
                    outputStream.Close();
                    return null;
                }

                if (isOverWrite) outputStream.Write(CODE, 0, CODE.Length); // 清空数据 准备覆盖
                else // 保留数据
                {
                    outputStream.Close();
                    Console.WriteLine($"HTTP Download : Target File Already Exists {localPath}");
                    return null;
                }
            }
            else
            {
                outputStream = new FileStream(localPath, FileMode.Create);
                outputStream.Write(CODE, 0, CODE.Length);
            }

            return outputStream;
        }

        internal static async Task<FileStream> AddFileHeaderAsync(
            string localPath,
            string remotePath,
            bool isOverWrite = false)
        {
            FileStream outputStream;
            if (File.Exists(localPath))
            {
                outputStream = new FileStream(localPath, FileMode.Open);
                var header = new byte[CODE.Length];
                _ = await outputStream.ReadAsync(header, 0, header.Length);
                var resume = !CODE.Where((t, i) => t != header[i]).Any();
                if (resume) return outputStream;

                using var md5 = System.Security.Cryptography.MD5.Create(); // 判断MD5 是否一致
                outputStream.Seek(0, SeekOrigin.Begin);
                var localMD5 = BitConverter.ToString(md5.ComputeHash(outputStream)).Replace("-", "").ToLower();
                var remoteMD5 = await HTTPGetMD5Async(remotePath);

                if (localMD5 == remoteMD5)
                {
                    outputStream.Close();
                    return null;
                }

                if (isOverWrite)
                {
                    outputStream.Seek(0, SeekOrigin.Begin); // 清空数据 准备覆盖
                    await outputStream.WriteAsync(CODE, 0, CODE.Length);
                }
                else // 保留数据
                {
                    outputStream.Close();
                    Console.WriteLine($"HTTP Download : Target File Already Exists {localPath}");
                    return null;
                }
            }
            else
            {
                outputStream = new FileStream(localPath, FileMode.Create);
                await outputStream.WriteAsync(CODE, 0, CODE.Length);
            }

            return outputStream;
        }
    }
}