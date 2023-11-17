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
            private const int FloatHashCode = -1247861489;

            private static float CommonLoadFloat(in string key, in float def)
            {
                if (!EditorPrefs.HasKey(key)) return def;
                return EditorPrefs.GetFloat(key);
            }

            private static void CommonSaveFloat(in string key, in float value)
            {
                EditorPrefs.SetFloat(key, value);
            }

            #region 1

            /// <summary>
            /// 加载 Float
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static float LoadFloat(in string field, in float def = 0)
            {
                var key = string.Concat(field.GetHashCode(), FloatHashCode);
                return CommonLoadFloat(key, def);
            }

            /// <summary>
            /// 保存为 Float
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveFloat(in string field, in float value)
            {
                var key = string.Concat(field.GetHashCode(), FloatHashCode);
                CommonSaveFloat(key, value);
            }

            #endregion

            #region 2

            /// <summary>
            /// 加载 Float
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static float LoadFloat<T>(in string field, in float def = 0)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), FloatHashCode);
                return CommonLoadFloat(key, def);
            }

            /// <summary>
            /// 保存为 Float
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveFloat<T>(in string field, in float value)
            {
                var key = string.Concat(typeof(T).FullName, field.GetHashCode(), FloatHashCode);
                CommonSaveFloat(key, value);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 Float
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static float LoadFloat<T>(in T data, in string field, in float def = 0)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), FloatHashCode);
                return CommonLoadFloat(key, def);
            }

            /// <summary>
            /// 保存为 Float
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveFloat<T>(in T data, in string field, in float value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), FloatHashCode);
                CommonSaveFloat(key, value);
            }

            #endregion
        }
    }
}