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
        void WriteInt64(in long value, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt64Array(in ICollection<long> value, in bool reverse = false);
    }
}