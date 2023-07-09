/*|==========|*|
|*|Author:   |*| -> SAM
|*|Date:     |*| -> 2023-06-11
|*|==========|*/

#define ENABLE_UPDATE_FUNCTION_CALLBACK

#if SUPPORTE_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_UPDATE_FUNCTION_CALLBACK)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UnityAsync
{
    /// <summary>
    /// Update等待队列
    /// </summary>
    [ContextStatic] private static readonly List<Delegate> actionQueuesUpdateFunc = new List<Delegate>();

    /// <summary>
    /// 执行状态
    /// </summary>
    [ContextStatic] private static volatile bool noActionQueueToExecuteUpdateFunc = true;

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteInUpdate<T>(T coroutine) where T : IEnumerator
    {
        if (instance == null) return;
        ExecuteInUpdate(() => instance.StartCoroutine(coroutine));
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteInUpdate<T>(Func<T> coroutine) where T : IEnumerator
    {
        if (instance == null) return;
        ExecuteInUpdate(() => instance.StartCoroutine(coroutine()));
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteInUpdate<T>(params Func<T>[] coroutine) where T : IEnumerator
    {
        if (instance == null) return;
        ExecuteInUpdate(() =>
        {
            for (var i = 0; i < coroutine.Length; i++)
            {
                instance.StartCoroutine(coroutine[i]());
            }
        });
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteInUpdate<T>(IList<Func<T>> coroutines) where T : IEnumerator
    {
        if (instance == null) return;
        ExecuteInUpdate(() =>
        {
            for (var i = 0; i < coroutines.Count; i++)
            {
                instance.StartCoroutine(coroutines[i]());
            }
        });
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void StartCoroutine<T>(T coroutines) where T : IEnumerator
    {
        if (instance == null) return;
        instance.StartCoroutine(coroutines);
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void StartCoroutine<T>(Func<T> coroutines) where T : IEnumerator
    {
        if (instance == null) return;
        instance.StartCoroutine(coroutines());
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void StartCoroutine<T>(IList<Func<T>> coroutines) where T : IEnumerator
    {
        if (instance is null || coroutines is null) return;
        for (var i = 0; i < coroutines.Count; i++)
            instance.StartCoroutine(coroutines[i]());
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteInUpdate<T>(T coroutine1, T coroutine2) where T : IEnumerator
    {
        if (instance == null) return;
        ExecuteInUpdate(() =>
        {
            instance.StartCoroutine(coroutine1);
            instance.StartCoroutine(coroutine2);
        });
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteInUpdate<T>(T coroutine1, T coroutine2, T coroutine3) where T : IEnumerator
    {
        if (instance == null) return;
        ExecuteInUpdate(() =>
        {
            instance.StartCoroutine(coroutine1);
            instance.StartCoroutine(coroutine2);
            instance.StartCoroutine(coroutine3);
        });
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public static void ExecuteInUpdate<T>(params T[] actions) where T : IEnumerator
    {
        if (actions is null || actions.Length == 0 || instance is null) return;
        ExecuteInUpdate(() =>
        {
            foreach (var action in actions)
            {
                instance.StartCoroutine(action);
            }
        });
    }

    /// <summary>
    /// 在Update中执行
    /// </summary>
    public static void ExecuteInUpdate<T>(Action<T> action, T arg)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (actionQueuesUpdateFunc)
        {
            // actionQueuesUpdateFunc.Add(() => action(arg));
            noActionQueueToExecuteUpdateFunc = false;
        }
    }

    // /// <summary>
    // /// 在Update中执行
    // /// </summary>
    // public static void ExecuteInUpdate<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
    // {
    //     if (action == null) throw new ArgumentNullException(nameof(action));
    //
    //     lock (actionQueuesUpdateFunc)
    //     {
    //         actionQueuesUpdateFunc.Add(() => action(arg1, arg2));
    //         noActionQueueToExecuteUpdateFunc = false;
    //     }
    // }
    //
    // /// <summary>
    // /// 在Update中执行
    // /// </summary>
    // public static void ExecuteInUpdate<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
    // {
    //     if (action == null) throw new ArgumentNullException(nameof(action));
    //
    //     lock (actionQueuesUpdateFunc)
    //     {
    //         actionQueuesUpdateFunc.Add(() => action(arg1, arg2, arg3));
    //         noActionQueueToExecuteUpdateFunc = false;
    //     }
    // }
    //
    // /// <summary>
    // /// 在Update中执行
    // /// </summary>
    // public static void ExecuteInUpdate<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    // {
    //     if (action == null) throw new ArgumentNullException(nameof(action));
    //
    //     lock (actionQueuesUpdateFunc)
    //     {
    //         actionQueuesUpdateFunc.Add(() => action(arg1, arg2, arg3, arg4));
    //         noActionQueueToExecuteUpdateFunc = false;
    //     }
    // }

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
    public static void ExecuteInUpdate(params Delegate[] action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (actionQueuesUpdateFunc)
        {
            actionQueuesUpdateFunc.AddRange(action);
            noActionQueueToExecuteUpdateFunc = false;
        }
    }

    /// <summary>
    /// 在Update中执行
    /// </summary>
    public static void ExecuteInUpdate(Delegate action)
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
    public static void ExecuteInUpdate(Action action1, Action action2)
    {
        if (action1 == null) throw new ArgumentNullException(nameof(action1));

        lock (actionQueuesUpdateFunc)
        {
            actionQueuesUpdateFunc.Add(action1);
            actionQueuesUpdateFunc.Add(action2);
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
    /// 在Update中执行
    /// </summary>
    public static void ExecuteInUpdate(ICollection<Action> action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (actionQueuesUpdateFunc)
        {
            actionQueuesUpdateFunc.AddRange(action);
            noActionQueueToExecuteUpdateFunc = false;
        }
    }

    internal partial class ThreadMono
    {
        private readonly List<Delegate> mAactionCopiedQueueUpdateFunc = new List<Delegate>();

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
            // foreach (var action in mAactionCopiedQueueUpdateFunc) action?.DynamicInvoke();
            if (mAactionCopiedQueueUpdateFunc.Count == 0) return;
            StartCoroutine(Invoke(mAactionCopiedQueueUpdateFunc.ToArray()));
            mAactionCopiedQueueUpdateFunc.Clear();
        }

        private static IEnumerator Invoke(IList<Delegate> delegates)
        {
            for (var i = 0; i < delegates.Count; i++)
            {
                delegates[i]?.DynamicInvoke();
            }

            yield break;
        }
    }
}

#endif