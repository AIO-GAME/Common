using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO.Unity.Editor
{
    public class IconsListGraphWindow : GraphicWindow
    {
        #region static

        static IconsListGraphWindow()
        {
            iconNames = new List<string>();
            iconContentListSmall = new List<GUIContent>();
            iconContentListBig = new List<GUIContent>();
            iconContentListAll = new List<GUIContent>();
        }

#if MONKEYCOMMANDER
        [MonKey.Command("Icons Manager",
            Help = "Icons 管理器",
            Category = "Windows",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Tools/Window/Editor Icons", priority = -1001)]
        public static void EditorIconsOpen()
        {
            UtilsEditor.Window.Open<IconsListGraphWindow>("Editor Icons");
        }

        private static List<GUIContent> iconContentListAll;
        private static List<GUIContent> iconContentListSmall;
        private static List<GUIContent> iconContentListBig;
        private static List<string> iconNames;

        private static GUIContent GetIcon(string icon_name)
        {
            GUIContent valid = null;
            Debug.unityLogger.logEnabled = false;
            if (!string.IsNullOrEmpty(icon_name)) valid = EditorGUIUtility.IconContent(icon_name);
            Debug.unityLogger.logEnabled = true;
            return valid?.image == null ? null : valid;
        }

        private static void AllTheTEXTURES(ref GUIStyle s, Texture2D t)
        {
            s.hover.background
                = s.onHover.background
                    = s.focused.background
                        = s.onFocused.background
                            = s.active.background
                                = s.onActive.background
                                    = s.normal.background
                                        = s.onNormal.background
                                            = t;
            s.hover.scaledBackgrounds
                = s.onHover.scaledBackgrounds
                    = s.focused.scaledBackgrounds
                        = s.onFocused.scaledBackgrounds
                            = s.active.scaledBackgrounds
                                = s.onActive.scaledBackgrounds
                                    = s.normal.scaledBackgrounds
                                        = s.onNormal.scaledBackgrounds
                                            = new Texture2D[] { t };
        }

        private static Texture2D Texture2DPixel(Color c)
        {
            var t = new Texture2D(1, 1);
            t.SetPixel(0, 0, c);
            t.Apply();
            return t;
        }

        private static void SaveIcon(string icon_name)
        {
            var tex = EditorGUIUtility.IconContent(icon_name).image as Texture2D;

            if (tex != null)
            {
                var path = EditorUtility.SaveFilePanel("Save icon", "", icon_name, "png");

                if (path != null)
                {
                    try
                    {
#if UNITY_2018
                        var outTex = new Texture2D(tex.width, tex.height, tex.format, true);
#else
                        var outTex = new Texture2D(tex.width, tex.height, tex.format, tex.mipmapCount, true);
#endif
                        Graphics.CopyTexture(tex, outTex);
                        File.WriteAllBytes(path, outTex.EncodeToPNG());
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Cannot save the icon : " + e.Message);
                    }
                }
            }
            else
            {
                Debug.LogError("Cannot save the icon : null texture error!");
            }
        }

        private static void InitIcons()
        {
            if (iconNames != null && iconNames.Count > 0) return;
            foreach (var x in Resources.FindObjectsOfTypeAll<Texture2D>())
            {
                Debug.unityLogger.logEnabled = false;
                var gc = EditorGUIUtility.IconContent(x.name);
                Debug.unityLogger.logEnabled = true;
                if (gc != null && gc.image != null)
                {
                    iconNames.Add(x.name);
                    gc.tooltip = x.name;
                    iconContentListAll.Add(gc);
                    if (!(gc.image.width <= 36 || gc.image.height <= 36)) iconContentListBig.Add(gc);
                    else iconContentListSmall.Add(gc);
                }
            }

            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        #endregion

        private Vector2 scroll;
        private int buttonSize = 70;
        private string search = "";

        private GUIContent SearchGUIContent;
        private GUIContent iconSelected;

        private GUIStyle iconButtonStyle = null;
        private GUIStyle iconPreviewBlack = null;
        private GUIStyle iconPreviewWhite = null;

        private bool doSearch => !string.IsNullOrWhiteSpace(search) && search != "";
        private bool isWide => Screen.width > 550;
        private bool viewBigIcons = true;
        private bool darkPreview = true;

        private void SearchGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                if (isWide) GUILayout.Space(10);

#if UNITY_2018
                search = EditorGUILayout.TextField(search, EditorStyles.toolbarTextField);
#else
                search = EditorGUILayout.TextField(search, EditorStyles.toolbarSearchField);
#endif
                //SVN_DeletedLocal
                if (GUILayout.Button(SearchGUIContent, EditorStyles.toolbarButton, GUILayout.Width(22))) search = "";
            }
        }

        protected override void OnEnable()
        {
            InitIcons();

            if (iconButtonStyle == null)
            {
                iconButtonStyle = new GUIStyle(EditorStyles.miniButton);
                iconButtonStyle.margin = new RectOffset(0, 0, 0, 0);
                iconButtonStyle.fixedHeight = 0;
            }

            if (iconPreviewBlack == null)
            {
                iconPreviewBlack = new GUIStyle(iconButtonStyle);
                AllTheTEXTURES(ref iconPreviewBlack, Texture2DPixel(new Color(0.15f, 0.15f, 0.15f)));
            }

            if (iconPreviewWhite == null)
            {
                iconPreviewWhite = new GUIStyle(iconButtonStyle);
                AllTheTEXTURES(ref iconPreviewWhite, Texture2DPixel(new Color(0.85f, 0.85f, 0.85f)));
            }

            if (SearchGUIContent == null)
            {
                SearchGUIContent = EditorGUIUtility.IconContent("winbtn_mac_close_h");
            }
        }

        protected override void OnGUI()
        {
            var ppp = EditorGUIUtility.pixelsPerPoint;
            if (!isWide) SearchGUI();
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label($"Select what icons to show [count:{iconNames.Count}]", GUILayout.Width(250));
                viewBigIcons = GUILayout.SelectionGrid(
                    viewBigIcons ? 1 : 0,
                    new string[] { "Small", "Big" },
                    2, EditorStyles.toolbarButton) == 1;

                if (isWide) SearchGUI();
            }

            if (isWide) GUILayout.Space(3);
            using (var scope = new GUILayout.ScrollViewScope(scroll))
            {
                GUILayout.Space(10);

                scroll = scope.scrollPosition;

                buttonSize = viewBigIcons ? 70 : 40;

                // scrollbar_width = ~ 12.5
                var render_width = (Screen.width / ppp - 13f);
                var gridW = Mathf.FloorToInt(render_width / buttonSize);
                var margin_left = (render_width - buttonSize * gridW) / 2;

                List<GUIContent> iconList;
                if (doSearch) iconList = iconContentListAll.Where(x => x.tooltip.ToLower().Contains(search.ToLower())).ToList();
                else iconList = viewBigIcons ? iconContentListBig : iconContentListSmall;

                int row = 0, index = 0;
                while (index < iconList.Count)
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Space(margin_left);
                        for (var i = 0; i < gridW; ++i)
                        {
                            var k = i + row * gridW;
                            var icon = iconList[k];
                            if (GUILayout.Button(icon, iconButtonStyle, GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
                            {
                                EditorGUI.FocusTextInControl("");
                                iconSelected = icon;
                            }

                            if (++index == iconList.Count) break;
                        }
                    }

                    row++;
                }

                GUILayout.Space(10);
            }


            if (iconSelected == null || iconSelected.image == null) return;

            GUILayout.FlexibleSpace();
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.MaxHeight(viewBigIcons ? 140 : 120)))
            {
                using (new GUILayout.VerticalScope(GUILayout.Width(130)))
                {
                    GUILayout.Space(2);
                    GUILayout.Button(iconSelected, darkPreview ? iconPreviewBlack : iconPreviewWhite, GUILayout.Width(128), GUILayout.Height(viewBigIcons ? 128 : 40));
                    GUILayout.Space(5);

                    darkPreview = GUILayout.SelectionGrid(
                        darkPreview ? 1 : 0,
                        new string[] { "Light", "Dark" },
                        2, EditorStyles.miniButton) == 1;

                    GUILayout.FlexibleSpace();
                }

                GUILayout.Space(10);
                using (new GUILayout.VerticalScope())
                {
                    var s = $"Size: {iconSelected.image.width}x{iconSelected.image.height}";
                    s += "\nIs Pro Skin Icon: " + (iconSelected.tooltip.IndexOf("d_", StringComparison.CurrentCulture) == 0 ? "Yes" : "No");
                    s += $"\nTotal {iconContentListAll.Count} icons";
                    GUILayout.Space(5);
                    EditorGUILayout.HelpBox(s, MessageType.None);
                    GUILayout.Space(5);
                    EditorGUILayout.TextField("EditorGUIUtility.IconContent(\"" + iconSelected.tooltip + "\")");
                    GUILayout.Space(5);
                    if (GUILayout.Button("Copy to clipboard", EditorStyles.miniButton))
                        EditorGUIUtility.systemCopyBuffer = iconSelected.tooltip;
                    if (GUILayout.Button("Save icon to file ...", EditorStyles.miniButton))
                        SaveIcon(iconSelected.tooltip);
                }

                GUILayout.Space(10);
                if (GUILayout.Button("X", GUILayout.ExpandHeight(true))) iconSelected = null;
            }
        }

        protected override void OnDestroy()
        {
            iconNames.Clear();
            iconContentListAll.Clear();
            iconContentListSmall.Clear();
            iconContentListBig.Clear();
        }
    }
}