using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteUInt16
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteUInt16(in ushort value, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteUInt16Array(in ICollection<ushort> value, in bool reverse = false);
    }
}