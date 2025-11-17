#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="System.Collections.Generic.SortedList{K,V}"/> 对象池
        /// </summary>
        internal class SortedList<K, V> : SingletonArray<SortedList<K, V>, System.Collections.Generic.SortedList<K, V>>
        {
            protected override void Recycle(System.Collections.Generic.SortedList<K, V> array) => array.Clear();

            protected override System.Collections.Generic.SortedList<K, V> Create() => new System.Collections.Generic.SortedList<K, V>();

            protected override void ClearAll()
            {
                foreach (var array in busy.Value) array.Clear();
            }

            public static System.Collections.Generic.SortedList<K, V> Alloc(IDictionary<K, V> val)
            {
                var array = Instance.Pop();
                if (val == null) return array;
                foreach (var kv in val) array.Add(kv.Key, kv.Value);
                return array;
            }
        }

        /// <summary>
        /// <see cref="System.Collections.SortedList"/> 对象池
        /// </summary>
        internal class SortedList : SingletonArray<SortedList, System.Collections.SortedList>
        {
            protected override void Recycle(System.Collections.SortedList array) => array.Clear();

            protected override System.Collections.SortedList Create() => new System.Collections.SortedList();

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
        public static SortedList<int, V> ToSortedListPooled<V>(this IEnumerable<V> source)
        {
            var list  = Pooling.SortedList<int, V>.Alloc();
            var index = 0;
            foreach (var item in source) list.Add(index++, item);
            return list;
        }
    }
}