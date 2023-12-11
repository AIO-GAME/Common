/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 快捷工具箱
    /// </summary>
    [InitializeOnLoad]
    internal static partial class LnkToolsHelper
    {
        private static bool IsEnableLnkTools;
        private static List<LnkTools> LnkToolList;
        private static float Height;
        private static GUIContent Content_Thumbnail;
        private static GUIContent Content_Restore;

        /// <summary>
        /// 缩略图
        /// </summary>
        private static bool Thumbnail;

        static LnkToolsHelper()
        {
            OnInitLnkTools();
        }

        /// <summary>
        /// 初始化快捷工具箱
        /// </summary>
        private static void OnInitLnkTools()
        {
            Content_Thumbnail = EditorGUIUtility.IconContent("ArrowNavigationLeft");
            Content_Thumbnail.tooltip = "Thumbnail";
            Content_Restore = EditorGUIUtility.IconContent("ArrowNavigationRight");
            Content_Restore.tooltip = "Restore";

            LnkToolList = new List<LnkTools>();
            Thumbnail = EditorPrefs.GetBool("Thumbnail", false);
            IsEnableLnkTools = EditorPrefs.GetBool(EditorPrefsTable.LnkTools_Enable, true);
            if (!IsEnableLnkTools) return;

            LnkToolList.Clear();
            var types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().Name.Contains("Editor")) continue;
                types.AddRange(assembly.GetTypes());
            }

            foreach (var method in
                     from type in types
                     select type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                     into methods
                     from method in methods
                     where method.IsDefined(typeof(LnkToolsAttribute), false)
                     select method) LnkToolList.Add(new LnkTools(method));

            LnkToolList.Sort((x, y) =>
            {
                if (x.Priority < y.Priority) return -1;
                return x.Priority == y.Priority ? 0 : 1;
            });

            Height = (LnkToolList.Count + 1) * 22 + 3;

            SceneView.duringSceneGui += OnLnkToolsGUI;
        }

        /// <summary>
        /// 快捷工具箱界面
        /// </summary>
        private static void OnLnkToolsGUI(SceneView sceneView)
        {
            Handles.BeginGUI();

            var rect = Rect.zero;
            var x = sceneView.position.width - 45;
            var y = sceneView.in2DMode ? 5 : sceneView.position.height / 2 - Height / 2;

            rect.Set(sceneView.position.width - 10, y, 10, 20);
            if (GUI.Button(rect, Thumbnail ? Content_Thumbnail : Content_Restore, "InvisibleButton"))
            {
                Thumbnail = !Thumbnail;
                EditorPrefs.SetBool("Thumbnail", Thumbnail);
            }


            if (!Thumbnail)
            {
                var height = 22 + 3 - Height / 2;
                foreach (var lnk in LnkToolList)
                {
                    switch (lnk.Mode)
                    {
                        case LnkToolsMode.OnlyRuntime:
                            GUI.enabled = EditorApplication.isPlaying;
                            break;
                        case LnkToolsMode.OnlyEditor:
                            GUI.enabled = !EditorApplication.isPlaying;
                            break;
                        case LnkToolsMode.AllMode:
                        default:
                            GUI.enabled = true;
                            break;
                    }

                    rect.Set(x, y + height, 30, 20);
                    height += 22;

                    GUI.backgroundColor = lnk.BGColor;
                    if (GUI.Button(rect, lnk.Content))
                    {
                        lnk.Method.Invoke(null, null);
                    }

                    GUI.backgroundColor = Color.white;
                }
            }

            GUI.enabled = true;

            Handles.EndGUI();
        }

        private class LnkTools
        {
            public GUIContent Content { get; set; }
            public Color BGColor { get; set; }
            public LnkToolsMode Mode { get; set; }
            public int Priority { get; set; }
            public MethodInfo Method { get; set; }

            public LnkTools(MethodInfo method)
            {
                var attribute = method.GetCustomAttribute<LnkToolsAttribute>();
                var icon = EditorGUIUtility.IconContent(attribute.BuiltinIcon);
                Content = new GUIContent
                {
                    image = icon?.image,
                    tooltip = attribute.Tooltip
                };
                BGColor = new Color(attribute.R, attribute.G, attribute.B, attribute.A);
                Mode = attribute.Mode;
                Priority = attribute.Priority;
                Method = method;
            }
        }
    }
}