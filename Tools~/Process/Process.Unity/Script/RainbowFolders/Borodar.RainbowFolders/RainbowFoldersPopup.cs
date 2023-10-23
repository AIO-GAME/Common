using System.Collections.Generic;
using System.IO;
using AIO.RainbowCore;
using AIO.RainbowFolders.Settings;
using UnityEditor;
using UnityEngine;

namespace AIO.RainbowFolders
{
    internal class RainbowFoldersPopup : CoreDraggablePopupWindow
    {
        private const float PADDING = 8f;

        private const float SPACING = 1f;

        private const float LINE_HEIGHT = 16f;

        private const float LABELS_WIDTH = 85f;

        private const float PREVIEW_SIZE_SMALL = 16f;

        private const float PREVIEW_SIZE_LARGE = 64f;

        private const float BUTTON_WIDTH = 55f;

        private const float BUTTON_WIDTH_SMALL = 16f;

        private const float WINDOW_WIDTH = 325f;

        private const float WINDOW_HEIGHT = 164f;

        private static readonly Vector2 WINDOW_SIZE = new Vector2(325f, 164f);

        private static Rect _windowRect;

        private static Rect _backgroundRect;

        private List<string> _paths;

        private ProjectRuleset _ruleset;

        private ProjectRule[] _existingItems;

        private ProjectRule _currentRule;

        public static RainbowFoldersPopup GetDraggableWindow()
        {
            return CoreDraggablePopupWindow.GetDraggableWindow<RainbowFoldersPopup>();
        }

        public void ShowWithParams(Vector2 inPosition, List<string> paths, int pathIndex)
        {
            _paths = paths;
            _ruleset = ProjectRuleset.Instance;
            int count = paths.Count;
            _existingItems = new ProjectRule[count];
            _currentRule = new ProjectRule(ProjectRule.KeyType.Path, paths[pathIndex]);
            for (int i = 0; i < count; i++)
            {
                _existingItems[i] = _ruleset.GetRuleByPath(paths[i]);
            }

            if (_existingItems[pathIndex] != null)
            {
                _currentRule.CopyFrom(_existingItems[pathIndex]);
            }

            float num = (_currentRule.HasCustomIcon() ? 32f : 0f);
            float num2 = (_currentRule.HasCustomBackground() ? 16f : 0f);
            Rect rect = new Rect(inPosition, WINDOW_SIZE);
            rect.height = 164f + num + num2;
            Rect inPosition2 = rect;
            _windowRect = new Rect(Vector2.zero, inPosition2.size);
            _backgroundRect = new Rect(Vector2.one, inPosition2.size - new Vector2(2f, 2f));
            Show(inPosition2);
        }

        public override void OnGUI()
        {
            base.OnGUI();
            ChangeWindowSize(_currentRule.HasCustomIcon(), _currentRule.HasCustomBackground());
            Rect rect = _windowRect;
            Color color = (EditorGUIUtility.isProSkin ? CoreEditorUtility.POPUP_BORDER_CLR_PRO : CoreEditorUtility.POPUP_BORDER_CLR_FREE);
            EditorGUI.DrawRect(_windowRect, color);
            Color color2 = (EditorGUIUtility.isProSkin ? CoreEditorUtility.POPUP_BACKGROUND_CLR_PRO : CoreEditorUtility.POPUP_BACKGROUND_CLR_FREE);
            EditorGUI.DrawRect(_backgroundRect, color2);
            DrawLabels(ref rect, _currentRule);
            DrawValues(ref rect, _currentRule, _paths);
            DrawPreview(ref rect, _currentRule);
            DrawSeparators(ref rect);
            DrawButtons(ref rect);
        }

