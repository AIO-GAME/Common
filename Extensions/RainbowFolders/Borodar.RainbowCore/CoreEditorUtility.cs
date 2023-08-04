using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowCore
{
	public static class CoreEditorUtility
	{
		public static readonly Color POPUP_BORDER_CLR_FREE = new Color(0.51f, 0.51f, 0.51f);

		public static readonly Color POPUP_BORDER_CLR_PRO = new Color(0.13f, 0.13f, 0.13f);

		public static readonly Color POPUP_BACKGROUND_CLR_FREE = new Color(0.83f, 0.83f, 0.83f);

		public static readonly Color POPUP_BACKGROUND_CLR_PRO = new Color(0.18f, 0.18f, 0.18f);

		public static readonly Color SEPARATOR_CLR_1_FREE = new Color(0.65f, 0.65f, 0.65f, 1f);

		public static readonly Color SEPARATOR_CLR_2_FREE = new Color(0.9f, 0.9f, 0.9f, 1f);

		public static readonly Color SEPARATOR_CLR_1_PRO = new Color(0.13f, 0.13f, 0.13f, 1f);

		public static readonly Color SEPARATOR_CLR_2_PRO = new Color(0.22f, 0.22f, 0.22f, 1f);

		public static void CreateAsset<T>(string baseName, string forcedPath = "") where T : ScriptableObject
		{
			if (baseName.Contains("/"))
			{
				throw new ArgumentException("Base name should not contain slashes");
			}
			T val = ScriptableObject.CreateInstance<T>();
			string text;
			if (!string.IsNullOrEmpty(forcedPath))
			{
				text = forcedPath;
				Directory.CreateDirectory(forcedPath);
			}
			else
			{
				text = AssetDatabase.GetAssetPath(Selection.activeObject);
				if (string.IsNullOrEmpty(text))
				{
					text = "Assets";
				}
				else if (Path.GetExtension(text) != string.Empty)
				{
					text = text.Replace(Path.GetFileName(text), string.Empty);
				}
			}
			string path = AssetDatabase.GenerateUniqueAssetPath(string.Concat(text, "/", baseName, ".asset"));
			AssetDatabase.CreateAsset(val, path);
			AssetDatabase.SaveAssets();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = val;
		}

		public static IEnumerable<EditorWindow> GetAllWindowsByType(string type)
		{
			return from obj in Resources.FindObjectsOfTypeAll(typeof(EditorWindow))
				where obj.GetType().ToString() == type
				select (EditorWindow)obj;
		}

		public static bool SearchField(ref string query, ref Enum filter, Enum defaultFilter, params GUILayoutOption[] options)
		{
			string value = query;
			Enum objB = filter;
			bool result = false;
			GUILayout.BeginHorizontal();
			Rect rect = GUILayoutUtility.GetRect(GUIContent.none, "ToolbarSeachTextFieldPopup", options);
			rect.width -= 18f;
			Rect position = rect;
			position.width = 20f;
			filter = EditorGUI.EnumPopup(position, filter, "label");
			if (!object.Equals(filter, objB))
			{
				result = true;
			}
			query = EditorGUI.TextField(rect, "", query, "ToolbarSeachTextFieldPopup");
			if (query != null && !query.Equals(value))
			{
				result = true;
			}
			Rect position2 = rect;
			position2.x += rect.width;
			position2.width = 18f;
			if (GUI.Button(position2, "", "ToolbarSeachCancelButton"))
			{
				query = string.Empty;
				filter = defaultFilter;
				result = true;
				GUIUtility.keyboardControl = 0;
			}
			GUILayout.EndHorizontal();
			return result;
		}
	}
}
