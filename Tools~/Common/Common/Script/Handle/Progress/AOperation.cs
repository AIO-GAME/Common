/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-15
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections;
using System.Threading.Tasks;

/// <summary>
/// 操作接口
/// </summary>
public class AOperation : IProgressOperation
{
    /// <summary>
    /// 进度回调参数
    /// </summary>
    protected readonly AProgress progress;

    /// <summary>
    /// 状态
    /// </summary>
    protected EProgressState State
    {
        get => progress.State;
        set => progress.State = value;
    }

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

    /// <summary>
    /// 构造函数
    /// </summary>
    public AOperation()
    {
        progress = new AProgress();
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
        yield return null;
    }

    /// <inheritdoc />
    public async Task WaitAsync()
    {
        if (State != EProgressState.Running) return;
        await OnWaitAsync();
        Finish();
    }

    private void Finish()
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