using System;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders
{
	public static class ProjectPreferences
	{
		private abstract class EditorPrefsItem<T>
		{
			protected readonly string Key;

			protected readonly GUIContent Label;

			protected readonly T DefaultValue;

			public abstract T Value { get; set; }

			protected EditorPrefsItem(string key, GUIContent label, T defaultValue)
			{
				if (string.IsNullOrEmpty(key))
				{
					throw new ArgumentNullException("key");
				}
				Key = key;
				Label = label;
				DefaultValue = defaultValue;
			}

			public abstract void Draw();

			public static implicit operator T(EditorPrefsItem<T> s)
			{
				return s.Value;
			}
		}

		private class EditorPrefsString : EditorPrefsItem<string>
		{
			public override string Value
			{
				get
				{
					return EditorPrefs.GetString(Key, DefaultValue);
				}
				set
				{
					EditorPrefs.SetString(Key, value);
				}
			}

			public EditorPrefsString(string key, GUIContent label, string defaultValue)
				: base(key, label, defaultValue)
			{
			}

			public override void Draw()
			{
				EditorGUIUtility.labelWidth = 100f;
				Value = EditorGUILayout.TextField(Label, Value);
			}
		}

		private class EditorPrefsModifierKey : EditorPrefsItem<EventModifiers>
		{
			public override EventModifiers Value
			{
				get
				{
					int @int = EditorPrefs.GetInt(Key, (int)DefaultValue);
					if (!Enum.IsDefined(typeof(EventModifiers), @int))
					{
						return DefaultValue;
					}
					return (EventModifiers)@int;
				}
				set
				{
					EditorPrefs.SetInt(Key, (int)value);
				}
			}

			public EditorPrefsModifierKey(string key, GUIContent label, EventModifiers defaultValue)
				: base(key, label, defaultValue)
			{
			}

			public override void Draw()
			{
				Value = (EventModifiers)(object)EditorGUILayout.EnumPopup(Label, Value);
			}
		}

		private class EditorPrefsBoolean : EditorPrefsItem<bool>
		{
			public override bool Value
			{
				get
				{
					return EditorPrefs.GetBool(Key, DefaultValue);
				}
				set
				{
					bool num = Value != value;
					EditorPrefs.SetBool(Key, value);
					if (num)
					{
						OnChange(value);
					}
				}
			}

			protected EditorPrefsBoolean(string key, GUIContent label, bool defaultValue)
				: base(key, label, defaultValue)
			{
			}

			public override void Draw()
			{
				EditorGUIUtility.labelWidth = 150f;
				Value = EditorGUILayout.Toggle(Label, Value);
			}

			protected virtual void OnChange(bool value)
			{
			}
		}

		private class EditorPrefsBooleanRepaint : EditorPrefsBoolean
		{
			public EditorPrefsBooleanRepaint(string key, GUIContent label, bool defaultValue)
				: base(key, label, defaultValue)
			{
			}

			protected override void OnChange(bool value)
			{
				EditorApplication.RepaintProjectWindow();
			}
		}

		private const float PREF_LABEL_WIDTH = 150f;

		private const string HOME_FOLDER_PREF_KEY = "Borodar.RainbowFolders.HomeFolder.";

		private const string HOME_FOLDER_DEFAULT = "Assets/Plugins/RainbowAssets/RainbowFolders";

		private const string HOME_FOLDER_HINT = "Change this setting to the new location of the \"Rainbow Folders\" if you move the folder around in your project.";

		private const string MOD_KEY_PREF_KEY = "Borodar.RainbowFolders.EditMod.";

		private const string MOD_KEY_HINT = "Modifier key that is used to show configuration dialogue when clicking on a folder icon.";

		private const EventModifiers MOD_KEY_DEFAULT = EventModifiers.Alt;

		private const string PROJECT_TREE_PREF_KEY = "Borodar.RainbowFolders.ShowProjectTree.";

		private const string PROJECT_TREE_HINT = "(Experimental)\nChange this setting to show/hide the \"branches\" in the project window.";

		private const bool PROJECT_TREE_DEFAULT = true;

		private const string ROW_SHADING_PREF_KEY = "Borodar.RainbowFolders.RowShading.";

		private const string ROW_SHADING_HINT = "Change this settings to enable/disable row shading in the project window.";

		private const bool ROW_SHADING_DEFAULT = true;

		private static readonly EditorPrefsString HOME_FOLDER_PREF;

		private static readonly EditorPrefsModifierKey MODIFIER_KEY_PREF;

		private static readonly EditorPrefsBoolean PROJECT_TREE_PREF;

		private static readonly EditorPrefsBoolean ROW_SHADING_PREF;

		public static string HomeFolder;

		public static EventModifiers ModifierKey;

		public static bool ShowProjectTree;

		public static bool DrawRowShading;

		private static string ProjectName
		{
			get
			{
				string[] array = Application.dataPath.Split('/');
				return array[array.Length - 2];
			}
		}

		static ProjectPreferences()
		{
			GUIContent label = new GUIContent("Folder Location", "Change this setting to the new location of the \"Rainbow Folders\" if you move the folder around in your project.");
			HOME_FOLDER_PREF = new EditorPrefsString(string.Concat("Borodar.RainbowFolders.HomeFolder.", ProjectName), label, "Assets/Plugins/RainbowAssets/RainbowFolders");
			HomeFolder = HOME_FOLDER_PREF.Value;
			GUIContent label2 = new GUIContent("Modifier Key", "Modifier key that is used to show configuration dialogue when clicking on a folder icon.");
			MODIFIER_KEY_PREF = new EditorPrefsModifierKey(string.Concat("Borodar.RainbowFolders.EditMod.", ProjectName), label2, EventModifiers.Alt);
			ModifierKey = MODIFIER_KEY_PREF.Value;
			GUIContent label3 = new GUIContent("Project Tree", "(Experimental)\nChange this setting to show/hide the \"branches\" in the project window.");
			PROJECT_TREE_PREF = new EditorPrefsBooleanRepaint(string.Concat("Borodar.RainbowFolders.ShowProjectTree.", ProjectName), label3, defaultValue: true);
			ShowProjectTree = PROJECT_TREE_PREF.Value;
			GUIContent label4 = new GUIContent("Row Shading", "Change this settings to enable/disable row shading in the project window.");
			ROW_SHADING_PREF = new EditorPrefsBooleanRepaint(string.Concat("Borodar.RainbowFolders.RowShading.", ProjectName), label4, defaultValue: true);
			DrawRowShading = ROW_SHADING_PREF.Value;
		}

		[SettingsProvider]
		public static SettingsProvider CreateSettingProvider()
		{
			return new SettingsProvider("Borodar/RainbowFolders", SettingsScope.Project)
			{
				label = "Rainbow Folders",
				guiHandler = delegate
				{
					EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
					EditorGUILayout.Separator();
					HOME_FOLDER_PREF.Draw();
					HomeFolder = HOME_FOLDER_PREF.Value;
					TinySeparator();
					MODIFIER_KEY_PREF.Draw();
					ModifierKey = MODIFIER_KEY_PREF.Value;
					EditorGUILayout.Separator();
					EditorGUILayout.LabelField("Enhancements", EditorStyles.boldLabel);
					EditorGUILayout.Separator();
					PROJECT_TREE_PREF.Draw();
					ShowProjectTree = PROJECT_TREE_PREF.Value;
					TinySeparator();
					ROW_SHADING_PREF.Draw();
					DrawRowShading = ROW_SHADING_PREF.Value;
					GUILayout.FlexibleSpace();
					EditorGUILayout.LabelField("Version 2.1.0", EditorStyles.centeredGreyMiniLabel);
				}
			};
		}

		private static void TinySeparator()
		{
			GUILayoutUtility.GetRect(0f, 0f);
		}
	}
}
