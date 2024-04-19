#region

using System;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public struct ViewRect
    {
        /// <summary>
        /// 最小宽度
        /// </summary>
        public float MinWidth;

        /// <summary>
        /// 最大宽度
        /// </summary>
        public float MaxWidth;

        /// <summary>
        /// 最小高度
        /// </summary>
        public float MinHeight;

        /// <summary>
        /// 最大高度
        /// </summary>
        public float MaxHeight;

        public ViewRect(float minWidth, float minHeight)
        {
            MinWidth            = minWidth;
            MaxWidth            = minWidth;
            MinHeight           = minHeight;
            MaxHeight           = minWidth;
            IsAllowHorizontal   = false;
            IsAllowVertical     = false;
            DragHorizontalWidth = 1;
            DragVerticalHeight  = 1;
            IsDragHorizontal    = false;
            IsDragVertical      = false;
            IsShow              = true;
            Current = new Rect
            {
                width  = minWidth,
                height = minHeight
            };
            RectDragHorizontal = new Rect();
            RectDragVertical   = new Rect();
        }

        public ViewRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
        {
            MinWidth            = minWidth;
            MaxWidth            = maxWidth;
            MinHeight           = minHeight;
            MaxHeight           = maxHeight;
            IsAllowHorizontal   = false;
            IsAllowVertical     = false;
            DragHorizontalWidth = 1;
            DragVerticalHeight  = 1;
            IsDragHorizontal    = false;
            IsDragVertical      = false;
            IsShow              = true;
            Current = new Rect
            {
                width  = (minWidth + maxWidth) / 2,
                height = (minHeight + maxHeight) / 2
            };
            RectDragHorizontal = new Rect();
            RectDragVertical   = new Rect();
        }

        public static implicit operator Rect(ViewRect viewRect) => viewRect.Current;

        private Rect Current;

        /// <summary>
        /// 是否允许 横向拖拽
        /// </summary>
        public bool IsAllowHorizontal;

        /// <summary>
        /// 是否允许 竖向拖拽
        /// </summary>
        public bool IsAllowVertical;

        /// <summary>
        /// 拖拽宽度
        /// </summary>
        public float DragHorizontalWidth;

        /// <summary>
        /// 拖拽高度
        /// </summary>
        public float DragVerticalHeight;

        /// <summary>
        /// 宽度
        /// </summary>
        public float width
        {
            get => Current.width;
            set => Current.width = value;
        }

        /// <summary>
        /// 高度
        /// </summary>
        public float height
        {
            get => Current.height;
            set => Current.height = value;
        }

        /// <summary>
        /// 位置 X
        /// </summary>
        public float x
        {
            get => Current.x;
            set => Current.x = value;
        }

        /// <summary>
        /// 位置 Y
        /// </summary>
        public float y
        {
            get => Current.y;
            set => Current.y = value;
        }

        /// <summary>
        /// 是否拖拽 横向
        /// </summary>
        private bool IsDragHorizontal;

        /// <summary>
        /// 是否拖拽 竖向
        /// </summary>
        private bool IsDragVertical;

        /// <summary>
        /// 是否拖拽
        /// </summary>
        public bool IsDragging => IsDragHorizontal || IsDragVertical;

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow;

        /// <summary>
        /// 拖拽区域
        /// </summary>
        private Rect RectDragHorizontal;

        /// <summary>
        /// 拖拽区域
        /// </summary>
        private Rect RectDragVertical;

        public void ContainsHorizontal(Event e)
        {
            if (!IsShow || !IsAllowHorizontal)
            {
                if (IsDragHorizontal) IsDragHorizontal = false;
                return;
            }

            IsDragHorizontal = RectDragHorizontal.Contains(e.mousePosition);
        }

        public void ContainsVertical(Event e)
        {
            if (!IsShow || !IsAllowVertical)
            {
                if (IsDragVertical) IsDragVertical = false;
                return;
            }

            IsDragVertical = RectDragVertical.Contains(e.mousePosition);
        }

        public void CancelHorizontal()
        {
            IsDragHorizontal = false;
        }

        public void CancelVertical()
        {
            IsDragVertical = false;
        }

        public void DragHorizontal(Event e)
        {
            if (!IsShow || !IsAllowHorizontal || !IsDragHorizontal) return;
            var temp = Current.width + e.delta.x;
            if (temp < MinWidth) Current.width      = MinWidth;
            else if (temp > MaxWidth) Current.width = MaxWidth;
            else Current.width                      = temp;
            e.Use();
        }

        public void DragVertical(Event e)
        {
            if (!IsShow || !IsAllowVertical || !IsDragVertical) return;
            var temp = Current.height + e.delta.y;
            if (temp < MinHeight) Current.height      = MinHeight;
            else if (temp > MaxHeight) Current.height = MaxHeight;
            else Current.height                       = temp;
            e.Use();
        }

        public void Draw(Action<Rect> onDraw, GUIStyle style = null)
        {
            if (!IsShow) return;
            Draw(Current, onDraw, style);
        }

        public void Draw(Rect rect, Action<Rect> onDraw, GUIStyle style = null)
        {
            if (!IsShow) return;
            if (IsAllowVertical)
            {
                rect.height -= DragVerticalHeight;
                RectDragVertical.Set(rect.x, rect.y + rect.height, rect.width, DragVerticalHeight);
                EditorGUIUtility.AddCursorRect(RectDragVertical, MouseCursor.ResizeVertical);
            }

            if (IsAllowHorizontal)
            {
                rect.width -= DragHorizontalWidth;
                RectDragHorizontal.Set(rect.x + rect.width, rect.y, DragHorizontalWidth, rect.height);
                EditorGUIUtility.AddCursorRect(RectDragHorizontal, MouseCursor.ResizeHorizontal);
            }

            try
            {
                if (style is null) GUILayout.BeginArea(rect);
                else GUILayout.BeginArea(rect, style);
                onDraw?.Invoke(rect);
                GUILayout.EndArea();
            }
            catch (Exception)
            {
                //  ignored
            }
        }
    }
}