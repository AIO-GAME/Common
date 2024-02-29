using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 查询存在相同元素的下标
        /// </summary>
        public static List<(int, T)> IndexOf<T>(this IList<T> list, in IEnumerable<T> items)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (items is null) throw new ArgumentNullException(nameof(items));
            var tuples = new List<(int, T)>();
            var index = 0;
            foreach (var item in items)
            {
                if (list.Contains(item)) tuples.Add((index, item));
                index++;
            }

            return tuples;
        }

        /// <summary>
        /// 查询存在相同元素的下标
        /// </summary>
        public static List<(int, T)> IndexOf<T>(this IList<T> list, in IList<T> items)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (items is null) throw new ArgumentNullException(nameof(items));
            var tuples = new List<(int, T)>();
            for (var i = 0; i < items.Count; i++)
            {
                if (list.Contains(items[i]))
                    tuples.Add((i, items[i]));
            }

            return tuples;
        }

        /// <summary>
        /// 查询存在相同元素的下标
        /// </summary>
        public static List<(int, T)> IndexOfParallel<T>(this IList<T> list, in IEnumerable<T> items)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (items is null) throw new ArgumentNullException(nameof(items));
            var hashset = new HashSet<T>(list);
            var tuples = new ConcurrentBag<(int, T)>();

            Parallel.ForEach(items, (item, state, index) =>
            {
                if (hashset.Contains(item)) tuples.Add(((int)index, item));
            });

            return tuples.ToList();
        }
    }
}