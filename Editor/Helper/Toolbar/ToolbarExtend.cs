/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-29
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if !UNITY_2019_1_OR_NEWER
using UnityEngine.Experimental.UIElements;
#else
using System.Threading.Tasks;
#endif


namespace AIO.UEditor
{
    public static class ToolbarExtend
    {
        private static readonly Type TOOLBAR_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

#if !UNITY_2021_1_OR_NEWER
        private static readonly Type containterType = typeof(IMGUIContainer);

        private static ScriptableObject ms_CurrentToolbar;

        private static readonly FieldInfo ONGUI_HANDLER_FIELDINFO = containterType.GetField("m_OnGUIHandler",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

#if UNITY_2020_1_OR_NEWER
        private static readonly Type GUIVIEW_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");

        private static readonly Type backendType =
            typeof(Editor).Assembly.GetType("UnityEditor.IWindowBackend");

        private static readonly PropertyInfo guiBackend = GUIVIEW_TYPE.GetProperty("windowBackend",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly PropertyInfo VISUALTREE_PROPERTYINFO = backendType.GetProperty("visualTree",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

#else
        private static readonly Type GUIVIEW_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");
        
        private static readonly PropertyInfo VISUALTREE_PROPERTYINFO = GUIVIEW_TYPE.GetProperty("visualTree",
           BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
#endif

#endif


        [AInit(Mode = EInitAttrMode.Both, Order = -1)]
        private static async void Init()
        {
#if UNITY_2021_1_OR_NEWER
            var toolbars = Resources.FindObjectsOfTypeAll(TOOLBAR_TYPE);
            if (toolbars is null || toolbars.Length <= 0) return;
            while (true)
            {
                var toolbar = TOOLBAR_TYPE.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(toolbars[0]) as VisualElement;

                if (toolbar is null)
                {
                    await Task.Delay(200);
                    continue;
                }

                var temp = toolbar
#if UNITY_2022_1_OR_NEWER && !UNITY_2023_1_OR_NEWER
                    .Q<VisualElement>("ToolbarContainerContent")
#endif
                    .Q<VisualElement>("ToolbarZonePlayMode")
                    .Q<VisualElement>("PlayMode")
                    .Children()
                    .First();

                if (temp is null) break;

                if (Application.isEditor)
                {
                    foreach (var lnk in from lnk in LnkToolsHelper.Data
                             where lnk.ShowMode == ELnkShowMode.Toolbar
                             where lnk.Mode == ELnkToolsMode.OnlyEditor || lnk.Mode == ELnkToolsMode.AllMode
                             select lnk)
                    {
                        temp.Add(LnkToolOverlay.GetVoid(lnk));
                    }
                }
                else if (Application.isPlaying)
                {
                    foreach (var lnk in from lnk in LnkToolsHelper.Data
                             where lnk.ShowMode == ELnkShowMode.Toolbar
                             where lnk.Mode == ELnkToolsMode.OnlyRuntime || lnk.Mode == ELnkToolsMode.AllMode
                             select lnk)
                    {
                        temp.Add(LnkToolOverlay.GetVoid(lnk));
                    }
                }

                break;
            }
#else
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
#endif
        }

#if !UNITY_2021_1_OR_NEWER
        private static void OnUpdate()
        {
            if (ms_CurrentToolbar != null) return;
            var toolbars = Resources.FindObjectsOfTypeAll(TOOLBAR_TYPE);
            ms_CurrentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
            if (ms_CurrentToolbar is null) return;
#if UNITY_2020_1_OR_NEWER
            var backend = guiBackend.GetValue(ms_CurrentToolbar);
            var elements = VISUALTREE_PROPERTYINFO.GetValue(backend, null) as VisualElement;
#else
            var elements = VISUALTREE_PROPERTYINFO.GetValue(ms_CurrentToolbar, null) as VisualElement;
#endif
            if (elements is null || elements.childCount == 0) return;
            if (elements[0] is null) return;
            var handler = ONGUI_HANDLER_FIELDINFO.GetValue(elements[0]) as Action;
            handler -= OnGUI;
            handler += OnGUI;
            ONGUI_HANDLER_FIELDINFO.SetValue(elements[0], handler);
        }

        private static void OnGUI()
        {
            var rect = new Rect(0, 5, 40, 24);
#if UNITY_2020_1_OR_NEWER
            rect.x = Screen.width / 2f - 110;
#else
            rect.x = Screen.width / 2f - 110;
#endif
            if (Application.isEditor)
            {
                foreach (var lnk in from lnk in LnkToolsHelper.Data
                         where lnk.ShowMode == ELnkShowMode.Toolbar
                         where lnk.Mode == ELnkToolsMode.OnlyEditor || lnk.Mode == ELnkToolsMode.AllMode
                         select lnk)
                {
                    if (GUI.Button(rect, lnk.Content, GEStyle.TEtoolbarbutton))
                    {
                        lnk.Invoke();
                    }

                    rect.x -= rect.width + 1;
                }
            }
            else if (Application.isPlaying)
            {
                foreach (var lnk in from lnk in LnkToolsHelper.Data
                         where lnk.ShowMode == ELnkShowMode.Toolbar
                         where lnk.Mode == ELnkToolsMode.OnlyRuntime || lnk.Mode == ELnkToolsMode.AllMode
                         select lnk)
                {
                    if (GUI.Button(rect, lnk.Content, GEStyle.TEtoolbarbutton))
                    {
                        lnk.Invoke();
                    }

                    rect.x -= rect.width + 1;
                }
            }
        }
#endif
    }
}