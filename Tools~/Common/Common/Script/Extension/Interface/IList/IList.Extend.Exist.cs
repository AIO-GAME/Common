using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 是否存在重复的
        /// </summary>
        public static bool ExistRepeat<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array), "The input array is null.");

            var hashSet = new HashSet<T>();
            return array.Any(item => !hashSet.Add(item));
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        public static bool ExistRepeat<T>(this IList<T> array, in Func<T, T, bool> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var hashSet = new HashSet<T>();
            foreach (var item in array)
            {
                if (!hashSet.Add(item) && compare(item, hashSet.First()))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        public static bool ExistRepeat<T>(this IList<T> array, in IEqualityComparer<T> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var hashSet = new HashSet<T>(compare);
            return array.Any(item => !hashSet.Add(item));
        }
    }
}