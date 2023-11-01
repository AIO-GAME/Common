using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 键迭代器
    /// </summary>
    public interface IKeyedCollection<in TKey, TItem> : ICollection<TItem>
    {
        /// <summary>
        /// 获取值
        /// </summary>
        TItem this[TKey key] { get; }

        /// <summary>
        /// 获取值
        /// </summary>
        TItem this[int index] { get; }

        /// <summary>
        /// 尝试获取值
        /// </summary>
        bool TryGetValue(TKey key, out TItem value);

        /// <summary>
        /// 存在
        /// </summary>
        bool Contains(TKey key);

        /// <summary>
        /// 移除
        /// </summary>
        bool Remove(TKey key);
    }
}