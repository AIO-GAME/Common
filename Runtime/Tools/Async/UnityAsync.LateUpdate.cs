﻿/*|==========|*|
|*|Author:   |*| -> XINAN
|*|Date:     |*| -> 2023-06-11
|*|==========|*/


#define ENABLE_LATEUPDATE_FUNCTION_CALLBACK
#if SUPPORTE_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_LATEUPDATE_FUNCTION_CALLBACK)

using System;
using System.Collections.Generic;

public partial class UnityAsync
{
    [ContextStatic] private static readonly List<Action> actionQueuesLateUpdateFunc = new List<Action>();
    [ContextStatic] private static volatile bool noActionQueueToExecuteLateUpdateFunc = true;

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

    internal partial class ThreadMono
    {
        private readonly List<Action> mActionCopiedQueueLateUpdateFunc = new List<Action>();

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

            foreach (var action in mActionCopiedQueueLateUpdateFunc)
#if SUPPORTE_UNITASK
            UniTask.RunOnThreadPool(action);
#else
                action.Invoke();
#endif
        }
    }
}

#endif
