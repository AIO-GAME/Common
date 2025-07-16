#region

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 异步 加载 Byte Array
            /// </summary>
            /// <param name="path">路径</param>
            public static async Task<byte[]> ReadByteArrayAsync(string path) { return await ReadAsync(path); }

            /// <summary>
            /// 异步 写入数据
            /// </summary>
            public static async Task<bool> WriteByteArrayAsync(
                string path,
                byte[] bytes,
                bool   coded  = false,
                bool   concat = false)
            {
                if (!coded) return await WriteAsync(path, bytes, 0, bytes.Length, concat);
                for (var i = 0; i < bytes.Length; i++)
                    bytes[i] = EncodingBitByte(bytes[i]);
                return await WriteAsync(path, bytes, 0, bytes.Length, concat);
            }

            /// <summary>
            /// 异步写入 将指定数据从offset开始写入length长度到文件中,是否追加到文件尾
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="bytes">内容</param>
            /// <param name="offset">写入内容位置</param>
            /// <param name="length">长度</param>
            /// <param name="concat">true:拼接 | false:覆盖</param>
            /// <param name="bufferSize">缓冲区大小</param>
            public static async Task<bool> WriteAsync(
                string path,
                byte[] bytes,
                int    offset,
                int    length,
                bool   concat,
                int    bufferSize = 4096)
            {
                FileStream fs = null;
                try
                {
                    var dir = Path.GetDirectoryName(path);
                    if (!string.IsNullOrEmpty(dir) && !ExistsDir(dir)) Directory.CreateDirectory(dir);
                    var mode = concat ? FileMode.Append : FileMode.OpenOrCreate;
                    fs = new FileStream(path, mode, FileAccess.Write, FileShare.ReadWrite | FileShare.Inheritable, bufferSize, true);
                    await fs.WriteAsync(bytes, offset, length);
                    // if (fs.CanSeek) fs.Seek(0, SeekOrigin.Begin); // 重置流位置
                    await fs.FlushAsync();
                    fs.Flush(flushToDisk: true);
                    File.SetLastWriteTimeUtc(path, DateTime.Now);
                }
                catch (Exception e)
                {
                    throw new IOException($"Error writing to file '{path}': {e.Message}", e);
                }
                finally
                {
                    fs?.Close();
                    fs?.Dispose();
                }

                return true;
            }

            /// <summary>
            /// 异步写入 将指定数据从offset开始写入length长度到文件中,是否追加到文件尾
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="stream">内容</param>
            /// <param name="concat">true:拼接 | false:覆盖</param>
            /// <param name="bufferSize">缓冲区大小</param>
            public static async Task<bool> WriteAsync(
                string path,
                Stream stream,
                bool   concat,
                int    bufferSize = 4096)
            {
                FileStream fs = null;
                try
                {
                    var dir = Path.GetDirectoryName(path);
                    if (!string.IsNullOrEmpty(dir) && !ExistsDir(dir)) Directory.CreateDirectory(dir);
                    var mode = concat ? FileMode.Append : FileMode.OpenOrCreate;
                    fs = new FileStream(path, mode, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Inheritable, bufferSize, true);
                    if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
                    await stream.CopyToAsync(fs, bufferSize);
                    await stream.FlushAsync();
                    // if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
                    await fs.FlushAsync();
                    fs.Flush(flushToDisk: true);
                    File.SetLastWriteTimeUtc(path, DateTime.Now);
                }
                catch (Exception e)
                {
                    throw new IOException($"Error writing to file '{path}': {e.Message}", e);
                }
                finally
                {
                    fs?.Close();
                    fs?.Dispose();
                }

                return true;
            }

            /// <summary>
            /// 异步读取
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="bufferSize">缓冲区大小</param>
            public static async Task<byte[]> ReadAsync(string path, int bufferSize = 4096)
            {
                if (!ExistsFile(path)) return Array.Empty<byte>();
                FileStream fs    = null;
                byte[]     datas = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Inheritable, bufferSize, true);
                    var length = (int)fs.Length;
                    var buffer = new byte[length];
                    var offset = 0;

                    while (offset < length)
                    {
                        var count = System.Math.Min(bufferSize, length - offset);
                        var n     = await fs.ReadAsync(buffer, offset, count);
                        if (n == 0) break; // 到达文件末尾
                        offset += n;
                    }

                    if (length < 3 || buffer[0] != 0xef || buffer[1] != 0xbb || buffer[2] != 0xbf)
                    {
                        datas = buffer;
                    }
                    else
                    {
                        var copyLength = buffer.Length - 3;
                        datas = new byte[copyLength];
                        System.Buffer.BlockCopy(buffer, 3, datas, 0, copyLength);
                    }

                    File.SetLastWriteTimeUtc(path, DateTime.Now);
                }
                catch (Exception ioEx)
                {
                    throw new IOException($"Error reading file '{path}': {ioEx.Message}", ioEx);
                }
                finally
                {
                    fs?.Close();
                    fs?.Dispose();
                }

                return datas;
            }
        }

        #endregion
    }
}