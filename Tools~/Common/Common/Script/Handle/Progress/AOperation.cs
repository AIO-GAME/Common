/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-15
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// 操作接口
    /// </summary>
    public class AOperation : IProgressOperation
    {
        /// <summary>
        /// 进度回调参数
        /// </summary>
        private readonly AProgress progress;

        /// <summary>
        /// 取消令牌
        /// </summary>
        private CancellationTokenSource cancellationTokenSource { get; }

        /// <summary>
        /// 取消令牌
        /// </summary>
        protected CancellationToken cancellationToken { get; private set; }

        /// <inheritdoc />
        public IProgressReport Report => progress;

        /// <inheritdoc />
        public IProgressEvent Event
        {
            get => progress;
            set
            {
                progress.OnProgress = value.OnProgress;
                progress.OnBegin = value.OnBegin;
                progress.OnComplete = value.OnComplete;
                progress.OnError = value.OnError;
                progress.OnResume = value.OnResume;
                progress.OnPause = value.OnPause;
                progress.OnCancel = value.OnCancel;
            }
        }

        /// <inheritdoc />
        public IProgressInfo Progress => progress;

        /// <summary>
        /// 状态
        /// </summary>
        protected EProgressState State
        {
            get => progress.State;
            set => progress.State = value;
        }

        /// <summary>
        /// 当前信息
        /// </summary>
        protected string CurrentInfo
        {
            get => progress.CurrentInfo;
            set => progress.CurrentInfo = value;
        }

        /// <summary>
        /// 开始值
        /// </summary>
        protected long StartValue
        {
            get => progress.StartValue;
            set => progress.StartValue = value;
        }

        /// <summary>
        /// 总进度
        /// </summary>
        protected long TotalValue
        {
            get => progress.TotalValue;
            set => progress.TotalValue = value;
        }

        /// <summary>
        /// 当前进度
        /// </summary>
        protected long CurrentValue
        {
            get => progress.CurrentValue;
            set => progress.CurrentValue = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AOperation()
        {
            progress = new AProgress();
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
        }

        /// <inheritdoc />
        public void Begin()
        {
            if (State != EProgressState.Ready) return;
            State = EProgressState.Running;
            progress.Begin();
            OnBegin();
            progress.OnBegin?.Invoke();
        }

        /// <inheritdoc />
        public void Cancel()
        {
            switch (State)
            {
                default:
                case EProgressState.Fail:
                case EProgressState.Cancel:
                case EProgressState.Finish:
                    return;
                case EProgressState.Pause:
                case EProgressState.Ready:
                case EProgressState.Running:
                    State = EProgressState.Cancel;
                    cancellationToken.ThrowIfCancellationRequested();
                    progress.Cancel();
                    OnCancel();
                    progress.OnCancel?.Invoke();
                    break;
            }
        }

        /// <inheritdoc />
        public void Pause()
        {
            switch (State)
            {
                default:
                case EProgressState.Fail:
                case EProgressState.Ready:
                case EProgressState.Cancel:
                case EProgressState.Finish:
                case EProgressState.Pause:
                    return;
                case EProgressState.Running:
                    State = EProgressState.Pause;
                    progress.Pause();
                    OnPause();
                    progress.OnPause?.Invoke();
                    break;
            }
        }

        /// <inheritdoc />
        public void Resume()
        {
            switch (State)
            {
                default:
                case EProgressState.Fail:
                case EProgressState.Ready:
                case EProgressState.Cancel:
                case EProgressState.Finish:
                case EProgressState.Running:
                    return;
                case EProgressState.Pause:
                    State = EProgressState.Running;
                    progress.Resume();
                    OnResume();
                    progress.OnResume?.Invoke();
                    break;
            }
        }

        /// <inheritdoc />
        public IEnumerator WaitCo()
        {
            if (State != EProgressState.Running) yield break;
            yield return OnWaitCo();
            Finish();
        }

        /// <summary>
        /// 迭代器等待
        /// </summary>
        protected virtual IEnumerator OnWaitCo()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task WaitAsync()
        {
            if (State != EProgressState.Running) return;
            await OnWaitAsync();
            Finish();
        }

        /// <inheritdoc />
        public async void WaitAsyncCallBack()
        {
            if (State != EProgressState.Running) return;
            await OnWaitAsync();
            Finish();
        }

        /// <summary>
        /// 完成
        /// </summary>
        protected void Finish()
        {
            progress.Complete();
            progress.OnComplete?.Invoke(progress);
        }

        /// <summary>
        /// 异步等待
        /// </summary>
        protected virtual Task OnWaitAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Wait()
        {
            if (State != EProgressState.Running) return;
            OnWait();
            Finish();
        }

        /// <summary>
        /// 开始
        /// </summary>
        protected virtual void OnBegin()
        {
        }

        /// <summary>
        /// 取消
        /// </summary>
        protected virtual void OnCancel()
        {
        }

        /// <summary>
        /// 暂停
        /// </summary>
        protected virtual void OnPause()
        {
        }

        /// <summary>
        /// 恢复
        /// </summary>
        protected virtual void OnResume()
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
            cancellationTokenSource.Dispose();
            OnDispose();
        }

        /// <summary>
        /// 等待
        /// </summary>
        protected virtual void OnWait()
        {
        }

        /// <summary>
        /// 释放
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        /// <inheritdoc />
        public sealed override string ToString()
        {
            return Report.ToString();
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            return 0;
        }

        /// <inheritdoc />
        public sealed override bool Equals(object obj)
        {
            return false;
        }
    }
}