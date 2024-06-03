#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 读取字典
    /// </summary>
    public interface IReadIList
    {
        /// <summary>
        /// 读取字典
        /// </summary>
        void ReadList<T>(IList<T> value);
    }
}