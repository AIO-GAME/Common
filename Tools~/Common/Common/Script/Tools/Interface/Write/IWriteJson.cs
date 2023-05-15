using System.Text;

using Newtonsoft.Json;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteJson
    {
        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="settings">json压缩设置</param>
        /// <param name="encoding">编码</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJson<T>(in T value, in JsonSerializerSettings settings = null, in Encoding encoding = null, in bool reverse = false);

        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="settings">json压缩设置</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJsonUTF8<T>(in T value, in JsonSerializerSettings settings = null, in bool reverse = false);

        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="settings">json压缩设置</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJsonASCII<T>(in T value, in JsonSerializerSettings settings = null, in bool reverse = false);

        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="settings">json压缩设置</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJsonUnicode<T>(in T value, in JsonSerializerSettings settings = null, in bool reverse = false);
    }
}