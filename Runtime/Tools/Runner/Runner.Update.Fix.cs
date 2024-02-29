#define ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK

#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK)
using System;
using System.Collections.Generic;

namespace AIO
{
    partial class Runner
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

        partial class ThreadMono
        {
            /// <summary>
            /// 在FixedUpdate更新
            /// </summary>
            public void FixedUpdate()
            {
                if (noActionQueueToExecuteFixedUpdateFunc) return;
            }
        }
    }

#endif
}