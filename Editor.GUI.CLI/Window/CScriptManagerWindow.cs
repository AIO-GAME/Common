/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-09                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 09:52:37                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 脚本管理
    /// </summary>
    [GWindow("脚本管理", Group = "Tools", MinSizeWidth = 600, MinSizeHeight = 600)]
    public class CScriptManagerGraphWindow : GraphicWindow
    {
        private List<Type> List;
        private Vector2 Vector;

        public CScriptManagerGraphWindow()
        {
            List = new List<Type>();
            Vector = new Vector2();
            foreach (var item in
                     from Assembly in AHelper.Assembly.GetReferanceAssemblies(AppDomain.CurrentDomain)
                     from item in Assembly.GetTypes()
                     where item.FullName != null && item.FullName.Contains("Framework")
                     select item)
            {
                List.Add(item);
            }
        }

        protected override void OnActivation()
        {
        }

        protected override void OnGUI()
        {
            using (GELayout.VHorizontal(EditorStyles.helpBox, GTOption.Width(true)))
            {
                GELayout.Space(5);
                GELayout.Label("类名");
                GELayout.Label("命名空间", GTOption.Width(200));
                GELayout.Label("静态类", new GUIStyle("CenteredLabel"), GTOption.Width(100));
                GELayout.Label("属性", GTOption.Width(500));
                GELayout.Space(10);
            }

            using (var scope = GELayout.VScrollView(Vector))
            {
                Vector = scope.scrollPosition;
                foreach (var type in List)
                {
                    using (GELayout.VHorizontal(EditorStyles.helpBox, GTOption.Width(true), GTOption.Height(60)))
                    {
                        GELayout.Space(10);
                        GELayout.Label(type.Name);
                        GELayout.Label(type.Namespace, GTOption.Width(200));
                        GELayout.Label(type.IsAbstract, new GUIStyle("CenteredLabel"), GTOption.Width(100));
                        GELayout.Label(type.Attributes.ToString(), GTOption.Width(500));
                        GELayout.Space(10);
                    }
                }
            }
        }

        /// <summary>
        /// 当可脚本化对象超出作用域时调用此函数。
        /// </summary>
        protected override void OnDisable()
        {
            List.Clear();
            List = null;
        }
    }
}