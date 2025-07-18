#region

using System;
using System.IO;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 加载 Byte Array
            /// </summary>
            /// <param name="path">路径</param>
            public static byte[] ReadByteArray(string path) { return Read(path); }

            /// <summary>
            /// 写入数据
            /// </summary>
            public static bool WriteByteArray(
                string    path,
                in byte[] bytes,
                in bool   coded  = false,
                in bool   concat = false)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!coded) return Write(path, bytes, 0, bytes.Length, concat);
                for (var i = 0; i < bytes.Length; i++)
                    bytes[i] = EncodingBitByte(bytes[i]);
                return Write(path, bytes, 0, bytes.Length, concat);
            }

            /// <summary>
            /// 将指定数据从offset开始写入length长度到文件中,是否追加到文件尾
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="bytes">内容</param>
            /// <param name="offset">写入内容位置</param>
            /// <param name="length">长度</param>
            /// <param name="concat">true:拼接 | false:覆盖</param>
            /// <param name="bufferSize">缓冲区大小</param>
            public static bool Write(
                string    path,
                in byte[] bytes,
                in int    offset,
                in int    length,
                in bool   concat,
                in int    bufferSize = 4096)
            {
                var        mode = concat ? FileMode.Append : FileMode.Create;
                FileStream fs   = null;
                try
                {
                    path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                    var dir = Path.GetDirectoryName(path);
                    if (!string.IsNullOrEmpty(dir) && !ExistsDir(dir)) Directory.CreateDirectory(dir);

                    fs = new FileStream(path, mode, FileAccess.Write, FileShare.ReadWrite | FileShare.Inheritable, bufferSize, false);
                    fs.Write(bytes, offset, length);
                    fs.Flush(true);
                }
                catch (Exception e)
                {
                    throw new IOException($"Error writing to file '{path}': {e.Message}", e);
                }
                finally
                {
                    fs?.Close();
                    fs?.Dispose();
                    File.SetLastWriteTimeUtc(path, DateTime.Now);
                }

                return true;
            }

            /// <summary>
            /// 读取
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="bufferSize">缓冲区大小</param>
            public static Stream ReadStream(string path, in int bufferSize = 4096)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) return Stream.Null;
                return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Inheritable, bufferSize, false);
            }

            /// <summary>
            /// 读取
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="bufferSize">缓冲区大小</param>
            public static byte[] Read(
                string path,
                in int bufferSize = 4096)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) return Array.Empty<byte>();
                FileStream fs     = null;
                byte[]     buffer = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Inheritable, bufferSize, false);
                    var length = (int)fs.Length;
                    buffer = new byte[length];
                    var offset = 0;
                    while (offset < length)
                    {
                        var count = System.Math.Min(bufferSize, length - offset);
                        var n     = fs.ReadAsync(buffer, offset, count).GetAwaiter().GetResult();
                        if (n == 0) break; // 到达文件末尾
                        offset += n;
                    }

                    if (length >= 3 && buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                    {
                        var copyLength = buffer.Length - 3;
                        var dataNew    = new byte[copyLength];
                        System.Buffer.BlockCopy(buffer, 3, dataNew, 0, copyLength);
                        buffer = dataNew;
                    }
                }
                catch (Exception ioEx)
                {
                    throw new IOException($"Error reading file '{path}': {ioEx.Message}", ioEx);
                }
                finally
                {
                    fs?.Close();
                    fs?.Dispose();
                    File.SetLastWriteTimeUtc(path, DateTime.Now);
                }

                end: ;
                return buffer;
            }
        }

        #endregion
    }
}