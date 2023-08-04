/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public static partial class GELayout
    {
        #region Text Area

        /// <summary> 文本区域 TextArea </summary>
        /// <param name="text">文本内容</param>
        /// <param name="options">排版格式</param>
        public static string Area(string text, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextArea(text, options);
        }

        /// <summary> 文本区域 TextArea </summary>
        /// <param name="text">文本内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Area(string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextArea(text, style, options);
        }

        #endregion
    }
}