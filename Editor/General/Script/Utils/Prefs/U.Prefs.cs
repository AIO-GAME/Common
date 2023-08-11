/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-30
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEditor;


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
        }
    }
}
