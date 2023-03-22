/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-12-06                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    public static partial class IDictionaryExtend
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        public static object Get(this IDictionary dic, in string key)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key)) return dic[key];
            return null;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        public static object Get(this IDictionary dic, in object key)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key is null) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key)) return dic[key];
            return null;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <typeparam name="T">任意泛型</typeparam>
        public static T Get<T>(this IDictionary dic, in string key)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key) && dic[key] is T t) return t;
            return default;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <typeparam name="T">任意泛型</typeparam>
        public static T Get<T>(this IDictionary dic, in object key)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key is null) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key) && dic[key] is T t) return t;
            return default;
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="T">任意泛型</typeparam>
        public static bool TryGet<T>(this IDictionary dic, in string key, out T value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key) && dic[key] is T temp)
            {
                value = temp;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="T">任意泛型</typeparam>
        public static bool TryGet<T>(this IDictionary dic, in object key, out T value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key is null) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key) && dic[key] is T temp)
            {
                value = temp;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        public static V Get<k, V>(this Dictionary<k, V> dic, in k key)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.ContainsKey(key)) return dic[key];
            return default;
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="K">任意泛型</typeparam>
        /// <typeparam name="V">任意泛型</typeparam>
        public static bool TryGet<K, V>(this Dictionary<K, V> dic, in K key, out V value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.ContainsKey(key))
            {
                value = dic[key];
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="K">任意泛型</typeparam>
        /// <typeparam name="V">任意泛型</typeparam>
        public static bool TrySet<K, V>(this Dictionary<K, V> dic, in K key, in V value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.ContainsKey(key))
            {
                dic[key] = value;
                return true;
            }

            return false;
        }
    }
}