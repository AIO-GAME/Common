using System.Text;

namespace AIO
{
    /// <summary>
    /// 写入数据 String
    /// </summary>
    public partial interface IWriteString
    {
        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="encoding">编码</param>
        /// <param name="reverse">是否反转</param>
        void WriteString(in string value, in Encoding encoding = null, in bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="encoding">编码</param>
        /// <param name="reverse">是否反转</param>
        void WriteString(in StringBuilder value, in Encoding encoding = null, in bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUTF8(in string value, in bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUTF8(in StringBuilder value, in bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringASCII(in string value, in bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringASCII(in StringBuilder value, in bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUnicode(in string value, in bool reverse = false);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteStringUnicode(in StringBuilder value, in bool reverse = false);
    }
}