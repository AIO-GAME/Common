using System;
using System.Collections.Generic;

public static partial class Pool
{
    /// <summary>
    /// Dictionary 对象池
    /// </summary>
    public static class Dictionary<K, V>
    {
        private static readonly System.Collections.Generic.Stack<System.Collections.Generic.Dictionary<K, V>>
            free = new System.Collections.Generic.Stack<System.Collections.Generic.Dictionary<K, V>>();

        private static readonly System.Collections.Generic.HashSet<System.Collections.Generic.Dictionary<K, V>>
            busy = new System.Collections.Generic.HashSet<System.Collections.Generic.Dictionary<K, V>>();

        /// <summary>
        /// 创建
        /// </summary>
        public static System.Collections.Generic.Dictionary<K, V> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new System.Collections.Generic.Dictionary<K, V>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public static void Free(System.Collections.Generic.Dictionary<K, V> list)
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
    public static Dictionary<int, V> ToDictionaryPooled<V>(this IEnumerable<V> source)
    {
        var list = Pool.Dictionary<int, V>.New();
        var index = 0;
        foreach (var item in source) list.Add(index++, item);
        return list;
    }

    /// <summary>
    /// 释放 Dictionary
    /// </summary>
    public static void Free<K, V>(this Dictionary<K, V> list)
    {
        Pool.Dictionary<K, V>.Free(list);
    }
}