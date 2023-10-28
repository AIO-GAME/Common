using UnityEditor;
using UnityEngine;

#pragma warning disable CS1591
namespace AIO
{
    internal partial class Plugins
    {
        private static PluginDataWindow Window;

        [MenuItem("AIO/Window/Plugin Data Manager #_F12")]
        public static void Open()
        {
            if (Window is null)
            {
                Window = ScriptableObject.CreateInstance<PluginDataWindow>();
                Window.titleContent = new GUIContent("插件管理界面", "Plugin Data Manager");
                Window.minSize = new Vector2(200, 600);
            }

            Window.Show(true);
            Window.Focus();
            Window.Repaint();
        }

        public static void Close()
        {
            Window = null;
        }
    }
}