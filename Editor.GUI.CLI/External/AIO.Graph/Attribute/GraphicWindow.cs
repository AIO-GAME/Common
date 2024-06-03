#region

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public partial class GraphicWindow
    {
        private static SettingsProvider provider;

        static GraphicWindow()
        {
            GroupTable  = new Dictionary<string, List<Type>> { { "Default", new List<Type>() } };
            WindowTypes = new Dictionary<string, GWindowAttribute>();

            var graphicType = typeof(GraphicWindow);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract || !type.IsSubclassOf(graphicType)) continue;
                var attribute = type.GetCustomAttribute<GWindowAttribute>(false);
                if (attribute is null)
                {
                    GroupTable["Default"].Add(type);
                }
                else
                {
                    var key = $"{attribute.Title.text}{type.FullName}";
                    if (WindowTypes.ContainsKey(key)) continue;
                    if (string.IsNullOrEmpty(attribute.Group)) attribute.Group = "Default";
                    attribute.RuntimeType = type;
                    WindowTypes.Add(key, attribute);
                    if (!GroupTable.ContainsKey(attribute.Group)) GroupTable[attribute.Group] = new List<Type>();
                    GroupTable[attribute.Group].Add(type);
                }
            }

            var containerWindowType = Assembly.GetAssembly(typeof(EditorWindow)).GetType("UnityEditor.PreferenceSettingsWindow");
            foreach (var item in GroupTable)
                item.Value.Add(containerWindowType);
        }

        private static Dictionary<string, GWindowAttribute> WindowTypes { get; }

        /// <summary>
        /// 组列表
        /// </summary>
        private static Dictionary<string, List<Type>> GroupTable { get; }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            if (provider != null) return provider;

            provider = new GraphicSettingsProvider(
                $"{nameof(AIO)}/{nameof(GWindowAttribute).Replace(nameof(Attribute), "")}",
                SettingsScope.User);
            provider.label = "Windows Header";
            provider.hasSearchInterestHandler = value =>
            {
                if (value.Contains("Window")) return true;
                if (value.Contains("Header")) return true;
                if (value.Contains("GUI")) return true;
                if (value.Contains("AIO")) return true;
                return false;
            };
            provider.keywords = new HashSet<string>(new[] { "Window", "Header", "GUI", "AIO" });
            provider.guiHandler = delegate
            {
                using (var scope = new EditorGUILayout.VerticalScope())
                {
                    EditorGUILayout.Space();

                    using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        EditorGUILayout.LabelField("Group", new GUIStyle("CenteredLabel"), GUILayout.Width(50));
                        EditorGUILayout.LabelField("|", GUILayout.Width(10));
                        EditorGUILayout.LabelField("Order", new GUIStyle("CenteredLabel"), GUILayout.Width(50));
                        EditorGUILayout.LabelField("|", GUILayout.Width(10));
                        EditorGUILayout.LabelField("Title", new GUIStyle("CenteredLabel"), GUILayout.Width(200));
                        EditorGUILayout.LabelField("|", GUILayout.Width(10));
                        EditorGUILayout.LabelField("Status", new GUIStyle("CenteredLabel"), GUILayout.Width(50));
                        EditorGUILayout.LabelField("|", GUILayout.Width(10));
                        EditorGUILayout.LabelField("Class", new GUIStyle("CenteredLabel"), GUILayout.ExpandWidth(true));
                    }

                    foreach (var window in WindowTypes)
                        using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                        {
                            EditorGUILayout.LabelField(window.Value.Group, new GUIStyle("CenteredLabel"),
                                                       GUILayout.Width(50));
                            EditorGUILayout.LabelField("|", GUILayout.Width(10));
                            EditorGUILayout.LabelField(window.Value.Order.ToString(), new GUIStyle("CenteredLabel"),
                                                       GUILayout.Width(50));
                            EditorGUILayout.LabelField("|", GUILayout.Width(10));
                            EditorGUILayout.LabelField(window.Value.Title, new GUIStyle("CenteredLabel"),
                                                       GUILayout.Width(200));
                            EditorGUILayout.LabelField("|", GUILayout.Width(10));
                            if (GUILayout.Button("Open", GUILayout.Width(50)))
                                EHelper.Window.Open(window.Value.RuntimeType, window.Value.Title,
                                                    GroupTable[window.Value.Group]);
                            EditorGUILayout.LabelField("|", GUILayout.Width(10));
                            EditorGUILayout.LabelField(window.Value.RuntimeType.FullName, GUILayout.ExpandWidth(true));
                        }

                    EditorGUILayout.Space();
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField($"Version {Setting.Version}", EditorStyles.centeredGreyMiniLabel);
            };
            return provider;
        }
    }
}