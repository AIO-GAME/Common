using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteInt32
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt32(in int value, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt32Array(in ICollection<int> value, in bool reverse = false);
    }
}