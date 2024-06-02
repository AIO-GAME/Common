#region namespace

using System;
using System.Collections;
#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

#endregion

namespace AIO
{
    partial class Runner
    {
        #region Nested type: ThreadMono

        partial class ThreadMono
        {
            private bool mIsUpdateFixedExe;

            private IEnumerator UpdateFixAction()
            {
                mIsUpdateFixedExe = true;
                while (QueueCopiedUpdateFixed.Count > 0)
                {
                    if (!QueueCopiedUpdateFixed.TryDequeue(out var action)) continue;
                    action?.DynamicInvoke();
                }

                mIsUpdateFixedExe = false;
                yield break;
            }

            public void FixedUpdate()
            {
                if (QueuesDelegateUpdateLateState) return;        //判断当前是否有操作正在执行
                if (QueuesDelegateUpdateFixed.Count == 0) return; //清空队列中 残留的操作函数
                QueuesDelegateUpdateLateState = true;             //并开启执行状态
                while (QueuesDelegateUpdateFixed.Count > 0)       //将等待队列中的操作 复制到执行队列中
                {
                    if (!QueuesDelegateUpdateFixed.TryDequeue(out var action)) continue;
                    QueueCopiedUpdateFixed.Enqueue(action);
                }

                QueuesDelegateUpdateLateState = false; //并开启执行状态
                if (!mIsUpdateFixedExe)
                {
#if SUPPORT_UNITASK
                    UpdateFixAction().ToUniTask().Preserve().SuppressCancellationThrow();
#else
                    StartCoroutine(UpdateFixAction());
#endif
                }
            }
        }

        #endregion
    }
}