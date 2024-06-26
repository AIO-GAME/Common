﻿using System;

namespace AIO
{
    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperationBase : IDisposable
    {
        /// <summary>
        /// 是否已经完成
        /// </summary>
        bool IsDone { get; }

        /// <summary>
        /// 处理进度
        /// </summary>
        byte Progress { get; }

        /// <summary>
        /// 是否有效
        /// </summary>
        bool IsValidate { get; }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        bool IsRunning { get; }
    }
}