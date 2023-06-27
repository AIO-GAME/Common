using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Utilities for Editor Panels.
	/// </summary>
	public static class DeEditorPanelUtils
	{
		private static Dictionary<EditorWindow, GUIContent> _winTitleContentByEditor;

		private static FieldInfo _fi_editorWindowParent;

		private static MethodInfo _miRepaintCurrentEditor;

		/// <summary>
		/// Connects to a <see cref="T:UnityEngine.ScriptableObject" /> asset.
		/// If the asset already exists at the given path, loads it and returns it.
		/// Otherwise, depending on the given parameters, either returns NULL or automatically creates it before loading and returning it.
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="adbFilePath">File path (relative to Unity's project folder)</param>
		/// <param name="createIfMissing">If TRUE and the requested asset doesn't exist, forces its creation</param>
		/// <param name="createFoldersIfMissing">If TRUE also creates the path folders if they don't exist</param>
		public static T ConnectToSourceAsset<T>(string adbFilePath, bool createIfMissing = false, bool createFoldersIfMissing = false) where T : ScriptableObject
		{
			if (!DeEditorFileUtils.AssetExists(adbFilePath))
			{
				if (!createIfMissing)
				{
					return null;
				}
				CreateScriptableAsset<T>(adbFilePath, createFoldersIfMissing);
			}
			T val = (T)AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
			if ((Object)val == (Object)null)
			{
				CreateScriptableAsset<T>(adbFilePath, createFoldersIfMissing);
				val = (T)AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
			}
			return val;
		}

		/// <summary>
		/// Check if the <see cref="T:UnityEngine.ScriptableObject" /> at the given path exists and eventually if it's available
		/// </summary>
		/// <param name="adbFilePath">File path (relative to Unity's project folder)</param>
		/// <param name="checkIfAvailable">If TRUE also check if the file is available
		/// (file can be unavailable if it was deleted outside Unity, or if Unity is just starting)</param>
		/// <returns></returns>
		public static bool SourceAssetExists<T>(string adbFilePath, bool checkIfAvailable = true) where T : ScriptableObject
		{
			if (!DeEditorFileUtils.AssetExists(adbFilePath))
			{
				return false;
			}
			if (!checkIfAvailable)
			{
				return true;
			}
			return (Object)(T)AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T)) != (Object)null;
		}

		/// <summary>
		/// Returns TRUE if the given <see cref="T:UnityEditor.EditorWindow" /> is dockable, FALSE if instead it's a utility window
		/// </summary>
		/// <param name="editor"></param>
		/// <returns></returns>
		public static bool IsDockableWindow(EditorWindow editor)
		{
			if (_fi_editorWindowParent == null)
			{
				_fi_editorWindowParent = editor.GetType().GetField("m_Parent", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			object value = _fi_editorWindowParent.GetValue(editor);
			if (value == null)
			{
				Debug.LogError("DeEditorPanelUtils.IsDockableWindow > parent is NULL, you should call this after the first GUI call happened");
				return false;
			}
			return value.GetType().ToString() == "UnityEditor.DockArea";
		}

		/// <summary>
		/// Sets the icon and title of an editor window. Works with older versions of Unity, where the titleContent property wasn't available.
		/// </summary>
		/// <param name="editor">Reference to the editor panel whose icon to set</param>
		/// <param name="icon">Icon to apply</param>
		/// <param name="title">Title. If NULL doesn't change it</param>
		public static void SetWindowTitle(EditorWindow editor, Texture icon, string title = null)
		{
			if (_winTitleContentByEditor == null)
			{
				_winTitleContentByEditor = new Dictionary<EditorWindow, GUIContent>();
			}
			GUIContent gUIContent;
			if (_winTitleContentByEditor.ContainsKey(editor))
			{
				gUIContent = _winTitleContentByEditor[editor];
				if (gUIContent != null)
				{
					if (gUIContent.image != icon)
					{
						gUIContent.image = icon;
					}
					if (title != null && gUIContent.text != title)
					{
						gUIContent.text = title;
					}
					return;
				}
				_winTitleContentByEditor.Remove(editor);
			}
			gUIContent = GetWinTitleContent(editor);
			if (gUIContent != null)
			{
				if (gUIContent.image != icon)
				{
					gUIContent.image = icon;
				}
				if (title != null && gUIContent.text != title)
				{
					gUIContent.text = title;
				}
				_winTitleContentByEditor.Add(editor, gUIContent);
			}
		}

		/// <summary>
		/// Repaints the currently focues editor
		/// </summary>
		public static void RepaintCurrentEditor()
		{
			if (_miRepaintCurrentEditor == null)
			{
				_miRepaintCurrentEditor = typeof(EditorGUIUtility).GetMethod("RepaintCurrentWindow", BindingFlags.Static | BindingFlags.NonPublic);
			}
			_miRepaintCurrentEditor.Invoke(null, null);
		}

		private static void CreateScriptableAsset<T>(string adbFilePath, bool createFoldersIfMissing) where T : ScriptableObject
		{
			T asset = ScriptableObject.CreateInstance<T>();
			if (createFoldersIfMissing)
			{
				string[] array = adbFilePath.Split(DeEditorFileUtils.ADBPathSlash.ToCharArray()[0]);
				string text = "Assets";
				for (int i = 1; i < array.Length - 1; i++)
				{
					string text2 = array[i];
					if (!DeEditorFileUtils.AssetExists(string.Concat(text, DeEditorFileUtils.ADBPathSlash, text2)))
					{
						AssetDatabase.CreateFolder(text, text2);
					}
					text = string.Concat(text, DeEditorFileUtils.ADBPathSlash, text2);
				}
			}
			AssetDatabase.CreateAsset(asset, adbFilePath);
		}

		private static GUIContent GetWinTitleContent(EditorWindow editor)
		{
			PropertyInfo property = typeof(EditorWindow).GetProperty("cachedTitleContent", BindingFlags.Instance | BindingFlags.NonPublic);
			if (property == null)
			{
				return null;
			}
			return property.GetValue(editor, null) as GUIContent;
		}
	}
}
