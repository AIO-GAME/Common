using System;
using Borodar.RainbowCore;
using UnityEngine;

namespace Borodar.RainbowFolders.Settings
{
	[Serializable]
	public class ProjectRule
	{
		public enum KeyType
		{
			Name,
			Path
		}

		public KeyType Type;

		public string Key;

		public int Ordinal;

		public int Priority;

		public ProjectIcon IconType;

		public Texture2D SmallIcon;

		public Texture2D LargeIcon;

		public bool IsIconRecursive;

		public CoreBackground BackgroundType;

		public Texture2D BackgroundTexture;

		public bool IsBackgroundRecursive;

		public bool IsHidden;

		public ProjectRule(ProjectRule value)
		{
			Type = value.Type;
			Key = value.Key;
			Ordinal = value.Ordinal;
			Priority = value.Priority;
			IconType = value.IconType;
			SmallIcon = value.SmallIcon;
			LargeIcon = value.LargeIcon;
			IsIconRecursive = value.IsIconRecursive;
			BackgroundType = value.BackgroundType;
			BackgroundTexture = value.BackgroundTexture;
			IsBackgroundRecursive = value.IsBackgroundRecursive;
		}

		public ProjectRule(KeyType type, string key)
		{
			Type = type;
			Key = key;
		}

		public ProjectRule(KeyType type, string key, ProjectIcon iconType)
		{
			Type = type;
			Key = key;
			IconType = iconType;
			SmallIcon = null;
			LargeIcon = null;
		}

		public ProjectRule(KeyType type, string key, Texture2D smallIcon, Texture2D largeIcon)
		{
			Type = type;
			Key = key;
			IconType = ProjectIcon.Custom;
			SmallIcon = smallIcon;
			LargeIcon = largeIcon;
		}

		public ProjectRule(KeyType type, string key, CoreBackground background)
		{
			Type = type;
			Key = key;
			BackgroundType = background;
			BackgroundTexture = null;
		}

		public ProjectRule(KeyType type, string key, Texture2D background)
		{
			Type = type;
			Key = key;
			IconType = ProjectIcon.Custom;
			BackgroundTexture = background;
		}

		public void CopyFrom(ProjectRule target)
		{
			Type = target.Type;
			Key = target.Key;
			Ordinal = target.Ordinal;
			Priority = target.Priority;
			IconType = target.IconType;
			SmallIcon = target.SmallIcon;
			LargeIcon = target.LargeIcon;
			IsIconRecursive = target.IsIconRecursive;
			BackgroundType = target.BackgroundType;
			BackgroundTexture = target.BackgroundTexture;
			IsBackgroundRecursive = target.IsBackgroundRecursive;
		}

		public bool HasIcon()
		{
			if (IconType != 0)
			{
				if (HasCustomIcon() && !(SmallIcon != null))
				{
					return LargeIcon != null;
				}
				return true;
			}
			return false;
		}

		public bool HasSmallIcon()
		{
			if (IconType != 0)
			{
				if (HasCustomIcon())
				{
					return SmallIcon != null;
				}
				return true;
			}
			return false;
		}

		public bool HasLargeIcon()
		{
			if (IconType != 0)
			{
				if (HasCustomIcon())
				{
					return LargeIcon != null;
				}
				return true;
			}
			return false;
		}

		public bool HasCustomIcon()
		{
			return IconType == ProjectIcon.Custom;
		}

		public bool HasBackground()
		{
			if (BackgroundType != 0)
			{
				if (HasCustomBackground())
				{
					return BackgroundTexture != null;
				}
				return true;
			}
			return false;
		}

		public bool HasCustomBackground()
		{
			return BackgroundType == CoreBackground.Custom;
		}

		public bool HasAtLeastOneTexture()
		{
			if (!HasIcon())
			{
				return HasBackground();
			}
			return true;
		}

		public bool IsRecursive()
		{
			if (!IsIconRecursive)
			{
				return IsBackgroundRecursive;
			}
			return true;
		}
	}
}
