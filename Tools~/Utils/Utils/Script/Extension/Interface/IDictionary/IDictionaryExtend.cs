using System;
using System.Collections.Generic;

namespace AIO
{
    public static partial class DictionaryExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DictTo<T>(this IDictionary<string, object> dict, string key)
        {
            if (dict.TryGetValue(key, out var o))
            {
                return CastTo<T>(o);
            }

            return default;
        }

        private static T CastTo<T>(object value)
        {
            if (value == null) return default;
            object result;
            var type = typeof(T);
            if (type.IsEnum)
            {
                result = Enum.Parse(type, value.ToString());
            }
            else if (type.IsClass)
            {
                result = value;
            }
            else
            {
                result = Convert.ChangeType(value, type);
            }

            return (T)result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static TV TryGet<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV def = default(TV))
        {
            if (dict == null) return default;
            if (dict.TryGetValue(key, out var r)) return r;
            return def;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static TV TryGetOrCreate<TK, TV>(this IDictionary<TK, TV> dict, TK key) where TV : new()
        {
            if (dict == null) return default;
            if (dict.TryGetValue(key, out var r)) return r;
            r = new TV();
            dict.Add(key, r);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static TV TryRemove<TK, TV>(this IDictionary<TK, TV> dict, TK key)
        {
            if (dict == null) return default;
            if (dict.ContainsKey(key))
            {
                var r = dict[key];
                dict.Remove(key);
                return r;
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="others"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        public static void Union<TK, TV>(this IDictionary<TK, TV> dict, IDictionary<TK, TV> others)
        {
            if (dict == null || others == null) return;
            foreach (var kv in others)
            {
                if (!dict.ContainsKey(kv.Key))
                {
                    dict.Add(kv.Key, kv.Value);
                }
            }
        }
    }
}