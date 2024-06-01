using System;
using AIO.RainbowCore;
using UnityEngine;

namespace AIO.RainbowFolders.Settings
{
    [Serializable]
    internal class ProjectRule
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
            Type                  = value.Type;
            Key                   = value.Key;
            Ordinal               = value.Ordinal;
            Priority              = value.Priority;
            IconType              = value.IconType;
            SmallIcon             = value.SmallIcon;
            LargeIcon             = value.LargeIcon;
            IsIconRecursive       = value.IsIconRecursive;
            BackgroundType        = value.BackgroundType;
            BackgroundTexture     = value.BackgroundTexture;
            IsBackgroundRecursive = value.IsBackgroundRecursive;
        }

        public ProjectRule(KeyType type, string key)
        {
            Type = type;
            Key  = key;
        }

        public ProjectRule(KeyType type, string key, ProjectIcon iconType)
        {
            Type      = type;
            Key       = key;
            IconType  = iconType;
            SmallIcon = null;
            LargeIcon = null;
        }

        public ProjectRule(KeyType type, string key, Texture2D smallIcon, Texture2D largeIcon)
        {
            Type      = type;
            Key       = key;
            IconType  = ProjectIcon.Custom;
            SmallIcon = smallIcon;
            LargeIcon = largeIcon;
        }

        public ProjectRule(KeyType type, string key, CoreBackground background)
        {
            Type              = type;
            Key               = key;
            BackgroundType    = background;
            BackgroundTexture = null;
        }

        public ProjectRule(KeyType type, string key, Texture2D background)
        {
            Type              = type;
            Key               = key;
            IconType          = ProjectIcon.Custom;
            BackgroundTexture = background;
        }

        public void CopyFrom(ProjectRule target)
        {
            Type                  = target.Type;
            Key                   = target.Key;
            Ordinal               = target.Ordinal;
            Priority              = target.Priority;
            IconType              = target.IconType;
            SmallIcon             = target.SmallIcon;
            LargeIcon             = target.LargeIcon;
            IsIconRecursive       = target.IsIconRecursive;
            BackgroundType        = target.BackgroundType;
            BackgroundTexture     = target.BackgroundTexture;
            IsBackgroundRecursive = target.IsBackgroundRecursive;
        }

        public bool HasIcon()
        {
            if (IconType == ProjectIcon.None) return false;

            if (IconType == ProjectIcon.Custom)
            {
                if (!SmallIcon) return false;
                if (!LargeIcon) return false;
            }

            return true;
        }

        public bool HasSmallIcon()
        {
            if (IconType == 0) return false;
            if (HasCustomIcon()) return !SmallIcon;
            return true;
        }

        public bool HasLargeIcon()
        {
            if (IconType == 0) return false;
            if (HasCustomIcon()) return !LargeIcon;
            return true;
        }

        public bool HasCustomIcon() { return IconType == ProjectIcon.Custom; }

        public bool HasBackground()
        {
            if (BackgroundType == 0) return false;
            if (HasCustomBackground()) return !BackgroundTexture;
            return true;
        }

        public bool HasCustomBackground() { return BackgroundType == CoreBackground.Custom; }

        public bool HasAtLeastOneTexture() { return HasIcon() || HasBackground(); }

        public bool IsRecursive() { return IsIconRecursive || IsBackgroundRecursive; }
    }
}