using System;
using System.Collections.Generic;
using System.Linq;
using AIO.RainbowCore;
using AIO.RainbowFolders.Settings;
using UnityEditor;
using UnityEngine;

namespace AIO.RainbowFolders
{
    internal class RainbowFoldersGUI
    {
        private const float SMALL_ICON_SIZE = 16f;

        private const float LARGE_ICON_SIZE = 64f;

        private static readonly Color ROW_SHADING_COLOR = new Color(0f, 0f, 0f, 0.03f);

        private static bool _multiSelection;

        private static readonly Dictionary<string, int> AssetIdCache = new Dictionary<string, int>();

        private static readonly Dictionary<string, string> AssetFolderCache = new Dictionary<string, string>();

        private static bool Enabled;

        internal static void Initialize()
        {
            if (Enabled == false && ProjectPreferences.Enable)
            {
                EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUI;
                EditorApplication.projectWindowItemOnGUI += DrawEditIcon;
                Enabled = true;
            }
            else if (Enabled)
            {
                EditorApplication.projectWindowItemOnGUI -= ProjectWindowItemOnGUI;
                EditorApplication.projectWindowItemOnGUI -= DrawEditIcon;
                Enabled = false;
            }
        }

        private static void ProjectWindowItemOnGUI(string guid, Rect rect)
        {
            var isSmall = IsIconSmall(rect);
            if (!AssetIdCache.TryGetValue(guid, out var assetId))
            {
                var text = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.IsValidFolder(text)) AssetFolderCache[guid] = text;
                else if (AssetFolderCache.ContainsKey(guid)) AssetFolderCache.Remove(guid);

                assetId = ProjectWindowAdapter.GetMainAssetInstanceId(text);
                AssetIdCache[guid] = assetId;
            }

            if (isSmall)
            {
                if (ProjectPreferences.DrawRowShading) DrawRowShading(rect);
                if (ProjectPreferences.ShowProjectTree) DrawProjectTree(rect, assetId);
            }

            if (ProjectPreferences.ReplaceFolderIcons || ProjectPreferences.DrawCustomBackground)
            {
                if (ProjectRuleset.Instance is null) return;
                if (!AssetFolderCache.TryGetValue(guid, out var path)) return;
                var ruleByPath = ProjectRuleset.Instance.GetRuleByPath(path, true);
                if (ruleByPath != null)
                {
                    if (ProjectPreferences.DrawCustomBackground)
                        DrawCustomBackground(rect, ruleByPath, isSmall);

                    if (ProjectPreferences.ReplaceFolderIcons)
                        ReplaceIcon(assetId, ruleByPath, isSmall);
                }
            }
        }

        private static void DrawRowShading(Rect rect)
        {
            if (Mathf.FloorToInt((rect.y - 4f) / 16f % 2f) == 0)
            {
                var rect2 = new Rect(rect);
                rect2.width += rect.x + 16f;
                rect2.height += 1f;
                rect2.x = 0f;
                EditorGUI.DrawRect(rect2, ROW_SHADING_COLOR);
                rect2.height = 1f;
                EditorGUI.DrawRect(rect2, ROW_SHADING_COLOR);
                rect2.y += 16f;
                EditorGUI.DrawRect(rect2, ROW_SHADING_COLOR);
            }
        }

        private static void DrawProjectTree(Rect rect, int id)
        {
            var rect2 = new Rect(rect) { width = 128f };
            var position = rect2;
            position.x -= 144f;
            GUI.DrawTexture(position, ProjectEditorUtility.GetFoldoutLevelsIcon());
            if (!IsRootItem(rect) && !ProjectWindowAdapter.HasChildren(id))
            {
                position.width = 16f;
                position.x = rect.x - 16f;
                GUI.DrawTexture(position, ProjectEditorUtility.GetFoldoutIcon());
            }
        }


