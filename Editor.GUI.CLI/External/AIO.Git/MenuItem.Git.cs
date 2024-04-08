using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.UEditor
{
    /// <summary>
    /// PackageGen
    /// </summary>
    internal static partial class PackageGen
    {
        private const string CMD_GIT = nameof(PrPlatform.Git);

        /// <summary>
        /// 生成
        /// </summary>
        [MenuItem("Git/~~~ Generate ~~~", false, 9999)]
        internal static void Generate()
        {
            Debug.Log($"<b><color=#5DADE2>[GIT]</color></b> {CMD_GIT} Generate");

            var packageInfos = AssetDatabase.FindAssets("package", new string[] { "Packages" }).
                                             Select(AssetDatabase.GUIDToAssetPath).Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null).
                                             Select(PackageInfo.FindForAssetPath).GroupBy(x => x.assetPath).Select(x => x.First()).Where(x =>
                                                                                                                                             File.Exists(Path.Combine(EHelper.Path.Project, x.resolvedPath, ".git")) ||
                                                                                                                                             Directory.Exists(Path.Combine(EHelper.Path.Project, x.resolvedPath, ".git"))
                                             ).ToList();

            var change = CreateProject();
            if (CreateTemplate(packageInfos)) change = true;
            if (!change) return;

            AssetDatabase.Refresh();
            var RefreshSettings =
                typeof(AssetDatabase).GetMethod("RefreshSettings", BindingFlags.Static | BindingFlags.Public);
            if (RefreshSettings != null) RefreshSettings.Invoke(null, null);
            CompilationPipeline.RequestScriptCompilation();
        }

        [AInit(mode: EInitAttrMode.Editor, ushort.MaxValue - 1)]
        internal static void AutoGenerate()
        {
            if (EHelper.Prefs.LoadBoolean("Git.AutoGenerate"))
                Generate();
        }

        /// <summary>
        /// 生成
        /// </summary>
        [MenuItem("Git/~~~ Clean ~~~", false, 9999)]
        internal static void Clean()
        {
            var OutPath = GetOutPath();
            if (!AssetDatabase.DeleteAsset(OutPath))
            {
                if (!Directory.Exists(OutPath)) return;
                Directory.Delete(OutPath, true);
            }

            AssetDatabase.Refresh();

#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#endif

            var RefreshSettings = typeof(AssetDatabase).GetMethod("RefreshSettings",
                                                                  BindingFlags.Static | BindingFlags.Public);
            if (RefreshSettings != null) RefreshSettings.Invoke(null, null);

            CompilationPipeline.RequestScriptCompilation();
        }

        private static string GetOutPath()
        {
            return Path.Combine(EHelper.Path.Assets, "Editor", "Gen", "Git");
        }
    }
}