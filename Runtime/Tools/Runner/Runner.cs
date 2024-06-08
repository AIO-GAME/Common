#region namespace

using System;
using System.Collections.Concurrent;
using AIO.UEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using UObject = UnityEngine.Object;
#if SUPPORT_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;
#endif

#endregion

namespace AIO
{
    /// <summary>
    /// Unity线程
    /// </summary>
    /// <c>开启协程</c>
    /// <code>
    /// 1: Runner.StartCoroutine(T);
    /// 2: Runner.StartCoroutine(T());
    /// 3: Runner.StartCoroutine(T, T1, T2);
    /// 4: Runner.StartCoroutine(T(), T1(), T2());
    /// 5: Runner.StartCoroutine(mono, T1, T2);
    /// 6: Runner.StartCoroutine(mono, T1(), T2());
    /// IEnumerator Test() => WaitForEndOfFrame;
    /// </code>
    /// <c>关闭协程</c>
    /// 1: Runner.StopCoroutine(Test);
    /// 2: Runner.StopCoroutine(mono);
    /// 3: Runner.StopCoroutine(mono, Test);
    /// 4: Runner.StopCoroutine(Test());
    /// <c>开启线程</c>
    /// <code>
    /// Runner.StartTask(() => { });
    /// </code>
    [Preserve]
    public static partial class Runner
    {
#if UNITY_EDITOR
        /// <summary>
        /// [EDITOR] 是否运行时
        /// </summary>
        private static bool IsRuntime { get; set; }

        private static GameObject RunnerMainEditor;

        [InitializeOnLoadMethod]
        private static void EditorInitialize()
        {
            EditorApplication.playModeStateChanged -= EditorPlayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorPlayModeStateChanged;
        }

        [AInit(EInitAttrMode.Editor)]
        private static void InitializeEditor() { EditorPlayModeStateChanged(PlayModeStateChange.EnteredEditMode); }

        [AInit(EInitAttrMode.RuntimeAfterAssembliesLoaded)]
        private static void InitializeRuntime() { EditorPlayModeStateChanged(PlayModeStateChange.EnteredPlayMode); }

        private static void EditorPlayModeStateChanged(PlayModeStateChange editor)
        {
            IsRuntime     = editor == PlayModeStateChange.EnteredPlayMode;
            IsAllowThread = true;
            if (IsRuntime)
            {
                DataInitialize();
                if (!RunnerMainRuntime)
                {
                    RunnerMainRuntime = new GameObject()
                    {
                        hideFlags = HideFlags.HideAndDontSave,
                        name      = nameof(RunnerMainRuntime)
                    };
                }

                if (!RunnerMainRuntime.TryGetComponent(out instance))
                    instance = RunnerMainRuntime.AddComponent<ThreadMono>();
            }
            else
            {
                if (!RunnerMainEditor)
                {
                    RunnerMainEditor = new GameObject()
                    {
                        hideFlags = HideFlags.HideAndDontSave,
                        name      = nameof(RunnerMainEditor)
                    };
                }

                if (!RunnerMainEditor.TryGetComponent(out instance))
                    instance = RunnerMainEditor.AddComponent<ThreadMono>();
            }

            if (!instance) throw new Exception("Runner Main Thread Execute instance is null");
#if SUPPORT_UNITASK
            GetCancellationTokenOnDestroy = instance.GetCancellationTokenOnDestroy();
#endif
            EditorApplication.quitting += Dispose;
            Application.quitting       += Dispose;
        }
#endif
#if SUPPORT_UNITASK
        private static CancellationToken GetCancellationTokenOnDestroy;
#endif
#if !UNITY_EDITOR
        static Runner()
        {
            DataInitialize();
            IsAllowThread = Application.platform != RuntimePlatform.WebGLPlayer;
            RunnerMainRuntime = new GameObject() //添加一个看不见的游戏物体到场景中
            {
                hideFlags = HideFlags.HideAndDontSave,
                name = nameof(RunnerMainRuntime)
            };
            instance = RunnerMainRuntime.AddComponent<ThreadMono>();
#if SUPPORT_UNITASK
            GetCancellationTokenOnDestroy = instance.GetCancellationTokenOnDestroy();
#endif
            Application.quitting += Dispose;
        }
#endif

        private static void Dispose()
        {
#if UNITY_EDITOR
            IsRuntime                  =  false;
            EditorApplication.quitting -= Dispose;
#endif
            Application.quitting -= Dispose;
            if (instance) return;
            UObject.DestroyImmediate(instance.gameObject, true);
            instance = null;
        }

        private static void DataInitialize()
        {
            if (QueuesDelegateUpdate == null) QueuesDelegateUpdate           = new ConcurrentQueue<Delegate>();
            if (QueuesDelegateUpdateFixed == null) QueuesDelegateUpdateFixed = new ConcurrentQueue<Delegate>();
            if (QueuesDelegateUpdateLate == null) QueuesDelegateUpdateLate   = new ConcurrentQueue<Delegate>();
        }

        /// <summary>
        /// 是否允许线程
        /// </summary>
        public static bool IsAllowThread { get; private set; }

        [ContextStatic] private static GameObject                RunnerMainRuntime;
        [ContextStatic] private static ThreadMono                instance;
        [ContextStatic] private static ConcurrentQueue<Delegate> QueuesDelegateUpdateFixed;
        [ContextStatic] private static ConcurrentQueue<Delegate> QueuesDelegateUpdate;
        [ContextStatic] private static ConcurrentQueue<Delegate> QueuesDelegateUpdateLate;

        [ContextStatic] private static volatile bool QueuesDelegateUpdateFixedState = true;
        [ContextStatic] private static volatile bool QueuesDelegateUpdateState      = true;
        [ContextStatic] private static volatile bool QueuesDelegateUpdateLateState  = true;

        [Preserve]
        private partial class ThreadMono : MonoBehaviour
        {
            private ConcurrentQueue<Delegate> QueueCopiedUpdateLate;  // LateUpdate
            private ConcurrentQueue<Delegate> QueueCopiedUpdateFixed; // FixedUpdate
            private ConcurrentQueue<Delegate> QueueCopiedUpdate;      // QueueUpdate

            public void Awake()
            {
                QueueCopiedUpdateLate  = new ConcurrentQueue<Delegate>();
                QueueCopiedUpdateFixed = new ConcurrentQueue<Delegate>();
                QueueCopiedUpdate      = new ConcurrentQueue<Delegate>();
                DontDestroyOnLoad(gameObject);
            }

            private void OnDestroy()
            {
                instance = null;

                lock (QueueCopiedUpdateLate) // 释放 mActionCopiedQueueLateUpdateFunc
                {
                    while (QueueCopiedUpdateLate.Count > 0)
                        QueueCopiedUpdateLate.TryDequeue(out _);
                    QueueCopiedUpdateLate = null;
                }

                lock (QueueCopiedUpdateFixed)
                {
                    while (QueueCopiedUpdateFixed.Count > 0)
                        QueueCopiedUpdateFixed.TryDequeue(out _);
                    QueueCopiedUpdateFixed = null;
                }

                lock (QueueCopiedUpdate)
                {
                    while (QueueCopiedUpdate.Count > 0)
                        QueueCopiedUpdate.TryDequeue(out _);
                    QueueCopiedUpdate = null;
                }
            }
        }
    }
}