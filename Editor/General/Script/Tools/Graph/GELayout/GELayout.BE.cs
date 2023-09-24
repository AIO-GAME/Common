/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-03                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 13:19:23                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UDefaultValue = UnityEngine.Internal.DefaultValueAttribute;

namespace AIO.UEditor
{
    public  partial class GELayout
    {
        /* ----------------EditorGUILayout---------------- */

        #region 创建一组可以禁用的控件

        /// <summary> 创建一组可以禁用的控件 </summary>
        /// <param name="action">方法体</param>
        /// <param name="disabled">排版格式</param>
        public static void DisabledGroup(Action action, bool disabled)
        {
            if (action == null) return;
            EditorGUI.BeginDisabledGroup(disabled);
            action();
            EditorGUI.EndDisabledGroup();
        }

        #endregion

        #region 绘制横排内容

        /// <summary> 绘制横排内容 </summary>
        public static void Horizontal(Action action, GUIStyle styles, params GUILayoutOption[] options)
        {
            if (action == null) return;
            if (styles == null) EditorGUILayout.BeginHorizontal(options);
            else EditorGUILayout.BeginHorizontal(styles, options);
            action();
            EditorGUILayout.EndHorizontal();
        }

        /// <summary> 绘制横排内容 </summary>
        public static void Horizontal(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            EditorGUILayout.BeginHorizontal(options);
            action();
            EditorGUILayout.EndHorizontal();
        }

        /// <summary> 绘制横排内容 </summary>
        public static void HorizontalBegin(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(options);
        }

        /// <summary> 绘制横排内容 </summary>
        public static void HorizontalEnd()
        {
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region 绘制竖排内容

        /// <summary> 绘制横排内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="styles">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Vertical(Action action, GUIStyle styles, params GUILayoutOption[] options)
        {
            if (action == null) return;
            EditorGUILayout.BeginVertical(styles, options);
            action();
            EditorGUILayout.EndVertical();
        }

        /// <summary> 绘制横排内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="options">排版格式</param>
        public static void Vertical(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            EditorGUILayout.BeginVertical(options);
            action();
            EditorGUILayout.EndVertical();
        }

        #endregion

        #region 绘制滚动视图

        /// <summary> 绘制滚动视图 </summary>
        /// <param name="action">方法体</param>
        /// <param name="v">二维坐标</param>
        /// <param name="styles_h">水平滚动条风格</param>
        /// <param name="styles_v">垂直滚动条风格</param>
        /// <param name="options">整体显示格式</param>
        public static Vector2 ScrollView(Action action, Vector2 v, GUIStyle styles_h, GUIStyle styles_v,
            params GUILayoutOption[] options)
        {
            if (action == null) return v;
            v = EditorGUILayout.BeginScrollView(v, styles_h, styles_v, options);
            action();
            EditorGUILayout.EndScrollView();
            return v;
        }

        /// <summary> 绘制滚动视图 </summary>
        /// <param name="action">方法体</param>
        /// <param name="v">二维坐标</param>
        /// <param name="options">整体显示格式</param>
        public static Vector2 ScrollView(Action action, Vector2 v, params GUILayoutOption[] options)
        {
            if (action == null) return v;
            v = EditorGUILayout.BeginScrollView(v, options);
            action();
            EditorGUILayout.EndScrollView();
            return v;
        }

        /// <summary> 绘制滚动视图 </summary>
        /// <param name="action">方法体</param>
        /// <param name="v">二维坐标</param>
        /// <param name="show_h">始终显示水平滚动条</param>
        /// <param name="show_v">始终显示垂直滚动条</param>
        /// <param name="options">整体显示格式</param>
        public static Vector2 ScrollView(Action action, Vector2 v, bool show_h, bool show_v,
            params GUILayoutOption[] options)
        {
            if (action == null) return v;
            v = EditorGUILayout.BeginScrollView(v, show_h, show_v, options);
            action();
            EditorGUILayout.EndScrollView();
            return v;
        }

        /// <summary> 绘制滚动视图 </summary>
        /// <param name="action">方法体</param>
        /// <param name="v">二维坐标</param>
        /// <param name="show_h">始终显示水平滚动条</param>
        /// <param name="show_v">始终显示垂直滚动条</param>
        /// <param name="styles_h">水平滚动条风格</param>
        /// <param name="styles_v">垂直滚动条风格</param>
        /// <param name="styles_b">底板风格</param>
        /// <param name="options">整体显示格式</param>
        public static Vector2 ScrollView(Action action, Vector2 v, bool show_h, bool show_v, GUIStyle styles_h,
            GUIStyle styles_v, GUIStyle styles_b,
            params GUILayoutOption[] options)
        {
            if (action == null) return v;
            v = EditorGUILayout.BeginScrollView(v, show_h, show_v, styles_h, styles_v, styles_b, options);
            action();
            EditorGUILayout.EndScrollView();
            return v;
        }

        /// <summary> 绘制滚动视图 </summary>
        /// <param name="action">方法体</param>
        /// <param name="v">二维坐标</param>
        /// <param name="styles">显示风格</param>
        /// <param name="options">整体显示格式</param>
        public static Vector2 ScrollView(Action<Vector2> action, Vector2 v, GUIStyle styles,
            params GUILayoutOption[] options)
        {
            if (action == null) return v;
            v = EditorGUILayout.BeginScrollView(v, styles, options);
            action(v);
            EditorGUILayout.EndScrollView();
            return v;
        }

        #endregion

        #region 绘制切换组件

        /// <summary> 绘制切换组件 如果为true 显示方法体中内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="label">标签</param>
        /// <param name="toggle">显示开关</param>
        public static bool ToggleGroup(Action action, bool toggle, GUIContent label)
        {
            if (action == null) return false;
            toggle = EditorGUILayout.BeginToggleGroup(label, toggle);
            if (toggle) action();
            EditorGUILayout.EndToggleGroup();
            return toggle;
        }

        /// <summary> 绘制切换组件 如果为true 显示方法体中内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="label">标签</param>
        /// <param name="toggle">显示开关</param>
        public static bool ToggleGroup(Action action, bool toggle, string label)
        {
            if (action == null) return false;
            toggle = EditorGUILayout.BeginToggleGroup(label, toggle);
            if (toggle) action();
            EditorGUILayout.EndToggleGroup();
            return toggle;
        }

        #endregion

        #region 隐藏显示区域

        /// <summary> 隐藏显示区域 </summary>
        /// <param name="action">方法体</param>
        /// <param name="value">0:hide,1:show</param>
        public static bool FadeGroup(Action action, float value)
        {
            if (action == null) return false;
            var off = EditorGUILayout.BeginFadeGroup(value);
            if (off)
            {
                Space();
                action();
                Space();
            }

            EditorGUILayout.EndFadeGroup();
            return off;
        }

        #endregion

        #region 获取分组信息

#if UNITY_2019_1_OR_NEWER
        /// <summary> 开始构建目标分组,并获取所选的BuildTargetGroup </summary>
        public static BuildTargetGroup BuildTargetSelectionGrouping()
        {
            var value = EditorGUILayout.BeginBuildTargetSelectionGrouping();
            EditorGUILayout.EndBuildTargetSelectionGrouping();
            return value;
        }
#endif

        #endregion
    }
}