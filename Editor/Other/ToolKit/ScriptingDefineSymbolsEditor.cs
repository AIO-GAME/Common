/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games
* All rights reserved.
* FileName:         Framework.ToolWindow
* Author:           XiNan
* Version:          0.4
* UnityVersion:     2019.4.10f1
* Date:             2021-07-06
* Time:             15:22:03
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary> 宏定义管理器 </summary>
    public class ScriptingDefineSymbols : GraphicWindow
    {
        private static EditorWindow Window;

        public static void Open()
        {
            if (Window != null)
            {
                Window.Close();
                Window = null;
            }

            Window = UtilsEditor.Window.Open<ScriptingDefineSymbols>("Scripting Define Symbols Editor", true, true);
        }

        private Vector2 Vector;
        private List<string> SymbolsList;

        public ScriptingDefineSymbols()
        {
            SymbolsList = new List<string>();
            Vector = new Vector2();
        }

        protected override void OnEnable()
        {
            Load();
        }

        protected void Load()
        {
            SymbolsList.Clear();
            for (var i = 0; i < EditorPrefs.GetInt("ScriptingDefineSymbolsNum"); i++)
            {
                SymbolsList.Add(EditorPrefs.GetString($"ScriptingDefineSymbolsNum{i}"));
            }

            if (SymbolsList.Count == 0)
            {
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                foreach (var item in str)
                {
                    if (!SymbolsList.Contains(item))
                    {
                        SymbolsList.Add(item);
                    }
                }
            }
        }

        protected void Save()
        {
            EditorPrefs.SetInt("ScriptingDefineSymbolsNum", SymbolsList.Count);
            for (var i = 0; i < SymbolsList.Count; i++)
            {
                EditorPrefs.SetString($"ScriptingDefineSymbolsNum{i}", SymbolsList[i]);
            }
        }

        /// <summary>
        /// 禁止你想要的宏定义
        /// </summary>
        public static void DelScriptingDefineSymbols(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length == 0) return;
            //获取当前是哪个平台
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            //获得当前平台已有的的宏定义
            var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!string.IsNullOrEmpty(str) && str.Length != 0 && str.Contains(value))
            {
                str = str.Replace(value, "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
            }
        }

        /// <summary>
        /// 添加你想要的宏定义
        /// </summary>
        public static void AddScriptingDefineSymbols(string value)
        {
            //获取当前是哪个平台
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            //获得当前平台已有的的宏定义
            var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!str.Contains(value))
            {
                //添加宏定义
                str += ";" + value;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
            }
        }

        protected override void OnGUI()
        {
            GELayout.Horizontal(() =>
            {
                GELayout.Label($"路径 :");
                GTOption.Separator();
            });

            GELayout.Horizontal(() =>
            {
                if (GELayout.Button("Load"))
                {
                    Load();
                }

                if (GELayout.Button("Save"))
                {
                    Save();
                }

                if (GELayout.Button("Add"))
                {
                    SymbolsList.Add("");
                }

                if (GELayout.Button("Show"))
                {
                }

                if (GELayout.Button("Hide"))
                {
                }
            });

            Vector = GELayout.ScrollView(() =>
            {
                GELayout.Label($"宏定义");
                for (var i = 0; i < SymbolsList.Count; i++)
                {
                    var i1 = i;
                    GELayout.Horizontal(() =>
                    {
                        SymbolsList[i1] = GELayout.Field($"NO.{i1}", SymbolsList[i1]);
                        if (GELayout.Button("Enabled", GUILayout.Width(60)))
                        {
                            AddScriptingDefineSymbols(SymbolsList[i1]);
                        }

                        if (GELayout.Button("Forbid", GUILayout.Width(60)))
                        {
                            DelScriptingDefineSymbols(SymbolsList[i1]);
                        }

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

        protected override void OnDisable()
        {
            Save();
            base.OnDisable();
        }
    }
}
