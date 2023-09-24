

/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate
|||✩ Document: ||| ->
|||✩ - - - - - |*/
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AIO.UEditor
{

    #region Bounds : EditorGUILayout.BoundsField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="value"> <see cref="Bounds"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(value, options);
        }

        /// <summary>
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(string label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(GUIContent label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public static Bounds Field(Texture label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region BoundsInt : EditorGUILayout.BoundsIntField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="value"> <see cref="BoundsInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(value, options);
        }

        /// <summary>
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(string label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(GUIContent label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public static BoundsInt Field(Texture label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region RectInt : EditorGUILayout.RectIntField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="value"> <see cref="RectInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(value, options);
        }

        /// <summary>
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(string label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(GUIContent label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public static RectInt Field(Texture label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Rect : EditorGUILayout.RectField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="value"> <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(value, options);
        }

        /// <summary>
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(string label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(GUIContent label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect Field(Texture label, Rect value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Vector2 : EditorGUILayout.Vector2Field
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Vector2
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 Field(string label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Vector2
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 Field(GUIContent label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Vector2
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 Field(Texture label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Vector2Int : EditorGUILayout.Vector2IntField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Vector2Int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        public static Vector2Int Field(string label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Vector2Int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        public static Vector2Int Field(GUIContent label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Vector2Int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        public static Vector2Int Field(Texture label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Vector3 : EditorGUILayout.Vector3Field
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Vector3
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 Field(string label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Vector3
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 Field(GUIContent label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Vector3
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3"/></returns>
        public static Vector3 Field(Texture label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Vector3Int : EditorGUILayout.Vector3IntField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Vector3Int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        public static Vector3Int Field(string label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Vector3Int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        public static Vector3Int Field(GUIContent label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Vector3Int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        public static Vector3Int Field(Texture label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Vector4 : EditorGUILayout.Vector4Field
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Vector4
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector4"/></returns>
        public static Vector4 Field(string label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Vector4
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector4"/></returns>
        public static Vector4 Field(GUIContent label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Vector4
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector4"/></returns>
        public static Vector4 Field(Texture label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region float : EditorGUILayout.DelayedFloatField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="value"> <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="value"> <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(value, options);
        }

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(string label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(string label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(GUIContent label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(label, value, options);
        }

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(Texture label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float FieldDelayed(Texture label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region int : EditorGUILayout.DelayedIntField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="value"> <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="value"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(value, options);
        }

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(label, value, options);
        }

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(Texture label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int FieldDelayed(Texture label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region double : EditorGUILayout.DelayedDoubleField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="value"> <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="value"> <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(value, options);
        }

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(string label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(string label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(GUIContent label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(label, value, options);
        }

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(Texture label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double FieldDelayed(Texture label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region string : EditorGUILayout.DelayedTextField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(value, options);
        }

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(label, value, options);
        }

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string FieldDelayed(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region float : EditorGUILayout.FloatField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="value"> <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="value"> <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, options);
        }

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(string label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(string label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, style, options);
        }

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(GUIContent label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(Texture label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Field(Texture label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region int : EditorGUILayout.IntField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="value"> <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="value"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, options);
        }

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, style, options);
        }

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(Texture label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Field(Texture label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region double : EditorGUILayout.DoubleField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="value"> <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="value"> <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, options);
        }

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(string label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(string label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, style, options);
        }

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(GUIContent label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(Texture label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public static double Field(Texture label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region long : EditorGUILayout.LongField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="value"> <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="value"> <see cref="long"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(value, options);
        }

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(string label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(string label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(GUIContent label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, style, options);
        }

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(GUIContent label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(Texture label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public static long Field(Texture label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region AnimationCurve : EditorGUILayout.CurveField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="value"> <see cref="AnimationCurve"/></param>
        /// <param name="color"> <see cref="Color"/></param>
        /// <param name="ranges"> <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(value, color, ranges, options);
        }

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="value"> <see cref="AnimationCurve"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(value, options);
        }

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="color"> <see cref="Color"/></param>
        /// <param name="ranges"> <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(string label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, color, ranges, options);
        }

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(string label, AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="color"> <see cref="Color"/></param>
        /// <param name="ranges"> <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(GUIContent label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, options);
        }

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(GUIContent label, AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="color"> <see cref="Color"/></param>
        /// <param name="ranges"> <see cref="Rect"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(Texture label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, color, ranges, options);
        }

        /// <summary>
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public static AnimationCurve Field(Texture label, AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Color : EditorGUILayout.ColorField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 Color
        /// </summary>
        /// <param name="value"> <see cref="Color"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public static Color Field(Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(value, options);
        }

        /// <summary>
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="showEyedropper"> <see cref="bool"/></param>
        /// <param name="showAlpha"> <see cref="bool"/></param>
        /// <param name="hdr"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public static Color Field(string label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary>
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public static Color Field(string label, Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="showEyedropper"> <see cref="bool"/></param>
        /// <param name="showAlpha"> <see cref="bool"/></param>
        /// <param name="hdr"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public static Color Field(GUIContent label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(label, value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary>
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public static Color Field(GUIContent label, Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="showEyedropper"> <see cref="bool"/></param>
        /// <param name="showAlpha"> <see cref="bool"/></param>
        /// <param name="hdr"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public static Color Field(Texture label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary>
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public static Color Field(Texture label, Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region Gradient : EditorGUILayout.GradientField

#if UNITY_2019_1_OR_NEWER
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="value"> <see cref="Gradient"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public static Gradient Field(Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(value, options);
        }

        /// <summary>
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="hdr"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public static Gradient Field(string label, Gradient value, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, hdr, options);
        }

        /// <summary>
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public static Gradient Field(string label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="hdr"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public static Gradient Field(GUIContent label, Gradient value, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(label, value, hdr, options);
        }

        /// <summary>
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public static Gradient Field(GUIContent label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(label, value, options);
        }

        /// <summary>
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="hdr"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public static Gradient Field(Texture label, Gradient value, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, hdr, options);
        }

        /// <summary>
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public static Gradient Field(Texture label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, options);
        }
    }

#endif
    #endregion

    #region string : EditorGUILayout.TextField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(value, options);
        }

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, value, style, options);
        }

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, value, options);
        }

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region T : EditorGUILayout.ObjectField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="value"> <see cref="T"/></param>
        /// <param name="type"> <see cref="Type"/></param>
        /// <param name="allowSceneObjects"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Field<T>(T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(value, type, allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type"> <see cref="Type"/></param>
        /// <param name="allowSceneObjects"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Field<T>(string label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(new GUIContent(label), value, type, allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type"> <see cref="Type"/></param>
        /// <param name="allowSceneObjects"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Field<T>(GUIContent label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, type, allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type"> <see cref="Type"/></param>
        /// <param name="allowSceneObjects"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Field<T>(Texture label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(new GUIContent(label), value, type, allowSceneObjects, options);
        }
    }
    #endregion

    #region int : EditorGUILayout.LayerField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="value"> <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(value, style, options);
        }

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="value"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(value, options);
        }

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, value, options);
        }

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(Texture label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Layer(Texture label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region string : EditorGUILayout.PasswordField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(value, style, options);
        }

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(value, options);
        }

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, value, options);
        }

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region int : EditorGUILayout.IntSlider
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 滑动条 int
        /// </summary>
        /// <param name="value"> <see cref="int"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Slider(int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Slider(string label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(new GUIContent(label), value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 int
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Slider(GUIContent label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(label, value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 int
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Slider(Texture label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(new GUIContent(label), value, leftValue, rightValue, options);
        }
    }
    #endregion

    #region float : EditorGUILayout.Slider
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 滑动条 float
        /// </summary>
        /// <param name="value"> <see cref="float"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Slider(float value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 float
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Slider(string label, float value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(new GUIContent(label), value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 float
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Slider(GUIContent label, float value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(label, value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 float
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="leftValue"> <see cref="int"/></param>
        /// <param name="rightValue"> <see cref="int"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public static float Slider(Texture label, float value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(new GUIContent(label), value, leftValue, rightValue, options);
        }
    }
    #endregion

    #region string : EditorGUILayout.TagField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(value, style, options);
        }

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="value"> <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(value, options);
        }

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, value, options);
        }

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 标签字段 string
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Tag(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region bool : EditorGUILayout.Toggle
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="value"> <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="value"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(Texture label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(Texture label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region bool : EditorGUILayout.Toggle
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="value"> <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="value"> <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(Texture label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Field(Texture label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region bool : EditorGUILayout.ToggleLeft
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 右侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ToggleLeft(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ToggleLeft(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ToggleLeft(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, style, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ToggleLeft(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ToggleLeft(Texture label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 bool
        /// </summary>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ToggleLeft(Texture label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(new GUIContent(label), value, options);
        }
    }
    #endregion

    #region int : EditorGUILayout.IntPopup
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(new GUIContent(label), value, displayedOptions, optionValues, options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, GUIContent[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(new GUIContent(label), value, displayedOptions, optionValues, style, options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, string[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues, options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, string[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues, style, options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, IEnumerable<GUIContent> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(new GUIContent(label), value, displayedOptions.ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, IEnumerable<GUIContent> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(new GUIContent(label), value, displayedOptions.ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, IEnumerable<string> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, IEnumerable<string> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, int[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, Array.ConvertAll<int, string>(displayedOptions, (i => i.ToString())), optionValues, options);
        }

        /// <summary>
        /// 绘制 弹窗整数 字段 int
        /// </summary>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">格式参数 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int value, int[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, Array.ConvertAll<int, string>(displayedOptions, (i => i.ToString())), optionValues, style, options);
        }
    }
    #endregion

    #region int : EditorGUILayout.Popup
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, GUIContent[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(GUIContent label, int selectedIndex, GUIContent[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(GUIContent label, int selectedIndex, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt; "/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, IEnumerable<GUIContent>  displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt; "/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, IEnumerable<GUIContent>  displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt; "/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(GUIContent label, int selectedIndex, IEnumerable<GUIContent>  displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt; "/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(GUIContent label, int selectedIndex, IEnumerable<GUIContent>  displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int selectedIndex, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt; "/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, IEnumerable<string>  displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt; "/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(int selectedIndex, IEnumerable<string>  displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt; "/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int selectedIndex, IEnumerable<string>  displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 int
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selectedIndex">选择下标 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt; "/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Popup(string label, int selectedIndex, IEnumerable<string>  displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions.ToArray(), style, options);
        }
    }
    #endregion

    #region  : EditorGUILayout.EnumPopup
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(selected, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(selected, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(GUIContent label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(GUIContent label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法 <see cref="Func&lt;Enum, bool&gt;"/></param>
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法 <see cref="Func&lt;Enum, bool&gt;"/></param>
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(string label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(string label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, style, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法 <see cref="Func&lt;Enum, bool&gt;"/></param>
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(string label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, checkEnabled, includeObsolete, options);
        }

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法 <see cref="Func&lt;Enum, bool&gt;"/></param>
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T Popup<T>(string label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, checkEnabled, includeObsolete, style, options);
        }
    }
    #endregion

    #region  : EditorGUILayout.HelpBox
    public partial class GELayout 
    {

        /// <summary>
        /// 帮助框 HelpBox 
        /// </summary>
        /// <param name="message">消息 <see cref="string"/></param>
        /// <param name="type">消息类型 <see cref="MessageType"/></param>
        public static void HelpBox(string message, MessageType type)
        {
             EditorGUILayout.HelpBox(message, type);
        }

        /// <summary>
        /// 帮助框 HelpBox 
        /// </summary>
        /// <param name="message">消息 <see cref="string"/></param>
        /// <param name="type">消息类型 <see cref="MessageType"/></param>
        /// <param name="wide = true">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分 <see cref="bool"/></param>
        public static void HelpBox(string message, MessageType type, bool wide = true)
        {
             EditorGUILayout.HelpBox(message, type, wide);
        }

        /// <summary>
        /// 帮助框 HelpBox 
        /// </summary>
        /// <param name="message">消息 <see cref="Texture"/></param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分 <see cref="bool"/></param>
        public static void HelpBox(Texture message, bool wide)
        {
             EditorGUILayout.HelpBox(new GUIContent(message), wide);
        }

        /// <summary>
        /// 帮助框 HelpBox 
        /// </summary>
        /// <param name="message">消息 <see cref="string"/></param>
        /// <param name="wide = true">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分 <see cref="bool"/></param>
        public static void HelpBox(string message, bool wide = true)
        {
             EditorGUILayout.HelpBox(new GUIContent(message), wide);
        }

        /// <summary>
        /// 帮助框 HelpBox 
        /// </summary>
        /// <param name="message">消息 <see cref="GUIContent"/></param>
        /// <param name="wide = true">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分 <see cref="bool"/></param>
        public static void HelpBox(GUIContent message, bool wide = true)
        {
             EditorGUILayout.HelpBox(message, wide);
        }
    }
    #endregion

    #region  : EditorGUILayout.TextArea
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制文本域 TextArea 
        /// </summary>
        /// <param name="value">文本内容 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Area(string value, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.TextArea(value, style, options);
        }

        /// <summary>
        /// 绘制文本域 TextArea 
        /// </summary>
        /// <param name="value">文本内容 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Area(string value, params GUILayoutOption[] options)
        {
             EditorGUILayout.TextArea(value, options);
        }
    }
    #endregion

    #region  : EditorGUILayout.LabelField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(GUIContent label, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(string label, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(string label, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(int label, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label.ToString(), options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(int label, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label.ToString(), style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(bool label, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label.ToString(), options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(bool label, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label.ToString(), style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="label2">向右显示的标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(GUIContent label, GUIContent label2, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, label2, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="label2">向右显示的标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(GUIContent label, GUIContent label2, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, label2, style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="label2">向右显示的标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(string label, string label2, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, label2, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="label2">向右显示的标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(string label, string label2, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.LabelField(label, label2, style, options);
        }
    }
    #endregion

    #region  : EditorGUILayout.PrefixLabel
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        public static void LabelPrefix(GUIContent label)
        {
             EditorGUILayout.PrefixLabel(label);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public static void LabelPrefix(GUIContent label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
             EditorGUILayout.PrefixLabel(label, followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public static void LabelPrefix(GUIContent label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
             EditorGUILayout.PrefixLabel(label, followingStyle);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        public static void LabelPrefix(string label)
        {
             EditorGUILayout.PrefixLabel(label);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public static void LabelPrefix(string label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
             EditorGUILayout.PrefixLabel(label, followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public static void LabelPrefix(string label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
             EditorGUILayout.PrefixLabel(label, followingStyle);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        public static void LabelPrefix(int label)
        {
             EditorGUILayout.PrefixLabel(label.ToString());
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public static void LabelPrefix(int label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
             EditorGUILayout.PrefixLabel(label.ToString(), followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public static void LabelPrefix(int label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
             EditorGUILayout.PrefixLabel(label.ToString(), followingStyle);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        public static void LabelPrefix(bool label)
        {
             EditorGUILayout.PrefixLabel(label.ToString());
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public static void LabelPrefix(bool label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
             EditorGUILayout.PrefixLabel(label.ToString(), followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public static void LabelPrefix(bool label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
             EditorGUILayout.PrefixLabel(label.ToString(), followingStyle);
        }
    }
    #endregion

    #region  : EditorGUILayout.SelectableLabel
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void LabelSelectable(string label, params GUILayoutOption[] options)
        {
             EditorGUILayout.SelectableLabel(label, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void LabelSelectable(string label, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.SelectableLabel(label, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void LabelSelectable(int label, params GUILayoutOption[] options)
        {
             EditorGUILayout.SelectableLabel(label.ToString(), options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void LabelSelectable(int label, GUIStyle style, params GUILayoutOption[] options)
        {
             EditorGUILayout.SelectableLabel(label.ToString(), style, options);
        }
    }
    #endregion

    #region  : EditorGUILayout.MaskField
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(string label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(string label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(string label, int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(string label, int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(GUIContent label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(GUIContent label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(GUIContent label, int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(GUIContent label, int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label.ToString(), mask, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label.ToString(), mask, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int label, int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label.ToString(), mask, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(int label, int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label.ToString(), mask, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(Texture label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(new GUIContent(label), mask, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(Texture label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(new GUIContent(label), mask, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(Texture label, int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(new GUIContent(label), mask, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Mask(Texture label, int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(new GUIContent(label), mask, displayedOptions.ToArray(), style, options);
        }
    }
    #endregion

    #region  : EditorGUILayout.EnumFlagsField

#if UNITY_2018_1_OR_NEWER
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(GUIContent label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(GUIContent label, T value, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, value, includeObsolete, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(GUIContent label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(GUIContent label, T value, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, value, includeObsolete, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(string label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(string label, T value, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, includeObsolete, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(string label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(string label, T value, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, includeObsolete, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(int label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(int label, T value, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, includeObsolete, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(int label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(int label, T value, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, includeObsolete, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(bool label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(bool label, T value, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, includeObsolete, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(bool label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(bool label, T value, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), value, includeObsolete, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(Texture label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(Texture label, T value, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, includeObsolete, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(Texture label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumFlags<T>(Texture label, T value, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), value, includeObsolete, style, options);
        }
    }

#endif
    #endregion

    #region  : EditorGUILayout.EnumMaskPopup

#if !UNITY_2020_1_OR_NEWER
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumPopupMask<T>(GUIContent label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumPopupMask<T>(GUIContent label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumPopupMask<T>(string label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumPopupMask<T>(string label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, style, options);
        }
    }

#endif
    #endregion

    #region  : EditorGUILayout.EnumMaskField

#if !UNITY_2020_1_OR_NEWER
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumMask<T>(T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumMask<T>(T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumMask<T>(GUIContent label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumMask<T>(GUIContent label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumMask<T>(string label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, options);
        }

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public  static T EnumMask<T>(string label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, style, options);
        }
    }

#endif
    #endregion

    #region  : EditorGUILayout.DropdownButton
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(GUIContent label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(label, FocusType.Passive, options);
        }

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(label, FocusType.Passive, style, options);
        }

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(string label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, options);
        }

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, style, options);
        }

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(Texture label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, options);
        }

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(Texture label, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, style, options);
        }
    }
    #endregion

    #region  : EditorGUILayout.LinkButton

#if UNITY_2021_1_OR_NEWER
    public partial class GELayout 
    {

        /// <summary>
        /// 绘制 Link按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonLink(GUIContent label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(label, options);
        }

        /// <summary>
        /// 绘制 Link按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonLink(string label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(new GUIContent(label), options);
        }

        /// <summary>
        /// 绘制 Link按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonLink(Texture label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(new GUIContent(label), options);
        }
    }

#endif
    #endregion
}
