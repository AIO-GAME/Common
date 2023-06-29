/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-03                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 13:21:20                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Internal;
using Object = UnityEngine.Object;

namespace UnityEditor
{
    public static partial class GTLayout
    {
        /// <summary>
        /// 数值绘制函数
        /// </summary>
        public static partial class AC
        {
         
            /* ----------------Field-Delayed---------------- */

            #region 延迟文本 FieldDelayedDouble

            /// <summary> 延迟文本 double </summary>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static double FieldDelayedDouble(double value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedDoubleField(value, style, options);
            }

            /// <summary> 延迟文本 double </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static double FieldDelayedDouble(double value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedDoubleField(value, options);
            }

            /// <summary> 延迟文本 double </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static double FieldDelayedDouble(string label, double value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedDoubleField(label, value, options);
            }

            /// <summary> 延迟文本 double </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static double FieldDelayedDouble(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedDoubleField(label, value, style, options);
            }

            /// <summary> 延迟文本 double </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static double FieldDelayedDouble(GUIContent label, double value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedDoubleField(label, value, options);
            }

            /// <summary> 延迟文本 double </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static double FieldDelayedDouble(string label, double value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedDoubleField(label, value, style, options);
            }

            #endregion

            #region 延迟文本 FieldDelayedFloat

            /// <summary> 延迟文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldDelayedFloat(SerializedProperty value, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.DelayedFloatField(value, label, options);
            }

            /// <summary> 延迟文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static void FieldDelayedFloat(SerializedProperty value, params GUILayoutOption[] options)
            {
                EditorGUILayout.DelayedFloatField(value, options);
            }

            /// <summary> 延迟文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedFloat(GUIContent label, float value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedFloatField(label, value, options);
            }

            /// <summary> 延迟文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            /// <param name="style">显示风格</param>
            public static float FieldDelayedFloat(string label, float value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedFloatField(label, value, style, options);
            }

            /// <summary> 延迟文本 float </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedFloat(string label, float value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedFloatField(label, value, options);
            }

            /// <summary> 延迟文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedFloat(float value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedFloatField(value, style, options);
            }

            /// <summary> 延迟文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedFloat(float value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedFloatField(value, options);
            }

            /// <summary> 延迟文本 float </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedFloat(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedFloatField(label, value, style, options);
            }

            #endregion

            #region 延迟文本 FieldDelayedInt

            /// <summary> 延迟文本 int </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldDelayedInt(SerializedProperty value, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.DelayedIntField(value, label, options);
            }

            /// <summary> 延迟文本 int </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static void FieldDelayedInt(SerializedProperty value, params GUILayoutOption[] options)
            {
                EditorGUILayout.DelayedIntField(value, options);
            }

            /// <summary> 延迟文本 int </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static int FieldDelayedInt(GUIContent label, int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedIntField(label, value, options);
            }

            /// <summary> 延迟文本 int </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            /// <param name="style">显示风格</param>
            public static float FieldDelayedInt(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedIntField(label, value, style, options);
            }

            /// <summary> 延迟文本 int </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedInt(string label, int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedIntField(label, value, options);
            }

            /// <summary> 延迟文本 int </summary>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedInt(int value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedIntField(value, style, options);
            }

            /// <summary> 延迟文本 int </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedInt(int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedIntField(value, options);
            }

            /// <summary> 延迟文本 int </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static float FieldDelayedInt(string label, int value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DelayedIntField(label, value, style, options);
            }

            #endregion

            /* ----------------Field---------------- */

            #region 序列化属性 FieldProperty

            /// <summary> SerializedProperty创建一个字段 </summary>
            public static bool FieldProperty(string label, SerializedProperty property, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PropertyField(property, new GUIContent(label), options);
            }

            /// <summary> SerializedProperty创建一个字段 </summary>
            public static bool FieldProperty(SerializedProperty property, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PropertyField(property, options);
            }

            /// <summary> SerializedProperty创建一个字段 </summary>
            public static bool FieldProperty(SerializedProperty property, GUIContent label, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PropertyField(property, label, options);
            }

            /// <summary> SerializedProperty创建一个字段 </summary>
            public static bool FieldProperty(SerializedProperty property, GUIContent label, bool includeChildren, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PropertyField(property, label, includeChildren, options);
            }

            /// <summary> SerializedProperty创建一个字段 </summary>
            public static bool FieldProperty(SerializedProperty property, bool includeChildren, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PropertyField(property, includeChildren, options);
            }

            #endregion

            #region 编辑动画曲线 FieldCurve

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            /// <param name="color">曲线颜色</param>
            /// <param name="ranges">限制曲线的矩形</param>
            /// <param name="options">排版格式</param>
            public static void FieldCurve(SerializedProperty value, Color color, Rect ranges, params GUILayoutOption[] options)
            {
                EditorGUILayout.CurveField(value, color, ranges, options);
            }

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            /// <param name="color">曲线颜色</param>
            /// <param name="ranges">限制曲线的矩形</param>
            /// <param name="label">可选标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldCurve(SerializedProperty value, Color color, Rect ranges, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.CurveField(value, color, ranges, label, options);
            }

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            /// <param name="color">曲线颜色</param>
            /// <param name="ranges">限制曲线的矩形</param>
            /// <param name="options">排版格式</param>
            public static AnimationCurve FieldCurve(GUIContent label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
            {
                return EditorGUILayout.CurveField(label, value, color, ranges, options);
            }

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            /// <param name="color">曲线颜色</param>
            /// <param name="ranges">限制曲线的矩形</param>
            /// <param name="label">可选标签</param>
            /// <param name="options">排版格式</param>
            public static AnimationCurve FieldCurve(string label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
            {
                return EditorGUILayout.CurveField(label, value, color, ranges, options);
            }

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            /// <param name="color">曲线颜色</param>
            /// <param name="ranges">限制曲线的矩形</param>
            /// <param name="options">排版格式</param>
            public static AnimationCurve FieldCurve(AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
            {
                return EditorGUILayout.CurveField(value, color, ranges, options);
            }

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            /// <param name="label">可选标签</param>
            /// <param name="options">排版格式</param>
            public static AnimationCurve FieldCurve(GUIContent label, AnimationCurve value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.CurveField(label, value, options);
            }

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            /// <param name="label">可选标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldCurve(string label, AnimationCurve value, params GUILayoutOption[] options)
            {
                EditorGUILayout.CurveField(label, value, options);
            }

            /// <summary> 编辑动画曲线 </summary>
            /// <param name="value">要编辑的曲线</param>
            public static void FieldCurve(AnimationCurve value, params GUILayoutOption[] options)
            {
                EditorGUILayout.CurveField(value, options);
            }

            #endregion

            #region 文本 FieldInt

            /// <summary> 文本 Int </summary>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int FieldInt(int value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntField(value, style, options);
            }

            /// <summary> 文本 Int </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static int FieldInt(string label, int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntField(label, value, options);
            }

            /// <summary> 文本 Int </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int FieldInt(string label, int value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntField(label, value, style, options);
            }

            /// <summary> 文本 Int </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static int FieldInt(GUIContent label, int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntField(label, value, options);
            }

            /// <summary> 文本 Int </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int FieldInt(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntField(label, value, style, options);
            }

            /// <summary> 文本 Int </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static int FieldInt(int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntField(value, options);
            }

            #endregion

            #region 文本 FieldDouble

            /// <summary> 文本 Double </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static double FieldDouble(string label, double value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DoubleField(value, label, options);
            }

            /// <summary> 文本 Double </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static double FieldDouble(string label, double value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DoubleField(value, label, options);
            }

            /// <summary> 文本 Double </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static double FieldDouble(GUIContent label, double value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DoubleField(label, value, options);
            }

            /// <summary> 文本 Double </summary>
            /// <param name="value">值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static double FieldDouble(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DoubleField(label, value, style, options);
            }

            /// <summary> 文本 Double </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static double FieldDouble(double value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DoubleField(value, options);
            }

            /// <summary> 文本 Double </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static double FieldDouble(double value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.DoubleField(value, style, options);
            }

            #endregion

            #region 文本 FieldFloat

            /// <summary> 文本 float </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static float FieldFloat(GUIContent label, float value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.FloatField(label, value, options);
            }

            /// <summary> 文本 float </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static float FieldFloat(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.FloatField(label, value, style, options);
            }

            /// <summary> 文本 float </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static float FieldFloat(string label, float value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.FloatField(label, value, style, options);
            }

            /// <summary> 文本 float </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static float FieldFloat(string label, float value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.FloatField(label, value, options);
            }


            /// <summary> 文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static float FieldFloat(float value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.FloatField(value, style, options);
            }


            /// <summary> 文本 float </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static float FieldFloat(float value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.FloatField(value, options);
            }

            #endregion

            #region 文本 渐变字段 FieldGradient

