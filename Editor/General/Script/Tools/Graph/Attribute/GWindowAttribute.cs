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


        private static Dictionary<string, Type> windowTypes;
        private static List<string> windowTypeKeys;

        private static SettingsProvider provider;

        private static List<Type> windowDock = new List<Type>();

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            if (provider != null) return provider;
            if (windowTypes == null) windowTypes = new Dictionary<string, Type>();
            if (windowTypeKeys == null) windowTypeKeys = new List<string>();

            // var PreferenceSettingsWindowType = Assembly.GetAssembly(typeof(EditorWindow))
            //     .GetType("UnityEditor.PreferenceSettingsWindow");
            // windowTypeKeys
            // 获取当前程序集中所有带有GWindowAttribute特性的窗口类型
            var graphicType = typeof(GraphicWindow);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsSubclassOf(graphicType)) continue;
                    var attribute = type.GetCustomAttribute<GWindowAttribute>(false);
                    if (attribute is null)
                    {
                        windowDock.Add(type);
                    }
                    else
                    {
                        var key = string.Format("{0}\n{1}", attribute.Title, type.FullName);
                        if (windowTypes.ContainsKey(key)) continue;
                        windowTypes.Add(key, type);
                        windowTypeKeys.Add(key);
                        windowDock.Add(type);
                    }
                }
            }

            provider = new GraphicSettingsProvider("AIO/Windows", SettingsScope.User);
            provider.label = "Windows Header";
            provider.guiHandler = delegate
            {
                GELayout.BeginVertical();
                GELayout.Space();

                for (var i = 0; i < windowTypeKeys.Count; i++)
                {
                    if (i >= windowTypeKeys.Count) continue;
                    GELayout.BeginHorizontal(GEStyle.HelpBox);
                    var label = windowTypeKeys[i].Split('\n');
                    GELayout.Label(label[1], GTOption.WidthExpand(true));
                    GELayout.Label(label[0], GEStyle.CenteredLabel);
                    if (GELayout.Button("Open", 50))
                        EHelper.Window.Open(windowTypes[windowTypeKeys[i]], label[0], windowDock);
                    GELayout.EndHorizontal();
                }


                GELayout.Space();
                GELayout.EndVertical();
            };
            provider.keywords = new HashSet<string>(windowTypeKeys);
            return provider;
        }

        private static Dictionary<string, Type> _WindowTypes;
    }
}