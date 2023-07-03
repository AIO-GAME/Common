using System;
using System.Collections.Generic;

public partial class Pool
{
    private static readonly object @lock = new object();

    /// <summary>
    /// Dictionary 对象池
    /// </summary>
    internal static class ASortedList<K, V>
    {
        private static readonly Stack<SortedList<K, V>>
            free = new Stack<SortedList<K, V>>();

        private static readonly HashSet<SortedList<K, V>>
            busy = new HashSet<SortedList<K, V>>();

        /// <summary>
        /// 创建
        /// </summary>
        public static SortedList<K, V> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new SortedList<K, V>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public static void Free(SortedList<K, V> array)
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
    /// 转化为 Dictionary 并存入对象池
    /// </summary>
    public static SortedList<int, V> ToSortedListPooled<V>(this IEnumerable<V> source)
    {
        var list = Pool.ASortedList<int, V>.New();
        var index = 0;
        foreach (var item in source) list.Add(index++, item);
        return list;
    }

    /// <summary>
    /// 释放 Dictionary
    /// </summary>
    public static void Free<K, V>(this SortedList<K, V> list)
    {
        Pool.ASortedList<K, V>.Free(list);
    }
}