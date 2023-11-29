/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-29
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using AIO;

/// <summary>
/// 进度信息
/// </summary>
public interface IProgressInfo
{
    /// <summary>
    /// 当前大小
    /// </summary>
    string CurrentStr { get; }

    /// <summary>
    /// 总大小
    /// </summary>
    string TotalStr { get; }

    /// <summary>
    /// 总值
    /// </summary>
    long Total { get; }

    /// <summary>
    /// 当前值
    /// </summary>
    long Current { get; }

    /// <summary>
    /// 进度
    /// </summary>
    int Progress { get; }

    /// <summary>
    /// 当前名称
    /// </summary>
    string CurrentInfo { get; }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns></returns>
    string ToString();
}

/// <summary>
/// 进度信息
/// </summary>
public struct ProgressInfo : IProgressInfo
{
    /// <inheritdoc />
    public string CurrentStr => Current.ToConverseStringFileSize();

    /// <inheritdoc />
    public string TotalStr => Total.ToConverseStringFileSize();

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
    string IProgressInfo.ToString()
    {
        return ToString();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Progress}% [{CurrentStr}/{TotalStr}] {CurrentInfo}";
    }
}