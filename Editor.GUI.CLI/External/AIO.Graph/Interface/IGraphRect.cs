﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 图形范围
    /// </summary>
    public interface IGraphRect : IGraphEvent, IDisposable
    {
        /// <summary>
        /// 矩形范围
        /// </summary>
        Rect RectData { get; }

        /// <summary>
        /// 中心点
        /// </summary>
        Rect RectCenter { get; }

        /// <summary>
        /// 绘制
        /// </summary>
        void Draw();

        /// <summary>
        /// 清空数据
        /// </summary>
        void Clear();

        /// <summary>
        /// 初始化
        /// </summary>
        void OnAwake();
    }
}