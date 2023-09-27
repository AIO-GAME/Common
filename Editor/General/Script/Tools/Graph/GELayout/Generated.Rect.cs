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
            action?.Invoke();
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

    }
}
