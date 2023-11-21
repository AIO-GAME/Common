using System;
using System.Net;
using System.Threading;
using AIO;

/// <summary>
/// 进度信息
/// </summary>
public struct ProgressInfo
{
    /// <summary>
    /// 当前大小
    /// </summary>
    public string CurrentSize => Current.ToConverseStringFileSize();

    /// <summary>
    /// 总大小
    /// </summary>
    public string TotalSize => Total.ToConverseStringFileSize();

    /// <summary>
    /// 总值
    /// </summary>
    public long Total { get; internal set; }

    /// <summary>
    /// 当前值
    /// </summary>
    public long Current { get; internal set; }

    /// <summary>
    /// 进度
    /// </summary>
    public int Progress => (int)(Current / (double)Total * 100);

    /// <summary>
    /// 当前名称
    /// </summary>
    public string CurrentInfo { get; internal set; }

    internal ProgressInfo(long total, long current, string currentInfo = null)
    {
        Total = total;
        Current = current;
        CurrentInfo = currentInfo;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Progress}% [{CurrentSize}/{TotalSize}] {CurrentInfo}";
    }
}

/// <summary>
/// 进度参数
/// </summary>
public struct ProgressArgs : IDisposable
{
    /// <summary>
    /// 是否取消
    /// </summary>
    public bool IsCancel;

    internal ProgressInfo Info;

    internal Action OnCancel;

    /// <summary>
    /// 取消令牌
    /// </summary>
    public CancellationToken CancellationToken;

    /// <summary>
    /// 总值
    /// </summary>
    internal long Total
    {
        get => Info.Total;
        set => Info.Total = value;
    }

    /// <summary>
    /// 当前值
    /// </summary>
    internal long Current
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
    /// 当前名称
    /// </summary>
    internal string CurrentInfo
    {
        get => Info.CurrentInfo;
        set => Info.CurrentInfo = value;
    }

    /// <summary>
    /// 进度回调
    /// </summary>
    public Action<ProgressInfo> OnProgress { get; set; }

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

/// <summary>
/// 处理器帮助类
/// </summary>
public static partial class AHandle
{
}