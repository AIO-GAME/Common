using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class ExtendIList
    {
        /// <summary>
        /// 移除第一个元素
        /// </summary>
        public static T RemoveFirst<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            var r = array[0];
            array.RemoveAt(0);
            return r;
        }

        /// <summary>
        /// 移除元素
        /// </summary>
        public static IList<T> Remove<T>(this IList<T> array, in Predicate<T> match)
        {
            if (match is null) throw new ArgumentNullException(nameof(match));

            int i = 0, j = 0;
            for (; i < array.Count; i++)
            {
                if (!match(array[i]))
                {
                    if (j != i) array[j] = array[i];
                    j++;
                }
            }

            var count = array.Count - j;
            if (count > 0)
            {
                for (var k = j; k < array.Count; k++) array[k] = default;
                Array.Clear((Array)array, j, count);
            }

            return array;
        }

        /// <summary>
        /// 移除重复元素
        /// </summary>
        public static IList<T> RemoveRepeat<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return array;
            var hashSet = new HashSet<T>();
            for (var i = 0; i < array.Count; i++)
            {
                var num = array[i];
                if (hashSet.Count == 0 || !hashSet.Contains(num))
                {
                    hashSet.Add(num);
                }
                else if (array is List<T> list)
                {
                    list.RemoveAll(x => Equals(x, num));
                }
                else
                {
                    array.RemoveAt(i--);
                }
            }

            return array;
        }

        /// <summary>
        /// 移除最后一个元素
        /// </summary>
        public static T RemoveLast<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            var idx = array.Count - 1;
            var r = array[idx];
            array.RemoveAt(idx);
            return r;
        }

        /// <summary>
        /// 移除指定范围的元素
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the collection argument is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if either index is less than zero or greater than or equal to the number of elements in the IList.</exception>
        public static IList<T> RemoveGroup<T>(this IList<T> array, in int startIndex, in int endIndex)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (startIndex < 0 || startIndex >= array.Count) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (endIndex <= startIndex || endIndex > array.Count)
                throw new ArgumentOutOfRangeException(nameof(endIndex));

            var countToRemove = endIndex - startIndex;
            if (countToRemove == 0) return array;

            for (var i = startIndex; i < endIndex; i++)
                array.RemoveAt(startIndex);
            return array;
        }

        /// <summary>
        /// 移除指定数量
        /// </summary>
        /// <param name="collection">数组</param>
        /// <param name="retainCount">保留数量</param>
        /// <param name="removeFromStart">Ture:从第一个移除 False:从最后一个移除</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <exception cref="ArgumentNullException">参数为NULL</exception>
        public static IList<T> Remove<T>(this IList<T> collection, in int retainCount, in bool removeFromStart)
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection), "The input collection is null.");

            if (retainCount <= 0 || collection.Count <= retainCount) return collection;

            if (removeFromStart)
            {
                var startIndex = collection.Count - retainCount;
                var retainedItems = collection.Skip(startIndex).Take(retainCount).ToList();

                collection.Clear();
                foreach (var item in retainedItems) collection.Add(item);
            }
            else
            {
                var retainedItems = collection.Take(retainCount).ToList();
                collection.Clear();
                foreach (var item in retainedItems) collection.Add(item);
            }

            return collection;
        }
    }
}