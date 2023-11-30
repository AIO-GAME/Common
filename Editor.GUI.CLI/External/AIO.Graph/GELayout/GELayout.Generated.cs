/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate
|||✩ Document: ||| ->
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

        #region Button

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, params GUILayoutOption[] options)
        {
            return GUILayout.Button(label, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, float width)
        {
            return GUILayout.Button(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, float width, float height)
        {
            return GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Button(label, style, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, GUIStyle style, float width)
        {
            return GUILayout.Button(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, GUIStyle style, float width, float height)
        {
            return GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Button(GUIContent label, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Button(GUIContent label, Action action, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Button(GUIContent label, Action action, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Button(GUIContent label, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, style, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Button(GUIContent label, Action action, GUIStyle style, float width)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Button(GUIContent label, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, params GUILayoutOption[] options)
        {
            return GUILayout.Button(label, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, float width)
        {
            return GUILayout.Button(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, float width, float height)
        {
            return GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Button(label, style, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, GUIStyle style, float width)
        {
            return GUILayout.Button(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, GUIStyle style, float width, float height)
        {
            return GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Button(string label, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Button(string label, Action action, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Button(string label, Action action, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Button(string label, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, style, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Button(string label, Action action, GUIStyle style, float width)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Button(string label, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

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

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(GTContent label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, options);
        }

        /// <summary>
        /// 绘制 下拉按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonDropdown(GTContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Passive, style, options);
        }

#if UNITY_2021_1_OR_NEWER

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

#endif

#if UNITY_2021_1_OR_NEWER

        /// <summary>
        /// 绘制 Link按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonLink(string label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(label, options);
        }

#endif

#if UNITY_2021_1_OR_NEWER

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

#endif

#if UNITY_2021_1_OR_NEWER

        /// <summary>
        /// 绘制 Link按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GTContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonLink(GTContent label, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LinkButton(label, options);
        }

#endif

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        public static void ButtonCopy<T>(string label, T content) 
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy<T>(string label, T content, GUIStyle style) 
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonCopy<T>(string label, T content, float width) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy<T>(string label, T content, float width, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonCopy<T>(string label, T content, float width, float height) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy<T>(string label, T content, float width, float height, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        public static void ButtonCopy(string label, string content)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, string content, GUIStyle style)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonCopy(string label, string content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, string content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonCopy(string label, string content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, string content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        public static void ButtonCopy(string label, long content)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, long content, GUIStyle style)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonCopy(string label, long content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, long content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonCopy(string label, long content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, long content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        public static void ButtonCopy(string label, double content)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, double content, GUIStyle style)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonCopy(string label, double content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, double content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonCopy(string label, double content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, double content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        public static void ButtonCopy(string label, Color content)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, Color content, GUIStyle style)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonCopy(string label, Color content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, Color content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonCopy(string label, Color content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, Color content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        public static void ButtonCopy(string label, Color32 content)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, Color32 content, GUIStyle style)
        {
            if (GUILayout.Button(label)) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonCopy(string label, Color32 content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, Color32 content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonCopy(string label, Color32 content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 复制按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonCopy(string label, Color32 content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) GEHelper.CopyAction(content);
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        public static void ButtonPaste<T>(string label, ref T content) 
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteAction<T>();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste<T>(string label, ref T content, GUIStyle style) 
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteAction<T>();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonPaste<T>(string label, ref T content, float width) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content = GEHelper.PasteAction<T>();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste<T>(string label, ref T content, float width, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content = GEHelper.PasteAction<T>();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonPaste<T>(string label, ref T content, float width, float height) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteAction<T>();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="T"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste<T>(string label, ref T content, float width, float height, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteAction<T>();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        public static void ButtonPaste(string label, ref string content)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionString();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref string content, GUIStyle style)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionString();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref string content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content = GEHelper.PasteActionString();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref string content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content = GEHelper.PasteActionString();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref string content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionString();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref string content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionString();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        public static void ButtonPaste(string label, ref long content)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionLong();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref long content, GUIStyle style)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionLong();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref long content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content = GEHelper.PasteActionLong();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref long content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content = GEHelper.PasteActionLong();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref long content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionLong();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="long"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref long content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionLong();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        public static void ButtonPaste(string label, ref double content)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionDouble();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref double content, GUIStyle style)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionDouble();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref double content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content = GEHelper.PasteActionDouble();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref double content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content = GEHelper.PasteActionDouble();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref double content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionDouble();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="double"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref double content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionDouble();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        public static void ButtonPaste(string label, ref Color content)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionColor();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref Color content, GUIStyle style)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionColor();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref Color content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content = GEHelper.PasteActionColor();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref Color content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content = GEHelper.PasteActionColor();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref Color content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionColor();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref Color content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionColor();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        public static void ButtonPaste(string label, ref Color32 content)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionColor32();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref Color32 content, GUIStyle style)
        {
            if (GUILayout.Button(label)) content = GEHelper.PasteActionColor32();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref Color32 content, float width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content = GEHelper.PasteActionColor32();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref Color32 content, float width, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content = GEHelper.PasteActionColor32();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonPaste(string label, ref Color32 content, float width, float height)
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionColor32();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">复制值 <see cref="Color32"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonPaste(string label, ref Color32 content, float width, float height, GUIStyle style)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content = GEHelper.PasteActionColor32();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        public static void ButtonClear<T>(string label, ICollection<T> content) 
        {
            if (GUILayout.Button(label)) content.Clear();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonClear<T>(string label, ICollection<T> content, GUIStyle style) 
        {
            if (GUILayout.Button(label)) content.Clear();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonClear<T>(string label, ICollection<T> content, float width) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content.Clear();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonClear<T>(string label, ICollection<T> content, float width, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content.Clear();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonClear<T>(string label, ICollection<T> content, float width, float height) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content.Clear();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonClear<T>(string label, ICollection<T> content, float width, float height, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content.Clear();
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content) 
        {
            if (GUILayout.Button(label)) content.Add(default);
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, GUIStyle style) 
        {
            if (GUILayout.Button(label)) content.Add(default);
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, float width) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content.Add(default);
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, float width, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content.Add(default);
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, float width, float height) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content.Add(default);
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, float width, float height, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content.Add(default);
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="func">添加值回调 <see cref="Func&lt;T&gt;"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, Func<T> func) 
        {
            if (GUILayout.Button(label)) content.Add(func());
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="func">添加值回调 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, Func<T> func, GUIStyle style) 
        {
            if (GUILayout.Button(label)) content.Add(func());
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="func">添加值回调 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, Func<T> func, float width) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) content.Add(func());
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="func">添加值回调 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, Func<T> func, float width, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) content.Add(func());
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="func">添加值回调 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, Func<T> func, float width, float height) 
        {
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height))) content.Add(func());
        }

        /// <summary>
        /// 绘制 粘贴按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="content">值 <see cref="ICollection&lt;T&gt;"/></param>
        /// <param name="func">添加值回调 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void ButtonAdd<T>(string label, ICollection<T> content, Func<T> func, float width, float height, GUIStyle style) 
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height))) content.Add(func());
        }

        #endregion

        #region Button Repeat

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(label, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, float width)
        {
            return GUILayout.RepeatButton(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, float width, float height)
        {
            return GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(label, style, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, GUIStyle style, float width)
        {
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, GUIStyle style, float width, float height)
        {
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(label, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, float width)
        {
            return GUILayout.RepeatButton(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, float width, float height)
        {
            return GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(label, style, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, GUIStyle style, float width)
        {
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, GUIStyle style, float width, float height)
        {
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(label, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, float width)
        {
            if (GUILayout.RepeatButton(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, float width, float height)
        {
            if (GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(label, style, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, GUIStyle style, float width)
        {
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void ButtonRepeat(string label, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(label, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonRepeat(string label, Action action, float width)
        {
            if (GUILayout.RepeatButton(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonRepeat(string label, Action action, float width, float height)
        {
            if (GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void ButtonRepeat(string label, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(label, style, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonRepeat(string label, Action action, GUIStyle style, float width)
        {
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonRepeat(string label, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void ButtonRepeat(Texture label, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonRepeat(Texture label, Action action, float width)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonRepeat(Texture label, Action action, float width, float height)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void ButtonRepeat(Texture label, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), style, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void ButtonRepeat(Texture label, Action action, GUIStyle style, float width)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void ButtonRepeat(Texture label, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        #endregion

        #region Mask

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Mask(int mask, string[] displayedOptions, params GUILayoutOption[] options)
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
        public new static int Mask(int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Mask(string label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
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
        public new static int Mask(string label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Mask(int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
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
        public new static int Mask(int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Mask(string label, int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
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
        public new static int Mask(string label, int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="mask">选择值 <see cref="int"/></param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Mask(GUIContent label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
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
        public new static int Mask(GUIContent label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
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
        public new static int Mask(GUIContent label, int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
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
        public new static int Mask(GUIContent label, int mask, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), style, options);
        }

        #endregion

        #region Enum

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(selected, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(GUIContent label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, selected, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(GUIContent label, T selected, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, selected, includeObsolete, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(selected, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(GUIContent label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, selected, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(GUIContent label, T selected, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(label, selected, includeObsolete, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(string label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), selected, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(string label, T selected, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), selected, includeObsolete, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(string label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), selected, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(string label, T selected, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label), selected, includeObsolete, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(int label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(int label, T selected, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, includeObsolete, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(int label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="int"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(int label, T selected, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, includeObsolete, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(bool label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(bool label, T selected, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, includeObsolete, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(bool label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, style, options);
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="bool"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="includeObsolete">true:包含带有ObsoleteAttribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumFlags<T>(bool label, T selected, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumFlagsField(new GUIContent(label.ToString()), selected, includeObsolete, style, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumPopupMask<T>(GUIContent label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumPopupMask<T>(GUIContent label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, style, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumPopupMask<T>(string label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumPopupMask<T>(string label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskPopup(label, selected, style, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumMask<T>(T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(value, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumMask<T>(T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(value, style, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumMask<T>(GUIContent label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumMask<T>(GUIContent label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, style, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumMask<T>(string label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, options);
        }

#endif

#if !UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 枚举菜单 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T EnumMask<T>(string label, T value, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumMaskField(label, value, style, options);
        }

#endif

        #endregion

        #region Struct

        /// <summary>
        /// 绘制 Bounds 
        /// </summary>
        /// <param name="value">值 <see cref="Bounds"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Bounds"/></returns>
        public new static Bounds Field(Bounds value, params GUILayoutOption[] options)
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
        public new static Bounds Field(string label, Bounds value, params GUILayoutOption[] options)
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
        public new static Bounds Field(GUIContent label, Bounds value, params GUILayoutOption[] options)
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
        public new static Bounds Field(Texture label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 BoundsInt 
        /// </summary>
        /// <param name="value">值 <see cref="BoundsInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="BoundsInt"/></returns>
        public new static BoundsInt Field(BoundsInt value, params GUILayoutOption[] options)
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
        public new static BoundsInt Field(string label, BoundsInt value, params GUILayoutOption[] options)
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
        public new static BoundsInt Field(GUIContent label, BoundsInt value, params GUILayoutOption[] options)
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
        public new static BoundsInt Field(Texture label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 RectInt 
        /// </summary>
        /// <param name="value">值 <see cref="RectInt"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="RectInt"/></returns>
        public new static RectInt Field(RectInt value, params GUILayoutOption[] options)
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
        public new static RectInt Field(string label, RectInt value, params GUILayoutOption[] options)
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
        public new static RectInt Field(GUIContent label, RectInt value, params GUILayoutOption[] options)
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
        public new static RectInt Field(Texture label, RectInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.RectIntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Rect 
        /// </summary>
        /// <param name="value">值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Rect"/></returns>
        public new static Rect Field(Rect value, params GUILayoutOption[] options)
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
        public new static Rect Field(string label, Rect value, params GUILayoutOption[] options)
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
        public new static Rect Field(GUIContent label, Rect value, params GUILayoutOption[] options)
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
        public new static Rect Field(Texture label, Rect value, params GUILayoutOption[] options)
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
        public new static Vector2Int Field(string label, Vector2Int value, params GUILayoutOption[] options)
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
        public new static Vector2Int Field(GUIContent label, Vector2Int value, params GUILayoutOption[] options)
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
        public new static Vector2Int Field(Texture label, Vector2Int value, params GUILayoutOption[] options)
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
        public new static Vector3Int Field(string label, Vector3Int value, params GUILayoutOption[] options)
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
        public new static Vector3Int Field(GUIContent label, Vector3Int value, params GUILayoutOption[] options)
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
        public new static Vector3Int Field(Texture label, Vector3Int value, params GUILayoutOption[] options)
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
        public new static Vector4 Field(string label, Vector4 value, params GUILayoutOption[] options)
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
        public new static Vector4 Field(GUIContent label, Vector4 value, params GUILayoutOption[] options)
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
        public new static Vector4 Field(Texture label, Vector4 value, params GUILayoutOption[] options)
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
        public new static Vector3 Field(string label, Vector3 value, params GUILayoutOption[] options)
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
        public new static Vector3 Field(GUIContent label, Vector3 value, params GUILayoutOption[] options)
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
        public new static Vector3 Field(Texture label, Vector3 value, params GUILayoutOption[] options)
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
        public new static Vector2 Field(string label, Vector2 value, params GUILayoutOption[] options)
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
        public new static Vector2 Field(GUIContent label, Vector2 value, params GUILayoutOption[] options)
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
        public new static Vector2 Field(Texture label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(new GUIContent(label), value, options);
        }

        #endregion

        #region Delayed

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float FieldDelayed(float value, params GUILayoutOption[] options)
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
        public new static float FieldDelayed(string label, float value, params GUILayoutOption[] options)
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
        public new static float FieldDelayed(float value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static float FieldDelayed(string label, float value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static float FieldDelayed(GUIContent label, float value, params GUILayoutOption[] options)
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
        public new static float FieldDelayed(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static float FieldDelayed(Texture label, float value, params GUILayoutOption[] options)
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
        public new static float FieldDelayed(Texture label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int FieldDelayed(int value, params GUILayoutOption[] options)
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
        public new static int FieldDelayed(string label, int value, params GUILayoutOption[] options)
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
        public new static int FieldDelayed(int value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static int FieldDelayed(string label, int value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static int FieldDelayed(GUIContent label, int value, params GUILayoutOption[] options)
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
        public new static int FieldDelayed(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static int FieldDelayed(Texture label, int value, params GUILayoutOption[] options)
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
        public new static int FieldDelayed(Texture label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double FieldDelayed(double value, params GUILayoutOption[] options)
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
        public new static double FieldDelayed(string label, double value, params GUILayoutOption[] options)
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
        public new static double FieldDelayed(double value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static double FieldDelayed(string label, double value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static double FieldDelayed(GUIContent label, double value, params GUILayoutOption[] options)
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
        public new static double FieldDelayed(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static double FieldDelayed(Texture label, double value, params GUILayoutOption[] options)
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
        public new static double FieldDelayed(Texture label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string FieldDelayed(string value, params GUILayoutOption[] options)
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
        public new static string FieldDelayed(string label, string value, params GUILayoutOption[] options)
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
        public new static string FieldDelayed(string value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static string FieldDelayed(string label, string value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static string FieldDelayed(GUIContent label, string value, params GUILayoutOption[] options)
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
        public new static string FieldDelayed(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
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
        public new static string FieldDelayed(Texture label, string value, params GUILayoutOption[] options)
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
        public new static string FieldDelayed(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, style, options);
        }

        #endregion

        #region Number

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, style, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(string label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(string label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(GUIContent label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(Texture label, float value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 float 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Field(Texture label, float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(Texture label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 int 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Field(Texture label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(string label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(string label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(GUIContent label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(Texture label, double value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 double 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="double"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="double"/></returns>
        public new static double Field(Texture label, double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(value, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(value, style, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(string label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(string label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(GUIContent label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(GUIContent label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(Texture label, long value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 long 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="long"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="long"/></returns>
        public new static long Field(Texture label, long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, style, options);
        }

        #endregion

        #region AnimationCurve

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(value, options);
        }

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(string label, AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, options);
        }

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="color">颜色 <see cref="Color"/></param>
        /// <param name="ranges">区间值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(value, color, ranges, options);
        }

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="color">颜色 <see cref="Color"/></param>
        /// <param name="ranges">区间值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(string label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, options);
        }

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(GUIContent label, AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, options);
        }

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="color">颜色 <see cref="Color"/></param>
        /// <param name="ranges">区间值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(GUIContent label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, options);
        }

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(Texture label, AnimationCurve value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 AnimationCurve 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="AnimationCurve"/></param>
        /// <param name="color">颜色 <see cref="Color"/></param>
        /// <param name="ranges">区间值 <see cref="Rect"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="AnimationCurve"/></returns>
        public new static AnimationCurve Field(Texture label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, color, ranges, options);
        }

        #endregion

        #region Color

        /// <summary>
        /// 绘制 Color 
        /// </summary>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public new static Color Field(Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(value, options);
        }

        /// <summary>
        /// 绘制 Color 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public new static Color Field(string label, Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Color 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="showEyedropper">颜色 <see cref="bool"/></param>
        /// <param name="showAlpha">区间值 <see cref="bool"/></param>
        /// <param name="hdr">区间值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public new static Color Field(string label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary>
        /// 绘制 Color 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public new static Color Field(GUIContent label, Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(label, value, options);
        }

        /// <summary>
        /// 绘制 Color 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="showEyedropper">颜色 <see cref="bool"/></param>
        /// <param name="showAlpha">区间值 <see cref="bool"/></param>
        /// <param name="hdr">区间值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public new static Color Field(GUIContent label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(label, value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary>
        /// 绘制 Color 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public new static Color Field(Texture label, Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Color 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Color"/></param>
        /// <param name="showEyedropper">颜色 <see cref="bool"/></param>
        /// <param name="showAlpha">区间值 <see cref="bool"/></param>
        /// <param name="hdr">区间值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Color"/></returns>
        public new static Color Field(Texture label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, showEyedropper, showAlpha, hdr, options);
        }

        #endregion

        #region Gradient

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 Gradient 
        /// </summary>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public new static Gradient Field(Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(value, options);
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 Gradient 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public new static Gradient Field(string label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, options);
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 Gradient 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public new static Gradient Field(GUIContent label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(label, value, options);
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 Gradient 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="Gradient"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Gradient"/></returns>
        public new static Gradient Field(Texture label, Gradient value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, options);
        }

#endif

        #endregion

        #region Text

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 string 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Field(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, style, options);
        }

        #endregion

        #region Object

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(T value, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(value, typeof(T), allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type">类型 <see cref="Type"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(value, type, allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type">类型 <see cref="Type"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(string label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, type, allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(string label, T value, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, typeof(T), allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type">类型 <see cref="Type"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(GUIContent label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, type, allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(GUIContent label, T value, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, typeof(T), allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type">类型 <see cref="Type"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(Texture label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(new GUIContent(label), value, type, allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="allowSceneObjects">是否允许选择场景物体 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(Texture label, T value, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(new GUIContent(label), value, typeof(T), allowSceneObjects, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="type">类型 <see cref="Type"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(T value, Type type, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(value, type, true, options);
        }

        /// <summary>
        /// 绘制 Object 
        /// </summary>
        /// <param name="value">值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Field<T>(T value, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(value, typeof(T), true, options);
        }

        #endregion

        #region Layer

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(value, options);
        }

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(string label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, value, options);
        }

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(value, style, options);
        }

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(string label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(GUIContent label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, value, options);
        }

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(Texture label, int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 Layer 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Layer(Texture label, int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, style, options);
        }

        #endregion

        #region Password

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(value, options);
        }

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, value, options);
        }

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(value, style, options);
        }

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, value, options);
        }

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 密码文本框 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Password(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, style, options);
        }

        #endregion

        #region Foldout

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(string label, bool foldout)
        {
            return EditorGUILayout.Foldout(foldout, label);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(string label, bool foldout, GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, label, style);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(string label, bool foldout, bool toggleOnLabelClick)
        {
            return EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(string label, bool foldout, bool toggleOnLabelClick, GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick, style);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, string label, bool foldout)
        {
            foldout = EditorGUILayout.Foldout(foldout, label);
            if (foldout) action?.Invoke();
            return foldout;
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, string label, bool foldout, GUIStyle style)
        {
            foldout = EditorGUILayout.Foldout(foldout, label, style);
            if (foldout) action?.Invoke();
            return foldout;
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, string label, bool foldout, bool toggleOnLabelClick)
        {
            foldout = EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick);
            if (foldout) action?.Invoke();
            return foldout;
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, string label, bool foldout, bool toggleOnLabelClick, GUIStyle style)
        {
            foldout = EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick, style);
            if (foldout) action?.Invoke();
            return foldout;
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(GUIContent label, bool foldout)
        {
            return EditorGUILayout.Foldout(foldout, label);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(GUIContent label, bool foldout, GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, label, style);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(GUIContent label, bool foldout, bool toggleOnLabelClick)
        {
            return EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(GUIContent label, bool foldout, bool toggleOnLabelClick, GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick, style);
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, GUIContent label, bool foldout)
        {
            foldout = EditorGUILayout.Foldout(foldout, label);
            if (foldout) action?.Invoke();
            return foldout;
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, GUIContent label, bool foldout, GUIStyle style)
        {
            foldout = EditorGUILayout.Foldout(foldout, label, style);
            if (foldout) action?.Invoke();
            return foldout;
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, GUIContent label, bool foldout, bool toggleOnLabelClick)
        {
            foldout = EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick);
            if (foldout) action?.Invoke();
            return foldout;
        }

        /// <summary>
        /// 绘制 折叠式箭头 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldout(Action action, GUIContent label, bool foldout, bool toggleOnLabelClick, GUIStyle style)
        {
            foldout = EditorGUILayout.Foldout(foldout, label, toggleOnLabelClick, style);
            if (foldout) action?.Invoke();
            return foldout;
        }

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 折页排版 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <param name="menuIcon">菜单ICON显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldoutHeader(Action action, string label, bool foldout, GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label, style, menuAction, menuIcon);
            if (foldout) action?.Invoke();
            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 开始绘制 折页排版 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <param name="menuIcon">菜单ICON显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool BeginFoldoutHeader(string label, bool foldout, GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            return EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label, style, menuAction, menuIcon);
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 折页排版 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <param name="menuIcon">菜单ICON显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldoutHeader(Action action, GUIContent label, bool foldout, GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label, style, menuAction, menuIcon);
            if (foldout) action?.Invoke();
            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 开始绘制 折页排版 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <param name="menuAction">操作菜单 <see cref="Action&lt;Rect&gt;"/></param>
        /// <param name="menuIcon">菜单ICON显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool BeginFoldoutHeader(GUIContent label, bool foldout, GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            return EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label, style, menuAction, menuIcon);
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 结束绘制 折页排版 
        /// </summary>
        [ExcludeFromDocs]
        public new static void EndFoldoutHeader()
        {
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 折页排版 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldoutHeaderGroup(Action action, string label, bool foldout, GUIStyle style = null)
        {
#if UNITY_2019_1_OR_NEWER
            foldout = EditorGUILayout.ToggleLeft(label, foldout, style ?? "FoldoutHeader", GTOption.WidthExpand(true));
#else
            foldout = EditorGUILayout.ToggleLeft(label, foldout, style ?? "GUIEditor.BreadcrumbLeft", GTOption.WidthExpand(true));
#endif
            EditorGUILayout.Space();
            if (foldout) action?.Invoke();
            return foldout;
        }

#endif

#if UNITY_2018_1_OR_NEWER

        /// <summary>
        /// 绘制 折页排版 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="style">显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        [ExcludeFromDocs]
        public new static bool VFoldoutHeaderGroup(Action action, GUIContent label, bool foldout, GUIStyle style = null)
        {
#if UNITY_2019_1_OR_NEWER
            foldout = EditorGUILayout.ToggleLeft(label, foldout, style ?? "FoldoutHeader", GTOption.WidthExpand(true));
#else
            foldout = EditorGUILayout.ToggleLeft(label, foldout, style ?? "GUIEditor.BreadcrumbLeft", GTOption.WidthExpand(true));
#endif
            EditorGUILayout.Space();
            if (foldout) action?.Invoke();
            return foldout;
        }

#endif

        #endregion

        #region HelpBox

        /// <summary>
        /// 绘制 帮助框 字段 
        /// </summary>
        /// <param name="message">消息 <see cref="string"/></param>
        /// <param name="type">消息类型 <see cref="MessageType"/></param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分 <see cref="bool"/></param>
        public new static void HelpBox(string message, MessageType type = MessageType.None, bool wide = true)
        {
            EditorGUILayout.HelpBox(message, type, wide);
        }

        /// <summary>
        /// 绘制 帮助框 字段 
        /// </summary>
        /// <param name="message">消息 <see cref="GUIContent"/></param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分 <see cref="bool"/></param>
        public new static void HelpBox(GUIContent message, bool wide = true)
        {
            EditorGUILayout.HelpBox(message, wide);
        }

        /// <summary>
        /// 绘制 帮助框 字段 
        /// </summary>
        /// <param name="message">消息 <see cref="Texture"/></param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分 <see cref="bool"/></param>
        public new static void HelpBox(Texture message, bool wide = true)
        {
            EditorGUILayout.HelpBox(new GUIContent(message), wide);
        }

        /// <summary>
        /// 绘制 帮助框 字段 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="message">消息 <see cref="string"/></param>
        /// <param name="type">消息类型 <see cref="MessageType"/></param>
        public new static void HelpBox(Rect rect, string message, MessageType type = MessageType.None)
        {
            EditorGUI.HelpBox(rect, message, type);
        }

        /// <summary>
        /// 绘制 帮助框 字段 
        /// </summary>
        /// <param name="pos">位置信息 <see cref="Vector2"/></param>
        /// <param name="size">大小信息 <see cref="Vector2"/></param>
        /// <param name="message">消息 <see cref="string"/></param>
        /// <param name="type">消息类型 <see cref="MessageType"/></param>
        public new static void HelpBox(Vector2 pos, Vector2 size, string message, MessageType type = MessageType.None)
        {
            EditorGUI.HelpBox(new Rect(pos - size / 2, size), message, type);
        }

        #endregion

        #region Label

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(GUIContent label, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(string label, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(int label, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label.ToString(), options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(int label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label.ToString(), style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(bool label, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label.ToString(), options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(bool label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label.ToString(), style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="label2">向右显示的标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(GUIContent label, GUIContent label2, params GUILayoutOption[] options)
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
        public new static void Label(GUIContent label, GUIContent label2, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, label2, style, options);
        }

        /// <summary>
        /// 绘制 标签文本框 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="label2">向右显示的标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Label(string label, string label2, params GUILayoutOption[] options)
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
        public new static void Label(string label, string label2, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, label2, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        public new static void LabelPrefix(GUIContent label)
        {
            EditorGUILayout.PrefixLabel(label);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public new static void LabelPrefix(GUIContent label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="GUIContent"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public new static void LabelPrefix(GUIContent label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        public new static void LabelPrefix(string label)
        {
            EditorGUILayout.PrefixLabel(label);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public new static void LabelPrefix(string label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public new static void LabelPrefix(string label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
            EditorGUILayout.PrefixLabel(label, followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        public new static void LabelPrefix(int label)
        {
            EditorGUILayout.PrefixLabel(label.ToString());
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public new static void LabelPrefix(int label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
            EditorGUILayout.PrefixLabel(label.ToString(), followingStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public new static void LabelPrefix(int label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
            EditorGUILayout.PrefixLabel(label.ToString(), followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        public new static void LabelPrefix(bool label)
        {
            EditorGUILayout.PrefixLabel(label.ToString());
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="followingStyle">后面的显示风格</param>
        public new static void LabelPrefix(bool label, [UnityEngine.Internal.DefaultValue("\"Button\"")] GUIStyle followingStyle)
        {
            EditorGUILayout.PrefixLabel(label.ToString(), followingStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="bool"/></param>
        /// <param name="followingStyle">后面的显示风格 <see cref="GUIStyle"/></param>
        /// <param name="labelStyle">显示风格 <see cref="GUIStyle"/></param>
        public new static void LabelPrefix(bool label, GUIStyle followingStyle, GUIStyle labelStyle)
        {
            EditorGUILayout.PrefixLabel(label.ToString(), followingStyle, labelStyle);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void LabelSelectable(string label, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(label, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void LabelSelectable(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(label, style, options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void LabelSelectable(int label, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(label.ToString(), options);
        }

        /// <summary>
        /// 绘制 可选择标签 
        /// </summary>
        /// <param name="label">第一个标签 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void LabelSelectable(int label, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.SelectableLabel(label.ToString(), style, options);
        }

        #endregion

        #region List

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<T> showFunc) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action<T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action<T> showFunc) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<T> showFunc) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<T> showFunc) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<int, T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Action<int, T> showFunc) 
        {
            FieldList(label, array, tips, index => showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action<int, T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action<int, T> showFunc) 
        {
            FieldList(label, array, null, index => showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<int, T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Action<int, T> showFunc) 
        {
            return FieldList(label, array, foldout, tips, index => showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<int, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<int, T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action&lt;int, T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action<int, T> showFunc) 
        {
            return FieldList(label, array, foldout, null, index => showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<T, T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<T, T> showFunc) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Func<T, T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Func<T, T> showFunc) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<T, T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<T, T> showFunc) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<T, T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;T, T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<T, T> showFunc) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<int, T, T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Action tips, Func<int, T, T> showFunc) 
        {
            FieldList(label, array, tips, index => array[index] = showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        public static void List<T>(string label, IList<T> array, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Func<int, T, T> showFunc, Func<T> addFunc) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        public static void List<T>(string label, IList<T> array, Func<int, T, T> showFunc) 
        {
            FieldList(label, array, null, index => array[index] = showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<int, T, T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="tips">提示信息回调函数 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Action tips, Func<int, T, T> showFunc) 
        {
            return FieldList(label, array, foldout, tips, index => array[index] = showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle, GUIStyle bgStyle) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, bgStyle);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<int, T, T> showFunc, Func<T> addFunc, GUIStyle labelStyle) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, labelStyle, null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<int, T, T> showFunc, Func<T> addFunc) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(index, array[index]), addFunc, new GUIStyle("CenteredLabel"), null);
        }

        /// <summary>
        /// 绘制 List 列表 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">列表数据 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">是否展开列表 <see cref="bool"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Func&lt;int, T, T&gt;"/></param>
        /// <returns>Ture: 打开列表, False: 关闭列表<see cref="bool"/></returns>
        public static bool List<T>(string label, IList<T> array, bool foldout, Func<int, T, T> showFunc) 
        {
            return FieldList(label, array, foldout, null, index => array[index] = showFunc.Invoke(index, array[index]), () => default, new GUIStyle("CenteredLabel"), null);
        }

        #endregion

        #region Popup

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, string[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, string[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, string[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, string[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, string[] displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, string[] displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, string[] displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, string[] displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<string> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<string> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<string> displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<string> displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<string> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<string> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<string> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<string> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;string&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<string> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, int[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, int[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, int[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, int[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, int[] displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, int[] displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, int[] displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, int[] displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, int[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.Select(T => T.ToString()).ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, int[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, int[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.Select(T => T.ToString()).ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, int[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<int> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<int> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<int> displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<int> displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<int> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<int> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<int> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<int> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<int> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.Select(T => T.ToString()).ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<int> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<int> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.Select(T => T.ToString()).ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(string label, int value, IEnumerable<int> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.Select(T => T.ToString()).ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, GUIContent[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, GUIContent[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, GUIContent[] displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, GUIContent[] displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, GUIContent[] displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions, optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, GUIContent[] displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions, optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, GUIContent[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, GUIContent[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<GUIContent> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, IEnumerable<GUIContent> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<GUIContent> displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式</param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, IEnumerable<GUIContent> displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues, style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<GUIContent> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, IEnumerable<GUIContent> displayedOptions, IEnumerable<int> optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues.ToArray(), options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<GUIContent> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(value, displayedOptions.ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 整数弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="optionValues">排版格式 <see cref="IEnumerable&lt;int&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, IEnumerable<GUIContent> displayedOptions, IEnumerable<int> optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, value, displayedOptions.ToArray(), optionValues.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<GUIContent> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, IEnumerable<GUIContent> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.ToArray(), options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(int value, IEnumerable<GUIContent> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(value, displayedOptions.ToArray(), style, options);
        }

        /// <summary>
        /// 绘制 弹窗 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="displayedOptions">弹窗内容 <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Popup(GUIContent label, int value, IEnumerable<GUIContent> displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, value, displayedOptions.ToArray(), style, options);
        }

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(selected, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(selected, style, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(string label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(string label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, style, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法 <see cref="Func&lt;Enum, bool&gt;"/></param>
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(string label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, checkEnabled, includeObsolete, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

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
        public new static T Popup<T>(string label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(new GUIContent(label), selected, checkEnabled, includeObsolete, style, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(GUIContent label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(GUIContent label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, style, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 绘制 弹窗枚举 字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="selected">枚举值 <see cref="T"/></param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法 <see cref="Func&lt;Enum, bool&gt;"/></param>
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="T"/></returns>
        public new static T Popup<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, options);
        }

#endif

#if UNITY_2018_3_OR_NEWER

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
        public new static T Popup<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, style, options);
        }

#endif

        #endregion

        #region Scope Horizontal

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void VHorizontal(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            using (new GUILayout.HorizontalScope(options))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void VHorizontal(Action action, float width)
        {
            if (action == null) return;
            using (new GUILayout.HorizontalScope(GUILayout.Width(width)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void VHorizontal(Action action, float width, float height)
        {
            if (action == null) return;
            using (new GUILayout.HorizontalScope(GUILayout.Width(width), GUILayout.Width(height)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void VHorizontal(Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (action == null) return;
            using (new GUILayout.HorizontalScope(style, options))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void VHorizontal(Action action, GUIStyle style, float width)
        {
            if (action == null) return;
            using (new GUILayout.HorizontalScope(style, GUILayout.Width(width)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void VHorizontal(Action action, GUIStyle style, float width, float height)
        {
            if (action == null) return;
            using (new GUILayout.HorizontalScope(style, GUILayout.Width(width), GUILayout.Width(height)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="EditorGUILayout.HorizontalScope"/></returns>
        public static EditorGUILayout.HorizontalScope VHorizontal(params GUILayoutOption[] options)
        {
            return new EditorGUILayout.HorizontalScope(options);
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.HorizontalScope"/></returns>
        public static EditorGUILayout.HorizontalScope VHorizontal(float width)
        {
            return new EditorGUILayout.HorizontalScope(GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.HorizontalScope"/></returns>
        public static EditorGUILayout.HorizontalScope VHorizontal(float width, float height)
        {
            return new EditorGUILayout.HorizontalScope(GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="EditorGUILayout.HorizontalScope"/></returns>
        public static EditorGUILayout.HorizontalScope VHorizontal(GUIStyle style, params GUILayoutOption[] options)
        {
            return new EditorGUILayout.HorizontalScope(style, options);
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.HorizontalScope"/></returns>
        public static EditorGUILayout.HorizontalScope VHorizontal(GUIStyle style, float width)
        {
            return new EditorGUILayout.HorizontalScope(style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.HorizontalScope"/></returns>
        public static EditorGUILayout.HorizontalScope VHorizontal(GUIStyle style, float width, float height)
        {
            return new EditorGUILayout.HorizontalScope(style, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginHorizontal(float width)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(width));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginHorizontal(float width, float height)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginHorizontal(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(options);
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(style, options);
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginHorizontal(GUIStyle style, float width)
        {
            EditorGUILayout.BeginHorizontal(style, GUILayout.Width(width));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginHorizontal(GUIStyle style, float width, float height)
        {
            EditorGUILayout.BeginHorizontal(style, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 结束绘制 空间视图 
        /// </summary>
        public static void EndHorizontal()
        {
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region Scope Vertical

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Vertical(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            using (new GUILayout.VerticalScope(options))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Vertical(Action action, float width)
        {
            if (action == null) return;
            using (new GUILayout.VerticalScope(GUILayout.Width(width)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Vertical(Action action, float width, float height)
        {
            if (action == null) return;
            using (new GUILayout.VerticalScope(GUILayout.Width(width), GUILayout.Width(height)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Vertical(Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (action == null) return;
            using (new GUILayout.VerticalScope(style, options))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Vertical(Action action, GUIStyle style, float width)
        {
            if (action == null) return;
            using (new GUILayout.VerticalScope(style, GUILayout.Width(width)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Vertical(Action action, GUIStyle style, float width, float height)
        {
            if (action == null) return;
            using (new GUILayout.VerticalScope(style, GUILayout.Width(width), GUILayout.Width(height)))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="EditorGUILayout.VerticalScope"/></returns>
        public static EditorGUILayout.VerticalScope Vertical(params GUILayoutOption[] options)
        {
            return new EditorGUILayout.VerticalScope(options);
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.VerticalScope"/></returns>
        public static EditorGUILayout.VerticalScope Vertical(float width)
        {
            return new EditorGUILayout.VerticalScope(GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.VerticalScope"/></returns>
        public static EditorGUILayout.VerticalScope Vertical(float width, float height)
        {
            return new EditorGUILayout.VerticalScope(GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="EditorGUILayout.VerticalScope"/></returns>
        public static EditorGUILayout.VerticalScope Vertical(GUIStyle style, params GUILayoutOption[] options)
        {
            return new EditorGUILayout.VerticalScope(style, options);
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.VerticalScope"/></returns>
        public static EditorGUILayout.VerticalScope Vertical(GUIStyle style, float width)
        {
            return new EditorGUILayout.VerticalScope(style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="EditorGUILayout.VerticalScope"/></returns>
        public static EditorGUILayout.VerticalScope Vertical(GUIStyle style, float width, float height)
        {
            return new EditorGUILayout.VerticalScope(style, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginVertical(float width)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(width));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginVertical(float width, float height)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginVertical(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(options);
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(style, options);
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginVertical(GUIStyle style, float width)
        {
            EditorGUILayout.BeginVertical(style, GUILayout.Width(width));
        }

        /// <summary>
        /// 开始绘制 空间视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginVertical(GUIStyle style, float width, float height)
        {
            EditorGUILayout.BeginVertical(style, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 结束绘制 空间视图 
        /// </summary>
        public static void EndVertical()
        {
            EditorGUILayout.EndVertical();
        }

        #endregion

        #region Scope ScrollView

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScrollView(Action action, Vector2 v2, params GUILayoutOption[] options)
        {
            v2 = EditorGUILayout.BeginScrollView(v2, options);
            action?.Invoke();
            EditorGUILayout.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScrollView(Action action, Vector2 v2, float width)
        {
            v2 = EditorGUILayout.BeginScrollView(v2, GUILayout.Width(width));
            action?.Invoke();
            EditorGUILayout.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScrollView(Action action, Vector2 v2, float width, float height)
        {
            v2 = EditorGUILayout.BeginScrollView(v2, GUILayout.Width(width), GUILayout.Width(height));
            action?.Invoke();
            EditorGUILayout.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScrollView(Action action, Vector2 v2, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
        {
            v2 = EditorGUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, options);
            action?.Invoke();
            EditorGUILayout.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScrollView(Action action, Vector2 v2, GUIStyle styles_h, GUIStyle styles_v, params GUILayoutOption[] options)
        {
            v2 = EditorGUILayout.BeginScrollView(v2, styles_h, styles_v, options);
            action?.Invoke();
            EditorGUILayout.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_b">底板风格 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScrollView(Action action, Vector2 v2, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle styles_h, GUIStyle styles_v, GUIStyle styles_b, params GUILayoutOption[] options)
        {
            v2 = EditorGUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v, styles_b, options);
            action?.Invoke();
            EditorGUILayout.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScrollView(Vector2 v2, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BeginScrollView(v2, options);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScrollView(Vector2 v2, float width)
        {
            return EditorGUILayout.BeginScrollView(v2, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScrollView(Vector2 v2, float width, float height)
        {
            return EditorGUILayout.BeginScrollView(v2, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScrollView(Vector2 v2, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, options);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScrollView(Vector2 v2, GUIStyle styles_h, GUIStyle styles_v, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BeginScrollView(v2, styles_h, styles_v, options);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_b">底板风格 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScrollView(Vector2 v2, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle styles_h, GUIStyle styles_v, GUIStyle styles_b, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v, styles_b, options);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        public static void EndScrollView()
        {
            EditorGUILayout.EndScrollView();
        }

        #endregion

        #region Scope Group

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="toggle">显示开关 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool AGroup(Action action, string label, bool toggle)
        {
            toggle = EditorGUILayout.BeginToggleGroup(label, toggle);
            if (toggle) action?.Invoke();
            EditorGUILayout.EndToggleGroup();
            return toggle;
        }

        /// <summary>
        /// 开始绘制 组视图 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="toggle">显示开关 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool BeginGroup(string label, bool toggle)
        {
            return EditorGUILayout.BeginToggleGroup(label, toggle);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="toggle">显示开关 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool AGroup(Action action, GUIContent label, bool toggle)
        {
            toggle = EditorGUILayout.BeginToggleGroup(label, toggle);
            if (toggle) action?.Invoke();
            EditorGUILayout.EndToggleGroup();
            return toggle;
        }

        /// <summary>
        /// 开始绘制 组视图 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="toggle">显示开关 <see cref="bool"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool BeginGroup(GUIContent label, bool toggle)
        {
            return EditorGUILayout.BeginToggleGroup(label, toggle);
        }

        /// <summary>
        /// 结束绘制 组视图 
        /// </summary>
        public static void EndGroup()
        {
            EditorGUILayout.EndToggleGroup();
        }

        /// <summary>
        /// 绘制 禁用组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="toggle">显示开关 <see cref="bool"/></param>
        public static void AGroupDisabled(Action action, bool toggle)
        {
            EditorGUI.BeginDisabledGroup(toggle);
            action?.Invoke();
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// 开始绘制 禁用组视图 
        /// </summary>
        /// <param name="toggle">显示开关 <see cref="bool"/></param>
        public static void BeginGroupDisabled(bool toggle)
        {
            EditorGUI.BeginDisabledGroup(toggle);
        }

        /// <summary>
        /// 结束绘制 禁用组视图 
        /// </summary>
        public static void EndGroupDisabled()
        {
            EditorGUI.EndDisabledGroup();
        }

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 开始构建目标分组 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action&lt;BuildTargetGroup&gt;"/></param>
        public static void AGroupBuildTargetSelection(Action<BuildTargetGroup> action)
        {
            var value = EditorGUILayout.BeginBuildTargetSelectionGrouping();
            action?.Invoke(value);
            EditorGUILayout.EndBuildTargetSelectionGrouping();
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 绘制 开始构建目标分组 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action&lt;BuildTargetGroup&gt;"/></param>
        /// <param name="value"> <see cref="BuildTargetGroup"/></param>
        /// <returns><see cref="BuildTargetGroup"/></returns>
        public static BuildTargetGroup AGroupBuildTargetSelection(Action<BuildTargetGroup> action, BuildTargetGroup value)
        {
            value = EditorGUILayout.BeginBuildTargetSelectionGrouping();
            action?.Invoke(value);
            EditorGUILayout.EndBuildTargetSelectionGrouping();
            return value;
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 开始绘制 目标分组视图 
        /// </summary>
        /// <returns><see cref="BuildTargetGroup"/></returns>
        public static BuildTargetGroup BeginGroupBuildTargetSelection()
        {
            return EditorGUILayout.BeginBuildTargetSelectionGrouping();
        }

#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 结束绘制 目标分组视图 
        /// </summary>
        public static void EndGroupBuildTargetSelection()
        {
            EditorGUILayout.EndBuildTargetSelectionGrouping();
        }

#endif

        /// <summary>
        /// 绘制 隐藏显示分组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="alpha">介于0到1之间的值，0是隐藏的，1是完全可见的 <see cref="float"/></param>
        public static void AGroupFade(Action action, float alpha)
        {
            if (action == null) return;
            if (EditorGUILayout.BeginFadeGroup(alpha)) action?.Invoke();
            EditorGUILayout.EndFadeGroup();
        }

        /// <summary>
        /// 绘制 隐藏显示分组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action&lt;bool&gt;"/></param>
        /// <param name="show"> <see cref="bool"/></param>
        /// <param name="alpha">介于0到1之间的值，0是隐藏的，1是完全可见的 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool AGroupFade(Action<bool> action, bool show, float alpha)
        {
            show = EditorGUILayout.BeginFadeGroup(alpha);
            action?.Invoke(show);
            EditorGUILayout.EndFadeGroup();
            return show;
        }

        /// <summary>
        /// 开始绘制 隐藏显示分组视图 
        /// </summary>
        /// <param name="alpha">介于0到1之间的值，0是隐藏的，1是完全可见的 <see cref="float"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool BeginGroupFade(float alpha)
        {
            return EditorGUILayout.BeginFadeGroup(alpha);
        }

        /// <summary>
        /// 结束绘制 隐藏显示分组视图 
        /// </summary>
        public static void EndGroupFade()
        {
            EditorGUILayout.EndFadeGroup();
        }

        #endregion

        #region Slider

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="leftValue">左侧值 <see cref="int"/></param>
        /// <param name="rightValue">右侧值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Slider(int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="leftValue">左侧值 <see cref="int"/></param>
        /// <param name="rightValue">右侧值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Slider(string label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(label, value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="leftValue">左侧值 <see cref="int"/></param>
        /// <param name="rightValue">右侧值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Slider(GUIContent label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(label, value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="int"/></param>
        /// <param name="leftValue">左侧值 <see cref="int"/></param>
        /// <param name="rightValue">右侧值 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public new static int Slider(Texture label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntSlider(new GUIContent(label), value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="leftValue">左侧值 <see cref="float"/></param>
        /// <param name="rightValue">右侧值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Slider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="leftValue">左侧值 <see cref="float"/></param>
        /// <param name="rightValue">右侧值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Slider(string label, float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(label, value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="leftValue">左侧值 <see cref="float"/></param>
        /// <param name="rightValue">右侧值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Slider(GUIContent label, float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(label, value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="float"/></param>
        /// <param name="leftValue">左侧值 <see cref="float"/></param>
        /// <param name="rightValue">右侧值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="float"/></returns>
        public new static float Slider(Texture label, float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Slider(new GUIContent(label), value, leftValue, rightValue, options);
        }

        /// <summary>
        /// 绘制 限制滑动条 
        /// </summary>
        /// <param name="minValue">滑动条最左边的值 <see cref="float"/></param>
        /// <param name="maxValue">滑动条最右边的值 <see cref="float"/></param>
        /// <param name="minLimit">限制滑动条最左边的值 <see cref="float"/></param>
        /// <param name="maxLimit">限制滑动条最右边的值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Slider(ref float minValue, ref float maxValue, float minLimit, float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        /// <summary>
        /// 绘制 限制滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="minValue">滑动条最左边的值 <see cref="float"/></param>
        /// <param name="maxValue">滑动条最右边的值 <see cref="float"/></param>
        /// <param name="minLimit">限制滑动条最左边的值 <see cref="float"/></param>
        /// <param name="maxLimit">限制滑动条最右边的值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Slider(string label, ref float minValue, ref float maxValue, float minLimit, float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        /// <summary>
        /// 绘制 限制滑动条 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="minValue">滑动条最左边的值 <see cref="float"/></param>
        /// <param name="maxValue">滑动条最右边的值 <see cref="float"/></param>
        /// <param name="minLimit">限制滑动条最左边的值 <see cref="float"/></param>
        /// <param name="maxLimit">限制滑动条最右边的值 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public new static void Slider(GUIContent label, ref float minValue, ref float maxValue, float minLimit, float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        #endregion

        #region Tag

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(value, options);
        }

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(string label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, value, options);
        }

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(value, style, options);
        }

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(string label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(GUIContent label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, value, options);
        }

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(label, value, style, options);
        }

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(Texture label, string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 标签字段 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string Tag(Texture label, string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TagField(new GUIContent(label), value, style, options);
        }

        #endregion

        #region TextArea

        /// <summary>
        /// 绘制 文本域 
        /// </summary>
        /// <param name="value">文本内容 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string AreaText(string value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextArea(value, options);
        }

        /// <summary>
        /// 绘制 文本域 
        /// </summary>
        /// <param name="value">文本内容 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public new static string AreaText(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextArea(value, style, options);
        }

        #endregion

        #region Toggle

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(label, value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(Texture label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Toggle(Texture label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(Texture label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 左侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool Field(Texture label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Toggle(new GUIContent(label), value, style, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool ToggleLeft(string label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool ToggleLeft(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, style, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool ToggleLeft(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool ToggleLeft(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(label, value, style, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool ToggleLeft(Texture label, bool value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(new GUIContent(label), value, options);
        }

        /// <summary>
        /// 绘制 右侧按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public new static bool ToggleLeft(Texture label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ToggleLeft(new GUIContent(label), value, style, options);
        }

        #endregion

        #region Selection Grid

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button.</param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, content, xCount, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button.</param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, GUIContent[] content, int xCount, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, content, xCount, style, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button. <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, IEnumerable<GUIContent> content, int xCount, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button. <see cref="IEnumerable&lt;GUIContent&gt;"/></param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, IEnumerable<GUIContent> content, int xCount, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, style, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button.</param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, string[] content, int xCount, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, content, xCount, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button.</param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, string[] content, int xCount, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, content, xCount, style, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button. <see cref="ICollection&lt;string&gt;"/></param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, ICollection<string> content, int xCount, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button. <see cref="ICollection&lt;string&gt;"/></param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, ICollection<string> content, int xCount, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, style, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button.</param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, Texture[] content, int xCount, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button.</param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, Texture[] content, int xCount, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, style, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button. <see cref="ICollection&lt;Texture&gt;"/></param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, ICollection<Texture> content, int xCount, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, options);
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="content">An array of text, image and tooltips for the button. <see cref="ICollection&lt;Texture&gt;"/></param>
        /// <param name="xCount">How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements. <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="int"/></returns>
        public static int Selection(int selected, ICollection<Texture> content, int xCount, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.SelectionGrid(selected, GTContent.Temp(content), xCount, style, options);
        }

        #endregion

    }
}
