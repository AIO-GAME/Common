﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-07
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using APool = Pool;
using UnityEngine;

namespace AIO
{
    public abstract partial class TimerContainer : ITimerContainer
    {
        private static int NUM;

        public ITimerOperator this[int index]
        {
            get => List[index];
        }

        public Stopwatch Watch { get; }

        public List<ITimerOperator> List { get; }

        public long Unit { get; protected set; }

        public long Counter { get; protected set; }

        public int RemainNum { get; protected set; }

        public int ID { get; }

        public long UpdateCacheTime { get; protected set; }

        private Task TaskHandle;

        protected CancellationToken TaskHandleToken { get; private set; }

        protected CancellationTokenSource TaskHandleTokenSource { get; private set; }

        protected TimerContainer()
        {
            Watch = Stopwatch.StartNew();
            List = APool.List<ITimerOperator>();
            ID = NUM++;
            Unit = 0;
            UpdateCacheTime = 0;
            RemainNum = 0;
        }

        public void Start()
        {
            if (List.Count <= 0)
            {
                UnityEngine.Debug.LogErrorFormat("TimerContainer.Start() -> 容器中没有操作器, 无法启动! [ID:{0}] [TYPE:{1}]", ID, GetType().FullName);
                return;
            }

            TaskHandleTokenSource = new CancellationTokenSource();
            TaskHandleToken = TaskHandleTokenSource.Token;
            TaskHandleToken.Register(Dispose);
            TaskHandle = Task.Factory.StartNew(Update, TaskHandleToken);
        }

        public void Cancel()
        {
            if (TaskHandle is null) return;
            if (!TaskHandle.IsCompleted) TaskHandleTokenSource?.Cancel(true);
            else TaskHandle.Dispose();
        }

        /// <summary>
        /// 更新
        /// </summary>
        protected abstract void Update();

        public virtual void Dispose()
        {
            if (TaskHandleTokenSource != null)
            {
                TaskHandleTokenSource.Dispose();
                TaskHandleTokenSource = null;
                TaskHandleToken = CancellationToken.None;
                TaskHandle = null;
            }

            if (List is null) return;
            for (var i = 0; i < List.Count; i++) List[i]?.Dispose();
            List.Free();
        }


        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"[{GetType().Name} ID:{ID}] [容器数量:{List.Count}] 精度单位:{Unit} 当前时间:{Counter} 剩余任务数量:{RemainNum}");
            foreach (var item in List) builder.AppendLine(item.ToString()).AppendLine();
            return builder.ToString();
        }

        public void PushUpdate(ITimerExecutor timer)
        {
            RemainNum += 1;
            lock (this)
            {
                switch (timer.OperatorIndex)
                {
                    case 0:
                        List[0].AddTimerSource(timer);
                        return;
                    default:
                        List[timer.OperatorIndex].AddTimerCache(timer);
                        return;
                }
            }
        }

        public void PushUpdate(List<ITimerExecutor> timer)
        {
            if (timer.Count == 0) return;
            RemainNum += timer.Count;
            lock (this)
            {
                for (var i = timer.Count - 1; i >= 0; i--)
                {
                    switch (timer[i].OperatorIndex)
                    {
                        case 0:
                            List[0].AddTimerSource(timer[i]);
                            break;
                        default:
                            List[timer[i].OperatorIndex].AddTimerCache(timer[i]);
                            break;
                    }

                    timer.RemoveAt(i);
                }
            }

            timer.Free();
        }

        public sealed override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
