﻿/*|=============================================↩|
|*|Author:          |XINAN                     |↩|
|*|Date:            |2022-11-23                |↩|
|*|Time:            |16:42:47                  |↩|
|*|E-Mail:          |1398581458@qq.com         |↩|
|*|=============================================*/

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AIO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace AIO.Unity.Editor
{
    public partial class EditorMenu
    {
        private const string GitLabel = "Package/";
        private const int DEFAULT = 0;

        [MenuItem(GitLabel + "Git Clone", true, DEFAULT)]
        [MenuItem(GitLabel + "Git Pull", true, DEFAULT + 100)]
        [MenuItem(GitLabel + "Git Add", true, DEFAULT + 200)]
        [MenuItem(GitLabel + "Git Commit", true, DEFAULT + 300)]
        [MenuItem(GitLabel + "Git Push", true, DEFAULT + 400)]
        [MenuItem(GitLabel + "Git Upload", true, DEFAULT + 500)]
        [MenuItem(GitLabel + "Git Remote Pull", true, DEFAULT + 600)]
        [MenuItem(GitLabel + "Initialize", true, DEFAULT + 1000)]
        [MenuItem(GitLabel + "Un Initialize", true, DEFAULT + 1100)]
        public static void Refresh()
        {
            AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#endif
        }

        #region Git

#if MONKEYCOMMANDER
        [MonKey.Command("Git Remote Pull All",
            Help = "所有Git库 设置远端地址",
            Category = "Git",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            Order = 0,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem(GitLabel + "Git Remote Pull/All", false, 1)]
        public static async void GITGitRemoteSetUrl()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            PackageData.GetNames();
            var vs = PackageData.Names
                .Select(item => Path.Combine(PackagePath, item))
                .Where(Directory.Exists).ToList();
            await PrPlatform.Git.RemoteSetUrl(vs, false);
        }

#if MONKEYCOMMANDER
        [MonKey.Command("Git Clone All",
            Help = "所有Git库 克隆",
            Category = "Git",
            Order = 1,
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem(GitLabel + "Git Clone/All", false, 1)]
        public static async void GITClone()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            await PrPlatform.Git.Clone(PackagePath, PackageData.URL, false);
        }

#if MONKEYCOMMANDER
        [MonKey.Command("Git Pull All",
            Help = "所有Git库 拉取",
            Category = "Git",
            Order = 2,
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem(GitLabel + "Git Pull/All", false, 1)]
        public static async void GITPull()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            PackageData.GetNames();
            var vs = PackageData.Names
                .Select(item => Path.Combine(PackagePath, item))
                .Where(Directory.Exists).ToList();

            await PrPlatform.Git.Pull(vs, false);
        }

#if UNITY_EDITOR_WIN
        [MenuItem(GitLabel + "Git Pull Branch/All", false, 1)]
        public static async void GITPullBranch()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            PackageData.GetNames();
            var vs = PackageData.Names
                .Select(item => Path.Combine(PackagePath, item))
                .Where(Directory.Exists).ToList();

            await PrWin.Git.PullBranch(vs, false);
        }
#endif

#if MONKEYCOMMANDER
        [MonKey.Command("Git Upload All",
            Help = "所有Git库 添加 提交 推送",
            Category = "Git",
            Order = 3,
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem(GitLabel + "Git Upload/All", false, 1)]
        public static async void GITUpload()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            PackageData.GetNames();
            var vs = PackageData.Names
                .Select(item => Path.Combine(PackagePath, item))
                .Where(Directory.Exists).ToList();
            await PrPlatform.Git.Upload(vs, true, false, false);
        }

#if MONKEYCOMMANDER
        [MonKey.Command("Git Push All",
            Help = "所有Git库推送",
            Category = "Git",
            Order = 4,
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem(GitLabel + "Git Push/All", false, 1)]
        public static async void GITPush()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            PackageData.GetNames();
            var vs = (from item in PackageData.Names
                select Path.Combine(PackagePath, item)
                into name
                where Directory.Exists(name)
                select (name, "")).ToList();

            await PrPlatform.Git.Push(vs, false);
        }

#if MONKEYCOMMANDER
        [MonKey.Command("Git Add All",
            Help = "所有Git库添加修改文件 ",
            Category = "Git",
            Order = 5,
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem(GitLabel + "Git Add/All", false, 1)]
        public static async void GITAdd()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            PackageData.GetNames();
            var vs = PackageData.Names
                .Select(item => Path.Combine(PackagePath, item))
                .Where(Directory.Exists).ToList();

            await PrPlatform.Git.Add(vs, false);
        }

#if MONKEYCOMMANDER
        [MonKey.Command("Git Commit All",
            Help = "提交所有Git库 ",
            Category = "Git",
            Order = 6,
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem(GitLabel + "Git Commit/All", false, 1)]
        public static async void GITCommit()
        {
            var PackagePath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagePath, PackageManagerWindow.PACKAGE_CLONE_FILE);
            var PackageData = JsonConvert.DeserializeObject<PackageData>(File.ReadAllText(FilePath));
            PackageData.GetNames();
            var vs = PackageData.Names
                .Select(item => Path.Combine(PackagePath, item))
                .Where(Directory.Exists).ToList();
            await PrPlatform.Git.Commit(vs, false);
        }

        #endregion

        internal static Type[] types = new Type[] { typeof(PluginsManagerWindow), typeof(PackageManagerWindow) };

#if MONKEYCOMMANDER
        [MonKey.Command("Unity Package Manager Windows",
            Help = "Unity扩展包管理面板",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false,
            Category = "Windows"
        )]
#endif
        [MenuItem(GitLabel + "Setting/Package")]
        public static void SettingWindow()
        {
            PackageManagerWindow.Open(types);
        }

#if MONKEYCOMMANDER
        [MonKey.Command("Plugins Manager Windows",
            Help = "插件管理面板",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false,
            Category = "Windows"
        )]
#endif
        [MenuItem(GitLabel + "Setting/Plugins")]
        public static void PluginsWindow()
        {
            PluginsManagerWindow.Open(types);
        }

#if UNITY_EDITOR_WIN
        [MenuItem("Assets/Open C# Project DLL")]
        public static async void OpenDllProject()
        {
            var executor = PrCmd.Create().Input($"start \"DLL Project\" {Path.Combine(Package.URL, "Tools~", "ALL.sln")} /B /Max /HIGH");
            (await executor.Async()).Debug();
        }
#endif
    }
}