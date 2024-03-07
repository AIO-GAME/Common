#define ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK

#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK)
using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    partial class Runner
    {
        [ContextStatic] private static readonly Queue<Action> actionQueuesFixedUpdateFunc = new Queue<Action>();

        [ContextStatic] private static volatile bool noActionQueueToExecuteFixedUpdateFunc = true;

        /// <summary>
        /// 在FixedUpdate更新
        /// </summary>
        public static void UpdateFixed(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (actionQueuesFixedUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }
        }

        private partial class ThreadMono
        {
            /// <summary>
            /// 在FixedUpdate更新
            /// </summary>
            public void FixedUpdate()
            {
                if (noActionQueueToExecuteFixedUpdateFunc) return;
                //清空队列中 残留的操作函数
                lock (actionQueuesFixedUpdateFunc)
                {
                    if (actionQueuesFixedUpdateFunc.Count == 0) return;
                    noActionQueueToExecuteFixedUpdateFunc = true; //并开启执行状态
                    lock (mActionCopiedQueueFixedUpdateFunc)
                    {
                        mActionCopiedQueueFixedUpdateFunc.AddRange(actionQueuesFixedUpdateFunc); //将等待队列中的操作 复制到执行队列中
                        actionQueuesFixedUpdateFunc.Clear();
                        var tempArray = Delegate.Combine(mActionCopiedQueueFixedUpdateFunc.ToArray());
                        mActionCopiedQueueFixedUpdateFunc.Clear();
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
    }

#endif
}