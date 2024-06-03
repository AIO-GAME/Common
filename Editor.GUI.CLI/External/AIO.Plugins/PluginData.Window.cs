#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using AIO.UEditor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

#endregion

namespace AIO
{
    internal partial class Plugins
    {
        [MenuItem("AIO/Window/Plugin Data Manager")]
        public static void Open()
        {
            EHelper.Window.Open<PluginDataWindow>();
        }

        #region Nested type: PluginDataWindow

        /// <summary>
        /// 插件管理界面
        /// </summary>
        [GWindow("插件管理界面", "Plugin Data Manager", MinSizeHeight = 600, MinSizeWidth = 400)]
        private class PluginDataWindow : GraphicWindow
        {
            private Dictionary<string, bool> DetailDic;

            /// <summary>
            /// 安装列表
            /// </summary>
            private List<string> InstallIndexList;

            private bool InstallIsSelect;

            private Dictionary<string, bool> InstallIsSelectDic;

            private string Root;

            /// <summary>
            /// 根节点
            /// </summary>
            private Dictionary<string, PluginData> RootData;

            /// <summary>
            /// 卸载列表
            /// </summary>
            private List<string> UnInstallIndexList;

            private bool UnInstallIsSelect;

            private Dictionary<string, bool> UnInstallIsSelectDic;
            private Vector2                  Vector;

            public PluginDataWindow()
            {
                DetailDic            = new Dictionary<string, bool>();
                UnInstallIsSelectDic = new Dictionary<string, bool>();
                InstallIsSelectDic   = new Dictionary<string, bool>();
                RootData             = new Dictionary<string, PluginData>();
                UnInstallIndexList   = new List<string>();
                InstallIndexList     = new List<string>();
            }

            ~PluginDataWindow()
            {
                UnInstallIsSelectDic.Clear();
                InstallIsSelectDic.Clear();
                RootData.Clear();
                DetailDic.Clear();
                InstallIndexList.Clear();
                UnInstallIndexList.Clear();
                RootData             = null;
                DetailDic            = null;
                InstallIndexList     = null;
                UnInstallIndexList   = null;
                InstallIsSelectDic   = null;
                UnInstallIsSelectDic = null;
            }

            private void UpdateData()
            {
                RootData.Clear();
                DetailDic.Clear();
                InstallIndexList.Clear();
                UnInstallIndexList.Clear();

                foreach (var data in EHelper.IO.GetAssetsRes<PluginData>($"t:{nameof(PluginData)}", "Packages",
                                                                         "Assets"))
                {
                    var filename = data.Name;
                    if (!RootData.ContainsKey(filename))
                    {
                        DetailDic.Add(filename, false);
                        RootData.Add(filename, data);
                        if (PluginDataEditor.GetValidDir(Root, data.TargetRelativePath).Exists)
                            UnInstallIndexList.Add(filename);
                        else InstallIndexList.Add(filename);
                    }
                }
            }

            private void HeaderView()
            {
                if (InstallIndexList is null || InstallIndexList.Count == 0) return;

                using (new EditorGUILayout.HorizontalScope("HeaderButton"))
                {
                    if (GUILayout.Button(InstallIsSelect ? "取消" : "选择", GUILayout.Width(60)))
                    {
                        InstallIsSelect = !InstallIsSelect;
                        InstallIsSelectDic.Clear();
                        foreach (var item in InstallIndexList) InstallIsSelectDic.Add(item, false);
                        return;
                    }

                    if (InstallIsSelect)
                        if (GUILayout.Button("执行", GUILayout.Width(60)))
                        {
                            InstallIsSelect = false;
                            if (InstallIsSelectDic.Count == 0) return;
                            var temp = InstallIndexList.Where(V => InstallIsSelectDic[V]).Select(item => RootData[item]).ToList();

                            CompilationPipeline.compilationFinished += compilationFinished;
                            _                                       =  PluginDataEditor.Initialize(temp);

                            return;
                        }

                    EditorGUILayout.LabelField("安装列表", new GUIStyle("PreLabel"));

                    if (!InstallIsSelect)
                        if (GUILayout.Button("安装全部", GUILayout.Width(60)))
                        {
                            CompilationPipeline.compilationFinished += compilationFinished;
                            _ = PluginDataEditor.Initialize(RootData.Values.Where(plugin =>
                                                                                      InstallIndexList.Contains(plugin.Name)));
                        }
                }
            }

            private void InstallView()
            {
                if (InstallIndexList.Count == 0) return;
                using (new EditorGUILayout.VerticalScope())
                {
                    foreach (var Data in InstallIndexList.Select(Name => RootData[Name]))
                    {
                        using (new EditorGUILayout.VerticalScope("HelpBox"))
                        {
                            using (new EditorGUILayout.HorizontalScope(GUILayout.Height(25)))
                            {
                                if (InstallIsSelect)
                                    InstallIsSelectDic[Data.Name] =
                                        EditorGUILayout.Toggle("", InstallIsSelectDic[Data.Name], GUILayout.Width(20));
                                if (GUILayout.Button("详", GUILayout.Width(25), GUILayout.Height(20)))
                                {
                                    DetailDic[Data.Name] = !DetailDic[Data.Name];
                                    if (DetailDic[Data.Name]) Selection.activeObject = Data;
                                }

                                EditorGUILayout.PrefixLabel(Data.Name);
                                EditorGUILayout.Separator();
                                EditorGUILayout.LabelField(Data.Introduction, GUILayout.Width(150));
                                EditorGUILayout.Separator();

                                if (!InstallIsSelect)
                                    if (GUILayout.Button("安装", GUILayout.Width(60), GUILayout.Height(20)))
                                    {
                                        _ = PluginDataEditor.Initialize(Data);
                                        UpdateData();
                                        return;
                                    }
                            }

                            if (DetailDic[Data.Name])
                            {
                                EditorGUILayout.LabelField("源文件路径 ->" + Data.SourceRelativePath);
                                EditorGUILayout.LabelField("安装路径ㅤ ->" + Data.TargetRelativePath);
                                if (!string.IsNullOrEmpty(Data.MacroDefinition)) EditorGUILayout.LabelField("宏定义ㅤㅤ ->" + Data.MacroDefinition);

                                if (!string.IsNullOrEmpty(Data.Introduction)) EditorGUILayout.LabelField("简介ㅤㅤㅤ ->" + Data.Introduction);
                            }
                        }

                        EditorGUILayout.Space();
                    }
                }
            }

