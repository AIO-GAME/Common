#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    [ScriptIcon(IconResource = "Editor/Icon/Color/general")]
    internal static partial class GWindowGen
    {
        private static string GetOutPath() { return Path.Combine(Application.dataPath, "Editor", "Gen", "GWindow"); }

        [AInit(EInitAttrMode.Editor, ushort.MaxValue - 2)]
        internal static void Generate()
        {
            var dic = new Dictionary<Type, GWindowAttribute>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || !type.IsClass || !type.IsSubclassOf(typeof(EditorWindow))) continue;
                var attr = type.GetCustomAttribute<GWindowAttribute>();
                if (attr is null) continue;
                ScriptIcon.SetIcon(attr.FilePath, attr.GetTexture2D());
                if (string.IsNullOrEmpty(attr.Menu)) continue;
                dic.Add(type, attr);
            }

            var change = CreateProject(dic);
            if (!change) return;
            AssetDatabase.Refresh();
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}