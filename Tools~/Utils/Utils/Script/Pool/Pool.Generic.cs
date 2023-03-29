using AIO;

using System;

using X = System.Collections.Generic;
public static partial class Pool
{
    /// <summary>
    /// 通用
    /// </summary>
    public static class Generic<T> where T : class, IPoolable
    {
        private static readonly object @lock = new object();
        private static readonly X.Stack<T> free = new X.Stack<T>();
        private static readonly X.HashSet<T> busy = new X.HashSet<T>(ReferenceEqualityComparer<T>.Instance);

        /// <summary>
        /// 创建
        /// </summary>
        public static T New(Func<T> constructor)
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(constructor());
                }

                var item = free.Pop();

                item.New();

                busy.Add(item);

                return item;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public static void Free(T item)
        {
            lock (@lock)
            {
                if (!busy.Remove(item))
                {
                    throw new ArgumentException("The item to free is not in use by the pool.", nameof(item));
                }

                item.Free();

                free.Push(item);
            }
        }
    }
}
