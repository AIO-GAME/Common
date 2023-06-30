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
        #region 标签字段 FieldTag

        /// <summary> 标签字段 FieldTag </summary>
        /// <param name="tag"></param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Tag(string tag, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(tag, style, options);
        }

        /// <summary> 标签字段 FieldTag </summary>
        /// <param name="label">标签</param>
        /// <param name="tag">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Tag(GUIContent label, string tag, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, tag, style, options);
        }

        /// <summary> 标签字段 FieldTag </summary>
        /// <param name="tag">值</param>
        /// <param name="options">排版格式</param>
        public static string Tag(string tag, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(tag, options);
        }

        /// <summary> 标签字段 FieldTag </summary>
        /// <param name="label">标签</param>
        /// <param name="tag">值</param>
        /// <param name="options">排版格式</param>
        public static string Tag(GUIContent label, string tag, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, tag, options);
        }

        /// <summary> 标签字段 FieldTag </summary>
        /// <param name="label">标签</param>
        /// <param name="tag">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Tag(string label, string tag, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, tag, style, options);
        }

        /// <summary> 标签字段 FieldTag </summary>
        /// <param name="label">标签</param>
        /// <param name="tag">值</param>
        /// <param name="options">排版格式</param>
        public static string Tag(string label, string tag, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, tag, options);
        }

        #endregion
    }
}