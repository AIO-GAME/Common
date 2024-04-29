#define ENABLE_LATEUPDATE_FUNCTION_CALLBACK
#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_LATEUPDATE_FUNCTION_CALLBACK)

#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Runner
    {
        [ContextStatic]
        private static readonly Queue<Action> actionQueuesLateUpdateFunc = new Queue<Action>();

        [ContextStatic]
        private static volatile bool noActionQueueToExecuteLateUpdateFunc = true;

        /// <summary>
        /// 在LateUpdate更新
        /// </summary>
        public static void UpdateLate(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (actionQueuesLateUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }
        }

        #region Nested type: ThreadMono

        private partial class ThreadMono
        {
            public void LateUpdate()
            {
                if (noActionQueueToExecuteLateUpdateFunc) return;

                //清空队列中 残留的操作函数
                lock (actionQueuesLateUpdateFunc)
                {
                    if (actionQueuesLateUpdateFunc.Count == 0) return;
                    noActionQueueToExecuteLateUpdateFunc = true; //并开启执行状态
                    lock (mActionCopiedQueueLateUpdateFunc)
                    {
                        mActionCopiedQueueLateUpdateFunc.AddRange(actionQueuesLateUpdateFunc); //将等待队列中的操作 复制到执行队列中
                        actionQueuesLateUpdateFunc.Clear();
                        var tempArray = Delegate.Combine(mActionCopiedQueueLateUpdateFunc.ToArray());
                        mActionCopiedQueueLateUpdateFunc.Clear();
#if SUPPORT_UNITASK
                        Action().ToUniTask().Preserve().SuppressCancellationThrow();
#else
                        StartCoroutine(Action());
#endif
                        return;

                        IEnumerator Action()
                        {
                            tempArray?.DynamicInvoke();
                            tempArray = null;
                            yield break;
                        }
                    }
                }
            }
        }

        #endregion
    }

#endif
}