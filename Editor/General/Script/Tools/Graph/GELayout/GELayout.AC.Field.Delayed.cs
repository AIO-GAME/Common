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
        #region Text Delayed Field

        /// <summary> 延迟文本 string </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static string DelayedField(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, options);
        }

        /// <summary> 延迟文本 string </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        /// <param name="style">显示风格</param>
        public static string DelayedField(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, style, options);
        }

        /// <summary> 延迟文本 string </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static string DelayedField(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, options);
        }

        /// <summary> 延迟文本 string </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string DelayedField(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(value, style, options);
        }

        /// <summary> 延迟文本 string </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static string DelayedField(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(value, options);
        }

        /// <summary> 延迟文本 string </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string DelayedField(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, style, options);
        }

        #endregion

        #region 延迟文本 Field Delayed Double

        /// <summary> 延迟文本 double </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(value, style, options);
        }

        /// <summary> 延迟文本 double </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(value, options);
        }

        /// <summary> 延迟文本 double </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(string label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, options);
        }

        /// <summary> 延迟文本 double </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, style, options);
        }

        /// <summary> 延迟文本 double </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(GUIContent label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, options);
        }

        /// <summary> 延迟文本 double </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(string label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, style, options);
        }

        #endregion

        #region 延迟文本 Field Delayed Float

        /// <summary> 延迟文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(GUIContent label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, options);
        }

        /// <summary> 延迟文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        /// <param name="style">显示风格</param>
        public static float FieldDelayed(string label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, style, options);
        }

        /// <summary> 延迟文本 float </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(string label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, options);
        }

        /// <summary> 延迟文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(value, style, options);
        }

        /// <summary> 延迟文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(value, options);
        }

        /// <summary> 延迟文本 float </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, style, options);
        }

        #endregion

        #region 延迟文本 Field Delayed Int

        /// <summary> 延迟文本 int </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, options);
        }

        /// <summary> 延迟文本 int </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        /// <param name="style">显示风格</param>
        public static float FieldDelayed(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, style, options);
        }

        /// <summary> 延迟文本 int </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, options);
        }

        /// <summary> 延迟文本 int </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(value, style, options);
        }

        /// <summary> 延迟文本 int </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(value, options);
        }

        /// <summary> 延迟文本 int </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, style, options);
        }

        #endregion
    }
}