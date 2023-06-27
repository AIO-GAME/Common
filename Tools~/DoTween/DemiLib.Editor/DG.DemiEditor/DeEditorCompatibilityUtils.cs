using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Utils to use he correct method based on Unity's version
	/// </summary>
	public static class DeEditorCompatibilityUtils
	{
		private static MethodInfo _miEncodeToPng;

		private static MethodInfo _miGetPrefabParent;

		/// <summary>
		/// Encodes to PNG using reflection to use correct method depending if editor is version 2017 or earlier
		/// </summary>
		public static byte[] EncodeToPNG(Texture2D texture)
		{
			if (_miEncodeToPng == null)
			{
				_miEncodeToPng = typeof(Texture2D).GetMethod("EncodeToPNG", BindingFlags.Instance | BindingFlags.Public);
				if (_miEncodeToPng == null)
				{
					_miEncodeToPng = Type.GetType("UnityEngine.ImageConversion, UnityEngine").GetMethod("EncodeToPNG", BindingFlags.Static | BindingFlags.Public);
				}
			}
			if (DeUnityEditorVersion.MajorVersion >= 2017)
			{
				object[] array = new Texture2D[1] { texture };
				object[] parameters = array;
				return _miEncodeToPng.Invoke(null, parameters) as byte[];
			}
			return _miEncodeToPng.Invoke(texture, null) as byte[];
		}

		/// <summary>
		/// Returns the prefab parent by using different code on Unity 2018 or later
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static UnityEngine.Object GetPrefabParent(GameObject instance)
		{
			if (_miGetPrefabParent == null)
			{
				if (DeUnityEditorVersion.Version < 2017.2f)
				{
					_miGetPrefabParent = typeof(PrefabUtility).GetMethod("GetPrefabParent", BindingFlags.Static | BindingFlags.Public);
				}
				else
				{
					_miGetPrefabParent = typeof(PrefabUtility).GetMethod("GetCorrespondingObjectFromSource", BindingFlags.Static | BindingFlags.Public);
				}
			}
			MethodInfo miGetPrefabParent = _miGetPrefabParent;
			object[] parameters = new GameObject[1] { instance };
			return (UnityEngine.Object)miGetPrefabParent.Invoke(null, parameters);
		}
	}
}
