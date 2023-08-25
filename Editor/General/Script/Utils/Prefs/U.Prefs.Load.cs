/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-30
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.ComponentModel;
using UnityEditor.TestTools;
using UnityEngine;
using UnityEditor;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        public partial class Prefs
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
                return EditorPrefs.GetString(string.Concat(fullName.GetHashCode(), field.GetHashCode()));
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
                return EditorPrefs.GetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()));
            }

            /// <summary>
            /// 加载Bool
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <returns>返回值</returns>
            public static bool LoadBool(in string field)
            {
                return EditorPrefs.GetInt(string.Concat(field, "_Boolean")) == 1;
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
                return EditorPrefs.GetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode())) == 1;
            }

            /// <summary>
            /// 加载String
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static string LoadString(in string key, in string def = null)
            {
                var keyname = string.Concat(key, "_String");
                if (!EditorPrefs.HasKey(keyname)) return def;
                return EditorPrefs.GetString(keyname);
            }

            /// <summary>
            /// 加载Int
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static int LoadInt(in string key, in int def = 0)
            {
                var keyname = string.Concat(key, "_Int");
                if (!EditorPrefs.HasKey(keyname)) return def;
                return EditorPrefs.GetInt(keyname);
            }

            /// <summary>
            /// 加载Enum
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">枚举泛型</typeparam>
            /// <returns>返回值</returns>
            public static T LoadEnum<T>(in string key, in T def = default) where T : Enum
            {
                var keyname = string.Concat(key, "_Enum");
                if (!EditorPrefs.HasKey(keyname)) return def;
                return (T)Enum.Parse(typeof(T), EditorPrefs.GetInt(keyname).ToString());
            }

            /// <summary>
            /// 加载Enum
            /// </summary>
            public static T2 LoadEnum<T1, T2>(T1 key, in string field, T2 value = default) where T2 : struct, Enum
            {
                var fullName = key.GetType().FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    var vt = EditorPrefs.GetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()));
                    value = (T2)Enum.Parse(typeof(T2), vt.ToString());
                }

                return value;
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
                var keyname = string.Concat(key, "_Json");
                if (!EditorPrefs.HasKey(keyname)) return def;
                var content = EditorPrefs.GetString(keyname);
                if (string.IsNullOrEmpty(content)) return def;
                return AHelper.Json.Deserialize<T>(content);
            }

            /// <summary>
            /// 加载Float
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static float LoadFloat(in string key, in float def = 0)
            {
                var keyname = string.Concat(key, "_Float");
                if (!EditorPrefs.HasKey(keyname)) return def;
                return EditorPrefs.GetFloat(keyname);
            }
        }
    }
}
