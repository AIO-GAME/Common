/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-12-06                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.Collections;

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class DictionaryExtend
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        public static object Get(this IDictionary dic, in string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            if (dic == null) return null;

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
            if (key == null || (string)key == "") return null;
            if (dic == null) return null;

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
            if (dic == null || string.IsNullOrEmpty(key)) return default;

            if (dic.Contains(key) && dic[key] != null)
            {
                if (dic[key] is T t) return t;
            }

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
            if (dic == null || key == null) return default;

            if (dic.Contains(key) && dic[key] != null)
            {
                if (dic[key] is T t) return t;
            }

            return default;
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void Set(this IDictionary dic, in string key, in object value)
        {
            if (dic == null || string.IsNullOrEmpty(key)) return;

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
            if (dic == null || key == null) return;

            if (dic.Contains(key)) dic[key] = value;
            else dic.Add(key, value);
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="T">任意泛型</typeparam>
        public static bool TryGet<T>(this IDictionary dic, in string key, ref T value)
        {
            if (dic == null || string.IsNullOrEmpty(key)) return false;
            if (dic.Contains(key) && dic[key] != null)
            {
                if (dic[key].GetType() == typeof(T))
                {
                    value = (T)dic[key];
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="T">任意泛型</typeparam>
        public static bool TryGet<T>(this IDictionary dic, in object key, ref T value)
        {
            if (dic == null || key == null) return false;
            if (dic.Contains(key) && dic[key] != null)
            {
                if (dic[key].GetType() == typeof(T))
                {
                    value = (T)dic[key];
                    return true;
                }
            }

            return false;
        }
    }
}