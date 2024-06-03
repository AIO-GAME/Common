#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteInt16
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt16(short value, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteInt16Array(ICollection<short> value, bool reverse = false);
    }
}