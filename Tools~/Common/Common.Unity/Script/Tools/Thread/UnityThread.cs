#define ENABLE_UPDATE_FUNCTION_CALLBACK
#define ENABLE_LATEUPDATE_FUNCTION_CALLBACK
#define ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Unity线程
/// </summary>
public partial class UnityThread : MonoBehaviour
{
    private static UnityThread instance;

    ////////////////////////////////////////////////UPDATE IMPL////////////////////////////////////////////////////////

    /// <summary>
    /// Update等待队列
    /// </summary>
    private static readonly List<Action> actionQueuesUpdateFunc = new List<Action>();

    /// <summary>
    /// Update执行队列
    /// </summary>
    private readonly List<Action> mAactionCopiedQueueUpdateFunc = new List<Action>();

    /// <summary>
    /// 执行状态
    /// </summary>
    private static volatile bool noActionQueueToExecuteUpdateFunc = true;


    ////////////////////////////////////////////////LATEUPDATE IMPL////////////////////////////////////////////////////////
    private static readonly List<Action> actionQueuesLateUpdateFunc = new List<Action>();
    private readonly List<Action> mActionCopiedQueueLateUpdateFunc = new List<Action>();
    private static volatile bool noActionQueueToExecuteLateUpdateFunc = true;


    ////////////////////////////////////////////////FIXEDUPDATE IMPL////////////////////////////////////////////////////////
    private static readonly List<Action> actionQueuesFixedUpdateFunc = new List<Action>();
    private readonly List<Action> mActionCopiedQueueFixedUpdateFunc = new List<Action>();
    private static volatile bool noActionQueueToExecuteFixedUpdateFunc = true;

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
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<UnityThread>();
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //////////////////////////////////////////////COROUTINE IMPL//////////////////////////////////////////////////////
#if (ENABLE_UPDATE_FUNCTION_CALLBACK)
    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteCoroutine(IEnumerator action)
    {
        if (instance != null) ExecuteInUpdate(() => instance.StartCoroutine(action));
    }

    ////////////////////////////////////////////UPDATE IMPL////////////////////////////////////////////////////
    /// <summary>
    /// 在Update中执行
    /// </summary>
    public static void ExecuteInUpdate(Action action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (actionQueuesUpdateFunc)
        {
            actionQueuesUpdateFunc.Add(action);
            noActionQueueToExecuteUpdateFunc = false;
        }
    }

    /// <summary>
    /// 在Update中执行
    /// </summary>
    public static void ExecuteInUpdate(params Action[] action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (actionQueuesUpdateFunc)
        {
            actionQueuesUpdateFunc.AddRange(action);
            noActionQueueToExecuteUpdateFunc = false;
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        //判断当前是否有操作正在执行
        if (noActionQueueToExecuteUpdateFunc) return;

        //清空队列中 残留的操作函数
        mAactionCopiedQueueUpdateFunc.Clear();
        lock (actionQueuesUpdateFunc)
        {
            //将等待队列中的操作 复制到执行队列中
            mAactionCopiedQueueUpdateFunc.AddRange(actionQueuesUpdateFunc);
            actionQueuesUpdateFunc.Clear();
            //并开启执行状态
            noActionQueueToExecuteUpdateFunc = true;
        }

        //实现执行队列
        for (var i = 0; i < mAactionCopiedQueueUpdateFunc.Count; i++) mAactionCopiedQueueUpdateFunc[i].Invoke();
    }
#endif

    ////////////////////////////////////////////LATEUPDATE IMPL////////////////////////////////////////////////////
#if (ENABLE_LATEUPDATE_FUNCTION_CALLBACK)
    /// <summary>
    /// 在LateUpdate更新
    /// </summary>
    public static void ExecuteInLateUpdate(Action action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (actionQueuesLateUpdateFunc)
        {
            actionQueuesLateUpdateFunc.Add(action);
            noActionQueueToExecuteLateUpdateFunc = false;
        }
    }

    /// <summary>
    /// 在LateUpdate更新
    /// </summary>
    public void LateUpdate()
    {
        if (noActionQueueToExecuteLateUpdateFunc) return;

        mActionCopiedQueueLateUpdateFunc.Clear();
        lock (actionQueuesLateUpdateFunc)
        {
            mActionCopiedQueueLateUpdateFunc.AddRange(actionQueuesLateUpdateFunc);
            actionQueuesLateUpdateFunc.Clear();
            noActionQueueToExecuteLateUpdateFunc = true;
        }

        for (var i = 0; i < mActionCopiedQueueLateUpdateFunc.Count; i++) mActionCopiedQueueLateUpdateFunc[i].Invoke();
    }
#endif

    ////////////////////////////////////////////FIXEDUPDATE IMPL//////////////////////////////////////////////////
#if (ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK)

    /// <summary>
    /// 在FixedUpdate更新
    /// </summary>
    public static void ExecuteInFixedUpdate(Action action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (actionQueuesFixedUpdateFunc)
        {
            actionQueuesFixedUpdateFunc.Add(action);
            noActionQueueToExecuteFixedUpdateFunc = false;
        }
    }

    /// <summary>
    /// 在FixedUpdate更新
    /// </summary>
    public void FixedUpdate()
    {
        if (noActionQueueToExecuteFixedUpdateFunc) return;

        mActionCopiedQueueFixedUpdateFunc.Clear();
        lock (actionQueuesFixedUpdateFunc)
        {
            mActionCopiedQueueFixedUpdateFunc.AddRange(actionQueuesFixedUpdateFunc);
            actionQueuesFixedUpdateFunc.Clear();
            noActionQueueToExecuteFixedUpdateFunc = true;
        }

        for (var i = 0; i < mActionCopiedQueueFixedUpdateFunc.Count; i++) mActionCopiedQueueFixedUpdateFunc[i].Invoke();
    }
#endif

    /// <summary>
    /// 开一个新的作业执行函数
    /// </summary>
    public static void Job(Action action)
    {
        Task.Factory.StartNew(action.Invoke);
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void OnDisable()
    {
        if (instance == this) instance = null;
    }
}