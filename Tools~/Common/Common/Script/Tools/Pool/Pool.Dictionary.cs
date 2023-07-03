using System;
using System.Collections.Generic;

public static partial class Pool
{
    /// <summary>
    /// Dictionary 对象池
    /// </summary>
    internal static class ADictionary<K, V>
    {
        private static readonly Stack<Dictionary<K, V>>
            free = new Stack<Dictionary<K, V>>();

        private static readonly HashSet<Dictionary<K, V>>
            busy = new HashSet<Dictionary<K, V>>();

        /// <summary>
        /// 创建
        /// </summary>
        public static Dictionary<K, V> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new Dictionary<K, V>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public static void Free(Dictionary<K, V> array)
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
    public static Dictionary<int, V> ToDictionaryPooled<V>(this IEnumerable<V> source)
    {
        var list = Pool.ADictionary<int, V>.New();
        var index = 0;
        foreach (var item in source) list.Add(index++, item);
        return list;
    }

    /// <summary>
    /// 释放 Dictionary
    /// </summary>
    public static void Free<K, V>(this Dictionary<K, V> list)
    {
        Pool.ADictionary<K, V>.Free(list);
    }
}