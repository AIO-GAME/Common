/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-08
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#if UNITY_2021_1_OR_NEWER
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MonoHook;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace AIO.UEditor
{
    internal static partial class LnkToolsHelper
    {
        private static MethodHook _hook1;
        private static MethodHook _hook2;
        private const BindingFlags ToolBarBind = BindingFlags.Instance | BindingFlags.Public;
        private const BindingFlags ToolBarBindNon = BindingFlags.Instance | BindingFlags.NonPublic;

        private static void Init()
        {
            var type = typeof(LnkToolOverlay);

            if (_hook1 is null)
            {
                var miTarget = type.GetMethod("CreateHorizontalToolbarContent", ToolBarBind);
                var miReplacement = type.GetMethod("OnCreateHorizontalToolbarContent", ToolBarBind);

                _hook1 = new MethodHook(miTarget, miReplacement);
                _hook1.Install();
            }

            if (_hook2 is null)
            {
                var Target = type.GetMethod("CreateVerticalToolbarContent", ToolBarBind);
                var Replacement = type.GetMethod("OnCreateVerticalToolbarContent", ToolBarBind);
                _hook2 = new MethodHook(Target, Replacement);
                _hook2.Install();
            }
        }

        static LnkToolsHelper()
        {
            Init();
        }

        [Overlay(typeof(SceneView), "Lnk", true)]
        [Icon("Packages/com.aio.cli.asset/Resources/Setting/icon_option_button.png")]
        public class LnkToolOverlay : ToolbarOverlay
        {
            private Editor m_Editor;
            private VisualElement m_Content;
            public bool visible => content?.visible ?? false;

            private VisualElement content
            {
                get
                {
                    if (m_Content == null)
                        m_Content = new VisualElement()
                        {
                            name = "",
                            tooltip = "Lnk"
                        };
                    return m_Content;
                }
            }

            protected override Layout supportedLayouts
            {
                get
                {
                    if (m_Editor is null) return Layout.Panel;
                    var temp = Layout.Panel;
                    foreach (var sInterface in m_Editor.GetType().GetInterfaces())
                    {
                        var fullName = sInterface.FullName;
                        if (string.IsNullOrEmpty(fullName)) continue;
                        if (fullName.StartsWith("UnityEditor.Overlays.ICreateVerticalToolbar"))
                            temp |= Layout.VerticalToolbar;
                        if (fullName.StartsWith("UnityEditor.Overlays.ICreateHorizontalToolbar"))
                            temp |= Layout.HorizontalToolbar;
                        if (temp == Layout.All) break;
                    }

                    return temp;
                }
            }

            private object GetVerticalToolbarContent()
            {
                return m_Editor.GetType().GetMethod("CreateVerticalToolbarContent", ToolBarBind)
                    ?.Invoke(m_Editor, null);
            }

            private object GetHorizontalToolbarContent()
            {
                return m_Editor.GetType().GetMethod("CreateHorizontalToolbarContent", ToolBarBind)
                    ?.Invoke(m_Editor, null);
            }

            private VisualElement GetBool(LnkTools lnk)
            {
                var toolbar = new EditorToolbarToggle(lnk.Content.image as Texture2D, lnk.Content.image as Texture2D)
                {
                    tooltip = lnk.Content.tooltip,
                    text = lnk.Content.text,
                };

                toolbar.RegisterValueChangedCallback(evt =>
                {
                    if (evt.newValue) lnk.Invoke();
                    toolbar.value = lnk.Status;
                    toolbar.style.backgroundColor = lnk.Status ? Color.gray : lnk.BackgroundColor;
                });
                return toolbar;
            }

            private VisualElement GetVoid(LnkTools lnk)
            {
                var toolbar = new ToolbarButton(lnk.Invoke)
                {
                    tooltip = lnk.Content.tooltip,
                    text = lnk.Content.text,
                };
                toolbar.text = lnk.Content.text;
                if (lnk.Content.image != null)
                {
                    toolbar.Add(new Image
                    {
                        image = lnk.Content.image,
                    });
                }

                return toolbar;
            }

            private void ChangeVisualElement(VisualElement element, int index)
            {
                if (index == 0)
                {
                    element.style.borderTopLeftRadius = 2f;
                    element.style.borderTopRightRadius = 2f;
                }
                else if (index >= LnkToolList.Count - 1)
                {
                    element.style.borderBottomLeftRadius = 2f;
                    element.style.borderBottomRightRadius = 2f;
                }

                element.style.alignItems = Align.Center;
                element.style.alignContent = Align.Center;
                element.style.alignSelf = Align.Center;
                element.style.justifyContent = Justify.SpaceBetween;
                if (layout == Layout.VerticalToolbar && !collapsed)
                {
                    element.style.marginTop = 0;
                    element.style.marginBottom = 0;
                    if (index > 0) element.style.marginTop = 0.7f;
                    if (index >= LnkToolList.Count - 1) element.style.marginBottom = 0.7f;
                }
                else
                {
                    if (collapsed || index > 0) element.style.marginLeft = 1.4f;
                    if (index >= LnkToolList.Count - 1) element.style.marginRight = 1.4f;
                    element.style.marginTop = 1.4f;
                    element.style.marginBottom = 1.4f;
                }
            }

            private void OnDraw(VisualElement element, FlexDirection flexDirection)
            {
                element.Clear();
                for (var index = 0; index < LnkToolList.Count; index++)
                {
                    var lnk = LnkToolList[index];
                    var toolbar = lnk.hasReturn ? GetBool(lnk) : GetVoid(lnk);

                    toolbar.tooltip = lnk.Content.tooltip;
                    toolbar.style.backgroundColor = lnk.BackgroundColor;
                    toolbar.style.unityBackgroundImageTintColor = lnk.ForegroundColor;

                    ChangeVisualElement(toolbar, index);
                    toolbar.style.width = 36;
                    toolbar.style.height = 18;
                    element.Add(toolbar);
                }

                element.style.borderBottomLeftRadius = 2f;
                element.style.borderBottomRightRadius = 2f;
                element.style.borderTopLeftRadius = 2f;
                element.style.borderTopRightRadius = 2f;
                element.style.unityBackgroundImageTintColor = Color.white;

                if (layout == Layout.VerticalToolbar && !collapsed)
                {
                    element.style.alignItems = Align.FlexStart;
                    element.style.alignSelf = Align.FlexStart;
                    element.style.alignContent = Align.FlexStart;
                }

                element.style.flexDirection = flexDirection;
                element.style.justifyContent = Justify.SpaceBetween;
            }

            public override VisualElement CreatePanelContent()
            {
                m_Content?.RemoveFromHierarchy();
                m_Content = null;
                if (m_Editor is null) return content;
                var flexDirection = FlexDirection.Row;
                if (isInToolbar)
                {
                    content.parent.
                    var snap = GetType().GetProperty("floatingSnapCorner", ToolBarBindNon | BindingFlags.GetProperty)?.GetValue(this, null) ?? 0;
                    Console.WriteLine(snap);

                    switch ((int)snap)
                    {
                        case 0: // 上 下
                        case 2:
                            if (GetHorizontalToolbarContent() is VisualElement visualElement)
                            {
                                content.Add(visualElement);
                                goto ok;
                            }

                            break;
                        default:
                            if (GetVerticalToolbarContent() is VisualElement visualElement2)
                            {
                                content.Add(visualElement2);
                                flexDirection = FlexDirection.Column;
                                goto ok;
                            }

                            break;
                    }
                }

                if (layout == Layout.VerticalToolbar)
                {
                    if (!collapsed)
                    {
                        if (GetVerticalToolbarContent() is VisualElement visualElement)
                        {
                            content.Add(visualElement);
                            flexDirection = FlexDirection.Column;
                            goto ok;
                        }
                    }
                    else
                    {
                        if (GetHorizontalToolbarContent() is VisualElement visualElement)
                        {
                            content.Add(visualElement);
                            goto ok;
                        }
                    }
                }

                if (layout == Layout.HorizontalToolbar)
                {
                    if (GetHorizontalToolbarContent() is VisualElement visualElement)
                    {
                        content.Add(visualElement);
                        goto ok;
                    }
                }

                content.Add(m_Editor.CreateInspectorGUI() ?? new IMGUIContainer(m_Editor.OnInspectorGUI));

                ok:
                OnDraw(content, flexDirection);
                return content;
            }

            [MethodImpl(MethodImplOptions.NoOptimization)]
            public virtual VisualElement OnCreateHorizontalToolbarContent()
            {
                return GetType().Name != nameof(LnkToolOverlay)
                    ? typeof(ToolbarOverlay).GetMethod("CreateToolbarContent",
                        ToolBarBindNon)?.Invoke(this, null) as VisualElement
                    : CreatePanelContent();
            }

            [MethodImpl(MethodImplOptions.NoOptimization)]
            public virtual VisualElement OnCreateVerticalToolbarContent()
            {
                return GetType().Name != nameof(LnkToolOverlay)
                    ? typeof(ToolbarOverlay).GetMethod("CreateToolbarContent",
                        ToolBarBindNon)?.Invoke(this, null) as VisualElement
                    : CreatePanelContent();
            }

            public LnkToolOverlay()
            {
                GetLnkTools();
                ToolManager.activeToolChanged += OnToolChanged;
                CreateEditor();
            }

            ~LnkToolOverlay()
            {
                ToolManager.activeToolChanged -= OnToolChanged;
            }

            private void OnToolChanged()
            {
                CreateEditor();
                GetType()
                    .GetMethod("RebuildContent", ToolBarBindNon)
                    ?.Invoke(this, null);
            }

            private void CreateEditor()
            {
                Object.DestroyImmediate(m_Editor);
                var type = typeof(ToolManager).Assembly.GetType("UnityEditor.EditorTools.EditorToolManager", true);
                var activeTool = type.GetProperty("activeTool", BindingFlags.Static | BindingFlags.NonPublic);
                m_Editor = Editor.CreateEditor(activeTool?.GetValue(null) as Object);
            }

            private void OnFloatingPositionChanged(Vector3 vector3)
            {
                floatingPosition = vector3;
                GetType().GetMethod("RebuildContent", ToolBarBindNon)?.Invoke(this, null);
            }

            public override void OnCreated()
            {
                floatingPositionChanged += OnFloatingPositionChanged;
                Undock();
            }

            public override void OnWillBeDestroyed()
            {
            }
        }
    }
}
#endif