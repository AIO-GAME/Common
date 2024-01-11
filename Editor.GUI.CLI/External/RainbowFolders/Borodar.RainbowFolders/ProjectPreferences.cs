using System;
using UnityEditor;
using UnityEngine;

namespace AIO.RainbowFolders
{
    internal static class ProjectPreferences
    {
        internal abstract class EditorPrefsItem<T>
        {
            protected readonly string Key;

            protected readonly GUIContent Label;

            protected readonly T DefaultValue;

            public abstract T Value { get; set; }

            public readonly Action<T> OnUpdate;

            protected EditorPrefsItem(string key, GUIContent label, T defaultValue, Action<T> onUpdate = null)
            {
                OnUpdate = onUpdate;
                if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
                Key = key;
                Label = label;
                DefaultValue = defaultValue;
                Update();
            }

            public void Update()
            {
                OnUpdate?.Invoke(Value);
            }

            public void Draw()
            {
                OnDraw();
                OnUpdate?.Invoke(Value);
            }

            protected abstract void OnDraw();

            public static implicit operator T(EditorPrefsItem<T> s)
            {
                return s.Value;
            }
        }

        internal class EditorPrefsString : EditorPrefsItem<string>
        {
            public override string Value
            {
                get => EditorPrefs.GetString(Key, DefaultValue);
                set => EditorPrefs.SetString(Key, value);
            }

            public EditorPrefsString(string key, GUIContent label, string defaultValue, Action<string> onUpdate = null)
                : base(key, label, defaultValue, onUpdate)
            {
            }

            protected override void OnDraw()
            {
                EditorGUIUtility.labelWidth = 100f;
                Value = EditorGUILayout.TextField(Label, Value);
            }
        }

        internal class EditorPrefsModifierKey : EditorPrefsItem<EventModifiers>
        {
            public override EventModifiers Value
            {
                get
                {
                    var @int = EditorPrefs.GetInt(Key, (int)DefaultValue);
                    if (!Enum.IsDefined(typeof(EventModifiers), @int))
                        return DefaultValue;
                    return (EventModifiers)@int;
                }
                set => EditorPrefs.SetInt(Key, (int)value);
            }

            public EditorPrefsModifierKey(string key, GUIContent label, EventModifiers defaultValue, Action<EventModifiers> onUpdate = null)
                : base(key, label, defaultValue, onUpdate)
            {
            }

            protected override void OnDraw()
            {
                Value = (EventModifiers)EditorGUILayout.EnumPopup(Label, Value);
            }
        }

        internal class EditorPrefsBoolean : EditorPrefsItem<bool>
        {
            public override bool Value
            {
                get => EditorPrefs.GetBool(Key, DefaultValue);
                set
                {
                    var num = Value != value;
                    EditorPrefs.SetBool(Key, value);
                    if (num) OnChange(value);
                }
            }

            protected EditorPrefsBoolean(string key, GUIContent label, bool defaultValue, Action<bool> onUpdate = null)
                : base(key, label, defaultValue, onUpdate)
            {
            }

            protected override void OnDraw()
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    Value = EditorGUILayout.ToggleLeft(Label, Value);
                }
            }

