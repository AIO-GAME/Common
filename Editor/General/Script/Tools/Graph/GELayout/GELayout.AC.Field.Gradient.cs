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
        #region 文本 渐变字段 Field Gradient

#if UNITY_2018_1_OR_NEWER

        /// <summary> 文本 渐变字段 FieldGradient </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="hdr">hdr状态</param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(GUIContent label, Gradient value, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(label, value, hdr, options);
        }

        /// <summary> 文本 渐变字段 FieldGradient </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(GUIContent label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(label, value, options);
        }

        /// <summary> 文本 渐变字段 FieldGradient </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(string label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(label, value, options);
        }

        /// <summary> 文本 渐变字段 FieldGradient </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(value, options);
        }
#endif

        #endregion
    }
}
