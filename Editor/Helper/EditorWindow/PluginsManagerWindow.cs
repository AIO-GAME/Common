﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 插件管理界面
    /// </summary>
    [GWindow("插件管理界面", Group = "Tools",
        MinSizeWidth = 200, MinSizeHeight = 600
    )]
    public class PluginsManagerWindow : GraphicWindow
    {
        protected Vector2 Vector;

        /// <summary>
        /// 根节点
        /// </summary>
        internal Dictionary<string, PluginsInfo> RootData;

        /// <summary>
        /// 安装列表
        /// </summary>
        internal List<string> InstallIndexList;

        /// <summary>
        /// 卸载列表
        /// </summary>
        internal List<string> UnInstallIndexList;

        internal string Root;

        public PluginsManagerWindow()
        {
            DetailDic = new Dictionary<string, bool>();
            UnInstallIsSelectDic = new Dictionary<string, bool>();
            InstallIsSelectDic = new Dictionary<string, bool>();
            RootData = new Dictionary<string, PluginsInfo>();
            UnInstallIndexList = new List<string>();
            InstallIndexList = new List<string>();
        }

        protected override void OnActivation()
        {
            Root = Application.dataPath.Replace("Assets", "Packages");
            UpdateData();
        }

#if UNITY_2019_1_OR_NEWER
        private void compilationStarted(object o)
        {
            CompilationPipeline.compilationStarted -= compilationStarted;
            UpdateData();
        }
#else
        private void compilationStarted(string o)
        {
            CompilationPipeline.assemblyCompilationStarted -= compilationStarted;
            UpdateData();
        }
#endif

        private void UpdateData()
        {
            RootData.Clear();
            DetailDic.Clear();
            InstallIndexList.Clear();
            UnInstallIndexList.Clear();

            foreach (var data in EHelper.IO.GetAssetsRes<PluginsInfo>("t:PluginsInfo", "Packages"))
            {
                var filename = data.Name;
                if (!RootData.ContainsKey(filename))
                {
                    DetailDic.Add(filename, false);
                    RootData.Add(filename, data);
                    if (PluginsInfoEditor.GetValidDir(Root, data.TargetRelativePath).Exists)
                        UnInstallIndexList.Add(filename);
                    else InstallIndexList.Add(filename);
                }
            }
        }

        protected override void OnGUI()
        {
            EditorGUILayout.LabelField("插件安装管理", new GUIStyle("PreLabel"));
            HeaderView();
            Vector = EditorGUILayout.BeginScrollView(Vector);
            InstallView();
            EditorGUILayout.Space();
            UnInstallView();
            EditorGUILayout.EndScrollView();
        }

        private void HeaderView()
        {
        }

        private bool InstallIsSelect = false;

        private Dictionary<string, bool> InstallIsSelectDic;
        private Dictionary<string, bool> DetailDic;

        private void InstallView()
        {
            if (InstallIndexList.Count == 0) return;

            EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(InstallIsSelect ? "取消" : "选择", GUILayout.Width(60)))
            {
                InstallIsSelect = !InstallIsSelect;
                InstallIsSelectDic.Clear();
                foreach (var item in InstallIndexList) InstallIsSelectDic.Add(item, false);
            }

            if (InstallIsSelect)
            {
                if (GUILayout.Button("执行", GUILayout.Width(60)))
                {
                    InstallIsSelect = false;
                    if (InstallIsSelectDic.Count == 0) return;
                    var temp = new List<PluginsInfo>();
                    foreach (var item in InstallIndexList.Where(V => InstallIsSelectDic[V]))
                        temp.Add(RootData[item]);
#if UNITY_2019_1_OR_NEWER
                    CompilationPipeline.compilationStarted += compilationStarted;
#else
                    CompilationPipeline.assemblyCompilationStarted += compilationStarted;
#endif
                    _ = PluginsInfoEditor.Initialize(temp);
                    return;
                }
            }

            EditorGUILayout.LabelField("安装列表", new GUIStyle("PreLabel"));

            if (!InstallIsSelect)
                if (GUILayout.Button("安装全部", GUILayout.Width(60)))
                {
#if UNITY_2019_1_OR_NEWER
                    CompilationPipeline.compilationStarted += compilationStarted;
#else
                    CompilationPipeline.assemblyCompilationStarted += compilationStarted;
#endif
                    _ = PluginsInfoEditor.Initialize(RootData.Values.Where(plugin =>
                        InstallIndexList.Contains(plugin.Name)));
                    return;
                }

            EditorGUILayout.EndHorizontal();

            foreach (var Data in InstallIndexList.Select(Name => RootData[Name]))
            {
                EditorGUILayout.BeginVertical("IN ThumbnailShadow");

                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(25));
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
                    {
                        if (GUILayout.Button("安装", GUILayout.Width(60), GUILayout.Height(20)))
                        {
#if UNITY_2019_1_OR_NEWER
                            CompilationPipeline.compilationStarted += compilationStarted;
#else
                            CompilationPipeline.assemblyCompilationStarted += compilationStarted;
#endif
                            _ = PluginsInfoEditor.Initialize(Data);
                            return;
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                }

                {
                    if (DetailDic[Data.Name])
                    {
                        EditorGUILayout.LabelField("源文件路径 ->" + Data.SourceRelativePath);
                        EditorGUILayout.LabelField("安装路径ㅤ ->" + Data.TargetRelativePath);
                        if (!string.IsNullOrEmpty(Data.MacroDefinition))
                        {
                            EditorGUILayout.LabelField("宏定义ㅤㅤ ->" + Data.MacroDefinition);
                        }

                        if (!string.IsNullOrEmpty(Data.Introduction))
                        {
                            EditorGUILayout.LabelField("简介ㅤㅤㅤ ->" + Data.Introduction);
                        }
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
        }

        private bool UnInstallIsSelect = false;

        private Dictionary<string, bool> UnInstallIsSelectDic;

        private void UnInstallView()
        {
            if (UnInstallIndexList.Count == 0) return;
            EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(UnInstallIsSelect ? "取消" : "选择", GUILayout.Width(60)))
            {
                UnInstallIsSelect = !UnInstallIsSelect;
                UnInstallIsSelectDic.Clear();
                foreach (var item in UnInstallIndexList) UnInstallIsSelectDic.Add(item, false);
            }

            if (UnInstallIsSelect)
            {
                if (GUILayout.Button("执行", GUILayout.Width(60)))
                {
                    UnInstallIsSelect = false;
                    var temp = new List<PluginsInfo>();
                    foreach (var item in UnInstallIndexList.Where(V => UnInstallIsSelectDic[V]))
                        temp.Add(RootData[item]);

                    if (temp.Count == 0) return;
#if UNITY_2019_1_OR_NEWER
                    CompilationPipeline.compilationStarted += compilationStarted;
#else
                    CompilationPipeline.assemblyCompilationStarted += compilationStarted;
#endif
                    _ = PluginsInfoEditor.UnInitialize(temp);
                    return;
                }
            }

            EditorGUILayout.LabelField("卸载列表", new GUIStyle("PreLabel"));
            if (!UnInstallIsSelect)
                if (GUILayout.Button("卸载全部", GUILayout.Width(60)))
                {
#if UNITY_2019_1_OR_NEWER
                    CompilationPipeline.compilationStarted += compilationStarted;
#else
                    CompilationPipeline.assemblyCompilationStarted += compilationStarted;
#endif
                    _ = PluginsInfoEditor.UnInitialize(RootData.Values.Where(plugin =>
                        UnInstallIndexList.Contains(plugin.Name)));
                    return;
                }

            EditorGUILayout.EndHorizontal();

            foreach (var Data in UnInstallIndexList.Select(Name => RootData[Name]))
            {
                EditorGUILayout.BeginVertical("IN ThumbnailShadow");
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(25));

                    if (UnInstallIsSelect)
                        UnInstallIsSelectDic[Data.Name] =
                            EditorGUILayout.Toggle("", UnInstallIsSelectDic[Data.Name], GUILayout.Width(20));
                    if (GUILayout.Button("详", GUILayout.Width(25), GUILayout.Height(20)))
                        DetailDic[Data.Name] = !DetailDic[Data.Name];

                    EditorGUILayout.PrefixLabel(Data.Name);
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField(Data.Introduction, GUILayout.Width(150));
                    EditorGUILayout.Separator();

                    if (!UnInstallIsSelect)
                    {
                        if (!string.IsNullOrEmpty(Data.MacroDefinition))
                        {
                            if (GUILayout.Button("更新宏", GUILayout.Width(60), GUILayout.Height(20)))
                            {
                                EHelper.Symbols.AddScriptingDefine(Data.MacroDefinition.Split(';'));
                                AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                                AssetDatabase.RefreshSettings();
#endif
#if UNITY_2019_1_OR_NEWER
                                CompilationPipeline.RequestScriptCompilation();
#endif
                                return;
                            }
                        }

                        if (GUILayout.Button("卸载", GUILayout.Width(60), GUILayout.Height(20)))
                        {
#if UNITY_2019_1_OR_NEWER
                            CompilationPipeline.compilationStarted += compilationStarted;
#else
                            CompilationPipeline.assemblyCompilationStarted += compilationStarted;
#endif
                            _ = PluginsInfoEditor.UnInitialize(Data);
                            return;
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                }
                {
                    if (DetailDic[Data.Name])
                    {
                        EditorGUILayout.LabelField("源文件路径 ->" + Data.SourceRelativePath);
                        EditorGUILayout.LabelField("安装路径ㅤ ->" + Data.TargetRelativePath);
                        if (!string.IsNullOrEmpty(Data.MacroDefinition))
                        {
                            EditorGUILayout.LabelField("宏定义ㅤㅤ ->" + Data.MacroDefinition);
                        }

                        if (!string.IsNullOrEmpty(Data.Introduction))
                        {
                            EditorGUILayout.LabelField("简介ㅤㅤㅤ ->" + Data.Introduction);
                        }
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
        }
    }
}