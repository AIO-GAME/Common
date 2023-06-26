/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-06                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 12:51:55                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using System;
using UnityEngine;

namespace UnityEditor
{
    public static class GraphicRectExtend
    {
#if UNITY_2020_3_OR_NEWER
        /// <summary>
        /// 折叠开始
        /// </summary>
        public static bool FoldoutHeaderGroupBegin(this GraphicRect rect, Vector2 Size, bool flodout, string content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            return GT.BE.FoldoutHeaderGroupBegin(new Rect(rect.Position, Size), flodout, content, style, menuAction, menuIcon);
        }

        /// <summary>
        /// 折叠结束
        /// </summary>
        public static void FoldoutHeaderGroupEnd(this GraphicRect rect)
        {
            GT.BE.FoldoutHeaderGroupEnd();
        }

        /// <summary>
        /// 折叠
        /// </summary>
        public static bool FoldoutHeaderGroup(this GraphicRect rect, Action action, Vector2 Size, bool flodout, string content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            var ON = GT.BE.FoldoutHeaderGroupBegin(new Rect(rect.Position, Size), flodout, content, style, menuAction, menuIcon);
            if (ON) action?.Invoke();
            GT.BE.FoldoutHeaderGroupEnd();
            return ON;
        }
#endif
    }
}