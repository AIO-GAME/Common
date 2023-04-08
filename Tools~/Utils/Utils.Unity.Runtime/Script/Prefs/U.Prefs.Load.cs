using System;
using UnityEngine;

public static partial class UtilsEngine
{
    public static partial class Prefs
    {
        /// <summary>
        /// 加载字符串
        /// </summary>
        /// <param name="key">类型</param>
        /// <param name="field">字段名称</param>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns>字符串</returns>
        public static string LoadString<T>(in T key, in string field)
        {
            var fullName = key.GetType().FullName;
            if (string.IsNullOrEmpty(fullName)) return string.Empty;
            return PlayerPrefs.GetString(string.Concat(fullName.GetHashCode(), field.GetHashCode()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int LoadInt<T>(in T key, in string field)
        {
            var fullName = key.GetType().FullName;
            if (string.IsNullOrEmpty(fullName)) return 0;
            return PlayerPrefs.GetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool LoadBool<T>(in T key, in string field)
        {
            var fullName = key.GetType().FullName;
            if (string.IsNullOrEmpty(fullName)) return false;
            return PlayerPrefs.GetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode())) == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string LoadString(in string key, in string def = null)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_String"))) return def;
            return PlayerPrefs.GetString(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int LoadInt(in string key, in int def = 0)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Int"))) return def;
            return PlayerPrefs.GetInt(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadEnum<T>(in string key, in T def = default) where T : Enum
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Enum"))) return def;
            return (T)Enum.Parse(typeof(T), PlayerPrefs.GetInt(key).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadJsonData<T>(in string key, in T def = default)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Json"))) return def;
            return Utils.Json.Deserialize<T>(PlayerPrefs.GetString(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static float LoadFloat(in string key, in float def = 0)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Float"))) return def;
            return PlayerPrefs.GetFloat(key);
        }
    }
}