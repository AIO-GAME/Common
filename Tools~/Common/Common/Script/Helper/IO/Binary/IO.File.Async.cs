using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 使用异步 从文件中读取数据
        /// </summary>
        public static Task<byte[]> ReadFileAsync(string Path)
        {
            return ReadAsync(Path);
        }

        /// <summary>
        /// 将数据写入文件,是否追加到文件尾 默认覆盖文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Bytes">内容</param>
        /// <param name="Concat">true:拼接 | false:覆盖</param>
        public static Task<bool> WriteFileAsync(
            string Path,
            byte[] Bytes,
            bool Concat = false)
        {
            return WriteAsync(Path, Bytes, 0, Bytes.Length, Concat);
        }
    }
}
