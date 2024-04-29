#region

using System;
using UnityEngine;

#endregion

namespace AIO
{
    partial class RHelper
    {
        #region Nested type: Prefs

        partial class Prefs
        {
            /// <summary>
            /// 加载字符串
            /// </summary>
            /// <param name="key">类型</param>
            /// <param name="field">字段名称</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static string LoadString<T>(in T key, in string field)
            {
                var fullName = key.GetType().FullName;
                if (string.IsNullOrEmpty(fullName)) return string.Empty;
                return PlayerPrefs.GetString(string.Concat(fullName.GetHashCode(), field.GetHashCode()));
            }

            /// <summary>
            /// 加载Int
            /// </summary>
            /// <param name="key">类型</param>
            /// <param name="field">字段名称</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static int LoadInt<T>(in T key, in string field)
            {
                var fullName = key.GetType().FullName;
                if (string.IsNullOrEmpty(fullName)) return 0;
                return PlayerPrefs.GetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()));
            }

            /// <summary>
            /// 加载Bool
            /// </summary>
            /// <param name="key">类型</param>
            /// <param name="field">字段名称</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static bool LoadBool<T>(in T key, in string field)
            {
                var fullName = key.GetType().FullName;
                if (string.IsNullOrEmpty(fullName)) return false;
                return PlayerPrefs.GetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode())) == 1;
            }

            /// <summary>
            /// 加载String
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static string LoadString(in string key, in string def = null)
            {
                if (PlayerPrefs.HasKey(string.Concat(key, "_String"))) return def;
                return PlayerPrefs.GetString(key);
            }

            /// <summary>
            /// 加载Int
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static int LoadInt(in string key, in int def = 0)
            {
                if (PlayerPrefs.HasKey(string.Concat(key, "_Int"))) return def;
                return PlayerPrefs.GetInt(key);
            }

            /// <summary>
            /// 加载Enum
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">枚举泛型</typeparam>
            /// <returns>返回值</returns>
            public static T LoadEnum<T>(in string key, in T def = default)
            where T : Enum
            {
                if (PlayerPrefs.HasKey(string.Concat(key, "_Enum"))) return def;
                return (T)Enum.Parse(typeof(T), PlayerPrefs.GetInt(key).ToString());
            }

            /// <summary>
            /// 加载Json数据
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型</typeparam>
            /// <returns>返回值</returns>
            public static T LoadJsonData<T>(in string key, in T def = default)
            {
                if (PlayerPrefs.HasKey(string.Concat(key, "_Json"))) return def;
                return AHelper.Json.Deserialize<T>(PlayerPrefs.GetString(key));
            }

            /// <summary>
            /// 加载Float
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static float LoadFloat(in string key, in float def = 0)
            {
                if (PlayerPrefs.HasKey(string.Concat(key, "_Float"))) return def;
                return PlayerPrefs.GetFloat(key);
            }
        }

        #endregion
    }
}