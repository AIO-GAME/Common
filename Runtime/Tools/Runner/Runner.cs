﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using UObject = UnityEngine.Object;

namespace AIO
{
    /// <summary>
    /// Unity线程
    /// </summary>
    [Preserve]
    public static partial class Runner
    {
        private static ThreadMono instance;

        /// <summary>
        /// 是否允许线程
        /// </summary>
        public static bool IsAllowThread { get; private set; }

        static Runner()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Initialize()
        {
            if (instance != null) return;
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
#endif
                var obj = new GameObject("RunnerMainThreadExecuteRuntime")
                {
                    hideFlags = HideFlags.HideAndDontSave
                }; //添加一个看不见的游戏物体到场景中
                instance = obj.AddComponent<ThreadMono>();
#if UNITY_EDITOR
            }
            else
            {
                var obj = new GameObject("RunnerMainThreadExecuteEditor")
                {
                    hideFlags = HideFlags.HideAndDontSave
                };
                instance = obj.AddComponent<ThreadMono>();
            }

            IsAllowThread = true;
            EditorApplication.quitting += Dispose;
#else
            IsAllowThread = Application.platform != RuntimePlatform.WebGLPlayer;
#endif
            Application.quitting += Dispose;
        }

        private static void Dispose()
        {
#if UNITY_EDITOR
            EditorApplication.quitting -= Dispose;
#endif
            Application.quitting -= Dispose;
            UObject.DestroyImmediate(instance.gameObject, true);
        }

        [Preserve]
        private partial class ThreadMono : MonoBehaviour
        {
            private List<Delegate> mActionCopiedQueueLateUpdateFunc { get; set; } // LateUpdate
            private List<Delegate> mActionCopiedQueueFixedUpdateFunc { get; set; } // FixedUpdate
            private List<Delegate> mActionCopiedQueueUpdateFunc { get; set; } // QueueUpdate

            public void Awake()
            {
                mActionCopiedQueueLateUpdateFunc = Pool.List<Delegate>();
                mActionCopiedQueueFixedUpdateFunc = Pool.List<Delegate>();
                mActionCopiedQueueUpdateFunc = Pool.List<Delegate>();
                DontDestroyOnLoad(gameObject);
            }

            private void OnDestroy()
            {
                instance = null;

                for (var index = 0; index < mActionCopiedQueueUpdateFunc.Count; index++)
                {
                    mActionCopiedQueueUpdateFunc[index] = null;
                }

                for (var index = 0; index < mActionCopiedQueueFixedUpdateFunc.Count; index++)
                {
                    mActionCopiedQueueFixedUpdateFunc[index] = null;
                }

                for (var index = 0; index < mActionCopiedQueueLateUpdateFunc.Count; index++)
                {
                    mActionCopiedQueueLateUpdateFunc[index] = null;
                }

                mActionCopiedQueueLateUpdateFunc.Free();
                mActionCopiedQueueFixedUpdateFunc.Free();
                mActionCopiedQueueUpdateFunc.Free();
            }
        }
    }
}