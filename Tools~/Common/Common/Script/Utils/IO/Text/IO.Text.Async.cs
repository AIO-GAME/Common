using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public partial class UtilsGen
{
    public partial class IO
    {
        /// <summary>
        /// 异步 将字符串按照指定编码写入文件,是否追加到文件尾
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="charset">保存文本格式</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteTextAsync(
            string path,
            string text,
            string charset,
            bool concat = false)
        {
            var b = Encoding.GetEncoding(charset).GetBytes(text);
            return await WriteAsync(path, b, 0, b.Length, concat);
        }

        /// <summary>
        /// 异步 按照指定编码读取文本文件
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<string> ReadTextAsync(
            string path,
            string charset)
        {
            return Encoding.GetEncoding(charset).GetString(await ReadFileAsync(path));
        }
    }
}
