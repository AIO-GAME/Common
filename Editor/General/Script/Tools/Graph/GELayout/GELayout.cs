/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        #region 隔行

        /// <summary>
        /// 分隔符
        /// </summary>
        public static void Separator()
        {
            EditorGUILayout.Separator();
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        public static void Separator(int num)
        {
            for (var i = 0; i < num; i++) EditorGUILayout.Separator();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space()
        {
            EditorGUILayout.Space();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(int num)
        {
            for (var i = 0; i < num; i++) EditorGUILayout.Space();
        }

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(int num, float width, bool expand = true)
        {
            for (var i = 0; i < num; i++) EditorGUILayout.Space(width, expand);
        }

#endif

        #endregion
    }
}