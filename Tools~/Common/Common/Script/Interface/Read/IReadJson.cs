#region

using System.Text;

#endregion

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
        /// <param name="encoding">字符串编码</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJson<T>(Encoding encoding = null, bool reverse = false);

        /// <summary>
        /// 读取Json数据
        /// </summary>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJsonUTF8<T>(bool reverse = false);

        /// <summary>
        /// 读取Json数据
        /// </summary>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJsonASCII<T>(bool reverse = false);

        /// <summary>
        /// 读取Json数据
        /// </summary>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadJsonUnicode<T>(bool reverse = false);
    }
}