/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           1.0                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-11-28                    *
* Nowtime:           00:02:04                      *
* Description:                                     *
* History:                                         *
***************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityEngine
{
    using Unitx = Unit;

    /// <summary>
    /// 定时器 时间调度器
    /// </summary>
    public static class TimerSystem
    {
        /// <summary>
        /// 计时器 精确时间刻度器
        /// </summary>
        public static Stopwatch Watch { get; private set; }

        /// <summary>
        /// 多层级定时器 主 有添加接口 有消耗 只有一个
        /// </summary>
        public static List<TimerOperator> MainList { get; private set; }

        /// <summary>
        /// 多层级定时器 Task 副 没有添加接口 只有消耗 有多个
        /// </summary>
        public static List<TimerTaskContainer> TaskList { get; private set; }

        /// <summary>
        /// 当前计时器计算单位
        /// </summary>
        public static long Unit { get; private set; }

        /// <summary>
        /// 计数器 代表当前程序运行时间 单位ms
        /// </summary>
        public static long Counter { get; private set; }

        /// <summary>
        /// 当前容器列表剩余数量
        /// </summary>
        public static int RemainNum { get; private set; }

        /// <summary>
        /// 更新刷新List时间
        /// </summary>
        public static long UPDATELISTTIME { get; private set; } = 10;

        /// <summary>
        /// 容量
        /// </summary>
        public static int Capacity { get; private set; } = 1024 * 8;

        /// <summary>
        /// 更新刷新List时间
        /// </summary>
        public static long UpdateCacheTime { get; private set; } = 0;

        /// <summary>
        /// 开关
        /// </summary>
        public static bool SWITCH { get; private set; }

        public static Task ThreadHandle { get; private set; }

        public static void Initialize(long updatelisttime = 10, int capacity = 1024 * 8)
        {
            UPDATELISTTIME = updatelisttime;
            Capacity = capacity;

            Watch = Stopwatch.StartNew();
            TaskList = new List<TimerTaskContainer>(8);
            MainList = new List<TimerOperator>(8);
            RegisterList();
            SWITCH = true;
            var token = new CancellationToken();
            ThreadHandle = new Task(Update, token, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// 注册主队列
        /// </summary>
        private static void RegisterList()
        {
            //保持当前计算单位是毫秒 因为当前时间单位计算底层是纳秒
            MainList.Clear();
            Unit = Unitx.Time.SECOND * 2;
            MainList.Add(new TimerOperator(0, Unit, Unitx.Time.MS_SECOND / Unit)); //01秒
            MainList.Add(new TimerOperator(1, Unitx.Time.MS_SECOND, 60)); //60秒
            MainList.Add(new TimerOperator(2, Unitx.Time.MS_MIN, 60)); //60分
            MainList.Add(new TimerOperator(3, Unitx.Time.MS_HOUR, 24)); //24时
            MainList.Add(new TimerOperator(4, Unitx.Time.MS_DAY, 7)); //07天
        }

        /// <summary>
        /// 自旋 周期执行函数
        /// 时间驱动 从下往上 单位小的 达到条件 通知单位大的
        /// 层层通知 达到60秒通知1分钟 达到60分钟 通知1小时 达到24小时 通知1天 这样 可以分层 减轻最底层处理逻辑负担 提高计算效率
        ///
        /// 事件驱动 从上往下 单位事件大的检测 达到条件 移交给单位小一级的 层层移交 如果移交不下去了 则判定为过期任务 需要执行销毁
        /// </summary>
        internal static void Update()
        {
            long nowMilliseconds;
            int index;
            try
            {
                //Profiler.BeginSample("TimerSystem");//, thread.thread.Name);
                while (SWITCH)
                {
                    //如果当前List队列中 没有定时器任务 直接跳过
                    if (RemainNum == 0) continue;

                    if (UpdateCacheTime > UPDATELISTTIME)
                    {
                        UpdateCacheTime = 0;
                        foreach (var t in MainList.Where(t => t.TimersCache.Count > 0))
                        {
                            t.TimersUpdate();
                        }
                    }

                    nowMilliseconds = Watch.ElapsedMilliseconds;

                    if (nowMilliseconds >= Unit)
                    {
                        //更新间隔
                        Counter += nowMilliseconds;
                        UpdateCacheTime += nowMilliseconds;
                        Watch.Restart();
                        index = 0;
                        lock (MainList)
                        {
                            while (true)
                            {
                                if (MainList.Count <= index) break;
                                MainList[index].SlotUpdate(Unit);
                                if (index == 0)
                                {
                                    RemainNum -= MainList[index].Update(Counter);
                                    if (RemainNum <= 0)
                                    {
                                        //重新计算剩余数量 保证异步线程修改 出现数据丢失
                                        RemainNum = 0;
                                        foreach (var item in MainList) RemainNum += item.AllCount;
                                        if (RemainNum <= 0) break;
                                    }
                                }

                                if (MainList[index].Slot >= MainList[index].SlotUnit)
                                {
                                    foreach (var t in MainList)
                                    {
                                        t.Update(Counter);
                                        t.SlotResest();
                                    }

                                    index++;
                                }
                                else break;
                            }
                        }
                    }
                }

                //Profiler.EndSample();
            }
            catch (Exception e)
            {
                Print.Exception(e);
            }
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void Add(long Count, Action Callback)
        {
            Add(new TimerExe(Count, 0, Counter, Callback));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 -1循环 0当前 1循环次数
        /// </summary>
        public static void Add(long Count, int Loop, Action Callback)
        {
            Add(new TimerExe(Count, Loop, Counter, Callback));
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        internal static void Add(TimerExe Timer)
        {
            lock (MainList)
            {
                if (RemainNum >= Capacity)
                {
                    //如果大于容量 则单独开一个线程 清空当前主函数中 所有列表
                    var v = new TimerTaskContainer(Unit, Counter + Watch.ElapsedMilliseconds, MainList);
                    RegisterList();
                    RemainNum -= Capacity; //因为线程计算问题 只能定时到一瞬间 因为数据的转移方式是全部转移 所以这里直接减去容量
                    lock (TaskList)
                    {
                        TaskList.Add(v);
                    }
                }

                for (int i = 0; i < MainList.Count; i++)
                {
                    if (Timer.Count <= MainList[i].Unit || //说明 当前 时间分级器单位 与 当前定时任务处理器 匹配
                        i == MainList.Count - 1) //当I等于最后一个分级层数时
                    {
                        MainList[i].Add(Timer);
                        break;
                    }
                }

                RemainNum++;
            }
        }

        /// <summary>
        /// 加入指定分层定时器
        /// 加入时 默认TimerExe参数正确
        /// </summary>
        /// <param name="Index">定时器层级</param>
        /// <param name="timer">执行器</param>
        internal static void Add(int Index, TimerExe timer)
        {
            MainList[Index].Add(timer);
        }

        /// <summary>
        /// 获取定时器信息
        /// </summary>
        public new static void ToString()
        {
            var builder = new StringBuilder();
            builder.Append("-----------<主分层器>-----------").AppendLine();
            lock (MainList)
            {
                foreach (var item in MainList)
                {
                    builder.Append(item).AppendLine();
                }
            }

            builder.Append("-----------<辅分层器>-----------").AppendLine();
            lock (TaskList)
            {
                foreach (var item in TaskList)
                {
                    builder.Append(item).AppendLine();
                }
            }

            Print.Log(builder.ToString());
        }

        public static void Destory()
        {
            SWITCH = false;
            try
            {
                lock (TaskList)
                {
                    foreach (var item in TaskList)
                    {
                        item.Dispose();
                    }

                    TaskList.Clear();
                    TaskList = null;
                }


                lock (MainList)
                {
                    foreach (var item in MainList)
                    {
                        item.Dispose();
                    }

                    MainList.Clear();
                    MainList = null;
                }

                Watch = null;
            }
            catch (Exception e)
            {
                Print.Log(e);
            }
        }
    }
}