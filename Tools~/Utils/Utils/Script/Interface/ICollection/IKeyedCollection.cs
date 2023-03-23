namespace AIO
{
    using System.Collections.Generic;

    /// <summary>
    /// 键迭代器
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public interface IKeyedCollection<TKey, TItem> : ICollection<TItem>
    {
        /// <summary>
        /// 获取值
        /// </summary>
        TItem this[in TKey key] { get; }

        /// <summary>
        /// 获取值
        /// </summary>
        TItem this[in int index] { get; }


        /// <summary>
        /// 尝试获取值
        /// </summary>
        bool TryGetValue(in TKey key, out TItem value);

        /// <summary>
        /// 存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(in TKey key);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">值</param>
        /// <returns>返回值</returns>
        bool Remove(in TKey key);
    }
}
