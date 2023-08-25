/*|==========|*|
|*|Author:   |*| -> XINAN
|*|Date:     |*| -> 2023-06-11
|*|==========|*/

#define ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK

#if SUPPORTE_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK)
using System;
using System.Collections.Generic;

public partial class UnityAsync
{
    [ContextStatic] private static readonly List<Action> actionQueuesFixedUpdateFunc = new List<Action>();

    [ContextStatic] private static volatile bool noActionQueueToExecuteFixedUpdateFunc = true;

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

    internal partial class ThreadMono
    {
        private readonly List<Action> mActionCopiedQueueFixedUpdateFunc = new List<Action>();

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
            foreach (var action in mActionCopiedQueueFixedUpdateFunc)
#if SUPPORTE_UNITASK
                UniTask.RunOnThreadPool(action);
#else
                action.Invoke();
#endif
        }
    }
}

#endif
