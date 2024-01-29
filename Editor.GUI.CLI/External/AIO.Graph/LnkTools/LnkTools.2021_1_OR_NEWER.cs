/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-08
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/


#if UNITY_2021_1_OR_NEWER
#if !UNITY_2023_1_OR_NEWER
using System.Runtime.CompilerServices;
using MonoHook;
using System;
#endif

using System.Reflection;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace AIO.UEditor
{
    [Overlay(typeof(SceneView), "Lnk", true
#if UNITY_2023_1_OR_NEWER
        , defaultLayout = Layout.VerticalToolbar
#endif
    )]
    [Icon("Packages/com.aio.package/Resources/Editor/Icon/Setting/icon_option_button.png")]
    public class LnkToolOverlay : ToolbarOverlay, ITransientOverlay
#if UNITY_2022_1_OR_NEWER
        , ICreateHorizontalToolbar, ICreateVerticalToolbar
#endif
    {
#if UNITY_2022_1_OR_NEWER
        /// <summary>
        /// 创建竖排工具栏
        /// </summary>
        public new OverlayToolbar CreateVerticalToolbarContent()
        {
            var toolbar = new OverlayToolbar();
            OnDraw(toolbar, true);
            return toolbar;
        }

        /// <summary>
        /// 创建横排工具栏
        /// </summary>
        public new OverlayToolbar CreateHorizontalToolbarContent()
        {
            var toolbar = new OverlayToolbar();
            OnDraw(toolbar, false);
            return toolbar;
        }
#else
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public virtual VisualElement OnCreateHorizontalToolbarContent()
        {
            var isLnk = GetType().Name != nameof(LnkToolOverlay);
            if (!isLnk) return CreatePanelContent();
            var type = typeof(ToolbarOverlay);
            var method = type.GetMethod("CreateToolbarContent", ToolBarBindNon);
            if (method is null) return CreatePanelContent();
            var invoke = method.Invoke(this, null) as VisualElement;
            return invoke ?? CreatePanelContent();
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public virtual VisualElement OnCreateVerticalToolbarContent()
        {
            var isLnk = GetType().Name != nameof(LnkToolOverlay);
            if (!isLnk) return CreatePanelContent();
            var type = typeof(ToolbarOverlay);
            var method = type.GetMethod("CreateToolbarContent", ToolBarBindNon);
            if (method is null) return CreatePanelContent();
            var invoke = method.Invoke(this, null) as VisualElement;
            return invoke ?? CreatePanelContent();
        }

        private static readonly MethodHook _hook1;
        private static readonly MethodHook _hook2;

        static LnkToolOverlay()
        {
            var type = typeof(LnkToolOverlay);
            Debug.unityLogger.logEnabled = false;
            var miTarget = type.GetMethod("CreateHorizontalToolbarContent", ToolBarBind);
            var miReplacement = type.GetMethod("OnCreateHorizontalToolbarContent", ToolBarBind);

            _hook1 = new MethodHook(miTarget, miReplacement);
            _hook1.Install();

            var Target = type.GetMethod("CreateVerticalToolbarContent", ToolBarBind);
            var Replacement = type.GetMethod("OnCreateVerticalToolbarContent", ToolBarBind);
            _hook2 = new MethodHook(Target, Replacement);
            _hook2.Install();
            Debug.unityLogger.logEnabled = true;
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
            if (m_Editor is null) return null;
            return m_Editor.GetType().GetMethod("CreateVerticalToolbarContent", ToolBarBind)?.Invoke(m_Editor, null);
        }

        private object GetHorizontalToolbarContent()
        {
            if (m_Editor is null) return null;
            return m_Editor.GetType().GetMethod("CreateHorizontalToolbarContent", ToolBarBind)?.Invoke(m_Editor, null);
        }

#endif


        private const BindingFlags ToolBarBind = BindingFlags.Instance | BindingFlags.Public;
        private const BindingFlags ToolBarBindNon = BindingFlags.Instance | BindingFlags.NonPublic;
        public const string k_Id = "unity-lnk-toolbar";


        public bool visible => content?.visible ?? false;
        private Editor m_Editor;
        private VisualElement m_Content;

        private VisualElement content
        {
            get
            {
                if (m_Content == null)
                {
                    m_Content = new VisualElement
                    {
                        name = "toolbar-overlay"
                    };
                }

                return m_Content;
            }
        }

        /// <summary>
        /// Toggle事件
        /// </summary>
        /// <param name="lnk">lnk数据</param>
        /// <returns>元素展示</returns>
        private static VisualElement GetBool(LnkTools lnk)
        {
            var toolbar = new ToolbarToggle
            {
                tooltip = lnk.Content.tooltip,
            };
            if (lnk.Content.image is Texture2D image)
            {
                toolbar.style.backgroundImage = image;
#if UNITY_2022_1_OR_NEWER
                toolbar.style.backgroundRepeat =
                    new StyleBackgroundRepeat(new BackgroundRepeat(Repeat.NoRepeat, Repeat.NoRepeat));
                toolbar.style.backgroundPositionX =
                    new StyleBackgroundPosition(new BackgroundPosition(BackgroundPositionKeyword.Center));
                toolbar.style.backgroundPositionY =
                    new StyleBackgroundPosition(new BackgroundPosition(BackgroundPositionKeyword.Center));
                toolbar.style.backgroundSize = new StyleBackgroundSize(new BackgroundSize(BackgroundSizeType.Contain));
#else
                toolbar.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
#endif
            }
            else toolbar.text = lnk.Content.text;

            toolbar.style.backgroundColor = lnk.Status ? Color.gray : lnk.BackgroundColor;
            toolbar.value = lnk.Status;
            toolbar.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue) lnk.Invoke();
                toolbar.value = lnk.Status;
                toolbar.style.backgroundColor = lnk.Status ? Color.gray : lnk.BackgroundColor;
            });
            return toolbar;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="lnk">lnk数据</param>
        /// <returns>元素展示</returns>
        private static VisualElement GetVoid(LnkTools lnk)
        {
            var toolbar = new ToolbarButton(lnk.Invoke)
            {
                tooltip = lnk.Content.tooltip,
            };
            if (lnk.Content.image is Texture2D image)
            {
                toolbar.style.backgroundImage = image;
#if UNITY_2022_1_OR_NEWER
                toolbar.style.backgroundRepeat =
                    new StyleBackgroundRepeat(new BackgroundRepeat(Repeat.NoRepeat, Repeat.NoRepeat));
                toolbar.style.backgroundPositionX =
                    new StyleBackgroundPosition(new BackgroundPosition(BackgroundPositionKeyword.Center));
                toolbar.style.backgroundPositionY =
                    new StyleBackgroundPosition(new BackgroundPosition(BackgroundPositionKeyword.Center));
                toolbar.style.backgroundSize = new StyleBackgroundSize(new BackgroundSize(BackgroundSizeType.Contain));
#else
                toolbar.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
#endif
            }
            else toolbar.text = lnk.Content.text;

            return toolbar;
        }

        /// <summary>
        /// 修改VisualElement
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="index">下标</param>
        /// <param name="isVertical">Ture:竖排 False:横排</param>
        private static void ChangeVisualElement(VisualElement element, int index, bool isVertical)
        {
            element.style.alignContent = Align.Center;
            element.style.alignItems = Align.Stretch;
            element.style.alignSelf = Align.Auto;
            element.style.flexDirection = FlexDirection.Row;
            element.style.justifyContent = Justify.Center;

            element.style.minHeight = 20;
            element.style.minWidth = 32;

            element.style.paddingLeft = 0;
            element.style.paddingRight = 0;
            element.style.paddingTop = 0;
            element.style.paddingBottom = 0;

            if (isVertical) // 竖排 且 未折叠
            {
                element.style.width = 37;
                element.style.height = 20;
                element.style.marginLeft = 0;
                element.style.marginRight = 0;
                element.style.marginTop = 0;
                element.style.marginBottom = 1;

                if (index == 0)
                {
                    element.style.borderTopLeftRadius = 3;
                    element.style.borderTopRightRadius = 3;
                    element.style.marginTop = 1.5f;
                }

                if (index >= LnkToolsHelper.Data.Count - 1)
                {
                    element.style.borderBottomLeftRadius = 3;
                    element.style.borderBottomRightRadius = 3;
                    element.style.marginBottom = 1.5f;
                }
            }
            else
            {
                element.style.width = 32;
                element.style.height = 20;
                element.style.marginLeft = 0;
                element.style.marginRight = 1;
                element.style.marginTop = 0;
                element.style.marginBottom = 0;

                if (index == 0)
                {
                    element.style.marginLeft = 1.5f;
                    element.style.marginRight = 1;
                    element.style.borderBottomLeftRadius = 3;
                    element.style.borderTopLeftRadius = 3;
                }

                if (index >= LnkToolsHelper.Data.Count - 1)
                {
                    element.style.marginRight = 1.5f;
                    element.style.borderTopRightRadius = 3;
                    element.style.borderBottomRightRadius = 3;
                }
            }
        }

        /// <summary>
        /// 绘制面板
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="isVertical">是否为竖排 Ture:竖排 False横排</param>
        private void OnDraw(VisualElement element, bool isVertical)
        {
            element.Clear();

            element.style.borderBottomLeftRadius = 2f;
            element.style.borderBottomRightRadius = 2f;
            element.style.borderTopLeftRadius = 2f;
            element.style.borderTopRightRadius = 2f;
            element.style.unityBackgroundImageTintColor = Color.white;

            element.style.alignItems = Align.FlexStart;
            element.style.alignSelf = Align.Stretch;
            element.style.alignContent = Align.Auto;
            if (isVertical)
            {
                element.style.flexDirection = FlexDirection.Column;
                element.style.justifyContent = Justify.Center;
            }
            else
            {
                element.style.flexDirection = FlexDirection.Row;
                element.style.justifyContent = Justify.FlexStart;
            }

            var index = 0;
            foreach(var lnk in LnkToolsHelper.Data)
            {
                if (lnk.ShowMode != ELnkShowMode.SceneView) continue;
                index++;
                var toolbar = lnk.hasReturn ? GetBool(lnk) : GetVoid(lnk);

                toolbar.tooltip = lnk.Content.tooltip;
                toolbar.style.backgroundColor = lnk.BackgroundColor;
                toolbar.style.unityBackgroundImageTintColor = lnk.ForegroundColor;
                ChangeVisualElement(toolbar, index, isVertical);

                switch (lnk.Mode)
                {
                    case ELnkToolsMode.OnlyRuntime: // 禁用元素点击
                        toolbar.SetEnabled(EditorApplication.isPlaying);
                        break;
                    case ELnkToolsMode.OnlyEditor:
                        toolbar.SetEnabled(!EditorApplication.isPlaying);
                        break;
                    default:
                        toolbar.SetEnabled(true);
                        break;
                }

                element.Add(toolbar);
            }
        }

        /// <summary>
        /// 是否为竖排
        /// </summary>
        /// <returns>Ture:竖排 False:横排</returns>
        private bool IsVertical()
        {
            if (collapsed) return false;

            if (isInToolbar && typeof(Overlay)
                    .GetProperty("rootVisualElement", ToolBarBindNon | BindingFlags.GetProperty)
                    ?.GetValue(this, null) is VisualElement visualElement)
                return visualElement.parent.ClassListContains("unity-overlay-container-vertical");

            return layout == Layout.VerticalToolbar;
        }

        /// <summary>
        /// 创建面板内容
        /// </summary>
        public override VisualElement CreatePanelContent()
        {
            m_Content?.RemoveFromHierarchy();
            m_Content = new VisualElement
            {
                name = "toolbar-overlay",
                style =
                {
                    display = DisplayStyle.Flex,
                },
                pickingMode = PickingMode.Position
            };
            if (m_Editor is null) return m_Content;
            var isVertical = IsVertical();
#if UNITY_2022_1_OR_NEWER
            m_Content.Add(CreateContent(isVertical ? Layout.VerticalToolbar : Layout.HorizontalToolbar));
#else
            if ((isVertical
                    ? GetVerticalToolbarContent()
                    : GetHorizontalToolbarContent()) is VisualElement visualElement) m_Content.Add(visualElement);
            else m_Content.Add(m_Editor.CreateInspectorGUI() ?? new IMGUIContainer(m_Editor.OnInspectorGUI));
#endif
            OnDraw(m_Content, isVertical);
            return m_Content;
        }

        public LnkToolOverlay()
        {
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
            typeof(Overlay).GetMethod("RebuildContent", ToolBarBindNon)?.Invoke(this, null);
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            typeof(Overlay).GetMethod("RebuildContent", ToolBarBindNon)?.Invoke(this, null);
        }

        public override void OnCreated()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        public override void OnWillBeDestroyed()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void CreateEditor()
        {
            Object.DestroyImmediate(m_Editor);
            var type = typeof(ToolManager).Assembly.GetType("UnityEditor.EditorTools.EditorToolManager", true);
            var activeTool = type.GetProperty("activeTool", BindingFlags.Static | BindingFlags.NonPublic);
            m_Editor = Editor.CreateEditor(activeTool?.GetValue(null) as Object);
        }
    }
}
#endif