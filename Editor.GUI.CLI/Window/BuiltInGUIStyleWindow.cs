/* * * * * * * * * * * * * * * * * * * * * * * *
*Copyright(C) 2021 by XiNansky
*All rights reserved.
*FileName:         Framework.EToolWindow
*Author:           XiNan
*Version:          0.1
*UnityVersion:     2020.3.5f1c1
*Date:             2021-07-04
*NOWTIME:          14:11:19
*Description:
*History:
* * * * * * * * * * * * * * * * * * * * * * * * */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 获取全部 Unity GUI Style Viewer 样式
    /// </summary>
    [GWindow("GUI Style View", Group = "Tools",
        MinSizeWidth = 600, MinSizeHeight = 600
    )]
    public class BuiltInGUIStyleGraphWindow : GraphicWindow
    {
        public BuiltInGUIStyleGraphWindow()
        {
            Array = new Dictionary<string, GUIStyle>(128);
            ArrayIndex = new Dictionary<string, int>(128);
            Vector = new Vector2();
            Height = GTOption.Height(50);
            Width = GTOption.Width(40);
        }

        private Dictionary<string, GUIStyle> Array;
        private Dictionary<string, int> ArrayIndex;

        private Vector2 Vector;
        private GUILayoutOption Height, Width;
        private GUIStyle Content, Label;
        private string search = "";

        protected override void OnActivation()
        {
            search = "";
        }

        protected override void OnGUI()
        {
            if (Label == null) Label = "SearchTextField";
            if (Content == null) Content = "DD HeaderStyle";

            if (Array.Count == 0)
            {
                var index = 0;
                foreach (var style in GUI.skin.customStyles)
                    if (style.name.ToLower().Contains(search.ToLower()))
                    {
                        Array.Add(style.name.ToLower(), style);
                        ArrayIndex.Add(style.name.ToLower(), index++ * 60 - (60 / 2) + 30);
                    }
            }

            using (GELayout.VHorizontal(EditorStyles.helpBox, GTOption.WidthExpand(true), GTOption.Height(30)))
            {
                search = GELayout.Field(search, Label);
                if (GELayout.Button("Find", GTOption.Width(50))) FindSearchStyles();
            }

            Vector = GELayout.VScrollView(DrawContext, Vector);
        }

        private void FindSearchStyles()
        {
            if (!string.IsNullOrEmpty(search) && ArrayIndex.ContainsKey(search.ToLower()))
            {
                Vector.y = ArrayIndex[search.ToLower()];
            }
        }

        private void DrawContext()
        {
            var Height = GTOption.Height(60);
            //if (!search.ToLower().IsNullOrEmpty() && Array.ContainsKey(search.ToLower()))
            //{
            //    GTLayout.VHorizontal(() => { DrawItem(Array[search.ToLower()]); }, Content, Height);
            //}
            //else
            //{
            //    foreach (var style in Array.Values)
            //    {
            //        GTLayout.VHorizontal(() => { DrawItem(style); }, Content, Height);
            //    }
            //}
            foreach (var style in Array.Values)
            {
                GELayout.VHorizontal(() => { DrawItem(style); }, Content, Height);
            }
        }

        private void DrawItem(GUIStyle style)
        {
            GULayout.Space(10);
            GELayout.LabelPrefix(style.name);
            GELayout.Separator();
            GELayout.LabelSelectable(style.name, style, Height);
            GELayout.Separator();
            GELayout.Button("Copy", () =>
            {
                var textEditor = new TextEditor();
                textEditor.text = style.name;
                textEditor.OnFocus();
                textEditor.Copy();
            }, Width, Height);
            GULayout.Space(10);
        }

        protected override void OnDispose()
        {
            Array.Clear();
        }
    }
}