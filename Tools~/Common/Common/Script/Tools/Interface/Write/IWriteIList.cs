using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入字典
    /// </summary>
    public interface IWriteIList
    {
        /// <summary>
        /// 写入字典
        /// </summary>
        void WriteList<T>(in IList<T> value);
    }
}