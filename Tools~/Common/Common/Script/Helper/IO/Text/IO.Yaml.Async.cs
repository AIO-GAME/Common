using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 读取 Yaml 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadYamlAsync<T>(string path, string charset = "utf-8")
        {
            var Content = await ReadTextAsync(path, charset);
            return Yaml.Deserialize<T>(Content);
        }

        /// <summary>
        /// 读取 Yaml 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadYamlUTF8Async<T>(string path)
        {
            var Content = await ReadUTF8Async(path);
            return Yaml.Deserialize<T>(Content);
        }

        /// <summary>
        /// 写入 Yaml 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteYamlAsync<T>(string path, T value, string charset = "utf-8")
        {
            if (value == null) return false;
            return await WriteTextAsync(path, Yaml.Serialize(value), charset);
        }

        /// <summary>
        /// 写入 Yaml 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<bool> WriteYamlUTF8Async<T>(string path, T text)
        {
            if (text == null) return false;
            return await WriteUTF8Async(path, Yaml.Serialize(text));
        }
    }
}