using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 读取字典
    /// </summary>
    public interface IReadIDictionary
    {
        /// <summary>
        /// 读取字典
        /// </summary>
        void ReadDictionary<K, V>(IDictionary<K, V> value);
    }
}