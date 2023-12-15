/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-29
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections;
using System.Threading.Tasks;

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
    /// 开始回调
    /// </summary>
    Action OnBegin { get; set; }

    /// <summary>
    /// 完成回调
    /// </summary>
    Action OnComplete { get; set; }

    /// <summary>
    /// 错误回调
    /// </summary>
    Action<Exception> OnError { get; set; }

    /// <summary>
    /// 恢复
    /// </summary>
    Action OnResume { get; set; }

    /// <summary>
    /// 暂停
    /// </summary>
    Action OnPause { get; set; }

    /// <summary>
    /// 取消
    /// </summary>
    Action OnCancel { get; set; }
}

/// <summary>
/// 进度回调参数
/// </summary>
public struct AProgressEvent : IProgressEvent
{
    /// <inheritdoc />
    public Action<IProgressInfo> OnProgress { get; set; }

    /// <inheritdoc />
    public Action OnBegin { get; set; }

    /// <inheritdoc />
    public Action OnComplete { get; set; }

    /// <inheritdoc />
    public Action<Exception> OnError { get; set; }

    /// <inheritdoc />
    public Action OnResume { get; set; }

    /// <inheritdoc />
    public Action OnPause { get; set; }

    /// <inheritdoc />
    public Action OnCancel { get; set; }
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
/// 进度状态
/// </summary>
public enum ProgressState : byte
{
    /// <summary>
    /// 准备
    /// </summary>
    Ready = 0,

    /// <summary>
    /// 运行
    /// </summary>
    Running,

    /// <summary>
    /// 取消
    /// </summary>
    Cancel,

    /// <summary>
    /// 暂停
    /// </summary>
    Pause,

    /// <summary>
    /// 完成
    /// </summary>
    Finish,
    
    /// <summary>
    /// 失败
    /// </summary>
    Fail,
}

/// <summary>
/// 进度操作
/// </summary>
public interface IProgressOperation : IDisposable
{
    /// <summary>
    /// 进度状态
    /// </summary>
    ProgressState State { get; }

    /// <summary>
    /// 进度参数
    /// </summary>
    IProgressEvent Event { get; set; }

    /// <summary>
    /// 开始
    /// </summary>
    void Begin();

    /// <summary>
    /// 取消
    /// </summary>
    void Cancel();

    /// <summary>
    /// 暂停
    /// </summary>
    void Pause();

    /// <summary>
    /// 恢复
    /// </summary>
    void Resume();

    /// <summary>
    /// 迭代器等待
    /// </summary>
    IEnumerator WaitCo();

    /// <summary>
    /// 异步等待
    /// </summary>
    /// <returns></returns>
    Task WaitAsync();

    /// <summary>
    /// 同步等待
    /// </summary>
    void Wait();
}

/// <summary>
/// 进度参数
/// </summary>
public struct AProgress : IProgressArg, IProgressEvent
{
    private ProgressInfo Info;

    /// <inheritdoc />
    public override string ToString()
    {
        return Info.ToString();
    }

    /// <inheritdoc />
    public long Total
    {
        set => Info.Total = value;
        get => Info.Total;
    }

    /// <inheritdoc />
    public long Current
    {
        get => Info.Current;
        set
        {
            Info.Current = value;
            OnProgress?.Invoke(Info);
        }
    }

    /// <inheritdoc />
    public string CurrentInfo
    {
        get => Info.CurrentInfo;
        set => Info.CurrentInfo = value;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        OnProgress = null;
        OnBegin = null;
        OnComplete = null;
        OnError = null;
        OnResume = null;
        OnPause = null;
        OnCancel = null;
    }

    /// <inheritdoc />
    public Action<IProgressInfo> OnProgress { get; set; }

    /// <inheritdoc />
    public Action OnBegin { get; set; }

    /// <inheritdoc />
    public Action OnComplete { get; set; }

    /// <inheritdoc />
    public Action<Exception> OnError { get; set; }

    /// <inheritdoc />
    public Action OnResume { get; set; }

    /// <inheritdoc />
    public Action OnPause { get; set; }

    /// <inheritdoc />
    public Action OnCancel { get; set; }
}