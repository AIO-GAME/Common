﻿#region

using System;
using AIO;

#endregion

/// <summary>
/// 国际标准单位制中的长度单位
/// </summary>
[Flags]
public enum ULength
{
    /// <summary>
    /// 单位 [千米] 1千米(公里)= 1,000米(公尺)= 100,000厘米(公分) = 1,000,000毫米(公厘)
    /// </summary>
    [UDefault(1E3f)]
    km = 1,

    /// <summary>
    /// 单位 [米]
    /// </summary>
    [UDefault(1)]
    m = 2,

    /// <summary>
    /// 单位 [分米] 1分米 = 0.0001千米(km) = 0.1米(m) =10厘米(cm) = 100毫米(mm)
    /// </summary>
    [UDefault(1E-1f)]
    dm = 4,

    /// <summary>
    /// 单位 [厘米] 1厘米 = 10毫米 = 0.1分米 = 0.01米 = 0.00001千米
    /// </summary>
    [UDefault(1E-2f)]
    cm = 8,

    /// <summary>
    /// 单位 [毫米] 1毫米=0.1厘米=0.01分米=0.001米=0.000001千米
    /// </summary>
    [UDefault(1E-4f)]
    mm = 16,

    /// <summary>
    /// 单位 [微米] 1微米相当于1米的一百万分之一
    /// </summary>
    [UDefault(1E-6f)]
    μm = 32,

    /// <summary>
    /// 单位 [纳米] 1纳米=10-9米
    /// </summary>
    [UDefault(1E-9f)]
    nm = 64,

    /// <summary>
    /// 单位 [皮米] 1皮米=10-12米=0.001 纳米(nm) =0.000001 微米(μm)
    /// </summary>
    [UDefault(1E-12f)]
    pm = 128,

    /// <summary>
    /// 单位 [拍米] 1Pm =10的15次方m 1Pm= 1000000000000000m
    /// </summary>
    [UDefault(1E-15d)]
    Pm

    // /// <summary>
    // /// 单位 [马咖米] [兆米] 1兆米=1000千米=1000000米
    // /// </summary>
    // Mm,

    // /// <summary>
    // /// 单位 [埃格斯特朗] 1 Å = 10-10米 = 0.1纳米
    // /// </summary>
    // Å,

    // /// <summary>
    // /// 单位 [普朗克长度] 大致等于1.6x10^-35米，即1.6x10-33厘米，是一个质子直径的1022分之一
    // /// </summary>
    // PI,

    // /// <summary>
    // /// 单位 [丝米] 1丝米=1/10000米
    // /// </summary>
    // dmm,

    // /// <summary>
    // /// 单位 [忽米] 1忽米=1/100,000米= 10微米= 0.1丝米= 0.01毫米
    // /// </summary>
    // cmm,

    // /// <summary>
    // /// 单位 [飞米] 1飞米=0.001皮米(pm) =0.000 001纳米(nm)
    // /// </summary>
    // fm,

    // /// <summary>
    // /// 单位 [阿米] 1阿米相当于10-18米 || 1阿米=0.001飞米(fm) =0.000 001皮米（pm） =0.000 000 001 纳米（nm） [3] 。阿米是长度单位，用于测量原子间距离，特别是晶体中的原子间距离。
    // /// </summary>
    // am,

    // /// <summary>
    // /// 单位 [幺米] 1幺米为1ym = 10-24m = 1.0570x10-40光年 =0.001仄米
    // /// </summary>
    // ym,

    // /// <summary>
    // /// 单位 [仄米] [介米] 1仄米相当于10-21米
    // /// </summary>
    // zeptom,
}