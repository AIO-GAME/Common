using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入字典
    /// </summary>
    public interface IWriteIDictionary
    {
        /// <summary>
        /// 写入字典
        /// </summary>
        void WriteDictionary<K, V>(IDictionary<K, V> value);
    }
}