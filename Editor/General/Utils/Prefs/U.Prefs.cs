/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-30
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEditor;
using UnityEngine;


namespace AIO.UEditor
{
    public partial class EHelper
    {
        /// <summary>
        /// Prefs
        /// </summary>
        public static partial class Prefs
        {
            /// <summary>
            /// 判断是否存在Key
            /// </summary>
            public static bool HasKey(in string key)
            {
                return EditorPrefs.HasKey(key);
            }

            /// <summary>
            /// 删除全部
            /// </summary>
            public static void DeleteAll()
            {
                EditorPrefs.DeleteAll();
            }

            /// <summary>
            /// 删除指定key
            /// </summary>
            public static void DeleteKey(in string key)
            {
                EditorPrefs.DeleteKey(key);
            }

            private static int CODE = Application.dataPath.GetHashCode();

            private static string CombineKey(in string field)
            {
                return string.Concat(CODE, field.GetHashCode());
            }

            private static string CombineKey(in string field, string code)
            {
                return string.Concat(CODE, field.GetHashCode(), code);
            }

            private static string CombineKey(in string field, int code)
            {
                return string.Concat(CODE, field.GetHashCode(), code);
            }

            private static string CombineKey<T>(in string field, int code)
            {
                return string.Concat(CODE, typeof(T).FullName, field.GetHashCode(), code);
            }
        }
    }
}