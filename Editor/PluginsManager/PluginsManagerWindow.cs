using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO.Package.Editor
{
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

            Window = GetWindow<PluginsManagerWindow>("Plugins Manager Windows", true, types);
            Window.wantsMouseMove = true;
            Window.Show(true); //展示     
        }

        protected Vector2 Vector;
        internal Dictionary<string, PluginsInfoJson> List;
        internal Dictionary<string, bool> ListStauts;
        internal string Root;

        public PluginsManagerWindow()
        {
            List = new Dictionary<string, PluginsInfoJson>();
            ListStauts = new Dictionary<string, bool>();
        }

        protected void OnEnable()
        {
            Root = Directory.GetParent(Application.dataPath).FullName;
            List.Clear();

            foreach (var data in new DirectoryInfo(Root).GetFiles("*.asset.json", SearchOption.AllDirectories))
            {
                var filename = data.Name;
                PluginsInfoJson json;
                if (!List.ContainsKey(filename))
                {
                    try
                    {
                        json = JsonUtility.FromJson<PluginsInfoJson>(File.ReadAllText(data.FullName));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Path : {0}, Error : {1}", data.FullName, e);
                        continue;
                    }

                    List.Add(filename, json);
                    ListStauts.Add(filename, PluginsInfoEditor.GetValidDir(Root, json.TargetRelativePath).Exists);
                }
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("插件安装管理", new GUIStyle("PreLabel"));

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("卸载全部", GUILayout.Width(100), GUILayout.Height(20)))
            {
                _ = PluginsInfoEditor.UnInitialize(List.Values.Where(plugin => plugin != null));
            }

            if (GUILayout.Button("安装全部", GUILayout.Width(100), GUILayout.Height(20)))
            {
                _ = PluginsInfoEditor.Initialize(List.Values.Where(plugin => plugin != null));
            }

            EditorGUILayout.EndHorizontal();

            Vector = EditorGUILayout.BeginScrollView(Vector);
            EditorGUILayout.BeginVertical();
            foreach (var plugin in List.Where(plugin => plugin.Value != null))
            {
                EditorGUILayout.BeginHorizontal("IN ThumbnailShadow", GUILayout.Height(25));
                EditorGUILayout.PrefixLabel(plugin.Value.Name);
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField(plugin.Value.MacroDefinition);
                EditorGUILayout.Separator();
                if (ListStauts[plugin.Key])
                {
                    if (GUILayout.Button("卸载", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        _ = PluginsInfoEditor.UnInitialize(plugin.Value);
                    }
                }
                else
                {
                    if (GUILayout.Button("安装", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        _ = PluginsInfoEditor.Initialize(plugin.Value);
                    }
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
    }
}