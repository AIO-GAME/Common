/*|==========|*|
|*|Author:   |*| -> SAM
|*|Date:     |*| -> 2023-06-03
|*|==========|*/

#if SUPPORT_YOOASSET

using System.IO;
using AIO;
using UnityEditor;
using UnityEngine;

namespace UnityEditor
{
    /// <summary>
    /// Unity 编辑器工具类
    /// </summary>
    internal static class YooAssetsTools
    {
        [MenuItem("YooAsset/Send FTP")]
        public static async void SendFTP()
        {
            var ftpRoot = @"E:\WWW\yooasset\";

            var platform = EditorUserBuildSettings.activeBuildTarget;
            var sanBox = Application.dataPath.Replace("Assets", "Bundles");
            var sanBoxList = UtilsGen.IO.GetFoldersInfo(sanBox, "*", SearchOption.TopDirectoryOnly);
            foreach (var art in sanBoxList)
            {
                var full = Path.Combine(art.FullName, platform.ToString());
                if (!UtilsGen.IO.ExistsFolder(full)) continue;

                var target = Path.Combine(ftpRoot, art.Name, platform.ToString());
                if (UtilsGen.IO.Exists(target))
                {
                    await UtilsGen.IO.DeleteFolderAsync(target, SearchOption.AllDirectories, true);
                }

                Debug.LogFormat("Copy {0} to {1}", full, target);
                UtilsGen.IO.CopyFolderAll(full, target, true);
            }
        }

        [MenuItem("YooAsset/Open Bundles")]
        public static async void OpenBundles()
        {
            var path = Application.dataPath.Replace("Assets", "Bundles");
            if (UtilsGen.IO.ExistsFolder(path)) await PrPlatform.Folder.Create(path);
            await PrPlatform.Open.Path(path);
        }

        [MenuItem("YooAsset/Clear Bundles")]
        public static async void ClearBundles()
        {
            var path = Application.dataPath.Replace("Assets", "Bundles");
            if (UtilsGen.IO.ExistsFolder(path))
                await PrPlatform.Folder.Del(Application.dataPath.Replace("Assets", "Bundles"));
        }
    }
}

#endif