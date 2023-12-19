/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

/// <summary>
/// 进度报告
/// </summary>
public interface IProgressReport
{
    /// <summary>
    /// 进度状态
    /// </summary>
    EProgressState State { get; }

    /// <summary>
    /// 平均速度
    /// </summary>
    long AverageSpeed { get; }

    /// <summary>
    /// 运行时间
    /// </summary>
    TimeSpan RemainingTime { get; }

    /// <summary>
    /// 开始时间
    /// </summary>
    DateTime StartTime { get; }

    /// <summary>
    /// 结束时间
    /// </summary>
    DateTime EndTime { get; }

    /// <summary>
    /// 结束进度
    /// </summary>
    long LastProgress { get; }

    /// <summary>
    /// 开始进度
    /// </summary>
    long StartProgress { get; }

    /// <summary>
    /// 开始值
    /// </summary>
    long StartValue { get; }

    /// <summary>
    /// 结束值
    /// </summary>
    long EndValue { get; }

    /// <summary>
    /// 有效值
    /// </summary>
    long VirtualValue  { get; }

    /// <summary>
    /// 有效进度
    /// </summary>
    long VirtualProgress { get; }

    /// <summary>
    /// 转化为字符串
    /// </summary>
    /// <returns>
    /// "Progress: 0.00% Speed: 0.00MB/s Remaining: 0:00:00"
    /// </returns>
    string ToString();
}