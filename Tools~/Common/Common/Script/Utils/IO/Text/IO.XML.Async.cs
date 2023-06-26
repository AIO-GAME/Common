using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class UtilsGen
{
    public partial class IO
    {
        /// <summary>
        /// 读取 XML 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadXmlAsync<T>(string path, string charset = "utf-8")
        {
            var Content = await ReadTextAsync(path, charset);
            return Xml.Deserialize<T>(Content);
        }

        /// <summary>
        /// 读取 XML 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadXmlUTF8Async<T>(string path)
        {
            var Content = await ReadUTF8Async(path);
            return Xml.Deserialize<T>(Content);
        }

        /// <summary>
        /// 写入 XML 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteXmlAsync<T>(string path, T value, string charset = "utf-8")
        {
            if (value == null) return false;
            return await WriteTextAsync(path, Xml.Serialize(value), charset);
        }

        /// <summary>
        /// 写入 XML 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteXmlUTF8Async<T>(string path, T text)
        {
            if (text == null) return false;
            return await WriteUTF8Async(path, Xml.Serialize(text));
        }
    }
}
