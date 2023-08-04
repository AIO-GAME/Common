/*|==========|*|
|*|Author:   |*| -> SAM
|*|Date:     |*| -> 2023-05-31
|*|==========|*/

using System.IO;
using AIO;
using UnityEditor;

namespace AIO.UEditor
{
    /// <summary>
    /// Menu_Open
    /// </summary>
    public partial class EditorMenu
    {
        private static class Open
        {
            private const string Title = "Tools/Open/Unity Editor Data/";

            [MenuItem(Title + "Data")]
            public static async void OpenApplicationContentsPath()
            {
                await PrPlatform.Open.Path(EditorApplication.applicationContentsPath);
            }

            [MenuItem(Title + "Tools")]
            public static async void OpenTools()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "Tools"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "Managed DLL")]
            public static async void OpenManagedDll()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "Managed"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "CG Includes")]
            public static async void OpenCGIncludes()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "CGIncludes"
                );
                await PrPlatform.Open.Path(path);
            }

            #region IL2CPP

            [MenuItem(Title + "il2cpp/cpp")]
            public static async void Openil2cpp_cpp()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "il2cpp", "libil2cpp"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "il2cpp/mono")]
            public static async void Openil2cpp_mono()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "il2cpp", "libmono"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "il2cpp/build")]
            public static async void Openil2cpp_build()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "il2cpp", "build"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "il2cpp/external")]
            public static async void Openil2cpp_external()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "il2cpp", "external"
                );
                await PrPlatform.Open.Path(path);
            }

            #endregion

            #region Build Playback Engines

            [MenuItem(Title + "Playback Engines Build/Android")]
            public static async void OpenApplicationPathPlaybackEnginesAndroid()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "PlaybackEngines", "AndroidPlayer"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "Playback Engines Build/IOS")]
            public static async void OpenApplicationPathPlaybackEnginesIOS()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "PlaybackEngines", "iOSSupport"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "Playback Engines Build/WebGL")]
            public static async void OpenApplicationPathPlaybackEnginesWebGL()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "PlaybackEngines", "WebGLSupport"
                );
                await PrPlatform.Open.Path(path);
            }

            [MenuItem(Title + "Playback Engines Build/Windows")]
            public static async void OpenApplicationPathPlaybackEnginesWindows()
            {
                var path = Path.Combine(EditorApplication.applicationContentsPath,
                    "PlaybackEngines", "windowsstandalonesupport"
                );
                await PrPlatform.Open.Path(path);
            }

            #endregion
        }
    }
}
