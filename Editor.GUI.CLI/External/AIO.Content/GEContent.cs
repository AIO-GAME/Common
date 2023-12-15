/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-15
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 图标
    /// </summary>
    public static class GEContent
    {
        internal static readonly Dictionary<string, GUIContent> GCSetting = new Dictionary<string, GUIContent>();
        internal static readonly Dictionary<string, GUIContent> GCApp = new Dictionary<string, GUIContent>();
        internal static readonly Dictionary<string, GUIContent> GCBuiltin = new Dictionary<string, GUIContent>();

        public static GUIContent GetSetting(string name)
        {
            if (GCSetting.ContainsKey(name)) return GCSetting[name];
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
            if (GCBuiltin.ContainsKey(name)) return GCBuiltin[name];
            GCBuiltin[name] = EditorGUIUtility.IconContent(name);
            return GCBuiltin[name];
        }

        public static GUIContent NewSetting(string name, string tooltip)
        {
            if (!GCSetting.ContainsKey(name))
                GCSetting[name] = new GUIContent(Resources.Load<Texture>(string.Format("Icon/Setting/{0}", name)));
            return new GUIContent(GCSetting[name].image, tooltip);
        }

        public static GUIContent NewSetting(string name)
        {
            if (!GCSetting.ContainsKey(name))
                GCSetting[name] = new GUIContent(Resources.Load<Texture>(string.Format("Icon/Setting/{0}", name)));
            return new GUIContent(GCSetting[name].image);
        }

        public static GUIContent NewApp(string name)
        {
            if (!GCApp.ContainsKey(name))
                GCApp[name] = new GUIContent(Resources.Load<Texture>(string.Format("Icon/App/{0}", name)));
            return new GUIContent(GCApp[name].image);
        }

        public static GUIContent NewApp(string name, string tooltip)
        {
            if (!GCApp.ContainsKey(name))
                GCApp[name] = new GUIContent(Resources.Load<Texture>(string.Format("Icon/App/{0}", name)));
            return new GUIContent(GCApp[name].image, tooltip);
        }

        public static GUIContent NewBuiltin(string name)
        {
            if (!GCBuiltin.ContainsKey(name))
                GCBuiltin[name] = EditorGUIUtility.IconContent(name);
            return new GUIContent(GCBuiltin[name].image);
        }

        public static GUIContent NewBuiltin(string name, string tooltip)
        {
            if (!GCBuiltin.ContainsKey(name))
                GCBuiltin[name] = EditorGUIUtility.IconContent(name);
            return new GUIContent(GCBuiltin[name].image, tooltip);
        }

        public static void LoadSetting()
        {
            GCSetting.Clear();
            foreach (var texture in Resources.LoadAll<Texture2D>("Icon/Setting"))
            {
                GCSetting[texture.name] = new GUIContent(texture);
            }
        }

        public static void LoadApp()
        {
            GCApp.Clear();
            foreach (var texture in Resources.LoadAll<Texture2D>("Icon/App"))
            {
                GCApp[texture.name] = new GUIContent(texture);
            }
        }
    }
}