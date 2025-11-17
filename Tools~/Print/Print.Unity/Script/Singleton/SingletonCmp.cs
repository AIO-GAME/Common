using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AIO;
using AIO.Internal;
using UnityEngine;

/// <summary>
/// 单例基类
/// </summary>
[ExecuteAlways]
[DisallowMultipleComponent]
[AddComponentMenu("单例管理器")]
public class SingletonBehaviour : MonoBehaviour
{
    /// <summary>
    /// 单例类型名称列表
    /// </summary>
    public Dictionary<Type, Singleton> Data => SingletonData.Data;

    /// <summary>
    /// 单例数组
    /// </summary>
    public ConcurrentQueue<SingletonArray> Array => SingletonData.Array;

    internal event Action OnUpdate;
    internal event Action OnFixedUpdate;
    internal event Action OnLateUpdate;

    /// <summary>
    /// 启用日志记录
    /// </summary>
    public static bool EnableLog;

    private void Update() { OnUpdate?.Invoke(); }

    private void FixedUpdate() { OnFixedUpdate?.Invoke(); }

    private void LateUpdate() { OnLateUpdate?.Invoke(); }

    private void OnApplicationQuit()
    {
        MonoSingleton.ShuttingDown = true;
        OnUpdate                   = null;
        OnFixedUpdate              = null;
        OnLateUpdate               = null;
        Destroy(MonoSingleton.LazyObj.Value.gameObject);
        Singleton.OnFullName = null;
    }
}