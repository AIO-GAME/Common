using System.Globalization;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Util to determine Unity editor version and store them as comparable numbers
	/// </summary>
	public static class DeUnityEditorVersion
	{
		/// <summary>Full major version + first minor version (ex: 2018.1f)</summary>
		public static readonly float Version;

		/// <summary>Major version</summary>
		public static readonly int MajorVersion;

		/// <summary>First minor version (ex: in 2018.1 it would be 1)</summary>
		public static readonly int MinorVersion;

		static DeUnityEditorVersion()
		{
			string unityVersion = Application.unityVersion;
			int num = unityVersion.IndexOf('.');
			if (num == -1)
			{
				MajorVersion = int.Parse(unityVersion);
				Version = MajorVersion;
				return;
			}
			string text = unityVersion.Substring(0, num);
			MajorVersion = int.Parse(text);
			string text2 = unityVersion.Substring(num + 1);
			num = text2.IndexOf('.');
			if (num != -1)
			{
				text2 = text2.Substring(0, num);
			}
			MinorVersion = int.Parse(text2);
			if (!float.TryParse(string.Concat(text, ".", text2), NumberStyles.Float, CultureInfo.InvariantCulture, out Version))
			{
				Debug.LogWarning($"DeUnityEditorVersion ► Error when detecting Unity Version from \"{text}.{text2}\"");
				Version = 2018.3f;
			}
		}
	}
}
