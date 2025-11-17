#region

using System.Collections.Concurrent;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/> 对象池
        /// </summary>
        internal class ConcurrentDictionary<K, V> : SingletonArray<ConcurrentDictionary<K, V>, System.Collections.Concurrent.ConcurrentDictionary<K, V>>
        {
            protected override void Recycle(System.Collections.Concurrent.ConcurrentDictionary<K, V> array) => array.Clear();

            protected override System.Collections.Concurrent.ConcurrentDictionary<K, V> Create() => new System.Collections.Concurrent.ConcurrentDictionary<K, V>();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
            }
        }
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为 Dictionary 并存入对象池
        /// </summary>
        public static ConcurrentDictionary<int, V> ToConcurrentDictionaryPooled<V>(this IEnumerable<V> source)
        {
            var list  = Pooling.ConcurrentDictionary<int, V>.Alloc();
            var index = 0;
            foreach (var item in source) list.TryAdd(index++, item);
            return list;
        }
    }
}