#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    partial class Pooling
    {
        /// <summary>
        /// <see cref="List{T}"/> 对象池
        /// </summary>
        internal class List<T> : SingletonArray<List<T>, System.Collections.Generic.List<T>>
        {
            protected override System.Collections.Generic.List<T> Create() => new System.Collections.Generic.List<T>();

            protected override void Recycle(System.Collections.Generic.List<T> array) => array.Clear();

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
        public static List<T> ToListPooled<T>(this IEnumerable<T> source)
        {
            var list = Pooling.List<T>.Alloc();
            list.AddRange(source);
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static List<T> ToListPooledKey<T, V>(this IDictionary<T, V> source)
        {
            var list = Pooling.List<T>.Alloc();
            list.AddRange(source.Select(item => item.Key));
            return list;
        }

        /// <summary>
        /// 转化为List并存入对象池
        /// </summary>
        public static List<T> ToListPooledValue<V, T>(this IDictionary<V, T> source)
        {
            var list = Pooling.List<T>.Alloc();
            list.AddRange(source.Select(item => item.Value));
            return list;
        }
    }
}