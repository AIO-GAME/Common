using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class UtilsGen
{
    public partial class IO
    {
        /// <summary>
        /// 异步 按照UTF-8读取文本文件
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<string> ReadUnicodeAsync(string path)
        {
            return await ReadTextAsync(path, "unicode");
        }

        /// <summary>
        /// 异步 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteUnicodeAsync(string path, string text, bool concat = false)
        {
            return await WriteTextAsync(path, text, "unicode", concat);
        }
    }
}
