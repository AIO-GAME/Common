using System.IO;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public static partial class MenuItem_Tools
    {
        [MenuItem("AIO/Home Page", false, -100)]
        public static void OpenHome()
        {
            Application.OpenURL("https://github.com/AIO-GAME");
        }

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

#if UNITY_EDITOR_WIN

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Calc) + " - 计算器")]
        public static async void OpenCalc() => await PrWin.Open.Calc();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Devmgmt) + " - 设备管理器")]
        public static async void OpenDevmgmt() => await PrWin.Open.Devmgmt();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Notepad) + " - 记事本")]
        public static async void OpenNotepad() => await PrWin.Open.Notepad();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Mspaint) + " - 画图")]
        public static async void OpenMspaint() => await PrWin.Open.Mspaint();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Mstsc) + " - 远程桌面")]
        public static async void OpenMstsc() => await PrWin.Open.Mstsc();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Explorer) + " - 资源管理器")]
        public static async void OpenExplorer() => await PrWin.Open.Explorer();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Diskmgmt) + " - 磁盘管理")]
        public static async void OpenDiskmgmt() => await PrWin.Open.Diskmgmt();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Fsmgmt) + " - 共享文件夹管理器")]
        public static async void OpenFsmgmt() => await PrWin.Open.Fsmgmt();

        [MenuItem("AIO/Win/" + nameof(PrWin.Open.Lusrmgr) + " - 本地用户和组")]
        public static async void OpenLusrmgr() => await PrWin.Open.Lusrmgr();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Perfmon) + " - 性能监视器")]
        // public static async void OpenPerfmon() => await PrWin.Open.Perfmon();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Magnify) + " - 放大镜")]
        // public static async void OpenMagnify() => await PrWin.Open.Magnify();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Eventvwr) + " - 事件查看器")]
        // public static async void OpenEventvwr() => await PrWin.Open.Eventvwr();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Regedt32) + " - 注册表")]
        // public static async void OpenRegedit() => await PrWin.Open.Regedt32();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Taskmgr) + " - 任务管理器")]
        // public static async void OpenTaskmgr() => await PrWin.Open.Taskmgr();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Wmplayer) + " - 媒体播放器")]
        // public static async void OpenWmplayer() => await PrWin.Open.Wmplayer();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Winver) + " - 系统版本")]
        // public static async void OpenWinver() => await PrWin.Open.Winver();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Control) + " - 控制面板")]
        // public static async void OpenControl() => await PrWin.Open.Control();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Intl) + " - 区域和语言")]
        // public static async void OpenIntl() => await PrWin.Open.Intl();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Mdsched) + " - 内存诊断工具")]
        // public static async void OpenMdsched() => await PrWin.Open.Mdsched();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Osk) + " - 屏幕键盘")]
        // public static async void OpenOsk() => await PrWin.Open.Osk();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Cmd) + " - 命令提示符")]
        // public static async void OpenCmd() => await PrWin.Open.Cmd();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Diskpart) + " - 磁盘分区")]
        // public static async void OpenDiskpart() => await PrWin.Open.Diskpart();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Dfrgui) + " - 磁盘碎片整理")]
        // public static async void OpenDfrgui() => await PrWin.Open.Dfrgui();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Dxdiag) + " - DirectX诊断工具")]
        // public static async void OpenDxdiag() => await PrWin.Open.Dxdiag();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.PsR) + " - PowerShell")]
        // public static async void OpenPsR() => await PrWin.Open.PsR();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.RecoveryDrive) + " - 创建恢复驱动器")]
        // public static async void OpenRecoveryDrive() => await PrWin.Open.RecoveryDrive();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Msinfo32) + " - 系统信息")]
        // public static async void OpenMsinfo32() => await PrWin.Open.Msinfo32();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Msconfig) + " - 系统配置")]
        // public static async void OpenMsconfig() => await PrWin.Open.Msconfig();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Rstrui) + " - 系统还原")]
        // public static async void OpenRstrui() => await PrWin.Open.Rstrui();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Sdclt) + " - 备份和还原")]
        // public static async void OpenSdclt() => await PrWin.Open.Sdclt();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Sigverif) + " - 文件签名验证")]
        // public static async void OpenSigverif() => await PrWin.Open.Sigverif();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Sndvol) + " - 音量控制")]
        // public static async void OpenSndvol() => await PrWin.Open.Sndvol();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Stikynot) + " - 便笺")]
        // public static async void OpenStikynot() => await PrWin.Open.Stikynot();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Sysdm) + " - 系统属性")]
        // public static async void OpenSysdm() => await PrWin.Open.Sysdm();

        // [MenuItem("AIO/Win/" + nameof(PrWin.Open.Tpm) + " - TPM管理器")]
        // public static async void OpenTpm() => await PrWin.Open.Tpm();

#endif
    }
}