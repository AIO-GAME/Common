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
        #region 编辑动画曲线 Curve Field

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="label">可选标签</param>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="color">曲线颜色</param>
        /// <param name="ranges">限制曲线的矩形</param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(GUIContent label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, options);
        }

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="color">曲线颜色</param>
        /// <param name="ranges">限制曲线的矩形</param>
        /// <param name="label">可选标签</param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(string label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, options);
        }

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="color">曲线颜色</param>
        /// <param name="ranges">限制曲线的矩形</param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(value, color, ranges, options);
        }

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="label">可选标签</param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(GUIContent label, AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, options);
        }

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="label">可选标签</param>
        /// <param name="options">排版格式</param>
        public static void Field(string label, AnimationCurve value, params GUILayoutOption[] options)
        {
            EditorGUILayout.CurveField(label, value, options);
        }

        /// <summary> 编辑动画曲线 </summary>
        /// <param name="value">要编辑的曲线</param>
        /// <param name="options">排版格式</param>
        public static void Field(AnimationCurve value, params GUILayoutOption[] options)
        {
            EditorGUILayout.CurveField(value, options);
        }

        #endregion
    }
}