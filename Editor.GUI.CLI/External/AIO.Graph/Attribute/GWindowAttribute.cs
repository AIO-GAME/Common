/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GWindowAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public GUIContent Title { get; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public uint MaxSizeWidth = 0;

        /// <summary>
        /// 最大高度
        /// </summary>
        public uint MaxSizeHeight = 0;

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Menu;

        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int MenuPriority;

        /// <summary>
        /// 菜单验证函数 返回值为bool
        /// </summary>
        public MethodInfo MenuValidate;

        /// <summary>
        /// 最大宽高
        /// </summary>
        public Vector2 MaxSize => new Vector2(MaxSizeWidth, MaxSizeHeight);

        /// <summary>
        /// 最小宽度
        /// </summary>
        public uint MinSizeWidth = 0;

        /// <summary>
        /// 最小高度
        /// </summary>
        public uint MinSizeHeight = 0;

        /// <summary>
        /// 最小宽高
        /// </summary>
        public Vector2 MinSize => new Vector2(MinSizeWidth, MinSizeHeight);

        /// <summary>
        /// 组
        /// </summary>
        public string Group;

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order;

        /// <summary>
        /// 运行时 窗口类型
        /// </summary>
        public Type RuntimeType;

        /// <inheritdoc />
        public GWindowAttribute(string title) : base(title)
        {
            Title = new GUIContent(title);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, Texture texture) : base(title)
        {
            Title = new GUIContent(title, texture);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, Texture image, string tooltip) : base(title)
        {
            Title = new GUIContent(title, image, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(Texture texture, string tooltip) : base(tooltip)
        {
            Title = new GUIContent(texture, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, string tooltip) : base(title)
        {
            Title = new GUIContent(title, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(GUIContent title) : base(title.text)
        {
            Title = title;
        }
    }

    public partial class GraphicWindow
    {
        private static Dictionary<string, GWindowAttribute> WindowTypes { get; }

        private static SettingsProvider provider;

        /// <summary>
        /// 组列表
        /// </summary>
        private static Dictionary<string, List<Type>> GroupTabel { get; }

        static GraphicWindow()
        {
            GroupTabel = new Dictionary<string, List<Type>> { { "Default", new List<Type>() } };
            WindowTypes = new Dictionary<string, GWindowAttribute>();

            var graphicType = typeof(GraphicWindow);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsSubclassOf(graphicType)) continue;
                    var attribute = type.GetCustomAttribute<GWindowAttribute>(false);
                    if (attribute is null) GroupTabel["Default"].Add(type);
                    else
                    {
                        var key = string.Format("{0}{1}", attribute.Title, type.FullName);
                        if (WindowTypes.ContainsKey(key)) continue;
                        if (string.IsNullOrEmpty(attribute.Group)) attribute.Group = "Default";
                        attribute.RuntimeType = type;
                        WindowTypes.Add(key, attribute);
                        if (!GroupTabel.ContainsKey(attribute.Group)) GroupTabel[attribute.Group] = new List<Type>();
                        GroupTabel[attribute.Group].Add(type);
                    }
                }
            }

            var containerWindowType = Assembly.GetAssembly(typeof(EditorWindow))
                .GetType("UnityEditor.PreferenceSettingsWindow");
            foreach (var item in GroupTabel)
                item.Value.Add(containerWindowType);
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            if (provider != null) return provider;

            provider = new GraphicSettingsProvider(
                string.Format("{0}/{1}", nameof(AIO), nameof(GWindowAttribute).Replace(nameof(Attribute), "")),
                SettingsScope.User);
            provider.label = "Windows Header";
            provider.hasSearchInterestHandler = (value) =>
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
                EditorGUILayout.BeginVertical();
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
                {
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
                                GroupTabel[window.Value.Group]);
                        EditorGUILayout.LabelField("|", GUILayout.Width(10));
                        EditorGUILayout.LabelField(window.Value.RuntimeType.FullName, GUILayout.ExpandWidth(true));
                    }
                }

                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();

                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("Version 1.0.1-preview", EditorStyles.centeredGreyMiniLabel);
            };
            return provider;
        }
    }
}