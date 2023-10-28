/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-24
|||✩ Document: ||| ->
|||✩ - - - - - |*/

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
        [InitializeOnLoadMethod]
        internal static void Generate()
        {
            var dataPath = Application.dataPath.Replace("Assets", "");

            var packageInfos = AssetDatabase.FindAssets("package", new string[] { "Packages" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath)
                .GroupBy(x => x.assetPath)
                .Select(x => x.First())
                .Where(x => File.Exists(Path.Combine(dataPath, x.resolvedPath, ".git")) ||
                            Directory.Exists(Path.Combine(dataPath, x.resolvedPath, ".git")))
                .ToList();

            var change = CreateProject();

            if (CreateTemplate(packageInfos)) change = true;

            if (change)
            {
                AssetDatabase.Refresh();

                var RefreshSettings = typeof(AssetDatabase).GetMethod("RefreshSettings",
                    BindingFlags.Static | BindingFlags.Public);
                if (RefreshSettings != null) RefreshSettings.Invoke(null, null);

                CompilationPipeline.RequestScriptCompilation();
            }
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

            Helper.AssetDatabase.Refresh();
            Helper.AssetDatabase.RefreshSettings();

            var RefreshSettings = typeof(AssetDatabase).GetMethod("RefreshSettings",
                BindingFlags.Static | BindingFlags.Public);
            if (RefreshSettings != null) RefreshSettings.Invoke(null, null);

            Helper.CompilationPipeline.RequestScriptCompilation();
        }

        private static string GetOutPath()
        {
            return Path.Combine(Application.dataPath, "Editor", "Gen", "Git");
        }
    }
}