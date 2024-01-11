﻿using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}