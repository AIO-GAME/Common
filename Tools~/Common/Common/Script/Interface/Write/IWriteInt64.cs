using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteInt64
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt64(long value, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt64Array(ICollection<long> value, bool reverse = false);
    }
}