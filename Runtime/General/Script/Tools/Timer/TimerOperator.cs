/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           1.0                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-12-02                    *
* Nowtime:           15:05:05                      *
* Description:                                     *
* History:                                         *
***************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine
{
    /// <summary>
    /// 时间分层定时器
    /// </summary>
    public class TimerOperator : IDisposable
    {
        /// <summary>
        /// 时间层级序列
        /// </summary>
        internal byte Index { get; }

        /// <summary>
        /// 当前定时器计时总单位
        /// </summary>
        internal long Unit { get; }

        /// <summary>
        /// 任务格数
        /// </summary>
        internal long SlotUnit { get; }

        /// <summary>
        /// 定时任务队列
        /// </summary>
        internal LinkedList<TimerExe> Timers { get; }

        /// <summary>
        /// 缓存100ms中 添加列表
        /// </summary>
        internal List<TimerExe> TimersCache { get; }

        /// <summary>
        /// 计算当前瞬间 定时器全部数量
        /// </summary>
        internal int AllCount { get; private set; }

        /// <summary>
        /// 当前执行器 一次性最多缓存个数
        /// </summary>
        internal int MaxCount { get; }

        /// <summary>
        /// 当前任务格数
        /// </summary>
        internal long Slot { get; private set; }

        internal TimerOperator(byte index, long unit, long slotUnit, int maxCount = 2048)
        {
            TimersCache = Pool.List<TimerExe>();
            Timers = Pool.LinkedList<TimerExe>();
            Index = index;
            Unit = unit * slotUnit;
            SlotUnit = slotUnit;
            MaxCount = maxCount;
            Slot = 0;
        }

        /// <summary>
        /// 更新格子数 时间更新
        /// </summary>
        internal void SlotUpdate(long UpdateSlot = 1)
        {
            Slot += UpdateSlot;
        }

        /// <summary>
        /// 更新格子数 时间更新
        /// </summary>
        internal void SlotResest()
        {
            Slot = 0;
        }

        /// <summary>
        /// 添加任务执行器
        /// </summary>
        internal void Add(TimerExe timer)
        {
            lock (TimersCache)
            {
                TimersCache.Add(timer);
                if (TimersCache.Count >= MaxCount)
                {
                    //如果里当前数量接近容量值 则立马添加到Timers中
                    TimersUpdate(TimersCache.ToArray());
                    TimersCache.Clear();
                }
            }

            AllCount++;
        }

        /// <summary>
        /// 添加任务执行器
        /// </summary>
        internal void Add(params TimerExe[] timer)
        {
            lock (TimersCache)
            {
                //因为缓存列表中 存在异步删减 所以需要加锁
                //foreach (var item in timer)
                //{
                //    TimersCache.Add(item);
                //}
                TimersCache.AddRange(timer);
                if (TimersCache.Count >= MaxCount)
                {
                    //如果里当前数量接近容量值 则立马添加到Timers中
                    TimersUpdate(TimersCache.ToArray());
                    TimersCache.Clear();
                }
            }

            AllCount += timer.Length;
        }

        /// <summary>
        /// 更新周期性收集的数据 加入链表
        /// </summary>
        internal void TimersUpdate()
        {
            lock (TimersCache)
            {
                TimersUpdate(TimersCache.ToArray());
                TimersCache.Clear();
            }
        }

        /// <summary>
        /// 更新链表
        /// </summary>
        private void TimersUpdate(params TimerExe[] exes)
        {
            try
            {
                //await Task.Factory.StartNew(() =>
                //{
                //    lock (Timers)
                {
                    //当前为单项遍历 只遍历一次 准备优化为 双向遍历 只遍历一次
                    var nowNode = Timers.First;
                    var lindex = 0;
                    while (true)
                    {
                        if (exes.Length <= lindex) return;

                        if (nowNode == null)
                        {
                            //1 如果当前节点 值为Null 说明整条节点没有合适的了 所以可以直接赋值 退出循环
                            while (exes.Length > lindex)
                            {
                                Timers.AddLast(exes[lindex++]);
                            }

                            return; //因为当前已经遍历到最后一个了 所以可以直接让之后的数据 依次接入链表
                        }

                        if (exes[lindex].EndTime < nowNode.Value.EndTime)
                        {
                            //2 如果待加入 结束时间 < 当前结束时间 那么说明 可以直接赋值 退出循环
                            nowNode = Timers.AddAfter(nowNode, exes[lindex++]);
                            continue;
                        }

                        nowNode = nowNode.Next;
                    }

                    //Timers.TryDequeue(out var nowNode); int lindex = 0;
                    //while (true)
                    //{
                    //    if (exes.Length <= lindex) return;

                    //    if (nowNode == null)
                    //    {   //1 如果当前节点 值为Null 说明整条节点没有合适的了 所以可以直接赋值 退出循环
                    //        while (exes.Length > lindex)
                    //        {
                    //            Timers.Enqueue(exes[lindex++]);
                    //        }
                    //        return;//因为当前已经遍历到最后一个了 所以可以直接让之后的数据 依次接入链表
                    //    }

                    //    if (exes[lindex].EndTime < nowNode.EndTime)
                    //    {   //2 如果待加入 结束时间 < 当前结束时间 那么说明 可以直接赋值 退出循环
                    //        Timers.Enqueue( exes[lindex++]);
                    //        continue;
                    //    }
                    //    Timers.TryDequeue(out nowNode);
                    //}
                }
                //});
            }
            catch (Exception e)
            {
                Print.Exception(e);
            }
        }

        private TimerExe TimerExe;

        /// <summary>
        /// 时间更新 事件执行
        /// </summary>
        internal int Update(long NowTime)
        {
            var FinshNum = 0;
            lock (Timers)
            {
                while (Timers.First != null) //判断当前需要移除哪些任务
                {
                    TimerExe = Timers.First.Value;
                    if (Timers.Count == 0 || TimerExe == null) return FinshNum;
                    if (Index > 0)
                    {
                        if (TimerExe.EndTime - NowTime < Unit)
                        {
                            //如果发现当前剩余时间不满足当前层级时间总量 则移动到下一次层级
                            TimerSystem.Add(Index - 1, TimerExe);
                            Timers.RemoveFirst();
                            AllCount--;
                            continue;
                        }
                    }
                    else if (TimerExe.EndTime <= NowTime)
                    {
                        //只有当Index为0的时候 才会出发此条件
                        if (Timers.First.Value.UpdateLoop())
                        {
                            //如果可以循环 则再次加入队列
                            TimerExe.UpdateData(NowTime);
                            TimerSystem.Add(TimerExe);
                        }

                        TimerExe.Execute();
                        Timers.RemoveFirst();
                        AllCount--;
                        FinshNum++;
                        continue;
                    }

                    break;
                }
            }

            return FinshNum;
        }

        /// <summary>
        /// 克隆基础数据 无法拷贝链表 List缓存数据 请手动赋值
        /// </summary>
        internal TimerOperator Clone()
        {
            var c = new TimerOperator(Index, Unit, SlotUnit, MaxCount);
            c.Slot = Slot;
            return c;
        }

        public override string ToString()
        {
            var @string = new StringBuilder();
            @string.Clear();
            @string.Append("当前毫秒:").Append(TimerSystem.Counter).Append("ms").AppendLine();
            @string.Append("定时器序号:").Append(Index).AppendLine();
            @string.Append("计时单位:").Append(Unit).AppendLine();
            @string.Append("当前时间:").Append(Slot).AppendLine();
            @string.Append("队列数量:").Append(Timers.Count).AppendLine();
            @string.Append("队列信息:").AppendLine();
            foreach (var item in Timers)
            {
                @string.Append("[").Append(' ');
                @string.Append("Count  =").Append(item.Count).Append("ms").Append(' ');
                @string.Append("Create =").Append(item.CreateTime).Append("ms").Append(' ');
                @string.Append("End    =").Append(item.EndTime).Append("ms").Append(' ');
                @string.Append("]").AppendLine();
            }

            return @string.ToString();
        }

        public void Dispose()
        {
            TimersCache.Free();
            Timers.Free();
        }
    }
}