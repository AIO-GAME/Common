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

namespace AIO.Unity.Editor
{
    /// <summary>
    /// 获取全部 Unity GUI Style Viewer 样式
    /// </summary>
    public class BuiltInGUIStyleGraphWindow : GraphicWindow
    {
        private static EditorWindow Window;

        public static void Open()
        {
            if (Window != null)
            {
                Window.Close();
                Window = null;
            }

            Window = UtilsEditor.Window.Open<BuiltInGUIStyleGraphWindow>("GUIStyle Viewer", true, true);
        }

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

        protected override void OnEnable()
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

            GELayout.Horizontal(() =>
            {
                search = GELayout.Field(search, Label);
                if (GELayout.Button("Find", GTOption.Width(50))) FindSearchStyles();
            }, EditorStyles.helpBox, GTOption.WidthExpand(true), GTOption.Height(30));

            Vector = GELayout.ScrollView(DrawContext, Vector);
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
            //    GTLayout.Horizontal(() => { DrawItem(Array[search.ToLower()]); }, Content, Height);
            //}
            //else
            //{
            //    foreach (var style in Array.Values)
            //    {
            //        GTLayout.Horizontal(() => { DrawItem(style); }, Content, Height);
            //    }
            //}
            foreach (var style in Array.Values)
            {
                GELayout.Horizontal(() => { DrawItem(style); }, Content, Height);
            }
        }

        private void DrawItem(GUIStyle style)
        {
            GTOption.Space(10);
            GELayout.LabelPrefix(style.name);
            GTOption.Separator();
            GELayout.LabelSelectable(style.name, style, Height);
            GTOption.Separator();
            GELayout.Button("Copy", () =>
            {
                var textEditor = new TextEditor();
                textEditor.text = style.name;
                textEditor.OnFocus();
                textEditor.Copy();
            }, Width, Height);
            GTOption.Space(10);
        }

        protected override void OnDestroy()
        {
            Array.Clear();
            base.OnDestroy();
        }
    }
}