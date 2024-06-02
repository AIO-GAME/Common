#region namespace

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// ScriptIconAttr
    /// </summary>
    public static class ScriptIcon
    {
        private static Dictionary<string, string> _iconCache = new Dictionary<string, string>();

        [AInit(mode: EInitAttrMode.Editor, int.MaxValue - 1)]
        private static void Generate()
        {
            if (PlayerPrefs.GetInt("ScriptIconAttribute.AutoGenerate", 1) != 1) return;
            foreach (var attribute in from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                      from type in assembly.GetTypes()
                                      from attribute in type.GetCustomAttributes<ScriptIconAttribute>(false)
                                      select attribute)
            {
                if (!string.IsNullOrEmpty(attribute.IconResource))
                {
                    SetIconResource(attribute.FilePath, attribute.IconResource);
                    continue;
                }

                if (!string.IsNullOrEmpty(attribute.IconRelative)) SetIconRelative(attribute.FilePath, attribute.IconRelative);
            }
        }

        public static void SetIconResource(string local, string addr)
        {
            if (local.StartsWith("Library")) return;
            if (string.IsNullOrEmpty(addr)) return;
            if (!_iconCache.TryGetValue(addr, out var guid))
            {
                var asset = Resources.Load<Texture2D>(addr);
                if (asset)
                {
                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset.GetInstanceID(), out guid, out long _);
                    _iconCache[addr] = guid;
                }
            }

            if (string.IsNullOrEmpty(guid)) return;
            SetIcon(local, guid);
        }

        public static void SetIcon(string local, Texture asset)
        {
            if (local.StartsWith("Library") || !asset) return;
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset.GetInstanceID(), out var guid, out long _);
            if (string.IsNullOrEmpty(guid)) return;
            SetIcon(local, guid);
        }

        public static void SetIconRelative(string local, string addr)
        {
            if (local.StartsWith("Library")) return;
            if (string.IsNullOrEmpty(addr)) return;
            if (!_iconCache.TryGetValue(addr, out var guid))
                _iconCache[addr] = AssetDatabase.AssetPathToGUID(addr);
            if (string.IsNullOrEmpty(guid)) return;
            SetIcon(local, guid);
        }

        private static void SetIcon(string local, string guid)
        {
            if (local.StartsWith("Library")) return;
            if (string.IsNullOrEmpty(guid)) return;
            var ScriptMeta = string.Concat(local, ".meta");
            if (!File.Exists(ScriptMeta)) return;
            var ydata = AHelper.IO.ReadYaml<Dictionary<object, object>>(ScriptMeta);
            var icon  = $"{{fileID: 2800000, guid: {guid}, type: 3}}";
            if (!ydata.ContainsKey("MonoImporter"))
            {
                ydata["MonoImporter"] = new Dictionary<object, object>
                {
                    { "externalObjects", new List<object>() },
                    { "serializedVersion", 2 },
                    { "defaultReferences", Array.Empty<object>() },
                    { "executionOrder", 0 },
                    { "icon", icon },
                    { "userData", null },
                    { "assetBundleName", null },
                    { "assetBundleVariant", null }
                };
                if (ydata.ContainsKey("timeCreated")) ydata.Remove("timeCreated");
                AHelper.IO.WriteUTF8(ScriptMeta, AHelper.Yaml.Serialize(ydata).Replace($"'{icon}'", icon));
            }
            else if (ydata["MonoImporter"] is Dictionary<object, object> MonoImporter)
            {
                MonoImporter["icon"] = icon;
                AHelper.IO.WriteUTF8(ScriptMeta, AHelper.Yaml.Serialize(ydata).Replace($"'{icon}'", icon));
            }
        }
    }
}