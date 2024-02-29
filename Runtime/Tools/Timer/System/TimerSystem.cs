using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AIO
{
    using UnitEx = Unit;

    /// <summary>
    /// 定时器单位回调
    /// </summary>
    public delegate void TimerUnitsTask(List<(long, long, long)> units);

    /// <summary>
    /// 定时器 时间调度器
    /// </summary>
    [IgnoreConsoleJump]
    public static partial class TimerSystem
    {
        /// <summary>
        /// 定时器单位回调
        /// </summary>
        /// <param name="updateLimit">更新限制</param>
        /// <param name="capacity">容量</param>
        public static void Initialize(long updateLimit = 10, int capacity = 1024 * 8)
        {
            Watch = Stopwatch.StartNew();

            //保持当前计算单位是毫秒 因为当前时间单位计算底层是纳秒
            Unit = UnitEx.Time.SECOND * 2;

            TaskList = Pool.List<ITimerContainer>();
            MainList = Pool.List<ITimerOperator>();
            TimingUnits = Pool.List<(long, long, long)>();
            TimerExecutors = Pool.Dictionary<long, ITimerExecutor>();

            if (TimingUnitsEvent is null)
            {
                TimingUnits.Add((UnitEx.Time.MS_SECOND, Unit, UnitEx.Time.MS_SECOND / Unit));
                TimingUnits.Add((UnitEx.Time.MS_MIN, UnitEx.Time.MS_SECOND, 60));
                TimingUnits.Add((UnitEx.Time.MS_HOUR, UnitEx.Time.MS_MIN, 60));
                TimingUnits.Add((UnitEx.Time.MS_DAY, UnitEx.Time.MS_HOUR, 24));
                TimingUnits.Add((UnitEx.Time.MS_WEEK, UnitEx.Time.MS_DAY, 7));
            }
            else TimingUnitsEvent.Invoke(TimingUnits);

            RemainNum = 0;
            UpdateCacheTime = 0;
            Capacity = capacity;
            UPDATELISTTIME = updateLimit;

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
                    foreach (var item in TaskList) item.Cancel();
                    TaskList.Clear();
                    TaskList = null;
                }

                lock (MainList)
                {
                    foreach (var item in MainList) item.Dispose();

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