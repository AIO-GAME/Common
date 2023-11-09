/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-28               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System.Globalization;
using UnityEditor;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        public partial class Prefs
        {
            private const int JsonHashCode = -198387194;

            private static T CommonLoadJson<T>(in string key, in T def = default)
            {
                if (!EditorPrefs.HasKey(key)) return def;
                var content = EditorPrefs.GetString(key);
                if (string.IsNullOrEmpty(content)) return def;
                return AHelper.Json.Deserialize<T>(content);
            }

            private static void CommonSaveJson<T>(in string key, in T value)
            {
                EditorPrefs.SetString(key, AHelper.Json.Serialize(value));
            }

            #region 1

            /// <summary>
            /// 加载 Json
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static T LoadJson<T>(in string field, in T def = default)
            {
                var key = string.Concat(field.GetHashCode(), JsonHashCode);
                return CommonLoadJson(key, def);
            }

            /// <summary>
            /// 保存为 Json
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveJson<T>(in string field, in T value)
            {
                var key = string.Concat(field.GetHashCode(), JsonHashCode);
                CommonSaveJson(key, value);
            }

            #endregion

            #region 2

            /// <summary>
            /// 加载 Json
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <typeparam name="T1">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static T1 LoadJson<T, T1>(in string field, in T1 def = default)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), JsonHashCode);
                return CommonLoadJson(key, def);
            }

            /// <summary>
            /// 保存为 Json
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <typeparam name="T1">泛型类型</typeparam>
            public static void SaveJson<T, T1>(in string field, in T1 value)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), JsonHashCode);
                CommonSaveJson(key, value);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 Json
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <typeparam name="T1">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static T1 LoadJson<T, T1>(in T data, in string field, in T1 def = default)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), JsonHashCode);
                return CommonLoadJson(key, def);
            }

            /// <summary>
            /// 保存为 Json
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <typeparam name="T1">泛型类型</typeparam>
            public static void SaveJson<T, T1>(in T data, in string field, in T1 value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), JsonHashCode);
                CommonSaveJson(key, value);
            }

            #endregion
        }
    }
}