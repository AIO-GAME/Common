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

namespace UnityEngine
{
    using System;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// 定时任务处理器
    /// </summary>
    internal class TimerExe : IComparable<TimerExe>, IDisposable
    {
        /// <summary>
        /// 创建时间 单位毫秒
        /// </summary>
        internal long CreateTime { get; private set; }

        /// <summary>
        /// 当前任务总计数 单位1ms
        /// </summary>
        internal long Count { get; }

        /// <summary>
        /// 终止时间 单位毫秒
        /// </summary>
        internal long EndTime { get; private set; }

        /// <summary>
        /// 循环次数
        /// -1:代表无限
        /// =0:代表执行一次
        /// >0:代表循环次数
        /// </summary>
        internal int Loop { get; private set; } = 0;

        /// <summary>
        /// 当前任务实际循环次数
        /// </summary>
        internal uint Number { get; private set; } = 0;

        /// <summary>
        /// 精度器 记录当前任务实际持续时间
        /// </summary>
        internal Stopwatch Watch { get; private set; }

        /// <summary>
        /// 当前携带任务
        /// </summary>
        internal Action CallBack { get; }

        internal TimerExe(long count, int loop, long createTime, Action callBack)
        {
            Watch = Stopwatch.StartNew();

            if (loop == 0) loop = 1;
            Loop = loop;
            Count = count;
            CallBack = callBack;
            CreateTime = createTime;
            EndTime = count + createTime;
        }

        /// <summary>
        /// 更新开始结束时间
        /// </summary>
        internal void UpdateData(long CreateTime)
        {
            this.CreateTime = CreateTime;
            var now = Watch.ElapsedMilliseconds;
            var interval = now - (Number * Count);
            EndTime = Count + CreateTime - interval;
        }

        /// <summary>
        /// 更新循环次数
        /// 返回Ture: 可以循环
        /// 返回False:循环结束
        /// </summary>
        internal bool UpdateLoop()
        {
            //次数减少
            --Loop;
            Number++;
            if (Loop == 0) return false; //达到次数
            if (Loop < 0) return true; //无限循环
            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var now = Watch.ElapsedMilliseconds;
            var ctime = now - Count * Number;
            builder.Append("定时单位").Append('=').Append(Count).Append(' ');
            builder.Append("创建时间").Append('=').Append(CreateTime).Append(' ');
            builder.Append("结束时间").Append('=').Append(EndTime).Append(' ');
            builder.Append("循环次数").Append('=').Append(Number).Append(' ');
            builder.Append("实际运行").Append('=').Append(now).Append(' ');
            builder.Append("误差时间").Append('=').Append(ctime).Append(' ');
            return builder.ToString();
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// ComparetTo:大于 1； 等于 0； 小于 -1；
        /// </summary>
        public int CompareTo(TimerExe other)
        {
            int result;
            if (EndTime < other.EndTime) result = 1;
            else if (EndTime > other.EndTime) result = -1;
            else result = 0;
            return result;
        }

        /// <summary>
        /// 执行回调
        /// </summary>
        public void Execute() //async
        {
            CallBack();
        }
    }
}