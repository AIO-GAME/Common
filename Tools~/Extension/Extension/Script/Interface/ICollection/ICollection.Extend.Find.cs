using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 集合
    /// </summary>
    public static partial class ICollectionExtend
    {
        /// <summary>
        /// 查找 -1:未找到
        /// </summary>
        public static int Find<T>(this IList<T> array, in T value) where T : IEquatable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (value == null) throw new ArgumentNullException(nameof(value));
            for (var i = 0; i < array.Count; i++)
            {
                if (value.Equals(array[i])) return i;
            }

            return -1;
        }
    }
}