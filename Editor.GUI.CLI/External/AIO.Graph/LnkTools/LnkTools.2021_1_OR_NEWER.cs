/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-08
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#if UNITY_2021_1_OR_NEWER
using System;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AIO.UEditor
{
    internal static partial class LnkToolsHelper
    {
        [Overlay(typeof(SceneView), "Lnk Tool", "Lnk Tool", "Lnk Tool")]
        public class LnkToolOverlay : ToolbarOverlay, ITransientOverlay
        {
            public bool visible => temp?.visible ?? false;

            private VisualElement temp;
            private Toolbar m_Toolbar;

            public override VisualElement CreatePanelContent()
            {
                temp = new VisualElement()
                {
                    name = "toolbar-overlay"
                };
                m_Toolbar = new Toolbar();
                // m_Toolbar.name = "LnkToolOverlay";
                m_Toolbar.style.flexDirection = FlexDirection.Row;
                m_Toolbar.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
                m_Toolbar.pickingMode = PickingMode.Position;
                OnDraw(m_Toolbar);
                temp.style.flexDirection = FlexDirection.Row;
                temp.pickingMode = PickingMode.Position;
                temp.Add(m_Toolbar);
                containerWindow.rootVisualElement.Add(temp);
                Console.WriteLine(m_Toolbar.name);
                if (collapsed)
                {
                    // unity-toolbar-overlay
                    // var button = temp
                    //     .Q<VisualElement>("overlay-collapsed-content")
                    //     .Q<VisualElement>("overlay-content")
                    //     .Q<Button>(null, "unity-content")
                    //     .Q<Label>(null, "unity-label");
                    // button.text = "";
                    // button.style.backgroundImage = Resources.Load<Texture2D>("Setting/icon_option_button");
                }

                return m_Toolbar;
            }

            public LnkToolOverlay()
            {
                GetLnkTools();
            }

            public override void OnCreated()
            {
                layoutChanged += OnLayoutChanged;
                floatingChanged += OnFloatingChanged;
                collapsedChanged += OnCollapsedChanged;
                displayedChanged += OnDisplayedChanged;
                OnLayoutChanged(layout);
            }

            public override void OnWillBeDestroyed()
            {
                layoutChanged -= OnLayoutChanged;
                floatingChanged -= OnFloatingChanged;
                collapsedChanged -= OnCollapsedChanged;
                displayedChanged -= OnDisplayedChanged;
            }

            private void OnDisplayedChanged(bool ADisplayed)
            {
                Console.WriteLine($"{nameof(OnDisplayedChanged)} : {ADisplayed.ToString()}");
            }

            private void OnCollapsedChanged(bool ACollapsed)
            {
                m_Toolbar.style.display = ACollapsed ? DisplayStyle.None : DisplayStyle.Flex;

                Console.WriteLine($"{nameof(OnCollapsedChanged)} : {ACollapsed.ToString()}");
            }

            private void OnFloatingChanged(bool AFloating)
            {
                Console.WriteLine($"{nameof(OnFloatingChanged)} : {AFloating.ToString()}");
            }

            private void OnDraw(VisualElement element)
            {
                element.Clear();
                foreach (var lnk in LnkToolList)
                {
                    var toolbar = new Button
                    {
                        tooltip = lnk.Content.tooltip,
                        style =
                        {
                            unityBackgroundScaleMode = ScaleMode.ScaleToFit,
                            backgroundImage = lnk.Content.image as Texture2D,
                            width = 35,
                            height = 20
                        }
                    };
                    toolbar.clickable.clicked += () => { lnk.Method?.Invoke(null, null); };
                    element.contentContainer.Add(toolbar);
                }
            }

            private void OnLayoutChanged(Layout ALayout)
            {
                Console.WriteLine($"{nameof(OnLayoutChanged)} : {ALayout.ToString()}");
            }
        }
    }
}
#endif