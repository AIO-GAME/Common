using System.Diagnostics;
using System.IO;
using UnityEditor;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.UEditor
{
    internal static class EditorMenu_Assets
    {
        private const string AIO_DLL_TITLE = "AIO/Solution/AIO C# Project";
        private const string AIO_NET_TITLE = "AIO/Solution/NET C# Project";

        private static PackageInfo PackageInfo;

        private static PackageInfo GetPackageInfo() => PackageInfo ?? (PackageInfo = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly));

        private static void Run(string work)
        {
            var exe = EditorPrefs.GetString("kScriptsDefaultApp");
            if (string.IsNullOrEmpty(exe))
            {
                var result = EditorUtility.DisplayDialog("Warning",
                                                         "Please set the external script editor in the Unity preferences.",
                                                         "Open Preferences",
                                                         "Cancel");
                if (result) SettingsService.OpenUserPreferences("Preferences/External Tools");
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName               = exe,
                Arguments              = $"\"{Path.Combine(GetPackageInfo().resolvedPath, "Tools~", $"{work}.sln")}\"",
                UseShellExecute        = false,
                RedirectStandardOutput = true,
                CreateNoWindow         = true
            });
        }

        [MenuItem(AIO_DLL_TITLE, true, 1000)]
        private static bool OpenDllProject() => File.Exists(Path.Combine(GetPackageInfo().resolvedPath, "Tools~", $"{nameof(ALL)}.sln"));

        [MenuItem(AIO_NET_TITLE, true, 1000)]
        private static bool OpenNetProject() => File.Exists(Path.Combine(GetPackageInfo().resolvedPath, "Tools~", $"{nameof(Net)}.sln"));

        [MenuItem(AIO_DLL_TITLE)]
        private static void ALL() => Run(nameof(ALL));

        [MenuItem(AIO_NET_TITLE)]
        private static void Net() => Run(nameof(Net));
    }
}