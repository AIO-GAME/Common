using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Assembly extensions
	/// </summary>
	public static class AssemblyExtensions
	{
		/// <summary>
		/// Full path to the assembly directory, without final slash
		/// </summary>
		public static string Directory(this Assembly assembly)
		{
			string text = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
			if (text.Substring(text.Length - 3) == "dll")
			{
				return Path.GetDirectoryName(text);
			}
			return Path.GetDirectoryName(assembly.Location);
		}

		/// <summary>
		/// AssetDatabase path to the assembly directory, without final slash
		/// </summary>
		public static string ADBDir(this Assembly assembly)
		{
			string text = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
			text = ((!(text.Substring(text.Length - 3) == "dll")) ? Path.GetDirectoryName(assembly.Location) : Path.GetDirectoryName(text));
			return text.Substring(Application.dataPath.Length - 6).Replace(DeEditorFileUtils.ADBPathSlashToReplace, DeEditorFileUtils.ADBPathSlash);
		}
	}
}