        private static void DrawLabels(ref Rect rect, ProjectRule rule)
        {
            rect.x += 4f;
            rect.y += 8f;
            rect.width = 77f;
            rect.height = 16f;
            rule.Type = (ProjectRule.KeyType)(object)EditorGUI.EnumPopup(rect, rule.Type);
            rect.y += 19f;
            EditorGUI.LabelField(rect, "Priority");
            rect.y += 29f;
            EditorGUI.LabelField(rect, "Icon");
            if (rule.HasCustomIcon())
            {
                rect.y += 20f;
                EditorGUI.LabelField(rect, "x16", EditorStyles.miniLabel);
                rect.y += 18f;
                EditorGUI.LabelField(rect, "x64", EditorStyles.miniLabel);
            }

            rect.y += 18f;
            EditorGUI.LabelField(rect, "Recursive", EditorStyles.miniLabel);
            rect.y += 22f;
            EditorGUI.LabelField(rect, "Background");
            if (rule.HasCustomBackground())
            {
                rect.y += 20f;
                EditorGUI.LabelField(rect, "x16", EditorStyles.miniLabel);
            }

            rect.y += 18f;
            EditorGUI.LabelField(rect, "Recursive", EditorStyles.miniLabel);
        }

        private static void DrawValues(ref Rect rect, ProjectRule rule, IList<string> paths)
        {
            rect.x += 85f;
            rect.y = _windowRect.y + 8f;
            rect.width = _windowRect.width - 85f - 8f;
            GUI.enabled = false;
            if (paths.Count == 1)
            {
                rule.Key = ((rule.Type == ProjectRule.KeyType.Path) ? paths[0] : Path.GetFileName(paths[0]));
            }
            else
            {
                rule.Key = "---";
            }

            EditorGUI.TextField(rect, GUIContent.none, rule.Key);
            GUI.enabled = true;
            rect.y += 19f;
            rule.Priority = EditorGUI.IntField(rect, GUIContent.none, rule.Priority);
            rect.width -= 72f;
            rect.y += 28f;
            DrawIconPopupMenu(rect, rule);
            if (rule.HasCustomIcon())
            {
                rect.y += 21f;
                rule.SmallIcon = (Texture2D)EditorGUI.ObjectField(rect, rule.SmallIcon, typeof(Texture2D), allowSceneObjects: false);
                rect.y += 17f;
                rule.LargeIcon = (Texture2D)EditorGUI.ObjectField(rect, rule.LargeIcon, typeof(Texture2D), allowSceneObjects: false);
            }

            rect.y += 18f;
            rule.IsIconRecursive = EditorGUI.Toggle(rect, rule.IsIconRecursive);
            rect.y += 22f;
            DrawBackgroundPopupMenu(rect, rule);
            if (rule.HasCustomBackground())
            {
                rect.y += 21f;
                rule.BackgroundTexture = (Texture2D)EditorGUI.ObjectField(rect, rule.BackgroundTexture, typeof(Texture2D), allowSceneObjects: false);
            }

            rect.y += 18f;
            rule.IsBackgroundRecursive = EditorGUI.Toggle(rect, rule.IsBackgroundRecursive);
        }

        private static void DrawPreview(ref Rect rect, ProjectRule rule)
        {
            rect.x += rect.width + 8f;
            rect.y = _windowRect.y + 32f + 15f;
            float num3 = (rect.width = (rect.height = 64f));
            GUI.DrawTexture(
                image: (!rule.HasLargeIcon())
                    ? ProjectEditorUtility.GetDefaultFolderIcon()
                    : (rule.HasCustomIcon() ? rule.LargeIcon : ProjectIconsStorage.GetIcons(rule.IconType).Item1), position: rect);
            rect.y += 44f;
            num3 = (rect.width = (rect.height = 16f));
            GUI.DrawTexture(
                image: (!rule.HasSmallIcon())
                    ? ProjectEditorUtility.GetDefaultFolderIcon()
                    : (rule.HasCustomIcon() ? rule.SmallIcon : ProjectIconsStorage.GetIcons(rule.IconType).Item2), position: rect);
            rect.y += 19f;
            rect.width = 64f;
            if (rule.HasBackground())
            {
                Texture2D image3 = (rule.HasCustomBackground() ? rule.BackgroundTexture : CoreBackgroundsStorage.GetBackground(rule.BackgroundType));
                GUI.DrawTexture(rect, image3);
            }

            rect.x += 13f;
            EditorGUI.LabelField(rect, "Folder");
        }

