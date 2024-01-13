/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-07-28

|||✩ - - - - - |*/


using System;
using AIO;

/// <summary>
/// 时间单位
/// </summary>
[Flags]
public enum UTime
{
    /// <summary>
    /// 纳秒
    /// </summary>
    [UDefault(1E-9f)] ns = 1,

    /// <summary>
    /// 微秒
    /// </summary>
    [UDefault(1E-6f)] μs = 2,

    /// <summary>
    /// 毫秒
    /// </summary>
    [UDefault(1E-3f)] ms = 4,

    /// <summary>
    /// 秒
    /// </summary>
    [UDefault(1)] s = 8,

    /// <summary>
    /// 分钟
    /// </summary>
    [UDefault(60)] m = 16,

    /// <summary>
    /// 小时
    /// </summary>
    [UDefault(3600)] h = 32,

    /// <summary>
    /// 天
    /// </summary>
    [UDefault(86400)] d = 64,

    /// <summary>
    /// 周
    /// </summary>
    [UDefault(86400 * 7f)] w = 128,

    /// <summary>
    /// 月
    /// </summary>
    [UDefault(86400 * 30f)] M = 256,

    /// <summary>
    /// 年
    /// </summary>
    [UDefault(86400 * 365f)] y = 512,

    /// <summary>
    /// 世纪
    /// </summary>
    [UDefault(86400 * 36500f)] ly = 1024,
}
