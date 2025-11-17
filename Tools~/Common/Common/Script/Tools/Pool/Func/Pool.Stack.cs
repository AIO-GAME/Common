#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Generic.Stack{T}"/> 对象池
        /// </summary>
        internal class Stack<T> : SingletonArray<Stack<T>, System.Collections.Generic.Stack<T>>
        {
            protected override System.Collections.Generic.Stack<T> Create() => new System.Collections.Generic.Stack<T>();

            protected override void Recycle(System.Collections.Generic.Stack<T> array) => array.Clear();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
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
            var Stack = Pooling.Stack<T>.Alloc();
            foreach (var item in source) Stack.Push(item);
            return Stack;
        }

        /// <summary>
        /// 转化为Stack并存入对象池
        /// </summary>
        public static Stack<T> ToStackPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var Stack = Pooling.Stack<T>.Alloc();
            foreach (var item in source) Stack.Push(item.Key);
            return Stack;
        }

        /// <summary>
        /// 转化为Stack并存入对象池
        /// </summary>
        public static Stack<T> ToStackPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var Stack = Pooling.Stack<T>.Alloc();
            foreach (var item in source) Stack.Push(item.Value);
            return Stack;
        }
    }
}