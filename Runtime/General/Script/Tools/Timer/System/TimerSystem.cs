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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using APool = Pool;
using Unitx = Unit;

namespace UnityEngine
{
    public enum ETimerUnit
    {
        Second,
        Min,
        Hour,
        Day,
        Week,
        Month,
        Year,
    }

    /// <summary>
    /// 定时器 时间调度器
    /// </summary>
    public static partial class TimerSystem
    {
        public static void Initialize(long updatelisttime = 10, int capacity = 1024 * 8)
        {
            RemainNum = 0;
            UpdateCacheTime = 0;
            Capacity = capacity;
            UPDATELISTTIME = updatelisttime;

            Watch = Stopwatch.StartNew();

            TaskList = APool.List<ITimerContainer>();
            MainList = APool.List<ITimerOperator>();
            TimerUnits = APool.List<long>();

            RegisterList();
            SWITCH = true;

            TaskHandleTokenSource = new CancellationTokenSource();
            TaskHandleToken = TaskHandleTokenSource.Token;
            ThreadHandle = Task.Factory.StartNew(Update, TaskHandleToken);
        }

        static partial void Update();

        /// <summary>
        /// 注册主队列
        /// </summary>
        private static void RegisterList()
        {
            //保持当前计算单位是毫秒 因为当前时间单位计算底层是纳秒
            MainList.Clear();
            Unit = Unitx.Time.SECOND * 2;
            MainList.Add(new TimerOperatorAuto(0, Unit, Unitx.Time.MS_SECOND / Unit)); //01秒
            MainList.Add(new TimerOperatorAuto(1, Unitx.Time.MS_SECOND, 60)); //60秒
            MainList.Add(new TimerOperatorAuto(2, Unitx.Time.MS_MIN, 60)); //60分
            MainList.Add(new TimerOperatorAuto(3, Unitx.Time.MS_HOUR, 24)); //24时
            MainList.Add(new TimerOperatorAuto(4, Unitx.Time.MS_DAY, 7)); //07天

            TimerUnits.Clear();
            for (var i = 0; i < MainList.Count; i++) TimerUnits.Add(MainList[i].Unit);
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
            try
            {
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
                Print.Exception(e);
            }
            finally
            {
                Print.Log("定时器系统已销毁");
            }
        }
    }
}