#region namespace

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
            private bool mIsUpdateExe;

            private IEnumerator UpdateAction()
            {
                mIsUpdateExe = true;
                while (QueueCopiedUpdate.Count > 0)
                {
                    if (!QueueCopiedUpdate.TryDequeue(out var action)) continue;
                    action?.DynamicInvoke();
                }

                mIsUpdateExe = false;
                yield break;
            }

            private void UpdateQueue()
            {
                QueuesDelegateUpdateState = true;      //并开启执行状态
                while (QueuesDelegateUpdate.Count > 0) //将等待队列中的操作 复制到执行队列中
                {
                    if (!QueuesDelegateUpdate.TryDequeue(out var action)) continue;
                    QueueCopiedUpdate.Enqueue(action);
                }

                QueuesDelegateUpdateState = false; //并开启执行状态
            }

            public void Update()
            {
                if (!QueuesCoroutineState && !QueuesCoroutine.IsEmpty) UpdateCoroutine();
                if (!QueuesDelegateUpdateState && !QueuesDelegateUpdate.IsEmpty) UpdateQueue();
                if (!mIsUpdateExe && !QueueCopiedUpdate.IsEmpty)
                {
#if SUPPORT_UNITASK
                    UpdateAction().ToUniTask().Preserve().SuppressCancellationThrow();
#else
                    StartCoroutine(UpdateAction());
#endif
                }
            }
        }

        #endregion
    }
}