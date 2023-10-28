﻿using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIO.UEditor
{
    /// <summary>
    /// ____DESC:   手动reload domain 工具
    /// </summary>
    public class ScriptCompileReloadTools
    {
        /* 说明
         * 关于域重载 https://docs.unity.cn/cn/2021.3/Manual/DomainReloading.html
         * EditorApplication.LockReloadAssemblies()和 EditorApplication.UnlockReloadAssemblies() 最好成对
         * 如果不小心LockReloadAssemblies3次 但是只UnlockReloadAssemblies了一次 那么还是不会重载 必须也要但是只UnlockReloadAssemblies3次
         */

        const string menuEnableManualReload = "AIO/Tools/Script/Manual Reload Domain Open";
        const string menuDisenableManualReload = "AIO/Tools/Script/Manual Reload Domain Close";
        const string menuRealodDomain = "AIO/Tools/Script/Unlock Reload";

        const string kManualReloadDomain = "ManualReloadDomain";
        const string kFirstEnterUnity = "FirstEnterUnity"; //是否首次进入unity
        const string kReloadDomainTimer = "ReloadDomainTimer"; //计时


        /**************************************************/
        /// <summary>
        /// 编译时间
        /// </summary>
        static Stopwatch compileSW = new Stopwatch();

        /// <summary>
        /// 是否手动reload
        /// </summary>
        static bool IsManualReload => PlayerPrefs.GetInt(kManualReloadDomain, -1) == 1;

        //缓存数据 域重载之后数据会变成false 如果不是false 那么就要重载
        static bool tempData = false;

        //https://github.com/INeatFreak/unity-background-recompiler 来自这个库 反射获取是否锁住
        static MethodInfo CanReloadAssembliesMethod;

        static bool IsLocked
        {
            get
            {
                if (CanReloadAssembliesMethod == null)
                {
                    // source: https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/EditorApplication.bindings.cs#L154
                    CanReloadAssembliesMethod = typeof(EditorApplication).GetMethod("CanReloadAssemblies",
                        BindingFlags.NonPublic | BindingFlags.Static);
                    if (CanReloadAssembliesMethod == null)
                        Debug.LogError("Can't find CanReloadAssemblies method. It might have been renamed or removed.");
                }

                return !(bool)CanReloadAssembliesMethod.Invoke(null, null);
            }
        }
        /**************************************************/


        [InitializeOnLoadMethod]
        static void InitCompile()
        {
            //**************不需要这个可以注释********************************
#if UNITY_2019_1_OR_NEWER
            CompilationPipeline.compilationStarted -= OnCompilationStarted;
            CompilationPipeline.compilationStarted += OnCompilationStarted;
            CompilationPipeline.compilationFinished -= OnCompilationFinished;
            CompilationPipeline.compilationFinished += OnCompilationFinished;
#endif
            //**************************************************************

            //域重载事件监听
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;

            EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;


            //Bug 首次启动的时候 并不会马上设置
            //if (PlayerPrefs.HasKey(kManualReloadDomain))
            //{
            //    Menu.SetChecked(menuEnableManualReload, IsManualReload ? true : false);
            //    Menu.SetChecked(menuDisenableManualReload, IsManualReload ? false : true);
            //}
            FirstCheckAsync();
        }

        //首次打开检测
        async static void FirstCheckAsync()
        {
            await Task.Delay(100);
            //判断是否首次打开
            //https://docs.unity.cn/cn/2021.3/ScriptReference/SessionState.html
            if (SessionState.GetBool(kFirstEnterUnity, true))
            {
                SessionState.SetBool(kFirstEnterUnity, false);
                Menu.SetChecked(menuEnableManualReload, IsManualReload ? true : false);
                Menu.SetChecked(menuDisenableManualReload, IsManualReload ? false : true);

                if (IsManualReload)
                {
                    UnlockReloadDomain();
                    LockRealodDomain();
                }

                Debug.Log($"<color=lime>当前ReloadDomain状态,是否手动: {IsManualReload}</color>");
            }
        }


        //运行模式改变
        private static void EditorApplication_playModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredEditMode:
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    if (tempData)
                    {
                        UnlockReloadDomain();
#if UNITY_2019_1_OR_NEWER
                        EditorUtility.RequestScriptReload();
#endif
                    }

                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    tempData = true;
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }

        //当开始编译脚本
        private static void OnCompilationStarted(object obj)
        {
            if (IsManualReload)
            {
                compileSW.Start();
                Debug.Log("<color=yellow>Begin Compile</color>");
            }
        }

        //结束编译
        private static void OnCompilationFinished(object obj)
        {
            if (IsManualReload)
            {
                compileSW.Stop();
                Debug.Log($"<color=yellow>End Compile 耗时:{compileSW.ElapsedMilliseconds} ms</color>");
                compileSW.Reset();
            }
        }

        //开始reload domain
        private static void OnBeforeAssemblyReload()
        {
            if (IsManualReload)
            {
                Debug.Log("<color=yellow>Begin Reload Domain ......</color>");
                //记录时间
                SessionState.SetInt(kReloadDomainTimer, (int)(EditorApplication.timeSinceStartup * 1000));
            }
        }

        //结束reload domain
        private static void OnAfterAssemblyReload()
        {
            if (IsManualReload)
            {
                var timeMS = (int)(EditorApplication.timeSinceStartup * 1000) -
                             SessionState.GetInt(kReloadDomainTimer, 0);
                Debug.Log($"<color=yellow>End Reload Domain 耗时:{timeMS} ms</color>");
                LockRealodDomain();
            }
        }

        static void LockRealodDomain()
        {
            //如果没有锁住 锁住
            if (!IsLocked)
            {
                EditorApplication.LockReloadAssemblies();
            }
        }

        static void UnlockReloadDomain()
        {
            //如果锁住了 打开
            if (IsLocked)
            {
                EditorApplication.UnlockReloadAssemblies();
            }
        }

        [MenuItem(menuEnableManualReload)]
        static void EnableManualReloadDomain()
        {
            Debug.Log("<color=cyan>开启手动 Reload Domain</color>");

            Menu.SetChecked(menuEnableManualReload, true);
            Menu.SetChecked(menuDisenableManualReload, false);

            PlayerPrefs.SetInt(kManualReloadDomain, 1);
            //编辑器设置 projectsetting->editor->enterPlayModeSetting
#if UNITY_2019_1_OR_NEWER
            EditorSettings.enterPlayModeOptionsEnabled = true;
            EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;
#endif
            LockRealodDomain();
        }

        [MenuItem(menuDisenableManualReload)]
        static void DisenableManualReloadDomain()
        {
            Debug.Log("<color=cyan>关闭手动 Reload Domain</color>");

            Menu.SetChecked(menuEnableManualReload, false);
            Menu.SetChecked(menuDisenableManualReload, true);

            PlayerPrefs.SetInt(kManualReloadDomain, 0);
            UnlockReloadDomain();
#if UNITY_2019_1_OR_NEWER
            EditorSettings.enterPlayModeOptionsEnabled = false;
#endif
        }

        //手动刷新
        [MenuItem(menuRealodDomain)]
        static void ManualReload()
        {
            if (IsManualReload)
            {
                UnlockReloadDomain();
#if UNITY_2019_1_OR_NEWER
                EditorUtility.RequestScriptReload();
#endif
            }
        }
    }
}