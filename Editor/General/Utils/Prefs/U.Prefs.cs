/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-30

|||✩ - - - - - |*/

#region

using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public partial class EHelper
    {
        #region Nested type: Prefs

        /// <summary>
        /// Prefs
        /// </summary>
        public static partial class Prefs
        {
            private static int _CODE;

            private static int CODE
            {
                get
                {
                    if (_CODE != 0) return _CODE;
                    var key = string.Concat(typeof(Prefs).FullName, "CODE");
                    if (EditorPrefs.HasKey(key))
                    {
                        _CODE = EditorPrefs.GetInt(key);
                    }
                    else
                    {
                        _CODE = System.IO.Path.Combine(System.IO.Path.GetTempPath(),
                                                       System.IO.Path.GetTempFileName())
                                      .GetHashCode();
                        EditorPrefs.SetInt(key, _CODE);
                    }

                    return _CODE;
                }
            }

            /// <summary>
            /// 判断是否存在Key
            /// </summary>
            public static bool HasKey(in string key) { return EditorPrefs.HasKey(key); }

            /// <summary>
            /// 删除全部
            /// </summary>
            public static void DeleteAll() { EditorPrefs.DeleteAll(); }

            /// <summary>
            /// 删除指定key
            /// </summary>
            public static void DeleteKey(in string key) { EditorPrefs.DeleteKey(key); }

            private static string CombineKey(in string field) { return string.Concat(CODE, field.GetHashCode()); }

            private static string CombineKey(in string field, string code) { return string.Concat(CODE, field.GetHashCode(), code); }

            private static string CombineKey(in string field, int code) { return string.Concat(CODE, field.GetHashCode(), code); }

            private static string CombineKey<T>(in string field, int code) { return string.Concat(CODE, typeof(T).FullName, field.GetHashCode(), code); }
        }

        #endregion
    }
}