﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-06
|||✩ Document: ||| ->
|||✩ - - - - - |*/

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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 定时任务处理器
    /// </summary>
    internal sealed class TimerExecutorAction : TimerExecutor<Action>
    {
        /// <summary>
        /// 定时计算器
        /// </summary>
        /// <param name="duration">定时长度 单位为毫秒</param>
        /// <param name="loop">循环次数</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="delegateValue">委托函数</param>
        internal TimerExecutorAction(long duration, int loop, long createTime, Action delegateValue) : base(duration, loop, createTime)
        {
            Delegates = delegateValue;
        }

        /// <summary>
        /// 定时计算器
        /// </summary>
        /// <param name="tid">识别ID</param>
        /// <param name="duration">定时长度 单位为毫秒</param>
        /// <param name="loop">循环次数</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="delegateValue">委托函数</param>
        internal TimerExecutorAction(long tid, long duration, int loop, long createTime, Action delegateValue) : base(duration, loop, createTime, tid)
        {
            Delegates = delegateValue;
        }

        protected override void xExecute()
        {
            Delegates.Invoke();
        }
    }
}
