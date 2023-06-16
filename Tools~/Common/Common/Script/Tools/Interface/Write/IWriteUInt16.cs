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
        void WriteUInt16(ushort value, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteUInt16Array(ICollection<ushort> value, bool reverse = false);
    }
}