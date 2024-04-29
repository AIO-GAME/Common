#region

using System.Collections.Generic;

#endregion

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
        void WriteList<T>(IList<T> value);
    }
}