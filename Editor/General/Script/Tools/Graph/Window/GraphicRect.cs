/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 图形矩形
    /// </summary>
    [DisplayName("图形矩形")]
    public abstract partial class GraphicRect : IGraphRect
    {
        private Rect Rect { get; set; }

        /// <summary>
        /// 中心点
        /// </summary>
        public Rect Center
        {
            get { return new Rect(Rect.size / 2, Rect.size); }
        }

        /// <summary>
        /// 中心点
        /// </summary>
        public Vector2 CenterPosition
        {
            get { return Rect.size / 2; }
        }

        private bool isShow;

        private bool isEvent;

        /// <summary>
        /// 子项
        /// </summary>
        public List<GraphicRect> Items { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected GraphicRect()
        {
            Rect = new Rect();
            Items = Pool.List<GraphicRect>();
            Awake();
            isShow = true;
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            IsEvent = false;
            IsShow = false;
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void Show()
        {
            IsEvent = true;
            IsShow = true;
        }

        private void Awake() => OnAwake();

        /// <summary>
        /// 绘制
        /// </summary>
        public void Draw()
        {
            if (isShow) OnDraw();
            if (isEvent) OnOpenEvent();
        }

        /// <summary>
        /// 居中
        /// </summary>
        public void Overview(in Vector2 size)
        {
            var rect = Rect;
            rect.position = (size - Rect.size) / 2;
            Rect = rect;
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        public sealed override string ToString()
        {
            return base.ToString();
        }
    }
}
