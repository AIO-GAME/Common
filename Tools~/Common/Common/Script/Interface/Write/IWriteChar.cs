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
        void WriteChar(char value, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteCharArray(ICollection<char> value, bool reverse = false);
    }
}