using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 迭代器扩展
    /// </summary>
    public static partial class IEnumerableExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Contain<T>(this IEnumerable<T> array, in T value) where T : IComparable
        {
            foreach (var item in array)
                if (value.CompareTo(item) == 0)
                    return true;
            return false;
        }
    }
}