        private void DrawSeparators(ref Rect rect)
        {
            Color color = (EditorGUIUtility.isProSkin ? CoreEditorUtility.SEPARATOR_CLR_1_PRO : CoreEditorUtility.SEPARATOR_CLR_1_FREE);
            Color color2 = (EditorGUIUtility.isProSkin ? CoreEditorUtility.SEPARATOR_CLR_2_PRO : CoreEditorUtility.SEPARATOR_CLR_2_FREE);
            rect.x = 4f;
            rect.y = 48f;
            rect.width = 317f;
            rect.height = 1f;
            EditorGUI.DrawRect(rect, color);
            rect.y += 1f;
            EditorGUI.DrawRect(rect, color2);
            rect.y = base.position.height - 16f - 11f;
            EditorGUI.DrawRect(rect, color);
            rect.y += 1f;
            EditorGUI.DrawRect(rect, color2);
        }

        private void DrawButtons(ref Rect rect)
        {
            rect.x = 8f;
            rect.y = base.position.height - 16f - 6f;
            rect.width = 16f;
            rect.height = 16f;
            ButtonSettings(rect);
            rect.x += 22f;
            ButtonFilter(rect);
            rect.x += 22f;
            ButtonDefault(rect);
            rect.x = 199f;
            rect.width = 55f;
            ButtonCancel(rect);
            rect.x = 262f;
            ButtonApply(rect);
        }

        private void ChangeWindowSize(bool hasCustomIcon, bool hasCustomBackground)
        {
            Rect rect = base.position;
            float num = (hasCustomIcon ? 38f : 0f);
            float num2 = (hasCustomBackground ? 20f : 0f);
            float num3 = 164f + num + num2;
            if (num3 != rect.height)
            {
                rect.height = num3;
                base.position = rect;
                _windowRect.height = rect.height;
                _backgroundRect.height = rect.height - 2f;
            }
        }

        private static void DrawIconPopupMenu(Rect rect, ProjectRule rule)
        {
            string text = rule.IconType.ToString();
            if (GUI.Button(rect, new GUIContent(text), "popup"))
            {
                RainbowFoldersIconsMenu.ShowDropDown(rect, rule);
            }
        }

        private static void DrawBackgroundPopupMenu(Rect rect, ProjectRule rule)
        {
            string text = rule.BackgroundType.ToString();
            if (GUI.Button(rect, new GUIContent(text), "popup"))
            {
                RainbowFoldersBackgroundsMenu.ShowDropDown(rect, rule);
            }
        }

        private void ButtonSettings(Rect rect)
        {
            Texture2D settingsButtonIcon = ProjectEditorUtility.GetSettingsButtonIcon();
            if (GUI.Button(rect, new GUIContent(settingsButtonIcon, "All Rules"), GUIStyle.none))
            {
                ProjectRuleset.ShowInspector();
                Close();
            }
        }

        private void ButtonFilter(Rect rect)
        {
            Texture2D filterButtonIcon = ProjectEditorUtility.GetFilterButtonIcon();
            if (GUI.Button(rect, new GUIContent(filterButtonIcon, "Folder Rules"), GUIStyle.none))
            {
                ProjectRuleset.ShowInspector(AssetDatabase.LoadAssetAtPath<DefaultAsset>(_paths[0]));
                Close();
            }
        }

        private void ButtonDefault(Rect rect)
        {
            Texture2D deleteButtonIcon = ProjectEditorUtility.GetDeleteButtonIcon();
            if (GUI.Button(rect, new GUIContent(deleteButtonIcon, "Revert to Default"), GUIStyle.none))
            {
                _currentRule.Priority = 0;
                _currentRule.IconType = ProjectIcon.None;
                _currentRule.SmallIcon = null;
                _currentRule.LargeIcon = null;
                _currentRule.IsIconRecursive = false;
                _currentRule.BackgroundType = CoreBackground.None;
                _currentRule.BackgroundTexture = null;
                _currentRule.IsBackgroundRecursive = false;
            }
        }

        private void ButtonCancel(Rect rect)
        {
            if (GUI.Button(rect, "Cancel"))
            {
                Close();
            }
        }

        private void ButtonApply(Rect rect)
        {
            if (GUI.Button(rect, "Apply"))
            {
                for (int i = 0; i < _existingItems.Length; i++)
                {
                    _currentRule.Key = ((_currentRule.Type == ProjectRule.KeyType.Path) ? _paths[i] : Path.GetFileName(_paths[i]));
                    _ruleset.UpdateRule(_existingItems[i], _currentRule);
                }

                Close();
            }
        }
    }
}