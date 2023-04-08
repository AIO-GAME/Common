using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入数据 SByte Array
    /// </summary>
    public partial interface IWriteSByte
    {
        /// <summary>
        /// 写入Sbyte数组
        /// </summary>
        /// <param name="value">输入源</param>
        void WriteSByte(in sbyte value);

        /// <summary>
        /// 写入Sbyte数组
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="reverse">是否反转</param>
        void WriteSByteArray(in ICollection<sbyte> value, in bool reverse = false);
    }
}