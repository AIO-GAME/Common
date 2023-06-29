/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using UnityEngine;

namespace UnityEditor
{
    public partial class GELayout
    {
        #region 矩形字段 Field Rect

        /// <summary> 矩形字段 FieldRect </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Rect Field(Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(value, options);
        }

        /// <summary> 矩形字段 FieldRect </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Rect Field(string label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(label, value, options);
        }

        /// <summary> 矩形字段 FieldRect </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Rect Field(GUIContent label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(label, value, options);
        }

        #endregion

        #region 矩形字段 Field Rect Int

        /// <summary> 矩形字段 FieldRectInt </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static RectInt Field(GUIContent label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(label, value, options);
        }

        /// <summary> 矩形字段 FieldRectInt </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static RectInt Field(string label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(label, value, options);
        }


        /// <summary> 矩形字段 FieldRectInt </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static RectInt Field(RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(value, options);
        }

        #endregion
    }
}