#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    /// <summary>
    /// 字典扩展
    /// </summary>
    public static partial class ExtendIDictionary
    {
        /// <summary>
        /// 字符串字典排序
        /// </summary>
        public static IDictionary<string, T> Sort<T>(this IDictionary<string, T> dictionary)
        {
            var keys = dictionary.Keys.ToList();
            keys.Sort((s, s1) => string.Compare(s, s1, StringComparison.CurrentCulture));
            return keys.ToDictionary(key => key, key => dictionary[key]);
        }

        /// <summary>
        ///  字典增量
        /// </summary>
        public static int Increment<T>(this IDictionary<T, int> dictionary, T key)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                dictionary[key] = value + 1;
                return dictionary[key];
            }

            dictionary[key] = 1;
            return 1;
        }

        /// <summary>
        /// 字典减量
        /// </summary>
        public static int Decrement<T>(this IDictionary<T, int> dictionary, T key)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                dictionary[key] = value - 1;
                return dictionary[key];
            }

            dictionary[key] = -1;
            return -1;
        }
    }
}