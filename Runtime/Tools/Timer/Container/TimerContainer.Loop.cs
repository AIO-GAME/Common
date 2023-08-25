/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-07
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using APool = Pool;
using UnityEngine;

namespace AIO
{
    /// <summary>
    /// 定时器 时间调度器 循环
    /// </summary>
    public class TimerContainerLoop : TimerContainer
    {
        public TimerContainerLoop(long unit)
        {
            Unit = unit;

            for (byte i = 0; i < TimerSystem.TimingUnits.Count; i++)
            {
                List.Add(new TimerOperatorLoop(
                    i,
                    TimerSystem.TimingUnits[i].Item2,
                    TimerSystem.TimingUnits[i].Item3,
                    DoneCallBack,
                    PushUpdate,
                    EvolutionCallBack
                ));
            }
        }

        protected override void Update()
        {
            TaskHandleToken.ThrowIfCancellationRequested();
            long nowMilliseconds;
            Counter = TimerSystem.Counter;
            try
            {
                while (TimerSystem.SWITCH)
                {
                    nowMilliseconds = Watch.ElapsedMilliseconds;
                    if (nowMilliseconds >= Unit) //更新间隔
                    {
                        Watch.Restart();
                        Counter += nowMilliseconds;
                        UpdateCacheTime += nowMilliseconds;

                        if (UpdateCacheTime > TimerSystem.UPDATELISTTIME)
                        {
                            UpdateCacheTime = 0; // 重置缓存更新时间
                            for (var i = 0; i < List.Count; i++) List[i].TimersUpdate();
                        }

                        List[0].SlotUpdate(Unit);
                        if (RemainNum <= 0)
                        {
                            if (List[0].Slot >= List[0].SlotUnit)
                            {
                                List[0].SlotResest();
                                for (var i = 1; i < List.Count; i++)
                                {
                                    List[i].SlotUpdate(List[i - 1].Unit);
                                    if (List[i].Slot >= List[i].SlotUnit)
                                    {
                                        List[i].OtherUpdate(Counter);
                                        List[i].SlotResest();
                                    }
                                    else break;
                                }
                            }
                        }
                        else
                        {
                            RemainNum = RemainNum - List[0].BottomUpdate(Counter);
                            if (RemainNum <= 0)
                            {
                                RemainNum = 0; //重新计算剩余数量 保证异步线程修改 出现数据丢失
                                foreach (var item in List) RemainNum = RemainNum + item.AllCount;
                            }

                            if (List[0].Slot >= List[0].SlotUnit)
                            {
                                List[0].SlotResest();
                                for (var i = 1; i < List.Count; i++)
                                {
                                    List[i].SlotUpdate(List[i - 1].Unit);
                                    if (List[i].Slot >= List[i].SlotUnit)
                                    {
                                        List[i].OtherUpdate(Counter);
                                        List[i].SlotResest();
                                    }
                                    else break;
                                }
                            }
                        }
                    }
                }


#if UNITY_EDITOR
                UnityEngine.Debug.Log($"[循环定时器:{ID}] [容器数量:{List.Count}] [状态:结束] 精度单位:{Unit} 当前时间:{Counter} 剩余任务数量:{RemainNum}");
#endif
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogErrorFormat($"[循环定时器:{ID}] [容器数量:{List.Count}] [状态:异常] 精度单位:{Unit} 当前时间:{Counter} 剩余任务数量:{RemainNum} 异常信息:{e}");
#endif
            }
            finally
            {
                Dispose();
            }
        }

        private void DoneCallBack(List<ITimerExecutor> list)
        {
            UnityAsync.RunTask(() =>
            {
                for (var i = 0; i < list.Count; i++)
                {
                    list[i].Execute();
                }

                list.Free();
            });
        }

        private void EvolutionCallBack(int Index, List<ITimerExecutor> list)
        {
            List[Index].AddTimerSource(list);
        }
    }
}
