using System;
using System.Collections.Generic;

public partial class Pool
{
    private static readonly object @lock = new object();

    /// <summary>
    /// Dictionary 对象池
    /// </summary>
    public static class SortedList<K, V>
    {
        private static readonly System.Collections.Generic.Stack<System.Collections.Generic.SortedList<K, V>>
            free = new System.Collections.Generic.Stack<System.Collections.Generic.SortedList<K, V>>();

        private static readonly System.Collections.Generic.HashSet<System.Collections.Generic.SortedList<K, V>>
            busy = new System.Collections.Generic.HashSet<System.Collections.Generic.SortedList<K, V>>();

        /// <summary>
        /// 创建
        /// </summary>
        public static System.Collections.Generic.SortedList<K, V> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new System.Collections.Generic.SortedList<K, V>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public static void Free(System.Collections.Generic.SortedList<K, V> list)
        {
            lock (@lock)
            {
                if (!busy.Contains(list))
                {
                    throw new ArgumentException("The list to free is not in use by the pool.", nameof(list));
                }

                list.Clear();

                busy.Remove(list);

                free.Push(list);
            }
        }
    }
}

public static partial class PoolExtend
{
    /// <summary>
    /// 转化为 Dictionary 并存入对象池
    /// </summary>
    public static SortedList<int, V> ToSortedListPooled<V>(this IEnumerable<V> source)
    {
        var list = Pool.SortedList<int, V>.New();
        var index = 0;
        foreach (var item in source) list.Add(index++, item);
        return list;
    }

    /// <summary>
    /// 释放 Dictionary
    /// </summary>
    public static void Free<K, V>(this SortedList<K, V> list)
    {
        Pool.SortedList<K, V>.Free(list);
    }
}