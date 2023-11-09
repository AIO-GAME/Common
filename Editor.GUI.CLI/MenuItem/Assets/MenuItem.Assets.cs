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
        private const string TITLE = "Assets/Open C# Project AIO DLL";

        [MenuItem(TITLE, true, 1000)]
        public static bool OpenDllProject()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var path = Path.Combine(info.resolvedPath, "Tools~", "ALL.sln");
            return File.Exists(path);
        }

#if UNITY_EDITOR_WIN
        [MenuItem(TITLE)]
        public static async void OpenDllProjectWIN()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var executor = PrCmd.Create().Input(
                $"start \"AIO DLL Project\" {Path.Combine(info.resolvedPath, "Tools~", "ALL.sln")} /B /Max /HIGH");
            (await executor.Async()).Debug();
        }
#endif

#if UNITY_EDITOR_OSX
        [MenuItem(TITLE)]
        public static async void OpenDllProjectOSX()
        {
            var info = PackageInfo.FindForAssembly(typeof(EditorMenu_Assets).Assembly);
            var executor = PrMac.Open.Path(Path.Combine(info.resolvedPath, "Tools~", "ALL.sln"));
            (await executor.Async()).Debug();
        }
#endif
    }
}