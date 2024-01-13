using System.Collections.Generic;

namespace AIO
{
    public static partial class Pool
    {
        /// <summary>
        /// Stack对象池
        /// </summary>
        internal static class AStack<T>
        {
            private static readonly Stack<Stack<T>>
                free = new Stack<Stack<T>>();

            private static readonly HashSet<Stack<T>>
                busy = new HashSet<Stack<T>>();

            /// <summary>
            /// 创建新的
            /// </summary>
            public static Stack<T> New()
            {
                lock (@lock)
                {
                    if (free.Count == 0)
                    {
                        free.Push(new Stack<T>());
                    }

                    var array = free.Pop();

                    busy.Add(array);

                    return array;
                }
            }

            /// <summary>
            /// 释放Stack
            /// </summary>
            public static void Free(Stack<T> array)
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
            var Stack = Pool.AStack<T>.New();
            foreach (var item in source) Stack.Push(item);
            return Stack;
        }

        /// <summary>
        /// 转化为Stack并存入对象池
        /// </summary>
        public static Stack<T> ToStackPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var Stack = Pool.AStack<T>.New();
            foreach (var item in source) Stack.Push(item.Key);
            return Stack;
        }

        /// <summary>
        /// 转化为Stack并存入对象池
        /// </summary>
        public static Stack<T> ToStackPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var Stack = Pool.AStack<T>.New();
            foreach (var item in source) Stack.Push(item.Value);
            return Stack;
        }

        /// <summary>
        /// 释放Stack
        /// </summary>
        public static void Free<T>(this Stack<T> Stack)
        {
            Pool.AStack<T>.Free(Stack);
        }
    }
}