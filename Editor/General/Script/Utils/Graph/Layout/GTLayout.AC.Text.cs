/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Internal;
using Object = UnityEngine.Object;

namespace UnityEditor
{
    public partial class GTLayout
    {
        public partial class AC
        {
            #region 文本文本框 FieldText

            /// <summary> 文本文本框 FieldText </summary>
            /// <param name="text">值</param>
            /// <param name="options">排版格式</param>
            public static string FieldText(string text, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextField(text, options);
            }

            /// <summary> 文本文本框 FieldText </summary>
            /// <param name="label">标签</param>
            /// <param name="text">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string FieldText(GUIContent label, string text, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextField(label, text, options);
            }

            /// <summary> 文本文本框 FieldText </summary>
            /// <param name="label">标签</param>
            /// <param name="text">值</param>
            /// <param name="options">排版格式</param>
            public static string FieldText(GUIContent label, string text, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextField(label, text, options);
            }

            /// <summary> 文本文本框 FieldText </summary>
            /// <param name="label">标签</param>
            /// <param name="text">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string FieldText(string label, string text, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextField(label, text, options);
            }

            /// <summary> 文本文本框 FieldText </summary>
            /// <param name="label">标签</param>
            /// <param name="text">值</param>
            /// <param name="options">排版格式</param>
            public static string FieldText(string label, string text, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextField(label, text, options);
            }

            /// <summary> 文本文本框 FieldText </summary>
            /// <param name="text">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string FieldText(string text, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextField(text, style, options);
            }

            #endregion

            #region Text Delayed Field

            /// <summary> 延迟文本 string </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void TextDelayedField(SerializedProperty value, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.DelayedTextField(value, label, options);
            }

            /// <summary> 延迟文本 string </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static void TextDelayedField(SerializedProperty value, params GUILayoutOption[] options)
            {
                EditorGUILayout.DelayedTextField(value, options);
            }

            /// <summary> 延迟文本 string </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static string TextDelayedField(GUIContent label, string value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedTextField(label, value, options);
            }

            /// <summary> 延迟文本 string </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            /// <param name="style">显示风格</param>
            public static string TextDelayedField(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedTextField(label, value, style, options);
            }

            /// <summary> 延迟文本 string </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static string TextDelayedField(string label, string value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedTextField(label, value, options);
            }

            /// <summary> 延迟文本 string </summary>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string TextDelayedField(string value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedTextField(value, style, options);
            }

            /// <summary> 延迟文本 string </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static string TextDelayedField(string value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedTextField(value, options);
            }

            /// <summary> 延迟文本 string </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string TextDelayedField(string label, string value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedTextField(label, value, style, options);
            }

            #endregion

            #region Text Area

            /// <summary> 文本区域 TextArea </summary>
            /// <param name="text">文本内容</param>
            /// <param name="options">排版格式</param>
            public static string TextArea(string text, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextArea(text, options);
            }

            /// <summary> 文本区域 TextArea </summary>
            /// <param name="text">文本内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string TextArea(string text, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TextArea(text, style, options);
            }

            #endregion
        }
    }
}