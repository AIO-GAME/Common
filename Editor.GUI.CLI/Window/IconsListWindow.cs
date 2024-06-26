﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    [HelpURL("https://blog.csdn.net/CJB_King/article/details/89356652"), GWindow("ICON Manager", Group = "Tools",
                                                                                 Menu = "AIO/Window/Editor Icons",
                                                                                 MinSizeWidth = 600, MinSizeHeight = 600
     )]
    public class IconsListGraphWindow : GraphicWindow
    {
        private int  buttonSize  = 70;
        private bool darkPreview = true;

        private GUIStyle   iconButtonStyle;
        private GUIStyle   iconPreviewBlack;
        private GUIStyle   iconPreviewWhite;
        private GUIContent iconSelected;

        private Vector2 scroll;
        private string  search = "";

        private GUIContent SearchGUIContent;
        private int        viewBigIcons;

        private static bool isWide => Screen.width > 550;

        private bool doSearch => !string.IsNullOrWhiteSpace(search) && search != "";

        protected override void OnDisable()
        {
            iconNames.Clear();
            iconContentListAll.Clear();
            iconContentListSmall.Clear();
            iconContentListBig.Clear();
            iconContentListAIO.Clear();
        }

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

        protected override void OnActivation()
        {
            InitIcons();

            if (iconButtonStyle == null)
                iconButtonStyle = new GUIStyle(EditorStyles.miniButton)
                {
                    margin      = new RectOffset(0, 0, 0, 0),
                    fixedHeight = 0
                };

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
#if UNITY_2023_1_OR_NEWER
                SearchGUIContent = EditorGUIUtility.TrIconContent("d_clear");
#else
                SearchGUIContent = EditorGUIUtility.TrIconContent("winbtn_mac_close_h");
#endif
            }
        }

        protected override void OnDraw()
        {
            var ppp = EditorGUIUtility.pixelsPerPoint;
            if (!isWide) SearchGUI();
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label($"Select what icons to show [count:{iconNames.Count}]", GUILayout.Width(250));
                viewBigIcons = GUILayout.SelectionGrid(
                    viewBigIcons,
                    new[] { "Small", "Big", "AIO" },
                    3, EditorStyles.toolbarButton);

                if (isWide) SearchGUI();
            }

            if (isWide) GUILayout.Space(3);
            using (var scope = new GUILayout.ScrollViewScope(scroll))
            {
                GUILayout.Space(10);

                scroll = scope.scrollPosition;

                buttonSize = viewBigIcons == 1 ? 70 : 40;

                // scrollbar_width = ~ 12.5
                var render_width = Screen.width / ppp - 13f;
                var gridW = Mathf.FloorToInt(render_width / buttonSize);
                var margin_left = (render_width - buttonSize * gridW) / 2;

                List<GUIContent> iconList;
                if (doSearch)
                    iconList = iconContentListAll.Where(x => x.tooltip.ToLower().Contains(search.ToLower())).ToList();
                else
                    switch (viewBigIcons)
                    {
                        case 0:
                            iconList = iconContentListSmall;
                            break;
                        case 2:
                            iconList = iconContentListAIO;
                            break;
                        case 1:
                            iconList = iconContentListBig;
                            break;
                        default:
                            iconList = iconContentListAll;
                            break;
                    }

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
                            if (GUILayout.Button(icon, iconButtonStyle, GUILayout.Width(buttonSize),
                                                 GUILayout.Height(buttonSize)))
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
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox,
                                                 GUILayout.MaxHeight(viewBigIcons == 1 ? 140 : 120)))
            {
                using (new GUILayout.VerticalScope(GUILayout.Width(130)))
                {
                    GUILayout.Space(2);
                    GUILayout.Button(iconSelected, darkPreview ? iconPreviewBlack : iconPreviewWhite,
                                     GUILayout.Width(128), GUILayout.Height(viewBigIcons == 1 ? 128 : 40));
                    GUILayout.Space(5);

                    darkPreview = GUILayout.SelectionGrid(
                        darkPreview ? 1 : 0,
                        new[] { "Light", "Dark" },
                        2, EditorStyles.miniButton) == 1;

                    GUILayout.FlexibleSpace();
                }

                GUILayout.Space(10);
                using (new GUILayout.VerticalScope())
                {
                    var s = $"Size: {iconSelected.image.width}x{iconSelected.image.height}";
                    s += "\nIs Pro Skin Icon: " +
                         (iconSelected.tooltip.IndexOf("d_", StringComparison.CurrentCulture) == 0 ? "Yes" : "No");
                    s += $"\nTotal {iconContentListAll.Count} icons";
                    GUILayout.Space(5);
                    EditorGUILayout.HelpBox(s, MessageType.None);
                    GUILayout.Space(5);

                    using (new GUILayout.HorizontalScope())
                    {
                        if (viewBigIcons == 2 && iconSelected.tooltip.Contains("|"))
                        {
                            var key = iconSelected.tooltip.Split('|');
                            EditorGUILayout.TextField($"GEContent.Get{key[0]}(\"" + key[1] + "\")");
                            if (GUILayout.Button("Copy", EditorStyles.miniButton, GUILayout.Width(50)))
                                EditorGUIUtility.systemCopyBuffer = $"GEContent.Get{key[0]}(\"" + key[1] + "\")";
                        }
                        else
                        {
                            EditorGUILayout.TextField("EditorGUIUtility.IconContent(\"" + iconSelected.tooltip + "\")");
                            if (GUILayout.Button("Copy", EditorStyles.miniButton, GUILayout.Width(50)))
                                EditorGUIUtility.systemCopyBuffer = iconSelected.tooltip;
                        }
                    }

                    GUILayout.Space(5);
                    using (new GUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button("Save icon to file ...", EditorStyles.miniButton)) SaveIcon(iconSelected.image as Texture2D);
                    }
                }

                GUILayout.Space(10);
                if (GUILayout.Button("X", GUILayout.ExpandHeight(true))) iconSelected = null;
            }
        }

        #region static

        static IconsListGraphWindow()
        {
            iconNames            = new Dictionary<string, string>();
            iconContentListSmall = new List<GUIContent>();
            iconContentListBig   = new List<GUIContent>();
            iconContentListAll   = new List<GUIContent>();
            iconContentListAIO   = new List<GUIContent>();
        }

        private static readonly List<GUIContent>           iconContentListAll;
        private static readonly List<GUIContent>           iconContentListSmall;
        private static readonly List<GUIContent>           iconContentListBig;
        private static readonly List<GUIContent>           iconContentListAIO;
        private static readonly Dictionary<string, string> iconNames;

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
                                            = new[] { t };
        }

        private static Texture2D Texture2DPixel(Color c)
        {
            var t = new Texture2D(1, 1);
            t.SetPixel(0, 0, c);
            t.Apply();
            return t;
        }

        private static void SaveIcon(Texture2D tex)
        {
            if (tex != null)
            {
                var path = EditorUtility.SaveFilePanel("Save icon", "", tex.name, "png");

                if (path == null) return;
                try
                {
#if UNITY_2019_1_OR_NEWER
                    var outTex = new Texture2D(tex.width, tex.height, tex.format, tex.mipmapCount, true);
#else
                    var outTex = new Texture2D(tex.width, tex.height, tex.format, true);
#endif
                    Graphics.CopyTexture(tex, outTex);
                    File.WriteAllBytes(path, outTex.EncodeToPNG());
                }
                catch (Exception e)
                {
                    Debug.LogError("Cannot save the icon : " + e.Message);
                }
            }
            else
            {
                Debug.LogError("Cannot save the icon : null texture error!");
            }
        }

        private static void InitIcons()
        {
            if (iconNames.Count > 0) return;
            foreach (var x in Resources.FindObjectsOfTypeAll<Texture2D>())
            {
                Debug.unityLogger.logEnabled = false;
                var gc = EditorGUIUtility.IconContent(x.name);
                Debug.unityLogger.logEnabled = true;
                if (gc == null || gc.image == null) continue;
                if (iconNames.ContainsKey(x.name)) continue;

                iconNames.Add(x.name, x.name);
                gc.tooltip = x.name;
                iconContentListAll.Add(gc);
                if (!(gc.image.width <= 36 || gc.image.height <= 36)) iconContentListBig.Add(gc);
                else iconContentListSmall.Add(gc);
            }

            GEContent.LoadSetting();
            GEContent.LoadApp();
            foreach (var gc in GEContent.GCSetting.Select(item => new GUIContent(item.Value.image, string.Concat("Setting|", item.Key))))
            {
                iconContentListAIO.Add(gc);
                iconContentListAll.Add(gc);
                iconNames.Add(gc.tooltip, gc.tooltip);
            }

            foreach (var gc in GEContent.GCApp.Select(item => new GUIContent(item.Value.image, string.Concat("App|", item.Key))))
            {
                iconContentListAIO.Add(gc);
                iconContentListAll.Add(gc);
                iconNames.Add(gc.tooltip, gc.tooltip);
            }

            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        #endregion
    }
}