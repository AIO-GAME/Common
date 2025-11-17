#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Hashtable"/> 对象池
        /// </summary>
        internal class Hashtable : SingletonArray<Hashtable, System.Collections.Hashtable>
        {
            protected override System.Collections.Hashtable Create() => new System.Collections.Hashtable();

            protected override void Recycle(System.Collections.Hashtable array) => array.Clear();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
            }
        }
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为 <see cref="Hashtable"/> 并存入对象池
        /// </summary>
        public static Hashtable ToHashtablePooledKey<T, V>(this IDictionary<T, V> source)
        {
            var alloc = Pooling.Hashtable.Alloc();
            foreach (var item in source) alloc.Add(item.Key, item.Value);
            return alloc;
        }

        /// <summary>
        /// 转化为 <see cref="Hashtable"/> 并存入对象池
        /// </summary>
        public static Hashtable ToHashtablePooledValue<V, T>(this IDictionary<V, T> source)
        {
            var alloc = Pooling.Hashtable.Alloc();
            foreach (var item in source) alloc.Add(item.Key, item.Value);
            return alloc;
        }
    }
}