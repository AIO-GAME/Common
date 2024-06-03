#region

using System;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public interface IGraphDraw
    {
        void Draw();
    }

    /// <summary>
    /// 图形范围
    /// </summary>
    public interface IGraphRect : IGraphEvent, IDisposable, IGraphDraw
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
        /// 清空数据
        /// </summary>
        void Clear();

        /// <summary>
        /// 初始化
        /// </summary>
        void OnAwake();
    }
}