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
        #region 文本 Field Float

        /// <summary> 文本 float </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static float Field(GUIContent label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, options);
        }

        /// <summary> 文本 float </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float Field(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, style, options);
        }

        /// <summary> 文本 float </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float Field(string label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, style, options);
        }

        /// <summary> 文本 float </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static float Field(string label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, options);
        }


        /// <summary> 文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float Field(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, style, options);
        }


        /// <summary> 文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static float Field(float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, options);
        }

        #endregion

        #region 文本 Field Int

        /// <summary> 文本 Int </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Field(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, style, options);
        }

        /// <summary> 文本 Int </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static int Field(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, options);
        }

        /// <summary> 文本 Int </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Field(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, style, options);
        }

        /// <summary> 文本 Int </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static int Field(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, options);
        }

        /// <summary> 文本 Int </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Field(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, style, options);
        }

        /// <summary> 文本 Int </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static int Field(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, options);
        }

        #endregion

        #region 文本 Field Double

        /// <summary> 文本 Double </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static double Field(string label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, label, options);
        }

        /// <summary> 文本 Double </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static double Field(string label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, label, options);
        }

        /// <summary> 文本 Double </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static double Field(GUIContent label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, options);
        }

        /// <summary> 文本 Double </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static double Field(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, style, options);
        }

        /// <summary> 文本 Double </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static double Field(double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, options);
        }

        /// <summary> 文本 Double </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static double Field(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, style, options);
        }

        #endregion

        #region 文本 Field Long

        /// <summary> 文本框 FieldLong </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static long Field(long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(value, options);
        }

        /// <summary> 文本框 FieldLong </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static long Field(long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(value, style, options);
        }

        /// <summary> 文本框 FieldLong </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static long Field(string label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, options);
        }

        /// <summary> 文本框 FieldLong </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static long Field(string label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, style, options);
        }

        /// <summary> 文本框 FieldLong </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static long Field(GUIContent label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, options);
        }

        /// <summary> 文本框 FieldLong </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static long Field(GUIContent label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, style, options);
        }

        #endregion
    }
}