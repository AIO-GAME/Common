#region

using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入数据 String
    /// </summary>
    public interface IWriteString
    {
        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="encoding">编码</param>
        /// <param name="reverse">是否反转</param>
        void WriteString(string value, Encoding encoding = null, bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="encoding">编码</param>
        /// <param name="reverse">是否反转</param>
        void WriteString(StringBuilder value, Encoding encoding = null, bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUTF8(string value, bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUTF8(StringBuilder value, bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringASCII(string value, bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringASCII(StringBuilder value, bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUnicode(string value, bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUnicode(StringBuilder value, bool reverse = false);
    }
}