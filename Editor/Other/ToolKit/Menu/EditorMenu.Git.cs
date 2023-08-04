/*|=============================================↩|
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

namespace AIO.UEditor
{
    public partial class EditorMenu
    {
        internal static Type[] types = new Type[] { typeof(PluginsManagerWindow), typeof(PackageManagerWindow) };

        [MenuItem("Tools/Setting/Package")]
        public static void SettingWindow()
        {
            PackageManagerWindow.Open(types);
        }

        [MenuItem("Tools/Setting/Plugins")]
        public static void PluginsWindow()
        {
            PluginsManagerWindow.Open(types);
        }

#if UNITY_EDITOR_WIN
        [MenuItem("Assets/Open C# Project DLL")]
        public static async void OpenDllProject()
        {
            var info = UnityEditor.PackageManager.PackageInfo.FindForAssembly(typeof(EditorMenu).Assembly);
            var executor = PrCmd.Create().Input($"start \"DLL Project\" {Path.Combine(info.resolvedPath, "Tools~", "ALL.sln")} /B /Max /HIGH");
            (await executor.Async()).Debug();
        }
#elif UNITY_EDITOR_OSX
        [MenuItem("Assets/Open C# Project DLL")]
        public static async void OpenDllProject()
        {
            var info = UnityEditor.PackageManager.PackageInfo.FindForAssembly(typeof(EditorMenu).Assembly);
            var executor = PrMac.Open.Path(Path.Combine(info.resolvedPath, "Tools~", "ALL.sln"));
            (await executor.Async()).Debug();
        }
#endif
    }
}
