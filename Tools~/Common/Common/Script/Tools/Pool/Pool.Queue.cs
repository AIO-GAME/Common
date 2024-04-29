#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public static partial class Pool
    {
        #region Nested type: AQueue

        /// <summary>
        /// Queue对象池
        /// </summary>
        internal static class AQueue<T>
        {
            private static readonly Stack<Queue<T>>
                free = new Stack<Queue<T>>();

            private static readonly HashSet<Queue<T>>
                busy = new HashSet<Queue<T>>();

            /// <summary>
            /// 创建新的
            /// </summary>
            public static Queue<T> New()
            {
                lock (@lock)
                {
                    if (free.Count == 0) free.Push(new Queue<T>());

                    var array = free.Pop();

                    busy.Add(array);

                    return array;
                }
            }

            /// <summary>
            /// 释放Queue
            /// </summary>
            public static void Free(Queue<T> array)
            {
                lock (@lock)
                {
                    array.Clear();
                    if (busy.Contains(array)) busy.Remove(array);
                    free.Push(array);
                }
            }
        }

        #endregion
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static Queue<T> ToQueuePooled<T>(this IEnumerable<T> source)
        {
            var Queue = Pool.AQueue<T>.New();
            foreach (var item in source) Queue.Enqueue(item);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static Queue<T> ToQueuePooledKey<T, V>(this IDictionary<T, V> source)
        {
            var Queue = Pool.AQueue<T>.New();
            foreach (var item in source) Queue.Enqueue(item.Key);
            return Queue;
        }

        /// <summary>
        /// 转化为Queue并存入对象池
        /// </summary>
        public static Queue<T> ToQueuePooledValue<V, T>(this IDictionary<V, T> source)
        {
            var Queue = Pool.AQueue<T>.New();
            foreach (var item in source) Queue.Enqueue(item.Value);
            return Queue;
        }

        /// <summary>
        /// 释放Queue
        /// </summary>
        public static void Free<T>(this Queue<T> Queue)
        {
            Pool.AQueue<T>.Free(Queue);
        }
    }
}