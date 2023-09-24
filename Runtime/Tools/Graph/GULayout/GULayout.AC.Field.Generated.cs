




/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate
|||✩ Document: ||| ->
|||✩ - - - - - |*/
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    public partial class GULayout 
    {

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
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, int width)
        {
            return GUILayout.Button(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, int width, int height)
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
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, GUIStyle style, int width)
        {
            return GUILayout.Button(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(GUIContent label, GUIStyle style, int width, int height)
        {
            return GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Width(height));
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
            return GUILayout.Button(label, GUILayout.Width(width), GUILayout.Width(height));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, int width)
        {
            return GUILayout.Button(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, int width, int height)
        {
            return GUILayout.Button(label, GUILayout.Width(width), GUILayout.Width(height));
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
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, GUIStyle style, int width)
        {
            return GUILayout.Button(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool Button(string label, GUIStyle style, int width, int height)
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
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void Button(GUIContent label, Action action, int width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void Button(GUIContent label, Action action, int width, int height)
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
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void Button(GUIContent label, Action action, GUIStyle style, int width)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void Button(GUIContent label, Action action, GUIStyle style, int width, int height)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Width(height))) action();
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
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void Button(string label, Action action, int width)
        {
            if (GUILayout.Button(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void Button(string label, Action action, int width, int height)
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
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void Button(string label, Action action, GUIStyle style, int width)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void Button(string label, Action action, GUIStyle style, int width, int height)
        {
            if (GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Width(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Button(Texture label, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(new GUIContent(label), options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Button(Texture label, Action action, float width)
        {
            if (GUILayout.Button(new GUIContent(label), GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Button(Texture label, Action action, float width, float height)
        {
            if (GUILayout.Button(new GUIContent(label), GUILayout.Width(width), GUILayout.Width(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void Button(Texture label, Action action, int width)
        {
            if (GUILayout.Button(new GUIContent(label), GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void Button(Texture label, Action action, int width, int height)
        {
            if (GUILayout.Button(new GUIContent(label), GUILayout.Width(width), GUILayout.Width(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="options">排版格式 <see cref="GUILayoutOption"/></param>
        public static void Button(Texture label, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(new GUIContent(label), style, options)) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        public static void Button(Texture label, Action action, GUIStyle style, float width)
        {
            if (GUILayout.Button(new GUIContent(label), style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="float"/></param>
        /// <param name="height">高度 <see cref="float"/></param>
        public static void Button(Texture label, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.Button(new GUIContent(label), style, GUILayout.Width(width), GUILayout.Width(height))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void Button(Texture label, Action action, GUIStyle style, int width)
        {
            if (GUILayout.Button(new GUIContent(label), style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void Button(Texture label, Action action, GUIStyle style, int width, int height)
        {
            if (GUILayout.Button(new GUIContent(label), style, GUILayout.Width(width), GUILayout.Width(height))) action();
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
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, int width)
        {
            return GUILayout.RepeatButton(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, int width, int height)
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
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, GUIStyle style, int width)
        {
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(GUIContent label, GUIStyle style, int width, int height)
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
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, int width)
        {
            return GUILayout.RepeatButton(label, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, int width, int height)
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
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, GUIStyle style, int width)
        {
            return GUILayout.RepeatButton(label, style, GUILayout.Width(width));
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ButtonRepeat(string label, GUIStyle style, int width, int height)
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
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, int width)
        {
            if (GUILayout.RepeatButton(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, int width, int height)
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
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, GUIStyle style, int width)
        {
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="GUIContent"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void ButtonRepeat(GUIContent label, Action action, GUIStyle style, int width, int height)
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
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void ButtonRepeat(string label, Action action, int width)
        {
            if (GUILayout.RepeatButton(label, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void ButtonRepeat(string label, Action action, int width, int height)
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
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void ButtonRepeat(string label, Action action, GUIStyle style, int width)
        {
            if (GUILayout.RepeatButton(label, style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void ButtonRepeat(string label, Action action, GUIStyle style, int width, int height)
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
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void ButtonRepeat(Texture label, Action action, int width)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void ButtonRepeat(Texture label, Action action, int width, int height)
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

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        public static void ButtonRepeat(Texture label, Action action, GUIStyle style, int width)
        {
            if (GUILayout.RepeatButton(new GUIContent(label), style, GUILayout.Width(width))) action();
        }

        /// <summary>
        /// 绘制 按钮 
        /// </summary>
        /// <param name="label">标签 <see cref="Texture"/></param>
        /// <param name="action">回调 <see cref="Action"/></param>
        /// <param name="style">样式 <see cref="GUIStyle"/></param>
        /// <param name="width">宽度 <see cref="int"/></param>
        /// <param name="height">高度 <see cref="int"/></param>
        public static void ButtonRepeat(Texture label, Action action, GUIStyle style, int width, int height)
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
    }
}
