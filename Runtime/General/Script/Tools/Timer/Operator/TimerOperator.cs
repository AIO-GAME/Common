/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-07
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Text;
using APool = Pool;

namespace UnityEngine
{
    /// <summary>
    /// 消耗型定时器
    /// </summary>
    internal abstract class TimerOperator : ITimerOperator
    {
        public byte Index { get; private set; }

        public long Unit { get; private set; }

        public long SlotUnit { get; private set; }

        public LinkedList<ITimerExecutor> Timers { get; private set; }

        public List<ITimerExecutor> TimersCache { get; private set; }

        /// <summary>
        /// 计算当前瞬间 定时器全部数量
        /// </summary>
        public int AllCount { get; internal set; }

        int ITimerOperator.AllCount
        {
            get => AllCount;
            set => AllCount = value;
        }

        public int MaxCount { get; private set; }

        public long Slot { get; private set; }

        protected TimerOperator()
        {
        }

        protected TimerOperator(byte index, long unit, long slotUnit, int maxCount = 2048)
        {
            TimersCache = APool.List<ITimerExecutor>();
            Timers = APool.LinkedList<ITimerExecutor>();
            Index = index;
            Unit = unit * slotUnit;
            SlotUnit = slotUnit;
            MaxCount = maxCount;
            Slot = 0;
        }

        public void Dispose()
        {
            TimersCache.Free();
            Timers.Free();
        }

        public sealed override string ToString()
        {
            var @string = new StringBuilder();
            @string.Clear();
            @string.Append("当前毫秒:").Append(TimerSystem.Counter).Append("ms").AppendLine();
            @string.Append("定时器序号:").Append(Index).AppendLine();
            @string.Append("计时单位:").Append(Unit).Append("ms").AppendLine();
            @string.Append("当前时间:").Append(Slot).AppendLine();
            @string.Append("队列数量:").Append(Timers.Count).AppendLine();
            @string.Append("队列信息:").AppendLine();
            foreach (var item in Timers)
            {
                @string.Append("[").Append(' ');
                @string.Append("Count  =").Append(item.Duration).Append("ms").Append(' ');
                @string.Append("Create =").Append(item.CreateTime).Append("ms").Append(' ');
                @string.Append("End    =").Append(item.EndTime).Append("ms").Append(' ');
                @string.Append("]").AppendLine();
            }

            return @string.ToString();
        }

        public void AddTimerSource(List<ITimerExecutor> timer)
        {
            if (timer.Count == 0) return;
            AllCount += timer.Count;
            TimersUpdate(timer);
        }

        public void AddTimerSource(ITimerExecutor timer)
        {
            AllCount += 1;
            TimersUpdate(timer);
        }

        /// <summary>
        /// 添加任务执行器 进入缓存
        /// </summary>
        public void AddTimerCache(ITimerExecutor timer)
        {
            lock (TimersCache)
            {
                TimersCache.Add(timer);

                if (TimersCache.Count >= MaxCount)
                {
                    //如果里当前数量接近容量值 则立马添加到Timers中
                    TimersUpdate(TimersCache);
                    TimersCache = APool.List<ITimerExecutor>();
                }
            }

            AllCount += 1;
        }

        public void AddTimerCache(params ITimerExecutor[] timer)
        {
            if (timer.Length == 0) return;
            lock (TimersCache)
            {
                for (var i = 0; i < timer.Length; i++) TimersCache.Add(timer[i]);
                if (TimersCache.Count >= MaxCount)
                {
                    TimersUpdate(TimersCache);
                    TimersCache = APool.List<ITimerExecutor>();
                }
            }

            AllCount += timer.Length;
        }

        public void SlotUpdate(long UpdateSlot = 1)
        {
            Slot += UpdateSlot;
        }

        public void SlotResest()
        {
            Slot = 0;
        }

        public void ReceiveFromData(ITimerOperator source)
        {
            Index = source.Index;
            Slot = source.Slot;
            SlotUnit = source.SlotUnit;
            AllCount = source.AllCount;
            MaxCount = source.MaxCount;
            Unit = source.Unit;

            TimersCache = source.TimersCache;
            Timers = source.Timers;
        }

        public void TimersUpdate()
        {
            if (TimersCache.Count == 0) return;
            lock (TimersCache)
            {
                TimersUpdate(TimersCache);
                TimersCache = APool.List<ITimerExecutor>();
            }
        }

        /// <summary>
        /// 更新链表
        /// </summary>
        protected virtual void TimersUpdate(ITimerExecutor executor)
        {
            //当前为单项遍历 只遍历一次 准备优化为 双向遍历 只遍历一次
            lock (Timers)
            {
                var nowNode = Timers.First;
                while (true)
                {
                    if (nowNode == null)
                    {
                        Timers.AddLast(executor);
                        return;
                    }

                    if (executor.EndTime < nowNode.Value.EndTime)
                    {
                        Timers.AddBefore(nowNode, executor);
                        return;
                    }

                    nowNode = nowNode.Next;
                }
            }
        }

        /// <summary>
        /// 更新链表
        /// </summary>
        protected virtual void TimersUpdate(List<ITimerExecutor> executors)
        {
            try
            {
                executors.Sort((a, b) => a.EndTime < b.EndTime ? -1 : 1);

                //当前为单项遍历 只遍历一次 准备优化为 双向遍历 只遍历一次
                lock (Timers)
                {
                    var nowNode = Timers.First;
                    var lindex = 0;
                    while (true)
                    {
                        if (executors.Count <= lindex) break;

                        if (nowNode == null)
                        {
                            //1 如果当前节点 值为Null 说明整条节点没有合适的了 所以可以直接赋值 退出循环
                            while (executors.Count > lindex) Timers.AddLast(executors[lindex++]);
                            break; //因为当前已经遍历到最后一个了 所以可以直接让之后的数据 依次接入链表
                        }

                        if (executors[lindex].EndTime < nowNode.Value.EndTime)
                        {
                            //2 如果待加入 结束时间 < 当前结束时间 那么说明 可以直接赋值 退出循环
                            nowNode = Timers.AddBefore(nowNode, executors[lindex++]);
                            continue;
                        }

                        nowNode = nowNode.Next;
                    }
                }
            }
            catch (Exception e)
            {
                Print.Exception(e);
            }
            finally
            {
                executors.Free();
            }
        }

        public abstract int BottomUpdate(long nowTime);

        public abstract void OtherUpdate(long nowTime);
    }
}