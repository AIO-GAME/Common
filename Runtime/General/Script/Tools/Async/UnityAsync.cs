#if SUPPORTE_UNITASK
using Cysharp.Threading.Tasks;
#endif

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

/// <summary>
/// Unity线程
/// </summary>
[Preserve]
public static partial class UnityAsync
{
    private static ThreadMono instance;

    /// <summary>
    /// 是否允许线程
    /// </summary>
    private static bool IsAllowThread;

    /// <summary>
    /// 主线程执行
    /// </summary>
    public static MonoBehaviour MainThread => instance;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="visible">是否可见</param>
    public static void Initialize(bool visible = false)
    {
        if (instance != null) return;
        if (Application.isPlaying)
        {
            //添加一个看不见的游戏物体到场景中
            var obj = new GameObject("MainThreadExecute");
            if (!visible) obj.hideFlags = HideFlags.HideAndDontSave;
            instance = obj.AddComponent<ThreadMono>();
        }

        IsAllowThread = Application.platform == RuntimePlatform.WebGLPlayer;
    }

    public static void Dispose()
    {
    }

    /// <summary>
    /// 开一个新的作业执行函数
    /// </summary>
    public static void RunTask(Action action)
    {
        if (IsAllowThread) ExecuteInUpdate(action);
        else
#if SUPPORTE_UNITASK
            UniTask.RunOnThreadPool(action);
#else
            Task.Factory.StartNew(action.Invoke);
#endif
    }

    [Preserve]
    internal partial class ThreadMono : MonoBehaviour
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void OnDisable()
        {
            if (instance == this) instance = null;
        }
    }
}