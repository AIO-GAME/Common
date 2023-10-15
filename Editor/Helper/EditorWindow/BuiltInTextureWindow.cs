/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-08                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 15:45:02                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using System.Collections.Generic;
using AIO.UEditor;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Unity内置图标
    /// </summary>
    [GWindow("Built In Texture Window", Group = "Tools",
        MinSizeWidth = 600, MinSizeHeight = 600
    )]
    public class BuiltInTextureGraphWindow : GraphicWindow
    {
        protected Vector2 Vector;
        protected List<string> m_Icons;
        protected int DrawAcrossNumber = 2;
        protected GUILayoutOption Height, AcrossHeight, TextWidth, TextureWidth;

        public BuiltInTextureGraphWindow()
        {
            m_Icons = new List<string>();
            Height = GUILayout.Height(30);
            AcrossHeight = GUILayout.Height(40);
            TextWidth = GUILayout.Width(160);
            TextureWidth = GUILayout.Width(100);
        }

        protected override void OnActivation()
        {
            m_Icons.Clear();
            foreach (var x in Resources.FindObjectsOfTypeAll<Texture2D>())
            {
                Debug.unityLogger.logEnabled = false;
                GUIContent gc = EditorGUIUtility.IconContent(x.name);
                Debug.unityLogger.logEnabled = true;
                if (gc != null && gc.image != null)
                    m_Icons.Add(x.name);
            }
        }

        protected override void OnGUI()
        {
            DrawAcrossNumber = GELayout.Slider("行ICON显示数量", DrawAcrossNumber, 2, 7);
            Vector = GELayout.VScrollView(() =>
            {
                for (int i = 0; i < m_Icons.Count; i += DrawAcrossNumber)
                {
                    DrawItemAcross(i, DrawAcrossNumber);
                }
            }, Vector, GTOption.WidthExpand(true));
        }

        /// <summary>
        /// 渲染一行
        /// </summary>
        protected void DrawItemAcross(int i, int count)
        {
            GELayout.VHorizontal(() =>
            {
                for (int j = 0, index; j < count; j++)
                {
                    index = i + j;
                    if (index < m_Icons.Count)
                        DrawItem(m_Icons[index]);
                }
            }, "sv_iconselector_back", GTOption.WidthExpand(true), AcrossHeight);
        }

        /// <summary>
        /// 渲染每个
        /// </summary>
        protected void DrawItem(string Name)
        {
            GELayout.LabelSelectable(Name, TextWidth, Height);
            GELayout.Button(EditorGUIUtility.IconContent(Name), () =>
            {
                TextEditor textEditor = new TextEditor();
                textEditor.text = Name;
                textEditor.OnFocus();
                textEditor.Copy();
            }, TextureWidth, Height);
        }
    }
}
