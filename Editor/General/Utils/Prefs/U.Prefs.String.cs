/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-10-28
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

#region

using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public partial class EHelper
    {
        #region Nested type: Prefs

        public partial class Prefs
        {
            private const int StringHashCode = 1186655223;

            #region 1

            /// <summary>
            /// 保存为 字符串
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveString(in string field, in string value)
            {
                var key = CombineKey(field, StringHashCode);
                EditorPrefs.SetString(key, value);
            }

            /// <summary>
            /// 加载 String
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static string LoadString(in string field, in string def = null)
            {
                var key = CombineKey(field, StringHashCode);
                return EditorPrefs.GetString(key, def);
            }

            #endregion

            #region 2

            /// <summary>
            /// 保存为 字符串
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveString<T>(in string field, in string value)
            {
                var key = CombineKey<T>(field, StringHashCode);
                EditorPrefs.SetString(key, value);
            }

            /// <summary>
            /// 加载 String
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static string LoadString<T>(in string field, in string def = null)
            {
                var key = CombineKey<T>(field, StringHashCode);
                return EditorPrefs.GetString(key, def);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 字符串
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static string LoadString<T>(in T data, in string field, in string def = null)
            {
                var key = CombineKey<T>(field, StringHashCode);
                return EditorPrefs.GetString(key, def);
            }

            /// <summary>
            /// 保存为 字符串
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveString<T>(in T data, in string field, in string value)
            {
                var key = CombineKey<T>(field, StringHashCode);
                EditorPrefs.SetString(key, value);
            }

            #endregion
        }

        #endregion
    }
}