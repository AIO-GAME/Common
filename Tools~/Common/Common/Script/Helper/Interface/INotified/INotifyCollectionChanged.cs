#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 通知集合 改变事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotifyCollectionChanged<T>
    {
        /// <summary>
        /// 添加事件
        /// </summary>
        event Action<T> ItemAdded;

        /// <summary>
        /// 移除事件
        /// </summary>
        event Action<T> ItemRemoved;

        /// <summary>
        /// 修改事件
        /// </summary>
        event Action CollectionChanged;
    }
}