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
        #region 滑动条 SliderInt

        /// <summary> 滑动条 Int </summary>
        /// <param name="value">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static int Slider(GUIContent label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(label, value, leftValue, rightValue, options);
        }

        /// <summary> 滑动条 Int </summary>
        /// <param name="value">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static int Slider(string label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(label, value, leftValue, rightValue, options);
        }

        /// <summary> 滑动条 Int </summary>
        /// <param name="value">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="options">排版格式</param>
        public static int Slider(int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(value, leftValue, rightValue, options);
        }

        #endregion

        #region 滑动条 Slider

        /// <summary> 滑动条 </summary>
        /// <param name="value">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="options">排版格式</param>
        public static float Slider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(value, leftValue, rightValue, options);
        }

        /// <summary> 滑动条 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="leftValue">左边值</param>
        /// <param name="rightValue">右边值</param>
        /// <param name="options">排版格式</param>
        public static float Slider(string label, float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(label, value, leftValue, rightValue, options);
        }

        #endregion

        #region 最小最大滑动条

        /// <summary> 最小最大滑动条 </summary>
        /// <param name="minValue">滑动条最左边的值</param>
        /// <param name="maxValue">滑动条最右边的值</param>
        /// <param name="minLimit">限制滑动条最左边的值</param>
        /// <param name="maxLimit">限制滑动条最右边的值</param>
        /// <param name="options">排版格式</param>
        public static void SliderMinMax(ref float minValue, ref float maxValue, float minLimit, float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        /// <summary> 最小最大滑动条 </summary>
        /// <param name="label">标签</param>
        /// <param name="minValue">滑动条最左边的值</param>
        /// <param name="maxValue">滑动条最右边的值</param>
        /// <param name="minLimit">限制滑动条最左边的值</param>
        /// <param name="maxLimit">限制滑动条最右边的值</param>
        /// <param name="options">排版格式</param>
        public static void SliderMinMax(string label, ref float minValue, ref float maxValue, float minLimit, float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        /// <summary> 最小最大滑动条 </summary>
        /// <param name="label">标签</param>
        /// <param name="minValue">滑动条最左边的值</param>
        /// <param name="maxValue">滑动条最右边的值</param>
        /// <param name="minLimit">限制滑动条最左边的值</param>
        /// <param name="maxLimit">限制滑动条最右边的值</param>
        /// <param name="options">排版格式</param>
        public static void SliderMinMax(GUIContent label, ref float minValue, ref float maxValue, float minLimit, float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        #endregion
    }
}
