#region

using System.Collections.Concurrent;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Concurrent.ConcurrentBag{T}"/> 对象池
        /// </summary>
        internal class ConcurrentBag<T> : SingletonArray<ConcurrentBag<T>, System.Collections.Concurrent.ConcurrentBag<T>>
        {
            protected override System.Collections.Concurrent.ConcurrentBag<T> Create() => new System.Collections.Concurrent.ConcurrentBag<T>();

            protected override void Recycle(System.Collections.Concurrent.ConcurrentBag<T> array) => array.TryTake(out _);

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.TryTake(out _);
            }
        }
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentBag<T> ToConcurrentBagPooled<T>(this IEnumerable<T> source)
        {
            var Queue = Pooling.ConcurrentBag<T>.Alloc();
            foreach (var item in source) Queue.Add(item);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentBag<T> ToConcurrentBagPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var Queue = Pooling.ConcurrentBag<T>.Alloc();
            foreach (var item in source) Queue.Add(item.Key);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentBag<T> ToConcurrentBagPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var Queue = Pooling.ConcurrentBag<T>.Alloc();
            foreach (var item in source) Queue.Add(item.Value);
            return Queue;
        }
    }
}