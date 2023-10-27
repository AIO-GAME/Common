using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 异步 按照UTF-8读取文本文件
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<string> ReadUTF8Async(string path)
        {
            return await ReadTextAsync(path, "utf-8");
        }

        /// <summary>
        /// 异步 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteUTF8Async(string path, string text, bool concat = false)
        {
            return await WriteTextAsync(path, text, "utf-8", concat);
        }

        /// <summary>
        /// 异步 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteUTF8Async(string path, StringBuilder text, bool concat = false)
        {
            return await WriteTextAsync(path, text.ToString(), "utf-8", concat);
        }
    }
}