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
        #region SP

        /// <summary> SerializedProperty创建一个字段 </summary>
        public static bool SP(string label, SerializedProperty property, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PropertyField(property, new GUIContent(label), options);
        }

        /// <summary> SerializedProperty创建一个字段 </summary>
        public static bool SP(SerializedProperty property, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PropertyField(property, options);
        }

        /// <summary> SerializedProperty创建一个字段 </summary>
        public static bool SP(SerializedProperty property, GUIContent label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PropertyField(property, label, options);
        }

        /// <summary> SerializedProperty创建一个字段 </summary>
        public static bool SP(SerializedProperty property, GUIContent label, bool includeChildren, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PropertyField(property, label, includeChildren, options);
        }

        /// <summary> SerializedProperty创建一个字段 </summary>
        public static bool SP(SerializedProperty property, bool includeChildren, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PropertyField(property, includeChildren, options);
        }

        #endregion

        #region SP Curve

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="color">曲线颜色</param>
        /// <param name="ranges">限制曲线的矩形</param>
        /// <param name="options">排版格式</param>
        public static void SPCurve(SerializedProperty value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            EditorGUILayout.CurveField(value, color, ranges, options);
        }

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="color">曲线颜色</param>
        /// <param name="ranges">限制曲线的矩形</param>
        /// <param name="label">可选标签</param>
        /// <param name="options">排版格式</param>
        public static void SPCurve(SerializedProperty value, Color color, Rect ranges, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.CurveField(value, color, ranges, label, options);
        }

        #endregion

        #region SP Object

        /// <summary> 物体文本框 FieldObject </summary>
        public static void SPObject<T>(SerializedProperty property, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.ObjectField(property, typeof(T), label, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static void SPObject(SerializedProperty property, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.ObjectField(property, label, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static void SPObject(SerializedProperty property, params GUILayoutOption[] options)
        {
            EditorGUILayout.ObjectField(property, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static void SPObject<T>(SerializedProperty property, params GUILayoutOption[] options)
        {
            EditorGUILayout.ObjectField(property, typeof(T), options);
        }

        #endregion

        #region SP Text Delayed Field

        /// <summary> 延迟文本 string </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPTextDelayed(SerializedProperty value, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.DelayedTextField(value, label, options);
        }

        /// <summary> 延迟文本 string </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static void SPTextDelayed(SerializedProperty value, params GUILayoutOption[] options)
        {
            EditorGUILayout.DelayedTextField(value, options);
        }

        #endregion

        #region SP Float Slider

        /// <summary> 滑动条 </summary>
        /// <param name="property">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPSlider(SerializedProperty property, float leftValue, float rightValue, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.Slider(property, leftValue, rightValue, options);
        }

        /// <summary> 滑动条 </summary>
        /// <param name="property">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPSlider(SerializedProperty property, float leftValue, float rightValue, string label, params GUILayoutOption[] options)
        {
            EditorGUILayout.Slider(property, leftValue, rightValue, options);
        }

        /// <summary> 滑动条 </summary>
        /// <param name="property">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="options">排版格式</param>
        public static void SPSlider(SerializedProperty property, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            EditorGUILayout.Slider(property, leftValue, rightValue, options);
        }

        /// <summary> 滑动条 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="options">排版格式</param>
        public static float SPSlider(GUIContent label, float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(label, value, leftValue, rightValue, options);
        }

        #endregion

        #region SP Float Delayed

        /// <summary> 延迟文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPFloatDelayed(SerializedProperty value, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.DelayedFloatField(value, label, options);
        }

        /// <summary> 延迟文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static void SPFloatDelayed(SerializedProperty value, params GUILayoutOption[] options)
        {
            EditorGUILayout.DelayedFloatField(value, options);
        }

        #endregion

        #region SP Int Slider

        /// <summary> 滑动条 Int </summary>
        /// <param name="property">预制件界面</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPIntSlider(SerializedProperty property, int leftValue, int rightValue, string label, params GUILayoutOption[] options)
        {
            EditorGUILayout.IntSlider(property, leftValue, rightValue, label, options);
        }

        /// <summary> 滑动条 Int </summary>
        /// <param name="property">预制件界面</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPIntSlider(SerializedProperty property, int leftValue, int rightValue, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.IntSlider(property, leftValue, rightValue, label, options);
        }

        /// <summary> 滑动条 Int </summary>
        /// <param name="property">预制件界面</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="options">排版格式</param>
        public static void SPIntSlider(SerializedProperty property, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            EditorGUILayout.IntSlider(property, leftValue, rightValue, options);
        }

        #endregion

        #region SP Int Popup

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="property">预制组件的用户界面</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="options">排版格式</param>
        public static void SPIntPopup(SerializedProperty property, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            EditorGUILayout.IntPopup(property, displayedOptions, optionValues, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="property">预制组件的用户界面</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPIntPopup(SerializedProperty property, GUIContent[] displayedOptions, int[] optionValues, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.IntPopup(property, displayedOptions, optionValues, label, options);
        }

        #endregion

        #region SP Int Delayed

        /// <summary> 延迟文本 int </summary>
        /// <param name="value">值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static void SPIntDelayed(SerializedProperty value, GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.DelayedIntField(value, label, options);
        }

        /// <summary> 延迟文本 int </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static void SPIntDelayed(SerializedProperty value, params GUILayoutOption[] options)
        {
            EditorGUILayout.DelayedIntField(value, options);
        }

        #endregion
    }
}
