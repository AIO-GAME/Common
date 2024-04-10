#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

#endregion

namespace AIO.UEditor
{
    #region

#if UNITY_2020_1_OR_NEWER
    using UnityEditorInternal;
#endif

    #endregion

    internal static class ManageRuntime
    {
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
            Assemblies      = new Dictionary<string, Assembly>();
            AssembliesCache = new Dictionary<string, Assembly>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var name = assembly.GetName().Name;
                if (name.Contains("editor")) continue;
                if (name.Contains("Editor")) continue;
                if (name.StartsWith("AIO.T4")) continue;
                if (name.StartsWith("AIO.PrCourse")) continue;
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

                if (name.StartsWith("AIO")) Assemblies.Add(assembly.FullName, assembly);
            }

            provider       = new GraphicSettingsProvider("AIO/ADF-DLL-Manager", SettingsScope.User);
            provider.label = "ADF DLL Manager";
            provider.guiHandler = delegate
            {
                GUILayout.BeginVertical();
                GUILayout.Space(10);

                using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                {
                    GUILayout.Label("Label", new GUIStyle("CenteredLabel"), GUILayout.ExpandWidth(true));
                    GUILayout.Label("Change", new GUIStyle("CenteredLabel"), GUILayout.Width(120));
                }

                foreach (var assembly in Assemblies)
                {
                    if (AssembliesCache.ContainsKey(assembly.Key)) continue;
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        GUILayout.Label(assembly.Key);


                        if (GUILayout.Button("Runtime", GUILayout.Width(60)))
                        {
                            Enable(GetInfo(assembly.Value));
                            return;
                        }

                        if (GUILayout.Button("Editor", GUILayout.Width(60)))
                        {
                            Disable(GetInfo(assembly.Value));
                            return;
                        }

                        if (GUILayout.Button("+", GUILayout.Width(20)))
                        {
                            AssembliesCache.Add(assembly.Key, assembly.Value);
                            return;
                        }
                    }
                }

                GUILayout.Space(10);
                GUILayout.EndVertical();

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Runtime Run"))
                    {
                        Enable(GetInfo(AssembliesCache.Values));
                        return;
                    }

                    if (GUILayout.Button("Editor Run"))
                    {
                        Disable(GetInfo(AssembliesCache.Values));
                        return;
                    }

                    if (GUILayout.Button("Add ALL", GUILayout.Width(60)))
                    {
                        AssembliesCache = new Dictionary<string, Assembly>(Assemblies);
                        return;
                    }

                    if (GUILayout.Button("Clear", GUILayout.Width(60)))
                    {
                        AssembliesCache.Clear();
                        return;
                    }
                }

                GUILayout.BeginVertical();
                foreach (var assembly in AssembliesCache.Keys)
                {
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    GUILayout.Label(assembly);
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        AssembliesCache.Remove(assembly);
                        return;
                    }

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField($"Version {Setting.Version}", EditorStyles.centeredGreyMiniLabel);
            };
            return provider;
        }

        #region Runtime

        internal const string KEY = nameof(AIO) + "." + nameof(UEditor) + "." + nameof(ManageRuntime) + ".Setting";

        private static IDictionary<string, EAssembliesType> GetEnable()
        {
            var list = new Dictionary<string, Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (list.ContainsKey(assembly.FullName)) continue;
                var name = assembly.GetName().Name;
                if (name.Contains("Editor")) continue;
                if (name.Contains("editor")) continue;
                if (name.StartsWith("AIO.T4")) continue;
                if (name.StartsWith("AIO.PrCourse")) continue;
                switch (name)
                {
                    case "HtmlAgilityPack":
                        list.Add(assembly.FullName, assembly);
                        continue;
                    case "ICSharpCode.SharpZipLib":
                        list.Add(assembly.FullName, assembly);
                        continue;
                    case "YamlDotNet":
                        list.Add(assembly.FullName, assembly);
                        continue;
                }

                if (!name.StartsWith("AIO")) continue;
                list.Add(assembly.FullName, assembly);
            }

            return GetInfo(list.Values);
        }

        private static IDictionary<string, EAssembliesType> GetDisable()
        {
            var list = new Dictionary<string, Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (list.ContainsKey(assembly.FullName)) continue;
                var name = assembly.GetName().Name;
                if (name.Contains("Editor")) continue;
                if (name.Contains("editor")) continue;
                if (name.StartsWith("AIO.T4")) continue;
                if (name.StartsWith("AIO.PrCourse")) continue;
                if (!name.StartsWith("AIO")) continue;
                list.Add(assembly.FullName, assembly);
            }

            return GetInfo(list.Values);
        }

        private const string MENU = "AIO/Runtime Export";
        //
        // [InitializeOnLoadMethod]
        // [RuntimeInitializeOnLoadMethod]
        // private static void MenuRefresh()
        // {
        //     var check = Menu.GetChecked(MENU);
        //     var set = EHelper.Prefs.LoadBoolean(KEY);
        //     if (check == set) return;
        //     Menu.SetChecked(MENU, set);
        // }
        //
        // [MenuItem(MENU, false, 9999)]
        // private static void Setting()
        // {
        //     EHelper.Prefs.ReverseBoolean(KEY);
        //     MenuRefresh();
        // }

        private static Dictionary<string, EAssembliesType> GetInfo(Assembly assembly, params Assembly[] assemblies)
        {
            return GetInfo(new[] { assembly }.Concat(assemblies));
        }

        private static Dictionary<string, EAssembliesType> GetInfo(IEnumerable<Assembly> assemblies)
        {
            var dictionary = new Dictionary<string, EAssembliesType>();
            if (assemblies is null) return dictionary;
            var project = Application.dataPath.Replace("Assets", "").Replace('/', '\\');
            foreach (var assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;
                try
                {
                    var packageInfo = PackageInfo.FindForAssembly(assembly);
                    var runtimePath = Path.Combine(project, packageInfo.resolvedPath);
                    foreach (var item in new DirectoryInfo(runtimePath).GetFiles("*", SearchOption.AllDirectories).Where(f => f.Extension == ".dll" || f.Extension == ".asmdef"))
                    {
                        if (assemblyName != item.Name.Replace(item.Extension, "")) continue;
                        var type = item.Extension.Contains("dll") ? EAssembliesType.DLL : EAssembliesType.ADF;
                        dictionary.Add(item.FullName, type);
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}:{1}", assemblyName, e);
                }
            }

            return dictionary;
        }

        public enum EAssembliesType
        {
            ADF,
            DLL
        }

        public static void Enable()
        {
            Enable(GetEnable());
        }

        public static void Disable()
        {
            Disable(GetDisable());
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
                if (assetPath.Contains("/Editor")) continue;
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
                        if (importer is null) break;
                        importer.SetCompatibleWithAnyPlatform(false);
                        importer.SetCompatibleWithEditor(true);
                        importer.SaveAndReimport();
                        break;
                }
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}