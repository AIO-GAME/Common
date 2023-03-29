using System;
using System.Collections.Generic;

using X = System.Collections.Generic;

/// <summary>
/// 对象池
/// </summary>
public static partial class Pool
{
    /// <summary>
    /// Stack对象池
    /// </summary>
    public static class Stack<T>
    {
        private static readonly object @lock = new object();
        private static readonly X.Stack<X.Stack<T>> free = new X.Stack<X.Stack<T>>();
        private static readonly X.HashSet<X.Stack<T>> busy = new X.HashSet<X.Stack<T>>();

        /// <summary>
        /// 创建新的
        /// </summary>
        public static X.Stack<T> New()
        {
            lock (@lock)
            {
                if (free.Count == 0)
                {
                    free.Push(new X.Stack<T>());
                }

                var array = free.Pop();

                busy.Add(array);

                return array;
            }
        }

        /// <summary>
        /// 释放Stack
        /// </summary>
        public static void Free(X.Stack<T> Stack)
        {
            lock (@lock)
            {
                if (!busy.Contains(Stack))
                {
                    throw new ArgumentException("The Stack to free is not in use by the pool.", nameof(Stack));
                }

                Stack.Clear();

                busy.Remove(Stack);

                free.Push(Stack);
            }
        }
    }

}

/// <summary>
/// 扩展
/// </summary>
public static partial class PoolExtend
{
    /// <summary>
    /// 转化为Stack并存入对象池
    /// </summary>
    public static Stack<T> ToStackPooled<T>(this IEnumerable<T> source)
    {
        var Stack = Pool.Stack<T>.New();
        foreach (var item in source) Stack.Push(item);
        return Stack;
    }

    /// <summary>
    /// 转化为Stack并存入对象池
    /// </summary>
    public static Stack<T> ToStackPooledKey<T, V>(this IDictionary<T, V> source)
    {
        var Stack = Pool.Stack<T>.New();
        foreach (var item in source) Stack.Push(item.Key);
        return Stack;
    }

    /// <summary>
    /// 转化为Stack并存入对象池
    /// </summary>
    public static Stack<T> ToStackPooledValue<V, T>(this IDictionary<V, T> source)
    {
        var Stack = Pool.Stack<T>.New();
        foreach (var item in source) Stack.Push(item.Value);
        return Stack;
    }

    /// <summary>
    /// 释放Stack
    /// </summary>
    public static void Free<T>(this Stack<T> Stack)
    {
        Pool.Stack<T>.Free(Stack);
    }
}
