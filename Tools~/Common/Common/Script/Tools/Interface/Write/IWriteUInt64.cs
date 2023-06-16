using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteUInt64
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteUInt64(ulong value, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteUInt64Array(ICollection<ulong> value, bool reverse = false);
    }
}