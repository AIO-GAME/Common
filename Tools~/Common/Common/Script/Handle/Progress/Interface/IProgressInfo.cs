/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-29
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

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
    long TotalValue { get; }

    /// <summary>
    /// 当前值
    /// </summary>
    long CurrentValue { get; }

    /// <summary>
    /// 每秒下载速度
    /// </summary>
    long Speed { get; }

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
