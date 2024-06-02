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

        private partial class ThreadMono
        {
            private bool mIsUpdateLateExe;

            private IEnumerator UpdateLateAction()
            {
                mIsUpdateLateExe = true;
                while (QueueCopiedUpdateLate.Count > 0)
                {
                    if (!QueueCopiedUpdateLate.TryDequeue(out var action)) continue;
                    action?.DynamicInvoke();
                }

                mIsUpdateLateExe = false;
                yield break;
            }

            public void LateUpdate()
            {
                if (QueuesDelegateUpdateFixedState) return;      //判断当前是否有操作正在执行
                if (QueuesDelegateUpdateLate.Count == 0) return; //清空队列中 残留的操作函数
                QueuesDelegateUpdateFixedState = true;           //并开启执行状态
                while (QueuesDelegateUpdateLate.Count > 0)       //将等待队列中的操作 复制到执行队列中
                {
                    if (!QueuesDelegateUpdateLate.TryDequeue(out var action)) continue;
                    QueueCopiedUpdateLate.Enqueue(action);
                }

                QueuesDelegateUpdateFixedState = false; //并开启执行状态
                if (!mIsUpdateLateExe)
                {
#if SUPPORT_UNITASK
                    UpdateLateAction().ToUniTask().Preserve().SuppressCancellationThrow();
#else
                    StartCoroutine(UpdateLateAction());
#endif
                }
            }
        }

        #endregion
    }
}