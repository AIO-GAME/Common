/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-07
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using APool = Pool;

namespace UnityEngine
{
    internal abstract class TimerContainer : ITimerContainer
    {
        private static int NUM;

        public Stopwatch Watch { get; }

        public List<ITimerOperator> List { get; }

        public long Unit { get; protected set; }

        public long Counter { get; protected set; }

        public int RemainNum { get; protected set; }

        public int TotalNum { get; protected set; }

        public int ID { get; }

        public long UpdateCacheTime { get; protected set; }

        private Task TaskHandle;

        private CancellationToken TaskHandleToken;

        private CancellationTokenSource TaskHandleTokenSource;

        protected TimerContainer()
        {
            Watch = Stopwatch.StartNew();
            List = APool.List<ITimerOperator>();
            ID = NUM++;
            Unit = 0;
            UpdateCacheTime = 0;
            TotalNum = 0;

            TotalNum = RemainNum;
            TaskHandleTokenSource = new CancellationTokenSource();
            TaskHandleToken = TaskHandleTokenSource.Token;
            TaskHandleToken.Register(Dispose);
            TaskHandle = Task.Factory.StartNew(Update, TaskHandleToken);
        }

        public void Cancel()
        {
            if (!TaskHandle.IsCompleted) TaskHandleTokenSource.Cancel(true);
            else TaskHandle.Dispose();
        }

        /// <summary>
        /// 更新
        /// </summary>
        protected abstract void Update();

        public void Dispose()
        {
            TaskHandleTokenSource.Dispose();
            TaskHandleTokenSource = null;
            TaskHandleToken = CancellationToken.None;
            TaskHandle = null;
            TimerSystem.DisposeContainer(this);

            if (List is null) return;
            for (var i = 0; i < List.Count; i++) List[i]?.Dispose();
            List.Free();
        }


        public sealed override string ToString()
        {
            return $"[辅助定时器:{ID}] [容器数量:{List.Count}] 精度单位:{Unit} 当前时间:{Counter} 任务总数量:{TotalNum} 完成任务数量:{TotalNum - RemainNum}";
        }
    }
}