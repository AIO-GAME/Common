﻿using System.Text.RegularExpressions;
using AIO;
using UnityEditor;
using UnityEngine;

namespace AIO.Unity.Editor
{
    public static partial class Package
    {
        internal static string URL { get; }
        internal const string Label = "Package";
        internal const string RegexName = "com.*.package";

        static Package()
        {
            var regex = new Regex(RegexName);
            foreach (var info in UtilsGen.IO.GetFoldersInfo(Application.dataPath.Replace("Assets", "Packages")))
                if (regex.IsMatch(info.Name))
                    URL = info.FullName;
        }

        internal const string Command_Git_Pull = "Git Pull";
        internal const string Command_Git_Push = "Git Push";
        internal const string Command_Git_Add = "Git Add";
        internal const string Command_Git_Commit = "Git Commit";
        internal const string Command_Git_Upload = "Git Upload";
        internal const string Command_Git_Remote_Pull = "Git Remote Pull";
        internal const string Command_Git_Clean_FDX = "Git Clean";
        
#if MONKEYCOMMANDER
        [MonKey.Command(Command_Git_Remote_Pull + " " + Label,
            Help = "设置远端GIT库 | Setting Remote Git Library",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            Order = 6,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false,
            Category = "Git"
        )]
#endif
        [MenuItem("Package/" + Command_Git_Remote_Pull + "/" + Label)]
        public static void GitRemoteSetUrl()
        {
            PrPlatform.Git.RemoteSetUrl(URL, false).Async();
        }

        [MenuItem("Package/" + Command_Git_Clean_FDX + "/" + Label)]
        public static void GitClean()
        {
            PrGit.Clean.ForceDirectoryx(URL).Async();
        }

#if MONKEYCOMMANDER
        [MonKey.Command(Command_Git_Pull + " " + Label,
            Help = "拉取GIT库 | Git Pull",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            Order = 5,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false,
            Category = "Git"
        )]
#endif
        [MenuItem("Package/" + Command_Git_Pull + "/" + Label)]
        public static void GitPull()
        {
            PrPlatform.Git.Pull(URL, false).Async();
        }

#if MONKEYCOMMANDER
        [MonKey.Command(Command_Git_Commit + " " + Label,
            Help = "提交GIT库 | Git Commit",
            Category = "Git",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            Order = 4,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Package/" + Command_Git_Commit + "/" + Label)]
        public static void GitCommit()
        {
            PrPlatform.Git.Commit((URL, null), false).Async();
        }

#if MONKEYCOMMANDER
        [MonKey.Command(Command_Git_Upload + " " + Label,
            Help = "添加 提交 推送 Git库 | Git Upload",
            Category = "Git",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            Order = 3,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Package/" + Command_Git_Upload + "/" + Label)]
        public static void GitUpload()
        {
            PrPlatform.Git.Upload(URL, false, false, false).Async();
        }

#if MONKEYCOMMANDER
        [MonKey.Command(Command_Git_Add + " " + Label,
            Help = "添加 Git库 | Git Add",
            Category = "Git",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            Order = 2,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Package/" + Command_Git_Add + "/" + Label)]
        public static void GitAdd()
        {
            PrPlatform.Git.Add(URL, false).Async();
        }


#if MONKEYCOMMANDER
        [MonKey.Command(Command_Git_Push + " " + Label,
            Help = "推送 Git库 | Git Push",
            Category = "Git",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            Order = 1,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Package/" + Command_Git_Push + "/" + Label)]
        public static void GitPush()
        {
            PrPlatform.Git.Push((URL, null), false).Async();
        }
    }
}