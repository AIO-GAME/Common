using System;
using System.IO;
using System.Reflection;
using System.Text;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

#pragma warning disable CS1591

namespace DG.DOTweenEditor
{
    public static class EditorUtils
    {
        private static readonly StringBuilder _Strb;

        private static bool _retrievedDependenciesData;

        private static bool _hasPro;

        private static bool _hasDOTweenTimeline;

        private static bool _hasDOTweenTimelineUnityPackage;

        private static string _proVersion;

        private static bool _hasCheckedForPro;

        private static string _editorADBDir;

        private static string _demigiantDir;

        private static string _dotweenDir;

        private static string _dotweenProDir;

        private static string _dotweenProEditorDir;

        private static string _dotweenModulesDir;

        private static string _dotweenTimelineDir;

        private static string _dotweenTimelineUnityPackageFilePath;

        public static string projectPath { get; private set; }

        public static string assetsPath { get; private set; }

        public static bool hasPro
        {
            get
            {
                RetrieveDependenciesData();
                return _hasPro;
            }
        }

        public static bool hasDOTweenTimeline
        {
            get
            {
                RetrieveDependenciesData();
                if (hasPro)
                {
                    return _hasDOTweenTimeline;
                }

                return false;
            }
        }

        public static bool hasDOTweenTimelineUnityPackage
        {
            get
            {
                RetrieveDependenciesData();
                if (hasPro)
                {
                    return _hasDOTweenTimelineUnityPackage;
                }

                return false;
            }
        }

        public static string proVersion
        {
            get
            {
                RetrieveDependenciesData();
                return _proVersion;
            }
        }

        public static string editorADBDir
        {
            get
            {
                RetrieveDependenciesData();
                return _editorADBDir;
            }
        }

        public static string demigiantDir
        {
            get
            {
                RetrieveDependenciesData();
                return _demigiantDir;
            }
        }

        public static string dotweenDir
        {
            get
            {
                RetrieveDependenciesData();
                return _dotweenDir;
            }
        }

        public static string dotweenProDir
        {
            get
            {
                RetrieveDependenciesData();
                return _dotweenProDir;
            }
        }

        public static string dotweenProEditorDir
        {
            get
            {
                RetrieveDependenciesData();
                return _dotweenProEditorDir;
            }
        }

        public static string dotweenModulesDir
        {
            get
            {
                RetrieveDependenciesData();
                return _dotweenModulesDir;
            }
        }

        public static string dotweenTimelineDir
        {
            get
            {
                RetrieveDependenciesData();
                return _dotweenTimelineDir;
            }
        }

        public static string dotweenTimelineUnityPackageFilePath
        {
            get
            {
                RetrieveDependenciesData();
                return _dotweenTimelineUnityPackageFilePath;
            }
        }

        public static bool isOSXEditor { get; private set; }

        public static string pathSlash { get; private set; }

        public static string pathSlashToReplace { get; private set; }

        static EditorUtils()
        {
            _Strb = new StringBuilder();
            isOSXEditor = Application.platform == RuntimePlatform.OSXEditor;
            bool num = Application.platform == RuntimePlatform.WindowsEditor;
            pathSlash = (num ? "\\" : "/");
            pathSlashToReplace = (num ? "/" : "\\");
            projectPath = Application.dataPath;
            // projectPath = Application.dataPath.Replace("Assets", "Packages");
            projectPath = projectPath.Substring(0, projectPath.LastIndexOf("/", StringComparison.CurrentCulture));
            projectPath = projectPath.Replace(pathSlashToReplace, pathSlash);
            assetsPath = string.Concat(projectPath, pathSlash, "Assets");
        }

        public static void RetrieveDependenciesData(bool force = false)
        {
            if (force || !_retrievedDependenciesData)
            {
                _retrievedDependenciesData = true;
                CheckForPro();
                StoreEditorADBDir();
                StoreDOTweenDirsAndFilePaths();
            }
        }

        public static void DelayedCall(float delay, Action callback)
        {
            new DelayedCall(delay, callback);
        }

        /// <summary>
        /// Checks that the given editor texture use the correct import settings,
        /// and applies them if they're incorrect.
        /// </summary>
        public static void SetEditorTexture(Texture2D texture, FilterMode filterMode = FilterMode.Point, int maxTextureSize = 32)
        {
            if (texture.wrapMode != TextureWrapMode.Clamp)
            {
                string assetPath = AssetDatabase.GetAssetPath(texture);
                TextureImporter obj = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                obj.textureType = TextureImporterType.GUI;
                obj.npotScale = TextureImporterNPOTScale.None;
                obj.filterMode = filterMode;
                obj.wrapMode = TextureWrapMode.Clamp;
                obj.maxTextureSize = maxTextureSize;
                obj.textureFormat = TextureImporterFormat.AutomaticTruecolor;
                AssetDatabase.ImportAsset(assetPath);
            }
        }

