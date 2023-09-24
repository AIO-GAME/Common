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
using UnityEngine;
using UDefaultValue = UnityEngine.Internal.DefaultValueAttribute;

namespace AIO
{
    public partial class GULayout
    {
        #region 绘制横排内容

        /// <summary> 绘制横排内容 </summary>
        public static void Horizontal(Action action, GUIStyle styles, params GUILayoutOption[] options)
        {
            if (action == null) return;
            if (styles == null) GUILayout.BeginHorizontal(options);
            else GUILayout.BeginHorizontal(styles, options);
            action();
            GUILayout.EndHorizontal();
        }

        /// <summary> 绘制横排内容 </summary>
        public static void Horizontal(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            GUILayout.BeginHorizontal(options);
            action();
            GUILayout.EndHorizontal();
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
            GUILayout.BeginVertical(styles, options);
            action();
            GUILayout.EndVertical();
        }

        /// <summary> 绘制横排内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="options">排版格式</param>
        public static void Vertical(Action action, params GUILayoutOption[] options)
        {
            if (action == null) return;
            GUILayout.BeginVertical(options);
            action();
            GUILayout.EndVertical();
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
            v = GUILayout.BeginScrollView(v, styles_h, styles_v, options);
            action();
            GUILayout.EndScrollView();
            return v;
        }

        /// <summary> 绘制滚动视图 </summary>
        /// <param name="action">方法体</param>
        /// <param name="v">二维坐标</param>
        /// <param name="options">整体显示格式</param>
        public static Vector2 ScrollView(Action action, Vector2 v, params GUILayoutOption[] options)
        {
            if (action == null) return v;
            v = GUILayout.BeginScrollView(v, options);
            action();
            GUILayout.EndScrollView();
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
            v = GUILayout.BeginScrollView(v, show_h, show_v, options);
            action();
            GUILayout.EndScrollView();
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
            v = GUILayout.BeginScrollView(v, show_h, show_v, styles_h, styles_v, styles_b, options);
            action();
            GUILayout.EndScrollView();
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
            v = GUILayout.BeginScrollView(v, styles, options);
            action(v);
            GUILayout.EndScrollView();
            return v;
        }

        #endregion
    }
}