        private static void DrawEditIcon(string guid, Rect rect)
        {
            if ((Event.current.modifiers & ProjectPreferences.ModifierKey) == 0)
            {
                _multiSelection = false;
                return;
            }

            var isSmall = IsIconSmall(rect);
            var iconRect = GetIconRect(rect, isSmall);
            var flag = rect.Contains(Event.current.mousePosition);
            var isSelected = !IsSelected(guid);
            _multiSelection = isSelected ? !flag && _multiSelection : flag || _multiSelection;
            if (!flag && (isSelected || !_multiSelection)) return;

            if (AssetFolderCache.ContainsKey(guid))
            {
                var editFolderIcon = ProjectEditorUtility.GetEditFolderIcon(isSmall, EditorGUIUtility.isProSkin);
                DrawCustomIcon(iconRect, editFolderIcon);
                if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
                {
                    ShowPopupWindow(iconRect, AssetFolderCache[guid]);
                }

                EditorApplication.RepaintProjectWindow();
            }
        }

        private static void ShowPopupWindow(Rect rect, string path)
        {
            var draggableWindow = RainbowFoldersPopup.GetDraggableWindow();
            var inPosition = GUIUtility.GUIToScreenPoint(rect.position + new Vector2(0f, rect.height + 2f));
            if (_multiSelection)
            {
                var list = Selection.assetGUIDs.Select(AssetDatabase.GUIDToAssetPath).Where(AssetDatabase.IsValidFolder).ToList();
                var pathIndex = list.IndexOf(path);
                draggableWindow.ShowWithParams(inPosition, list, pathIndex);
            }
            else
            {
                draggableWindow.ShowWithParams(inPosition, new List<string> { path }, 0);
            }
        }

        private static void DrawCustomIcon(Rect rect, Texture texture)
        {
            var position = rect;
            if (position.width > 64f)
            {
                var num = (position.width - 64f) / 2f;
                position = new Rect(position.x + num, position.y + num, 64f, 64f);
            }

            GUI.DrawTexture(position, texture);
        }

        /// <summary>
        /// 绘制自定义背景颜色
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="rule"></param>
        /// <param name="isSmall"></param>
        private static void DrawCustomBackground(Rect rect, ProjectRule rule, bool isSmall)
        {
            if (rule is null || !rule.HasBackground()) return;

            var backgroundRect = GetBackgroundRect(rect, isSmall);
            var image = rule.HasCustomBackground()
                ? rule.BackgroundTexture
                : CoreBackgroundsStorage.GetBackground(rule.BackgroundType);
            GUI.DrawTexture(backgroundRect, image);
        }

        private static void ReplaceIcon(int assetId, ProjectRule rule, bool isSmall)
        {
            if (!rule.HasIcon()) return;

            Texture2D texture2D = null;
            if (rule.HasCustomIcon()) texture2D = isSmall ? rule.SmallIcon : rule.LargeIcon;
            else
            {
                var icons = ProjectIconsStorage.GetIcons(rule.IconType);
                if (icons != null) texture2D = isSmall ? icons.Item2 : icons.Item1;
            }

            if (texture2D is null) return;
            ProjectWindowAdapter.ApplyIconByPath(assetId, texture2D, isSmall);
        }

        private static bool IsIconSmall(Rect rect)
        {
            return rect.width > rect.height;
        }

        private static Rect GetIconRect(Rect rect, bool isSmall)
        {
            if (isSmall)
            {
                rect.width = rect.height;
            }
            else
            {
                rect.height = rect.width;
            }

            return rect;
        }

        private static Rect GetBackgroundRect(Rect rect, bool isSmall)
        {
            if (isSmall)
            {
                rect.x += 17f;
                rect.width -= 17f;
            }
            else
            {
                rect.y += rect.width;
                rect.height -= rect.width;
            }

            return rect;
        }

        private static bool IsSelected(string guid)
        {
            return Selection.assetGUIDs.Contains(guid);
        }

        private static bool IsRootItem(Rect rect)
        {
            return rect.x <= 20f;
        }
    }
}