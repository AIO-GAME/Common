using System.IO;
using Borodar.RainbowCore;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders
{
	public static class ProjectEditorUtility
	{
		private const string LOAD_ASSET_ERROR_MSG = "Could not load {0}\nDid you move the \"Rainbow Folders\" around in your project? Go to \"Preferences -> Rainbow Folders\" and update the location of the asset.";

		private static Texture2D _defaultFolderIcon;

		private static Texture2D _editIconSmallPro;

		private static Texture2D _editIconSmallFree;

		private static Texture2D _editIconLargePro;

		private static Texture2D _editIconLargeFree;

		private static Texture2D _settingsIcon;

		private static Texture2D _filterIcon;

		private static Texture2D _deleteIcon;

		private static Texture2D _foldoutIcon;

		private static Texture2D _foldoutLevelsIcon;

		public static T LoadFromAsset<T>(string relativePath) where T : Object
		{
			string text = Path.Combine(ProjectPreferences.HomeFolder, relativePath);
			T val = AssetDatabase.LoadAssetAtPath<T>(text);
			if (!(Object)val)
			{
				Debug.LogError($"Could not load {text}\nDid you move the \"Rainbow Folders\" around in your project? Go to \"Preferences -> Rainbow Folders\" and update the location of the asset.");
			}
			return val;
		}

		public static Texture2D GetDefaultFolderIcon()
		{
			if (_defaultFolderIcon == null)
			{
				_defaultFolderIcon = EditorGUIUtility.FindTexture("Folder Icon");
			}
			return _defaultFolderIcon;
		}

		public static Texture2D GetEditFolderIcon(bool isSmall, bool isPro)
		{
			if (!isSmall)
			{
				return GetEditIconLarge(isPro);
			}
			return GetEditIconSmall(isPro);
		}

		public static Texture2D GetSettingsButtonIcon()
		{
			return GetCoreTexture(ref _settingsIcon, CoreEditorTexture.IcnSettings);
		}

		public static Texture2D GetFilterButtonIcon()
		{
			return GetCoreTexture(ref _filterIcon, CoreEditorTexture.IcnFilter);
		}

		public static Texture2D GetDeleteButtonIcon()
		{
			return GetCoreTexture(ref _deleteIcon, CoreEditorTexture.IcnDelete);
		}

		public static Texture2D GetFoldoutIcon()
		{
			return GetCoreTexture(ref _foldoutIcon, CoreEditorTexture.IcnFoldoutMiddle);
		}

		public static Texture2D GetFoldoutLevelsIcon()
		{
			return GetCoreTexture(ref _foldoutLevelsIcon, CoreEditorTexture.IcnFoldoutLevels);
		}

		private static Texture2D GetEditIconSmall(bool isPro)
		{
			if (!isPro)
			{
				return GetTexture(ref _editIconSmallFree, ProjectEditorTexture.IcnEditFreeSmall);
			}
			return GetTexture(ref _editIconSmallPro, ProjectEditorTexture.IcnEditProSmall);
		}

		private static Texture2D GetEditIconLarge(bool isPro)
		{
			if (!isPro)
			{
				return GetTexture(ref _editIconLargeFree, ProjectEditorTexture.IcnEditFreeLarge);
			}
			return GetTexture(ref _editIconLargePro, ProjectEditorTexture.IcnEditProLarge);
		}

		private static Texture2D GetTexture(ref Texture2D texture, ProjectEditorTexture type)
		{
			if (texture == null)
			{
				texture = ProjectEditorTexturesStorage.GetTexture(type);
			}
			return texture;
		}

		private static Texture2D GetCoreTexture(ref Texture2D texture, CoreEditorTexture type)
		{
			if (texture == null)
			{
				texture = CoreEditorTexturesStorage.GetTexture(type);
			}
			return texture;
		}
	}
}
