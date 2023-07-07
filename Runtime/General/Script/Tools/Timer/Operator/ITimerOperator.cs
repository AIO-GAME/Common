/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-07
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;

namespace UnityEngine
{
    internal partial interface ITimerOperator : IDisposable
    {
        /// <summary>
        /// 时间层级序列
        /// </summary>
        byte Index { get; }

        /// <summary>
        /// 当前定时器计时总单位
        /// </summary>
        long Unit { get; }

        /// <summary>
        /// 任务格数
        /// </summary>
        long SlotUnit { get; }

        /// <summary>
        /// 定时任务队列
        /// </summary>
        LinkedList<ITimerExecutor> Timers { get; }

        /// <summary>
        /// 缓存100ms中 添加列表
        /// </summary>
        List<ITimerExecutor> TimersCache { get; }

        /// <summary>
        /// 计算当前瞬间 定时器全部数量
        /// </summary>
        int AllCount { get; internal set; }

        /// <summary>
        /// 当前执行器 一次性最多缓存个数
        /// </summary>
        int MaxCount { get; }

        /// <summary>
        /// 当前任务格数
        /// </summary>
        long Slot { get; }

        string ToString();

        /// <summary>
        /// 添加任务执行器 进入数据源
        /// </summary>
        void AddTimerSource(List<ITimerExecutor> timer);

        /// <summary>
        /// 添加任务执行器 进入数据源
        /// </summary>
        void AddTimerSource(ITimerExecutor timer);

        /// <summary>
        /// 添加任务执行器 进入缓存
        /// </summary>
        void AddTimerCache(ITimerExecutor timer);

        /// <summary>
        /// 添加任务执行器 进入缓存
        /// </summary> 
        void AddTimerCache(params ITimerExecutor[] timer);

        /// <summary>
        /// 更新周期性收集的数据 加入链表
        /// </summary>
        void TimersUpdate();

        /// <summary>
        /// 底部更新
        /// </summary>
        int BottomUpdate(long nowTime);

        /// <summary>
        /// 其他更新
        /// </summary>
        void OtherUpdate(long nowTime);

        /// <summary>
        /// 更新格子数 时间更新
        /// </summary>
        void SlotUpdate(long UpdateSlot = 1);

        /// <summary>
        /// 更新格子数 时间更新
        /// </summary>
        void SlotResest();

        /// <summary>
        /// 数据推送
        /// </summary>
        void ReceiveFromData(ITimerOperator source);
    }
}