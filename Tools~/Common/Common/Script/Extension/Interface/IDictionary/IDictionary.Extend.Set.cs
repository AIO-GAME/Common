using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<K, V>(this IDictionary<K, V> dic, in K key, in V value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.ContainsKey(key)) dic[key] = value;
            else dic.Add(key, value);
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="K">任意泛型</typeparam>
        /// <typeparam name="V">任意泛型</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySet<K, V>(this IDictionary<K, V> dic, in K key, in V value)
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