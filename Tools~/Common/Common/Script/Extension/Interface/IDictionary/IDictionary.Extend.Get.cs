#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendIDictionary
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        public static V Get<k, V>(this IDictionary<k, V> dic, in k key)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.TryGetValue(key, out var value)) return value;
            throw new KeyNotFoundException(nameof(key));
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="defaultValue">默认值</param>
        public static V GetOrDefault<k, V>(this IDictionary<k, V> dic, in k key, in V defaultValue = default)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return dic.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="defaultValue">默认值</param>
        public static object GetOrDefault(this IDictionary dic, in object key, in object defaultValue = null)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key is null) throw new ArgumentNullException(nameof(key));

            return dic.Contains(key) ? dic[key] : defaultValue;
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
            throw new KeyNotFoundException(nameof(key));
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="defaultValue">默认值</param>
        public static object GetOrDefault(this IDictionary dic, in string key, in object defaultValue = null)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            return dic.Contains(key) ? dic[key] : defaultValue;
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
            throw new KeyNotFoundException(nameof(key));
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
            throw new KeyNotFoundException(nameof(key));
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="defaultValue">默认值</param>
        public static T GetOrDefault<T>(this IDictionary dic, in object key, in T defaultValue = default)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key is null) throw new ArgumentNullException(nameof(key));

            if (dic.Contains(key) && dic[key] is T t) return t;
            return defaultValue;
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
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="K">任意泛型</typeparam>
        /// <typeparam name="V">任意泛型</typeparam>
        public static bool TryGet<K, V>(this IDictionary<K, V> dic, in K key, out V value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.TryGetValue(key, out var value1))
            {
                value = value1;
                return true;
            }

            value = default;
            return false;
        }
    }
}