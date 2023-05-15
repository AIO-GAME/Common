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
        void WriteByte(in byte value);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="reverse"></param>
        void WriteByteArray(in ICollection<byte> value, in bool reverse = false);
    }
}