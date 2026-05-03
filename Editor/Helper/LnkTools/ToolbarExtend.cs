using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#region

#if !UNITY_2019_1_OR_NEWER
using UnityEngine.Experimental.UIElements;
#elif UNITY_2021_1_OR_NEWER
using System.Threading.Tasks;
#endif

#endregion

namespace AIO.UEditor
{
    public static class ToolbarExtend
    {
        private static readonly Type TOOLBAR_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

#if !UNITY_2021_1_OR_NEWER
        private static readonly Type containterType = typeof(IMGUIContainer);

        private static ScriptableObject ms_CurrentToolbar;

        private static readonly FieldInfo ONGUI_HANDLER_FIELDINFO =
            containterType.GetField("m_OnGUIHandler", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

#if UNITY_2020_1_OR_NEWER
        private static readonly Type GUIVIEW_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");

        private static readonly Type backendType =
            typeof(Editor).Assembly.GetType("UnityEditor.IWindowBackend");

        private static readonly PropertyInfo guiBackend =
            GUIVIEW_TYPE.GetProperty("windowBackend", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly PropertyInfo VISUALTREE_PROPERTYINFO =
            backendType.GetProperty("visualTree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

#else
        private static readonly Type GUIVIEW_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");

        private static readonly PropertyInfo VISUALTREE_PROPERTYINFO = GUIVIEW_TYPE.GetProperty("visualTree",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
#endif

#endif

        private static Dictionary<GUIContent, VisualElement> toolbarElements =
            new Dictionary<GUIContent, VisualElement>();

        private static bool isInitialized;

        [InitializeOnLoadMethod]
        private static void Install()
        {
            isInitialized               =  false;
            EditorApplication.delayCall -= Initialize;
            EditorApplication.delayCall += Initialize;
        }

#if UNITY_2021_1_OR_NEWER
        private static async void Initialize()
#else
        private static void Initialize()
#endif
        {
            if (isInitialized) return;
#if UNITY_2021_1_OR_NEWER
            var toolbars = Resources.FindObjectsOfTypeAll(TOOLBAR_TYPE);
            if (toolbars is null || toolbars.Length <= 0) return;
            while (true)
            {
                var toolbar = TOOLBAR_TYPE.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(toolbars[0]) as VisualElement;
                var temp = toolbar?.
#if UNITY_2022_1_OR_NEWER && !UNITY_2023_1_OR_NEWER
                           Q<VisualElement>("ToolbarContainerContent").
#endif
                           Q<VisualElement>("ToolbarZonePlayMode").
                           Q<VisualElement>("PlayMode").
                           Children().
                           First();
                if (temp is null)
                {
                    await Task.Delay(100);
                    continue;
                }

                var isPlayMode = Application.isPlaying;
                var isEditor   = Application.isEditor;
                foreach (var lnk in Data)
                {
                    switch (lnk.RuntimeMode)
                    {
                        default:
                        case ELnkToolsMode.AllMode: break;
                        case ELnkToolsMode.OnlyEditor when !isEditor:
                        case ELnkToolsMode.OnlyRuntime when !isPlayMode:
                        case ELnkToolsMode.NoMode:
                            continue;
                    }

                    switch (lnk.ShowMode)
                    {
                        default:
                        case ELnkShowMode.SceneView: continue;
                        case ELnkShowMode.ToolbarLeft:
                        case ELnkShowMode.ToolbarRight:
                            break;
                    }

                    if (toolbarElements.TryGetValue(lnk.Content, out var value) && value != null) continue;

                    var element = toolbarElements[lnk.Content] = LnkToolOverlay.GetVoid(lnk);
                    switch (lnk.ShowMode)
                    {
                        case ELnkShowMode.ToolbarLeft:
                            temp.Insert(0, element);
                            break;
                        case ELnkShowMode.ToolbarRight:
                            temp.Add(element);
                            break;
                        default:
                        case ELnkShowMode.SceneView: break;
                    }
                }

                break;
            }
#else
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
#endif
            isInitialized = true;
        }

#if !UNITY_2021_1_OR_NEWER
        private static void OnUpdate()
        {
            if (ms_CurrentToolbar) return;
            var toolbars = Resources.FindObjectsOfTypeAll(TOOLBAR_TYPE);
            ms_CurrentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
            if (!ms_CurrentToolbar) return;
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
                                    where lnk.ShowMode == ELnkShowMode.ToolbarLeft || lnk.ShowMode == ELnkShowMode.ToolbarRight
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
                                    where lnk.ShowMode == ELnkShowMode.ToolbarLeft || lnk.ShowMode == ELnkShowMode.ToolbarRight
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

        private static readonly Lazy<List<LnkToolDataInternal>> Lazy = new Lazy<List<LnkToolDataInternal>>(GetLnkTools);

        internal static List<LnkToolDataInternal> AddData => Lazy.Value;

        internal static IReadOnlyList<LnkToolDataInternal> Data => Lazy.Value;

        private static List<LnkToolDataInternal> GetLnkTools()
        {
            var list  = Pool.List<LnkToolDataInternal>();
            var types = Pool.List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var info = assembly.GetName();
                if (!info.Name.Contains("Editor", StringComparison.InvariantCultureIgnoreCase)) continue;
                if (info.Name.Contains("UnityEngine", StringComparison.InvariantCultureIgnoreCase)) continue;
                if (info.Name.Contains("UnityEditor", StringComparison.InvariantCultureIgnoreCase)) continue;
                types.AddRange(assembly.GetTypes().Where(type => !type.IsEnum).Where(type => !type.IsInterface));
            }

            try
            {
                foreach (var method in types.Select(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).
                                             SelectMany(methods => methods, (methods, method) => new
                                             {
                                                 methods,
                                                 method
                                             }).
                                             Where(t => !t.method.IsConstructor).
                                             Where(t => t.method.IsDefined(typeof(LnkToolsAttribute), false)).
                                             Where(t => t.method.ReturnType == typeof(bool) || t.method.ReturnType == typeof(void)).
                                             Select(t => t.method))
                    list.Add(new LnkToolDataInternal(method));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            list.Sort((x, y) =>
            {
                if (x.Priority < y.Priority) return -1;
                return x.Priority == y.Priority ? 0 : 1;
            });
            return list;
        }
    }
}