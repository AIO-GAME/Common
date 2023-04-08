using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteInt16
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt16(in short value, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt16Array(in ICollection<short> value, in bool reverse = false);
    }
}