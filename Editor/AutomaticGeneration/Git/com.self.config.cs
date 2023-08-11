/*|============================================|*|
|*|Author:        |*|Automatic Generation      |*|
|*|Date:          |*|2023-08-11                |*|
|*|=============================================*/

namespace AIO.UEditor
{
    using System;
    using UnityEditor;
    using UnityEngine;
    using System.IO;

    /// <summary>
    /// Git Manager AIO Config
    /// </summary>
    [InitializeOnLoad]
    internal static partial class GIT_COM_SELF_CONFIG
    {
        internal const string URL = "Packages/com.blz.config";
        internal const string DisplayName = "AIO Config";
        internal const string PackageName = "com.self.config";

        static GIT_COM_SELF_CONFIG()
        {
            Refresh();
        }

        [MenuItem("Git/" + DisplayName + "/打开 Open", false, 0)]
        internal static async void Open()
        {
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Combine(URL, "package.json"));
            await PrPlatform.Open.Path(Application.dataPath.Replace("Assets", URL));
        }

        private static bool HasUpdate = false;

        [MenuItem("Git/" + DisplayName + "/刷新 Refresh", false, 1)]
        internal static async void Refresh()
        {
            var ret = await PrGit.Helper.GetBehind(Application.dataPath.Replace("Assets", URL));
            HasUpdate = ret > 0;
            if (ret < 0)
            {
                Debug.LogWarning($"Refresh : 本地Git库版本 提交数超过 远程库版本 : {Math.Abs(ret)}");
                return;
            }
        }

        [MenuItem("Git/" + DisplayName + "/拉取 Pull", true)]        private static bool GetHasUpdate()
        {
            return HasUpdate;
        }

        [MenuItem("Git/" + DisplayName + "/添加 Add", false, 101)]
        internal static async void Add()
        {
            await PrPlatform.Git.Add(
                Application.dataPath.Replace("Assets", URL), ".", false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/拉取 Pull", false, 102)]
        internal static async void Pull()
        {
            await PrPlatform.Git.Pull(
                Application.dataPath.Replace("Assets", URL), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/推送 Push", false, 103)]
        internal static async void Push()
        {
            await PrPlatform.Git.Push(
                (Application.dataPath.Replace("Assets", URL), null), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/提交 Commit", false, 104)]
        internal static async void Commit()
        {
            await PrPlatform.Git.Commit(
                (Application.dataPath.Replace("Assets", URL),null), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/上传 Upload", false, 105)]
        internal static async void Upload()
        {
            await PrPlatform.Git.Upload(
                Application.dataPath.Replace("Assets", URL), false, false, false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/清理 Clean", false, 106)]
        internal static async void Clean()
        {
            await PrPlatform.Git.Clean(
                Application.dataPath.Replace("Assets", URL), "-fd -x", false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/重置 Reset/--Hard 重置 [分支 暂存区 工作区]", false)]
        internal static async void ResetHard()
        {
            await PrPlatform.Git.ResetHard(
                Application.dataPath.Replace("Assets", URL), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/重置 Reset/--Keep 重置 [索引] 如果提交和HEAD之间的文件与HEAD不同，则重置将中止", false)]
        internal static async void ResetKeep()
        {
            await PrPlatform.Git.ResetKeep(
                Application.dataPath.Replace("Assets", URL), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/重置 Reset/--Merge 重置 [索引 暂存区] 更改和索引产生 重置将被终止", false)]
        internal static async void ResetMerge()
        {
            await PrPlatform.Git.ResetMerge(
                Application.dataPath.Replace("Assets", URL), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/重置 Reset/--Mixed 重置 [分支 暂存区]", false)]
        internal static async void ResetMixed()
        {
            await PrPlatform.Git.ResetMixed(
                Application.dataPath.Replace("Assets", URL), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/重置 Reset/--Soft 重置 [分支]", false)]
        internal static async void ResetSoft()
        {
            await PrPlatform.Git.ResetSoft(
                Application.dataPath.Replace("Assets", URL), false
            ).Async();
        }

        [MenuItem("Git/" + DisplayName + "/设置关联远端库 RemoteSetUrl", false, 107)]
        internal static async void RemoteSetUrl()
        {
            await PrPlatform.Git.RemoteSetUrl(
                Application.dataPath.Replace("Assets", URL), false
            ).Async();
        }
    }
}