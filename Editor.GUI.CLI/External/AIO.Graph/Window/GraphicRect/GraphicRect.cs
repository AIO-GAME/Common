#region

using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 图形矩形
    /// </summary>
    [DisplayName("图形矩形")]
    public abstract partial class GraphicRect : IGraphRect
    {
        private bool isEvent;

        private bool isShow;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected GraphicRect()
        {
            Rect  = new Rect();
            Items = new List<GraphicRect>();
            Awake();
            isShow = true;
        }

        public string Title { get; protected set; }

        private Rect Rect { get; set; }

        /// <summary>
        /// 中心点
        /// </summary>
        public Vector2 CenterPosition => Rect.size / 2;

        /// <summary>
        /// 子项
        /// </summary>
        public List<GraphicRect> Items { get; private set; }

        #region IGraphRect Members

        /// <summary>
        /// 中心点
        /// </summary>
        public Rect RectCenter => new Rect(Rect.size / 2, Rect.size);

        /// <summary>
        /// 绘制
        /// </summary>
        public void Draw()
        {
            if (isShow) OnDraw();
            if (isEvent) OnOpenEvent();
        }

        #endregion

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            IsEvent = false;
            IsShow  = false;
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void Show()
        {
            IsEvent = true;
            IsShow  = true;
        }

        private void Awake()
        {
            OnAwake();
        }

        /// <summary>
        /// 居中
        /// </summary>
        public void Overview(in Vector2 size)
        {
            var rect = Rect;
            rect.position = (size - Rect.size) / 2;
            Rect          = rect;
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