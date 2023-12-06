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
    internal static partial class GWindowGen
    {
        private static string GetOutPath()
        {
            return Path.Combine(Application.dataPath, "Editor", "Gen", "GWindow");
        }

        [InitializeOnLoadMethod]
        internal static void Generate()
        {
            var dic = new Dictionary<Type, GWindowAttribute>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract) continue;
                    if (!type.IsSubclassOf(typeof(EditorWindow))) continue; // 必须是 EditorWindow 的子类
                    var attribute = type.GetCustomAttribute<GWindowAttribute>();
                    if (attribute is null) continue;
                    if (string.IsNullOrEmpty(attribute.Menu)) continue;
                    dic.Add(type, attribute);
                }
            }

            var change = CreateProject(dic);
            if (change)
            {
                AssetDatabase.Refresh();

                var RefreshSettings = typeof(AssetDatabase).GetMethod("RefreshSettings",
                    BindingFlags.Static | BindingFlags.Public);
                if (RefreshSettings != null) RefreshSettings.Invoke(null, null);

                CompilationPipeline.RequestScriptCompilation();
            }
        }
    }
}