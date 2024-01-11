/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2024-01-11               
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class ScriptIconAttribute : Attribute
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
        public string FilePath { get; set; }

        private static Dictionary<string, string> _iconCache = new Dictionary<string, string>();

        private static string Project
        {
            get
            {
                if (string.IsNullOrEmpty(_project))
                    _project = Application.dataPath.Substring(0,
                        Application.dataPath.LastIndexOf("/", StringComparison.CurrentCulture) + 1);
                return _project;
            }
        }

        private static string _project;

        public ScriptIconAttribute([CallerFilePath] string filePath = "")
        {
            FilePath = filePath.Replace('\\', '/').Replace(Project, "");
        }

#if UNITY_EDITOR

        [InitializeOnLoadMethod]
        private static void Init()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsClass) continue;
                    foreach (var attribute in type.GetCustomAttributes<ScriptIconAttribute>(false)) attribute.SetIcon();
                }
            }
        }

        public void SetIcon()
        {
            if (FilePath.StartsWith("Library")) return;

            string AssetGuid = null;
            if (!string.IsNullOrEmpty(IconRelative))
            {
                _iconCache.TryGetValue(IconRelative, out AssetGuid);
                if (AssetGuid is null)
                    _iconCache[IconRelative] = AssetGuid = AssetDatabase.AssetPathToGUID(IconRelative);
            }

            if (AssetGuid is null && !string.IsNullOrEmpty(IconResource))
            {
                _iconCache.TryGetValue(IconResource, out AssetGuid);
                if (AssetGuid is null)
                    _iconCache[IconResource] = AssetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Resources.Load<Texture2D>(IconResource)));
            }

            if (string.IsNullOrEmpty(AssetGuid)) return;

            var ScriptMeta = string.Concat(FilePath, ".meta");
            if (!File.Exists(ScriptMeta)) return;
            var ydata = AHelper.IO.ReadYaml<Dictionary<object, object>>(ScriptMeta);
            var icon = string.Format(@"{{fileID: 2800000, guid: {0}, type: 3}}", AssetGuid);
            if (ydata["MonoImporter"] is Dictionary<object, object> MonoImporter)
            {
                MonoImporter["icon"] = icon;
                AHelper.IO.WriteUTF8(ScriptMeta, AHelper.Yaml.Serialize(ydata).Replace($"'{icon}'", icon));
            }
        }
    }
#endif
}