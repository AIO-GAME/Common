using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Unity.Editor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace UnityEditor
{
    /// <summary>
    /// 插件管理界面
    /// </summary>
    public class PluginsManagerWindow : EditorWindow
    {
        private static EditorWindow Window;

        public static void Open(Type[] types = null)
        {
            if (Window != null)
            {
                Window.Close();
                Window = null;
            }

            Window = GetWindow<PluginsManagerWindow>("Plugins Manager", true, types);
            Window.minSize = new Vector2(500, 200);
            Window.wantsMouseMove = true;
            Window.Show(true); //展示     
        }

        protected Vector2 Vector;

        /// <summary>
        /// 根节点
        /// </summary>
        internal Dictionary<string, PluginsInfo> RootData;

        /// <summary>
        /// 安装列表
        /// </summary>
        internal List<string> IntsallIndexList;

        /// <summary>
        /// 卸载列表
        /// </summary>
        internal List<string> UnIntsallIndexList;

        internal string Root;

        public PluginsManagerWindow()
        {
            DetailDic = new Dictionary<string, bool>();
            UnInsallIsSelectDic = new Dictionary<string, bool>();
            InsallIsSelectDic = new Dictionary<string, bool>();
            RootData = new Dictionary<string, PluginsInfo>();
            UnIntsallIndexList = new List<string>();
            IntsallIndexList = new List<string>();
        }

        protected void OnEnable()
        {
            Root = Application.dataPath.Replace("Assets", "Packages");
            UpdateData();
        }

        private void compilationStarted(object o)
        {
            CompilationPipeline.compilationStarted -= compilationStarted;
            UpdateData();
        }

        private void UpdateData()
        {
            RootData.Clear();
            DetailDic.Clear();
            IntsallIndexList.Clear();
            UnIntsallIndexList.Clear();

            foreach (var data in UtilsEditor.IO.GetAssetsRes<PluginsInfo>("t:PluginsInfo", "Packages"))
            {
                var filename = data.Name;
                if (!RootData.ContainsKey(filename))
                {
                    DetailDic.Add(filename, false);
                    RootData.Add(filename, data);
                    if (PluginsInfoEditor.GetValidDir(Root, data.TargetRelativePath).Exists)
                        UnIntsallIndexList.Add(filename);
                    else IntsallIndexList.Add(filename);
                }
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("插件安装管理", new GUIStyle("PreLabel"));
            HeaderView();
            Vector = EditorGUILayout.BeginScrollView(Vector);
            InsallView();
            EditorGUILayout.Space();
            UnInsallView();
            EditorGUILayout.EndScrollView();
        }

        private void HeaderView()
        {
        }

        private bool InsallIsSelect = false;

        private Dictionary<string, bool> InsallIsSelectDic;
        private Dictionary<string, bool> DetailDic;

        private void InsallView()
        {
            if (IntsallIndexList.Count == 0) return;

            EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(InsallIsSelect ? "取消" : "选择", GUILayout.Width(60)))
            {
                InsallIsSelect = !InsallIsSelect;
                InsallIsSelectDic.Clear();
                foreach (var item in IntsallIndexList) InsallIsSelectDic.Add(item, false);
            }

            if (InsallIsSelect)
            {
                if (GUILayout.Button("执行", GUILayout.Width(60)))
                {
                    InsallIsSelect = false;
                    if (InsallIsSelectDic.Count == 0) return;
                    var temp = new List<PluginsInfo>();
                    foreach (var item in IntsallIndexList.Where(V => InsallIsSelectDic[V]))
                        temp.Add(RootData[item]);
                    CompilationPipeline.compilationStarted += compilationStarted;
                    _ = PluginsInfoEditor.Initialize(temp);
                    return;
                }
            }

            EditorGUILayout.LabelField("安装列表", new GUIStyle("PreLabel"));

            if (!InsallIsSelect)
                if (GUILayout.Button("安装全部", GUILayout.Width(60)))
                {
                    CompilationPipeline.compilationStarted += compilationStarted;
                    _ = PluginsInfoEditor.Initialize(RootData.Values.Where(plugin => IntsallIndexList.Contains(plugin.Name)));
                    return;
                }

            EditorGUILayout.EndHorizontal();

            foreach (var Data in IntsallIndexList.Select(Name => RootData[Name]))
            {
                EditorGUILayout.BeginVertical("IN ThumbnailShadow");

                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(25));
                    if (InsallIsSelect)
                        InsallIsSelectDic[Data.Name] = EditorGUILayout.Toggle("", InsallIsSelectDic[Data.Name], GUILayout.Width(20));
                    if (GUILayout.Button("详", GUILayout.Width(25), GUILayout.Height(20)))
                    {
                        DetailDic[Data.Name] = !DetailDic[Data.Name];
                    }

                    EditorGUILayout.PrefixLabel(Data.Name);
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField(Data.Introduction, GUILayout.Width(150));
                    EditorGUILayout.Separator();

                    if (!InsallIsSelect)
                    {
                        if (GUILayout.Button("安装", GUILayout.Width(60), GUILayout.Height(20)))
                        {
                            CompilationPipeline.compilationStarted += compilationStarted;
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

        private bool UnInsallIsSelect = false;

        private Dictionary<string, bool> UnInsallIsSelectDic;

        private void UnInsallView()
        {
            if (UnIntsallIndexList.Count == 0) return;
            EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(UnInsallIsSelect ? "取消" : "选择", GUILayout.Width(60)))
            {
                UnInsallIsSelect = !UnInsallIsSelect;
                UnInsallIsSelectDic.Clear();
                foreach (var item in UnIntsallIndexList) UnInsallIsSelectDic.Add(item, false);
            }

            if (UnInsallIsSelect)
            {
                if (GUILayout.Button("执行", GUILayout.Width(60)))
                {
                    UnInsallIsSelect = false;
                    var temp = new List<PluginsInfo>();
                    foreach (var item in UnIntsallIndexList.Where(V => UnInsallIsSelectDic[V]))
                        temp.Add(RootData[item]);

                    if (temp.Count == 0) return;
                    CompilationPipeline.compilationStarted += compilationStarted;
                    _ = PluginsInfoEditor.UnInitialize(temp);
                    return;
                }
            }

            EditorGUILayout.LabelField("卸载列表", new GUIStyle("PreLabel"));
            if (!UnInsallIsSelect)
                if (GUILayout.Button("卸载全部", GUILayout.Width(60)))
                {
                    CompilationPipeline.compilationStarted += compilationStarted;
                    _ = PluginsInfoEditor.UnInitialize(RootData.Values.Where(plugin => UnIntsallIndexList.Contains(plugin.Name)));
                    return;
                }

            EditorGUILayout.EndHorizontal();

            foreach (var Data in UnIntsallIndexList.Select(Name => RootData[Name]))
            {
                EditorGUILayout.BeginVertical("IN ThumbnailShadow");
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(25));

                    if (UnInsallIsSelect)
                        UnInsallIsSelectDic[Data.Name] = EditorGUILayout.Toggle("", UnInsallIsSelectDic[Data.Name], GUILayout.Width(20));
                    if (GUILayout.Button("详", GUILayout.Width(25), GUILayout.Height(20))) DetailDic[Data.Name] = !DetailDic[Data.Name];

                    EditorGUILayout.PrefixLabel(Data.Name);
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField(Data.Introduction, GUILayout.Width(150));
                    EditorGUILayout.Separator();

                    if (!UnInsallIsSelect)
                    {
                        if (!string.IsNullOrEmpty(Data.MacroDefinition))
                        {
                            if (GUILayout.Button("更新宏", GUILayout.Width(60), GUILayout.Height(20)))
                            {
                                UtilsEditor.Symbols.AddScriptingDefine(Data.MacroDefinition.Split(';'));
                                AssetDatabase.Refresh();
                                AssetDatabase.RefreshSettings();
                                CompilationPipeline.RequestScriptCompilation();
                                return;
                            }
                        }

                        if (GUILayout.Button("卸载", GUILayout.Width(60), GUILayout.Height(20)))
                        {
                            CompilationPipeline.compilationStarted += compilationStarted;
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