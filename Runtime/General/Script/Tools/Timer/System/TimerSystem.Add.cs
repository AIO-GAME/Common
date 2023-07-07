/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-06
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
    public static partial class TimerSystem
    {
        #region Timer Executor Enumerator

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void Add(long duration, Func<IEnumerator> delegateValue)
        {
            AddUpdate(new TimerExecutorEnumerator(duration, 1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Add(long duration, ushort loop, Func<IEnumerator> delegateValue)
        {
            AddUpdate(new TimerExecutorEnumerator(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Add(long duration, byte loop, Func<IEnumerator> delegateValue)
        {
            AddUpdate(new TimerExecutorEnumerator(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        #endregion

        #region Timer Executor Action

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void Add(long duration, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(duration, 1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Add(long duration, ushort loop, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Add(long duration, byte loop, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        #endregion

        #region Add

        private static void AddUpdate(ITimerExecutor timer)
        {
            for (var i = 0; i < TimerUnits.Count - 1; i++)
            {
                //说明 当前 时间分级器单位 与 当前定时任务处理器 匹配
                //当I等于最后一个分级层数时
                if (timer.Duration <= TimerUnits[i] || i == TimerUnits.Count - 1)
                {
                    if (i == 0) timer.OperatorIndex = 0;
                    else timer.OperatorIndex = (byte)(i - 1);
                    break;
                }
            }

            if (RemainNum >= Capacity)
            {
                lock (Watch)
                {
                    //如果大于容量 则单独开一个线程 清空当前主函数中 所有列表
                    var v = new TimerContainerTask(Unit, Counter + Watch.ElapsedMilliseconds, MainList);
                    RegisterList();
                    RemainNum = RemainNum - Capacity; //因为线程计算问题 只能定时到一瞬间 因为数据的转移方式是全部转移 所以这里直接减去容量
                    lock (TaskList) TaskList.Add(v);
                }
            }

            Add(timer);
            RemainNum = RemainNum + 1;
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        internal static void Add(ITimerExecutor timer)
        {
            lock (MainList)
            {
                switch (timer.OperatorIndex)
                {
                    case 0:
                        MainList[0].AddTimerSource(timer);
                        break;
                    default:
                        MainList[timer.OperatorIndex].AddTimerCache(timer);
                        break;
                }
            }
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        internal static void AddLoop(List<ITimerExecutor> timers)
        {
            lock (MainList)
            {
                for (var i = 0; i < timers.Count; i++)
                {
                    switch (timers[i].OperatorIndex)
                    {
                        case 0:
                            MainList[0].AddTimerSource(timers[i]);
                            break;
                        default:
                            MainList[timers[i].OperatorIndex].AddTimerCache(timers[i]);
                            break;
                    }
                }

                timers.Free();
            }
        }

        #endregion

        /// <summary>
        /// 加入指定分层定时器
        /// 加入时 默认TimerExe参数正确
        /// </summary>
        /// <param name="index">定时器层级</param>
        /// <param name="timer">执行器</param>
        internal static void UpdateSlot(int index, ITimerExecutor timer)
        {
            lock (MainList) MainList[index].AddTimerSource(timer);
        }

        /// <summary>
        /// 加入指定分层定时器
        /// 加入时 默认TimerExe参数正确
        /// </summary>
        /// <param name="index">定时器层级</param>
        /// <param name="timer">执行器</param>
        internal static void UpdateSlot(int index, List<ITimerExecutor> timer)
        {
            lock (MainList) MainList[index].AddTimerSource(timer);
        }
    }
}