/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-29
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Threading;

/// <summary>
/// 进度回调参数
/// </summary>
public interface IProgressEvent
{
    /// <summary>
    /// 进度回调
    /// </summary>
    Action<IProgressInfo> OnProgress { get; set; }

    /// <summary>
    /// 完成回调
    /// </summary>
    Action OnComplete { get; set; }

    /// <summary>
    /// 错误回调
    /// </summary>
    Action<Exception> OnError { get; set; }
}

/// <summary>
/// 进度回调参数
/// </summary>
public struct AProgressEvent : IProgressEvent
{
    /// <inheritdoc />
    public Action<IProgressInfo> OnProgress { get; set; }

    /// <inheritdoc />
    public Action OnComplete { get; set; }

    /// <inheritdoc />
    public Action<Exception> OnError { get; set; }
}

/// <summary>
/// 进度参数
/// </summary>
public interface IProgressArg : IDisposable
{
    /// <summary>
    /// 总值
    /// </summary>
    long Total { set; get; }

    /// <summary>
    /// 当前值
    /// </summary>
    long Current { set; get; }

    /// <summary>
    /// 当前信息
    /// </summary>
    string CurrentInfo { set; get; }
}

/// <summary>
/// 进度参数
/// </summary>
public interface IProgressHandle : IProgressEvent, IProgressArg
{
}

/// <summary>
/// 进度参数
/// </summary>
public struct AProgress : IProgressHandle
{
    /// <summary>
    /// 是否取消
    /// </summary>
    public bool IsCancel;

    internal ProgressInfo Info;

    internal Action OnCancel;

    /// <summary>
    /// 构造函数
    /// </summary>
    public AProgress(IProgressEvent @event)
    {
        IsCancel = false;
        Info = new ProgressInfo();
        OnCancel = null;

        if (@event is null)
        {
            OnProgress = null;
            OnComplete = null;
            OnError = null;
        }
        else
        {
            OnProgress = @event.OnProgress;
            OnComplete = @event.OnComplete;
            OnError = @event.OnError;
        }
    }

    /// <summary>
    /// 取消令牌
    /// </summary>
    public CancellationToken CancellationToken;

    /// <summary>
    /// 总值
    /// </summary>
    public long Total
    {
        set => Info.Total = value;
        get => Info.Total;
    }

    /// <summary>
    /// 当前值
    /// </summary>
    public long Current
    {
        get => Info.Current;
        set
        {
            if (IsCancel) return;
            Info.Current = value;
            OnProgress?.Invoke(Info);
        }
    }

    /// <summary>
    /// 当前信息
    /// </summary>
    public string CurrentInfo
    {
        get => Info.CurrentInfo;
        set => Info.CurrentInfo = value;
    }

    /// <summary>
    /// 进度回调
    /// </summary>
    public Action<IProgressInfo> OnProgress { get; set; }

    /// <summary>
    /// 完成回调
    /// </summary>
    public Action OnComplete { get; set; }

    /// <summary>
    /// 错误回调
    /// </summary>
    public Action<Exception> OnError { get; set; }

    /// <summary>
    /// 取消
    /// </summary>
    public void Cancel()
    {
        IsCancel = true;
        if (!CancellationToken.CanBeCanceled) return;

        CancellationToken.ThrowIfCancellationRequested();
        if (OnCancel != null) CancellationToken.Register(OnCancel);
    }

    /// <summary>
    /// 析构函数
    /// </summary>
    public void Dispose()
    {
        IsCancel = false;
        OnProgress = null;
        OnComplete = null;
        OnError = null;
    }
}