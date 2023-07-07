/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           1.0                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-12-02                    *
* Nowtime:           15:06:36                      *
* Description:                                     *
* History:                                         *
***************************************************/

using System;
using System.Diagnostics;
using System.Text;

namespace UnityEngine
{
    /// <summary>
    /// 定时任务处理器
    /// </summary>
    internal abstract class TimerExecutor<T> : ITimerExecutor<T> where T : Delegate
    {
        public long CreateTime { get; private set; }

        public long Duration { get; }

        public long EndTime { get; private set; }

        public long Interval { get; private set; }

        public int Loop { get; private set; }

        public uint Number { get; private set; }

        byte ITimerExecutor.OperatorIndex { get; set; }

        public Stopwatch Watch { get; private set; }

        public T Delegates { get; protected set; }

        /// <summary>
        /// 定时计算器
        /// </summary>
        /// <param name="duration">定时长度 单位为毫秒</param>
        /// <param name="loop">循环次数</param>
        /// <param name="createTime">创建时间</param>
        protected TimerExecutor(long duration, int loop, long createTime)
        {
            Watch = Stopwatch.StartNew();
            Loop = loop;
            Duration = duration;
            CreateTime = createTime;
            EndTime = duration + createTime;
            Number = 0;
            Interval = 0;
        }

        public abstract void Execute();

        /// <summary>
        /// 获取当前时间
        /// </summary>
        private long CurrentTime { get; set; }

        /// <summary>
        /// 更新循环次数
        /// 返回Ture: 可以循环
        /// 返回False:循环结束
        /// </summary>
        public bool UpdateLoop()
        {
            Number = Number + 1; //次数增加
            CurrentTime = Watch.ElapsedMilliseconds;
            Interval = CurrentTime - (Number * Duration);

            switch (--Loop) //次数减少
            {
                case 0:
                    Watch.Stop();
                    Watch = null;
                    Debug.Log(ToString());
                    return false; //达到次数
                case < 0:
                case > 0:
                {
                    Debug.Log(ToString());
                    CreateTime = EndTime;
                    EndTime = Duration + CreateTime - Interval;
                    return true; //无限循环
                }
            }
        }

        public int CompareTo(ITimerExecutor other)
        {
            if (EndTime < other.EndTime) return 1;
            if (EndTime > other.EndTime) return -1;
            return 0;
        }

        public void Dispose()
        {
            Delegates = null;
        }

        public sealed override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("定时单位").Append('=').Append(Duration.ToString("00000000")).Append(' ');
            builder.Append("创建时间").Append('=').Append(CreateTime.ToString("00000000")).Append(' ');
            builder.Append("结束时间").Append('=').Append(EndTime.ToString("00000000")).Append(' ');
            builder.Append("循环次数").Append('=').Append(Number.ToString("00000")).Append(' ');
            builder.Append("实际时间").Append('=').Append(CurrentTime.ToString("00000000")).Append(' ');
            builder.Append("误差时间").Append('=').Append(Interval.ToString("00000")).Append(' ');
            return builder.ToString();
        }
    }
}