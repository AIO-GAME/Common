#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Generic.Dictionary{K,V}"/> 对象池
        /// </summary>
        internal class Dictionary<K, V> : SingletonArray<Dictionary<K, V>, System.Collections.Generic.Dictionary<K, V>>
        {
            protected override void Recycle(System.Collections.Generic.Dictionary<K, V> array) => array.Clear();

            protected override System.Collections.Generic.Dictionary<K, V> Create() => new System.Collections.Generic.Dictionary<K, V>();

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
        public static Dictionary<int, V> ToDictionaryPooled<V>(this IEnumerable<V> source)
        {
            var list  = Pooling.Dictionary<int, V>.Alloc();
            var index = 0;
            foreach (var item in source) list.Add(index++, item);
            return list;
        }
    }
}