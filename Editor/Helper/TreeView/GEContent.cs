#region namespace

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#endregion

namespace AIO
{
    /// <summary>
    /// 图标
    /// </summary>
    public static class GEContent
    {
        internal static readonly Dictionary<string, GUIContent> GCApp     = new Dictionary<string, GUIContent>();
        internal static readonly Dictionary<string, GUIContent> GCBuiltin = new Dictionary<string, GUIContent>();
        internal static readonly Dictionary<string, GUIContent> GCSetting = new Dictionary<string, GUIContent>();
        internal static readonly Dictionary<string, GUIContent> GCTemp    = new Dictionary<string, GUIContent>();

        /// <summary>
        /// 修改提示
        /// </summary>
        public static GUIContent SetTooltips(this GUIContent content, string tooltip)
        {
            content.tooltip = tooltip;
            if (string.IsNullOrEmpty(content.text)) return content;
            if (!GCTemp.ContainsKey(content.text))
                GCTemp[content.text] = content;
            return content;
        }

        public static GUIContent GetSetting(string name)
        {
            if (GCSetting.TryGetValue(name, out var setting)) return setting;
            GCSetting[name] = new GUIContent(Resources.Load<Texture>(name));
            return GCSetting[name];
        }

        public static GUIContent GetApp(string name)
        {
            if (!GCApp.ContainsKey(name)) return GCApp[name];
            GCApp[name] = new GUIContent(Resources.Load<Texture>(name));
            return GCApp[name];
        }

        public static GUIContent GetBuiltin(string name)
        {
            if (GCBuiltin.TryGetValue(name, out var builtin)) return builtin;
            GCBuiltin[name] = EditorGUIUtility.IconContent(name);
            return GCBuiltin[name];
        }

        public static GUIContent NewSetting(string name, string tooltip)
        {
            if (!GCSetting.ContainsKey(name))
                GCSetting[name] = EditorGUIUtility.TrIconContent(Resources.Load<Texture>($"Editor/Icon/Setting/{name}"), tooltip);
            return EditorGUIUtility.TrIconContent(GCSetting[name].image, tooltip);
        }

        public static GUIContent NewSettingCustom(string path, string tooltip)
        {
            if (!GCSetting.ContainsKey(path))
                GCSetting[path] = EditorGUIUtility.TrIconContent(Resources.Load<Texture>(path));
            return EditorGUIUtility.TrIconContent(GCSetting[path].image, tooltip);
        }

        public static GUIContent NewSetting(string name)
        {
            if (!GCSetting.ContainsKey(name))
                GCSetting[name] = EditorGUIUtility.TrIconContent(Resources.Load<Texture>($"Editor/Icon/Setting/{name}"));
            return EditorGUIUtility.TrIconContent(GCSetting[name].image);
        }

        public static GUIContent NewApp(string name)
        {
            if (!GCApp.ContainsKey(name))
                GCApp[name] = new GUIContent(Resources.Load<Texture>($"Editor/Icon/App/{name}"));
            return EditorGUIUtility.TrIconContent(GCApp[name].image);
        }

        public static GUIContent NewApp(string name, string tooltip)
        {
            if (!GCApp.ContainsKey(name))
                GCApp[name] = EditorGUIUtility.TrIconContent(Resources.Load<Texture>($"Editor/Icon/App/{name}"));
            return EditorGUIUtility.TrIconContent(GCApp[name].image, tooltip);
        }

        public static GUIContent NewBuiltin(string name)
        {
            if (!GCBuiltin.ContainsKey(name))
                GCBuiltin[name] = EditorGUIUtility.IconContent(name);
            return EditorGUIUtility.TrIconContent(GCBuiltin[name].image);
        }

        public static GUIContent NewBuiltin(string name, string tooltip)
        {
            if (!GCBuiltin.ContainsKey(name))
                GCBuiltin[name] = EditorGUIUtility.IconContent(name);
            return EditorGUIUtility.TrIconContent(GCBuiltin[name].image, tooltip);
        }

        public static void LoadSetting()
        {
            GCSetting.Clear();
            foreach (var texture in Resources.LoadAll<Texture2D>("Editor/Icon/Setting")) GCSetting[texture.name] = new GUIContent(texture);
        }

        public static void LoadApp()
        {
            GCApp.Clear();
            foreach (var texture in Resources.LoadAll<Texture2D>("Editor/Icon/App")) GCApp[texture.name] = new GUIContent(texture);
        }
    }
}