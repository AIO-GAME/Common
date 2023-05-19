// /***************************************************
// * Copyright(C) 2021 by DefaultCompany              *
// * All Rights Reserved By Author lihongliu.         *
// * Author:            XiNan                         *
// * Email:             1398581458@qq.com             *
// * Version:           1.0                           *
// * UnityVersion:      2020.3.12f1c1                 *
// * Date:              2021-12-02                    *
// * Nowtime:           15:08:30                      *
// * Description:                                     *
// * History:                                         *
// ***************************************************/
//
// namespace AIO
// {
//     using System;
//     using System.Collections.Generic;
//     using System.Diagnostics;
//
//     /// <summary>
//     /// 辅助定时器
//     /// </summary>
//     public class TimerTaskContainer : IDisposable
//     {
//         /// <summary>
//         /// 精度表
//         /// </summary>
//         public Stopwatch Watch { get; }
//
//         /// <summary>
//         /// 容器列表
//         /// </summary>
//         public List<TimerOperator> List { get; }
//
//         /// <summary>
//         /// 精度单位
//         /// </summary>
//         public long Unit { get; }
//
//         /// <summary>
//         /// 当前时间
//         /// </summary>
//         public long Counter { get; private set; }
//
//         /// <summary>
//         /// 剩余定时器任务数量
//         /// </summary>
//         public int RemainNum { get; private set; } = 0;
//
//         public TimerTaskContainer(long unit, long counter, List<TimerOperator> operators)
//         {
//             Unit = unit;
//             Counter = counter;
//             Watch = Stopwatch.StartNew();
//             List = new List<TimerOperator>(operators);
//
//             foreach (var item in operators)
//             {
//                 RemainNum += item.AllCount; //计算当前定时器数量
//                 item.TimersUpdate(); //更新缓存与执行容器
//             }
//
//             new ThreadCT($"{GetType().Name}-{Guid.NewGuid()}", Update, Execute, Exception).Start();
//         }
//
//         internal void Update()
//         {
//             try
//             {
//                 long nowMilliseconds;
//                 byte index;
//                 while (TimerSystem.SWITCH)
//                 {
//                     if (RemainNum == 0) break;
//
//                     nowMilliseconds = Watch.ElapsedMilliseconds;
//                     if (nowMilliseconds >= Unit)
//                     {
//                         // 更新间隔
//                         Counter += nowMilliseconds;
//                         Watch.Restart();
//                         index = 0;
//                         lock (List)
//                         {
//                             while (true)
//                             {
//                                 if (List.Count <= index) break;
//                                 List[index].SlotUpdate(Unit);
//                                 if (index == 0)
//                                 {
//                                     RemainNum -= List[index].Update(Counter);
//                                     if (RemainNum <= 0) break;
//                                 }
//
//                                 if (List[index].Slot >= List[index].SlotUnit)
//                                 {
//                                     List[index++].SlotResest();
//                                 }
//                                 else break;
//                             }
//                         }
//                     }
//                 }
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//             }
//         }
//
//         public static int NUM;
//
//         private void Execute()
//         {
//             Console.WriteLine($"TimerManager TimerTaskContainer Succeed: Unit={Unit} Counter={Counter} RemainNum={RemainNum} NUM={++NUM}");
//             Dispose();
//         }
//
//         private void Exception()
//         {
//             Console.WriteLine($"TimerManager TimerTaskContainer Error: Unit={Unit} Counter={Counter} RemainNum={RemainNum}");
//             Dispose();
//         }
//
//         /// <summary>
//         /// 释放
//         /// </summary>
//         public void Dispose()
//         {
//             if (TimerSystem.SWITCH)
//             {
//                 TimerSystem.TaskList.Remove(this);
//             }
//
//             for (int i = 0; i < List.Count; i++)
//             {
//                 List[i].Dispose();
//                 List[i] = null;
//             }
//
//             List.Clear();
//         }
//     }
// }