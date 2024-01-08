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
        [Overlay(typeof(SceneView), "Lnk Tool")]
        public class LnkToolOverlay : ToolbarOverlay, ITransientOverlay
        {
            public bool visible => Root?.visible ?? false;

            private IMGUIContainer Root;

            public override VisualElement CreatePanelContent()
            {
                var root = new IMGUIContainer();
                root.name = "toolbar-overlay";
                root.style.flexGrow = 2;
                root.style.flexDirection = FlexDirection.Row;
                root.tooltip = "Lnk Tool";
                root.onGUIHandler = OnDraw;
#if UNITY_2022_1_OR_NEWER
                var toolbar = CreateContent(Layout.HorizontalToolbar);
#else
                var toolbar = new ToolbarButton();
#endif
                toolbar.style.backgroundImage = Resources.Load<Texture2D>("Setting/icon_option_button");
                toolbar.name = "";
                toolbar.style.flexDirection = FlexDirection.Row;
#if !UNITY_2022_1_OR_NEWER
                toolbar.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
#endif
                toolbar.pickingMode = PickingMode.Position;
                root.Add(toolbar);
                OnDraw(root);
                return root;
            }

            private void OnDraw()
            {
                foreach (var lnk in LnkToolList)
                {
                    if (GUILayout.Button(lnk.Content, EditorStyles.toolbarButton))
                    {
                        lnk.Method?.Invoke(null, null);
                    }
                }
            }

            private void OnDraw(VisualElement element)
            {
                element.Clear();
                foreach (var lnk in LnkToolList)
                {
                    var toolbar = new ToolbarButton
                    {
                        name = lnk.Content.text,
                        tooltip = lnk.Content.tooltip,
                        style =
                        {
#if !UNITY_2022_1_OR_NEWER
                            unityBackgroundScaleMode = ScaleMode.ScaleToFit,
#endif
                            backgroundImage = lnk.Content.image as Texture2D,
                            width = 35,
                            height = 20
                        }
                    };
                    toolbar.clickable.clicked += () => { lnk.Method?.Invoke(null, null); };
                    element.AddToClassList(ToolbarButton.ussClassName);
                    element.Add(toolbar);
                }
            }

            public LnkToolOverlay()
            {
                GetLnkTools();
            }

            public override void OnCreated()
            {
                Root = (IMGUIContainer)CreatePanelContent();
                layoutChanged += OnLayoutChanged;
                floatingChanged += OnFloatingChanged;
                collapsedChanged += OnCollapsedChanged;
                displayedChanged += OnDisplayedChanged;
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
                Root.MarkDirtyRepaint();
                Console.WriteLine($"{nameof(OnDisplayedChanged)} : {ADisplayed.ToString()}");
            }

            private void OnCollapsedChanged(bool ACollapsed)
            {
                if (ACollapsed)
                {
                    // var temp = Resources.Load<Texture2D>("Setting/icon_option_button");
                    // Root.style.backgroundImage = temp;
                }
                else
                {
                    // button1.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
                    // button1.style.backgroundImage = null;
                }

                Root.MarkDirtyRepaint();
                Console.WriteLine($"{nameof(OnCollapsedChanged)} : {ACollapsed.ToString()}");
            }

            private void OnFloatingChanged(bool AFloating)
            {
                Root.MarkDirtyRepaint();
                Console.WriteLine($"{nameof(OnFloatingChanged)} : {AFloating.ToString()}");
            }

            private void OnLayoutChanged(Layout ALayout)
            {
                Root.MarkDirtyRepaint();
                Console.WriteLine($"{nameof(OnLayoutChanged)} : {ALayout.ToString()}");
            }
        }
    }
}
#endif