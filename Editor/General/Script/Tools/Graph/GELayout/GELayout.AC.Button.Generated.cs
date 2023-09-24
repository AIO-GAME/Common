/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate
|||✩ Document: ||| ->
|||✩ - - - - - |*/
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout // Button : Button
    {
        #region string

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        public static void Button(string name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, options)) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void Button(string name, Action action, float width, float height)
        {
            if (GUILayout.Button(name, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        public static void Button(string name, Action action, float width)
        {
            if (GUILayout.Button(name, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        /// <param name="style">样式</param>
        public static void Button(string name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, style, options)) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        public static void Button(string name, Action action, GUIStyle style, float width)
        {
            if (GUILayout.Button(name, style, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        public static void Button(string name, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.Button(name, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        #endregion

        #region GUIContent

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(GUIContent name, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, options);
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="style">样式</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(GUIContent name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, style, options);
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(GUIContent name, float width)
        {
            return GUILayout.Button(name, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(GUIContent name, float width, float height)
        {
            return GUILayout.Button(name, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(GUIContent name, GUIStyle style, float width)
        {
            return GUILayout.Button(name, style, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(GUIContent name, GUIStyle style, float width, float height)
        {
            return GUILayout.Button(name, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        public static void Button(GUIContent name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, options)) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void Button(GUIContent name, Action action, float width, float height)
        {
            if (GUILayout.Button(name, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        public static void Button(GUIContent name, Action action, float width)
        {
            if (GUILayout.Button(name, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        /// <param name="style">样式</param>
        public static void Button(GUIContent name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, style, options)) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        public static void Button(GUIContent name, Action action, GUIStyle style, float width)
        {
            if (GUILayout.Button(name, style, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        public static void Button(GUIContent name, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.Button(name, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        #endregion

        #region Texture

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(Texture name, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, options);
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="style">样式</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(Texture name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, style, options);
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(Texture name, float width)
        {
            return GUILayout.Button(name, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(Texture name, float width, float height)
        {
            return GUILayout.Button(name, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(Texture name, GUIStyle style, float width)
        {
            return GUILayout.Button(name, style, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool Button(Texture name, GUIStyle style, float width, float height)
        {
            return GUILayout.Button(name, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        public static void Button(Texture name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, options)) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void Button(Texture name, Action action, float width, float height)
        {
            if (GUILayout.Button(name, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        public static void Button(Texture name, Action action, float width)
        {
            if (GUILayout.Button(name, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        /// <param name="style">样式</param>
        public static void Button(Texture name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, style, options)) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        public static void Button(Texture name, Action action, GUIStyle style, float width)
        {
            if (GUILayout.Button(name, style, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 Button 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        public static void Button(Texture name, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.Button(name, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        #endregion

    }
    public partial class GELayout // ButtonRepeat : RepeatButton
    {

        #region string

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(string name, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, options);
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="style">样式</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(string name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, style, options);
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(string name, float width)
        {
            return GUILayout.RepeatButton(name, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(string name, float width, float height)
        {
            return GUILayout.RepeatButton(name, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(string name, GUIStyle style, float width)
        {
            return GUILayout.RepeatButton(name, style, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(string name, GUIStyle style, float width, float height)
        {
            return GUILayout.RepeatButton(name, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        public static void ButtonRepeat(string name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, options)) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void ButtonRepeat(string name, Action action, float width, float height)
        {
            if (GUILayout.RepeatButton(name, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        public static void ButtonRepeat(string name, Action action, float width)
        {
            if (GUILayout.RepeatButton(name, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(string name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, style, options)) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(string name, Action action, GUIStyle style, float width)
        {
            if (GUILayout.RepeatButton(name, style, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(string name, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.RepeatButton(name, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        #endregion

        #region GUIContent

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(GUIContent name, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, options);
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="style">样式</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(GUIContent name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, style, options);
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(GUIContent name, float width)
        {
            return GUILayout.RepeatButton(name, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(GUIContent name, float width, float height)
        {
            return GUILayout.RepeatButton(name, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(GUIContent name, GUIStyle style, float width)
        {
            return GUILayout.RepeatButton(name, style, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(GUIContent name, GUIStyle style, float width, float height)
        {
            return GUILayout.RepeatButton(name, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        public static void ButtonRepeat(GUIContent name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, options)) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void ButtonRepeat(GUIContent name, Action action, float width, float height)
        {
            if (GUILayout.RepeatButton(name, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        public static void ButtonRepeat(GUIContent name, Action action, float width)
        {
            if (GUILayout.RepeatButton(name, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(GUIContent name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, style, options)) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(GUIContent name, Action action, GUIStyle style, float width)
        {
            if (GUILayout.RepeatButton(name, style, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(GUIContent name, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.RepeatButton(name, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        #endregion

        #region Texture

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(Texture name, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, options);
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="style">样式</param>
        /// <param name="options">参数</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(Texture name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, style, options);
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(Texture name, float width)
        {
            return GUILayout.RepeatButton(name, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(Texture name, float width, float height)
        {
            return GUILayout.RepeatButton(name, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(Texture name, GUIStyle style, float width)
        {
            return GUILayout.RepeatButton(name, style, GUILayout.Width(width));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        /// <returns>Ture:点击 False:未点击</returns>
        public static bool ButtonRepeat(Texture name, GUIStyle style, float width, float height)
        {
            return GUILayout.RepeatButton(name, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        public static void ButtonRepeat(Texture name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, options)) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void ButtonRepeat(Texture name, Action action, float width, float height)
        {
            if (GUILayout.RepeatButton(name, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        public static void ButtonRepeat(Texture name, Action action, float width)
        {
            if (GUILayout.RepeatButton(name, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="options">参数</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(Texture name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, style, options)) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(Texture name, Action action, GUIStyle style, float width)
        {
            if (GUILayout.RepeatButton(name, style, GUILayout.Width(width))) action();
        }

        /// <summary> 
        /// 按钮 ButtonRepeat 
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="action">回调</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="style">样式</param>
        public static void ButtonRepeat(Texture name, Action action, GUIStyle style, float width, float height)
        {
            if (GUILayout.RepeatButton(name, style, GUILayout.Width(width), GUILayout.Height(height))) action();
        }

        #endregion

    }
}
