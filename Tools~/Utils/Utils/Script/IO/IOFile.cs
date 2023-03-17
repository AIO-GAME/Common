/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System.Threading.Tasks;

    /// <summary>
    /// IO 读取
    /// </summary>
    public static partial class IOUtils
    {
        /// <summary>
        /// 使用异步 从文件中读取数据
        /// </summary>
        public static async Task<byte[]> ReadFileAsync(string Path)
        {
            return await ReadAsync(Path);
        }

        /// <summary>
        /// 从文件中读取数据
        /// </summary>
        public static byte[] ReadFile(string Path)
        {
            return Read(Path);
        }

        /// <summary>
        /// 将数据写入文件,是否追加到文件尾 默认覆盖文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Bytes">内容</param>
        /// <param name="Concat">true:拼接 | false:覆盖</param>
        public static async Task<bool> WriteFileAsync(string Path, byte[] Bytes, bool Concat = false)
        {
            return await WriteAsync(Path, Bytes, 0, Bytes.Length, Concat);
        }


        /// <summary>
        /// 将数据写入文件,是否追加到文件尾 默认覆盖文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Bytes">内容</param>
        /// <param name="Concat">true:拼接 | false:覆盖</param>
        public static bool WriteFile(string Path, byte[] Bytes, bool Concat = false)
        {
            return Write(Path, Bytes, 0, Bytes.Length, Concat);
        }
    }
}
