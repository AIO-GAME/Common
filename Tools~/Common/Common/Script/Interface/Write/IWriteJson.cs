#region

using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteJson
    {
        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="encoding">编码</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJson<T>(T value, Encoding encoding = null, bool reverse = false);

        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJsonUTF8<T>(T value, bool reverse = false);

        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJsonASCII<T>(T value, bool reverse = false);

        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteJsonUnicode<T>(T value, bool reverse = false);
    }
}