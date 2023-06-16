using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteByte
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        /// <param name="value"></param>
        void WriteByte(byte value);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="reverse"></param>
        void WriteByteArray(ICollection<byte> value, bool reverse = false);
    }
}