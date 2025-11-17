#region

using System;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// 通用
        /// </summary>
        public static class Generic<T>
        where T : IPoolable
        {
            private static readonly System.Collections.Generic.Stack<T>
                free = new System.Collections.Generic.Stack<T>();

            private static readonly System.Collections.Generic.HashSet<T>
                busy = new System.Collections.Generic.HashSet<T>(ReferenceEqualityComparer<T>.Instance);

            /// <summary>
            /// 创建
            /// </summary>
            public static T Alloc(Func<T> constructor)
            {
                lock (@lock)
                {
                    var item = constructor();
                    busy.Add(item);
                    item.Initialize();
                    return item;
                }
            }

            /// <summary>
            /// 创建
            /// </summary>
            public static T Alloc()
            {
                lock (@lock)
                {
                    var item = free.Count > 0 ? free.Pop() : Activator.CreateInstance<T>();
                    busy.Add(item);
                    item.Initialize();
                    return item;
                }
            }

            /// <summary>
            /// 释放
            /// </summary>
            public static void Recycle(T item)
            {
                if (item == null) return;
                lock (@lock)
                {
                    item.Reset();
                    if (busy.Contains(item)) busy.Remove(item);
                    free.Push(item);
                }
            }

            /// <summary>
            /// 清空缓存
            /// </summary>
            public static void ClearCache()
            {
                lock (@lock)
                {
                    while (free.Count > 0)
                    {
                        free.Pop().Dispose();
                    }
                }
            }
        }
    }
}