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
    [GWindow("Built In GUI Style View", Group = "Tools",
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
            if (Label == null) Label = new GUIStyle("SearchTextField");
            if (Content == null) Content = new GUIStyle("DD HeaderStyle");

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

            using (new GUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.ExpandWidth(true), GUILayout.Height(30)))
            {
                search = EditorGUILayout.TextField(search, Label);
                if (GUILayout.Button("Find", GUILayout.Width(50))) FindSearchStyles();
                if (GUILayout.Button("Gen", GUILayout.Width(50))) GEStyle.Gen();
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
                using (new GUILayout.HorizontalScope(Content, GTOption.Height(60)))
                {
                    DrawItem(style);
                }
            }
        }

        private void DrawItem(GUIStyle style)
        {
            GULayout.Space(10);
            EditorGUILayout.PrefixLabel(style.name);
            EditorGUILayout.Separator();
            EditorGUILayout.SelectableLabel(style.name, style, Height);
            EditorGUILayout.Separator();

            if (GULayout.Button("Copy", Width, Height))
            {
                var textEditor = new TextEditor { text = style.name };
                textEditor.OnFocus();
                textEditor.Copy();
            }

            GULayout.Space(10);
        }

        protected override void OnDispose()
        {
            Array.Clear();
        }
    }
}