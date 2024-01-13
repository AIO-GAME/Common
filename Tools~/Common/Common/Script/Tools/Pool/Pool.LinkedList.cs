using System.Collections.Generic;

namespace AIO
{
    public static partial class Pool
    {
        /// <summary>
        /// List对象池
        /// </summary>
        internal static class ALinkedList<T>
        {
            private static readonly Stack<LinkedList<T>>
                free = new Stack<LinkedList<T>>();

            private static readonly HashSet<LinkedList<T>>
                busy = new HashSet<LinkedList<T>>();

            /// <summary>
            /// 创建新的
            /// </summary>
            public static LinkedList<T> New()
            {
                lock (@lock)
                {
                    if (free.Count == 0)
                    {
                        free.Push(new LinkedList<T>());
                    }

                    var array = free.Pop();

                    busy.Add(array);

                    return array;
                }
            }

            /// <summary>
            /// 释放List
            /// </summary>
            internal static void Free(LinkedList<T> array)
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
        public static LinkedList<T> ToLinkedListPooled<T>(this IEnumerable<T> source)
        {
            var list = Pool.ALinkedList<T>.New();
            foreach (var item in source) list.AddLast(item);
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static LinkedList<T> ToLinkedListPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var list = Pool.ALinkedList<T>.New();
            foreach (var item in source) list.AddLast(item.Key);
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static LinkedList<T> ToLinkedListPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var list = Pool.ALinkedList<T>.New();
            foreach (var item in source) list.AddLast(item.Value);
            return list;
        }

        /// <summary>
        /// 释放List
        /// </summary>
        public static void Free<T>(this LinkedList<T> list)
        {
            Pool.ALinkedList<T>.Free(list);
        }
    }
}