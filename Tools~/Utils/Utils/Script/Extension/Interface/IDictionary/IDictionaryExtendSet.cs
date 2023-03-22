using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    public static partial class IDictionaryExtend
    {
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void Set(this IDictionary dic, in string key, in object value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key)) dic[key] = value;
            else dic.Add(key, value);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void Set(this IDictionary dic, in object key, in object value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key is null) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key)) dic[key] = value;
            else dic.Add(key, value);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void Set<K, V>(this Dictionary<K, V> dic, in K key, in V value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.ContainsKey(key)) dic[key] = value;
            else dic.Add(key, value);
        }
    }
}