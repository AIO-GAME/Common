#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        #region Nested type: AHashSet

        /// <summary>
        /// <see cref="System.Collections.Generic.HashSet{T}"/> 对象池
        /// </summary>
        internal class HashSet<T> : SingletonArray<HashSet<T>, System.Collections.Generic.HashSet<T>>
        {
            protected override System.Collections.Generic.HashSet<T> Create() => new System.Collections.Generic.HashSet<T>();

            protected override void Recycle(System.Collections.Generic.HashSet<T> array) => array.Clear();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
            }
        }

        #endregion
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为HashSet并存入对象池
        /// </summary>
        public static HashSet<T> ToHashSetPooled<T>(this IEnumerable<T> source)
        {
            var HashSet = Pooling.HashSet<T>.Alloc();
            foreach (var item in source) HashSet.Add(item);
            return HashSet;
        }

        /// <summary>
        /// 转化为HashSet并存入对象池
        /// </summary>
        public static HashSet<T> ToHashSetPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var HashSet = Pooling.HashSet<T>.Alloc();
            foreach (var item in source) HashSet.Add(item.Key);
            return HashSet;
        }

        /// <summary>
        /// 转化为HashSet并存入对象池
        /// </summary>
        public static HashSet<T> ToHashSetPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var HashSet = Pooling.HashSet<T>.Alloc();
            foreach (var item in source) HashSet.Add(item.Value);
            return HashSet;
        }
    }
}