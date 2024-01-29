/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-29
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;

#else
using UnityEngine.Experimental.UIElements;
#endif
namespace AIO.UEditor
{
    [InitializeOnLoad]
    public static class ToolbarExtend
    {
        private static readonly Type containterType = typeof(IMGUIContainer);
        private static readonly Type TOOLBAR_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static readonly Type GUIVIEW_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");
#if UNITY_2020_1_OR_NEWER
        private static readonly Type backendType =
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.IWindowBackend");

        private static readonly PropertyInfo guiBackend = GUIVIEW_TYPE.GetProperty("windowBackend",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly PropertyInfo VISUALTREE_PROPERTYINFO = backendType.GetProperty("visualTree",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

#else
        private static readonly PropertyInfo VISUALTREE_PROPERTYINFO = GUIVIEW_TYPE.GetProperty("visualTree",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

#endif

        private static readonly FieldInfo ONGUI_HANDLER_FIELDINFO = containterType.GetField("m_OnGUIHandler",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        private static int ms_ToolIconCount;
        private static GUIStyle ms_CommandStyle;
        private static GUIStyle ms_CommandButtonStyle;

        private static ScriptableObject ms_CurrentToolbar;
        private const string START_GAME_OPTION = "START_GAME_OPTION";
        private static string startSceneName = "Launch";

        /// <summary>
        /// 游戏启动时调用（仅只一次）
        /// </summary>
#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnStartGame()
        {
            var scene = SceneManager.GetActiveScene();
            if (!scene.name.Equals(startSceneName))
            {
                if (EditorPrefs.GetBool(START_GAME_OPTION))
                {
                    // SceneManager.LoadScene(startSceneName);
                }
            }

            EditorPrefs.SetBool(START_GAME_OPTION, false);
            AssetDatabase.Refresh();
        }
#endif

        static ToolbarExtend()
        {
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }

        public static GUIStyle GetCommandButtonStyle()
        {
            return ms_CommandButtonStyle;
        }

        private static void OnUpdate()
        {
            if (ms_CurrentToolbar != null) return;
            var toolbars = Resources.FindObjectsOfTypeAll(TOOLBAR_TYPE);
            ms_CurrentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
            if (ms_CurrentToolbar == null) return;

#if UNITY_2020_1_OR_NEWER
            var backend = guiBackend.GetValue(ms_CurrentToolbar);
            var elements = VISUALTREE_PROPERTYINFO.GetValue(backend, null) as VisualElement;
#else
            var elements = VISUALTREE_PROPERTYINFO.GetValue(ms_CurrentToolbar, null) as VisualElement;
#endif

            if (elements is null) return;

#if UNITY_2019_1_OR_NEWER
            var container = elements[0];
#else
            var container = elements[0] as IMGUIContainer;
#endif
            if (container is null) return;

            var handler = ONGUI_HANDLER_FIELDINFO.GetValue(container) as Action;
            handler -= OnGUI;
            handler += OnGUI;
            ONGUI_HANDLER_FIELDINFO.SetValue(container, handler);
        }

        private static void OnGUI()
        {
            var rect = new Rect(400, 5, 40, 25);
            if (Application.isEditor)
            {
                foreach (var lnk in from lnk in LnkToolsHelper.Data
                         let temp = lnk.Mode == LnkToolsMode.OnlyEditor || lnk.Mode == LnkToolsMode.AllMode
                         where temp
                         select lnk)
                {
                    if (GUI.Button(rect, lnk.Content, GEStyle.TEtoolbarbutton))
                    {
                        lnk.Invoke();
                    }

                    rect.x += rect.width + 1;
                }
            }
            else if (Application.isPlaying)
            {
                foreach (var lnk in from lnk in LnkToolsHelper.Data
                         let temp = lnk.Mode == LnkToolsMode.OnlyRuntime || lnk.Mode == LnkToolsMode.AllMode
                         where temp
                         select lnk)
                {
                    if (GUI.Button(rect, lnk.Content, GEStyle.TEtoolbarbutton))
                    {
                        lnk.Invoke();
                    }

                    rect.x += rect.width +  1;
                }
            }
        }
    }
}
#endif