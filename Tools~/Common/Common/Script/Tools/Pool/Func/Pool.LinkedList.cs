#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Generic.LinkedList{T}"/> 对象池
        /// </summary>
        internal class LinkedList<T> : SingletonArray<LinkedList<T>, System.Collections.Generic.LinkedList<T>>
        {
            protected override System.Collections.Generic.LinkedList<T> Create() => new System.Collections.Generic.LinkedList<T>();

            protected override void Recycle(System.Collections.Generic.LinkedList<T> array) => array.Clear();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
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
            var list = Pooling.LinkedList<T>.Alloc();
            foreach (var item in source) list.AddLast(item);
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static LinkedList<T> ToLinkedListPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var list = Pooling.LinkedList<T>.Alloc();
            foreach (var item in source) list.AddLast(item.Key);
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static LinkedList<T> ToLinkedListPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var list = Pooling.LinkedList<T>.Alloc();
            foreach (var item in source) list.AddLast(item.Value);
            return list;
        }
    }
}