        /// <summary>
        /// Returns TRUE if setup is required
        /// </summary>
        public static bool DOTweenSetupRequired()
        {
            if (!Directory.Exists(dotweenDir))
            {
                return false;
            }

            return Directory.GetFiles(string.Concat(dotweenDir, "Editor"), "DOTweenUpgradeManager.*").Length != 0;
        }

        public static void DeleteDOTweenUpgradeManagerFiles()
        {
            Type type = Type.GetType("DG.DOTweenUpgradeManager.Autorun, DOTweenUpgradeManager");
            if (type != null)
            {
                string location = type.Assembly.Location;
                location = location.Substring(0, location.LastIndexOf('.'));
                AssetDatabase.StartAssetEditing();
                DeleteAssetsIfExist(new string[4]
                {
                    FullPathToADBPath(string.Concat(location, ".dll")),
                    FullPathToADBPath(string.Concat(location, ".dll.mdb")),
                    FullPathToADBPath(string.Concat(location, ".pdb")),
                    FullPathToADBPath(string.Concat(location, ".xml"))
                });
                AssetDatabase.StopAssetEditing();
            }
        }

        public static void DeleteLegacyNoModulesDOTweenFiles()
        {
            string text = FullPathToADBPath(dotweenDir);
            AssetDatabase.StartAssetEditing();
            DeleteAssetsIfExist(new string[21]
            {
                string.Concat(text, "DOTween43.dll"),
                string.Concat(text, "DOTween43.xml"),
                string.Concat(text, "DOTween43.dll.mdb"),
                string.Concat(text, "DOTween43.dll.addon"),
                string.Concat(text, "DOTween43.xml.addon"),
                string.Concat(text, "DOTween43.dll.mdb.addon"),
                string.Concat(text, "DOTween46.dll"),
                string.Concat(text, "DOTween46.xml"),
                string.Concat(text, "DOTween46.dll.mdb"),
                string.Concat(text, "DOTween46.dll.addon"),
                string.Concat(text, "DOTween46.xml.addon"),
                string.Concat(text, "DOTween46.dll.mdb.addon"),
                string.Concat(text, "DOTween50.dll"),
                string.Concat(text, "DOTween50.xml"),
                string.Concat(text, "DOTween50.dll.mdb"),
                string.Concat(text, "DOTween50.dll.addon"),
                string.Concat(text, "DOTween50.xml.addon"),
                string.Concat(text, "DOTween50.dll.mdb.addon"),
                string.Concat(text, "DOTweenTextMeshPro.cs.addon"),
                string.Concat(text, "DOTweenTextMeshPro_mod.cs"),
                string.Concat(text, "DOTweenTk2d.cs.addon")
            });
            AssetDatabase.StopAssetEditing();
        }

        public static void DeleteOldDemiLibCore()
        {
            string assemblyFilePath = GetAssemblyFilePath(typeof(DOTween).Assembly);
            string text = ((assemblyFilePath.IndexOf("/") != -1) ? "/" : "\\");
            assemblyFilePath = assemblyFilePath.Substring(0, assemblyFilePath.LastIndexOf(text));
            assemblyFilePath = string.Concat(assemblyFilePath.Substring(0, assemblyFilePath.LastIndexOf(text)), text, "DemiLib");
            string text2 = FullPathToADBPath(assemblyFilePath);
            if (!AssetExists(text2))
            {
                return;
            }

            string text3 = string.Concat(text2, "/Core");
            if (AssetExists(text3))
            {
                DeleteAssetsIfExist(new string[7]
                {
                    string.Concat(text2, "/DemiLib.dll"),
                    string.Concat(text2, "/DemiLib.xml"),
                    string.Concat(text2, "/DemiLib.dll.mdb"),
                    string.Concat(text2, "/Editor/DemiEditor.dll"),
                    string.Concat(text2, "/Editor/DemiEditor.xml"),
                    string.Concat(text2, "/Editor/DemiEditor.dll.mdb"),
                    string.Concat(text2, "/Editor/Imgs")
                });
                if (AssetExists(string.Concat(text2, "/Editor")) && Directory.GetFiles(string.Concat(assemblyFilePath, text, "Editor")).Length == 0)
                {
                    AssetDatabase.DeleteAsset(string.Concat(text2, "/Editor"));
                    AssetDatabase.ImportAsset(text3, ImportAssetOptions.ImportRecursive);
                }
            }
        }

