#define ENABLE_LATEUPDATE_FUNCTION_CALLBACK
#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_LATEUPDATE_FUNCTION_CALLBACK)
using System;
using System.Collections.Generic;

namespace AIO
{
    partial class Runner
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

        partial class ThreadMono
        {
            public void LateUpdate()
            {
                if (noActionQueueToExecuteLateUpdateFunc) return;
            }
        }
    }

#endif
}