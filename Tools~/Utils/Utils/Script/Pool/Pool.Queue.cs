using System;
using System.Collections.Generic;

using X = System.Collections.Generic;

public static partial class Pool
{
    /// <summary>
    /// Queue对象池
    /// </summary>
    public static class Queue<T>
    {
        private static readonly object @lock = new object();
        private static readonly X.Stack<X.Queue<T>> free = new X.Stack<X.Queue<T>>();
        private static readonly X.HashSet<X.Queue<T>> busy = new X.HashSet<X.Queue<T>>();

        /// <summary>
        /// 创建新的
        /// </summary>
        public static X.Queue<T> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new X.Queue<T>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放Queue
        /// </summary>
        public static void Free(X.Queue<T> Queue)
        {
            lock (@lock)
            {
                if (!busy.Contains(Queue))
                {
                    throw new ArgumentException("The Queue to free is not in use by the pool.", nameof(Queue));
                }

                Queue.Clear();

                busy.Remove(Queue);

                free.Push(Queue);
            }
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
        var Queue = Pool.Queue<T>.New();
        foreach (var item in source) Queue.Enqueue(item);
        return Queue;
    }

    /// <summary>
    /// 转化为Queue并存入对象池
    /// </summary>
    public static Queue<T> ToQueuePooledKey<T, V>(this IDictionary<T, V> source)
    {
        var Queue = Pool.Queue<T>.New();
        foreach (var item in source) Queue.Enqueue(item.Key);
        return Queue;
    }

    /// <summary>
    /// 转化为Queue并存入对象池
    /// </summary>
    public static Queue<T> ToQueuePooledValue<V, T>(this IDictionary<V, T> source)
    {
        var Queue = Pool.Queue<T>.New();
        foreach (var item in source) Queue.Enqueue(item.Value);
        return Queue;
    }

    /// <summary>
    /// 释放Queue
    /// </summary>
    public static void Free<T>(this Queue<T> Queue)
    {
        Pool.Queue<T>.Free(Queue);
    }
}
