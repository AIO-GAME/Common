/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           1.0                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-12-02                    *
* Nowtime:           15:08:30                      *
* Description:                                     *
* History:                                         *
***************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using APool = Pool;

namespace UnityEngine
{
    /// <summary>
    /// 辅助定时器
    /// </summary>
    internal partial class TimerContainerTask : TimerContainer
    {
        private Task TaskHandle;

        private CancellationToken TaskHandleToken;

        private CancellationTokenSource TaskHandleTokenSource;

        public TimerContainerTask(long unit, long counter, List<ITimerOperator> operators)
        {
            Unit = unit;
            Counter = counter;

            var isSort = true;
            lock (operators)
            {
                for (var i = operators.Count - 1; i >= 0; i--)
                {
                    if (isSort && operators[i].AllCount <= 0) continue;
                    isSort = false;
                    var op = new TimerOperatorConsume(operators[i], DoneCallBack, LoopCallBack, EvolutionCallBack);
                    RemainNum += op.AllCount; //计算当前定时器数量
                    List.Add(op);
                }
            }


            List.Sort((a, b) =>
            {
                if (a.Index < b.Index) return -1;
                if (a.Index > b.Index) return 1;
                return 0;
            });

            TotalNum = RemainNum;
        }

        protected override void Update()
        {
            TaskHandleToken.ThrowIfCancellationRequested();

            foreach (var operators in List) operators.TimersUpdate(); //更新缓存与执行容器

            long nowMilliseconds;
            try
            {
                while (RemainNum > 0)
                {
                    nowMilliseconds = Watch.ElapsedMilliseconds;

                    if (nowMilliseconds >= Unit) // 更新间隔
                    {
                        Watch.Restart();

                        Counter += nowMilliseconds;
                        UpdateCacheTime += nowMilliseconds;
                        if (UpdateCacheTime > TimerSystem.UPDATELISTTIME)
                        {
                            UpdateCacheTime = 0; // 重置缓存更新时间
                            for (var i = 0; i < List.Count; i++)
                            {
                                List[i].TimersUpdate();
                            }
                        }

                        List[0].SlotUpdate(Unit);
                        RemainNum = RemainNum - List[0].BottomUpdate(Counter);
                        if (RemainNum <= 0)
                        {
                            RemainNum = 0; //重新计算剩余数量 保证异步线程修改 出现数据丢失
                            foreach (var item in List) RemainNum = RemainNum + item.AllCount;
                            if (RemainNum <= 0) break;
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
#if UNITY_EDITOR
                Print.Log($"[辅助定时器:{ID}] [容器数量:{List.Count}] [状态:结束] 精度单位:{Unit} 当前时间:{Counter} 任务总数量:{TotalNum} 完成任务数量:{TotalNum - RemainNum}");
#endif
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Print.ErrorFormat($"[辅助定时器:{ID}] [容器数量:{List.Count}] [状态:异常] 精度单位:{Unit} 当前时间:{Counter} 任务总数量:{TotalNum} 完成任务数量:{TotalNum - RemainNum} 异常信息:{e}");
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
                lock (list)
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        list[i].Execute();
                    }
                }

                list.Free();
            });
        }

        private void LoopCallBack(List<ITimerExecutor> list)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var executor = list[i];
                if (executor.OperatorIndex < List.Count)
                {
                    switch (executor.OperatorIndex)
                    {
                        case 0:
                            List[0].AddTimerSource(executor);
                            break;
                        default:
                            List[executor.OperatorIndex].AddTimerCache(executor);
                            break;
                    }

                    list.RemoveAt(i);
                }
            }

            if (list.Count > 0) TimerSystem.AddLoop(list);
            else list.Free();
        }

        private void EvolutionCallBack(int Index, List<ITimerExecutor> list)
        {
            List[Index].AddTimerSource(list);
        }
    }
}