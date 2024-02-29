using System.Threading.Tasks;

namespace AIO
{
    public class TimerOperatorAuto : TimerOperator
    {
        public TimerOperatorAuto()
        {
        }

        public TimerOperatorAuto(byte index, long unit, long slotUnit, int maxCount = 2048) : base(index, unit,
            slotUnit, maxCount)
        {
        }

        public override int BottomUpdate(long nowTime)
        {
            var DoneList = Pool.List<ITimerExecutor>(); // 用于存储已经完成的任务
            var LoopList = Pool.List<ITimerExecutor>(); // 用于存储需要循环的任务

            var FinshNumber = 0;
            lock (Timers)
            {
                while (Timers.Count > 0 && Timers.First != null) //判断当前需要移除哪些任务
                {
                    var executor = Timers.First.Value;
                    if (executor.EndTime <= nowTime)
                    {
                        //只有当Index为0的时候 才会出发此条件 并且再次加入队列
                        if (executor.UpdateLoop()) LoopList.Add(executor);
                        else FinshNumber++;

                        DoneList.Add(executor);
                        Timers.RemoveFirst();
                    }
                    else break;
                }
            }

            AllCount = AllCount - LoopList.Count - DoneList.Count;
            if (LoopList.Count > 0) TimerSystem.AddLoop(LoopList);
            else LoopList.Free();

            if (DoneList.Count > 0)
            {
                Task.Factory.StartNew(() =>
                {
                    for (var i = 0; i < DoneList.Count; i++) DoneList[i].Execute();
                    DoneList.Free();
                });
            }
            else DoneList.Free();

            return FinshNumber;
        }

        public override void OtherUpdate(long nowTime)
        {
            var EvolutionList = Pool.List<ITimerExecutor>(); // 用于存储已经完成的任务
            lock (Timers)
            {
                while (Timers.Count > 0 && Timers.First != null) //判断当前需要移除哪些任务
                {
                    var executor = Timers.First.Value;
                    if (executor.EndTime - nowTime < Unit) //如果发现当前剩余时间不满足当前层级时间总量 则移动到下一次层级
                    {
                        EvolutionList.Add(executor);
                        Timers.RemoveFirst();
                    }
                    else break;
                }
            }

            if (EvolutionList.Count > 0)
            {
                AllCount = AllCount - EvolutionList.Count;
                TimerSystem.UpdateSlot(Index - 1, EvolutionList);
            }
            else EvolutionList.Free();
        }
    }
}