        private static void DeleteAssetsIfExist(string[] adbFilePaths)
        {
            foreach (string text in adbFilePaths)
            {
                if (AssetExists(text))
                {
                    AssetDatabase.DeleteAsset(text);
                }
            }
        }

        /// <summary>
        /// Returns TRUE if the file/directory at the given path exists.
        /// </summary>
        /// <param name="adbPath">Path, relative to Unity's project folder</param>
        /// <returns></returns>
        public static bool AssetExists(string adbPath)
        {
            string path = ADBPathToFullPath(adbPath);
            if (!File.Exists(path))
            {
                return Directory.Exists(path);
            }

            return true;
        }

        /// <summary>
        /// Converts the given project-relative path to a full path,
        /// with backward (\) slashes).
        /// </summary>
        public static string ADBPathToFullPath(string adbPath)
        {
            adbPath = adbPath.Replace(pathSlashToReplace, pathSlash);
            return string.Concat(projectPath, pathSlash, adbPath);
        }

        /// <summary>
        /// Converts the given full path to a path usable with AssetDatabase methods
        /// (relative to Unity's project folder, and with the correct Unity forward (/) slashes).
        /// </summary>
        public static string FullPathToADBPath(string fullPath)
        {
            return fullPath.Substring(projectPath.Length + 1).Replace("\\", "/");
        }

        /// <summary>
        /// Connects to a <see cref="T:UnityEngine.ScriptableObject" /> asset.
        /// If the asset already exists at the given path, loads it and returns it.
        /// Otherwise, either returns NULL or automatically creates it before loading and returning it
        /// (depending on the given parameters).
        /// </summary>
        /// <typeparam name="T">Asset type</typeparam>
        /// <param name="adbFilePath">File path (relative to Unity's project folder)</param>
        /// <param name="createIfMissing">If TRUE and the requested asset doesn't exist, forces its creation</param>
        public static T ConnectToSourceAsset<T>(string adbFilePath, bool createIfMissing = false) where T : ScriptableObject
        {
            if (!AssetExists(adbFilePath))
            {
                if (!createIfMissing)
                {
                    return null;
                }

                CreateScriptableAsset<T>(adbFilePath);
            }

            T val = (T)AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
            if ((UnityEngine.Object)val == (UnityEngine.Object)null)
            {
                CreateScriptableAsset<T>(adbFilePath);
                val = (T)AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
            }

            return val;
        }

        /// <summary>
        /// Full path for the given loaded assembly, assembly file included
        /// </summary>
        public static string GetAssemblyFilePath(Assembly assembly)
        {
            string text = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
            if (text.Substring(text.Length - 3) == "dll")
            {
                return text;
            }

            return Path.GetFullPath(assembly.Location);
        }

        /// <summary>
        /// Adds the given global define if it's not already present
        /// </summary>
        public static void AddGlobalDefine(string id)
        {
            bool flag = false;
            int num = 0;
            BuildTargetGroup[] array = (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup));
            foreach (BuildTargetGroup buildTargetGroup in array)
            {
                if (IsValidBuildTargetGroup(buildTargetGroup))
                {
                    string scriptingDefineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
                    if (Array.IndexOf(scriptingDefineSymbolsForGroup.Split(';'), id) == -1)
                    {
                        flag = true;
                        num++;
                        scriptingDefineSymbolsForGroup = string.Concat(scriptingDefineSymbolsForGroup, (scriptingDefineSymbolsForGroup.Length > 0) ? string.Concat(";", id) : id);
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, scriptingDefineSymbolsForGroup);
                    }
                }
            }

