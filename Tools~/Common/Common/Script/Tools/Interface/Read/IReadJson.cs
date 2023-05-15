using System.Text;

using Newtonsoft.Json;

namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadJson
    {
        /// <summary>
        /// 读取Json数据
        /// </summary>
        /// <param name="settings">反序列化设置</param>
        /// <param name="encoding">字符串编码</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJson<T>(in JsonSerializerSettings settings = null, in Encoding encoding = null, in bool reverse = false);

        /// <summary>
        /// 读取Json数据
        /// </summary>
        /// <param name="settings">反序列化设置</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJsonUTF8<T>(in JsonSerializerSettings settings = null, in bool reverse = false);

        /// <summary>
        /// 读取Json数据
        /// </summary>
        /// <param name="settings">反序列化设置</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJsonASCII<T>(in JsonSerializerSettings settings = null, in bool reverse = false);

        /// <summary>
        /// 读取Json数据
        /// </summary>
        /// <param name="settings">反序列化设置</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJsonUnicode<T>(in JsonSerializerSettings settings = null, in bool reverse = false);
    }
}