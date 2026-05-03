using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AIO.Internal
{
    /// <summary>
    /// 从这个基类继承以创建单例。
    /// </summary>
    [Preserve]
    public abstract class MonoSingleton : Singleton
    {
        /// <summary>
        /// 日志记录器。
        /// </summary>
        protected internal LOG Log => LazyLog.Value;

        private Lazy<LOG> LazyLog = new Lazy<LOG>(CreateLOG, true);

        private static LOG CreateLOG() => new LOG(string.Empty, true);

        /// <summary>
        /// 启用或禁用日志记录。
        /// </summary>
        public bool LogEnabled
        {
            get => Log.Enabled;
            set => Log.Enabled = value;
        }

        #region SetLogInfo

        private string Color = "#00ffff";

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 设置日志信息
        /// </summary>
        /// <param name="title">标题</param>
        protected void SetLogInfo(string title)
        {
            if (!string.IsNullOrEmpty(title)) Title = title;
            Log.TAG = $"<color={Color}><b>【{Title}】</b></color> ";
        }

        /// <summary>
        /// 设置日志信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="color">颜色</param>
        protected void SetLogInfo(string title, string color)
        {
            if (!string.IsNullOrEmpty(title)) Title = title;
            if (!string.IsNullOrEmpty(color)) Color = color;
            Log.TAG = $"<color={Color}><b>【{Title}】</b></color> ";
        }

        /// <summary>
        /// 设置日志信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="color">颜色</param>
        protected void SetLogInfo(string title, Color color)
        {
            if (!string.IsNullOrEmpty(title)) Title = title;
            Color   = $"#{ColorUtility.ToHtmlStringRGB(color)}";
            Log.TAG = $"<color={Color}><b>【{Title}】</b></color> ";
        }

        /// <summary>
        /// 设置日志信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="color">颜色</param>
        protected void SetLogInfo(string title, Color32 color)
        {
            if (!string.IsNullOrEmpty(title)) Title = title;
            Color   = $"#{ColorUtility.ToHtmlStringRGB(color)}";
            Log.TAG = $"<color={Color}><b>【{Title}】</b></color> ";
        }

        #endregion

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void EditorInit()
        {
            if (Application.isPlaying)
            {
                Object.DontDestroyOnLoad(LazyObj.Value);
                Application.quitting += __Quitting;
            }
            else
            {
                Object.Destroy(LazyObj.Value.gameObject);
            }
        }

        private static void __Quitting()
        {
            ShuttingDown         =  true;
            Application.quitting -= __Quitting;
            var list = SingletonData.Data.Values.Where(singleton => singleton.AutoDispose).ToList();
            list.Sort((a, b) => a.AutoDisposePriority.CompareTo(b.AutoDisposePriority));
            lock (@lock)
            {
                for (var index = list.Count - 1; index >= 0; index--)
                {
                    if (SingletonBehaviour.EnableLog) Debug.Log($"【Singleton】Dispose {list[index].RealType.FullName} ...");
                    try
                    {
                        SingletonData.Data.Remove(list[index].RealType);
                        list[index].Dispose();
                        list[index] = null;
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }

            SingletonData.Data.Clear();
            Pool.DisposeAll();
            ShuttingDown = false;
        }

        internal static readonly Lazy<SingletonBehaviour> LazyObj = new Lazy<SingletonBehaviour>(() =>
        {
            var singletons                     = GameObject.Find("【Singletons】");
            if (singletons == null) singletons = new GameObject("【Singletons】");
            singletons.isStatic  = true;
            singletons.hideFlags = HideFlags.DontSave;
            var cmp              = singletons.GetComponent<SingletonBehaviour>();
            if (cmp == null) cmp = singletons.AddComponent<SingletonBehaviour>();
            singletons.gameObject.SetActive(true);
            return cmp;
        });

        /// <summary>
        /// 检查我们是否即将被销毁。
        /// </summary>
        internal static bool ShuttingDown;

        /// <summary>
        /// 销毁单例。
        /// </summary>
        [DebuggerStepThrough, DebuggerHidden]
        internal override void Dispose()
        {
            if (!IsInitialized) return;
            OnDispose();

            if (LazyLog.IsValueCreated) LazyLog.Value?.Dispose();
            LazyLog       = null;
            IsInitialized = false;
        }

        #region 日志

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        protected void LogI(string msg) => Log.I(msg);

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        /// <param name="args"> 参数 </param>
        protected void LogI(string msg, params object[] args) => Log.I(msg, args);

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        protected void LogW(string msg) => Log.W(msg);

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        /// <param name="args"> 参数 </param>
        protected void LogW(string msg, params object[] args) => Log.W(msg, args);

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        protected void LogE(string msg) => Log.E(msg);

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        /// <param name="args"> 参数 </param>
        protected void LogE(string msg, params object[] args) => Log.E(msg, args);

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        protected void LogE(Exception msg) => Log.E(msg);

        #endregion

        #region Mono

        /// <summary>
        /// 单例根节点。
        /// </summary>
        public static GameObject gameObject => LazyObj.Value.gameObject;

        /// <summary>
        /// 单例根节点的Transform。
        /// </summary>
        public static Transform transform => LazyObj.Value.transform;

        /// <summary>
        /// 为单例添加组件。
        /// </summary>
        /// <typeparam name="T"> 组件类型。</typeparam>
        protected static T AddComponent<T>()
        where T : Behaviour => LazyObj.Value.gameObject.AddComponent<T>();

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"> 原对象 </param>
        /// <typeparam name="T"> 对象类型 </typeparam>
        /// <returns> 新对象 </returns>
        protected static T Instantiate<T>(T original)
        where T : Object => Object.Instantiate(original);

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"> 原对象 </param>
        /// <typeparam name="T"> 对象类型 </typeparam>
        /// <param name="trans"> 变换组件 </param>
        /// <returns> 新对象 </returns>
        protected static T Instantiate<T>(T original, Transform trans)
        where T : Object => Object.Instantiate(original, trans);

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"> 原对象 </param>
        /// <typeparam name="T"> 对象类型 </typeparam>
        /// <param name="trans"> 变换组件 </param>
        /// <param name="worldPositionStays"> 是否保持世界位置 </param>
        /// <returns> 新对象 </returns>
        protected static T Instantiate<T>(T original, Transform trans, bool worldPositionStays)
        where T : Object => Object.Instantiate(original, trans, worldPositionStays);

        /// <summary>
        /// 场景切换时不销毁对象
        /// </summary>
        /// <param name="target"> 目标对象 </param>
        protected static void DontDestroyOnLoad(Object target) { Object.DontDestroyOnLoad(target); }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="target"> 目标对象 </param>
        protected static void Destroy(Object target) { Object.Destroy(target); }

        /// <summary>
        /// 启动协程
        /// </summary>
        /// <param name="routine"></param>
        protected static void StartCoroutine(IEnumerator routine)
        {
            if (ShuttingDown) return;
            LazyObj.Value.StartCoroutine(routine);
        }

        /// <summary>
        /// 停止协程
        /// </summary>
        /// <param name="routine"></param>
        protected static void StopCoroutine(IEnumerator routine)
        {
            if (ShuttingDown) return;
            LazyObj.Value.StopCoroutine(routine);
        }

        #endregion

        #region event

        /// <summary>
        /// 添加更新事件
        /// </summary>
        public static event Action OnUpdate
        {
            add => LazyObj.Value.OnUpdate += value;
            remove => LazyObj.Value.OnUpdate -= value;
        }

        /// <summary>
        /// 添加更新事件
        /// </summary>
        public static event Action OnLateUpdate
        {
            add => LazyObj.Value.OnLateUpdate += value;
            remove => LazyObj.Value.OnLateUpdate -= value;
        }

        /// <summary>
        /// 添加更新事件
        /// </summary>
        public static event Action OnFixedUpdate
        {
            add => LazyObj.Value.OnFixedUpdate += value;
            remove => LazyObj.Value.OnFixedUpdate -= value;
        }

        #endregion
    }
}