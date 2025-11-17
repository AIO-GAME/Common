#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.ArrayList"/> 对象池
        /// </summary>
        internal class ArrayList : SingletonArray<ArrayList, System.Collections.ArrayList>
        {
            protected override System.Collections.ArrayList Create() => new System.Collections.ArrayList();

            protected override void Recycle(System.Collections.ArrayList array) => array.Clear();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
            }
        }
    }

    public static partial class PoolExtend
    {
        /// <summary>
        /// 转化为 <see cref="ArrayList"/> 并存入对象池
        /// </summary>
        public static ArrayList ToArrayListPooled<T>(this IEnumerable<T> source)
        {
            var alloc = Pooling.ArrayList.Alloc();
            foreach (var item in source) alloc.Add(item);
            return alloc;
        }

        /// <summary>
        /// 转化为 <see cref="ArrayList"/> 并存入对象池
        /// </summary>
        public static ArrayList ToArrayListPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var alloc = Pooling.ArrayList.Alloc();
            foreach (var item in source) alloc.Add(item.Key);
            return alloc;
        }

        /// <summary>
        /// 转化为 <see cref="ArrayList"/> 并存入对象池
        /// </summary>
        public static ArrayList ToArrayListPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var alloc = Pooling.ArrayList.Alloc();
            foreach (var item in source) alloc.Add(item.Value);
            return alloc;
        }
    }
}