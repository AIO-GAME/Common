using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class UtilsGen
{
    public partial class IO
    {
        /// <summary>
        /// 异步 加载 Byte Array
        /// </summary>
        /// <param name="path">路径</param>
        public static async Task<byte[]> ReadByteArrayAsync(string path)
        {
            return await ReadAsync(path);
        }

        /// <summary>
        /// 异步 写入数据
        /// </summary>
        public static async Task<bool> WriteByteArrayAsync(
            string path,
            byte[] bytes,
            bool coded = false,
            bool concat = false)
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
            int offset,
            int length,
            bool concat,
            int bufferSize = 4096)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !ExistsFolder(dir))
                Directory.CreateDirectory(dir);

            var mode = concat ? FileMode.Append : FileMode.Create;
            using (var fs = new FileStream(path, mode, FileAccess.Write, FileShare.None, bufferSize, true))
            {
                await fs.WriteAsync(bytes, offset, length);
                return true;
            }
        }

        /// <summary>
        /// 异步读取
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="bufferSize">缓冲区大小</param>
        public static async Task<byte[]> ReadAsync(string path, int bufferSize = 4096)
        {
            if (!ExistsFile(path)) return Array.Empty<byte>();
            try
            {
                using (var fsSource = new FileStream(path,
                           FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize, true))
                {
                    var length = (int)fsSource.Length;
                    var buffer = new byte[length];
                    var offset = 0;

                    while (offset < length)
                    {
                        var count = System.Math.Min(bufferSize, length - offset);
                        var n = await fsSource.ReadAsync(buffer, offset, count);
                        if (n == 0) break; // 到达文件末尾
                        offset += n;
                    }

                    if (length >= 3 && buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                    {
                        var copyLength = buffer.Length - 3;
                        var dataNew = new byte[copyLength];
                        Buffer.BlockCopy(buffer, 3, dataNew, 0, copyLength);
                        return dataNew;
                    }

                    return buffer;
                }
            }
            catch (Exception ioEx)
            {
                Console.WriteLine(ioEx);
            }

            return Array.Empty<byte>();
        }
    }
}