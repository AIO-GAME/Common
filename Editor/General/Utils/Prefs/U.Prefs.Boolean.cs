/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-28               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using UnityEditor;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        public partial class Prefs
        {
            private const int BooleanHashCode = 2010873062;

            #region 1

            /// <summary>
            /// 加载 Bool
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static bool LoadBoolean(in string field, in bool def = false)
            {
                var key = string.Concat(field.GetHashCode(), BooleanHashCode);
                return EditorPrefs.GetInt(key, def ? 1 : 0) == 1;
            }

            /// <summary>
            /// 保存为 Bool
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveBoolean(in string field, in bool value)
            {
                var key = string.Concat(field.GetHashCode(), BooleanHashCode);
                EditorPrefs.SetInt(key, value ? 1 : 0);
            }

            /// <summary>
            /// 反转bool值
            /// </summary>
            /// <param name="field">字段名称</param>
            public static void ReverseBoolean(in string field)
            {
                var key = string.Concat(field.GetHashCode(), BooleanHashCode);
                var value = EditorPrefs.GetInt(key, 0);
                EditorPrefs.SetInt(key, value == 0 ? 1 : 0);
            }

            #endregion

            #region 2

            /// <summary>
            /// 加载 Bool
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static bool LoadBoolean<T>(in string field, in bool def = false)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), BooleanHashCode);
                return EditorPrefs.GetInt(key, def ? 1 : 0) == 1;
            }

            /// <summary>
            /// 保存为 Bool
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveBoolean<T>(in string field, in bool value)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), BooleanHashCode);
                EditorPrefs.SetInt(key, value ? 1 : 0);
            }

            /// <summary>
            /// 反转bool值
            /// </summary>
            /// <param name="field">字段名称</param>
            public static void ReverseBoolean<T>(in string field)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), BooleanHashCode);
                var value = EditorPrefs.GetInt(key, 0);
                EditorPrefs.SetInt(key, value == 0 ? 1 : 0);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 Bool
            /// </summary>
            /// <param name="data">值类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static bool LoadBoolean<T>(in T data, in string field, in bool def = false)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), BooleanHashCode);
                return EditorPrefs.GetInt(key, def ? 1 : 0) == 1;
            }

            /// <summary>
            /// 保存为 Bool
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveBoolean<T>(T data, in string field, in bool value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), BooleanHashCode);
                EditorPrefs.SetInt(key, value ? 1 : 0);
            }

            /// <summary>
            /// 反转bool值
            /// </summary>
            /// <param name="data">值类型</param>
            /// <param name="field">字段名称</param>
            public static void ReverseBoolean<T>(in T data, in string field)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), BooleanHashCode);
                var value = EditorPrefs.GetInt(key, 0);
                EditorPrefs.SetInt(key, value == 0 ? 1 : 0);
            }

            #endregion
        }
    }
}