#if UNITY_2018_3 || UNITY_2019 || UNITY_2020
            /// <summary> 文本 渐变字段 FieldGradient </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="hdr">hdr状态</param>
            /// <param name="options">排版格式</param>
            public static Gradient FieldGradient(GUIContent label, Gradient value, bool hdr, params GUILayoutOption[] options)
            {
                return EditorGUILayout.GradientField(label, value, hdr, options);
            }

            /// <summary> 文本 渐变字段 FieldGradient </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Gradient FieldGradient(GUIContent label, Gradient value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.GradientField(label, value, options);
            }

            /// <summary> 文本 渐变字段 FieldGradient </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Gradient FieldGradient(string label, Gradient value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.GradientField(label, value, options);
            }

            /// <summary> 文本 渐变字段 FieldGradient </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Gradient FieldGradient(Gradient value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.GradientField(value, options);
            }
#endif

            #endregion

            #region 坐标文本框 Field Vector2 Vector3 Vector4

            /// <summary> 二维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector2 FieldVector2(GUIContent label, Vector2 value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector2Field(label, value, options);
            }

            /// <summary> 二维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector2 FieldVector2(string label, Vector2 value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector2Field(label, value, options);
            }

            /// <summary> 二维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector2Int FieldVector2Int(GUIContent label, Vector2Int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector2IntField(label, value, options);
            }

            /// <summary> 二维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector2Int FieldVector2Int(string label, Vector2Int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector2IntField(label, value, options);
            }

            /// <summary> 三维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector3 FieldVector3(GUIContent label, Vector3 value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector3Field(label, value, options);
            }

            /// <summary> 三维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector3 FieldVector3(string label, Vector3 value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector3Field(label, value, options);
            }

            /// <summary> 三维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector3Int FieldVector3Int(GUIContent label, Vector3Int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector3IntField(label, value, options);
            }

            /// <summary> 三维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector3Int FieldVector3Int(string label, Vector3Int value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector3IntField(label, value, options);
            }

            /// <summary> 四维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector4 FieldVector4(string label, Vector4 value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector4Field(label, value, options);
            }

            /// <summary> 四维坐标文本框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Vector4 FieldVector4(GUIContent label, Vector4 value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Vector4Field(label, value, options);
            }

            #endregion

            #region 标签文本框 FieldLabel

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="label2">向右显示的标签</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(GUIContent label, GUIContent label2, GUIStyle style, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, label2, style, options);
            }

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(string label, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, options);
            }

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, options);
            }

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="label2">向右显示的标签</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(string label, string label2, GUIStyle style, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, label2, style, options);
            }

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="label2">向右显示的标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(string label, string label2, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, label2, options);
            }

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, style, options);
            }

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(string label, GUIStyle style, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, style, options);
            }

            /// <summary> 标签文本框 FieldLabel </summary>
            /// <param name="label">第一个标签</param>
            /// <param name="label2">向右显示的标签</param>
            /// <param name="options">排版格式</param>
            public static void FieldLabel(GUIContent label, GUIContent label2, params GUILayoutOption[] options)
            {
                EditorGUILayout.LabelField(label, label2, options);
            }

            #endregion

            #region 选择层文本框 FieldLayer

            /// <summary> 选择层文本框 FieldLayer </summary>
            /// <param name="layer">层数</param>
            /// <param name="options">排版格式</param>
            public static int LayerField(int layer, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LayerField(layer, options);
            }

            /// <summary> 选择层文本框 FieldLayer </summary>
            /// <param name="layer">层数</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static int LayerField(int layer, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LayerField(layer, style, options);
            }

            /// <summary> 选择层文本框 FieldLayer </summary>
            /// <param name="label">标签</param>
            /// <param name="layer">层数</param>
            /// <param name="options">排版格式</param>
            public static int LayerField(string label, int layer, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LayerField(label, layer, options);
            }

            /// <summary> 选择层文本框 FieldLayer </summary>
            /// <param name="label">标签</param>
            /// <param name="layer">层数</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static int LayerField(string label, int layer, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LayerField(label, layer, style, options);
            }

            /// <summary> 选择层文本框 FieldLayer </summary>
            /// <param name="label">标签</param>
            /// <param name="layer">层数</param>
            /// <param name="options">排版格式</param>
            public static int LayerField(GUIContent label, int layer, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LayerField(label, layer, options);
            }

            /// <summary> 选择层文本框 FieldLayer </summary>
            /// <param name="label">标签</param>
            /// <param name="layer">层数</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static int LayerField(GUIContent label, int layer, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LayerField(label, layer, style, options);
            }

            #endregion

            #region 文本框 FieldLong

            /// <summary> 文本框 FieldLong </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static long FieldLong(long value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LongField(value, options);
            }

            /// <summary> 文本框 FieldLong </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static long FieldLong(long value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LongField(value, style, options);
            }

            /// <summary> 文本框 FieldLong </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static long FieldLong(string label, long value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LongField(label, value, options);
            }

            /// <summary> 文本框 FieldLong </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static long FieldLong(string label, long value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LongField(label, value, style, options);
            }

            /// <summary> 文本框 FieldLong </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static long FieldLong(GUIContent label, long value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LongField(label, value, options);
            }

            /// <summary> 文本框 FieldLong </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static long FieldLong(GUIContent label, long value, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.LongField(label, value, style, options);
            }

            #endregion

            #region 屏蔽文本框 FieldMask

            /// <summary> 屏蔽文本框 FieldMask </summary>
            /// <param name="mask">选择值</param>
            /// <param name="displayedOptions">选择内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int FieldMask(int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.MaskField(mask, displayedOptions, style, options);
            }

            /// <summary> 屏蔽文本框 FieldMask </summary>
            /// <param name="mask">选择值</param>
            /// <param name="displayedOptions">选择内容</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int FieldMask(string label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.MaskField(label, mask, displayedOptions, options);
            }

            /// <summary> 屏蔽文本框 FieldMask </summary>
            /// <param name="mask">选择值</param>
            /// <param name="displayedOptions">选择内容</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int FieldMask(string label, int mask, List<string> displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), options);
            }

            /// <summary> 屏蔽文本框 FieldMask </summary>
            /// <param name="mask">选择值</param>
            /// <param name="displayedOptions">选择内容</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int FieldMask(GUIContent label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.MaskField(label, mask, displayedOptions, options);
            }

            /// <summary> 屏蔽文本框 FieldMask </summary>
            /// <param name="mask">选择值</param>
            /// <param name="displayedOptions">选择内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int FieldMask(string label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.MaskField(label, mask, displayedOptions, style, options);
            }

            /// <summary> 屏蔽文本框 FieldMask </summary>
            /// <param name="mask">选择值</param>
            /// <param name="displayedOptions">选择内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int FieldMask(GUIContent label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.MaskField(label, mask, displayedOptions, style, options);
            }

            /// <summary> 屏蔽文本框 FieldMask </summary>
            /// <param name="mask">选择值</param>
            /// <param name="displayedOptions">选择内容</param>
            /// <param name="options">排版格式</param>
            public static int FieldMask(int mask, string[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.MaskField(mask, displayedOptions, options);
            }

            #endregion

            #region 物体文本框 FieldObject

            /// <summary> 物体文本框 FieldObject </summary>
            public static T FieldObject<T>(T Obj, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
            {
                return (T)EditorGUILayout.ObjectField(Obj, typeof(T), allowSceneObjects, options);
            }

            /// <summary> 物体文本框 FieldObject </summary>
            public static T FieldObject<T>(T Obj, string name, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
            {
                return (T)EditorGUILayout.ObjectField(name, Obj, typeof(T), allowSceneObjects, options);
            }

            /// <summary> 物体文本框 FieldObject </summary>
            public static T FieldObject<T>(T Obj, GUIContent name, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
            {
                return (T)EditorGUILayout.ObjectField(name, Obj, typeof(T), allowSceneObjects, options);
            }

            /// <summary> 物体文本框 FieldObject </summary>
            public static void FieldObject<T>(SerializedProperty property, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.ObjectField(property, typeof(T), label, options);
            }

            /// <summary> 物体文本框 FieldObject </summary>
            public static void FieldObject(SerializedProperty property, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.ObjectField(property, label, options);
            }

            /// <summary> 物体文本框 FieldObject </summary>
            public static void FieldObject(SerializedProperty property, params GUILayoutOption[] options)
            {
                EditorGUILayout.ObjectField(property, options);
            }

            /// <summary> 物体文本框 FieldObject </summary>
            public static void FieldObject<T>(SerializedProperty property, params GUILayoutOption[] options)
            {
                EditorGUILayout.ObjectField(property, typeof(T), options);
            }

            #endregion

            #region 密码文本框 FieldPassword

            /// <summary> 密码文本框 FieldPassword </summary>
            /// <param name="password">遮掩码</param>
            /// <param name="options">排版格式</param>
            public static string FieldPassword(string password, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PasswordField(password, options);
            }

            /// <summary> 密码文本框 FieldPassword </summary>
            /// <param name="password">遮掩码</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static string PasswordField(string password, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PasswordField(password, style, options);
            }


            /// <summary> 密码文本框 FieldPassword </summary>
            /// <param name="password">遮掩码</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static string PasswordField(string label, string password, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PasswordField(label, password, options);
            }

            /// <summary> 密码文本框 FieldPassword </summary>
            /// <param name="password">遮掩码</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            /// <param name="label">标签</param>
            public static string PasswordField(string label, string password, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PasswordField(label, password, style, options);
            }

            /// <summary> 密码文本框 FieldPassword </summary>
            /// <param name="password">遮掩码</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static string PasswordField(GUIContent label, string password, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PasswordField(label, password, options);
            }

            /// <summary> 密码文本框 FieldPassword </summary>
            /// <param name="password">遮掩码</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            /// <param name="label">标签</param>
            public static string PasswordField(GUIContent label, string password, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.PasswordField(label, password, style, options);
            }

            #endregion

            #region 矩形字段 FieldRect

            /// <summary> 矩形字段 FieldRect </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Rect FieldRect(Rect value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.RectField(value, options);
            }

            /// <summary> 矩形字段 FieldRect </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Rect FieldRect(string label, Rect value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.RectField(value, options);
            }

            /// <summary> 矩形字段 FieldRect </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static Rect FieldRect(GUIContent label, Rect value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.RectField(value, options);
            }

            #endregion

            #region 矩形字段 FieldRectInt

            /// <summary> 矩形字段 FieldRectInt </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static RectInt FieldRectInt(GUIContent label, RectInt value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.RectIntField(label, value, options);
            }

            /// <summary> 矩形字段 FieldRectInt </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static RectInt FieldRectInt(string label, RectInt value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.RectIntField(label, value, options);
            }


            /// <summary> 矩形字段 FieldRectInt </summary>
            /// <param name="value">值</param>
            /// <param name="options">排版格式</param>
            public static RectInt FieldRectInt(RectInt value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.RectIntField(value, options);
            }

            #endregion

            #region 标签字段 FieldTag

            /// <summary> 标签字段 FieldTag </summary>
            /// <param name="tag"></param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string TagField(string tag, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TagField(tag, style, options);
            }

            /// <summary> 标签字段 FieldTag </summary>
            /// <param name="label">标签</param>
            /// <param name="tag">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string TagField(GUIContent label, string tag, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TagField(label, tag, style, options);
            }

            /// <summary> 标签字段 FieldTag </summary>
            /// <param name="tag">值</param>
            /// <param name="options">排版格式</param>
            public static string TagField(string tag, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TagField(tag, options);
            }

            /// <summary> 标签字段 FieldTag </summary>
            /// <param name="label">标签</param>
            /// <param name="tag">值</param>
            /// <param name="options">排版格式</param>
            public static string TagField(GUIContent label, string tag, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TagField(label, tag, options);
            }

            /// <summary> 标签字段 FieldTag </summary>
            /// <param name="label">标签</param>
            /// <param name="tag">值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static string TagField(string label, string tag, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TagField(label, tag, style, options);
            }

            /// <summary> 标签字段 FieldTag </summary>
            /// <param name="label">标签</param>
            /// <param name="tag">值</param>
            /// <param name="options">排版格式</param>
            public static string TagField(string label, string tag, params GUILayoutOption[] options)
            {
                return EditorGUILayout.TagField(label, tag, options);
            }

            #endregion

            /* ----------------Popup---------------- */

            #region 弹窗枚举菜单 PopupEnum

