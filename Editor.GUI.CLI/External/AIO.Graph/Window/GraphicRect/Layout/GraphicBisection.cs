using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 左右分割图形矩形
    /// </summary>
    [DisplayName("左右分割图形矩形")]
    public class GraphicBisection : GraphicRect
    {
        private Rect RightRect;
        private Vector2 RightPosition;

        private Rect LeftRect;
        private Vector2 LeftPosition;

        private Rect SplitRect;

        public uint RightMinWidth { get; set; }
        public uint LeftMinWidth { get; set; }
        public uint SplitWidth { get; set; } = 20;

        public GraphicBisection()
        {
        }

        public override void OnAwake()
        {
            IsEvent = true;
        }

        protected sealed override void OnDraw()
        {
            if (RightMinWidth <= 0 && LeftMinWidth <= 0) RightMinWidth = LeftMinWidth = (uint)Width / 3;

            if (RightRect.width < RightMinWidth && LeftRect.width < LeftMinWidth)
                SplitRect.x = (RectData.width - SplitWidth) / 2;

            SplitRect.y = RightRect.y = LeftRect.y = RectData.y;
            SplitRect.height = RightRect.height = LeftRect.height = RectData.height;
            SplitRect.width = SplitWidth;

            LeftRect.width = SplitRect.x;
            LeftRect.x = RectData.x + 10;
            RightRect.x = SplitRect.x + SplitWidth;
            RightRect.width = RectData.width - RightRect.x - 10;

            using (new GUILayout.AreaScope(RectData, ""))
            {
                using (new GUILayout.AreaScope(LeftRect, "", GEStyle.TEBoxBackground))
                {
                    LeftPosition = GELayout.VScrollView(() => { OnDrawLeft(LeftRect); }, LeftPosition, false, false);
                }

                using (new GUILayout.AreaScope(RightRect, "", GEStyle.TEBoxBackground))
                {
                    RightPosition =
                        GELayout.VScrollView(() => { OnDrawRight(RightRect); }, RightPosition, false, false);
                }
            }

            AddCursor(SplitRect, MouseCursor.SplitResizeLeftRight);
        }

        /// <summary>
        /// 绘制左边
        /// </summary>
        /// <param name="rect"></param>
        protected virtual void OnDrawLeft(Rect rect)
        {
        }

        /// <summary>
        /// 绘制右边
        /// </summary>
        /// <param name="rect"></param>
        protected virtual void OnDrawRight(Rect rect)
        {
        }

        private bool isDrag;

        public sealed override void EventMouseDown(in Event eventData)
        {
            if (SplitRect.Contains(eventData.mousePosition))
            {
                isDrag = true;
            }
        }

        public sealed override void EventMouseUp(in Event eventData)
        {
            isDrag = false;
        }

        public sealed override void EventMouseDrag(in Event eventData)
        {
            if (!SplitRect.Contains(eventData.mousePosition) && !isDrag) return;

            var b = false;
            if (Event.current.button == 0)
            {
                SplitRect.x += eventData.delta.x;

                if (SplitRect.x <= LeftMinWidth)
                    SplitRect.x = LeftMinWidth;
                else if (RectData.width - SplitRect.x - SplitWidth - 10 <= RightMinWidth) // 右边最小宽度
                    SplitRect.x = RectData.width - RightMinWidth - SplitWidth - 10;

                b = true;
            }

            if (b) eventData.Use();
        }
    }
}