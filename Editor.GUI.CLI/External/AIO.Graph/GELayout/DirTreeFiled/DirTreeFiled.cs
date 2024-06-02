#region

using System;
using System.IO;
using System.Linq;
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

        public void OnDraw(Rect rect)
        {
            if (GC_FOLDOUT is null)
            {
                GC_FOLDOUT    = GEContent.NewSetting("quanping-shouqi-xian", "收缩");
                GC_FOLDOUT_ON = GEContent.NewSetting("quanping-zhankai-xian", "展开");
                GC_REFRESH    = GEContent.NewSetting("重置", "刷新");
            }

            using (new GUI.ClipScope(rect))
            {
                var cell = new Rect(0, 0, rect.width, rect.height);
                if (string.IsNullOrEmpty(DirPath))
                {
                    if (Directory.Exists(DirPath))
                    {
                        CreateTree(DirPath, OptionDirDepth);
                    }
                    else
                    {
                        if (GUI.Button(cell, "Please Select Folder", GEStyle.TEtoolbarbutton))
                        {
                            GUI.FocusControl(null);
                            DirPath = EditorUtility.OpenFolderPanel("Select Folder", DirPath ?? Application.dataPath, "");
                            if (string.IsNullOrEmpty(DirPath)) return;
                            CreateTree(DirPath, OptionDirDepth);
                        }

                        return;
                    }
                }


                cell.width = 20;
                cell.x     = rect.width - cell.width;
                if (GUI.Button(cell, GC_REFRESH, GEStyle.toolbarbutton))
                {
                    GUI.FocusControl(null);
                    CreateTree(DirPath, OptionDirDepth);
                }

                cell.width =  50;
                cell.x     -= cell.width;
                if (GUI.Button(cell, "打开", GEStyle.toolbarbutton))
                {
                    GUI.FocusControl(null);
                    PrPlatform.Open.Path(GetFullPath()).Async();
                }

                if (OptionDirMaxDepth > 0)
                {
                    if (OptionFolded)
                    {
                        var list = Root.ToList();
                        for (var i = list.Count - 1; i >= 0; i--)
                        {
                            cell.width          =  75 * (i + 1);
                            cell.x              -= cell.width;
                            list[i].SelectIndex =  EditorGUI.Popup(cell, list[i].SelectIndex, list[i].Paths, GEStyle.PreDropDown);
                        }
                    }

                    if (OptionShowDepth)
                    {
                        cell.width     =  75;
                        cell.x         -= cell.width;
                        OptionDirDepth =  EditorGUI.IntSlider(cell, OptionDirDepth, 0, OptionDirMaxDepth);
                    }
                }


                if (!OptionFolded)
                {
                    cell.width =  75 * Root.MaxDepth;
                    cell.x     -= cell.width;
                    GUI.Label(cell, GetAbsolutePath(), GEStyle.toolbarbutton);
                }

                cell.width = cell.x - 30;
                cell.x     = 30;
                if (GUI.Button(cell, DirPath, GEStyle.toolbarbutton))
                {
                    GUI.FocusControl(null);
                    DirPath = EditorUtility.OpenFolderPanel("Select Folder", DirPath ?? Application.dataPath, string.Empty);
                    if (string.IsNullOrEmpty(DirPath)) return;
                    CreateTree(DirPath, OptionDirDepth);
                }

                cell.width =  30;
                cell.x     -= cell.width;
                if (GUI.Button(cell, OptionFolded ? GC_FOLDOUT : GC_FOLDOUT_ON, GEStyle.TEtoolbarbutton))
                {
                    GUI.FocusControl(null);
                    OptionFolded = !OptionFolded;
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