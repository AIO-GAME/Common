using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.UEditor
{
    public static class ManageRuntime
    {
        [MenuItem("Tools/AIO/Disable Runtime", false, 0)]
        public static void Disable()
        {
            Selection.activeObject = null;
            var packageInfo = PackageInfo.FindForAssembly(Assembly.GetExecutingAssembly());
            var project = Application.dataPath.Replace("Assets", "").Replace('/', '\\');
            var runtimePath = Path.Combine(project, packageInfo.resolvedPath);

            foreach (var item in AHelper.IO.GetFiles(Path.Combine(runtimePath, "Plugins"), "*.dll"))
            {
                var importer = (PluginImporter)AssetImporter.GetAtPath(item.Replace(project, ""));
                importer.SetCompatibleWithAnyPlatform(false);
                importer.SetCompatibleWithEditor(true);
                importer.SaveAndReimport();
            }

            foreach (var item in AHelper.IO.GetFiles(Path.Combine(runtimePath, "Runtime"), "*.asmdef"))
            {
                var assetPath = item.Replace(project, "");
                var hashtable = AHelper.Json.ToHashTable(File.ReadAllText(item));
                hashtable["includePlatforms"] = new List<string> { "Editor" };
                hashtable["excludePlatforms"] = new List<string>();
                AHelper.IO.WriteJson(item, hashtable);
                AssetDatabase.SaveAssetIfDirty(AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath));
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/AIO/Enable Runtime", false, 0)]
        public static void Enable()
        {
            Selection.activeObject = null;
            var packageInfo = PackageInfo.FindForAssembly(Assembly.GetExecutingAssembly());
            var project = Application.dataPath.Replace("Assets", "").Replace('/', '\\');
            var runtimePath = Path.Combine(project, packageInfo.resolvedPath);

            foreach (var item in AHelper.IO.GetFiles(Path.Combine(runtimePath, "Plugins"), "*.dll"))
            {
                var importer = (PluginImporter)AssetImporter.GetAtPath(item.Replace(project, ""));
                importer.SetCompatibleWithAnyPlatform(true);
                importer.SetCompatibleWithEditor(true);
                importer.SaveAndReimport();
            }

            foreach (var item in AHelper.IO.GetFiles(Path.Combine(runtimePath, "Runtime"), "*.asmdef"))
            {
                var assetPath = item.Replace(project, "");
                var hashtable = AHelper.Json.ToHashTable(File.ReadAllText(item));
                hashtable["includePlatforms"] = new List<string>();
                hashtable["excludePlatforms"] = new List<string>();
                AHelper.IO.WriteJson(item, hashtable);
                AssetDatabase.SaveAssetIfDirty(AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath));
            }

            AssetDatabase.Refresh();
        }
    }
}