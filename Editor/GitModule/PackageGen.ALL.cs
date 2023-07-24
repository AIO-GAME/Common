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

        [MenuItem(CMD_Git + CMD_Git_Push, true, DEFAULT)]
        [MenuItem(CMD_Git + CMD_Git_Pull, true, DEFAULT + 100)]
        [MenuItem(CMD_Git + CMD_Git_Clone, true, DEFAULT + 200)]
        [MenuItem(CMD_Git + CMD_Git_Add, true, DEFAULT + 300)]
        [MenuItem(CMD_Git + CMD_Git_Commit, true, DEFAULT + 400)]
        [MenuItem(CMD_Git + CMD_Git_Upload, true, DEFAULT + 500)]
        [MenuItem(CMD_Git + CMD_Git_RemoteSetUrl, true, DEFAULT + 600)]
        [MenuItem(CMD_Git + CMD_Git_Clean, true, DEFAULT + 1000)]
        public static void Refresh()
        {
            AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#endif
        }

        private static IEnumerable<PackageInfo> GetInfo()
        {
            var packageInfos = AssetDatabase.FindAssets("package", new string[] { "Packages" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath)
                .GroupBy(x => x.assetPath)
                .Select(x => x.First())
                .Where(x => Directory.Exists(Path.Combine(x.resolvedPath, ".git")))
                .ToList();

            return packageInfos;
        }

        #region Git All

        [MenuItem(CMD_Git + "/All/克隆", false, 0)]
        public static async void GITClone()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, "AutoGitClone.ini");
            var PackageData = JsonConvert.DeserializeObject<Hashtable>(File.ReadAllText(FilePath));
            await PrPlatform.Git.Clone(PackagePath, PackageData.Get<List<string>>("URL"), false);
        }

        [MenuItem(CMD_Git + "/All/添加", false, 1)]
        public static async void GITAdd()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Add(list, false);
        }

        [MenuItem(CMD_Git + "/All/拉取", false, 2)]
        public static async void GITPull()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Pull(list, false);
        }

#if UNITY_EDITOR_WIN
        [MenuItem(CMD_Git + "/All/拉取分支", false, 2)]
        public static async void GITPullBranch()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrWin.Git.PullBranch(list, false);
        }
#endif

        [MenuItem(CMD_Git + "/All/推送", false, 3)]
        public static async void GITPush()
        {
            var list = GetInfo()
                .Select(x => (x.resolvedPath, ""))
                .ToList();
            await PrPlatform.Git.Push(list, false);
        }

        [MenuItem(CMD_Git + "/All/提交", false, 4)]
        public static async void GITCommit()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Commit(list, false);
        }

        [MenuItem(CMD_Git + "/All/上传", false, 5)]
        public static async void GITUpload()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Upload(list, true, false, false);
        }

        [MenuItem(CMD_Git + "/All/清理", false, 6)]
        public static async void GITClean()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.Clean(list, "-fd -x", false);
        }

        [MenuItem(CMD_Git + "/All/设置关联远端库", false, 7)]
        public static async void GITGitRemoteSetUrl()
        {
            var list = GetInfo()
                .Select(x => x.resolvedPath)
                .ToList();
            await PrPlatform.Git.RemoteSetUrl(list, false);
        }

        #endregion
    }
}