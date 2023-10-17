/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.UEditor
{
    public static class GEHelper
    {
        #region 获取编辑器控件的矩形 GetControlRect

        /*
         * 获取编辑器控件的rect。
         * 当创建一个新的编辑器控件时，它是一个合理的设计来实现实际的控件，
         * 而不依赖于GUlLayout，取而代之的是控件接受一个Rect作为参数，类似于EditorGUl类中的控件。
         * 这确保了控件也可以在PropertyDrawer中使用，而PropertyDrawer不允许使用GUlLayout。
         * 一旦实现了控件的非布局版本，就可以轻松地创建布局版本，只需调用非布局版本即可。
         * 为了得到适合控件的rect，可以使用GetControlRect函数。
         */

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

        #region 工具基类 EditorToolbar

#if UNITY_2018_1_OR_NEWER

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
            EditorGUILayout.EditorToolbar(tools);
        }
#endif

        #endregion

        #region 类似于检查窗口的标题栏 InspectorTitlebar

        // see https://docs.unity3d.com/2020.3/Documentation/ScriptReference/EditorGUILayout.InspectorTitlebar.html

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

        /// <summary>
        /// 按钮
        /// </summary>
        public static float Knob(Vector2 knobSize, float value, float minValue, float maxValue, string unit,
            Color backgroundColor, Color activeColor, bool showValue,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.Knob(knobSize, value, minValue, maxValue, unit, backgroundColor, activeColor,
                showValue, options);
        }

        #region CopyAction

        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static void CopyAction(string contents)
        {
            var textEditor = new TextEditor { text = contents };
            textEditor.OnFocus();
            textEditor.Copy();
        }

        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static void CopyAction(long contents)
        {
            var textEditor = new TextEditor { text = contents.ToString(CultureInfo.CurrentCulture) };
            textEditor.OnFocus();
            textEditor.Copy();
        }

        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static void CopyAction(double contents)
        {
            var textEditor = new TextEditor { text = contents.ToString(CultureInfo.CurrentCulture) };
            textEditor.OnFocus();
            textEditor.Copy();
        }

        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static void CopyAction(Color contents)
        {
            var textEditor = new TextEditor { text = contents.ToConvertHtmlString() };
            textEditor.OnFocus();
            textEditor.Copy();
        }

        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static void CopyAction(Color32 contents)
        {
            var textEditor = new TextEditor { text = contents.ToConvertHtmlString() };
            textEditor.OnFocus();
            textEditor.Copy();
        }

        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static void CopyAction<T>(T contents)
        {
            var textEditor = new TextEditor { text = AHelper.Json.Serialize(contents) };
            textEditor.OnFocus();
            textEditor.Copy();
        }

        #endregion

        #region PasteAction

        /// <summary>
        /// 粘贴信息
        /// </summary>
        public static T PasteAction<T>()
        {
            var textEditor = new TextEditor();
            textEditor.OnFocus();
            textEditor.Paste();
            try
            {
                return AHelper.Json.Deserialize<T>(textEditor.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return default;
            }
        }

        /// <summary>
        /// 粘贴信息
        /// </summary>
        public static Color PasteActionColor()
        {
            var textEditor = new TextEditor();
            textEditor.OnFocus();
            textEditor.Paste();
            try
            {
                return RHelper.Color.HexToColor(textEditor.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return default;
            }
        }

        /// <summary>
        /// 粘贴信息
        /// </summary>
        public static Color32 PasteActionColor32()
        {
            var textEditor = new TextEditor();
            textEditor.OnFocus();
            textEditor.Paste();
            try
            {
                return RHelper.Color.HexToColor32(textEditor.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return default;
            }
        }

        /// <summary>
        /// 粘贴信息
        /// </summary>
        public static string PasteActionString()
        {
            var textEditor = new TextEditor();
            textEditor.OnFocus();
            textEditor.Paste();
            return textEditor.text;
        }

        /// <summary>
        /// 粘贴信息
        /// </summary>
        public static long PasteActionLong()
        {
            var textEditor = new TextEditor();
            textEditor.OnFocus();
            textEditor.Paste();
            try
            {
                return long.Parse(textEditor.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return default;
            }
        }

        /// <summary>
        /// 粘贴信息
        /// </summary>
        public static double PasteActionDouble()
        {
            var textEditor = new TextEditor();
            textEditor.OnFocus();
            textEditor.Paste();
            try
            {
                return double.Parse(textEditor.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return default;
            }
        }

        #endregion
    }
}