/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AIO
{
    /// <summary>
    /// 脚本图标 请不要直接传入参数 请使用字段赋值
    /// </summary>
    /// <remarks>
    /// 用于标记脚本图标
    /// 如果使用局部类,则需要在局部类中标记
    /// </remarks>
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Enum |
        AttributeTargets.Struct |
        AttributeTargets.Interface |
        AttributeTargets.Delegate |
        AttributeTargets.Field |
        AttributeTargets.Event,
        AllowMultiple = true, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ScriptIconAttribute : Attribute
    {
        /// <summary>
        /// 相对路径图标 使用 AssetDatabase.LoadAssetAtPath 加载
        /// </summary>
        public string IconRelative { get; set; }

        /// <summary>
        /// 资源路径图标 使用 Resources.Load 加载
        /// </summary>
        public string IconResource { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        private string FilePath { get; set; }

        /// <summary>
        /// 是否自动生成 持续更新
        /// </summary>
        public static bool AutoGenerate
        {
            get => PlayerPrefs.GetInt("ScriptIconAttribute.AutoGenerate", 1) == 1;
            set => PlayerPrefs.SetInt("ScriptIconAttribute.AutoGenerate", value ? 1 : 0);
        }

        private static Dictionary<string, string> _iconCache = new Dictionary<string, string>();

        private static int Project
        {
            get
            {
                if (_project == 0)
                    _project = Application.dataPath.LastIndexOf("/", StringComparison.CurrentCulture) + 1;
                return _project;
            }
        }

        private static int _project;

        public ScriptIconAttribute([CallerFilePath] string filePath = "")
        {
            if (filePath.StartsWith(".\\Packages\\")) FilePath = filePath.Substring(2);
            else FilePath = filePath.Replace('\\', '/').Substring(Project);
        }

#if UNITY_EDITOR

        [InitializeOnLoadMethod]
        private static void Generate()
        {
            if (!AutoGenerate) return;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var attribute in type.GetCustomAttributes<ScriptIconAttribute>(false))
                    {
                        if (!string.IsNullOrEmpty(attribute.IconResource))
                        {
                            SetIconResource(attribute.FilePath, attribute.IconResource);
                            continue;
                        }

                        if (!string.IsNullOrEmpty(attribute.IconRelative))
                        {
                            SetIconRelative(attribute.FilePath, attribute.IconRelative);
                        }
                    }
                }
            }
        }

        public static void SetIconResource(string local, string addr)
        {
            if (local.StartsWith("Library")) return;
            if (string.IsNullOrEmpty(addr)) return;
            if (!_iconCache.TryGetValue(addr, out var guid))
            {
                var asset = Resources.Load<Texture2D>(addr);
                if (asset != null)
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
            if (local.StartsWith("Library") || asset is null) return;
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
            var icon = $"{{fileID: 2800000, guid: {guid}, type: 3}}";
            if (!ydata.ContainsKey("MonoImporter"))
            {
                ydata["MonoImporter"] = new Dictionary<object, object>
                {
                    { "externalObjects", new List<object>() },
                    { "serializedVersion", 2 },
                    { "defaultReferences", Array.Empty<object>() },
                    { "executionOrder", 0 },
                    { "icon", icon },
                    { "userData", "" },
                    { "assetBundleName", "" },
                    { "assetBundleVariant", "" }
                };
                AHelper.IO.WriteUTF8(ScriptMeta, AHelper.Yaml.Serialize(ydata).Replace($"'{icon}'", icon));
            }
            else if (ydata["MonoImporter"] is Dictionary<object, object> MonoImporter)
            {
                MonoImporter["icon"] = icon;
                AHelper.IO.WriteUTF8(ScriptMeta, AHelper.Yaml.Serialize(ydata).Replace($"'{icon}'", icon));
            }
        }
    }
#endif
}