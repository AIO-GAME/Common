using System;

using Borodar.RainbowCore;

using UnityEditor;

using UnityEngine;

namespace Borodar.RainbowFolders.Settings
{
    [CustomPropertyDrawer(typeof(ProjectRule))]
    public class ProjectRuleDrawer : PropertyDrawer
    {
        private class SerializedItemWrapper
        {
            public readonly SerializedProperty Property;

            public readonly SerializedProperty FolderKey;

            public readonly SerializedProperty FolderKeyType;

            public readonly SerializedProperty Priority;

            public readonly SerializedProperty IconType;

            public readonly SerializedProperty SmallIcon;

            public readonly SerializedProperty LargeIcon;

            public readonly SerializedProperty IconRecursive;

            public readonly SerializedProperty BackgroundType;

            public readonly SerializedProperty Background;

            public readonly SerializedProperty BackgroundRecursive;

            public readonly bool HasIcon;

            public readonly bool HasCustomIcon;

            public readonly bool HasBackground;

            public readonly bool HasCustomBackground;

            public SerializedItemWrapper(SerializedProperty property)
            {
                Property = property;
                FolderKey = property.FindPropertyRelative("Key");
                FolderKeyType = property.FindPropertyRelative("Type");
                Priority = property.FindPropertyRelative("Priority");
                IconType = property.FindPropertyRelative("IconType");
                SmallIcon = property.FindPropertyRelative("SmallIcon");
                LargeIcon = property.FindPropertyRelative("LargeIcon");
                IconRecursive = property.FindPropertyRelative("IsIconRecursive");
                BackgroundType = property.FindPropertyRelative("BackgroundType");
                Background = property.FindPropertyRelative("BackgroundTexture");
                BackgroundRecursive = property.FindPropertyRelative("IsBackgroundRecursive");
                HasIcon = IconType.intValue != 0;
                HasCustomIcon = IconType.intValue == 1;
                HasBackground = BackgroundType.intValue != 0;
                HasCustomBackground = BackgroundType.intValue == 1;
            }
        }

        private const float PADDING = 8f;

        private const float SPACING = 1f;

        private const float LINE_HEIGHT = 16f;

        private const float LABELS_WIDTH = 100f;

        private const float PREVIEW_SIZE_SMALL = 16f;

        private const float PREVIEW_SIZE_LARGE = 64f;

