#region

using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using Debug = UnityEngine.Debug;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// ____DESC:   手动reload domain 工具
    /// </summary>
    internal static class ScriptCompileReloadTools
    {
        /* 说明
         * 关于域重载 https://docs.unity.cn/cn/2021.3/Manual/DomainReloading.html
         * EditorApplication.LockReloadAssemblies()和 EditorApplication.UnlockReloadAssemblies() 最好成对
         * 如果不小心LockReloadAssemblies3次 但是只UnlockReloadAssemblies了一次 那么还是不会重载 必须也要但是只UnlockReloadAssemblies3次
         */

        private const string MenuEnableManualReload      = "AIO/Tools/Script/Manual Reload Domain Open";
        private const string menuDispensableManualReload = "AIO/Tools/Script/Manual Reload Domain Close";
        private const string MenuReloadDomain            = "AIO/Tools/Script/Unlock Reload";

        private const string kManualReloadDomain = "ManualReloadDomain";

        /// <summary>
        /// 是否首次进入unity
        /// </summary>
        private const string kFirstEnterUnity = "FirstEnterUnity";

        /// <summary>
        /// 计时
        /// </summary>
        private const string kReloadDomainTimer = "ReloadDomainTimer";


        /**************************************************/
        /// <summary>
        /// 编译时间
        /// </summary>
        private static Stopwatch _CompileSW;

        /// <summary>
        /// 缓存数据 域重载之后数据会变成false 如果不是false 那么就要重载
        /// </summary>
        private static bool _TempData;

        /// <summary>
        /// 反射获取是否锁住
        /// </summary>
        /// <remarks>https://github.com/INeatFreak/unity-background-recompiler</remarks>
        private static MethodInfo _CanReloadAssembliesMethod;

        /// <summary>
        /// 是否手动reload
        /// </summary>
        private static bool _IsManualReload => PlayerPrefs.GetInt(kManualReloadDomain, -1) == 1;

        /// <summary>
        /// 是否锁住
        /// </summary>
        private static bool IsLocked
        {
            get
            {
                if (_CanReloadAssembliesMethod is null)
                    // source: https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/EditorApplication.bindings.cs#L154
                    _CanReloadAssembliesMethod = typeof(EditorApplication).GetMethod("CanReloadAssemblies",
                                                                                     BindingFlags.NonPublic | BindingFlags.Static);

                if (_CanReloadAssembliesMethod is null)
                {
                    Debug.LogError("Can't find CanReloadAssemblies method. It might have been renamed or removed.");
                    return false;
                }

                return !(bool)_CanReloadAssembliesMethod.Invoke(null, null);
            }
        }

        /**************************************************/


        [InitializeOnLoadMethod]
        private static void InitCompile()
        {
            _CompileSW = new Stopwatch();

            //**************不需要这个可以注释********************************
#if UNITY_2019_1_OR_NEWER
            CompilationPipeline.compilationStarted  -= OnCompilationStarted;
            CompilationPipeline.compilationStarted  += OnCompilationStarted;
            CompilationPipeline.compilationFinished -= OnCompilationFinished;
            CompilationPipeline.compilationFinished += OnCompilationFinished;
#endif
            //**************************************************************

            // 域重载事件监听
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload  -= OnAfterAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload  += OnAfterAssemblyReload;

            EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;


            // Bug 首次启动的时候 并不会马上设置
            //if (PlayerPrefs.HasKey(kManualReloadDomain))
            //{
            //    Menu.SetChecked(menuEnableManualReload, IsManualReload ? true : false);
            //    Menu.SetChecked(menuDispensableManualReload, IsManualReload ? false : true);
            //}


            FirstCheckAsync();
        }

        /// <summary>
        /// 首次打开检测
        /// </summary>
        private static async void FirstCheckAsync()
        {
            await Task.Delay(100);
            //判断是否首次打开
            //https://docs.unity.cn/cn/2021.3/ScriptReference/SessionState.html
            if (!SessionState.GetBool(kFirstEnterUnity, true)) return;
            SessionState.SetBool(kFirstEnterUnity, false);
            Menu.SetChecked(MenuEnableManualReload, _IsManualReload);
            Menu.SetChecked(menuDispensableManualReload, !_IsManualReload);

            if (_IsManualReload)
            {
                UnlockReloadDomain();
                LockReloadDomain();
            }

            Debug.Log(
                $"<color=lime><b>[LOG]</b></color> Current Reload Domain Status : {(_IsManualReload ? "Manual Mode" : "Auto Mode")}");
        }


        /// <summary>
        /// 运行模式改变
        /// </summary>
        /// <param name="state">状态</param>
        private static void EditorApplication_playModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredEditMode:
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    if (_TempData)
                    {
                        UnlockReloadDomain();
#if UNITY_2019_1_OR_NEWER
                        EditorUtility.RequestScriptReload();
#endif
                    }

                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    _TempData = true;
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }

        /// <summary>
        /// 当开始编译脚本
        /// </summary>
        private static void OnCompilationStarted(object obj)
        {
            if (!_IsManualReload) return;
            _CompileSW.Start();
            Debug.Log("<color=yellow>Begin Compile</color>");
        }

        /// <summary>
        /// 结束编译
        /// </summary>
        private static void OnCompilationFinished(object obj)
        {
            if (!_IsManualReload) return;
            _CompileSW.Stop();
            Debug.Log($"<color=yellow>End Compile 耗时:{_CompileSW.ElapsedMilliseconds} ms</color>");
            _CompileSW.Reset();
        }

        /// <summary>
        /// 开始 reload domain
        /// </summary>
        private static void OnBeforeAssemblyReload()
        {
            if (!_IsManualReload) return;
            Debug.Log("<color=yellow>Begin Reload Domain ......</color>");
            SessionState.SetInt(kReloadDomainTimer, (int)(EditorApplication.timeSinceStartup * 1000)); //记录时间
        }

        /// <summary>
        /// 结束 reload domain
        /// </summary>
        private static void OnAfterAssemblyReload()
        {
            if (!_IsManualReload) return;
            var timeMS = (int)(EditorApplication.timeSinceStartup * 1000) - SessionState.GetInt(kReloadDomainTimer, 0);
            Debug.Log($"<color=yellow>End Reload Domain 耗时:{timeMS} ms</color>");
            LockReloadDomain();
        }

        private static void LockReloadDomain()
        {
            if (IsLocked) return; //如果没有锁住 锁住
            EditorApplication.LockReloadAssemblies();
        }

        private static void UnlockReloadDomain()
        {
            if (!IsLocked) return; //如果锁住了 打开
            EditorApplication.UnlockReloadAssemblies();
        }

        [MenuItem(MenuEnableManualReload)]
        private static void EnableManualReloadDomain()
        {
            Debug.Log("<color=cyan>开启手动 Reload Domain</color>");

            Menu.SetChecked(MenuEnableManualReload, true);
            Menu.SetChecked(menuDispensableManualReload, false);

            PlayerPrefs.SetInt(kManualReloadDomain, 1);
            //编辑器设置 project setting->editor->enterPlayModeSetting
#if UNITY_2019_1_OR_NEWER
            EditorSettings.enterPlayModeOptionsEnabled = true;
            EditorSettings.enterPlayModeOptions        = EnterPlayModeOptions.DisableDomainReload;
#endif
            LockReloadDomain();
        }

        [MenuItem(menuDispensableManualReload)]
        private static void DispensableManualReloadDomain()
        {
            Debug.Log("<color=cyan>关闭手动 Reload Domain</color>");

            Menu.SetChecked(MenuEnableManualReload, false);
            Menu.SetChecked(menuDispensableManualReload, true);

            PlayerPrefs.SetInt(kManualReloadDomain, 0);
            UnlockReloadDomain();
#if UNITY_2019_1_OR_NEWER
            EditorSettings.enterPlayModeOptionsEnabled = false;
#endif
        }

        /// <summary>
        /// 手动刷新
        /// </summary>
        [MenuItem(MenuReloadDomain)]
        private static void ManualReload()
        {
            if (!_IsManualReload) return;
            UnlockReloadDomain();
#if UNITY_2019_1_OR_NEWER
            EditorUtility.RequestScriptReload();
#endif
        }
    }
}