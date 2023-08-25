﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;
using UnityEngine.Internal;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        #region Label Field

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="label2">向右显示的标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Label(GUIContent label, GUIContent label2, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, label2, style, options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="options">排版格式</param>
        public static void Label(string label, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="options">排版格式</param>
        public static void Label(object label, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label.ToString(), options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="options">排版格式</param>
        public static void Label(GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="label2">向右显示的标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Label(string label, string label2, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, label2, style, options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="label2">向右显示的标签</param>
        /// <param name="options">排版格式</param>
        public static void Label(string label, string label2, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, label2, options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Label(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, style, options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Label(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, style, options);
        }

        /// <summary> 标签文本框 FieldLabel </summary>
        /// <param name="label">第一个标签</param>
        /// <param name="label2">向右显示的标签</param>
        /// <param name="options">排版格式</param>
        public static void Label(GUIContent label, GUIContent label2, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, label2, options);
        }

        #endregion

        #region Label Prefix

        /// <summary> 前置标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="followingStyle">后面的显示风格</param>
        /// <param name="labelStyle"></param>
        public static void LabelPrefix(GUIContent label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle, labelStyle);
        }

        /// <summary> 前置标签 </summary>
        /// <param name="label">标签</param>
        [ExcludeFromDocs]
        public static void LabelPrefix(GUIContent label)
        {
            EditorGUILayout.PrefixLabel(label);
        }

        /// <summary> 前置标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="followingStyle">后面的显示风格</param>
        /// <param name="style">显示风格</param>
        public static void LabelPrefix(string label, GUIStyle followingStyle, GUIStyle style)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle, style);
        }

        /// <summary> 前置标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="followingStyle">后面的显示风格</param>
        public static void LabelPrefix(GUIContent label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle);
        }

        /// <summary> 前置标签 </summary>
        /// <param name="label">标签</param>
        /// <param name="followingStyle">后面的显示风格</param>
        public static void LabelPrefix(string label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle);
        }

        /// <summary> 前置标签 </summary>
        /// <param name="label">标签</param>
        [ExcludeFromDocs]
        public static void LabelPrefix(string label)
        {
            EditorGUILayout.PrefixLabel(label);
        }

        /// <summary> 前置标签 </summary>
        /// <param name="label">标签</param>
        [ExcludeFromDocs]
        public static void LabelPrefix(int label)
        {
            EditorGUILayout.PrefixLabel(label.ToString());
        }

        #endregion

        #region Label Selectable

        /// <summary> 可选择标签 SelectableLabel </summary>
        /// <param name="text">文本内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void LabelSelectable(string text, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(text, style, options);
        }

        /// <summary> 可选择标签 SelectableLabel </summary>
        /// <param name="text">文本内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void LabelSelectable(int text, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(text.ToString(), style, options);
        }

        /// <summary> 可选择标签 SelectableLabel </summary>
        /// <param name="text">文本内容</param>
        /// <param name="options">排版格式</param>
        public static void LabelSelectable(string text, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(text, options);
        }

        /// <summary> 可选择标签 SelectableLabel </summary>
        /// <param name="text">文本内容</param>
        /// <param name="options">排版格式</param>
        public static void LabelSelectable(int text, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(text.ToString(), options);
        }

        #endregion

        #region Label

        //
        // /// <summary> 标签 </summary>
        // /// <param name="image">图标</param>
        // /// <param name="options">排版格式</param>
        // public static void Label(Texture image, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(image, options);
        // }
        //
        // /// <summary> 标签 </summary>
        // /// <param name="obj">内容</param>
        // /// <param name="options">排版格式</param>
        // public static void Label(object obj, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(obj.ToString(), options);
        // }
        //
        // /// <summary> 标签 </summary>
        // /// <param name="text">文本内容</param>
        // /// <param name="options">排版格式</param>
        // public static void Label(string text, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(text, options);
        // }
        //
        // /// <summary> 标签 </summary>
        // /// <param name="text">文本内容</param>
        // /// <param name="options">排版格式</param>
        // public static void Label(GUIContent text, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(text, options);
        // }
        //
        //
        // /// <summary> 标签 </summary>
        // /// <param name="image">图标</param>
        // /// <param name="style">显示风格</param>
        // /// <param name="options">排版格式</param>
        // public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(image, style, options);
        // }
        //
        // /// <summary> 标签 </summary>
        // /// <param name="text">文本内容</param>
        // /// <param name="style">显示风格</param>
        // /// <param name="options">排版格式</param>
        // public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(text, style, options);
        // }
        //
        // /// <summary> 标签 </summary>
        // /// <param name="obj">内容</param>
        // /// <param name="options">排版格式</param>
        // /// <param name="style">显示风格</param>
        // public static void Label(object obj, GUIStyle style, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(obj.ToString(), style, options);
        // }
        //
        // /// <summary> 标签 </summary>
        // /// <param name="text">文本内容</param>
        // /// <param name="style">显示风格</param>
        // /// <param name="options">排版格式</param>
        // public static void Label(GUIContent text, GUIStyle style, params GUILayoutOption[] options)
        // {
        //     GUILayout.Label(text, style, options);
        // }

        #endregion
    }
}