            protected virtual void OnChange(bool value)
            {
            }
        }

        internal class EditorPrefsBooleanRepaint : EditorPrefsBoolean
        {
            public EditorPrefsBooleanRepaint(string key, GUIContent label, bool defaultValue, Action<bool> onUpdate = null)
                : base(key, label, defaultValue, onUpdate)
            {
            }

            protected override void OnChange(bool value)
            {
                EditorApplication.RepaintProjectWindow();
            }
        }

        internal static EditorPrefsString HOME_FOLDER_PREF;

        internal static EditorPrefsModifierKey MODIFIER_KEY_PREF;

        internal static EditorPrefsBoolean PROJECT_TREE_PREF;

        internal static EditorPrefsBoolean ROW_SHADING_PREF;

        internal static EditorPrefsBoolean EP_DRAW_CUSTOM_BACKGROUND;

        internal static EditorPrefsBoolean EP_REPLACE_FOLDERICONS;

        internal static EditorPrefsBoolean EP_ENABLE;

        public static string HomeFolder;

        public static EventModifiers ModifierKey;

        public static bool ShowProjectTree;

        public static bool DrawRowShading;

        public static bool DrawCustomBackground;

        public static bool ReplaceFolderIcons;

        /// <summary>
        /// 插件是否启用
        /// </summary>
        public static bool Enable;

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            var array = Application.dataPath.Split('/');
            var projectName = array[array.Length - 2];

            var label = new GUIContent("Folder Location", "Change this setting to the new location of the \"Rainbow Folders\" if you move the folder around in your project.");
            HOME_FOLDER_PREF = new EditorPrefsString(string.Concat("AIO.RainbowFolders.HomeFolder.", projectName),
                label, "Assets/Editor/Setting/ProjectRule", v => { HomeFolder = v; });

            var label2 = new GUIContent("Modifier Key", "Modifier key that is used to show configuration dialogue when clicking on a folder icon.");
            MODIFIER_KEY_PREF = new EditorPrefsModifierKey(string.Concat("AIO.RainbowFolders.EditMod.", projectName),
                label2, EventModifiers.Alt, v => { ModifierKey = v; });

            var label3 = new GUIContent("绘制 项目树", "Draw Project Tree");
            PROJECT_TREE_PREF = new EditorPrefsBooleanRepaint(string.Concat("AIO.RainbowFolders.ShowProjectTree.", projectName),
                label3, true, v => { ShowProjectTree = v; });

            var label4 = new GUIContent("绘制 行阴影", "Draw Project Row Shading");
            ROW_SHADING_PREF = new EditorPrefsBooleanRepaint(string.Concat("AIO.RainbowFolders.RowShading.", projectName),
                label4, true, v => { DrawRowShading = v; });

            var label5 = new GUIContent("绘制 文件夹 背景色", "Draw Project Foldouts Background");
            EP_DRAW_CUSTOM_BACKGROUND = new EditorPrefsBooleanRepaint(string.Concat("AIO.RainbowFolders.DrawCustomBackground.", projectName),
                label5, true, v => { DrawCustomBackground = v; });

            var label6 = new GUIContent("绘制 文件夹 自定义ICON", "Draw Project Replace Folder Icons");
            EP_REPLACE_FOLDERICONS = new EditorPrefsBooleanRepaint(string.Concat("AIO.RainbowFolders.ReplaceFolderIcons.", projectName),
                label6, true, v => { ReplaceFolderIcons = v; });

            var label7 = new GUIContent("开启 插件", "enable / disable");
            EP_ENABLE = new EditorPrefsBooleanRepaint(string.Concat("AIO.RainbowFolders.Enable.", projectName),
                label7, true, v =>
                {
                    Enable = v;
                    if (Enable) RainbowFoldersGUI.Initialize();
                });
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider()
        {
            return new SettingsProvider("AIO/Borda Rainbow Folders", SettingsScope.User)
            {
                label = "Rainbow Folders",
                guiHandler = delegate
                {
                    EP_ENABLE.Draw();
                    Enable = EP_ENABLE.Value;
                    GUILayoutUtility.GetRect(0f, 0f);

                    if (Enable == false) return;

                    EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
                    EditorGUILayout.Separator();

                    HOME_FOLDER_PREF.Draw();
                    HomeFolder = HOME_FOLDER_PREF.Value;
                    GUILayoutUtility.GetRect(0f, 0f);

                    MODIFIER_KEY_PREF.Draw();
                    ModifierKey = MODIFIER_KEY_PREF.Value;

                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Enhancements", EditorStyles.boldLabel);
                    EditorGUILayout.Separator();

                    PROJECT_TREE_PREF.Draw();
                    ShowProjectTree = PROJECT_TREE_PREF.Value;
                    GUILayoutUtility.GetRect(0f, 0f);

                    ROW_SHADING_PREF.Draw();
                    DrawRowShading = ROW_SHADING_PREF.Value;
                    GUILayoutUtility.GetRect(0f, 0f);

                    EP_DRAW_CUSTOM_BACKGROUND.Draw();
                    DrawCustomBackground = EP_DRAW_CUSTOM_BACKGROUND.Value;
                    GUILayoutUtility.GetRect(0f, 0f);

                    EP_REPLACE_FOLDERICONS.Draw();
                    ReplaceFolderIcons = EP_REPLACE_FOLDERICONS.Value;
                    GUILayoutUtility.GetRect(0f, 0f);

                    GUILayout.FlexibleSpace();
                    EditorGUILayout.LabelField("Version 2.1.0", EditorStyles.centeredGreyMiniLabel);
                }
            };
        }
    }
}