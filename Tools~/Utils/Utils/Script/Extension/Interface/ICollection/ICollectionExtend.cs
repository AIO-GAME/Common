namespace AIO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 数组
    /// </summary>
    public static partial class ICollectionExtend
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int Find<T>(this ICollection<T> array, in T value) where T : IComparable
        {
            var index = 0;
            foreach (var item in array)
            {
                if (value.CompareTo(item) == 0)
                    return index;
                index++;
            }

            if (index >= array.Count) index = 0;
            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int Find<T>(this ICollection<T> array, in T value, int number) where T : IComparable
        {
            var index = 0;
            foreach (var item in array)
            {
                if (value.CompareTo(item) == 0)
                {
                    if (--number <= 0) return index;
                }

                index++;
            }

            if (index >= array.Count) return 0;
            return index;
        }
    }
}