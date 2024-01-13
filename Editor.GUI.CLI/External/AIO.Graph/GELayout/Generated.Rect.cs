/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate

|||✩ - - - - - |*/

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 //

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Internal;

namespace AIO.UEditor
{
    /// <summary>
    /// Layout
    /// </summary>
    public partial class GELayout
    {
        #region TextArea

        /// <summary>
        /// 绘制 文本域
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="content">内容 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string AreaText(Rect rect, string content, GUIStyle style)
        {
            return EditorGUI.TextArea(rect, content, style);
        }

        /// <summary>
        /// 绘制 文本域
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="content">内容 <see cref="string"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string AreaText(Rect rect, string content)
        {
            return EditorGUI.TextArea(rect, content, EditorStyles.textArea);
        }

        #endregion

        #region Button

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Rect rect, GUIContent label)
        {
            return GUI.Button(rect, label);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Rect rect, GUIContent label, GUIStyle style)
        {
            return GUI.Button(rect, label, style);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Vector2 pos, Vector2 size, GUIContent label)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Vector2 pos, Vector2 size, GUIContent label, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label, style);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public new static void Button(Rect rect, GUIContent label, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, label)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public new static void Button(Rect rect, GUIContent label, Action action, GUIStyle style)
        {
            if (action is null) return;
            if (GUI.Button(rect, label, style)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public new static void Button(Vector2 pos, Vector2 size, GUIContent label, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), label)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public new static void Button(Vector2 pos, Vector2 size, GUIContent label, Action action, GUIStyle style)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), label, style)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Rect rect, string label)
        {
            return GUI.Button(rect, label);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Rect rect, string label, GUIStyle style)
        {
            return GUI.Button(rect, label, style);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Vector2 pos, Vector2 size, string label)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Vector2 pos, Vector2 size, string label, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label, style);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public new static void Button(Rect rect, string label, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, label)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public new static void Button(Rect rect, string label, Action action, GUIStyle style)
        {
            if (action is null) return;
            if (GUI.Button(rect, label, style)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public new static void Button(Vector2 pos, Vector2 size, string label, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), label)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public new static void Button(Vector2 pos, Vector2 size, string label, Action action, GUIStyle style)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), label, style)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Rect rect, Texture label)
        {
            return GUI.Button(rect, label);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Rect rect, Texture label, GUIStyle style)
        {
            return GUI.Button(rect, label, style);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Vector2 pos, Vector2 size, Texture label)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Button(Vector2 pos, Vector2 size, Texture label, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label, style);
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public new static void Button(Rect rect, Texture label, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, label)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public new static void Button(Rect rect, Texture label, Action action, GUIStyle style)
        {
            if (action is null) return;
            if (GUI.Button(rect, label, style)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public new static void Button(Vector2 pos, Vector2 size, Texture label, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), label)) action();
        }

        /// <summary>
        /// 绘制 按钮
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public new static void Button(Vector2 pos, Vector2 size, Texture label, Action action, GUIStyle style)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), label, style)) action();
        }

        #endregion

        #region Change Check

        /// <summary>
        /// 开启代码块来检查GUI更改
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        [ExcludeFromDocs]
        public static void VChangeCheck(Action action)
        {
            if (action == null) return;
            EditorGUI.BeginChangeCheck();
            action.Invoke();
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// 启动一个新的代码块来检查GUI更改
        /// </summary>
        [ExcludeFromDocs]
        public static void VChangeCheckBegin()
        {
            EditorGUI.BeginChangeCheck();
        }

        /// <summary>
        /// 关闭代码块
        /// </summary>
        [ExcludeFromDocs]
        public static void VChangeCheckEnd()
        {
            EditorGUI.EndChangeCheck();
        }

        #endregion

        #region Disabled Group

        /// <summary>
        /// 开启代码块来检查GUI更改
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="disable">禁用 <see cref="bool"/></param>
        [ExcludeFromDocs]
        public static void VDisabledGroup(Action action, bool disable)
        {
            if (action == null) return;
            EditorGUI.BeginDisabledGroup(disable);
            action.Invoke();
            EditorGUI.EndChangeCheck();
        }

        /// <summary>
        /// 启动一个新的代码块来检查GUI更改
        /// </summary>
        /// <param name="disable">禁用 <see cref="bool"/></param>
        [ExcludeFromDocs]
        public static void VDisabledGroupBegin(bool disable)
        {
            EditorGUI.BeginDisabledGroup(disable);
        }

        /// <summary>
        /// 关闭代码块
        /// </summary>
        [ExcludeFromDocs]
        public static void VDisabledGroupEnd()
        {
            EditorGUI.EndDisabledGroup();
        }

        #endregion

        #region Vector

        /// <summary>
        /// 创建区域 绘制 Vector2 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <returns><see cref="Vector2"/></returns>
        [ExcludeFromDocs]
        public static Vector2 Field(Rect rect, GTContent label, Vector2 value)
        {
            return EditorGUI.Vector2Field(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector2 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector2"/></param>
        /// <returns><see cref="Vector2"/></returns>
        [ExcludeFromDocs]
        public static Vector2 Field(Vector2 pos, Vector2 size, GTContent label, Vector2 value)
        {
            return EditorGUI.Vector2Field(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector2Int 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        [ExcludeFromDocs]
        public static Vector2Int Field(Rect rect, GTContent label, Vector2Int value)
        {
            return EditorGUI.Vector2IntField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector2Int 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector2Int"/></param>
        /// <returns><see cref="Vector2Int"/></returns>
        [ExcludeFromDocs]
        public static Vector2Int Field(Vector2 pos, Vector2 size, GTContent label, Vector2Int value)
        {
            return EditorGUI.Vector2IntField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector3 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <returns><see cref="Vector3"/></returns>
        [ExcludeFromDocs]
        public static Vector3 Field(Rect rect, GTContent label, Vector3 value)
        {
            return EditorGUI.Vector3Field(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector3 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector3"/></param>
        /// <returns><see cref="Vector3"/></returns>
        [ExcludeFromDocs]
        public static Vector3 Field(Vector2 pos, Vector2 size, GTContent label, Vector3 value)
        {
            return EditorGUI.Vector3Field(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector3Int 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        [ExcludeFromDocs]
        public static Vector3Int Field(Rect rect, GTContent label, Vector3Int value)
        {
            return EditorGUI.Vector3IntField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector3Int 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector3Int"/></param>
        /// <returns><see cref="Vector3Int"/></returns>
        [ExcludeFromDocs]
        public static Vector3Int Field(Vector2 pos, Vector2 size, GTContent label, Vector3Int value)
        {
            return EditorGUI.Vector3IntField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector4 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <returns><see cref="Vector4"/></returns>
        [ExcludeFromDocs]
        public static Vector4 Field(Rect rect, GTContent label, Vector4 value)
        {
            return EditorGUI.Vector4Field(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Vector4 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Vector4"/></param>
        /// <returns><see cref="Vector4"/></returns>
        [ExcludeFromDocs]
        public static Vector4 Field(Vector2 pos, Vector2 size, GTContent label, Vector4 value)
        {
            return EditorGUI.Vector4Field(new Rect(pos, size), label, value);
        }

        #endregion

        #region Color

        /// <summary>
        /// 创建区域 绘制 Color 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <returns><see cref="Color"/></returns>
        [ExcludeFromDocs]
        public static Color Field(Rect rect, Color value)
        {
            return EditorGUI.ColorField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 Color 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <returns><see cref="Color"/></returns>
        [ExcludeFromDocs]
        public static Color Field(Rect rect, GTContent label, Color value)
        {
            return EditorGUI.ColorField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Color 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <returns><see cref="Color"/></returns>
        [ExcludeFromDocs]
        public static Color Field(Vector2 pos, Vector2 size, Color value)
        {
            return EditorGUI.ColorField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 Color 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <returns><see cref="Color"/></returns>
        [ExcludeFromDocs]
        public static Color Field(Vector2 pos, Vector2 size, GTContent label, Color value)
        {
            return EditorGUI.ColorField(new Rect(pos, size), label, value);
        }

        #endregion

        #region Bounds

        /// <summary>
        /// 创建区域 绘制 Bounds 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <returns><see cref="Bounds"/></returns>
        [ExcludeFromDocs]
        public static Bounds Field(Rect rect, Bounds value)
        {
            return EditorGUI.BoundsField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 Bounds 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <returns><see cref="Bounds"/></returns>
        [ExcludeFromDocs]
        public static Bounds Field(Rect rect, GTContent label, Bounds value)
        {
            return EditorGUI.BoundsField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Bounds 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <returns><see cref="Bounds"/></returns>
        [ExcludeFromDocs]
        public static Bounds Field(Vector2 pos, Vector2 size, Bounds value)
        {
            return EditorGUI.BoundsField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 Bounds 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <returns><see cref="Bounds"/></returns>
        [ExcludeFromDocs]
        public static Bounds Field(Vector2 pos, Vector2 size, GTContent label, Bounds value)
        {
            return EditorGUI.BoundsField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 BoundsInt 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        [ExcludeFromDocs]
        public static BoundsInt Field(Rect rect, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 BoundsInt 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        [ExcludeFromDocs]
        public static BoundsInt Field(Rect rect, GTContent label, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 BoundsInt 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        [ExcludeFromDocs]
        public static BoundsInt Field(Vector2 pos, Vector2 size, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 BoundsInt 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        [ExcludeFromDocs]
        public static BoundsInt Field(Vector2 pos, Vector2 size, GTContent label, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(new Rect(pos, size), label, value);
        }

        #endregion

        #region Rect

        /// <summary>
        /// 创建区域 绘制 Rect 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <returns><see cref="Rect"/></returns>
        [ExcludeFromDocs]
        public static Rect Field(Rect rect, Rect value)
        {
            return EditorGUI.RectField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 Rect 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <returns><see cref="Rect"/></returns>
        [ExcludeFromDocs]
        public static Rect Field(Rect rect, GTContent label, Rect value)
        {
            return EditorGUI.RectField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Rect 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <returns><see cref="Rect"/></returns>
        [ExcludeFromDocs]
        public static Rect Field(Vector2 pos, Vector2 size, Rect value)
        {
            return EditorGUI.RectField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 Rect 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <returns><see cref="Rect"/></returns>
        [ExcludeFromDocs]
        public static Rect Field(Vector2 pos, Vector2 size, GTContent label, Rect value)
        {
            return EditorGUI.RectField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 RectInt 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <returns><see cref="RectInt"/></returns>
        [ExcludeFromDocs]
        public static RectInt Field(Rect rect, RectInt value)
        {
            return EditorGUI.RectIntField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 RectInt 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <returns><see cref="RectInt"/></returns>
        [ExcludeFromDocs]
        public static RectInt Field(Rect rect, GTContent label, RectInt value)
        {
            return EditorGUI.RectIntField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 RectInt 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <returns><see cref="RectInt"/></returns>
        [ExcludeFromDocs]
        public static RectInt Field(Vector2 pos, Vector2 size, RectInt value)
        {
            return EditorGUI.RectIntField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 RectInt 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <returns><see cref="RectInt"/></returns>
        [ExcludeFromDocs]
        public static RectInt Field(Vector2 pos, Vector2 size, GTContent label, RectInt value)
        {
            return EditorGUI.RectIntField(new Rect(pos, size), label, value);
        }

        #endregion

        #region AnimationCurve

        /// <summary>
        /// 创建区域 绘制 AnimationCurve 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        [ExcludeFromDocs]
        public static AnimationCurve Field(Rect rect, AnimationCurve value)
        {
            return EditorGUI.CurveField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 AnimationCurve 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        [ExcludeFromDocs]
        public static AnimationCurve Field(Rect rect, GTContent label, AnimationCurve value)
        {
            return EditorGUI.CurveField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 AnimationCurve 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        [ExcludeFromDocs]
        public static AnimationCurve Field(Vector2 pos, Vector2 size, AnimationCurve value)
        {
            return EditorGUI.CurveField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 AnimationCurve 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        [ExcludeFromDocs]
        public static AnimationCurve Field(Vector2 pos, Vector2 size, GTContent label, AnimationCurve value)
        {
            return EditorGUI.CurveField(new Rect(pos, size), label, value);
        }

        #endregion

        #region Number

        /// <summary>
        /// 创建区域 绘制 double 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <returns><see cref="double"/></returns>
        [ExcludeFromDocs]
        public static double Field(Rect rect, double value)
        {
            return EditorGUI.DoubleField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 double 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <returns><see cref="double"/></returns>
        [ExcludeFromDocs]
        public static double Field(Rect rect, GTContent label, double value)
        {
            return EditorGUI.DoubleField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 double 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <returns><see cref="double"/></returns>
        [ExcludeFromDocs]
        public static double Field(Vector2 pos, Vector2 size, double value)
        {
            return EditorGUI.DoubleField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 double 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <returns><see cref="double"/></returns>
        [ExcludeFromDocs]
        public static double Field(Vector2 pos, Vector2 size, GTContent label, double value)
        {
            return EditorGUI.DoubleField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 float 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <returns><see cref="float"/></returns>
        [ExcludeFromDocs]
        public static float Field(Rect rect, float value)
        {
            return EditorGUI.FloatField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 float 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <returns><see cref="float"/></returns>
        [ExcludeFromDocs]
        public static float Field(Rect rect, GTContent label, float value)
        {
            return EditorGUI.FloatField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 float 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <returns><see cref="float"/></returns>
        [ExcludeFromDocs]
        public static float Field(Vector2 pos, Vector2 size, float value)
        {
            return EditorGUI.FloatField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 float 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <returns><see cref="float"/></returns>
        [ExcludeFromDocs]
        public static float Field(Vector2 pos, Vector2 size, GTContent label, float value)
        {
            return EditorGUI.FloatField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 int 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <returns><see cref="int"/></returns>
        [ExcludeFromDocs]
        public static int Field(Rect rect, int value)
        {
            return EditorGUI.IntField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 int 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <returns><see cref="int"/></returns>
        [ExcludeFromDocs]
        public static int Field(Rect rect, GTContent label, int value)
        {
            return EditorGUI.IntField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 int 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <returns><see cref="int"/></returns>
        [ExcludeFromDocs]
        public static int Field(Vector2 pos, Vector2 size, int value)
        {
            return EditorGUI.IntField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 int 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <returns><see cref="int"/></returns>
        [ExcludeFromDocs]
        public static int Field(Vector2 pos, Vector2 size, GTContent label, int value)
        {
            return EditorGUI.IntField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建区域 绘制 long 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <returns><see cref="long"/></returns>
        [ExcludeFromDocs]
        public static long Field(Rect rect, long value)
        {
            return EditorGUI.LongField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 long 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <returns><see cref="long"/></returns>
        [ExcludeFromDocs]
        public static long Field(Rect rect, GTContent label, long value)
        {
            return EditorGUI.LongField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 long 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <returns><see cref="long"/></returns>
        [ExcludeFromDocs]
        public static long Field(Vector2 pos, Vector2 size, long value)
        {
            return EditorGUI.LongField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 long 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <returns><see cref="long"/></returns>
        [ExcludeFromDocs]
        public static long Field(Vector2 pos, Vector2 size, GTContent label, long value)
        {
            return EditorGUI.LongField(new Rect(pos, size), label, value);
        }

        #endregion

        #region Gradient

        /// <summary>
        /// 创建区域 绘制 Gradient 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <returns><see cref="Gradient"/></returns>
        [ExcludeFromDocs]
        public static Gradient Field(Rect rect, Gradient value)
        {
            return EditorGUI.GradientField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 Gradient 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <returns><see cref="Gradient"/></returns>
        [ExcludeFromDocs]
        public static Gradient Field(Rect rect, GTContent label, Gradient value)
        {
            return EditorGUI.GradientField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 Gradient 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <returns><see cref="Gradient"/></returns>
        [ExcludeFromDocs]
        public static Gradient Field(Vector2 pos, Vector2 size, Gradient value)
        {
            return EditorGUI.GradientField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 Gradient 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <returns><see cref="Gradient"/></returns>
        [ExcludeFromDocs]
        public static Gradient Field(Vector2 pos, Vector2 size, GTContent label, Gradient value)
        {
            return EditorGUI.GradientField(new Rect(pos, size), label, value);
        }

        #endregion

        #region Text

        /// <summary>
        /// 创建区域 绘制 string 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <returns><see cref="string"/></returns>
        [ExcludeFromDocs]
        public static string Field(Rect rect, string value)
        {
            return EditorGUI.TextField(rect, value);
        }

        /// <summary>
        /// 创建区域 绘制 string 字段
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <returns><see cref="string"/></returns>
        [ExcludeFromDocs]
        public static string Field(Rect rect, GTContent label, string value)
        {
            return EditorGUI.TextField(rect, label, value);
        }

        /// <summary>
        /// 创建区域 绘制 string 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <returns><see cref="string"/></returns>
        [ExcludeFromDocs]
        public static string Field(Vector2 pos, Vector2 size, string value)
        {
            return EditorGUI.TextField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建区域 绘制 string 字段
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="label">标题 <see cref="GTContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <returns><see cref="string"/></returns>
        [ExcludeFromDocs]
        public static string Field(Vector2 pos, Vector2 size, GTContent label, string value)
        {
            return EditorGUI.TextField(new Rect(pos, size), label, value);
        }

        #endregion

        #region Foldout Header Group

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRect(Rect rect, GTContent label, bool foldout, Action action)
        {
            foldout = EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label);
            if (foldout) action?.Invoke();
            EditorGUI.EndFoldoutHeaderGroup();
            return foldout;
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRect(Rect rect, GTContent label, bool foldout, Action action, GUIStyle style)
        {
            foldout = EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label, style);
            if (foldout) action?.Invoke();
            EditorGUI.EndFoldoutHeaderGroup();
            return foldout;
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRect(Rect rect, GTContent label, bool foldout, Action action, GUIStyle style, Action<Rect> menuAction)
        {
            foldout = EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label, style, menuAction);
            if (foldout) action?.Invoke();
            EditorGUI.EndFoldoutHeaderGroup();
            return foldout;
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <param name="menuIcon">菜单ICON显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRect(Rect rect, GTContent label, bool foldout, Action action, GUIStyle style, Action<Rect> menuAction, GUIStyle menuIcon)
        {
            foldout = EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label, style, menuAction, menuIcon);
            if (foldout) action?.Invoke();
            EditorGUI.EndFoldoutHeaderGroup();
            return foldout;
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 开始绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRectBegin(Rect rect, GTContent label, bool foldout)
        {
            return EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label);
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 开始绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRectBegin(Rect rect, GTContent label, bool foldout, GUIStyle style)
        {
            return EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label, style);
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 开始绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRectBegin(Rect rect, GTContent label, bool foldout, GUIStyle style, Action<Rect> menuAction)
        {
            return EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label, style, menuAction);
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 开始绘制 折页排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <param name="menuIcon">菜单ICON显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public static bool VFoldoutHeaderGroupRectBegin(Rect rect, GTContent label, bool foldout, GUIStyle style, Action<Rect> menuAction, GUIStyle menuIcon)
        {
            return EditorGUI.BeginFoldoutHeaderGroup(rect, foldout, label, style, menuAction, menuIcon);
        }

#endif

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// 结束绘制 折页排版
        /// </summary>
        [ExcludeFromDocs]
        public static void VFoldoutHeaderGroupRectEnd()
        {
            EditorGUI.EndFoldoutHeaderGroup();
        }

#endif

        #endregion

        #region Property Rect

        /// <summary>
        /// 绘制 属性排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="property">属性 <see cref="SerializedProperty"/></param>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        [ExcludeFromDocs]
        public static void VPropertyRect(Rect rect, GTContent label, SerializedProperty property, Action action)
        {
            EditorGUI.BeginProperty(rect, label, property);
            action?.Invoke();
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// 开始绘制 属性排版
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="property">属性 <see cref="SerializedProperty"/></param>
        [ExcludeFromDocs]
        public static void VPropertyRectBegin(Rect rect, GTContent label, SerializedProperty property)
        {
            EditorGUI.BeginProperty(rect, label, property);
        }

        /// <summary>
        /// 结束绘制 属性排版
        /// </summary>
        [ExcludeFromDocs]
        public static void VPropertyRectEnd()
        {
            EditorGUI.EndProperty();
        }

        #endregion

        #region PropertyField

        /// <summary>
        /// 绘制 Serialized Property
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="property">属性 <see cref="SerializedProperty"/></param>
        /// <param name="includeChildren">是否包含子属性 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool SP(Rect rect, SerializedProperty property, bool includeChildren = false)
        {
            return EditorGUI.PropertyField(rect, property, includeChildren);
        }

        /// <summary>
        /// 绘制 Serialized Property
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="property">属性 <see cref="SerializedProperty"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="includeChildren">是否包含子属性 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool SP(Rect rect, SerializedProperty property, string label, bool includeChildren = false)
        {
            return EditorGUI.PropertyField(rect, property, new GUIContent(label), includeChildren);
        }

        /// <summary>
        /// 绘制 Serialized Property
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="property">属性 <see cref="SerializedProperty"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="includeChildren">是否包含子属性 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool SP(Vector2 pos, Vector2 size, SerializedProperty property, string label,
            bool includeChildren = false)
        {
            return EditorGUI.PropertyField(new Rect(pos - size / 2, size), property, new GUIContent(label),
                includeChildren);
        }

        /// <summary>
        /// 绘制 Serialized Property
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="property">属性 <see cref="SerializedProperty"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="includeChildren">是否包含子属性 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool SP(Rect rect, SerializedProperty property, GUIContent label, bool includeChildren = false)
        {
            return EditorGUI.PropertyField(rect, property, label, includeChildren);
        }

        /// <summary>
        /// 绘制 Serialized Property
        /// </summary>
        /// <param name="pos">位置 <see cref="Vector2"/></param>
        /// <param name="size">大小 <see cref="Vector2"/></param>
        /// <param name="property">属性 <see cref="SerializedProperty"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="includeChildren">是否包含子属性 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool SP(Vector2 pos, Vector2 size, SerializedProperty property, GUIContent label,
            bool includeChildren = false)
        {
            return EditorGUI.PropertyField(new Rect(pos - size / 2, size), property, label, includeChildren);
        }

        #endregion
    }
}
