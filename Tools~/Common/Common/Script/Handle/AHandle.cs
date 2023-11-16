using System;
using AIO;

/// <summary>
/// 进度信息
/// </summary>
public readonly struct ProgressInfo
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
    public long Total { get; }

    /// <summary>
    /// 当前值
    /// </summary>
    public long Current { get; }

    /// <summary>
    /// 进度
    /// </summary>
    public int Progress => (int)(Current / (double)Total * 100);

    /// <summary>
    /// 当前名称
    /// </summary>
    public string CurrentName { get; }

    internal ProgressInfo(long total, long current, string currentName = null)
    {
        Total = total;
        Current = current;
        CurrentName = currentName;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[{CurrentSize}/{TotalSize}] {Progress}%";
    }
}

/// <summary>
/// 进度参数
/// </summary>
public struct ProgressArgs : IDisposable
{
    /// <summary>
    /// 总值
    /// </summary>
    internal long Total { get; set; }

    private long _Current;

    /// <summary>
    /// 当前值
    /// </summary>
    internal long Current
    {
        get => _Current;
        set
        {
            _Current = value;
            OnProgress?.Invoke(new ProgressInfo(Total, _Current, CurrentName));
        }
    }

    /// <summary>
    /// 当前名称
    /// </summary>
    internal string CurrentName { get; set; }

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
    /// 析构函数
    /// </summary>
    public void Dispose()
    {
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