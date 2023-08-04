using System;
using System.Collections.Generic;
using System.Linq;

using Borodar.RainbowCore;
using Borodar.RainbowFolders.Settings;

using UnityEditor;

using UnityEngine;

namespace Borodar.RainbowFolders
{
    [InitializeOnLoad]
    public class RainbowFoldersGUI
    {
        private const float SMALL_ICON_SIZE = 16f;

        private const float LARGE_ICON_SIZE = 64f;

        private static readonly Color ROW_SHADING_COLOR;

        private static bool _multiSelection;

        static RainbowFoldersGUI()
        {
            ROW_SHADING_COLOR = new Color(0f, 0f, 0f, 0.03f);
            EditorApplication.projectWindowItemOnGUI = (EditorApplication.ProjectWindowItemCallback)Delegate.Combine(EditorApplication.projectWindowItemOnGUI, new EditorApplication.ProjectWindowItemCallback(ProjectWindowItemOnGUI));
            EditorApplication.projectWindowItemOnGUI = (EditorApplication.ProjectWindowItemCallback)Delegate.Combine(EditorApplication.projectWindowItemOnGUI, new EditorApplication.ProjectWindowItemCallback(DrawEditIcon));
        }

        private static void ProjectWindowItemOnGUI(string guid, Rect rect)
        {
            bool flag = IsIconSmall(rect);
            string text = AssetDatabase.GUIDToAssetPath(guid);
            int mainAssetInstanceId = ProjectWindowAdapter.GetMainAssetInstanceId(text);
            if (flag)
            {
                DrawRowShading(rect);
                DrawFoldouts(rect, mainAssetInstanceId);
            }
            ReplaceFolderIcons(rect, mainAssetInstanceId, text, flag);
        }

        private static void DrawRowShading(Rect rect)
        {
            if (ProjectPreferences.DrawRowShading && Mathf.FloorToInt((rect.y - 4f) / 16f % 2f) == 0)
            {
                Rect rect2 = new Rect(rect);
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

        private static void DrawFoldouts(Rect rect, int id)
        {
            if (ProjectPreferences.ShowProjectTree)
            {
                Rect rect2 = new Rect(rect);
                rect2.width = 128f;
                Rect position = rect2;
                position.x -= 144f;
                GUI.DrawTexture(position, ProjectEditorUtility.GetFoldoutLevelsIcon());
                if (!IsRootItem(rect) && !ProjectWindowAdapter.HasChildren(id))
                {
                    position.width = 16f;
                    position.x = rect.x - 16f;
                    GUI.DrawTexture(position, ProjectEditorUtility.GetFoldoutIcon());
                }
            }
        }

        private static void ReplaceFolderIcons(Rect rect, int assetId, string path, bool isSmall)
        {
            if (AssetDatabase.IsValidFolder(path) && !(ProjectRuleset.Instance == null))
            {
                ProjectRule ruleByPath = ProjectRuleset.Instance.GetRuleByPath(path, allowRecursive: true);
                if (ruleByPath != null)
                {
                    DrawCustomBackground(rect, ruleByPath, isSmall);
                    ReplaceIcon(assetId, ruleByPath, isSmall);
                }
            }
        }

        private static void DrawEditIcon(string guid, Rect rect)
        {
            if ((Event.current.modifiers & ProjectPreferences.ModifierKey) == 0)
            {
                _multiSelection = false;
                return;
            }
            bool isSmall = IsIconSmall(rect);
            Rect iconRect = GetIconRect(rect, isSmall);
            bool flag = rect.Contains(Event.current.mousePosition);
            _multiSelection = ((!IsSelected(guid)) ? (!flag && _multiSelection) : (flag || _multiSelection));
            if (!flag && (!IsSelected(guid) || !_multiSelection))
            {
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (AssetDatabase.IsValidFolder(path))
            {
                Texture2D editFolderIcon = ProjectEditorUtility.GetEditFolderIcon(isSmall, EditorGUIUtility.isProSkin);
                DrawCustomIcon(iconRect, editFolderIcon);
                if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
                {
                    ShowPopupWindow(iconRect, path);
                }
                EditorApplication.RepaintProjectWindow();
            }
        }

        private static void ShowPopupWindow(Rect rect, string path)
        {
            RainbowFoldersPopup draggableWindow = RainbowFoldersPopup.GetDraggableWindow();
            Vector2 inPosition = GUIUtility.GUIToScreenPoint(rect.position + new Vector2(0f, rect.height + 2f));
            if (_multiSelection)
            {
                List<string> list = Selection.assetGUIDs.Select(AssetDatabase.GUIDToAssetPath).Where(AssetDatabase.IsValidFolder).ToList();
                int pathIndex = list.IndexOf(path);
                draggableWindow.ShowWithParams(inPosition, list, pathIndex);
            }
            else
            {
                draggableWindow.ShowWithParams(inPosition, new List<string> { path }, 0);
            }
        }

        private static void DrawCustomIcon(Rect rect, Texture texture)
        {
            Rect position = rect;
            if (position.width > 64f)
            {
                float num = (position.width - 64f) / 2f;
                position = new Rect(position.x + num, position.y + num, 64f, 64f);
            }
            GUI.DrawTexture(position, texture);
        }

        private static void DrawCustomBackground(Rect rect, ProjectRule rule, bool isSmall)
        {
            if (rule != null && rule.HasBackground())
            {
                Rect backgroundRect = GetBackgroundRect(rect, isSmall);
                Texture2D image = (rule.HasCustomBackground() ? rule.BackgroundTexture : CoreBackgroundsStorage.GetBackground(rule.BackgroundType));
                GUI.DrawTexture(backgroundRect, image);
            }
        }

        private static void ReplaceIcon(int assetId, ProjectRule rule, bool isSmall)
        {
            if (!rule.HasIcon())
            {
                return;
            }
            Texture2D texture2D = null;
            if (rule.HasCustomIcon())
            {
                texture2D = (isSmall ? rule.SmallIcon : rule.LargeIcon);
            }
            else
            {
                var icons = ProjectIconsStorage.GetIcons(rule.IconType);
                if (icons != null)
                {
                    texture2D = (isSmall ? icons.Item2 : icons.Item1);
                }
            }
            if (texture2D != null)
            {
                ProjectWindowAdapter.ApplyIconByPath(assetId, texture2D, isSmall);
            }
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
