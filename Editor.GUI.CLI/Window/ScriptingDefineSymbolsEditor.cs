﻿#region

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary> 宏定义管理器 </summary>
    [GWindow("宏定义管理器", Group = "Tools",
             MinSizeWidth = 600, MinSizeHeight = 600
    )]
    public class ScriptingDefineSymbols : GraphicWindow
    {
        private List<string> SymbolsList;
        private Vector2      Vector;

        public ScriptingDefineSymbols()
        {
            SymbolsList = new List<string>();
            Vector      = new Vector2();
        }

        protected override void OnDisable()
        {
            Save();
        }

        protected override void OnActivation()
        {
            Load();
        }

        protected void Load()
        {
            SymbolsList.Clear();
            for (var i = 0; i < EditorPrefs.GetInt("ScriptingDefineSymbolsNum"); i++) SymbolsList.Add(EditorPrefs.GetString($"ScriptingDefineSymbolsNum{i}"));

            if (SymbolsList.Count == 0)
            {
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                var str = EHelper.Symbols.GetScriptingDefine(buildTargetGroup);
                foreach (var item in str)
                    if (!SymbolsList.Contains(item))
                        SymbolsList.Add(item);
            }
        }

        protected void Save()
        {
            EditorPrefs.SetInt("ScriptingDefineSymbolsNum", SymbolsList.Count);
            for (var i = 0; i < SymbolsList.Count; i++) EditorPrefs.SetString($"ScriptingDefineSymbolsNum{i}", SymbolsList[i]);
        }

        /// <summary>
        /// 禁止你想要的宏定义
        /// </summary>
        public static void DelScriptingDefineSymbols(string value)
        {
            EHelper.Symbols.DelScriptingDefine(value);
        }

        /// <summary>
        /// 添加你想要的宏定义
        /// </summary>
        public static void AddScriptingDefineSymbols(string value)
        {
            EHelper.Symbols.AddScriptingDefine(value);
        }

        protected override void OnDraw()
        {
            GELayout.VHorizontal(() =>
            {
                GELayout.Label("路径 :");
                GELayout.Separator();
            });

            GELayout.VHorizontal(() =>
            {
                if (GELayout.Button("Load")) Load();

                if (GELayout.Button("Save")) Save();

                if (GELayout.Button("Add")) SymbolsList.Add("");

                if (GELayout.Button("Show")) { }

                if (GELayout.Button("Hide")) { }
            });

            Vector = GELayout.VScrollView(() =>
            {
                GELayout.Label("宏定义");
                for (var i = 0; i < SymbolsList.Count; i++)
                {
                    var i1 = i;
                    GELayout.VHorizontal(() =>
                    {
                        SymbolsList[i1] = GELayout.Field($"NO.{i1}", SymbolsList[i1]);
                        if (GELayout.Button("Enabled", GUILayout.Width(60))) AddScriptingDefineSymbols(SymbolsList[i1]);

                        if (GELayout.Button("Forbid", GUILayout.Width(60))) DelScriptingDefineSymbols(SymbolsList[i1]);

                        if (GELayout.Button("DEL", GUILayout.Width(60)))
                        {
                            DelScriptingDefineSymbols(SymbolsList[i1]);
                            SymbolsList.RemoveAt(i1);
                            Save();
                        }
                    });
                }
            }, Vector);
        }
    }
}