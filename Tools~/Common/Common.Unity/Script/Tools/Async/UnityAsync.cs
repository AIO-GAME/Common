using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Unity线程
/// </summary>
public static partial class UnityAsync
{
    private static ThreadMono instance;

    private static bool IsWebGL;

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

        IsWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
    }

    /// <summary>
    /// 开一个新的作业执行函数
    /// </summary>
    public static void RunTask(Action action)
    {
        if (IsWebGL) ExecuteInUpdate(action);
        else Task.Factory.StartNew(action.Invoke);
    }

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