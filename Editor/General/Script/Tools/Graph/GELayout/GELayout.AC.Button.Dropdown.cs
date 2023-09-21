/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(Texture label, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(Texture label, FocusType focusType, GUIStyle style,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), focusType, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static bool ButtonDropdown(Texture label, FocusType focusType, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), focusType, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        /// <param name="tooltip">提示</param>
        public static bool ButtonDropdown(string label, string tooltip, GUIStyle style,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label, tooltip), FocusType.Passive, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="tooltip">提示</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(string label, string tooltip, FocusType focusType, GUIStyle style,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label, tooltip), focusType, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="tooltip">提示</param>
        public static bool ButtonDropdown(string label, string tooltip, FocusType focusType,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label, tooltip), focusType, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(string label, FocusType focusType, GUIStyle style,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), focusType, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static bool ButtonDropdown(string label, FocusType focusType, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), focusType, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(label, FocusType.Passive, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(GUIContent label, FocusType focusType, GUIStyle style,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(label, focusType, style, options);
        }

        /// <summary>
        /// 下拉按钮
        /// </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static bool ButtonDropdown(GUIContent label, FocusType focusType, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(label, focusType, options);
        }
    }
}