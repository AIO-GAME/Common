





/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate
|||✩ Document: ||| ->
|||✩ - - - - - |*/

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIO
{
    /// <summary>
    /// Layout
    /// </summary>
    public partial class GULayout
    {

        #region Toggle

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(string label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, label, style, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(string label, bool value, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, label, GUI.skin.toggle, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(GUIContent label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, label, style, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(GUIContent label, bool value, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, label, GUI.skin.toggle, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(Texture label, bool value, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, label, style, options);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="value">值 <see cref="bool"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Toggle(Texture label, bool value, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, label, GUI.skin.toggle, options);
        }

        #endregion

        #region Area Text

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(string text, params GUILayoutOption[] options)
        {
            return GUILayout.TextArea(text, options);
        }

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.TextArea(text, style, options);
        }

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(string text, int maxLength, params GUILayoutOption[] options)
        {
            return GUILayout.TextArea(text, maxLength, options);
        }

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.TextArea(text, maxLength, style, options);
        }

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(Rect rect, string text)
        {
            return GUI.TextArea(rect, text);
        }

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(Rect rect, string text, GUIStyle style)
        {
            return GUI.TextArea(rect, text, style);
        }

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(Rect rect, string text, int maxLength)
        {
            return GUI.TextArea(rect, text, maxLength);
        }

        /// <summary>
        /// 绘制 文本视图 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="string"/></returns>
        public static string AreaText(Rect rect, string text, int maxLength, GUIStyle style)
        {
            return GUI.TextArea(rect, text, maxLength, style);
        }

        #endregion

        #region Password

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string password, char mask, params GUILayoutOption[] options)
        {
            return GUILayout.PasswordField(password, mask, options);
        }

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string password, char mask, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.PasswordField(password, mask, style, options);
        }

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string password, char mask, int maxLength, params GUILayoutOption[] options)
        {
            return GUILayout.PasswordField(password, mask, maxLength, options);
        }

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(string password, char mask, int maxLength, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.PasswordField(password, mask, maxLength, style, options);
        }

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(Rect rect, string password, char mask)
        {
            return GUI.PasswordField(rect, password, mask);
        }

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(Rect rect, string password, char mask, GUIStyle style)
        {
            return GUI.PasswordField(rect, password, mask, style);
        }

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(Rect rect, string password, char mask, int maxLength)
        {
            return GUI.PasswordField(rect, password, mask, maxLength);
        }

        /// <summary>
        /// 绘制 密码框 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="password">文本内容 <see cref="string"/></param>
        /// <param name="mask">屏蔽密码的字符 <see cref="char"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Password(Rect rect, string password, char mask, int maxLength, GUIStyle style)
        {
            return GUI.PasswordField(rect, password, mask, maxLength, style);
        }

        #endregion

        #region Label

        /// <summary>
        /// 绘制 标签名 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(string label, params GUILayoutOption[] options)
        {
            GUILayout.Label(label, options);
        }

        /// <summary>
        /// 绘制 标签名 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.Label(label, style, options);
        }

        /// <summary>
        /// 绘制 标签名 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(GUIContent label, params GUILayoutOption[] options)
        {
            GUILayout.Label(label, options);
        }

        /// <summary>
        /// 绘制 标签名 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.Label(label, style, options);
        }

        /// <summary>
        /// 绘制 标签名 
        /// </summary>
        /// <param name="label">标签 <see cref="float"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(float label, params GUILayoutOption[] options)
        {
            GUILayout.Label(new GUIContent(label.ToString()), options);
        }

        /// <summary>
        /// 绘制 标签名 
        /// </summary>
        /// <param name="label">标签 <see cref="float"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Label(float label, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.Label(new GUIContent(label.ToString()), style, options);
        }

        #endregion

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
            return GUILayout.Button(label, GUILayout.Width(width), GUILayout.Width(height));
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
            return GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Width(height));
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
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Width(height))) action();
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
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Width(height))) action();
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
            return GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Width(height));
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
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Width(height))) action();
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
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Width(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(Rect rect, GUIContent label)
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
        public static bool Button(Rect rect, GUIContent label, GUIStyle style)
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
        public static bool Button(Vector2 pos, Vector2 size, GUIContent label)
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
        public static bool Button(Vector2 pos, Vector2 size, GUIContent label, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label, style);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public static void Button(Rect rect, GUIContent label, Action action)
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
        public static void Button(Rect rect, GUIContent label, Action action, GUIStyle style)
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
        public static void Button(Vector2 pos, Vector2 size, GUIContent label, Action action)
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
        public static void Button(Vector2 pos, Vector2 size, GUIContent label, Action action, GUIStyle style)
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
        public static bool Button(Rect rect, string label)
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
        public static bool Button(Rect rect, string label, GUIStyle style)
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
        public static bool Button(Vector2 pos, Vector2 size, string label)
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
        public static bool Button(Vector2 pos, Vector2 size, string label, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label, style);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public static void Button(Rect rect, string label, Action action)
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
        public static void Button(Rect rect, string label, Action action, GUIStyle style)
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
        public static void Button(Vector2 pos, Vector2 size, string label, Action action)
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
        public static void Button(Vector2 pos, Vector2 size, string label, Action action, GUIStyle style)
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
        public static bool Button(Rect rect, Texture label)
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
        public static bool Button(Rect rect, Texture label, GUIStyle style)
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
        public static bool Button(Vector2 pos, Vector2 size, Texture label)
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
        public static bool Button(Vector2 pos, Vector2 size, Texture label, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), label, style);
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="rect">绘制区域 <see cref="Rect"/></param>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        public static void Button(Rect rect, Texture label, Action action)
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
        public static void Button(Rect rect, Texture label, Action action, GUIStyle style)
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
        public static void Button(Vector2 pos, Vector2 size, Texture label, Action action)
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
        public static void Button(Vector2 pos, Vector2 size, Texture label, Action action, GUIStyle style)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), label, style)) action();
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
            return GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Width(height));
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
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Width(height));
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
            return GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Width(height));
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
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Width(height));
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
            if (GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Width(height))) action();
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
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Width(height))) action();
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
            if (GUILayout.RepeatButton(label, GUILayout.Width(width), GUILayout.Width(height))) action();
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
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width), GUILayout.Width(height))) action();
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
            if (GUILayout.RepeatButton(new GUIContent(label), GUILayout.Width(width), GUILayout.Width(height))) action();
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
            if (GUILayout.RepeatButton(new GUIContent(label), style, GUILayout.Width(width), GUILayout.Width(height))) action();
        }

        #endregion

        #region Field Text

        /// <summary>
        /// 绘制 文本框 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string text, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(text, options);
        }

        /// <summary>
        /// 绘制 文本框 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(text, style, options);
        }

        /// <summary>
        /// 绘制 文本框 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string text, int maxLength, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(text, maxLength, options);
        }

        /// <summary>
        /// 绘制 文本框 
        /// </summary>
        /// <param name="text">文本内容 <see cref="string"/></param>
        /// <param name="maxLength">输入字符串最大长度 <see cref="int"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        /// <returns><see cref="string"/></returns>
        public static string Field(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(text, maxLength, style, options);
        }

        #endregion

        #region Scope Horizontal

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void VHorizontal(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            GUILayout.BeginHorizontal(options);
            action();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void VHorizontal(Action action, float width)
        {
            if (action == null) return;
            GUILayout.BeginHorizontal(GUILayout.Width(width));
            action();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void VHorizontal(Action action, float width, float height)
        {
            if (action == null) return;
            GUILayout.BeginHorizontal(GUILayout.Width(width), GUILayout.Width(height));
            action();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void VHorizontal(Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (action == null) return;
            GUILayout.BeginHorizontal(style, options);
            action();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void VHorizontal(Action action, GUIStyle style, float width)
        {
            if (action == null) return;
            GUILayout.BeginHorizontal(style, GUILayout.Width(width));
            action();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void VHorizontal(Action action, GUIStyle style, float width, float height)
        {
            if (action == null) return;
            GUILayout.BeginHorizontal(style, GUILayout.Width(width), GUILayout.Width(height));
            action();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginHorizontal(float width)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginHorizontal(float width, float height)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginHorizontal(params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginHorizontal(GUIStyle style, float width)
        {
            GUILayout.BeginHorizontal(style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginHorizontal(GUIStyle style, float width, float height)
        {
            GUILayout.BeginHorizontal(style, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 横排视图 
        /// </summary>
        public static void EndHorizontal()
        {
            GUILayout.EndHorizontal();
        }

        #endregion

        #region Scope Vertical

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Vertical(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            GUILayout.BeginVertical(options);
            action();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Vertical(Action action, float width)
        {
            if (action == null) return;
            GUILayout.BeginVertical(GUILayout.Width(width));
            action();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Vertical(Action action, float width, float height)
        {
            if (action == null) return;
            GUILayout.BeginVertical(GUILayout.Width(width), GUILayout.Width(height));
            action();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Vertical(Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (action == null) return;
            GUILayout.BeginVertical(style, options);
            action();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Vertical(Action action, GUIStyle style, float width)
        {
            if (action == null) return;
            GUILayout.BeginVertical(style, GUILayout.Width(width));
            action();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Vertical(Action action, GUIStyle style, float width, float height)
        {
            if (action == null) return;
            GUILayout.BeginVertical(style, GUILayout.Width(width), GUILayout.Width(height));
            action();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginVertical(float width)
        {
            GUILayout.BeginVertical(GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginVertical(float width, float height)
        {
            GUILayout.BeginVertical(GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginVertical(params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void BeginVertical(GUIStyle style, float width)
        {
            GUILayout.BeginVertical(style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void BeginVertical(GUIStyle style, float width, float height)
        {
            GUILayout.BeginVertical(style, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 竖排视图 
        /// </summary>
        public static void EndVertical()
        {
            GUILayout.EndVertical();
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
            if (action == null) return v2;
            v2 = GUILayout.BeginScrollView(v2, options);
            action();
            GUILayout.EndScrollView();
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
            if (action == null) return v2;
            v2 = GUILayout.BeginScrollView(v2, GUILayout.Width(width));
            action();
            GUILayout.EndScrollView();
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
            if (action == null) return v2;
            v2 = GUILayout.BeginScrollView(v2, GUILayout.Width(width), GUILayout.Width(height));
            action();
            GUILayout.EndScrollView();
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
            if (action == null) return v2;
            v2 = GUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, options);
            action();
            GUILayout.EndScrollView();
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
            if (action == null) return v2;
            v2 = GUILayout.BeginScrollView(v2, styles_h, styles_v, options);
            action();
            GUILayout.EndScrollView();
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
            if (action == null) return v2;
            v2 = GUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v, styles_b, options);
            action();
            GUILayout.EndScrollView();
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
            return GUILayout.BeginScrollView(v2, options);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScrollView(Vector2 v2, float width)
        {
            return GUILayout.BeginScrollView(v2, GUILayout.Width(width));
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
            return GUILayout.BeginScrollView(v2, GUILayout.Width(width), GUILayout.Width(height));
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
            return GUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, options);
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
            return GUILayout.BeginScrollView(v2, styles_h, styles_v, options);
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
            return GUILayout.BeginScrollView(v2, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v, styles_b, options);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        public static void EndScrollView()
        {
            GUILayout.EndScrollView();
        }

        #endregion

        #region Scope Scroll

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScroll(Action action, Rect rect, Vector2 v2, Rect viewRect)
        {
            if (action == null) return v2;
            v2 = GUI.BeginScrollView(rect, v2, viewRect);
            action();
            GUI.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScroll(Action action, Rect rect, Vector2 v2, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
        {
            if (action == null) return v2;
            v2 = GUI.BeginScrollView(rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical);
            action();
            GUI.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScroll(Action action, Rect rect, Vector2 v2, Rect viewRect, GUIStyle styles_h, GUIStyle styles_v)
        {
            if (action == null) return v2;
            v2 = GUI.BeginScrollView(rect, v2, viewRect, styles_h, styles_v);
            action();
            GUI.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 VScroll(Action action, Rect rect, Vector2 v2, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle styles_h, GUIStyle styles_v)
        {
            if (action == null) return v2;
            v2 = GUI.BeginScrollView(rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v);
            action();
            GUI.EndScrollView();
            return v2;
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScroll(Rect rect, Vector2 v2, Rect viewRect)
        {
            return GUI.BeginScrollView(rect, v2, viewRect);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScroll(Rect rect, Vector2 v2, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
        {
            return GUI.BeginScrollView(rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScroll(Rect rect, Vector2 v2, Rect viewRect, GUIStyle styles_h, GUIStyle styles_v)
        {
            return GUI.BeginScrollView(rect, v2, viewRect, styles_h, styles_v);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        /// <param name="rect">屏幕上用于滚动视图的矩形 <see cref="Rect"/></param>
        /// <param name="v2">视图在X和Y方向上滚动的像素距离 <see cref="Vector2"/></param>
        /// <param name="viewRect">在滚动视图内部使用的矩形 <see cref="Rect"/></param>
        /// <param name="alwaysShowHorizontal">始终显示水平滚动条 <see cref="bool"/></param>
        /// <param name="alwaysShowVertical">始终显示垂直滚动条 <see cref="bool"/></param>
        /// <param name="styles_h">水平滚动条风格 <see cref="GUIStyle"/></param>
        /// <param name="styles_v">垂直滚动条风格 <see cref="GUIStyle"/></param>
        /// <returns><see cref="Vector2"/></returns>
        public static Vector2 BeginScroll(Rect rect, Vector2 v2, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle styles_h, GUIStyle styles_v)
        {
            return GUI.BeginScrollView(rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v);
        }

        /// <summary>
        /// 绘制 滚动视图 
        /// </summary>
        public static void EndScroll()
        {
            GUI.EndScrollView();
        }

        #endregion

        #region Scope Area

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        public static void VArea(Action action, Rect rect)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        public static void VArea(Action action, Rect rect, string label)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect, label);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VArea(Action action, Rect rect, string label, GUIStyle style)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect, label, style);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VArea(Action action, Rect rect, GUIStyle style)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect, style);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        public static void BeginArea(Rect rect)
        {
            GUILayout.BeginArea(rect);
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        public static void BeginArea(Rect rect, string label)
        {
            GUILayout.BeginArea(rect, label);
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginArea(Rect rect, string label, GUIStyle style)
        {
            GUILayout.BeginArea(rect, label, style);
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginArea(Rect rect, GUIStyle style)
        {
            GUILayout.BeginArea(rect, style);
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        public static void EndArea()
        {
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        public static void VArea(Action action, Rect rect, GUIContent label)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect, label);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VArea(Action action, Rect rect, GUIContent label, GUIStyle style)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect, label, style);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        public static void BeginArea(Rect rect, GUIContent label)
        {
            GUILayout.BeginArea(rect, label);
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginArea(Rect rect, GUIContent label, GUIStyle style)
        {
            GUILayout.BeginArea(rect, label, style);
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        public static void VArea(Action action, Rect rect, Texture label)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect, label);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VArea(Action action, Rect rect, Texture label, GUIStyle style)
        {
            if (action == null) return;
            GUILayout.BeginArea(rect, label, style);
            action();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        public static void BeginArea(Rect rect, Texture label)
        {
            GUILayout.BeginArea(rect, label);
        }

        /// <summary>
        /// 绘制 区域视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginArea(Rect rect, Texture label, GUIStyle style)
        {
            GUILayout.BeginArea(rect, label, style);
        }

        #endregion

        #region Scope Clip

        /// <summary>
        /// 绘制 裁剪视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        public static void VClip(Action action, Rect rect)
        {
            if (action == null) return;
            GUI.BeginClip(rect);
            action();
            GUI.EndClip();
        }

        /// <summary>
        /// 绘制 裁剪视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="scrollOffset">滚动区域补偿 <see cref="Vector2"/></param>
        /// <param name="renderOffset">显示区域补偿 <see cref="Vector2"/></param>
        /// <param name="resetOffset">重置补偿 <see cref="bool"/></param>
        public static void VClip(Action action, Rect rect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
        {
            if (action == null) return;
            GUI.BeginClip(rect, scrollOffset, renderOffset, resetOffset);
            action();
            GUI.EndClip();
        }

        /// <summary>
        /// 绘制 裁剪视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="rectOffset">区域补偿 <see cref="Rect"/></param>
        /// <param name="resetOffset">重置补偿 <see cref="bool"/></param>
        public static void VClip(Action action, Rect rect, Rect rectOffset, bool resetOffset)
        {
            if (action == null) return;
            GUI.BeginClip(rect, rectOffset.position, rectOffset.size, resetOffset);
            action();
            GUI.EndClip();
        }

        /// <summary>
        /// 开始绘制 裁剪视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        public static void BeginClip(Rect rect)
        {
            GUI.BeginClip(rect);
        }

        /// <summary>
        /// 开始绘制 裁剪视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="scrollOffset">滚动区域补偿 <see cref="Vector2"/></param>
        /// <param name="renderOffset">显示区域补偿 <see cref="Vector2"/></param>
        /// <param name="resetOffset">重置补偿 <see cref="bool"/></param>
        public static void BeginClip(Rect rect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
        {
            GUI.BeginClip(rect, scrollOffset, renderOffset, resetOffset);
        }

        /// <summary>
        /// 开始绘制 裁剪视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="rectOffset">区域补偿 <see cref="Rect"/></param>
        /// <param name="resetOffset">重置补偿 <see cref="bool"/></param>
        public static void BeginClip(Rect rect, Rect rectOffset, bool resetOffset)
        {
            GUI.BeginClip(rect, rectOffset.position, rectOffset.size, resetOffset);
        }

        /// <summary>
        /// 结束绘制 裁剪视图 
        /// </summary>
        public static void EndClip()
        {
            GUI.EndClip();
        }

        #endregion

        #region Scope Group

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        public static void VGroup(Action action, Rect rect)
        {
            if (action == null) return;
            GUI.BeginGroup(rect);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        public static void VGroup(Action action, Rect rect, string label)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, label);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VGroup(Action action, Rect rect, string label, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, label, style);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VGroup(Action action, Rect rect, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, style);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        public static void BeginGroup(Rect rect)
        {
            GUI.BeginGroup(rect);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        public static void BeginGroup(Rect rect, string label)
        {
            GUI.BeginGroup(rect, label);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginGroup(Rect rect, string label, GUIStyle style)
        {
            GUI.BeginGroup(rect, label, style);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginGroup(Rect rect, GUIStyle style)
        {
            GUI.BeginGroup(rect, style);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        public static void EndGroup()
        {
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        public static void VGroup(Action action, Rect rect, GUIContent label)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, label);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VGroup(Action action, Rect rect, GUIContent label, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, label, style);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        public static void BeginGroup(Rect rect, GUIContent label)
        {
            GUI.BeginGroup(rect, label);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginGroup(Rect rect, GUIContent label, GUIStyle style)
        {
            GUI.BeginGroup(rect, label, style);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        public static void VGroup(Action action, Rect rect, Texture label)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, label);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="action">回调函数 <see cref="Action"/></param>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void VGroup(Action action, Rect rect, Texture label, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, label, style);
            action();
            GUI.EndGroup();
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        public static void BeginGroup(Rect rect, Texture label)
        {
            GUI.BeginGroup(rect, label);
        }

        /// <summary>
        /// 绘制 组视图 
        /// </summary>
        /// <param name="rect">矩形 <see cref="Rect"/></param>
        /// <param name="label">标题 <see cref="Texture"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        public static void BeginGroup(Rect rect, Texture label, GUIStyle style)
        {
            GUI.BeginGroup(rect, label, style);
        }

        #endregion

    }
}
