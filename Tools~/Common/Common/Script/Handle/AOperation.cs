/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-15
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Collections;
using System.Threading.Tasks;

/// <summary>
/// 操作接口
/// </summary>
public class AOperation : IProgressOperation
{
    /// <inheritdoc />
    public ProgressState State { get; protected set; }


    /// <summary>
    /// 进度回调参数
    /// </summary>
    protected AProgress progress;

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
        State = ProgressState.Ready;
    }

    /// <inheritdoc />
    public void Begin()
    {
        if (State != ProgressState.Ready) return;
        State = ProgressState.Running;
        OnBegin();
        progress.OnBegin?.Invoke();
    }

    /// <inheritdoc />
    public void Cancel()
    {
        switch (State)
        {
            default:
            case ProgressState.Fail:
            case ProgressState.Cancel:
            case ProgressState.Finish:
                return;
            case ProgressState.Pause:
            case ProgressState.Ready:
            case ProgressState.Running:
                State = ProgressState.Cancel;
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
            case ProgressState.Fail:
            case ProgressState.Ready:
            case ProgressState.Cancel:
            case ProgressState.Finish:
            case ProgressState.Pause:
                return;
            case ProgressState.Running:
                State = ProgressState.Pause;
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
            case ProgressState.Fail:
            case ProgressState.Ready:
            case ProgressState.Cancel:
            case ProgressState.Finish:
            case ProgressState.Running:
                return;
            case ProgressState.Pause:
                State = ProgressState.Running;
                OnResume();
                progress.OnResume?.Invoke();
                break;
        }
    }

    /// <inheritdoc />
    public IEnumerator WaitCo()
    {
        if (State != ProgressState.Running) yield break;
        yield return OnWaitCo();
    }

    /// <summary>
    /// 迭代器等待
    /// </summary>
    protected virtual IEnumerator OnWaitCo()
    {
        yield break;
    }

    /// <inheritdoc />
    public Task WaitAsync()
    {
        if (State != ProgressState.Running) throw new TaskCanceledException();
        return OnWaitAsync();
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
        if (State != ProgressState.Running) return;
        OnWait();
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
}