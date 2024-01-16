/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-10-09
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="contents">An array of text, image and tooltips for the button.</param>
        /// <param name="cb">回调</param>
        /// <param name="width">单体宽度</param>
        /// <returns><see cref="T"/>选中目标</returns>
        public static T Selection<T>(T selected, IDictionary<string, T> contents, Action<T> cb, float width = 100)
        {
            using (new GUILayout.HorizontalScope("PR Insertion", GUILayout.Width(contents.Count * width)))
            {
                foreach (var content in contents)
                {
                    if (GUILayout.Button(content.Key, selected.Equals(content.Value)
                                ? "PreLabel"
#if UNITY_2020_1_OR_NEWER
                                : "StatusBarIcon"
#else
                                : "IN EditColliderButton"
#endif
                            , GUILayout.Width(width)))
                    {
                        selected = content.Value;
                        cb(selected);
                        return selected;
                    }
                }
            }

            return selected;
        }

        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="contents">An array of text, image and tooltips for the button.</param>
        /// <param name="cb">回调</param>
        /// <param name="width">单体宽度</param>
        /// <returns><see cref="T"/>选中目标</returns>
        public static T Selection<T>(T selected, IDictionary<GUIContent, T> contents, Action<T> cb, float width = 100)
        {
            using (new GUILayout.HorizontalScope("PR Insertion", GUILayout.Width(contents.Count * width)))
            {
                foreach (var content in contents)
                {
                    if (GUILayout.Button(content.Key, selected.Equals(content.Value)
                                ? "PreLabel"
#if UNITY_2020_1_OR_NEWER
                                : "StatusBarIcon"
#else
                                : "IN EditColliderButton"
#endif
                            , GUILayout.Width(width)))
                    {
                        selected = content.Value;
                        cb(selected);
                        return selected;
                    }
                }
            }

            return selected;
        }


        /// <summary>
        /// 绘制 空间视图 
        /// </summary>
        /// <returns><see cref="EditorGUILayout.HorizontalScope"/></returns>
        public static EditorGUILayout.ScrollViewScope VScrollView(Vector2 v2)
        {
            return new EditorGUILayout.ScrollViewScope(v2);
        }

        /// <summary>
        /// 绘制 折叠视图 带帮助按钮
        /// </summary>
        /// <param name="content">标题</param>
        /// <param name="isActive">激活状态</param>
        /// <param name="helpAction">帮助回调</param>
        /// <param name="helpContent">帮助信息</param>
        /// <returns>激活状态</returns>
        public static bool VFoldoutWithHelp(string content, bool isActive, Action helpAction = null,
            GUIContent helpContent = null)
        {
            return VFoldoutWithHelp(new GUIContent(content), isActive, helpAction, helpContent);
        }

        /// <summary>
        /// 绘制 折叠视图 带帮助按钮
        /// </summary>
        /// <param name="content">标题</param>
        /// <param name="isActive">激活状态</param>
        /// <param name="helpAction">帮助回调</param>
        /// <param name="helpContent">帮助信息</param>
        /// <returns>激活状态</returns>
        public static bool VFoldoutWithHelp(GUIContent content, bool isActive, Action helpAction = null,
            GUIContent helpContent = null)
        {
            var controlRect = EditorGUILayout.GetControlRect();
            var iconStyle = GUI.skin.FindStyle("IconButton") ??
                            EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("IconButton");
            if (helpAction != null)
            {
                var helpRect = controlRect;
                helpRect.x = controlRect.x + controlRect.width - helpRect.height;
                helpRect.width = helpRect.height;
                if (GUI.Button(helpRect, helpContent ?? EditorGUIUtility.IconContent("_Help"), iconStyle))
                    helpAction.Invoke();
            }

            var isPressedDown = controlRect.Contains(Event.current.mousePosition)
                                && Event.current.type == EventType.MouseDown
                                && Event.current.button == 0;
            if (isPressedDown)
            {
                isActive = !isActive;
                Event.current.Use();
                GUI.changed = true;
            }

            EditorGUI.Foldout(controlRect, isActive, content, false);
            return isActive;
        }

        /// <summary>
        /// 绘制 折叠视图 带帮助按钮
        /// </summary>
        /// <param name="content">标题</param>
        /// <param name="isActive">激活状态</param>
        /// <param name="helpAction">帮助回调</param>
        /// <param name="indent">缩进</param>
        /// <param name="menuAction">菜单操作</param>
        /// <returns>激活状态</returns>
        public static bool VFoldoutHeaderGroupWithHelp(
            string content,
            bool isActive,
            Action helpAction = null,
            int indent = 0,
            Action<Rect> menuAction = null,
            GUIContent helpContent = null
        )
        {
            return VFoldoutHeaderGroupWithHelp(new GUIContent(content), isActive, helpAction, indent, menuAction,
                helpContent);
        }

        /// <summary>
        /// 绘制 折叠视图 带帮助按钮
        /// </summary>
        /// <param name="content">标题</param>
        /// <param name="isActive">激活状态</param>
        /// <param name="helpAction">帮助回调</param>
        /// <param name="indent">缩进</param>
        /// <param name="menuAction">菜单操作</param>
        /// <param name="helpContent">帮助信息</param>
        /// <returns>激活状态</returns>
        public static bool VFoldoutHeaderGroupWithHelp(
            GUIContent content,
            bool isActive,
            Action helpAction = null,
            int indent = 0,
            Action<Rect> menuAction = null,
            GUIContent helpContent = null
        )
        {
            var headerRect = EditorGUILayout.GetControlRect();

            var bgRect = new Rect(headerRect)
            {
                x = 0,
                width = EditorGUIUtility.currentViewWidth
            };
            var isHover = bgRect.Contains(Event.current.mousePosition);
            EditorGUI.DrawRect(bgRect, isHover ? HeaderHoverColor : HeaderNormalColor);

            bgRect.y = headerRect.y - 1;
            bgRect.height = 1;
            var color = HeaderBorderColor;
            EditorGUI.DrawRect(bgRect, color);
            bgRect.y = headerRect.y + headerRect.height + 1;
            bgRect.height = 0.5f;
            EditorGUI.DrawRect(bgRect, color);
            headerRect.y += 1;

            if (indent > 0)
            {
                headerRect.x += indent;
                headerRect.width -= indent;
            }

            var iconStyle = GUI.skin.FindStyle("IconButton") ??
                            EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("IconButton");
            if (menuAction != null)
            {
                var menuButtonRect = headerRect;
                menuButtonRect.x = headerRect.x + headerRect.width - menuButtonRect.height;
                menuButtonRect.width = menuButtonRect.height;
                if (GUI.Button(menuButtonRect, EditorGUIUtility.IconContent("_Popup"), iconStyle))
                    menuAction.Invoke(menuButtonRect);
            }

            if (helpAction != null)
            {
                var helpRect = headerRect;
                helpRect.x = headerRect.x + headerRect.width - helpRect.height;
                if (menuAction != null)
                    helpRect.x -= helpRect.height;
                helpRect.width = helpRect.height;
                if (GUI.Button(helpRect, helpContent ?? EditorGUIUtility.IconContent("_Help"), iconStyle))
                    helpAction.Invoke();
            }

            var isPressedDown = isHover && Event.current.type == EventType.MouseDown && Event.current.button == 0;
            if (isPressedDown)
            {
                isActive = !isActive;
                Event.current.Use();
                GUI.changed = true;
            }

            EditorGUI.Foldout(headerRect, isActive, content, false);
            if (isActive) GUILayout.Space(6f);
            return isActive;
        }


        /// <summary>
        /// 绘制 折叠视图 带帮助按钮
        /// </summary>
        /// <param name="action">显示函数</param>
        /// <param name="content">标题</param>
        /// <param name="isActive">激活状态</param>
        /// <param name="helpAction">帮助回调</param>
        /// <param name="indent">缩进</param>
        /// <param name="menuAction">菜单操作</param>
        /// <param name="helpContent">帮助信息</param>
        /// <returns>激活状态</returns>
        public static bool VFoldoutHeaderGroupWithHelp(Action action,
            string content,
            bool isActive,
            Action helpAction = null,
            int indent = 0,
            Action<Rect> menuAction = null,
            GUIContent helpContent = null
        )
        {
            return VFoldoutHeaderGroupWithHelp(action, new GUIContent(content), isActive, helpAction, indent,
                menuAction, helpContent);
        }

        /// <summary>
        /// 绘制 折叠视图 带帮助按钮
        /// </summary>
        /// <param name="action">显示函数</param>
        /// <param name="content">标题</param>
        /// <param name="isActive">激活状态</param>
        /// <param name="helpAction">帮助回调</param>
        /// <param name="indent">缩进</param>
        /// <param name="menuAction">菜单操作</param>
        /// <param name="helpContent">帮助信息</param>
        /// <returns>激活状态</returns>
        public static bool VFoldoutHeaderGroupWithHelp(Action action,
            GUIContent content,
            bool isActive,
            Action helpAction = null,
            int indent = 0,
            Action<Rect> menuAction = null,
            GUIContent helpContent = null
        )
        {
            var headerRect = EditorGUILayout.GetControlRect();

            var bgRect = new Rect(headerRect)
            {
                x = 0,
                width = EditorGUIUtility.currentViewWidth
            };
            var isHover = bgRect.Contains(Event.current.mousePosition);
            EditorGUI.DrawRect(bgRect, isHover ? HeaderHoverColor : HeaderNormalColor);

            bgRect.y = headerRect.y - 1;
            bgRect.height = 1;
            var color = HeaderBorderColor;
            EditorGUI.DrawRect(bgRect, color);
            bgRect.y = headerRect.y + headerRect.height + 1;
            bgRect.height = 0.5f;
            EditorGUI.DrawRect(bgRect, color);
            headerRect.y += 1;

            if (indent > 0)
            {
                headerRect.x += indent;
                headerRect.width -= indent;
            }

            var iconStyle = GUI.skin.FindStyle("IconButton") ??
                            EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("IconButton");
            if (menuAction != null)
            {
                var menuButtonRect = headerRect;
                menuButtonRect.x = headerRect.x + headerRect.width - menuButtonRect.height;
                menuButtonRect.width = menuButtonRect.height;
                if (GUI.Button(menuButtonRect, EditorGUIUtility.IconContent("_Popup"), iconStyle))
                    menuAction.Invoke(menuButtonRect);
            }

            if (helpAction != null)
            {
                var helpRect = headerRect;
                helpRect.x = headerRect.x + headerRect.width - helpRect.height;
                if (menuAction != null)
                    helpRect.x -= helpRect.height;
                helpRect.width = helpRect.height;
                if (GUI.Button(helpRect, helpContent ?? EditorGUIUtility.IconContent("_Help"), iconStyle))
                    helpAction.Invoke();
            }

            var isPressedDown = isHover && Event.current.type == EventType.MouseDown && Event.current.button == 0;
            if (isPressedDown)
            {
                isActive = !isActive;
                Event.current.Use();
                GUI.changed = true;
            }

            EditorGUI.Foldout(headerRect, isActive, content, false);
            if (isActive)
            {
                GUILayout.Space(6f);
                action.Invoke();
            }

            return isActive;
        }
    }
}