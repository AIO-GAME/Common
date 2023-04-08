using System;
using AIO;

public static partial class Pool
{
    /// <summary>
    /// 通用
    /// </summary>
    public static class Generic<T> where T : class, IPoolable
    {
        private static readonly System.Collections.Generic.Stack<T>
            free = new System.Collections.Generic.Stack<T>();

        private static readonly System.Collections.Generic.HashSet<T>
            busy = new System.Collections.Generic.HashSet<T>(ReferenceEqualityComparer<T>.Instance);

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