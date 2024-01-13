using System;
using System.Collections.Generic;

namespace AIO
{
    public static partial class Pool
    {
        /// <summary>
        /// 通用
        /// </summary>
        public static class Generic<T> where T : class, IPoolable, new()
        {
            private static readonly Stack<T>
                free = new Stack<T>();

            private static readonly HashSet<T>
                busy = new HashSet<T>(ReferenceEqualityComparer<T>.Instance);

            /// <summary>
            /// 创建
            /// </summary>
            public static T New(Func<T> constructor)
            {
                lock (@lock)
                {
                    var item = constructor();
                    item.New();
                    busy.Add(item);
                    return item;
                }
            }

            /// <summary>
            /// 创建
            /// </summary>
            public static T New()
            {
                lock (@lock)
                {
                    var item = Activator.CreateInstance<T>();
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
                    if (busy.Contains(item)) busy.Remove(item);
                    item.Free();
                    free.Push(item);
                }
            }
        }
    }
}