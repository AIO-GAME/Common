#region

using System.Collections.Concurrent;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/> 对象池
        /// </summary>
        internal class ConcurrentQueue<T> : SingletonArray<ConcurrentQueue<T>, System.Collections.Concurrent.ConcurrentQueue<T>>
        {
            protected override System.Collections.Concurrent.ConcurrentQueue<T> Create() => new System.Collections.Concurrent.ConcurrentQueue<T>();

            protected override void Recycle(System.Collections.Concurrent.ConcurrentQueue<T> array) => array.TryDequeue(out _);

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.TryDequeue(out _);
            }
        }
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentQueue<T> ToConcurrentQueuePooled<T>(this IEnumerable<T> source)
        {
            var Queue = Pooling.ConcurrentQueue<T>.Alloc();
            foreach (var item in source) Queue.Enqueue(item);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentQueue<T> ToConcurrentQueuePooledKey<T, V>(this IDictionary<T, V> source)
        {
            var Queue = Pooling.ConcurrentQueue<T>.Alloc();
            foreach (var item in source) Queue.Enqueue(item.Key);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentQueue<T> ToConcurrentQueuePooledValue<V, T>(this IDictionary<V, T> source)
        {
            var Queue = Pooling.ConcurrentQueue<T>.Alloc();
            foreach (var item in source) Queue.Enqueue(item.Value);
            return Queue;
        }
    }
}