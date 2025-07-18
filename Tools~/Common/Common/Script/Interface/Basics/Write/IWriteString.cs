#region

using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入数据 <see cref="string"/>
    /// </summary>
    public interface IWriteString
    {
        /// <summary>
        /// 编码
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// 写入 <see cref="string"/>
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteString(string value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="string"/>
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteString(StringBuilder value, bool reverse = false);
    }
}