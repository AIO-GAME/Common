/*|==========|*|
|*|Author:   |*| -> SAM
|*|Date:     |*| -> 2023-06-03
|*|==========|*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using YooAsset;

namespace AIO.UEditor.YooAsset
{
    /// <summary>
    /// Unity 编辑器工具类
    /// </summary>
    public static class YooAssetsTools
    {
        [MenuItem("YooAsset/Create Config")]
        public static void CreateConfig()
        {
            var BundlesDir = Application.dataPath.Replace("Assets", "Bundles");
            if (!Directory.Exists(BundlesDir))
            {
                Debug.LogWarningFormat("Bundles 目录不存在 : 无需创建配置文件");
                return;
            }

            var BundlesConfigDir = Path.Combine(BundlesDir, "Version");
            if (Directory.Exists(BundlesConfigDir)) Directory.Delete(BundlesConfigDir, true);
            Directory.CreateDirectory(BundlesConfigDir);

            var TabelDic = new Dictionary<BuildTarget, Hashtable>();
            TabelDic.Add(BuildTarget.Android, new Hashtable());
            TabelDic.Add(BuildTarget.WebGL, new Hashtable());
            TabelDic.Add(BuildTarget.iOS, new Hashtable());
            TabelDic.Add(BuildTarget.StandaloneWindows, new Hashtable());
            TabelDic.Add(BuildTarget.StandaloneWindows64, new Hashtable());
            TabelDic.Add(BuildTarget.StandaloneOSX, new Hashtable());

            var BundlesInfo = new DirectoryInfo(BundlesDir);
            var versions = new List<DirectoryInfo>();

            foreach (var package in BundlesInfo.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                if (package.Name.StartsWith("Version")) continue;
                var PackageInfo = new DirectoryInfo(package.FullName);
                foreach (var PlatformInfo in PackageInfo.GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    switch (PlatformInfo.Name)
                    {
                        case nameof(BuildTarget.Android):
                        case nameof(BuildTarget.WebGL):
                        case nameof(BuildTarget.iOS):
                        case nameof(BuildTarget.StandaloneWindows):
                        case nameof(BuildTarget.StandaloneWindows64):
                        case nameof(BuildTarget.StandaloneOSX):
                            break;
                        default: continue;
                    }

                    versions.Clear();
                    foreach (var version in PlatformInfo.GetDirectories("*", SearchOption.TopDirectoryOnly))
                    {
                        if (version.Name.StartsWith("OutputCache")) continue;
                        if (version.Name.StartsWith("Simulate")) continue;
                        versions.Add(version);
                    }

                    if (versions.Count <= 0) continue;
                    var last = GetLastWriteTimeUtc(versions);
                    if (Enum.TryParse<BuildTarget>(PlatformInfo.Name, out var enums))
                        TabelDic[enums].Set(package.Name, last.Name);
                    else Debug.LogWarningFormat("未知平台 : {0}", PlatformInfo.Name);
                }
            }

            var BundlesConfigInfo = new DirectoryInfo(BundlesConfigDir);
            foreach (var hashtable in TabelDic)
            {
                if (hashtable.Value.Count <= 0) continue;
                var filename = hashtable.Key.ToString();
                var filePath = Path.Combine(BundlesConfigInfo.FullName, string.Concat(filename, ".json"));
                File.WriteAllText(filePath, JsonConvert.SerializeObject(hashtable.Value));
            }
        }

        /// <summary>
        /// 获取最新的文件夹
        /// </summary>
        /// <param name="directoryInfos">文件夹列表</param>
        /// <returns><see cref="System.IO.DirectoryInfo"/>文件夹信息</returns>
        private static DirectoryInfo GetLastWriteTimeUtc(ICollection<DirectoryInfo> directoryInfos)
        {
            DirectoryInfo last = null;
            foreach (var directoryInfo in directoryInfos)
            {
                if (last is null)
                {
                    last = directoryInfo;
                    continue;
                }

                if (last.LastWriteTimeUtc < directoryInfo.LastWriteTimeUtc)
                {
                    last = directoryInfo;
                }
            }

            return last;
        }

        [MenuItem("YooAsset/Open/Bundles")]
        public static async void OpenBundles()
        {
            var path = Application.dataPath.Replace("Assets", "Bundles");
            if (AHelper.IO.ExistsFolder(path)) await PrPlatform.Folder.Create(path);
            await PrPlatform.Open.Path(path);
        }

        [MenuItem("YooAsset/Open/Sandbox")]
        public static async void OpenSandbox()
        {
            var path = Application.dataPath.Replace("Assets", "Sandbox");
            if (AHelper.IO.ExistsFolder(path)) await PrPlatform.Folder.Create(path);
            await PrPlatform.Open.Path(path);
        }

        [MenuItem("YooAsset/Clear/Bundles")]
        public static async void ClearBundles()
        {
            var path = Application.dataPath.Replace("Assets", "Bundles");
            if (AHelper.IO.ExistsFolder(path))
                await PrPlatform.Folder.Del(path);
        }

        [MenuItem("YooAsset/Clear/Sandbox")]
        public static async void ClearSandbox()
        {
            var path = Application.dataPath.Replace("Assets", "Sandbox");
            if (AHelper.IO.ExistsFolder(path))
                await PrPlatform.Folder.Del(path);
        }
    }
}