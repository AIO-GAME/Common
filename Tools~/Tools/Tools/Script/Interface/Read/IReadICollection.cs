using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 读取字典
    /// </summary>
    public interface IReadICollection
    {
        /// <summary>
        /// 读取字典
        /// </summary>
        void ReadCollection<T>(ICollection<T> value);
    }
}