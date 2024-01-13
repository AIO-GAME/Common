using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    public static partial class Pool
    {
        /// <summary>
        /// List对象池
        /// </summary>
        internal static class AList<T>
        {
            private static readonly Stack<List<T>>
                free = new Stack<List<T>>();

            private static readonly HashSet<List<T>>
                busy = new HashSet<List<T>>();

            /// <summary>
            /// 创建新的
            /// </summary>
            public static List<T> New()
            {
                lock (@lock)
                {
                    if (free.Count == 0) free.Push(new List<T>());
                    var array = free.Pop();
                    busy.Add(array);
                    return array;
                }
            }

            /// <summary>
            /// 释放List
            /// </summary>
            public static void Free(List<T> array)
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
        /// 转化为List并存入对象池
        /// </summary>
        public static List<T> ToListPooled<T>(this IEnumerable<T> source)
        {
            var list = Pool.AList<T>.New();
            list.AddRange(source);
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static List<T> ToListPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var list = Pool.AList<T>.New();
            list.AddRange(source.Select(item => item.Key));
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static List<T> ToListPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var list = Pool.AList<T>.New();
            list.AddRange(source.Select(item => item.Value));
            return list;
        }

        /// <summary>
        /// 释放List
        /// </summary>
        public static void Free<T>(this List<T> list)
        {
            Pool.AList<T>.Free(list);
        }
    }
}