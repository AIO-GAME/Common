/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-24
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.Text.RegularExpressions;
using AIO;
using UnityEditor;
using UnityEngine;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// Package
    /// </summary>
    public partial class Package
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

        [MenuItem("Package/" + Command_Git_Remote_Pull + "/" + Label)]
        public static void GitRemoteSetUrl()
        {
            PrPlatform.Git.RemoteSetUrl(URL, false).Async();
        }

        [MenuItem("Package/" + Command_Git_Clean_FDX + "/" + Label)]
        public static async void GitClean()
        {
            await PrGit.Clean.ForceDirectoryx(URL).Async();
        }

        [MenuItem("Package/" + Command_Git_Pull + "/" + Label)]
        public static void GitPull()
        {
            PrPlatform.Git.Pull(URL, false).Async();
        }

        [MenuItem("Package/" + Command_Git_Commit + "/" + Label)]
        public static void GitCommit()
        {
            PrPlatform.Git.Commit((URL, null), false).Async();
        }

        [MenuItem("Package/" + Command_Git_Upload + "/" + Label)]
        public static void GitUpload()
        {
            PrPlatform.Git.Upload(URL, false, false, false).Async();
        }

        [MenuItem("Package/" + Command_Git_Add + "/" + Label)]
        public static void GitAdd()
        {
            PrPlatform.Git.Add(URL, false).Async();
        }

        [MenuItem("Package/" + Command_Git_Push + "/" + Label)]
        public static void GitPush()
        {
            PrPlatform.Git.Push((URL, null), false).Async();
        }
    }
}