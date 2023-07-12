/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-09
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections;

namespace UnityEngine
{
    public partial class TimerSystem
    {
        #region Timer Executor Enumerator

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void Push(long duration, Func<IEnumerator> delegateValue)
        {
            AddUpdate(new TimerExecutorEnumerator(duration, 1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void PushLoop(long duration, Func<IEnumerator> delegateValue)
        {
            AddUpdate(new TimerExecutorEnumerator(duration, -1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Push(long duration, ushort loop, Func<IEnumerator> delegateValue)
        {
            AddUpdate(new TimerExecutorEnumerator(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Push(long duration, byte loop, Func<IEnumerator> delegateValue)
        {
            AddUpdate(new TimerExecutorEnumerator(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        #endregion

        #region Timer Executor Action

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void Push(long duration, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(duration, 1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void Push(long tid, long duration, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(tid, duration, 1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void Push(string tidstring, long duration, Action delegateValue)
        {
            var tid = tidstring.GetHashCode();
            AddUpdate(new TimerExecutorAction(tid, duration, 1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void PushLoop(long duration, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(duration, -1, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器
        /// </summary>
        public static void PushLoop(long tid, long duration, Action delegateValue)
        {
            var temp = new TimerExecutorAction(tid, duration, -1, Counter, delegateValue);

            AddUpdate(temp);
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Push(long duration, ushort loop, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        /// <summary>
        /// 添加定时任务处理器 是否循环 0循环 1循环次数
        /// </summary>
        public static void Push(long duration, byte loop, Action delegateValue)
        {
            AddUpdate(new TimerExecutorAction(duration, loop == 0 ? -1 : loop, Counter, delegateValue));
        }

        #endregion

        /// <summary>
        /// 取出循环任务
        /// </summary>
        public static void PopLoop(long tid)
        {
            if (!TimerExecutors.TryGetValue(tid, out var executor)) return;
            executor.Loop = -2;
            TimerExecutors.Remove(tid);
        }


        /// <summary>
        /// 取出循环任务
        /// </summary>
        public static void Pop(long tid)
        {
            if (!TimerExecutors.TryGetValue(tid, out var executor)) return;
            executor.Loop = -2;
            TimerExecutors.Remove(tid);
        }

        public static bool Exist(long tid)
        {
            return TimerExecutors.ContainsKey(tid);
        }
    }
}