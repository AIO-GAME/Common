using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// File utils
	/// </summary>
	public static class DeEditorFileUtils
	{
		/// <summary>Path slash for AssetDatabase format</summary>
		public static readonly string ADBPathSlash;

		/// <summary>Path slash to replace for AssetDatabase format</summary>
		public static readonly string ADBPathSlashToReplace;

		/// <summary>Current OS path slash</summary>
		public static readonly string PathSlash;

		/// <summary>Path slash to replace on current OS</summary>
		public static readonly string PathSlashToReplace;

		private static readonly StringBuilder _Strb;

		private static readonly char[] _ValidFilenameChars;

		private static string _fooProjectPath;

		/// <summary>
		/// Full path to project directory, without final slash.
		/// </summary>
		public static string projectPath
		{
			get
			{
				if (_fooProjectPath == null)
				{
					_fooProjectPath = Application.dataPath;
					_fooProjectPath = _fooProjectPath.Substring(0, _fooProjectPath.LastIndexOf(ADBPathSlash));
					_fooProjectPath = _fooProjectPath.Replace(ADBPathSlash, PathSlash);
				}
				return _fooProjectPath;
			}
		}

		/// <summary>
		/// Full path to project's Assets directory, without final slash.
		/// </summary>
		public static string assetsPath => string.Concat(projectPath, PathSlash, "Assets");

		static DeEditorFileUtils()
		{
			ADBPathSlash = "/";
			ADBPathSlashToReplace = "\\";
			_Strb = new StringBuilder();
			_ValidFilenameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-()!.$&+- ".ToCharArray();
			bool num = Application.platform == RuntimePlatform.WindowsEditor;
			PathSlash = (num ? "\\" : "/");
			PathSlashToReplace = (num ? "/" : "\\");
		}

		/// <summary>
		/// Returns TRUE if the given path is an absolute path
		/// </summary>
		public static bool IsFullPath(string path)
		{
			return path[1] == ':';
		}

		/// <summary>
		/// Returns TRUE if the given path is an AssetDatabase path
		/// </summary>
		public static bool IsADBPath(string path)
		{
			return path.StartsWith("Assets");
		}

		/// <summary>
		/// Returns TRUE if the given GUID refers to a valid and existing project folder
		/// </summary>
		public static bool IsProjectFolder(string assetGuid)
		{
			if (string.IsNullOrEmpty(assetGuid))
			{
				return false;
			}
			string text = AssetDatabase.GUIDToAssetPath(assetGuid);
			if (!string.IsNullOrEmpty(text))
			{
				return Directory.Exists(ADBPathToFullPath(text));
			}
			return false;
		}

		/// <summary>
		/// Converts the given project-relative path to a full path
		/// </summary>
		public static string ADBPathToFullPath(string adbPath)
		{
			adbPath = adbPath.Replace(ADBPathSlash, PathSlash);
			return string.Concat(projectPath, PathSlash, adbPath);
		}

		/// <summary>
		/// Converts the given full path to a project-relative path
		/// </summary>
		public static string FullPathToADBPath(string fullPath)
		{
			return fullPath.Substring(projectPath.Length + 1).Replace(ADBPathSlashToReplace, ADBPathSlash);
		}

		/// <summary>
		/// Returns TRUE if the file/directory at the given path exists.
		/// </summary>
		/// <param name="adbPath">Path, relative to Unity's project folder</param>
		public static bool AssetExists(string adbPath)
		{
			string path = ADBPathToFullPath(adbPath);
			if (!File.Exists(path))
			{
				return Directory.Exists(path);
			}
			return true;
		}

		/// <summary>
		/// Validates the string as a valid fileName
		/// (uses commonly accepted characters an all systems instead of system-specific ones).<para />
		/// BEWARE: doesn't check for reserved words
		/// </summary>
		/// <param name="s">string to replace</param>
		/// <param name="minLength">Minimum length for considering the string valid</param>
		public static bool IsValidFileName(string s, int minLength = 2)
		{
			if (string.IsNullOrEmpty(s) || s.Length < minLength)
			{
				return false;
			}
			foreach (char value in s)
			{
				if (Array.IndexOf(_ValidFilenameChars, value) == -1)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns TRUE if the given filepath is within this Unity project Assets folder
		/// </summary>
		/// <param name="fullFilePath">Full file path</param>
		public static bool FilePathIsWithinUnityProjectAssets(string fullFilePath)
		{
			return ApplySystemDirectorySeparators(fullFilePath).StartsWith(assetsPath);
		}

		/// <summary>
		/// Returns the given string stripped of any invalid filename characters.<para />
		/// BEWARE: doesn't check for reserved words
		/// </summary>
		/// <param name="s">string to replace</param>
		/// <param name="replaceWith">Character to use as replacement for invalid ones</param>
		public static string ConvertToValidFilename(string s, char replaceWith = '_')
		{
			_Strb.Length = 0;
			char[] array = s.ToCharArray();
			foreach (char c in array)
			{
				_Strb.Append((Array.IndexOf(_ValidFilenameChars, c) == -1) ? replaceWith : c);
			}
			return _Strb.ToString();
		}

		/// <summary>
		/// Returns the given path with all slashes converted to the correct ones used by the system
		/// </summary>
		public static string ApplySystemDirectorySeparators(string path)
		{
			return path.Replace(PathSlashToReplace, PathSlash);
		}

		/// <summary>
		/// Returns the asset path of the given GUID (relative to Unity project's folder),
		/// or an empty string if either the GUID is invalid or the related path doesn't exist.
		/// </summary>
		public static string GUIDToExistingAssetPath(string guid)
		{
			if (string.IsNullOrEmpty(guid))
			{
				return "";
			}
			string text = AssetDatabase.GUIDToAssetPath(guid);
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}
			if (AssetExists(text))
			{
				return text;
			}
			return "";
		}

		public static void CreateScriptableObjectInCurrentFolder<T>() where T : ScriptableObject
		{
			if (Selection.activeObject == null)
			{
				return;
			}
			string text = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (text == "")
			{
				text = "Assets";
			}
			else if (Path.GetExtension(text) != "")
			{
				text = text.Substring(0, text.IndexOf('.'));
			}
			if (!Directory.Exists(ADBPathToFullPath(text)))
			{
				Debug.LogWarning("DeEditorUtils.CreateScriptableObjectInCurrentFolder ► No valid project folder selected");
				return;
			}
			string text2 = typeof(T).ToString();
			int num = text2.LastIndexOf('.');
			if (num != -1)
			{
				text2 = text2.Substring(num + 1);
			}
			string path = AssetDatabase.GenerateUniqueAssetPath(string.Concat(text, $"/New {text2}.asset"));
			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), path);
		}

		/// <summary>
		/// Checks if the given directory (full path) is empty or not
		/// </summary>
		public static bool IsEmpty(string dir)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(dir);
			if (directoryInfo.GetFiles().Length != 0)
			{
				return false;
			}
			if (directoryInfo.GetDirectories().Length != 0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Deletes all files and subdirectories from the given directory
		/// </summary>
		public static void MakeEmpty(string dir)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(dir);
			FileInfo[] files = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				if (fileInfo.Extension != "meta")
				{
					AssetDatabase.DeleteAsset(FullPathToADBPath(fileInfo.ToString()));
				}
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				AssetDatabase.DeleteAsset(FullPathToADBPath(directories[i].ToString()));
			}
		}

		/// <summary>Returns the adb path to the given ScriptableObject</summary>
		public static string MonoInstanceADBPath(ScriptableObject scriptableObj)
		{
			return AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(scriptableObj));
		}

		/// <summary>Returns the adb path to the given MonoBehaviour</summary>
		public static string MonoInstanceADBPath(MonoBehaviour monobehaviour)
		{
			return AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour(monobehaviour));
		}

		/// <summary>Returns the adb directory that contains the given ScriptableObject without final slash</summary>
		public static string MonoInstanceADBDir(ScriptableObject scriptableObj)
		{
			string assetPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(scriptableObj));
			return assetPath.Substring(0, assetPath.LastIndexOf(ADBPathSlash));
		}

		/// <summary>Returns the adb directory that contains the given MonoBehaviour without final slash</summary>
		public static string MonoInstanceADBDir(MonoBehaviour monobehaviour)
		{
			string assetPath = AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour(monobehaviour));
			return assetPath.Substring(0, assetPath.LastIndexOf(ADBPathSlash));
		}

		/// <summary>
		/// Returns the adb paths to the selected folders in the Project panel, or NULL if there is none.
		/// Contrary to Selection.activeObject, which only returns folders selected in the right side of the panel,
		/// this method also works with folders selected in the left side.
		/// </summary>
		public static List<string> SelectedADBDirs()
		{
			List<string> list = null;
			UnityEngine.Object[] filtered = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
			for (int i = 0; i < filtered.Length; i++)
			{
				string assetPath = AssetDatabase.GetAssetPath(filtered[i]);
				if (!string.IsNullOrEmpty(assetPath) && Directory.Exists(ADBPathToFullPath(assetPath)))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(assetPath);
				}
			}
			return list;
		}

		/// <summary>
		/// Sets the script execution order of the given MonoBehaviour
		/// </summary>
		public static void SetScriptExecutionOrder(MonoBehaviour monobehaviour, int order)
		{
			MonoImporter.SetExecutionOrder(MonoScript.FromMonoBehaviour(monobehaviour), order);
		}

		/// <summary>
		/// Gets the script execution order of the given MonoBehaviour
		/// </summary>
		public static int GetScriptExecutionOrder(MonoBehaviour monobehaviour)
		{
			return MonoImporter.GetExecutionOrder(MonoScript.FromMonoBehaviour(monobehaviour));
		}
	}
}
