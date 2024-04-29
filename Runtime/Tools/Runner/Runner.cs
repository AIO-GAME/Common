#region

using System;
using System.Collections.Generic;
using AIO.UEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using UObject = UnityEngine.Object;

#endregion

namespace AIO
{
    /// <summary>
    /// Unity线程
    /// </summary>
    [Preserve]
    public static partial class Runner
    {
        private static ThreadMono instance;

#if UNITY_EDITOR
        /// <summary>
        /// [EDITOR] 是否运行时 
        /// </summary>
        public static bool IsRuntime { get; private set; }
#endif

        /// <summary>
        /// 是否允许线程
        /// </summary>
        public static bool IsAllowThread { get; private set; }

#if UNITY_EDITOR
        [AInit(EInitAttrMode.Editor | EInitAttrMode.RuntimeBeforeSplashScreen, int.MaxValue)]
        private static void Initialize()
        {
            IsAllowThread = true;
            try
            {
                IsRuntime = EditorApplication.isPlaying;
            }
            catch (Exception)
            {
                IsRuntime = false;
            }

            if (IsRuntime)
            {
                var obj = new GameObject("RunnerMainThreadExecuteRuntime")
                {
                    hideFlags = HideFlags.HideAndDontSave
                }; //添加一个看不见的游戏物体到场景中
                instance = obj.AddComponent<ThreadMono>();
            }
            else
            {
                var obj = new GameObject("RunnerMainThreadExecuteEditor")
                {
                    hideFlags = HideFlags.HideAndDontSave
                };
                instance = obj.AddComponent<ThreadMono>();
            }

            EditorApplication.quitting += Dispose;
            Application.quitting       += Dispose;
        }
#endif

#if !UNITY_EDITOR
        static Runner()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (instance != null) return;
            IsAllowThread = Application.platform != RuntimePlatform.WebGLPlayer;
            var obj = new GameObject("RunnerMainThreadExecuteRuntime") //添加一个看不见的游戏物体到场景中
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            instance = obj.AddComponent<ThreadMono>();
            Application.quitting += Dispose;
        }
#endif

        private static void Dispose()
        {
#if UNITY_EDITOR
            EditorApplication.quitting -= Dispose;
#endif
            Application.quitting -= Dispose;
            if (instance != null) return;
            UObject.DestroyImmediate(instance.gameObject, true);
            instance = null;
        }

        [Preserve]
        private partial class ThreadMono : MonoBehaviour
        {
            private List<Delegate> mActionCopiedQueueLateUpdateFunc  { get; set; } // LateUpdate
            private List<Delegate> mActionCopiedQueueFixedUpdateFunc { get; set; } // FixedUpdate
            private List<Delegate> mActionCopiedQueueUpdateFunc      { get; set; } // QueueUpdate

            public void Awake()
            {
                mActionCopiedQueueLateUpdateFunc  = Pool.List<Delegate>();
                mActionCopiedQueueFixedUpdateFunc = Pool.List<Delegate>();
                mActionCopiedQueueUpdateFunc      = Pool.List<Delegate>();
                DontDestroyOnLoad(gameObject);
            }

            private void OnDestroy()
            {
                instance = null;

                for (var index = 0; index < mActionCopiedQueueUpdateFunc.Count; index++) mActionCopiedQueueUpdateFunc[index] = null;

                for (var index = 0; index < mActionCopiedQueueFixedUpdateFunc.Count; index++) mActionCopiedQueueFixedUpdateFunc[index] = null;

                for (var index = 0; index < mActionCopiedQueueLateUpdateFunc.Count; index++) mActionCopiedQueueLateUpdateFunc[index] = null;

                mActionCopiedQueueLateUpdateFunc.Free();
                mActionCopiedQueueFixedUpdateFunc.Free();
                mActionCopiedQueueUpdateFunc.Free();
            }
        }
    }
}