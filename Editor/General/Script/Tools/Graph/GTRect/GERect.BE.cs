/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-03                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 13:30:29                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using System;
using UnityEngine;

namespace UnityEditor
{
    public static partial class GERect
    {
        #region 检查

        /// <summary>
        /// 改变检查
        /// </summary>
        public static void ChangeCheck(Action action)
        {
            if (action == null) return;
            EditorGUI.BeginChangeCheck();
            action();
            EditorGUI.EndChangeCheck();
        }

        /// <summary>
        /// 改变检查 开始
        /// </summary>
        public static void ChangeCheckBegin()
        {
            EditorGUI.BeginChangeCheck();
        }

        /// <summary>
        /// 改变检查 结束
        /// </summary>
        public static void ChangeCheckEnd()
        {
            EditorGUI.EndChangeCheck();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 属性
        /// </summary>
        public static void Property(Action action, Rect rect, GUIContent label, SerializedProperty property)
        {
            if (action == null) return;
            EditorGUI.BeginProperty(rect, label, property);
            action();
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// 属性
        /// </summary>
        public static void Property(Action action, Rect rect, string label, SerializedProperty property)
        {
            if (action == null) return;
            EditorGUI.BeginProperty(rect, new GUIContent(label), property);
            action();
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// 属性
        /// </summary>
        public static void PropertyBegin(Rect rect, string label, SerializedProperty property)
        {
            EditorGUI.BeginProperty(rect, new GUIContent(label), property);
        }

        /// <summary>
        /// 属性
        /// </summary>
        public static void PropertyEnd()
        {
            EditorGUI.EndProperty();
        }

        #endregion

        #region 禁用

        /// <summary>
        /// 禁用
        /// </summary>
        public static void DisabledGroup(Action action, bool disable)
        {
            if (action == null) return;
            EditorGUI.BeginDisabledGroup(disable);
            action();
            EditorGUI.EndChangeCheck();
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public static void DisabledGroupBegin(bool disable)
        {
            EditorGUI.BeginDisabledGroup(disable);
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public static void DisabledGroupEnd()
        {
            EditorGUI.EndChangeCheck();
        }

        #endregion

#if UNITY_2020_1_OR_NEWER

        #region 折叠

        /// <summary>
        /// 折叠
        /// </summary>
        public static bool FoldoutHeaderGroup(Action action, Rect rect, bool flodout, string content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            if (action == null) return false;
            if (style == null) style = EditorStyles.foldoutHeader;
#if UNITY_2021_1_OR_NEWER
                if (menuIcon == null) menuIcon = EditorStyles.iconButton;
#endif
            var b = EditorGUI.BeginFoldoutHeaderGroup(rect, flodout, content, style, menuAction, menuIcon);
            action();
            EditorGUI.EndFoldoutHeaderGroup();
            return b;
        }

        /// <summary>
        /// 折叠
        /// </summary>
        public static bool FoldoutHeaderGroup(Action action, Rect rect, bool flodout, GUIContent content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            if (action == null) return false;
            if (style == null) style = EditorStyles.foldoutHeader;
#if UNITY_2021_1_OR_NEWER
                if (menuIcon == null) menuIcon = EditorStyles.iconButton;
#endif
            var b = EditorGUI.BeginFoldoutHeaderGroup(rect, flodout, content, style, menuAction, menuIcon);
            action();
            EditorGUI.EndFoldoutHeaderGroup();
            return b;
        }

        /// <summary>
        /// 折叠
        /// </summary>
        public static bool FoldoutHeaderGroupBegin(Rect rect, bool flodout, GUIContent content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            if (style == null) style = EditorStyles.foldoutHeader;
#if UNITY_2021_1_OR_NEWER
                if (menuIcon == null) menuIcon = EditorStyles.iconButton;
#endif
            return EditorGUI.BeginFoldoutHeaderGroup(rect, flodout, content, style, menuAction, menuIcon);
        }

        /// <summary>
        /// 折叠
        /// </summary>
        public static bool FoldoutHeaderGroupBegin(Rect rect, bool flodout, string content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            if (style == null) style = EditorStyles.foldoutHeader;
#if UNITY_2021_1_OR_NEWER
                if (menuIcon == null) menuIcon = EditorStyles.iconButton;
#endif
            return EditorGUI.BeginFoldoutHeaderGroup(rect, flodout, content, style, menuAction, menuIcon);
        }

        /// <summary>
        /// 折叠
        /// </summary>
        public static void FoldoutHeaderGroupEnd()
        {
            EditorGUI.EndFoldoutHeaderGroup();
        }

        #endregion

#endif
    }
}