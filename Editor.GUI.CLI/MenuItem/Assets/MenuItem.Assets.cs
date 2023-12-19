/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-08-10
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System.IO;
using UnityEditor;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.UEditor
{
    public static partial class EditorMenu_Assets
    {
        private const string AIO_DLL_TITLE = "Assets/Open C# Project AIO DLL";
        private const string AIO_NET_TITLE = "Assets/Open C# Project Net DLL";

        [MenuItem(AIO_DLL_TITLE, true, 1000)]
        public static bool OpenDllProject()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var path = Path.Combine(info.resolvedPath, "Tools~", "ALL.sln");
            return File.Exists(path);
        }

        [MenuItem(AIO_NET_TITLE, true, 1000)]
        public static bool OpenNetProject()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var path = Path.Combine(info.resolvedPath, "Tools~", "Net.sln");
            return File.Exists(path);
        }

#if UNITY_EDITOR_WIN
        [MenuItem(AIO_DLL_TITLE)]
        public static async void OpenDllProjectWIN()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var executor = PrCmd.Create().Input(
                $"start \"AIO DLL Project\" {Path.Combine(info.resolvedPath, "Tools~", "ALL.sln")} /B /Max /HIGH");
            (await executor.Async()).Debug();
        }

        [MenuItem(AIO_NET_TITLE)]
        public static async void OpenNetProjectWIN()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var executor = PrCmd.Create().Input(
                $"start \"AIO DLL Project\" {Path.Combine(info.resolvedPath, "Tools~", "Net.sln")} /B /Max /HIGH");
            (await executor.Async()).Debug();
        }
#endif

#if UNITY_EDITOR_OSX
        [MenuItem(AIO_DLL_TITLE)]
        public static async void OpenDllProjectOSX()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var executor = PrMac.Open.Path(Path.Combine(info.resolvedPath, "Tools~", "ALL.sln"));
            (await executor.Async()).Debug();
        }
        
        [MenuItem(AIO_NET_TITLE)]
        public static async void OpenNetProjectWIN()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var executor = PrMac.Open.Path(Path.Combine(info.resolvedPath, "Tools~", "Net.sln"));
            (await executor.Async()).Debug();
        }
#endif
    }
}