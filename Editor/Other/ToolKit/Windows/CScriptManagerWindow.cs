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

namespace AIO.Unity.Editor
{
    /// <summary>
    /// 脚本管理
    /// </summary>
    public class CScriptManagerGraphWindow : GraphicWindow
    {
        private List<Type> List;
        private Vector2 Vector;
        private GUILayoutOption Width;
        private GUIStyle Content;

        public CScriptManagerGraphWindow() : base()
        {
            List = new List<Type>();
            Vector = new Vector2();
            foreach (var item in
                     from Assembly in UtilsGen.Assembly.GetReferanceAssemblies(AppDomain.CurrentDomain)
                     from item in Assembly.GetTypes()
                     where item.FullName != null && item.FullName.Contains("Framework")
                     select item)
            {
                List.Add(item);
            }

            Content = "DD HeaderStyle";
            Width = GTOption.Width(120);
        }

        protected override void OnEnable()
        {
        }

        protected override void OnGUI()
        {
            var Height = GTOption.Height(60);
            GTLayout.BE.Horizontal(() =>
            {
                GTOption.Space(10);
                GTLayout.AC.Label("类名", Width);
                GTLayout.AC.Label("命名空间", GTOption.Width(200));
                GTLayout.AC.Label("静态类", Width);
                GTLayout.AC.Label("属性", GTOption.Width(500));
                GTOption.Space(10);
            }, Content, GTOption.Height(40));
            Vector = GTLayout.BE.ScrollView(() =>
            {
                foreach (var item in List)
                    GTLayout.BE.Horizontal(() => { DrawItem(item); }, Content, Height);
            }, Vector);
        }

        private void DrawItem(Type type)
        {
            GTOption.Space(10);
            GTLayout.AC.Label(type.Name, Width);
            GTLayout.AC.Label(type.Namespace, GTOption.Width(200));
            GTLayout.AC.Label(type.IsAbstract, Width);
            GTLayout.AC.Label(type.Attributes.ToString(), GTOption.Width(500));
            GTOption.Space(10);
        }
    }
}