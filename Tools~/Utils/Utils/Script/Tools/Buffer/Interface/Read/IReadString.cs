using System.Text;

namespace AIO
{
    /// <summary>
    /// 读取 数据 String
    /// </summary>
    public partial interface IReadString
    {
        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>字符串</returns>
        string ReadStringUTF8(in bool reverse = false);

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>字符串</returns>
        string ReadStringASCII(in bool reverse = false);

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>字符串</returns>
        string ReadStringUnicode(in bool reverse = false);

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <param name="reverse">是否反转</param>
        /// <returns>字符串</returns>
        string ReadString(in Encoding encoding = null, in bool reverse = false);
    }
}