        private const float PROPERTY_HEIGHT = 124f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.FindPropertyRelative("IsHidden").boolValue)
            {
                Rect originalPosition = position;
                SerializedItemWrapper item = new SerializedItemWrapper(property);
                EditorGUI.BeginChangeCheck();
                DrawLabels(ref position, item);
                DrawValues(ref position, originalPosition, item);
                DrawPreview(ref position, originalPosition, item);
                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.FindPropertyRelative("IsHidden").boolValue)
            {
                return 0f;
            }
            SerializedProperty serializedProperty = property.FindPropertyRelative("IconType");
            SerializedProperty serializedProperty2 = property.FindPropertyRelative("BackgroundType");
            bool num = serializedProperty.intValue == 1;
            bool flag = serializedProperty2.intValue == 1;
            float num2 = 124f;
            if (num)
            {
                num2 += 32f;
            }
            if (flag)
            {
                num2 += 16f;
            }
            return num2;
        }

        private static void DrawLabels(ref Rect position, SerializedItemWrapper item)
        {
            position.y += 8f;
            position.width = 92f;
            position.height = 16f;
            ProjectRule.KeyType keyType = (ProjectRule.KeyType)Enum.GetValues(typeof(ProjectRule.KeyType)).GetValue(item.FolderKeyType.enumValueIndex);
            item.FolderKeyType.enumValueIndex = (int)(ProjectRule.KeyType)(object)EditorGUI.EnumPopup(position, keyType);
            position.y += 20f;
            EditorGUI.LabelField(position, "Priority");
            position.y += 20f;
            EditorGUI.LabelField(position, "Icon");
            if (item.HasCustomIcon)
            {
                position.y += 17f;
                EditorGUI.LabelField(position, "x16");
                position.y += 17f;
                EditorGUI.LabelField(position, "x64");
            }
            position.y += 17f;
            EditorGUI.LabelField(position, "Recursive");
            position.y += 20f;
            EditorGUI.LabelField(position, "Background");
            if (item.HasCustomBackground)
            {
                position.y += 17f;
                EditorGUI.LabelField(position, "x16");
            }
            position.y += 17f;
            EditorGUI.LabelField(position, "Recursive");
        }

        private static void DrawValues(ref Rect position, Rect originalPosition, SerializedItemWrapper item)
        {
            position.x += 100f;
            position.y = originalPosition.y + 8f;
            position.width = originalPosition.width - 100f;
            EditorGUI.PropertyField(position, item.FolderKey, GUIContent.none);
            position.y += 20f;
            EditorGUI.PropertyField(position, item.Priority, GUIContent.none);
            position.width = originalPosition.width - 100f - 64f - 8f;
            position.y += 20f;
            DrawIconPopupMenu(position, item.Property, item.HasCustomIcon, item.IconType.intValue);
            if (item.HasCustomIcon)
            {
                position.y += 17f + (EditorGUIUtility.isProSkin ? 1f : 0f);
                EditorGUI.PropertyField(position, item.SmallIcon, GUIContent.none);
                position.y += 17f;
                EditorGUI.PropertyField(position, item.LargeIcon, GUIContent.none);
            }
            position.y += 16f + (EditorGUIUtility.isProSkin ? 0f : 1f);
            EditorGUI.PropertyField(position, item.IconRecursive, GUIContent.none);
            position.y += 20f;
            DrawBackgroundPopupMenu(position, item.Property, item.HasCustomBackground, item.BackgroundType.intValue);
            if (item.HasCustomBackground)
            {
                position.y += 17f;
                EditorGUI.PropertyField(position, item.Background, GUIContent.none);
            }
            position.y += 16f + (EditorGUIUtility.isProSkin ? 0f : 1f);
            EditorGUI.PropertyField(position, item.BackgroundRecursive, GUIContent.none);
        }

        private static void DrawPreview(ref Rect position, Rect originalPosition, SerializedItemWrapper item)
        {
            Texture2D texture2D = null;
            Texture2D texture2D2 = null;
            if (item.HasIcon)
            {
                if (item.HasCustomIcon)
                {
                    texture2D = (Texture2D)item.SmallIcon.objectReferenceValue;
                    texture2D2 = (Texture2D)item.LargeIcon.objectReferenceValue;
                }
                else
                {
                    var icons = ProjectIconsStorage.GetIcons(item.IconType.intValue);
                    if (icons != null)
                    {
                        texture2D2 = icons.Item1;
                        texture2D = icons.Item2;
                    }
                }
            }
            if (texture2D == null)
            {
                texture2D = ProjectEditorUtility.GetDefaultFolderIcon();
            }
            if (texture2D2 == null)
            {
                texture2D2 = ProjectEditorUtility.GetDefaultFolderIcon();
            }
            position.x += position.width + 8f;
            position.y = originalPosition.y + 32f + 1f + 8f;
            float num3 = (position.width = (position.height = 64f));
            GUI.DrawTexture(position, texture2D2);
            position.y += 44f;
            num3 = (position.width = (position.height = 16f));
            GUI.DrawTexture(position, texture2D);
            position.y += 20f;
            position.width = 64f;
            if (item.HasBackground)
            {
                Texture2D texture2D3 = (item.HasCustomBackground ? ((Texture2D)item.Background.objectReferenceValue) : CoreBackgroundsStorage.GetBackground(item.BackgroundType.intValue));
                if (texture2D3 != null)
                {
                    GUI.DrawTexture(position, texture2D3);
                }
            }
            position.x += 13f;
            EditorGUI.LabelField(position, "Folder");
        }

        private static void DrawIconPopupMenu(Rect rect, SerializedProperty property, bool hasCustomIcon, int iconType)
        {
            object obj;
            if (!hasCustomIcon)
            {
                ProjectIcon projectIcon = (ProjectIcon)iconType;
                obj = projectIcon.ToString();
            }
            else
            {
                obj = "Custom";
            }
            string text = (string)obj;
            if (GUI.Button(rect, new GUIContent(text), "MiniPopup"))
            {
                RainbowFoldersIconsMenu.ShowDropDown(rect, property);
            }
        }

        private static void DrawBackgroundPopupMenu(Rect rect, SerializedProperty property, bool hasCustomBackground, int backgroundType)
        {
            object obj;
            if (!hasCustomBackground)
            {
                CoreBackground coreBackground = (CoreBackground)backgroundType;
                obj = coreBackground.ToString();
            }
            else
            {
                obj = "Custom";
            }
            string text = (string)obj;
            if (GUI.Button(rect, new GUIContent(text), "MiniPopup"))
            {
                RainbowFoldersBackgroundsMenu.ShowDropDown(rect, property);
            }
        }
    }
}
