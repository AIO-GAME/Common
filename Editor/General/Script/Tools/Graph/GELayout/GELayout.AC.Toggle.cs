/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        /// <summary> 开关按钮 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static bool Toggle(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary> 开关按钮 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static bool Toggle(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary> 开关按钮 </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static bool Toggle(bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, options);
        }

        /// <summary> 开关按钮 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static bool Toggle(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary> 开关按钮 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static bool Toggle(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary> 开关按钮 </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static bool Toggle(bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, style, options);
        }

        /// <summary> 左侧开关 右侧标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static bool ToggleLeft(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, options);
        }

        /// <summary> 左侧开关 右侧标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static bool ToggleLeft(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, options);
        }

        /// <summary> 左侧开关 右侧标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="labelStyle">标签显示风格</param>
        /// <param name="options">排版格式</param>
        public static bool ToggleLeft(string label, bool value, GUIStyle labelStyle, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, labelStyle, options);
        }

        /// <summary> 左侧开关 右侧标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="labelStyle">标签显示风格</param>
        /// <param name="options">排版格式</param>
        public static bool ToggleLeft(GUIContent label, bool value, GUIStyle labelStyle, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, labelStyle, options);
        }
    }
}
