/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GWindowAttribute : Attribute
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
        public GWindowAttribute(string title)
        {
            Title = new GUIContent(title);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, Texture texture)
        {
            Title = new GUIContent(title, texture);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, Texture image, string tooltip)
        {
            Title = new GUIContent(title, image, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(Texture texture, string tooltip)
        {
            Title = new GUIContent(texture, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, string tooltip)
        {
            Title = new GUIContent(title, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(GUIContent title)
        {
            Title = title;
        }

        private static Dictionary<string, GWindowAttribute> windowTypes;

        private static SettingsProvider provider;

        private static Dictionary<string, List<Type>> windowDock;

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            if (provider != null) return provider;
            if (windowTypes == null) windowTypes = new Dictionary<string, GWindowAttribute>();
            if (windowDock == null) windowDock = new Dictionary<string, List<Type>> { { "Default", new List<Type>() } };

            var graphicType = typeof(GraphicWindow);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsSubclassOf(graphicType)) continue;
                    var attribute = type.GetCustomAttribute<GWindowAttribute>(false);
                    if (attribute is null) windowDock["Default"].Add(type);
                    else
                    {
                        var key = string.Format("{0}{1}", attribute.Title, type.FullName);
                        if (windowTypes.ContainsKey(key)) continue;
                        attribute.RuntimeType = type;
                        windowTypes.Add(key, attribute);
                        if (!windowDock.ContainsKey(attribute.Group)) windowDock[attribute.Group] = new List<Type>();
                        windowDock[attribute.Group].Add(type);
                    }
                }
            }
            //
            // var containerWindowType = Assembly.GetAssembly(typeof(EditorWindow))
            //     .GetType("UnityEditor.PreferenceSettingsWindow");
            // foreach (var item in windowDock) item.Value.Add(containerWindowType);

            provider = new GraphicSettingsProvider("AIO/Windows", SettingsScope.User);
            provider.label = "Windows Header";
            provider.guiHandler = delegate
            {
                GELayout.BeginVertical();
                GELayout.Space();

                GELayout.BeginHorizontal(GEStyle.HelpBox);
                GELayout.Label("Group", GEStyle.CenteredLabel, GTOption.Width(50));
                GELayout.Label("Order", GEStyle.CenteredLabel, GTOption.Width(50));
                GELayout.Label("Class", GEStyle.CenteredLabel, GTOption.WidthExpand(true));
                GELayout.Label("Title", GEStyle.CenteredLabel, GTOption.Width(200));
                GELayout.Label("Status", GEStyle.CenteredLabel, GTOption.Width(50));
                GELayout.EndHorizontal();

                foreach (var window in windowTypes)
                {
                    GELayout.BeginHorizontal(GEStyle.HelpBox);
                    GELayout.Label(window.Value.Group, GEStyle.CenteredLabel, GTOption.Width(50));
                    GELayout.Label(window.Value.Order, GEStyle.CenteredLabel, GTOption.Width(50));
                    GELayout.Label(window.Value.RuntimeType.FullName, GTOption.WidthExpand(true));
                    GELayout.Label(window.Value.Title, GEStyle.CenteredLabel, GTOption.Width(200));
                    if (GELayout.Button("Open", 50))
                        EHelper.Window.Open(window.Value.RuntimeType, window.Value.Title,
                            windowDock[window.Value.Group]);
                    GELayout.EndHorizontal();
                }

                GELayout.Space();
                GELayout.EndVertical();
            };
            return provider;
        }

        // internal static GUIContent GetLocalizedTitleContentFromType(System.Type t)
        // {
        //     EditorWindowTitleAttribute windowTitleAttribute = EditorWindow.GetEditorWindowTitleAttribute(t);
        //     if (windowTitleAttribute == null)
        //         return new GUIContent(t.Name);
        //     string str = "";
        //     if (!string.IsNullOrEmpty(windowTitleAttribute.icon))
        //         str = windowTitleAttribute.icon;
        //     else if (windowTitleAttribute.useTypeNameAsIconName)
        //         str = t.ToString();
        //     return !string.IsNullOrEmpty(str) && (bool) (UnityEngine.Object) EditorGUIUtility.LoadIcon(str) ? EditorGUIUtility.TrTextContentWithIcon(windowTitleAttribute.title, str) : EditorGUIUtility.TrTextContent(windowTitleAttribute.title);
        // }
    }
}