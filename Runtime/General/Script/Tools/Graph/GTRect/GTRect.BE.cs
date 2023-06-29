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

namespace UnityEngine
{
    public static partial class GURect
    {
        #region Clip

        /// <summary>
        /// 裁剪
        /// </summary>
        public static void VClip(Action action, Rect rect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
        {
            if (action == null) return;
            GUI.BeginClip(rect, scrollOffset, renderOffset, resetOffset);
            action();
            GUI.EndClip();
        }

        /// <summary>
        /// 裁剪
        /// </summary>
        public static void VClip(Action action, Rect rect, Rect offset, bool resetOffset)
        {
            if (action == null) return;
            GUI.BeginClip(rect, offset.position, offset.size, resetOffset);
            action();
            GUI.EndClip();
        }

        /// <summary>
        /// 裁剪
        /// </summary>
        public static void VClip(Action action, Rect rect)
        {
            if (action == null) return;
            GUI.BeginClip(rect);
            action();
            GUI.EndClip();
        }

        #endregion

        #region Group

        /// <summary>
        /// 裁剪
        /// </summary>
        public static void VGroup(Action action, Rect rect)
        {
            if (action == null) return;
            GUI.BeginGroup(rect);
            action();
            GUI.EndClip();
        }

        /// <summary>
        ///   <para>Begin a group. Must be matched with a call to EndGroup.</para>
        /// </summary>
        public static void VGroup(Action action, Rect rect, string content)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, content);
            action();
            GUI.EndClip();
        }


        /// <summary>
        ///   <para>Begin a group. Must be matched with a call to EndGroup.</para>
        /// </summary>
        public static void VGroup(Action action, Rect rect, Texture content)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, content);
            action();
            GUI.EndClip();
        }

        /// <summary>
        ///   <para>Begin a group. Must be matched with a call to EndGroup.</para>
        /// </summary>
        public static void VGroup(Action action, Rect rect, GUIContent content)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, content);
            action();
            GUI.EndClip();
        }

        /// <summary>
        ///   <para>Begin a group. Must be matched with a call to EndGroup.</para>
        /// </summary>
        public static void VGroup(Action action, Rect rect, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, style);
            action();
            GUI.EndClip();
        }

        /// <summary>
        ///   <para>Begin a group. Must be matched with a call to EndGroup.</para>
        /// </summary>
        public static void VGroup(Action action, Rect rect, string content, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, content, style);
            action();
            GUI.EndClip();
        }

        /// <summary>
        ///   <para>Begin a group. Must be matched with a call to EndGroup.</para>
        /// </summary>
        public static void VGroup(Action action, Rect rect, Texture content, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, content, style);
            action();
            GUI.EndClip();
        }

        /// <summary>
        ///   <para>Begin a group. Must be matched with a call to EndGroup.</para>
        /// </summary>
        public static void VGroup(Action action, Rect rect, GUIContent content, GUIStyle style)
        {
            if (action == null) return;
            GUI.BeginGroup(rect, content, style);
            action();
            GUI.EndClip();
        }

        #endregion

        #region Scroll

        /// <summary>
        ///   <para>Begin a scrolling view inside your GUI.</para>
        /// </summary>
        public static Vector2 VScroll(Action action, Rect rect, Vector2 scrollPosition, Rect view)
        {
            if (action == null) return scrollPosition;
            scrollPosition = GUI.BeginScrollView(rect, scrollPosition, view);
            action();
            GUI.EndScrollView();
            return scrollPosition;
        }

        /// <summary>
        ///   <para>Begin a scrolling view inside your GUI.</para>
        /// </summary>
        public static Vector2 VScroll(Action action,
            Rect rect,
            Vector2 scrollPosition,
            Rect viewRect,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical)
        {
            if (action == null) return scrollPosition;
            scrollPosition = GUI.BeginScrollView(rect, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
            action();
            GUI.EndScrollView();
            return scrollPosition;
        }

        /// <summary>
        ///   <para>Begin a scrolling view inside your GUI.</para>
        /// </summary>
        public static Vector2 VScroll(Action action,
            Rect rect,
            Vector2 scrollPosition,
            Rect viewRect,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar)
        {
            if (action == null) return scrollPosition;
            scrollPosition = GUI.BeginScrollView(
                rect,
                scrollPosition,
                viewRect,
                false,
                false,
                horizontalScrollbar,
                verticalScrollbar);
            action();
            GUI.EndScrollView();
            return scrollPosition;
        }

        /// <summary>
        ///   <para>Begin a scrolling view inside your GUI.</para>
        /// </summary>
        public static Vector2 VScroll(Action action, Rect rect, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar)
        {
            if (action == null) return scrollPosition;
            scrollPosition = GUI.BeginScrollView(rect,
                scrollPosition,
                viewRect,
                alwaysShowHorizontal,
                alwaysShowVertical,
                horizontalScrollbar,
                verticalScrollbar);
            action();
            GUI.EndScrollView();
            return scrollPosition;
        }

        #endregion
    }
}