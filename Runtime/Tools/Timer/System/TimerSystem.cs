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
using System.Threading;
using System.Threading.Tasks;
using APool = Pool;

namespace AIO
{
    using Unitx = Unit;

    /// <summary>
    /// 定时器单位回调
    /// </summary>
    public delegate void TimerUnitsTask(List<(long, long, long)> units);

    /// <summary>
    /// 定时器 时间调度器
    /// </summary>
    public static partial class TimerSystem
    {
        public static void Initialize(long updatelisttime = 10, int capacity = 1024 * 8)
        {
            Watch = Stopwatch.StartNew();

            //保持当前计算单位是毫秒 因为当前时间单位计算底层是纳秒
            Unit = Unitx.Time.SECOND * 2;

            TaskList = APool.List<ITimerContainer>();
            MainList = APool.List<ITimerOperator>();
            TimingUnits = APool.List<(long, long, long)>();
            TimerExecutors = APool.Dictionary<long, ITimerExecutor>();

            if (TimingUnitsEvent is null)
            {
                TimingUnits.Add((Unitx.Time.MS_SECOND, Unit, Unitx.Time.MS_SECOND / Unit));
                TimingUnits.Add((Unitx.Time.MS_MIN, Unitx.Time.MS_SECOND, 60));
                TimingUnits.Add((Unitx.Time.MS_HOUR, Unitx.Time.MS_MIN, 60));
                TimingUnits.Add((Unitx.Time.MS_DAY, Unitx.Time.MS_HOUR, 24));
                TimingUnits.Add((Unitx.Time.MS_WEEK, Unitx.Time.MS_DAY, 7));
            }
            else TimingUnitsEvent.Invoke(TimingUnits);

            RemainNum = 0;
            UpdateCacheTime = 0;
            Capacity = capacity;
            UPDATELISTTIME = updatelisttime;

            RegisterList();

            SWITCH = true;

            TaskHandleTokenSource = new CancellationTokenSource();
            TaskHandleToken = TaskHandleTokenSource.Token;
            ThreadHandle = Task.Factory.StartNew(Update, TaskHandleToken);

            if (Settings.EnableLoopThread)
            {
                LoopContainer = new TimerContainerLoop(Unit);
                LoopContainer.Start();
            }
        }

        static partial void Update();

        /// <summary>
        /// 注册主队列
        /// </summary>
        private static void RegisterList()
        {
            MainList.Clear();
            for (byte i = 0; i < TimingUnits.Count; i++)
            {
                MainList.Add(new TimerOperatorAuto(i, TimingUnits[i].Item2, TimingUnits[i].Item3));
            }
        }

        /// <summary>
        /// 释放辅助定时器
        /// </summary>
        internal static void DisposeContainer(ITimerContainer container)
        {
            if (SWITCH) TaskList.Remove(container);
        }

        public static void Dispose()
        {
            ToString();
            SWITCH = false;
            TaskList.Free();
            MainList.Free();
            TimingUnits.Free();
            TimerExecutors.Free();

            try
            {
                lock (LoopContainer)
                {
                    LoopContainer.Cancel();
                    LoopContainer = null;
                }

                lock (TaskList)
                {
                    for (var i = 0; i < TaskList.Count; i++) TaskList[i].Cancel();
                    TaskList.Clear();
                    TaskList = null;
                }

                lock (MainList)
                {
                    for (var i = 0; i < MainList.Count; i++) MainList[i].Dispose();
                    MainList.Clear();
                    MainList = null;
                }

                if (!ThreadHandle.IsCompleted)
                {
                    TaskHandleTokenSource.Cancel(true);
                }
                else ThreadHandle.Dispose();

                Watch = null;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
            finally
            {
                UnityEngine.Debug.Log("定时器系统已销毁");
            }
        }
    }
}