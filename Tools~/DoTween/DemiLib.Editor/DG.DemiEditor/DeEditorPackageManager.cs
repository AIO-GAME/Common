using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Utils to manage UnityPackages import/export and file mirroring
	/// </summary>
	public static class DeEditorPackageManager
	{
		private static readonly StringBuilder _Strb = new StringBuilder();

		/// <summary>
		/// Stores all file paths (excluding metas) found in the given AssetDatabase directory and subdirectory
		/// into the given AssetDatabase file (which will be created if missing),
		/// writing them as relative to the given directory.<para />
		/// EXAMPLE:<para />
		/// <code>adbReadFromDirPath = "Plugins/DOTween"<para />
		/// file "Assets/Plugins/DOTween/aScript.cs" stored as "aScript.cs"<para />
		/// file "Assets/Plugins/DOTween/Subdir/aScript.cs" stored as "Subdir/aScript.cs"<para />
		/// </code>
		/// </summary>
		/// <param name="adbWriteToFilePath">AssetDatabase path ("Assets/...") where the list should be written</param>
		/// <param name="adbReadFromDirPath">AssetDatabase path ("Assets/...") from which the list of files should be retrieved, without final slash</param>
		public static void WriteFileListTo(string adbWriteToFilePath, string adbReadFromDirPath)
		{
			if (adbReadFromDirPath.Length == 0)
			{
				Debug.LogWarning("WriteFileListTo ► parameter adbReadFromDirPath can't be empty");
				return;
			}
			if (adbReadFromDirPath.EndsWith("/") || adbReadFromDirPath.EndsWith("\\"))
			{
				adbReadFromDirPath = adbWriteToFilePath.Substring(0, adbWriteToFilePath.Length - 1);
			}
			string path = DeEditorFileUtils.ADBPathToFullPath(adbReadFromDirPath);
			if (!Directory.Exists(path))
			{
				Debug.LogError($"WriteFileListTo ► adbReadFromDirPath doesn't exist ({adbReadFromDirPath})");
				return;
			}
			string[] array = (from name in Directory.GetFiles(path, "*", SearchOption.AllDirectories)
				where !name.EndsWith(".meta", ignoreCase: true, CultureInfo.InvariantCulture)
				select name).ToArray();
			if (array.Length == 0)
			{
				Debug.LogWarning("WriteFileListTo ► file list empty, canceling operation");
				return;
			}
			MakeFilePathsRelativeTo(array, adbReadFromDirPath);
			string text = DeEditorFileUtils.ADBPathToFullPath(adbWriteToFilePath);
			using (StreamWriter streamWriter = new StreamWriter(text, append: false))
			{
				for (int i = 0; i < array.Length; i++)
				{
					streamWriter.WriteLine(array[i]);
				}
			}
			AssetDatabase.ImportAsset(DeEditorFileUtils.FullPathToADBPath(text));
			Debug.Log($"WriteFileListTo ► {array.Length} files written to \"{text}\"");
			EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(adbWriteToFilePath));
		}

		/// <summary>
		/// Parses a file list created via <see cref="M:DG.DemiEditor.DeEditorPackageManager.WriteFileListTo(System.String,System.String)" /> and removes any files not present in the list from the given directory
		/// </summary>
		/// <param name="label">Label to use when logging the result</param>
		/// <param name="adbListFilePath">AssetDatabase path ("Assets/...") to the file containing the list</param>
		/// <param name="adbParseDirPath">AssetDatabase path ("Assets/...") to the directory to parse for extra files to remove</param>
		/// <param name="simulate">If TRUE only returns a report log and doesn't actually delete the files</param>
		public static void ParseListAndRemoveExtraFiles(string label, string adbListFilePath, string adbParseDirPath, bool simulate = false)
		{
			string path = DeEditorFileUtils.ADBPathToFullPath(adbParseDirPath);
			if (!Directory.Exists(path))
			{
				Debug.LogError($"ParseListAndRemoveExtraFiles ► adbParseDirPath doesn't exist ({adbParseDirPath})");
				return;
			}
			string path2 = DeEditorFileUtils.ADBPathToFullPath(adbListFilePath);
			if (!File.Exists(path2))
			{
				Debug.LogWarning($"ParseListAndRemoveExtraFiles ► file \"{adbListFilePath}\" doesn't exist, canceling operation");
				return;
			}
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(path2))
			{
				string item;
				while ((item = streamReader.ReadLine()) != null)
				{
					list.Add(item);
				}
			}
			string[] array = (from name in Directory.GetFiles(path, "*", SearchOption.AllDirectories)
				where !name.EndsWith(".meta", ignoreCase: true, CultureInfo.InvariantCulture)
				select name).ToArray();
			if (array.Length == 0)
			{
				return;
			}
			_Strb.Length = 0;
			AssetDatabase.StartAssetEditing();
			int num = 0;
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					string item2 = MakeFilePathRelativeTo(array[i], adbParseDirPath);
					if (!list.Contains(item2))
					{
						num++;
						string text = DeEditorFileUtils.FullPathToADBPath(array[i]);
						_Strb.Append("\n   - ").Append(text);
						if (!simulate)
						{
							AssetDatabase.DeleteAsset(text);
						}
					}
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			finally
			{
				AssetDatabase.StopAssetEditing();
			}
			if (simulate)
			{
				_Strb.Insert(0, $"{label} ► SIMULATION ► Would've deleted {num} files");
			}
			else
			{
				_Strb.Insert(0, $"{label} ► Deleted {num} files");
			}
			string message2 = _Strb.ToString();
			_Strb.Length = 0;
			Debug.Log(message2);
		}

		private static void MakeFilePathsRelativeTo(string[] fullFilePaths, string adbDir)
		{
			for (int i = 0; i < fullFilePaths.Length; i++)
			{
				fullFilePaths[i] = MakeFilePathRelativeTo(fullFilePaths[i], adbDir);
			}
		}

		private static string MakeFilePathRelativeTo(string fullFilePath, string adbDir)
		{
			return DeEditorFileUtils.FullPathToADBPath(fullFilePath).Substring(adbDir.Length + 1);
		}
	}
}
