using System;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    public static partial class IListExtend
    {
        #region Remove

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
        public static int Remove<T>(this IList<T> array, in Predicate<T> match)
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

            return count;
        }

        /// <summary>
        /// 移除重复元素
        /// </summary>
        public static void RemoveDistinct<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 1) return;
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

        #endregion
    }
}