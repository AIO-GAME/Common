using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入字典
    /// </summary>
    public interface IWriteICollection
    {
        /// <summary>
        /// 写入字典
        /// </summary>
        void WriteCollection<T>(in ICollection<T> value);
    }
}