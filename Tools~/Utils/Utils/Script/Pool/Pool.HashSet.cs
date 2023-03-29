using System;
using System.Collections.Generic;

using X = System.Collections.Generic;

public static partial class Pool
{
    /// <summary>
    /// HashSet对象池
    /// </summary>
    public static class HashSet<T>
    {
        private static readonly object @lock = new object();
        private static readonly X.Stack<X.HashSet<T>> free = new X.Stack<X.HashSet<T>>();
        private static readonly X.HashSet<X.HashSet<T>> busy = new X.HashSet<X.HashSet<T>>();

        /// <summary>
        /// 创建新的
        /// </summary>
        public static X.HashSet<T> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new X.HashSet<T>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放HashSet
        /// </summary>
        public static void Free(X.HashSet<T> HashSet)
        {
            lock (@lock)
            {
                if (!busy.Contains(HashSet))
                {
                    throw new ArgumentException("The HashSet to free is not in use by the pool.", nameof(HashSet));
                }

                HashSet.Clear();

                busy.Remove(HashSet);

                free.Push(HashSet);
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
        var HashSet = Pool.HashSet<T>.New();
        foreach (var item in source) HashSet.Add(item);
        return HashSet;
    }

    /// <summary>
    /// 转化为HashSet并存入对象池
    /// </summary>
    public static HashSet<T> ToHashSetPooledKey<T, V>(this IDictionary<T, V> source)
    {
        var HashSet = Pool.HashSet<T>.New();
        foreach (var item in source) HashSet.Add(item.Key);
        return HashSet;
    }

    /// <summary>
    /// 转化为HashSet并存入对象池
    /// </summary>
    public static HashSet<T> ToHashSetPooledValue<V, T>(this IDictionary<V, T> source)
    {
        var HashSet = Pool.HashSet<T>.New();
        foreach (var item in source) HashSet.Add(item.Value);
        return HashSet;
    }

    /// <summary>
    /// 释放HashSet
    /// </summary>
    public static void Free<T>(this HashSet<T> HashSet)
    {
        Pool.HashSet<T>.Free(HashSet);
    }
}
