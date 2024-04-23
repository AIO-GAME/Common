#region

using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    [StructLayout(LayoutKind.Auto)]
    public partial struct ViewRect
    {
        public static implicit operator Rect(ViewRect viewRect)
        {
            return viewRect.Current;
        }

        /// <summary>
        ///     是否显示
        /// </summary>
        public bool IsShow;

        /// <summary>
        ///     最小宽度
        /// </summary>
        public float MinWidth;

        /// <summary>
        ///     最大宽度
        /// </summary>
        public float MaxWidth;

        /// <summary>
        ///     最小高度
        /// </summary>
        public float MinHeight;

        /// <summary>
        ///     最大高度
        /// </summary>
        public float MaxHeight;

        private Rect Current;

        public ViewRect(float minWidth, float minHeight, DragStretchType allowDragStretch = DragStretchType.None)
        {
            MinWidth = minWidth;
            MaxWidth = minWidth;
            MinHeight = minHeight;
            MaxHeight = minWidth;

            DragStretchHorizontalWidth = 1;
            DragStretchVerticalHeight = 1;

            AllowDragStretch = allowDragStretch;
            DragStretch = DragStretchType.None;

            IsShow = true;
            Current = new Rect
            {
                width = minWidth,
                height = minHeight
            };
            RectDragHorizontal = new Rect();
            RectDragVertical = new Rect();
        }

        /// <summary>
        ///     宽度
        /// </summary>
        public float width
        {
            get => Current.width;
            set => Current.width = value;
        }

        /// <summary>
        ///     高度
        /// </summary>
        public float height
        {
            get => Current.height;
            set => Current.height = value;
        }

        /// <summary>
        ///     位置 X
        /// </summary>
        public float x
        {
            get => Current.x;
            set => Current.x = value;
        }

        /// <summary>
        ///     位置 Y
        /// </summary>
        public float y
        {
            get => Current.y;
            set => Current.y = value;
        }

        /// <summary>
        ///     中心
        /// </summary>
        public Vector2 center
        {
            get => Current.center;
            set => Current.center = value;
        }


        public void Draw(Action<Rect> onDraw, GUIStyle style = null)
        {
            if (IsShow) Draw(Current, onDraw, style);
        }

        private void Draw(Rect rect, Action<Rect> onDraw, GUIStyle style = null)
        {
            if (!IsShow) return;
            switch (AllowDragStretch)
            {
                case DragStretchType.Horizontal:
                    rect.width -= DragStretchHorizontalWidth;
                    RectDragHorizontal.Set(rect.x + rect.width, rect.y, DragStretchHorizontalWidth, rect.height);
                    EditorGUIUtility.AddCursorRect(RectDragHorizontal, MouseCursor.ResizeHorizontal);
                    break;
                case DragStretchType.Vertical:
                    rect.height -= DragStretchVerticalHeight;
                    RectDragVertical.Set(rect.x, rect.y + rect.height, rect.width, DragStretchVerticalHeight);
                    EditorGUIUtility.AddCursorRect(RectDragVertical, MouseCursor.ResizeVertical);
                    break;
                case DragStretchType.Both:
                    rect.width -= DragStretchHorizontalWidth;
                    RectDragHorizontal.Set(rect.x + rect.width, rect.y, DragStretchHorizontalWidth, rect.height);
                    EditorGUIUtility.AddCursorRect(RectDragHorizontal, MouseCursor.ResizeHorizontal);

                    rect.height -= DragStretchVerticalHeight;
                    RectDragVertical.Set(rect.x, rect.y + rect.height, rect.width, DragStretchVerticalHeight);
                    EditorGUIUtility.AddCursorRect(RectDragVertical, MouseCursor.ResizeVertical);
                    break;

                case DragStretchType.None:
                default:
                    break;
            }

            if (style != null) GUI.Box(rect, GUIContent.none, style);
            using (new GUI.ClipScope(rect))
            {
                if (onDraw == null) return;
                rect.x = 0;
                rect.y = 0;
                try
                {
                    onDraw.Invoke(rect);
                }
                catch (Exception)
                {
                    //  ignored
                }
            }
        }
    }
}