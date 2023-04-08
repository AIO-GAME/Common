using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteBool
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteBool(in bool b);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteBoolArray(in ICollection<bool> value, in bool reverse = false);
    }
}