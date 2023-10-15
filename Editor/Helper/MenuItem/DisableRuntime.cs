using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.UEditor
{
    public static class ManageRuntime
    {
        #region MyRegion

        private const string KEY = nameof(AIO) + "." + nameof(UEditor) + "." + nameof(ManageRuntime) + "." + nameof(Setting);

        public static bool IsEnableRuntime => EHelper.Prefs.LoadBoolean(KEY);

        [InitializeOnLoadMethod]
        [RuntimeInitializeOnLoadMethod]
        private static void MenuRefresh()
        {
            Menu.SetChecked("Tools/AIO/Runtime Export", EHelper.Prefs.LoadBoolean(KEY));
        }

        [MenuItem("Tools/AIO/Runtime Export", false, 0)]
        private static void Setting()
        {
            EHelper.Prefs.ReverseBoolean(KEY);
            MenuRefresh();
        }

        public static void Disable()
        {
            var list = new List<Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var name = assembly.GetName().Name;
                if (name.Contains("Editor")) continue;
                if (name.Contains("AIO.T4")) continue;
                if (name.Contains("AIO.PrCourse")) continue;
                if (name.Contains("AIO")) Assemblies.Add(assembly.FullName, assembly);
            }

            Disable(GetInfo(list));
        }

        public static void Enable()
        {
            var list = new List<Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var name = assembly.GetName().Name;
                if (name.Contains("Editor")) continue;
                if (name.Contains("AIO.T4")) continue;
                if (name.Contains("AIO.PrCourse")) continue;
                switch (name)
                {
                    case "HtmlAgilityPack":
                        Assemblies.Add(assembly.FullName, assembly);
                        continue;
                    case "ICSharpCode.SharpZipLib":
                        Assemblies.Add(assembly.FullName, assembly);
                        continue;
                    case "YamlDotNet":
                        Assemblies.Add(assembly.FullName, assembly);
                        continue;
                }

                if (name.Contains("AIO")) Assemblies.Add(assembly.FullName, assembly);
            }

            Enable(GetInfo(list));
        }

        private static Dictionary<string, EAssembliesType> GetInfo(Assembly assembly, params Assembly[] assemblies)
        {
            return GetInfo(new Assembly[] { assembly }.Concat(assemblies));
        }

        private static Dictionary<string, EAssembliesType> GetInfo(IEnumerable<Assembly> assemblies)
        {
            var dictionary = new Dictionary<string, EAssembliesType>();
            if (assemblies is null) return dictionary;
            foreach (var assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;
                var packageInfo = PackageInfo.FindForAssembly(assembly);
                var project = Application.dataPath.Replace("Assets", "").Replace('/', '\\');
                var runtimePath = Path.Combine(project, packageInfo.resolvedPath);
                foreach (var item in new DirectoryInfo(runtimePath).GetFiles("*", SearchOption.AllDirectories)
                             .Where(f => f.Extension == ".dll" || f.Extension == ".asmdef"))
                {
                    if (assemblyName != item.Name.Replace(item.Extension, "")) continue;
                    var type = item.Extension.Contains("dll") ? EAssembliesType.DLL : EAssembliesType.ADF;
                    dictionary.Add(item.FullName, type);
                    break;
                }
            }

            return dictionary;
        }

        private enum EAssembliesType
        {
            ADF,
            DLL,
        }

        private static void Enable(IDictionary<string, EAssembliesType> dictionary)
        {
            if (dictionary is null || dictionary.Count == 0) return;
            Selection.activeObject = null;
            var project = Application.dataPath.Replace("Assets", "").Replace('/', '\\');
            foreach (var item in dictionary)
            {
                if (!AHelper.IO.ExistsFile(item.Key)) continue;
                var assetPath = item.Key.Replace(project, "");
                switch (item.Value)
                {
                    case EAssembliesType.ADF:
                        var hashtable = AHelper.Json.ToHashTable(File.ReadAllText(item.Key));
                        hashtable["includePlatforms"] = new List<string>();
                        hashtable["excludePlatforms"] = new List<string>();
                        AHelper.IO.WriteJson(item.Key, hashtable);
#if !UNITY_2020_1_OR_NEWER
                        AssetDatabase.SaveAssets();
#else
                        AssetDatabase.SaveAssetIfDirty(
                            AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath));
#endif
                        break;
                    case EAssembliesType.DLL:
                        var importer = (PluginImporter)AssetImporter.GetAtPath(assetPath);
                        importer.SetCompatibleWithAnyPlatform(true);
                        importer.SetCompatibleWithEditor(true);
                        importer.SaveAndReimport();
                        break;
                }
            }

            AssetDatabase.Refresh();
        }

        private static void Disable(IDictionary<string, EAssembliesType> dictionary)
        {
            if (dictionary is null || dictionary.Count == 0) return;
            Selection.activeObject = null;
            var project = Application.dataPath.Replace("Assets", "").Replace('/', '\\');
            foreach (var item in dictionary)
            {
                if (!AHelper.IO.ExistsFile(item.Key)) continue;
                var assetPath = item.Key.Replace(project, "");
                switch (item.Value)
                {
                    case EAssembliesType.ADF:
                        var hashtable = AHelper.Json.ToHashTable(File.ReadAllText(item.Key));
                        hashtable["includePlatforms"] = new List<string> { "Editor" };
                        hashtable["excludePlatforms"] = new List<string>();
                        AHelper.IO.WriteJson(item.Key, hashtable);
#if !UNITY_2020_1_OR_NEWER
                        AssetDatabase.SaveAssets();
#else
                        AssetDatabase.SaveAssetIfDirty(
                            AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath));
#endif
                        break;
                    case EAssembliesType.DLL:
                        var importer = (PluginImporter)AssetImporter.GetAtPath(assetPath);
                        importer.SetCompatibleWithAnyPlatform(false);
                        importer.SetCompatibleWithEditor(true);
                        importer.SaveAndReimport();
                        break;
                }
            }

            AssetDatabase.Refresh();
        }

        #endregion

        private static SettingsProvider provider;

        private static Dictionary<string, Assembly> Assemblies;

        private static Dictionary<string, Assembly> AssembliesCache;

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            if (provider != null) return provider;
            Assemblies = new Dictionary<string, Assembly>();
            AssembliesCache = new Dictionary<string, Assembly>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var name = assembly.GetName().Name;
                if (name.Contains("Editor")) continue;
                if (name.Contains("AIO.T4")) continue;
                if (name.Contains("AIO.PrCourse")) continue;
                if (name == "HtmlAgilityPack")
                {
                    Assemblies.Add(assembly.FullName, assembly);
                    continue;
                }

                if (name == "ICSharpCode.SharpZipLib")
                {
                    Assemblies.Add(assembly.FullName, assembly);
                    continue;
                }

                if (name == "YamlDotNet")
                {
                    Assemblies.Add(assembly.FullName, assembly);
                    continue;
                }

                if (name.Contains("AIO"))
                {
                    Assemblies.Add(assembly.FullName, assembly);
                }
            }

            provider = new GraphicSettingsProvider("AIO/ADF-DLL-Manager", SettingsScope.User);
            provider.label = "ADF DLL Manager";
            provider.guiHandler = delegate
            {
                GELayout.BeginVertical();
                GELayout.Space();

                using (GELayout.VHorizontal(GEStyle.HelpBox))
                {
                    GELayout.Label("Label", GEStyle.CenteredLabel, GTOption.WidthExpand(true));
                    GELayout.Label("Change", GEStyle.CenteredLabel, GTOption.Width(120));
                }

                foreach (var assembly in Assemblies)
                {
                    if (AssembliesCache.ContainsKey(assembly.Key)) continue;
                    using (GELayout.VHorizontal(GEStyle.HelpBox))
                    {
                        GELayout.Label(assembly.Key);


                        if (GELayout.Button("Runtime", 60))
                        {
                        }

                        if (GELayout.Button("Editor", 60))
                        {
                        }

                        if (GELayout.Button("+", 20))
                        {
                            AssembliesCache.Add(assembly.Key, assembly.Value);
                            return;
                        }
                    }
                }

                GELayout.Space();
                GELayout.EndVertical();

                using (GELayout.VHorizontal())
                {
                    if (GELayout.Button("Runtime Run"))
                    {
                        Enable(GetInfo(AssembliesCache.Values));
                        return;
                    }

                    if (GELayout.Button("Editor Run"))
                    {
                        Disable(GetInfo(AssembliesCache.Values));
                        return;
                    }

                    if (GELayout.Button("Add ALL", 60))
                    {
                        AssembliesCache = new Dictionary<string, Assembly>(Assemblies);
                        return;
                    }

                    if (GELayout.Button("Clear", 60))
                    {
                        AssembliesCache.Clear();
                        return;
                    }
                }

                GELayout.BeginVertical();
                foreach (var assembly in AssembliesCache.Keys)
                {
                    GELayout.BeginHorizontal(GEStyle.HelpBox);
                    GELayout.Label(assembly);
                    if (GELayout.Button("-", 20))
                    {
                        AssembliesCache.Remove(assembly);
                        return;
                    }

                    GELayout.EndHorizontal();
                }

                GELayout.EndVertical();
            };
            return provider;
        }
    }
}