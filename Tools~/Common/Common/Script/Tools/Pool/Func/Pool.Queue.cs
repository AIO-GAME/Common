#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Generic.Queue{T}"/> 对象池
        /// </summary>
        internal class Queue<T> : SingletonArray<Queue<T>, System.Collections.Generic.Queue<T>>
        {
            protected override System.Collections.Generic.Queue<T> Create() => new System.Collections.Generic.Queue<T>();

            protected override void Recycle(System.Collections.Generic.Queue<T> array) => array.Clear();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
            }
        }
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static Queue<T> ToQueuePooled<T>(this IEnumerable<T> source)
        {
            var Queue = Pooling.Queue<T>.Alloc();
            foreach (var item in source) Queue.Enqueue(item);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static Queue<T> ToQueuePooledKey<T, V>(this IDictionary<T, V> source)
        {
            var Queue = Pooling.Queue<T>.Alloc();
            foreach (var item in source) Queue.Enqueue(item.Key);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static Queue<T> ToQueuePooledValue<V, T>(this IDictionary<V, T> source)
        {
            var Queue = Pooling.Queue<T>.Alloc();
            foreach (var item in source) Queue.Enqueue(item.Value);
            return Queue;
        }
    }
}