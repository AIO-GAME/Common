#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteInt64
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