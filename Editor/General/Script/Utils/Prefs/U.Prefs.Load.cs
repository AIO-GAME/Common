/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-30
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;

namespace UnityEditor
{
    public partial class UtilsEditor
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
                if (EditorPrefs.HasKey(string.Concat(key, "_String"))) return def;
                return EditorPrefs.GetString(key);
            }

            /// <summary>
            /// 加载Int
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static int LoadInt(in string key, in int def = 0)
            {
                if (EditorPrefs.HasKey(string.Concat(key, "_Int"))) return def;
                return EditorPrefs.GetInt(key);
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
                if (EditorPrefs.HasKey(string.Concat(key, "_Enum"))) return def;
                return (T)Enum.Parse(typeof(T), EditorPrefs.GetInt(key).ToString());
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
                if (EditorPrefs.HasKey(string.Concat(key, "_Json"))) return def;
                return UtilsGen.Json.Deserialize<T>(EditorPrefs.GetString(key));
            }

            /// <summary>
            /// 加载Float
            /// </summary>
            /// <param name="key">key</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static float LoadFloat(in string key, in float def = 0)
            {
                if (EditorPrefs.HasKey(string.Concat(key, "_Float"))) return def;
                return EditorPrefs.GetFloat(key);
            }
        }
    }
}