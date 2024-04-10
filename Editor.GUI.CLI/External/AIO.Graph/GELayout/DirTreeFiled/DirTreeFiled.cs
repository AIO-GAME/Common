#region

using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    [Serializable]
    public class DirTreeFiled : DirTree
    {
        private static GUIContent GC_FOLDOUT;
        private static GUIContent GC_FOLDOUT_ON;
        private static GUIContent GC_REFRESH;

        /// <summary>
        /// 是否折叠
        /// </summary>
        public bool OptionFolded;

        /// <summary>
        /// 是否显示深度
        /// </summary>
        public bool OptionShowDepth = true;

        /// <summary>
        /// 目录最大深度
        /// </summary>
        private int OptionDirMaxDepth;

        public DirTreeFiled() { }

        public DirTreeFiled(string directoryPath, int optionDirDepth = 1) : base(directoryPath, optionDirDepth) { }

        public void OnDraw()
        {
            if (GC_FOLDOUT is null)
            {
                GC_FOLDOUT    = GEContent.NewSetting("quanping-shouqi-xian", "收缩");
                GC_FOLDOUT_ON = GEContent.NewSetting("quanping-zhankai-xian", "展开");
                GC_REFRESH    = GEContent.NewSetting("重置", "刷新");
            }

            if (string.IsNullOrEmpty(DirPath))
            {
                if (Directory.Exists(DirPath))
                {
                    CreateTree(DirPath, OptionDirDepth);
                }
                else
                {
                    if (GUILayout.Button("Please Select Folder", GEStyle.TEtoolbarbutton))
                    {
                        GUI.FocusControl(null);
                        DirPath = EditorUtility.OpenFolderPanel("Select Folder", DirPath ?? Application.dataPath, "");
                        if (string.IsNullOrEmpty(DirPath)) return;
                        CreateTree(DirPath, OptionDirDepth);
                    }

                    return;
                }
            }

            using (new EditorGUILayout.HorizontalScope(GEStyle.Toolbar))
            {
                if (GELayout.Button(OptionFolded ? GC_FOLDOUT : GC_FOLDOUT_ON,
                                    GEStyle.TEtoolbarbutton, GTOption.Width(30)))
                {
                    GUI.FocusControl(null);
                    OptionFolded = !OptionFolded;
                }

                if (GUILayout.Button(DirPath,
                                     GEStyle.toolbarbutton, GUILayout.ExpandWidth(true)))
                {
                    GUI.FocusControl(null);
                    DirPath = EditorUtility.OpenFolderPanel("Select Folder",
                                                            DirPath ?? Application.dataPath, string.Empty);
                    if (string.IsNullOrEmpty(DirPath)) return;
                    CreateTree(DirPath, OptionDirDepth);
                }

                if (!OptionFolded)
                    GUILayout.Label(GetAbsolutePath(), GEStyle.toolbarbutton,
                                    GUILayout.MinWidth(50 * Root.MaxDepth), GUILayout.MaxWidth(100 * Root.MaxDepth));

                if (OptionDirMaxDepth > 0)
                {
                    if (OptionFolded)
                        foreach (var item in Root)
                            item.SelectIndex = EditorGUILayout.Popup(item.SelectIndex, item.Paths,
                                                                     GEStyle.PreDropDown, GUILayout.MinWidth(50), GUILayout.MaxWidth(100));

                    if (OptionShowDepth)
                        OptionDirDepth = GELayout.Slider(OptionDirDepth, 0, OptionDirMaxDepth,
                                                         GUILayout.MinWidth(50), GUILayout.MaxWidth(100));
                }

                if (GUILayout.Button(GC_REFRESH, GEStyle.toolbarbutton, GUILayout.Width(20)))
                {
                    GUI.FocusControl(null);
                    CreateTree(DirPath, OptionDirDepth);
                }

                if (GUILayout.Button("打开", GEStyle.toolbarbutton, GUILayout.Width(50)))
                {
                    GUI.FocusControl(null);
                    PrPlatform.Open.Path(GetFullPath()).Async();
                }
            }
        }

        protected override void OnUpdateOption()
        {
            var len = DirPath.Length;
            foreach (var directory in new DirectoryInfo(DirPath).GetDirectories("*", SearchOption.AllDirectories))
            {
                // 匹配正则表达式
                if (!string.IsNullOrEmpty(OptionSearchPatternFolder))
                    if (!Regex.IsMatch(directory.Name, OptionSearchPatternFolder))
                        continue;

                var depth = directory.FullName.Substring(len).Replace("\\", "/").Split('/').Length - 1;
                if (depth > OptionDirMaxDepth) OptionDirMaxDepth = depth;
            }
        }
    }
}