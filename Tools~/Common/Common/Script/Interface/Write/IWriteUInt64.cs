#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteUInt64
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