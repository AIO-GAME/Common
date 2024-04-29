#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteUInt32
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteUInt32(uint value, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteUInt32Array(ICollection<uint> value, bool reverse = false);
    }
}