            if (flag)
            {
                Debug.Log($"DOTween : added global define \"{id}\" to {num} BuildTargetGroups");
            }
        }

        /// <summary>
        /// Removes the given global define if it's present
        /// </summary>
        public static void RemoveGlobalDefine(string id)
        {
            bool flag = false;
            int num = 0;
            BuildTargetGroup[] array = (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup));
            foreach (BuildTargetGroup buildTargetGroup in array)
            {
                if (!IsValidBuildTargetGroup(buildTargetGroup))
                {
                    continue;
                }

                string[] array2 = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                if (Array.IndexOf(array2, id) == -1)
                {
                    continue;
                }

                flag = true;
                num++;
                _Strb.Length = 0;
                for (int j = 0; j < array2.Length; j++)
                {
                    if (!(array2[j] == id))
                    {
                        if (_Strb.Length > 0)
                        {
                            _Strb.Append(';');
                        }

                        _Strb.Append(array2[j]);
                    }
                }

                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, _Strb.ToString());
            }

            _Strb.Length = 0;
            if (flag)
            {
                Debug.Log($"DOTween : removed global define \"{id}\" from {num} BuildTargetGroups");
            }
        }

        /// <summary>
        /// Returns TRUE if the given global define is present in all the <see cref="T:UnityEditor.BuildTargetGroup" />
        /// or only in the given <see cref="T:UnityEditor.BuildTargetGroup" />, depending on passed parameters.<para />
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildTargetGroup"><see cref="T:UnityEditor.BuildTargetGroup" />to use. Leave NULL to check in all of them.</param>
        public static bool HasGlobalDefine(string id, BuildTargetGroup? buildTargetGroup = null)
        {
            BuildTargetGroup[] array = ((!buildTargetGroup.HasValue) ? ((BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup))) : new BuildTargetGroup[1] { buildTargetGroup.Value });
            foreach (BuildTargetGroup buildTargetGroup2 in array)
            {
                if (IsValidBuildTargetGroup(buildTargetGroup2) && Array.IndexOf(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup2).Split(';'), id) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        private static void CheckForPro()
        {
            _hasCheckedForPro = true;
            try
            {
                _proVersion = Assembly.Load("DOTweenPro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").GetType("DG.Tweening.DOTweenPro")
                    .GetField("Version", BindingFlags.Static | BindingFlags.Public)
                    .GetValue(null) as string;
                _hasPro = true;
            }
            catch
            {
                _hasPro = false;
                _proVersion = "-";
            }
        }

        private static void StoreEditorADBDir()
        {
            _editorADBDir = string.Concat(Path.GetDirectoryName(GetAssemblyFilePath(Assembly.GetExecutingAssembly())).Substring(Application.dataPath.Length + 1).Replace("\\", "/"), "/");
        }

        private static void StoreDOTweenDirsAndFilePaths()
        {
            _dotweenDir = Path.GetDirectoryName(GetAssemblyFilePath(Assembly.GetExecutingAssembly()));
            string text = ((_dotweenDir.IndexOf("/") != -1) ? "/" : "\\");
            _dotweenDir = _dotweenDir.Substring(0, _dotweenDir.LastIndexOf(text) + 1);
            string text2 = _dotweenDir.Substring(0, _dotweenDir.LastIndexOf(text));
            text2 = text2.Substring(0, text2.LastIndexOf(text) + 1);
            _dotweenProDir = string.Concat(text2, "DOTweenPro", text);
            _dotweenTimelineDir = string.Concat(text2, "DOTweenTimeline", text);
            _demigiantDir = ((text2.Substring(text2.Length - 10, 9) == "Demigiant") ? text2 : null);
            _dotweenDir = _dotweenDir.Replace(pathSlashToReplace, pathSlash);
            // _dotweenDir = _dotweenDir.Replace("\\Plugins","\\");
            _dotweenProDir = _dotweenProDir.Replace(pathSlashToReplace, pathSlash);
            _dotweenProEditorDir = string.Concat(_dotweenProDir, "Editor", pathSlash);
            _dotweenModulesDir = string.Concat(_dotweenDir, "Modules", pathSlash);
            if (_demigiantDir != null)
            {
                _demigiantDir = _demigiantDir.Replace(pathSlashToReplace, pathSlash);
            }

            _dotweenTimelineUnityPackageFilePath = string.Concat(_dotweenProDir, "DOTweenTimeline_UnityPackage.unitypackage");
            _hasDOTweenTimelineUnityPackage = File.Exists(_dotweenTimelineUnityPackageFilePath);
            _hasDOTweenTimeline = Directory.Exists(_dotweenTimelineDir);
        }

        private static void CreateScriptableAsset<T>(string adbFilePath) where T : ScriptableObject
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), adbFilePath);
        }

        private static bool IsValidBuildTargetGroup(BuildTargetGroup group)
        {
            if (group == BuildTargetGroup.Unknown)
            {
                return false;
            }

            MethodInfo method = Type.GetType("UnityEditor.Modules.ModuleManager, UnityEditor.dll").GetMethod("GetTargetStringFromBuildTargetGroup", BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo method2 = typeof(PlayerSettings).GetMethod("GetPlatformName", BindingFlags.Static | BindingFlags.NonPublic);
            string value = (string)method.Invoke(null, new object[1] { group });
            string value2 = (string)method2.Invoke(null, new object[1] { group });
            if (string.IsNullOrEmpty(value))
            {
                return !string.IsNullOrEmpty(value2);
            }

            return true;
        }
    }
}