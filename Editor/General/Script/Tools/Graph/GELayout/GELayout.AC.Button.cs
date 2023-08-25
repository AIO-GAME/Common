/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        /// <summary> 按钮 Button </summary>
        public static bool Button(string name, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, options);
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(string name, int Width, int Height)
        {
            return GUILayout.Button(name, GUILayout.Width(Width), GUILayout.Height(Height));
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(string name, int Width)
        {
            return GUILayout.Button(name, GUILayout.Width(Width));
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(string name, float Width)
        {
            return GUILayout.Button(name, GUILayout.Width(Width));
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(string name, float Width, float Height)
        {
            return GUILayout.Button(name, GUILayout.Width(Width), GUILayout.Height(Height));
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(GUIContent name, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, options);
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(Texture image, params GUILayoutOption[] options)
        {
            return GUILayout.Button(image, options);
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(string name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, style, options);
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(GUIContent name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Button(name, style, options);
        }

        /// <summary> 按钮 Button </summary>
        public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Button(image, style, options);
        }

        /// <summary> 按钮 Button </summary>
        public static void Button(string name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, options)) action();
        }

        /// <summary> 按钮 Button </summary>
        public static void Button(GUIContent name, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, options)) action();
        }

        /// <summary> 按钮 Button </summary>
        public static void Button(Texture image, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(image, options)) action();
        }

        /// <summary> 按钮 Button </summary>
        public static void Button(string name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, style, options)) action();
        }

        /// <summary> 按钮 Button </summary>
        public static void Button(GUIContent name, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(name, style, options)) action();
        }

        /// <summary> 按钮 Button </summary>
        public static void Button(Texture image, Action action, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(image, style, options)) action();
        }

        /// <summary> 按钮 Button </summary>
        public static void Button(string name, Action action, float h, float w)
        {
            if (GUILayout.Button(name, EditorStyles.miniButton, GUILayout.Height(h), GUILayout.Height(w))) action();
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="name"> 名字 </param>
        /// <param name="options"></param>
        public static bool ButtonRepeat(string name, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, options);
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="name"> 名字 </param>
        /// <param name="options"></param>
        public static bool ButtonRepeat(GUIContent name, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, options);
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="image"> 图片 </param>
        /// <param name="options"></param>
        public static bool ButtonRepeat(Texture image, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(image, options);
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="name"> 名字 </param>
        /// <param name="style"></param>
        /// <param name="options"></param>
        public static bool ButtonRepeat(string name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, style, options);
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="name"> 名字 </param>
        /// <param name="style"></param>
        /// <param name="options"></param>
        public static bool ButtonRepeat(GUIContent name, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(name, style, options);
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="image"> 图片 </param>
        /// <param name="style"></param>
        /// <param name="options"></param>
        public static bool ButtonRepeat(Texture image, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.RepeatButton(image, style, options);
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="name"> 名字 </param>
        /// <param name="action"></param>
        /// <param name="options"></param>
        public static void ButtonRepeat(Action action, string name, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, options)) action();
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="action"></param>
        /// <param name="name"> 名字 </param>
        /// <param name="options"></param>
        public static void ButtonRepeat(Action action, GUIContent name, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, options)) action();
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="action"></param>
        /// <param name="name"> 名字 </param>
        /// <param name="style"></param>
        /// <param name="options"></param>
        public static void ButtonRepeat(Action action, string name, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, style, options)) action();
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="action"></param>
        /// <param name="name"> 名字 </param>
        /// <param name="style"></param>
        /// <param name="options"></param>
        public static void ButtonRepeat(Action action, GUIContent name, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(name, style, options)) action();
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="action"></param>
        /// <param name="texture"> 图片 </param>
        /// <param name="options"></param>
        public static void ButtonRepeat(Action action, Texture texture, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(texture, options)) action();
        }

        /// <summary> 重复按钮 ButtonRepeat </summary>
        /// <param name="action"></param>
        /// <param name="texture"> 图片 </param>
        /// <param name="style"></param>
        /// <param name="options"></param>
        public static void ButtonRepeat(Action action, Texture texture, GUIStyle style, params GUILayoutOption[] options)
        {
            if (GUILayout.RepeatButton(texture, style, options)) action();
        }

        /// <summary> 延迟文本 Double </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static bool ButtonDropdown(GUIContent label, FocusType focusType, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(label, focusType, style, options);
        }

        /// <summary> 延迟文本 Double </summary>
        /// <param name="focusType">按钮是否可以通过键盘选择</param>
        /// <param name="label">标签</param>
        /// <param name="options">排版格式</param>
        public static bool ButtonDropdown(GUIContent label, FocusType focusType, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DropdownButton(label, focusType, options);
        }

        /// <summary>
        /// 复制字符按钮
        /// </summary>
        public static void ButtonCopyText(string name, float height, float width, string content, GUIStyle style = null)
        {
            if (Button(name, style, GUILayout.Height(height), GUILayout.Width(width))) GEHelper.CopyTextAction(content);
        }
    }
}
