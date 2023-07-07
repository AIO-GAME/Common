/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-07
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UnityEngine
{
    /// <summary>
    /// 辅助定时器容器
    /// </summary>
    internal interface ITimerContainer : IDisposable
    {
        /// <summary>
        /// 精度表
        /// </summary>
        Stopwatch Watch { get; }

        /// <summary>
        /// 容器列表
        /// </summary>
        List<ITimerOperator> List { get; }

        /// <summary>
        /// 精度单位
        /// </summary>
        long Unit { get; }

        /// <summary>
        /// 当前时间
        /// </summary>
        long Counter { get; }

        /// <summary>
        /// 剩余定时器任务数量
        /// </summary>
        int RemainNum { get; }

        /// <summary>
        /// 当前辅助定时器总数量
        /// </summary>
        int TotalNum { get; }

        /// <summary>
        /// 取消定时器
        /// </summary>
        void Cancel();

        int ID { get; }

        /// <summary>
        /// 更新刷新List时间
        /// </summary>
        long UpdateCacheTime { get; }
    }
}