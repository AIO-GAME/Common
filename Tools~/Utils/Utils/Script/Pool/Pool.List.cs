using System;
using System.Collections.Generic;

using X = System.Collections.Generic;

public static partial class Pool
{
    /// <summary>
    /// List对象池
    /// </summary>
    public static class List<T>
    {
        private static readonly object @lock = new object();
        private static readonly X.Stack<X.List<T>> free = new X.Stack<X.List<T>>();
        private static readonly X.HashSet<X.List<T>> busy = new X.HashSet<X.List<T>>();

        /// <summary>
        /// 创建新的
        /// </summary>
        public static X.List<T> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new X.List<T>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放List
        /// </summary>
        public static void Free(X.List<T> list)
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
    /// 转化为List并存入对象池
    /// </summary>
    public static List<T> ToListPooled<T>(this IEnumerable<T> source)
    {
        var list = Pool.List<T>.New();
        foreach (var item in source) list.Add(item);
        return list;
    }

    /// <summary>
    /// 转化为List并存入对象池
    /// </summary>
    public static List<T> ToListPooledKey<T, V>(this IDictionary<T, V> source)
    {
        var list = Pool.List<T>.New();
        foreach (var item in source) list.Add(item.Key);
        return list;
    }

    /// <summary>
    /// 转化为List并存入对象池
    /// </summary>
    public static List<T> ToListPooledValue<V, T>(this IDictionary<V, T> source)
    {
        var list = Pool.List<T>.New();
        foreach (var item in source) list.Add(item.Value);
        return list;
    }

    /// <summary>
    /// 释放List
    /// </summary>
    public static void Free<T>(this List<T> list)
    {
        Pool.List<T>.Free(list);
    }
}
