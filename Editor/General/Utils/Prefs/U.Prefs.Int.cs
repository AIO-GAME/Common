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
            private const int IntHashCode = -198387194;

            #region 1

            /// <summary>
            /// 加载 Int
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static int LoadInt(in string field, in int def = 0)
            {
                var key = string.Concat(field.GetHashCode(), IntHashCode);
                return EditorPrefs.GetInt(key, def);
            }

            /// <summary>
            /// 保存为 Int
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveInt(in string field, in int value)
            {
                var key = string.Concat(field.GetHashCode(), IntHashCode);
                EditorPrefs.SetInt(key, value);
            }

            #endregion

            #region 2

            /// <summary>
            /// 加载 Int
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static int LoadInt<T>(in string field, in int def = 0)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), IntHashCode);
                return EditorPrefs.GetInt(key, def);
            }

            /// <summary>
            /// 保存为 Int
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveInt<T>(in string field, in int value)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), IntHashCode);
                EditorPrefs.SetInt(key, value);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 Int
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static int LoadInt<T>(in T data, in string field, in int def = 0)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), IntHashCode);
                return EditorPrefs.GetInt(key, def);
            }

            /// <summary>
            /// 保存为 Int
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveInt<T>(in T data, in string field, in int value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), IntHashCode);
                EditorPrefs.SetInt(key, value);
            }

            #endregion
        }
    }
}