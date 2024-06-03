#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    public static partial class Pool
    {
        #region Nested type: Generic

        /// <summary>
        /// 通用
        /// </summary>
        public static class Generic<T>
        where T : IPoolable, new()
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
                    if (free.Count > 0)
                    {
                        var item = free.Pop();
                        busy.Add(item);
                        return item;
                    }
                    else
                    {
                        var item = Activator.CreateInstance<T>();
                        busy.Add(item);
                        return item;
                    }
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
                    free.Push(item);
                }
            }
        }

        #endregion
    }
}