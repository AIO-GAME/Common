/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-01
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace AIO.UEditor
{
    [ScriptIcon(IconResource = "Editor/Icon/Color/general")]
    internal static partial class GWindowGen
    {
        private static string GetOutPath()
        {
            return Path.Combine(Application.dataPath, "Editor", "Gen", "GWindow");
        }

        [AInit(mode: EInitAttrMode.Editor, int.MaxValue - 2)]
        internal static void Generate()
        {
            var dic = new Dictionary<Type, GWindowAttribute>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract || !type.IsClass || !type.IsSubclassOf(typeof(EditorWindow))) continue;
                    var attribute = type.GetCustomAttribute<GWindowAttribute>();
                    if (attribute is null) continue;
                    ScriptIcon.SetIcon(attribute.FilePath, attribute.GetTexture2D());
                    if (string.IsNullOrEmpty(attribute.Menu)) continue;
                    dic.Add(type, attribute);
                }
            }

            var change = CreateProject(dic);
            if (!change) return;
            AssetDatabase.Refresh();
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}