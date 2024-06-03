#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendIEnumerable
    {
        /// <summary>
        /// 查找 -1:未找到
        /// </summary>
        public static int Find<T>(this IEnumerable<T> array, in T value)
        where T : IEquatable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var index = 0;
            foreach (var item in array)
            {
                if (value.Equals(item))
                    return index;
                index++;
            }

            return -1;
        }

        /// <summary>
        /// 查找 -1:未找到
        /// </summary>
        public static int Find<T>(this IEnumerable<T> array, in T value, int number)
        where T : IEquatable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (value == null) throw new ArgumentNullException(nameof(value));
            var index = 0;
            foreach (var item in array)
            {
                if (value.Equals(item))
                    if (--number <= 0)
                        return index;

                index++;
            }

            return -1;
        }
    }
}