#region

using System.Collections.Concurrent;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Concurrent.ConcurrentStack{T}"/> 对象池
        /// </summary>
        internal class ConcurrentStack<T> : SingletonArray<ConcurrentStack<T>, System.Collections.Concurrent.ConcurrentStack<T>>
        {
            protected override System.Collections.Concurrent.ConcurrentStack<T> Create() => new System.Collections.Concurrent.ConcurrentStack<T>();

            protected override void Recycle(System.Collections.Concurrent.ConcurrentStack<T> array) => array.Clear();

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
        public static ConcurrentStack<T> ToConcurrentStackPooled<T>(this IEnumerable<T> source)
        {
            var Queue = Pooling.ConcurrentStack<T>.Alloc();
            foreach (var item in source) Queue.Push(item);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentStack<T> ToConcurrentStackPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var Queue = Pooling.ConcurrentStack<T>.Alloc();
            foreach (var item in source) Queue.Push(item.Key);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static ConcurrentStack<T> ToConcurrentStackPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var Queue = Pooling.ConcurrentStack<T>.Alloc();
            foreach (var item in source) Queue.Push(item.Value);
            return Queue;
        }
    }
}