using System.Collections.Generic;

namespace AIO
{
    public static partial class Pool
    {
        /// <summary>
        /// HashSet对象池
        /// </summary>
        internal static class AHashSet<T>
        {
            private static readonly Stack<HashSet<T>>
                free = new Stack<HashSet<T>>();

            private static readonly HashSet<HashSet<T>>
                busy = new HashSet<HashSet<T>>();

            /// <summary>
            /// 创建新的
            /// </summary>
            public static HashSet<T> New()
            {
                lock (@lock)
                {
                    if (free.Count == 0)
                    {
                        free.Push(new HashSet<T>());
                    }

                    var array = free.Pop();

                    busy.Add(array);

                    return array;
                }
            }

            /// <summary>
            /// 释放HashSet
            /// </summary>
            public static void Free(HashSet<T> array)
            {
                lock (@lock)
                {
                    array.Clear();
                    if (busy.Contains(array)) busy.Remove(array);
                    free.Push(array);
                }
            }
        }
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为HashSet并存入对象池
        /// </summary>
        public static HashSet<T> ToHashSetPooled<T>(this IEnumerable<T> source)
        {
            var HashSet = Pool.AHashSet<T>.New();
            foreach (var item in source) HashSet.Add(item);
            return HashSet;
        }

        /// <summary>
        /// 转化为HashSet并存入对象池
        /// </summary>
        public static HashSet<T> ToHashSetPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var HashSet = Pool.AHashSet<T>.New();
            foreach (var item in source) HashSet.Add(item.Key);
            return HashSet;
        }

        /// <summary>
        /// 转化为HashSet并存入对象池
        /// </summary>
        public static HashSet<T> ToHashSetPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var HashSet = Pool.AHashSet<T>.New();
            foreach (var item in source) HashSet.Add(item.Value);
            return HashSet;
        }

        /// <summary>
        /// 释放HashSet
        /// </summary>
        public static void Free<T>(this HashSet<T> HashSet)
        {
            Pool.AHashSet<T>.Free(HashSet);
        }
    }
}