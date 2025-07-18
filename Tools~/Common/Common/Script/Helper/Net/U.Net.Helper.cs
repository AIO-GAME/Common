﻿#region

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: Net

        /// <summary>
        /// 网络 工具类
        /// </summary>
        public partial class Net
        {
            /// <summary>
            /// Model对象转换为uri网址参数形式
            /// </summary>
            /// <param name="obj">Model对象</param>
            public static string UriParamSerialize(in object obj)
            {
                var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var sb         = new StringBuilder();
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
                var size   = stream.Read(buffer, 0, buffer.Length);
                var offset = 0;
                while (size > 0)
                {
                    stream.Seek(offset, SeekOrigin.Begin);
                    stream.Write(buffer, 0, size);
                    offset += size;
                    stream.Seek(offset + headerSize, SeekOrigin.Begin);
                    size = stream.Read(buffer, 0, buffer.Length);
                }

                stream.SetLength(stream.Length - headerSize);
                stream.Flush();
            }

            /// <summary>
            /// 移除文件头
            /// </summary>
            /// <param name="stream">文件流</param>
            /// <param name="bufferSize">容量大小</param>
            /// <param name="cancellationToken">取消令牌</param>
            internal static async Task RemoveFileHeaderAsync(Stream            stream,
                                                             int               bufferSize        = BUFFER_SIZE,
                                                             CancellationToken cancellationToken = default)
            {
                if (cancellationToken == default) cancellationToken = CancellationToken.None;
                var headerSize                                      = CODE.Length;

                var buffer = new byte[bufferSize];
                stream.Seek(headerSize, SeekOrigin.Begin);
                var offset = 0;
                var size   = await stream.ReadAsync(buffer, 0, bufferSize, cancellationToken);
                while (size > 0)
                {
                    stream.Seek(offset, SeekOrigin.Begin);
                    await stream.WriteAsync(buffer, 0, size, cancellationToken);
                    offset += size;
                    stream.Seek(offset + headerSize, SeekOrigin.Begin);
                    size = await stream.ReadAsync(buffer, 0, bufferSize, cancellationToken);
                }

                stream.SetLength(stream.Length - headerSize);
                await stream.FlushAsync(cancellationToken);
            }

            internal static FileStream AddFileHeader(string localPath, Func<string> remoteMD5Cb, bool isOverWrite = false)
            {
                var parent = localPath.Substring(0, localPath.LastIndexOf('\\'));
                if (!Directory.Exists(parent)) Directory.CreateDirectory(parent);

                FileStream outputStream;
                if (File.Exists(localPath))
                {
                    outputStream = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Inheritable);
                    var header = new byte[CODE.Length];
                    _ = outputStream.Read(header, 0, header.Length);
                    var resume = !CODE.Where((t, i) => t != header[i]).Any();
                    if (resume)
                    {
                        outputStream.Seek(outputStream.Length, SeekOrigin.Begin);
                        return outputStream; // 断点续传
                    }

                    outputStream.Seek(0, SeekOrigin.Begin);
                    if (outputStream.GetMD5() == remoteMD5Cb.Invoke()) return outputStream;

                    if (isOverWrite)
                    {
                        outputStream.Seek(0, SeekOrigin.Begin);
                        outputStream.Write(CODE, 0, CODE.Length); // 清空数据 准备覆盖
                        return outputStream;
                    }

                    CS.WriteLine($"HTTP Download : Target File Already Exists {localPath}");
                    return null;
                }

                outputStream = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Inheritable);
                outputStream.Write(CODE, 0, CODE.Length);
                return outputStream;
            }

            internal static async Task<FileStream> AddFileHeaderAsync(
                string             localPath,
                Func<Task<string>> remoteMD5Cb,
                bool               isOverWrite       = false,
                CancellationToken  cancellationToken = default)
            {
                if (cancellationToken == default) cancellationToken = CancellationToken.None;
                localPath = localPath.Replace('/', '\\');
                var fileinfo = new FileInfo(localPath);
                var parent   = fileinfo.Directory;
                if (!parent.Exists) parent.Create();

                FileStream outputStream;
                if (fileinfo.Exists)
                {
                    outputStream = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Inheritable);
                    var header = new byte[CODE.Length];
                    _ = await outputStream.ReadAsync(header, 0, header.Length, cancellationToken);
                    var resume = !CODE.Where((t, i) => t != header[i]).Any();
                    if (resume)
                    {
                        outputStream.Seek(outputStream.Length, SeekOrigin.Begin);
                        return outputStream; // 断点续传
                    }

                    outputStream.Seek(0, SeekOrigin.Begin);
                    var localMD5  = await outputStream.GetMD5Async(cancellationToken: cancellationToken);
                    var remoteMD5 = await remoteMD5Cb.Invoke();
                    if (localMD5 == remoteMD5)
                    {
                        return outputStream;
                    }

                    if (isOverWrite)
                    {
                        outputStream.Seek(0, SeekOrigin.Begin);
                        await outputStream.WriteAsync(CODE, 0, CODE.Length, cancellationToken);
                        await outputStream.FlushAsync(cancellationToken);
                        return outputStream;
                    }

                    CS.WriteLine($"HTTP Download : Target File Already Exists {localPath}");
                    return null;
                }

                outputStream = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Inheritable);
                await outputStream.WriteAsync(CODE, 0, CODE.Length, cancellationToken);
                await outputStream.FlushAsync(cancellationToken);
                return outputStream;
            }
        }

        #endregion
    }
}