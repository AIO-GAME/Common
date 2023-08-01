/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-24
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.Unity.Editor
{
    internal static partial class PackageGen
    {
        private const string GitLabel = "Package/";
        private const int DEFAULT = 0;

        private static IEnumerable<PackageInfo> GetInfo()
        {
            var packageInfos = AssetDatabase.FindAssets("package", new string[] { "Packages" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath)
                .GroupBy(x => x.assetPath)
                .Select(x => x.First())
                .Where(x => Directory.Exists(Path.Combine(x.resolvedPath, ".git") || File.Exists(Path.Combine(x.resolvedPath, ".git")))
                .ToList();

            return packageInfos;
        }

        #region Git All

        [MenuItem(CMD_GIT + "/All/克隆 Clone", false, 0)]
        private static async void GITClone()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, "AutoGitClone.ini");
            var PackageData = JsonConvert.DeserializeObject<Hashtable>(File.ReadAllText(FilePath));
            await PrPlatform.Git.Clone(PackagePath, PackageData.Get<List<string>>("URL"), false);
        }

        [MenuItem(CMD_GIT + "/All/添加 Add [.]", false, 1)]
        private static async void GITAdd()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Add(list, ".", false);
        }

        [MenuItem(CMD_GIT + "/All/拉取 Pull", false, 2)]
        private static async void GITPull()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Pull(list, false);
        }

#if UNITY_EDITOR_WIN
        [MenuItem(CMD_GIT + "/All/拉取分支 Pull Branch", false, 2)]
        private static async void GITPullBranch()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrWin.Git.PullBranch(list, false);
        }
#endif

        [MenuItem(CMD_GIT + "/All/推送 Push", false, 3)]
        private static async void GITPush()
        {
            var list = GetInfo()
                .Select(x => (x.resolvedPath, ""))
                .ToList();
            await PrPlatform.Git.Push(list, false);
        }

        [MenuItem(CMD_GIT + "/All/提交 Commit", false, 4)]
        private static async void GITCommit()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Commit(list, false);
        }

        [MenuItem(CMD_GIT + "/All/上传 Pull Add Commit Push", false, 5)]
        private static async void GITUpload()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Upload(list, true, false, false);
        }

        [MenuItem(CMD_GIT + "/All/清理 Clean/-FDX 强制清理文件夹 不受忽略文件影响", false, 6)]
        private static async void CleanFDX()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Clean(list, "-fdx", false);
        }

        [MenuItem(CMD_GIT + "/All/清理 Clean/-FD 强制清理文件夹", false, 6)]
        private static async void CleanFD()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Clean(list, "-fd", false);
        }

        [MenuItem(CMD_GIT + "/All/重置 Reset/--Hard 重置 [分支 暂存区 工作区]", false, 6)]
        private static async void ResetHard()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.ResetHard(list, false);
        }

        [MenuItem(CMD_GIT + "/All/重置 Reset/--Keep 重置 [索引] 如果提交和HEAD之间的文件与HEAD不同，则重置将中止", false, 6)]
        private static async void ResetKeep()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.ResetKeep(list, false);
        }

        [MenuItem(CMD_GIT + "/All/重置 Reset/--Merge 重置 [索引 暂存区] 更改和索引产生 重置将被终止", false, 6)]
        private static async void ResetMerge()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.ResetMerge(list, false);
        }

        [MenuItem(CMD_GIT + "/All/重置 Reset/--Mixed 重置 [分支 暂存区]", false, 6)]
        private static async void ResetMixed()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.ResetMixed(list, false);
        }

        [MenuItem(CMD_GIT + "/All/重置 Reset/--Soft 重置 [分支]", false, 6)]
        private static async void ResetSoft()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.ResetSoft(list, false);
        }

        #endregion
    }
}