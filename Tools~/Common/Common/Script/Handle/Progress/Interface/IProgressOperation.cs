﻿/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/


using System;
using System.Threading.Tasks;

/// <summary>
/// 进度操作
/// </summary>
public interface IProgressOperation : IDisposable
{
    /// <summary>
    /// 进度报告
    /// </summary>
    IProgressReport Report { get; }

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

#if NET_5_0_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// 迭代器等待
    /// </summary>
    [Obsolete("net framework 5 or net core 3.0 or later version support this", true)]
    IEnumerator WaitCo();
#endif

    /// <summary>
    /// 异步等待
    /// </summary>
    /// <returns></returns>
    Task WaitAsync();

    /// <summary>
    /// 同步等待
    /// </summary>
    void Wait();

    /// <summary>
    /// 异步回调
    /// </summary>
    void WaitAsyncCallBack();
}