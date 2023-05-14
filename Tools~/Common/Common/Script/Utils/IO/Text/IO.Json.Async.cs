using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class Utils
{
    public partial class IO
    {
        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadJsonAsync<T>(
            string path,
            string charset = "utf-8")
        {
            var Content = await ReadTextAsync(path, charset);
            return Json.Deserialize<T>(Content);
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadJsonUTF8Async<T>(string path)
        {
            var Content = await ReadUTF8Async(path);
            return Json.Deserialize<T>(Content);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteJsonAsync<T>(
            string path, T value,
            bool concat = false,
            string charset = "utf-8")
        {
            if (value == null) return false;
            return await WriteTextAsync(path, Json.Serialize(value), charset, concat);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteJsonUTF8Async<T>(
            string path,
            T text,
            bool concat = false)
        {
            if (text == null) return false;
            return await WriteUTF8Async(path, Json.Serialize(text), concat);
        }
    }
}
