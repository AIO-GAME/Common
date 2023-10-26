/*|==========|*|
|*|Author:   |*| -> XINAN
|*|Date:     |*| -> 2023-05-31
|*|==========|*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    internal static class MenuItem_Tools
    {
        [MenuItem("AIO/Tools/Open/Application/Persistent Data Path")]
        public static async void OpenApplicationPersistentDataPath()
        {
            await PrPlatform.Open.Path(Application.persistentDataPath);
        }

        [MenuItem("AIO/Tools/Open/Application/Data Path")]
        public static async void OpenApplicationDataPath()
        {
            await PrPlatform.Open.Path(Application.dataPath);
        }

        [MenuItem("AIO/Tools/Open/Application/Streaming Assets Path")]
        public static async void OpenApplicationStreamingAssetsPath()
        {
            await PrPlatform.Open.Path(Application.streamingAssetsPath);
        }

        [MenuItem("AIO/Tools/Open/Application/Console Log Path")]
        public static async void OpenApplicationConsoleLogPath()
        {
            await PrPlatform.Open.Path(Application.consoleLogPath);
        }

        [MenuItem("AIO/Tools/Open/Application/Temporary Cache Path")]
        public static async void OpenApplicationTemporaryCachePath()
        {
            await PrPlatform.Open.Path(Application.temporaryCachePath);
        }

        private const string Title = "AIO/Tools/Open/Editor Application/";

        [MenuItem(Title + "Application Contents Path")]
        public static async void OpenApplicationContentsPath()
        {
            await PrPlatform.Open.Path(EditorApplication.applicationContentsPath);
        }

        [MenuItem(Title + "Application Contents Path + Tools")]
        public static async void OpenTools()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "Tools"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + Managed (DLL)")]
        public static async void OpenManagedDll()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "Managed"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + CG Includes")]
        public static async void OpenCGIncludes()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "CGIncludes"
            );
            await PrPlatform.Open.Path(path);
        }

        #region IL2CPP

        [MenuItem(Title + "Application Contents Path + il2cpp cpp")]
        public static async void Openil2cpp_cpp()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "il2cpp", "libil2cpp"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + il2cpp mono")]
        public static async void Openil2cpp_mono()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "il2cpp", "libmono"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + il2cpp build")]
        public static async void Openil2cpp_build()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "il2cpp", "build"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + il2cpp external")]
        public static async void Openil2cpp_external()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "il2cpp", "external"
            );
            await PrPlatform.Open.Path(path);
        }

        #endregion

        #region Build Playback Engines

        [MenuItem(Title + "Application Contents Path + Playback Engines Build Android")]
        public static async void OpenApplicationPathPlaybackEnginesAndroid()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "PlaybackEngines", "AndroidPlayer"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + Playback Engines Build IOS")]
        public static async void OpenApplicationPathPlaybackEnginesIOS()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "PlaybackEngines", "iOSSupport"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + Playback Engines Build WebGL")]
        public static async void OpenApplicationPathPlaybackEnginesWebGL()
        {
            var path = Path.Combine(EditorApplication.applicationContentsPath,
                "PlaybackEngines", "WebGLSupport"
            );
            await PrPlatform.Open.Path(path);
        }

        [MenuItem(Title + "Application Contents Path + Playback Engines Build Windows")]
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