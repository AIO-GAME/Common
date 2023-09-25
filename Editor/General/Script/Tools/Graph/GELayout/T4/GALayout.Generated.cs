






/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate
|||✩ Document: ||| ->
|||✩ - - - - - |*/

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace AIO.UEditor
{
    /// <summary>
    /// Layout
    /// </summary>
    public partial class GALayout
    {

        #region Struct

        /// <summary>
        /// 绘制 Bounds 
        /// </summary>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(value, options);
        }

        /// <summary>
        /// 绘制 Bounds 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(string label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(label, value, options);
        }

        /// <summary>
        /// 绘制 Bounds 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(GUIContent label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(label, value, options);
        }

        /// <summary>
        /// 绘制 Bounds 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(Texture label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 BoundsInt 
        /// </summary>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(value, options);
        }

        /// <summary>
        /// 绘制 BoundsInt 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(string label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(label, value, options);
        }

        /// <summary>
        /// 绘制 BoundsInt 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(GUIContent label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(label, value, options);
        }

        /// <summary>
        /// 绘制 BoundsInt 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(Texture label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 RectInt 
        /// </summary>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(value, options);
        }

        /// <summary>
        /// 绘制 RectInt 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(string label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(label, value, options);
        }

        /// <summary>
        /// 绘制 RectInt 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(GUIContent label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(label, value, options);
        }

        /// <summary>
        /// 绘制 RectInt 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(Texture label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Rect 
        /// </summary>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(value, options);
        }

        /// <summary>
        /// 绘制 Rect 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(string label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(label, value, options);
        }

        /// <summary>
        /// 绘制 Rect 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(GUIContent label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(label, value, options);
        }

        /// <summary>
        /// 绘制 Rect 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(Texture label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Vector2Int 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        public static Vector2Int Field(string label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector2Int 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        public static Vector2Int Field(GUIContent label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector2Int 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        public static Vector2Int Field(Texture label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Vector3Int 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        public static Vector3Int Field(string label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector3Int 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        public static Vector3Int Field(GUIContent label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector3Int 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        public static Vector3Int Field(Texture label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Vector4 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector4"/></returns>
        public static Vector4 Field(string label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector4 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector4"/></returns>
        public static Vector4 Field(GUIContent label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector4 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector4"/></returns>
        public static Vector4 Field(Texture label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Vector3 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 Field(string label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector3 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 Field(GUIContent label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector3 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 Field(Texture label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Vector2 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 Field(string label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector2 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 Field(GUIContent label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(label, value, options);
        }

        /// <summary>
        /// 绘制 Vector2 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 Field(Texture label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(new GUIContent(label), value, options);
        }

        #endregion

        #region Default

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(string label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(value, style, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(string label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(GUIContent label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(Texture label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(Texture label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(Texture label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(Texture label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(string label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(string label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(GUIContent label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(Texture label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(Texture label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, style, options);
        }

        #endregion

    }
}
