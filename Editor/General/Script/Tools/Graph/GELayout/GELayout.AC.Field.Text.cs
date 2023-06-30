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
    public partial class GELayout
    {
        #region 文本文本框 FieldText

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Field(Texture label, string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), text, style, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="options">排版格式</param>
        public static string Field(Texture label, string text, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), text, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Field(GUIContent label, string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, text, style, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="options">排版格式</param>
        public static string Field(GUIContent label, string text, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, text, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, text, style, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, string text, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, text, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="text">值</param>
        /// <param name="options">排版格式</param>
        public static string Field(string text, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(text, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="text">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Field(string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(text, style, options);
        }

        #endregion
    }
}