            private void UnInstallView()
            {
                if (UnInstallIndexList.Count == 0) return;
                using (new EditorGUILayout.VerticalScope("ChannelStripBg"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button(UnInstallIsSelect ? "取消" : "选择", GUILayout.Width(60)))
                        {
                            UnInstallIsSelect = !UnInstallIsSelect;
                            UnInstallIsSelectDic.Clear();
                            foreach (var item in UnInstallIndexList) UnInstallIsSelectDic.Add(item, false);
                        }

                        if (UnInstallIsSelect)
                            if (GUILayout.Button("执行", GUILayout.Width(60)))
                            {
                                UnInstallIsSelect = false;
                                var temp = UnInstallIndexList.Where(V => UnInstallIsSelectDic[V]).Select(item => RootData[item]).ToList();

                                if (temp.Count == 0) return;

                                CompilationPipeline.compilationFinished += compilationFinished;
                                _                                       =  PluginDataEditor.UnInitialize(temp);
                                return;
                            }

                        EditorGUILayout.LabelField("卸载列表", new GUIStyle("PreLabel"));
                        if (!UnInstallIsSelect)
                            if (GUILayout.Button("卸载全部", GUILayout.Width(60)))
                            {
                                CompilationPipeline.compilationFinished += compilationFinished;
                                _ = PluginDataEditor.UnInitialize(RootData.Values.Where(plugin =>
                                                                                            UnInstallIndexList.Contains(plugin.Name)));
                                return;
                            }
                    }

                    foreach (var Data in UnInstallIndexList.Select(Name => RootData[Name]))
                    {
                        using (new EditorGUILayout.VerticalScope("HelpBox"))
                        {
                            using (new EditorGUILayout.HorizontalScope(GUILayout.Height(25)))
                            {
                                if (UnInstallIsSelect)
                                    UnInstallIsSelectDic[Data.Name] =
                                        EditorGUILayout.Toggle("", UnInstallIsSelectDic[Data.Name],
                                                               GUILayout.Width(20));
                                if (GUILayout.Button("详", GUILayout.Width(25), GUILayout.Height(20)))
                                    DetailDic[Data.Name] = !DetailDic[Data.Name];

                                EditorGUILayout.PrefixLabel(Data.Name);
                                EditorGUILayout.Separator();
                                EditorGUILayout.LabelField(Data.Introduction, GUILayout.Width(150));
                                EditorGUILayout.Separator();

                                if (!UnInstallIsSelect)
                                {
                                    if (!string.IsNullOrEmpty(Data.MacroDefinition))
                                        if (GUILayout.Button("更新宏", GUILayout.Width(60), GUILayout.Height(20)))
                                        {
                                            EHelper.Symbols.AddScriptingDefine(
                                                EditorUserBuildSettings.selectedBuildTargetGroup,
                                                Data.MacroDefinition.Split(';'));
                                            AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                                            AssetDatabase.RefreshSettings();
#endif
                                            CompilationPipeline.RequestScriptCompilation();
                                            return;
                                        }

                                    if (GUILayout.Button("卸载", GUILayout.Width(60), GUILayout.Height(20)))
                                    {
                                        _                                       =  PluginDataEditor.UnInitialize(Data);
                                        CompilationPipeline.compilationFinished += compilationFinished;
                                        return;
                                    }
                                }
                            }

                            if (DetailDic[Data.Name])
                            {
                                EditorGUILayout.LabelField("源文件路径 ->" + Data.SourceRelativePath);
                                EditorGUILayout.LabelField("安装路径ㅤ ->" + Data.TargetRelativePath);
                                if (!string.IsNullOrEmpty(Data.MacroDefinition))
                                    EditorGUILayout.LabelField("宏定义ㅤㅤ ->" + Data.MacroDefinition);
                                if (!string.IsNullOrEmpty(Data.Introduction))
                                    EditorGUILayout.LabelField("简介ㅤㅤㅤ ->" + Data.Introduction);
                            }
                        }

                        EditorGUILayout.Space();
                    }
                }
            }

            protected override void OnActivation()
            {
                var root = Directory.GetParent(Application.dataPath);
                if (root is null) throw new DirectoryNotFoundException("未找到 Application.dataPath 根目录");
                Root = Path.Combine(root.FullName, "Packages");
                UpdateData();
            }

            protected override void OnDraw()
            {
                EditorGUILayout.LabelField(new GUIContent("插件安装管理"), "PreLabel");
                HeaderView();
                Vector = EditorGUILayout.BeginScrollView(Vector);
                InstallView();
                EditorGUILayout.Space();
                UnInstallView();
                EditorGUILayout.EndScrollView();
            }

            private void compilationFinished(object o)
            {
                CompilationPipeline.compilationFinished -= compilationFinished;
                UpdateData();
            }
        }

        #endregion
    }
}