using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteChar
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteChar(in char value, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteCharArray(in ICollection<char> value, in bool reverse = false);
    }
}