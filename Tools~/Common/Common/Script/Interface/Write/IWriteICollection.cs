#region

using System.Collections.Generic;

#endregion

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
        void WriteCollection<T>(ICollection<T> value);
    }
}