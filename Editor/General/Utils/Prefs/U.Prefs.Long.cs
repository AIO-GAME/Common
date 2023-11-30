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
            private const int LongHashCode = 576184436;

            private static long CommonLoadLong(in string key, in long def)
            {
                if (!EditorPrefs.HasKey(key)) return def;
                var value = EditorPrefs.GetString(key);
                return long.TryParse(value, out var result) ? result : def;
            }

            private static void CommonSaveLong(in string key, in long value)
            {
                EditorPrefs.SetString(key, value.ToString());
            }

            #region 1

            /// <summary>
            /// 加载 Long
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static long LoadLong(in string field, in long def = 0)
            {
                var key = CombineKey(field, LongHashCode);
                return CommonLoadLong(key, def);
            }

            /// <summary>
            /// 保存为 Long
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveLong(in string field, in long value)
            {
                var key = CombineKey(field, LongHashCode);
                CommonSaveLong(key, value);
            }

            #endregion

            #region 2

            /// <summary>
            /// 加载 Long
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static long LoadLong<T>(in string field, in long def = 0)
            {
                var key = CombineKey<T>(field, LongHashCode);
                return CommonLoadLong(key, def);
            }

            /// <summary>
            /// 保存为 Long
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveLong<T>(in string field, in long value)
            {
                var key = CombineKey<T>(field, LongHashCode);
                CommonSaveLong(key, value);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 Long
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static long LoadLong<T>(in T data, in string field, in long def = 0)
            {
                var key = CombineKey<T>(field, LongHashCode);
                return CommonLoadLong(key, def);
            }

            /// <summary>
            /// 保存为 Long
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="T">泛型类型</typeparam>
            public static void SaveLong<T>(in T data, in string field, in long value)
            {
                var key = CombineKey<T>(field, LongHashCode);
                CommonSaveLong(key, value);
            }

            #endregion
        }
    }
}