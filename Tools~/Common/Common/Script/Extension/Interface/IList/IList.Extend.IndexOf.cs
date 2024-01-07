using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AIO
{
    public static partial class ExtendIList
    {
        /// <summary>
        /// 查询存在相同元素的下标
        /// </summary>
        public static List<(int, T)> IndexOf<T>(this IList<T> list, in IEnumerable<T> items)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (items is null) throw new ArgumentNullException(nameof(items));
            var indexlist = new List<(int, T)>();
            var index = 0;
            foreach (var item in items)
            {
                if (list.Contains(item)) indexlist.Add((index, item));
                index++;
            }

            return indexlist;
        }

        /// <summary>
        /// 查询存在相同元素的下标
        /// </summary>
        public static List<(int, T)> IndexOf<T>(this IList<T> list, in IList<T> items)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (items is null) throw new ArgumentNullException(nameof(items));
            var indexlist = new List<(int, T)>();
            for (var i = 0; i < items.Count; i++)
            {
                if (list.Contains(items[i]))
                    indexlist.Add((i, items[i]));
            }

            return indexlist;
        }

        /// <summary>
        /// 查询存在相同元素的下标
        /// </summary>
        public static List<(int, T)> IndexOfParallel<T>(this IList<T> list, in IEnumerable<T> items)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (items is null) throw new ArgumentNullException(nameof(items));
            var hashset = new HashSet<T>(list);
            var indexlist = new ConcurrentBag<(int, T)>();

            Parallel.ForEach(items, (item, state, index) =>
            {
                if (hashset.Contains(item)) indexlist.Add(((int)index, item));
            });

            return indexlist.ToList();
        }
    }
}