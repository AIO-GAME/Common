using System;
using UnityEngine;

namespace AIO.UEditor
{
    public partial struct ViewRect
    {
        [Flags]
        public enum DragStretchType : byte
        {
            None       = 0
          , Horizontal = 1
          , Vertical   = 2
          , Both       = Horizontal | Vertical
        }

        private DragStretchType AllowDragStretch;

        private DragStretchType DragStretch;

        /// <summary>
        ///     是否允许 横向拖拽
        /// </summary>
        public bool IsAllowDragStretchHorizontal
        {
            get => AllowDragStretch.HasFlag(DragStretchType.Horizontal);
            set
            {
                if (value) AllowDragStretch |= DragStretchType.Horizontal;
                else AllowDragStretch       &= ~DragStretchType.Horizontal;
            }
        }

        /// <summary>
        ///     是否允许 竖向拖拽
        /// </summary>
        public bool IsAllowDragStretchVertical
        {
            get => AllowDragStretch.HasFlag(DragStretchType.Vertical);
            set
            {
                if (value) AllowDragStretch |= DragStretchType.Vertical;
                else AllowDragStretch       &= ~DragStretchType.Vertical;
            }
        }

        /// <summary>
        ///     拖拽宽度
        /// </summary>
        public float DragStretchHorizontalWidth;

        /// <summary>
        ///     拖拽高度
        /// </summary>
        public float DragStretchVerticalHeight;

        /// <summary>
        ///     是否拖拽拉伸 横向
        /// </summary>
        private bool IsDragStretchHorizontal
        {
            get => DragStretch.HasFlag(DragStretchType.Horizontal);
            set
            {
                if (value) DragStretch |= DragStretchType.Horizontal;
                else DragStretch       &= ~DragStretchType.Horizontal;
            }
        }

        /// <summary>
        ///     是否拖拽拉伸 竖向
        /// </summary>
        private bool IsDragStretchVertical
        {
            get => DragStretch.HasFlag(DragStretchType.Vertical);
            set
            {
                if (value) DragStretch |= DragStretchType.Vertical;
                else DragStretch       &= ~DragStretchType.Vertical;
            }
        }

        /// <summary>
        ///     拖拽区域
        /// </summary>
        private Rect RectDragHorizontal;

        /// <summary>
        ///     拖拽区域
        /// </summary>
        private Rect RectDragVertical;

        /// <summary>
        ///     判断是否在拖拽区域内 横向
        /// </summary>
        /// <param name="eventData"> 事件 </param>
        /// <param name="type"> 类型 </param>
        public bool ContainsDragStretch(Event eventData, DragStretchType type)
        {
            return ContainsDragStretch(eventData.mousePosition, type);
        }

        /// <summary>
        ///     判断是否在拖拽区域内
        /// </summary>
        /// <param name="point"> 点 </param>
        /// <param name="type"> 类型 </param>
        public bool ContainsDragStretch(Vector2 point, DragStretchType type)
        {
            switch (type)
            {
                case DragStretchType.Horizontal:
                    if (!IsShow || !IsAllowDragStretchHorizontal) IsDragStretchHorizontal = false;
                    else IsDragStretchHorizontal                               = RectDragHorizontal.Contains(point);
                    return IsDragStretchHorizontal;
                case DragStretchType.Vertical:
                    if (!IsShow || !IsAllowDragStretchVertical) IsDragStretchVertical = false;
                    else IsDragStretchVertical                             = RectDragVertical.Contains(point);
                    return IsDragStretchVertical;
                case DragStretchType.Both:
                    if (IsShow) return IsDragStretchHorizontal || IsDragStretchVertical;
                    IsDragStretchHorizontal = IsAllowDragStretchHorizontal && RectDragHorizontal.Contains(point);
                    IsDragStretchVertical   = IsAllowDragStretchVertical && RectDragVertical.Contains(point);
                    return IsDragStretchHorizontal || IsDragStretchVertical;
                case DragStretchType.None:
                default:
                    return false;
            }
        }

        /// <summary>
        ///     取消拖拽
        /// </summary>
        public void CancelDragStretch(DragStretchType type = DragStretchType.Both)
        {
            switch (type)
            {
                case DragStretchType.Horizontal:
                    IsDragStretchHorizontal = false;
                    break;
                case DragStretchType.Vertical:
                    IsDragStretchVertical = false;
                    break;
                case DragStretchType.Both:
                    DragStretch = DragStretchType.None;
                    break;
                case DragStretchType.None:
                default:
                    return;
            }
        }

        /// <summary>
        ///     拖拽 竖向
        /// </summary>
        /// <param name="e"> 事件 </param>
        /// <param name="type"> 类型 </param>
        public void DraggingStretch(Event e, DragStretchType type = DragStretchType.Both)
        {
            if (e == null || !IsShow) return;
            switch (type)
            {
                case DragStretchType.Horizontal:
                {
                    if (!IsAllowDragStretchHorizontal || !IsDragStretchHorizontal) return;
                    var tempX = Current.width + e.delta.x;
                    if (tempX < MinWidth) Current.width      = MinWidth;
                    else if (tempX > MaxWidth) Current.width = MaxWidth;
                    else Current.width                       = tempX;
                    break;
                }
                case DragStretchType.Vertical:
                {
                    if (!IsAllowDragStretchVertical || !IsDragStretchVertical) return;
                    var tempY = Current.height + e.delta.y;
                    if (tempY < MinHeight) Current.height      = MinHeight;
                    else if (tempY > MaxHeight) Current.height = MaxHeight;
                    else Current.height                        = tempY;
                    break;
                }
                case DragStretchType.Both:
                {
                    if (!IsAllowDragStretchHorizontal || !IsDragStretchHorizontal) return;
                    var tempX = Current.width + e.delta.x;
                    if (tempX < MinWidth) Current.width      = MinWidth;
                    else if (tempX > MaxWidth) Current.width = MaxWidth;
                    else Current.width                       = tempX;

                    if (!IsAllowDragStretchVertical || !IsDragStretchVertical) break;
                    var tempY = Current.height + e.delta.y;
                    if (tempY < MinHeight) Current.height      = MinHeight;
                    else if (tempY > MaxHeight) Current.height = MaxHeight;
                    else Current.height                        = tempY;
                    break;
                }
                case DragStretchType.None:
                default:
                    return;
            }

            e.Use();
        }
    }
}