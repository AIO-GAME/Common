#region

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 获取全部 Unity GUI Style Viewer 样式
    /// </summary>
    [GWindow("Built In GUI Style View", Group = "Tools", MinSizeWidth = 600, MinSizeHeight = 600)]
    public class BuiltInGUIStyleGraphWindow : GraphicWindow
    {
        private Dictionary<string, List<GUIStyle>> Array;
        private GUIStyle                           Content, Label;
        private Dictionary<string, GUIStyle>       DataTable;
        private GUILayoutOption                    Height, Width;
        private string                             search = "";

        private Vector2 Vector;

        private Dictionary<string, bool> versionFolds;

        public BuiltInGUIStyleGraphWindow()
        {
            Array        = new Dictionary<string, List<GUIStyle>>();
            DataTable    = new Dictionary<string, GUIStyle>();
            Vector       = new Vector2();
            Height       = GUILayout.Height(50);
            Width        = GUILayout.Width(40);
            versionFolds = new Dictionary<string, bool>();
        }

        protected override void OnDisable()
        {
            Array.Clear();
            versionFolds.Clear();
        }

        protected override void OnActivation()
        {
            search = "";
        }


        protected override void OnDraw()
        {
            if (Label == null) Label     = new GUIStyle("SearchTextField");
            if (Content == null) Content = new GUIStyle("DD HeaderStyle");

            if (Array.Count == 0)
            {
                Array.Clear();
                var formatPath = PackageInfo.FindForAssembly(typeof(GEStyle).Assembly).resolvedPath +
                                 "/Resources/Editor/Graph/Style/{0}.txt";
                var versions = new[] { "2019", "2020", "2021", "2022", "2023", "2024", "2025" };

                if (File.Exists(string.Format(formatPath, "Common")))
                {
                    var list = new List<GUIStyle>(128);
                    foreach (var line in File.ReadAllLines(string.Format(formatPath, "Common")))
                    {
                        var key = line.ToLower();
                        if (DataTable.ContainsKey(key)) continue;
                        DataTable.Add(key, line);
                        list.Add(DataTable[key]);
                    }

                    versionFolds.Add("Common", false);
                    Array["Common"] = list;
                }

                var unityVersion = Application.unityVersion.Split('.')[0];
                foreach (var version in versions)
                {
                    var path = string.Format(formatPath, version);
                    if (!File.Exists(path)) continue;
                    Array.Add(version, new List<GUIStyle>(128));
                    versionFolds.Add(version, unityVersion == version);
                    foreach (var line in File.ReadAllLines(path))
                    {
                        var key = line.ToLower();
                        if (DataTable.ContainsKey(key)) continue;
                        DataTable.Add(key, line);
                        Array[version].Add(DataTable[key]);
                    }
                }
            }

            using (new GUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.ExpandWidth(true),
                                                 GUILayout.Height(30)))
            {
                search = EditorGUILayout.TextField(search, Label);
                if (GUILayout.Button("Find", GUILayout.Width(50))) FindSearchStyles();
                if (GUILayout.Button("Gen", GUILayout.Width(50))) GEStyle.Gen();
            }

            Vector = GELayout.VScrollView(DrawContext, Vector);
        }

        private void FindSearchStyles()
        {
            // if (!string.IsNullOrEmpty(search) && DataTabel.ContainsKey(search.ToLower()))
            // {
            //     Vector.y = DataTabel[search.ToLower()];
            // }
        }

        private void DrawContext()
        {
            foreach (var keyValuePair in Array)
                versionFolds[keyValuePair.Key] = GELayout.VFoldoutHeader(
                    delegate { DrawListItem(keyValuePair.Value); },
                    string.Concat(keyValuePair.Key, '[', keyValuePair.Value.Count, ']'),
                    versionFolds[keyValuePair.Key]
                );
        }

        private void DrawListItem(IEnumerable<GUIStyle> collection)
        {
            using (new GUILayout.HorizontalScope("ChannelStripAttenuationBar", GUILayout.ExpandWidth(true)))
            {
                GULayout.Space(10);
            }

            foreach (var style in collection)
                using (new GUILayout.VerticalScope(GUILayout.ExpandWidth(true)))
                {
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        GULayout.Space(10);
                        EditorGUILayout.PrefixLabel(style.name);
                        EditorGUILayout.Separator();
                        if (GULayout.Button("Copy", Width))
                        {
                            var textEditor = new TextEditor { text = string.Concat('"', style.name, '"') };
                            textEditor.OnFocus();
                            textEditor.Copy();
                        }

                        GULayout.Space(10);
                    }


                    EditorGUILayout.SelectableLabel(style.name, style, Height);

                    using (new GUILayout.HorizontalScope("ChannelStripAttenuationBar", GUILayout.ExpandWidth(true)))
                    {
                        GULayout.Space(10);
                    }
                }
        }
    }
}