#if UNITY_2018_3_OR_NEWER

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="selected">枚举值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(selected, style, options);
            }

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="label">标签</param>
            /// <param name="selected">枚举值</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(string label, T selected, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(label, selected, options);
            }

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="label">标签</param>
            /// <param name="selected">枚举值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(string label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(label, selected, style, options);
            }

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="label">标签</param>
            /// <param name="selected">枚举值</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(GUIContent label, T selected, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(label, selected, options);
            }

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="label">标签</param>
            /// <param name="selected">枚举值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(GUIContent label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(label, selected, style, options);
            }

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="label">标签</param>
            /// <param name="selected">枚举值</param>
            /// <param name="checkEnabled">显示每个Enum值,返回指定的方法</param>
            /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, options);
            }

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="label">标签</param>
            /// <param name="selected">枚举值</param>
            /// <param name="checkEnabled">显示每个Enum值,返回指定的方法</param>
            /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, style, options);
            }

            /// <summary> 弹窗枚举菜单 </summary>
            /// <param name="selected">枚举值</param>
            /// <param name="options">排版格式</param>
            public static T PopupEnum<T>(T selected, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumPopup(selected, options);
            }
#endif

            #endregion

            #region 弹出整数选择字段 PopupInt

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="label">标签</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(GUIContent label, int selectedValue, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions, optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="label">标签</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(GUIContent label, int selectedValue, List<GUIContent> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions.ToArray(), optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="label">标签</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(string label, int selectedValue, string[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions, optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="label">标签</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(string label, int selectedValue, List<string> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions.ToArray(), optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="label">标签</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(string label, int selectedValue, int[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                var vs = new string[displayedOptions.Length];
                var index = 0;
                foreach (var item in displayedOptions)
                    vs[index++] = item.ToString();
                return EditorGUILayout.IntPopup(label, selectedValue, vs, optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(int selectedValue, GUIContent[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, style, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="label">标签</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(GUIContent label, int selectedValue, GUIContent[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions, optionValues, style, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="property">预制组件的用户界面</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="options">排版格式</param>
            public static void PopupInt(SerializedProperty property, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                EditorGUILayout.IntPopup(property, displayedOptions, optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="property">预制组件的用户界面</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void PopupInt(SerializedProperty property, GUIContent[] displayedOptions, int[] optionValues, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.IntPopup(property, displayedOptions, optionValues, label, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="property">预制组件的用户界面</param>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="label">标签</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static int PopupInt(string label, int selectedValue, string[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions, optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(int selectedValue, string[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="options">排版格式</param>
            /// <param name="style">显示风格</param>
            public static int PopupInt(int selectedValue, string[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, style, options);
            }

            /// <summary> 弹出整数选择字段 </summary>
            /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
            /// <param name="optionValues">包含每个选项的值的数组</param>
            /// <param name="selectedValue">字段显示的选项的值</param>
            /// <param name="options">排版格式</param>
            public static int PopupInt(int selectedValue, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, options);
            }

            #endregion

            #region 弹窗 Popup

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int Popup(int selectedIndex, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int Popup(string label, int selectedIndex, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int Popup(GUIContent label, int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int Popup(string label, int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int Popup(GUIContent label, int selectedIndex, GUIContent[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            /// <param name="label">标签</param>
            public static int Popup(GUIContent label, int selectedIndex, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="options">排版格式</param>
            public static int Popup(int selectedIndex, GUIContent[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(selectedIndex, displayedOptions, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static int Popup(int selectedIndex, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, options);
            }

            /// <summary> 弹窗 Popup </summary>
            /// <param name="selectedIndex">值</param>
            /// <param name="displayedOptions">弹窗内容</param>
            /// <param name="options">排版格式</param>
            public static int Popup(int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Popup(selectedIndex, displayedOptions, options);
            }

            #endregion

            /* ----------------Slider---------------- */

            #region 滑动条 SliderInt

            /// <summary> 滑动条 Int </summary>
            /// <param name="property">预制件界面</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void SliderInt(SerializedProperty property, int leftValue, int rightValue, string label, params GUILayoutOption[] options)
            {
                EditorGUILayout.IntSlider(property, leftValue, rightValue, label, options);
            }

            /// <summary> 滑动条 Int </summary>
            /// <param name="property">预制件界面</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void SliderInt(SerializedProperty property, int leftValue, int rightValue, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.IntSlider(property, leftValue, rightValue, label, options);
            }

            /// <summary> 滑动条 Int </summary>
            /// <param name="property">预制件界面</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="options">排版格式</param>
            public static void SliderInt(SerializedProperty property, int leftValue, int rightValue, params GUILayoutOption[] options)
            {
                EditorGUILayout.IntSlider(property, leftValue, rightValue, options);
            }

            /// <summary> 滑动条 Int </summary>
            /// <param name="value">值</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static int SliderInt(GUIContent label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntSlider(label, value, leftValue, rightValue, options);
            }

            /// <summary> 滑动条 Int </summary>
            /// <param name="value">值</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static int SliderInt(string label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
            {
                return EditorGUILayout.IntSlider(label, value, leftValue, rightValue, options);
            }

            /// <summary> 滑动条 Int </summary>
            /// <param name="value">值</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="options">排版格式</param>
            public static int SliderInt(int value, int leftValue, int rightValue, params GUILayoutOption[] options)
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
            /// <param name="property">值</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void Slider(SerializedProperty property, float leftValue, float rightValue, string label, params GUILayoutOption[] options)
            {
                EditorGUILayout.Slider(property, leftValue, rightValue, options);
            }

            /// <summary> 滑动条 </summary>
            /// <param name="property">值</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="options">排版格式</param>
            public static void Slider(SerializedProperty property, float leftValue, float rightValue, params GUILayoutOption[] options)
            {
                EditorGUILayout.Slider(property, leftValue, rightValue, options);
            }

            /// <summary> 滑动条 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">值</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="options">排版格式</param>
            public static float Slider(GUIContent label, float value, float leftValue, float rightValue, params GUILayoutOption[] options)
            {
                return EditorGUILayout.Slider(label, value, leftValue, rightValue, options);
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

            /// <summary> 滑动条 </summary>
            /// <param name="property">值</param>
            /// <param name="leftValue">左边值</param>
            /// <param name="rightValue">右边值</param>
            /// <param name="label">标签</param>
            /// <param name="options">排版格式</param>
            public static void Slider(SerializedProperty property, float leftValue, float rightValue, GUIContent label, params GUILayoutOption[] options)
            {
                EditorGUILayout.Slider(property, leftValue, rightValue, options);
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

            /* ----------------Enum---------------- */

            #region 枚举菜单 EnumFlagsField

#if UNITY_2018_3 || UNITY_2019 || UNITY_2020

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="enumValue">枚举值</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(T enumValue, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(enumValue, options);
            }

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="label">标签</param>
            /// <param name="enumValue">枚举值</param>
            /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(GUIContent label, T enumValue, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(label, enumValue, includeObsolete, style, options);
            }

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="label">标签</param>
            /// <param name="enumValue">枚举值</param>
            /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(GUIContent label, T enumValue, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(label, enumValue, includeObsolete, options);
            }

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="label">标签</param>
            /// <param name="enumValue">枚举值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(GUIContent label, T enumValue, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(label, enumValue, style, options);
            }

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="label">标签</param>
            /// <param name="enumValue">枚举值</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(GUIContent label, T enumValue, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(label, enumValue, options);
            }

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="label">标签</param>
            /// <param name="enumValue">枚举值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(string label, T enumValue, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(label, enumValue, style, options);
            }

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="label">标签</param>
            /// <param name="enumValue">枚举值</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(string label, T enumValue, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(label, enumValue, options);
            }

            /// <summary> 枚举菜单 EnumFlagsField </summary>
            /// <param name="enumValue">枚举值</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static T EnumFlagsField<T>(T enumValue, GUIStyle style, params GUILayoutOption[] options) where T : Enum
            {
                return (T)EditorGUILayout.EnumFlagsField(enumValue, style, options);
            }
#endif

            #endregion

            #region 枚举菜单 EnumMaskField 已弃用

            //[Obsolete("EnumMaskField 已经弃用. 请使用 EnumFlagsField.", true)]
            //public static Enum EnumMaskField(string label, Enum enumValue, GUIStyle style, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskField(label, enumValue, style, options);

            //[Obsolete("EnumMaskField 已经弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskField(GUIContent label, Enum enumValue, GUIStyle style, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskField(label, enumValue, style, options);

            //[Obsolete("EnumMaskField 已经弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskField(Enum enumValue, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskField(enumValue, options);

            //[Obsolete("EnumMaskField 已经弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskField(Enum enumValue, GUIStyle style, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskField(enumValue, style, options);

            //[Obsolete("EnumMaskField 已经弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskField(string label, Enum enumValue, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskField(label, enumValue, options);

            //[Obsolete("EnumMaskField 已经弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskField(GUIContent label, Enum enumValue, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskField(label, enumValue, options);

            #endregion

            #region 枚举菜单 EnumMaskPopup 已弃用

            //[Obsolete("EnumMaskPopup 已弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskPopup(string label, Enum selected, params GUILayoutOption[] options)
            //  => EditorGUILayout.EnumMaskPopup(label, selected, options);

            //[Obsolete("EnumMaskPopup 已弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskPopup(GUIContent label, Enum selected, GUIStyle style, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskPopup(label, selected, style, options);

            //[Obsolete("EnumMaskPopup 已弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskPopup(string label, Enum selected, GUIStyle style, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskPopup(label, selected, style, options);

            //[Obsolete("EnumMaskPopup 已弃用. 请使用 EnumFlagsField.")]
            //public static Enum EnumMaskPopup(GUIContent label, Enum selected, params GUILayoutOption[] options)
            //    => EditorGUILayout.EnumMaskPopup(label, selected, options);

            #endregion

            /* ----------------Toggle---------------- */

            /* ----------------Other---------------- */

            #region 折叠式箭头 Foldout

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <param name="style">显示风格</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            public static bool Foldout(string content, bool foldout, [DefaultValue("EditorStyles.foldout")] GUIStyle style)
            {
                return EditorGUILayout.Foldout(foldout, content, style);
            }

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            [ExcludeFromDocs] //从文档中排除
            public static bool Foldout(string content, bool foldout)
            {
                return EditorGUILayout.Foldout(foldout, content);
            }

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <param name="style">显示风格</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            public static bool Foldout(GUIContent content, bool foldout, [DefaultValue("EditorStyles.foldout")] GUIStyle style)
            {
                return EditorGUILayout.Foldout(foldout, content, style);
            }

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            [ExcludeFromDocs]
            public static bool Foldout(GUIContent content, bool foldout)
            {
                return EditorGUILayout.Foldout(foldout, content);
            }

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <param name="style">显示风格</param>
            /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            public static bool Foldout(GUIContent content, bool foldout, bool toggleOnLabelClick, [DefaultValue("EditorStyles.foldout")] GUIStyle style)
            {
                return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick, style);
            }

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            [ExcludeFromDocs]
            public static bool Foldout(GUIContent content, bool foldout, bool toggleOnLabelClick)
            {
                return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick);
            }

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <param name="style">显示风格</param>
            /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            public static bool Foldout(string content, bool foldout, bool toggleOnLabelClick, [DefaultValue("EditorStyles.foldout")] GUIStyle style)
            {
                return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick, style);
            }

            /// <summary> 折叠式箭头 </summary>
            /// <param name="foldout">显示的折叠状态</param>
            /// <param name="content">显示的标签</param>
            /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
            /// <returns>true:呈现子对象,false:隐藏</returns>
            [ExcludeFromDocs]
            public static bool Foldout(string content, bool foldout, bool toggleOnLabelClick)
            {
                return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick);
            }

            #endregion

            #region 获取编辑器控件的矩形 GetControlRect

            /// <summary> 获取编辑器控件的矩形 </summary>
            /// <param name="hasLabel">true:有标签,false:没有</param>
            /// <param name="height">控件的高度(以像素为单位)</param>
            /// <param name="style">显示风格</param>
            /// <param name="options">排版格式</param>
            public static Rect GetControlRect(bool hasLabel, float height, GUIStyle style, params GUILayoutOption[] options)
            {
                return EditorGUILayout.GetControlRect(hasLabel, height, style, options);
            }

            /// <summary> 获取编辑器控件的矩形 </summary>
            /// <param name="hasLabel">true:有标签,false:没有</param>
            /// <param name="options">排版格式</param>
            public static Rect GetControlRect(bool hasLabel, params GUILayoutOption[] options)
            {
                return EditorGUILayout.GetControlRect(hasLabel, options);
            }

            /// <summary> 获取编辑器控件的矩形 </summary>
            /// <param name="options">排版格式</param>
            public static Rect GetControlRect(params GUILayoutOption[] options)
            {
                return EditorGUILayout.GetControlRect(options);
            }

            /// <summary> 获取编辑器控件的矩形 </summary>
            /// <param name="hasLabel">true:有标签,false:没有</param>
            /// <param name="height">控件的高度(以像素为单位)</param>
            /// <param name="options">排版格式</param>
            public static Rect GetControlRect(bool hasLabel, float height, params GUILayoutOption[] options)
            {
                return EditorGUILayout.GetControlRect(hasLabel, height, options);
            }

            #endregion

            #region 帮助框 HelpBox

            /// <summary> 帮助框 HelpBox </summary>
            /// <param name="message">内容</param>
            /// <param name="type">提示等级</param>
            public static void HelpBox(string message, MessageType type)
            {
                EditorGUILayout.HelpBox(message, type);
            }

            /// <summary> 帮助框 HelpBox </summary>
            /// <param name="message">内容</param>
            /// <param name="type">提示等级</param>
            /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分</param>
            public static void HelpBox(string message, MessageType type, bool wide)
            {
                EditorGUILayout.HelpBox(message, type, wide);
            }

            /// <summary> 帮助框 HelpBox </summary>
            /// <param name="message">内容</param>
            /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分</param>
            public static void HelpBox(GUIContent content, bool wide = true)
            {
                EditorGUILayout.HelpBox(content, wide);
            }

            #endregion

            #region 类似于检查窗口的标题栏 InspectorTitlebar

            /// <summary> 类似于检查窗口的标题栏 </summary>
            /// <param name="foldout">箭头显示的折叠状态</param>
            /// <param name="targetObj">标题栏用于的对象(例如组件)或对象</param>
            /// <returns>用户选择的折叠状态</returns>
            public static bool InspectorTitlebar(bool foldout, Object targetObj)
            {
                return EditorGUILayout.InspectorTitlebar(foldout, targetObj);
            }

            /// <summary> 类似于检查窗口的标题栏 </summary>
            /// <param name="foldout">箭头显示的折叠状态</param>
            /// <param name="targetObj">标题栏用于的对象(例如组件)或对象</param>
            /// <param name="expandable">是否允许打开</param>
            /// <returns>用户选择的折叠状态</returns>
            public static bool InspectorTitlebar(bool foldout, Object targetObj, bool expandable)
            {
                return EditorGUILayout.InspectorTitlebar(foldout, targetObj, expandable);
            }

            /// <summary> 类似于检查窗口的标题栏 </summary>
            /// <param name="targetObjs">标题栏用于的对象(例如组件)或对象</param>
            /// <returns>用户选择的折叠状态</returns>
            public static void InspectorTitlebar(Object[] targetObjs)
            {
                EditorGUILayout.InspectorTitlebar(targetObjs);
            }

            /// <summary> 类似于检查窗口的标题栏 </summary>
            /// <param name="foldout">箭头显示的折叠状态</param>
            /// <param name="editor">自定义创建自定义检查器或编辑器</param>
            /// <returns>用户选择的折叠状态</returns>
            public static bool InspectorTitlebar(bool foldout, Editor editor)
            {
                return EditorGUILayout.InspectorTitlebar(foldout, editor);
            }

            /// <summary> 类似于检查窗口的标题栏 </summary>
            /// <param name="foldout">箭头显示的折叠状态</param>
            /// <param name="targetObjs">标题栏用于的对象(例如组件)或对象</param>
            /// <param name="expandable">是否允许打开</param>
            /// <returns>用户选择的折叠状态</returns>
            public static bool InspectorTitlebar(bool foldout, Object[] targetObjs, bool expandable)
            {
                return EditorGUILayout.InspectorTitlebar(foldout, targetObjs, expandable);
            }

            /// <summary> 类似于检查窗口的标题栏 </summary>
            /// <param name="foldout">箭头显示的折叠状态</param>
            /// <param name="targetObjs">标题栏用于的对象(例如组件)或对象</param>
            /// <returns>用户选择的折叠状态</returns>
            public static bool InspectorTitlebar(bool foldout, Object[] targetObjs)
            {
                return EditorGUILayout.InspectorTitlebar(foldout, targetObjs);
            }

            #endregion

            #region 工具基类 EditorToolbar

#if UNITY_2019 || UNITY_2018_3

            /// <summary> 工具 bar </summary>
            /// <param name="tools">使用指定的编辑器工具集合填充工具栏 基类</param>
            public static void EditorToolbar(params EditorTool[] tools)
            {
                EditorGUILayout.EditorToolbar(tools);
            }

            /// <summary> 目标对象的 EditorTool Attribute </summary>
            /// <param name="target">工具对象</param>
            public static void EditorToolbarForTarget(Object target)
            {
                EditorGUILayout.EditorToolbarForTarget(target);
            }

            /// <summary> 目标对象的 EditorTool Attribute </summary>
            /// <param name="tools">工具对象</param>
            public static void EditorToolbar<T>(IList<T> tools) where T : EditorTool
            {
                EditorGUILayout.EditorToolbar<T>(tools);
            }
#endif

            #endregion

        }
    }
}