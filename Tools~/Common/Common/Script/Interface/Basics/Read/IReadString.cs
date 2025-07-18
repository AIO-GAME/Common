#region

using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 读取 数据 String
    /// </summary>
    public partial interface IReadString
    {
        /// <summary>
        /// 编码
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>字符串</returns>
        string ReadString(bool reverse = false);
    }
}