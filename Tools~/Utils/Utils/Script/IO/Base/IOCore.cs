/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary> IO 核心方法 </summary>
    public static partial class IOUtils
    {
        /// <summary>
        /// 将指定数据从offset开始写入length长度到文件中,是否追加到文件尾
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="bytes">内容</param>
        /// <param name="offset">写入内容位置</param>
        /// <param name="length">长度</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        public static bool Write(string @path, byte[] bytes, int offset, int length, bool concat)
        {
            var dir = Path.GetDirectoryName(@path);
            if (!DirExists(dir)) Directory.CreateDirectory(dir);
            FileMode mode = (concat) ? FileMode.Append : FileMode.Create;
            using (FileStream fs = new FileStream(@path, mode, FileAccess.Write))
            {
                if (fs.CanWrite)
                {
                    fs.Write(bytes, offset, length);
                    fs.Close();
                    return true;
                }
                else throw new IOException($"Current File Cannot Written | Path : [{@path}]");
            }
        }

        /// <summary>
        /// 异步写入 将指定数据从offset开始写入length长度到文件中,是否追加到文件尾
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="bytes">内容</param>
        /// <param name="offset">写入内容位置</param>
        /// <param name="length">长度</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        public static async Task<bool> WriteAsync(string @path, byte[] bytes, int offset, int length, bool concat)
        {
            var dir = Path.GetDirectoryName(@path);
            if (!DirExists(dir)) Directory.CreateDirectory(dir);
            FileMode mode = (concat) ? FileMode.Append : FileMode.Create;
            using (FileStream fs = new FileStream(@path, mode, FileAccess.Write))
            {
                if (fs.CanWrite)
                {
                    await fs.WriteAsync(bytes, offset, length);
                    fs.Close();
                    return true;
                }
                else throw new IOException($"Current File Cannot Written | Path : [{@path}]");
            }
        }

        /// <summary>
        /// 异步读取
        /// </summary>
        public static async Task<byte[]> ReadAsync(string path)
        {
            byte[] bytes = default;
            if (FileExists(@path))
            {
                try
                {
                    using (var fsSource = new FileStream(@path, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[fsSource.Length];
                        var numBytesToRead = fsSource.Length;
                        var numBytesRead = 0;
                        int n = 0;
                        while (numBytesToRead > 0)
                        {
                            if (numBytesToRead > int.MaxValue)
                            {//如果读取的文件大小 超出int值 则会进行多次读取
                                n = await fsSource.ReadAsync(bytes, numBytesRead, int.MaxValue);
                            }
                            else n = await fsSource.ReadAsync(bytes, numBytesRead, (int)numBytesToRead);
                            if (n == 0) break;
                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                        numBytesToRead = bytes.Length;
                    }
                }
                catch (Exception ioEx) { throw ioEx; }
            }
            return bytes;
        }

        /// <summary>
        /// 读取
        /// </summary>
        public static byte[] Read(string path)
        {
            if (FileExists(@path))
            {
                try
                {
                    using (var fsSource = new FileStream(@path, FileMode.Open, FileAccess.Read))
                    {
                        var buffer = new byte[fsSource.Length];
                        var numBytesToRead = fsSource.Length;
                        var numBytesRead = 0;
                        int n = 0;
                        while (numBytesToRead > 0)
                        {
                            if (numBytesToRead > int.MaxValue)
                            {//如果读取的文件大小 超出int值 则会进行多次读取
                                n = fsSource.Read(buffer, numBytesRead, int.MaxValue);
                            }
                            else n = fsSource.Read(buffer, numBytesRead, (int)numBytesToRead);
                            if (n == 0) break;
                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                        numBytesToRead = buffer.Length;
                        if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                        {// UTF-8 BOM
                            int copyLength = buffer.Length - 3;
                            byte[] dataNew = new byte[copyLength];
                            Buffer.BlockCopy(buffer, 3, dataNew, 0, copyLength);
                            return dataNew;
                        }
                        else return buffer;
                    }
                }
                catch (Exception ioEx) { throw ioEx; }
            }
            return EMPTY_BYTES;
        }
    }
}
