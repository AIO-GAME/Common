/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
#if UNITY_2021_1_OR_NEWER
    public partial class GELayout
    {
        /// <summary>
        /// Link按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static bool ButtonLink(Texture label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(new GUIContent(label), options);
        }

        /// <summary>
        /// Link按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="tooltip">提示</param>
        public static bool ButtonLink(Texture label, string tooltip, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(new GUIContent(label, tooltip), options);
        }

        /// <summary>
        /// Link按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="tooltip">提示</param>
        public static bool ButtonLink(string label, string tooltip, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(new GUIContent(label, tooltip), options);
        }

        /// <summary>
        /// Link按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static bool ButtonLink(GUIContent label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(label, options);
        }
    }
#endif
}