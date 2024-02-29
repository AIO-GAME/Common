using UnityEngine;

namespace AIO.UEngine
{
    /// <summary>
    /// Rect扩展
    /// </summary>
    public static class RectExtend
    {
        /// <summary>
        /// 设置高
        /// </summary>
        public static Rect SetHeight(this ref Rect rect, float value)
        {
            rect.height = value;
            return rect;
        }

        /// <summary>
        /// 设置宽
        /// </summary>
        public static Rect SetWidth(this ref Rect rect, float value)
        {
            rect.width = value;
            return rect;
        }

        /// <summary>
        /// 设置X
        /// </summary>
        public static Rect SetX(this ref Rect rect, float value)
        {
            rect.x = value;
            return rect;
        }

        /// <summary>
        /// 设置X
        /// </summary>
        public static Rect SetY(this ref Rect rect, float value)
        {
            rect.y = value;
            return rect;
        }

        /// <summary>
        /// 设置XMin
        /// </summary>
        public static Rect SetXMin(this ref Rect rect, float value)
        {
            rect.xMin = value;
            return rect;
        }

        /// <summary>
        /// 设置XMin
        /// </summary>
        public static Rect SetYMin(this ref Rect rect, float value)
        {
            rect.yMin = value;
            return rect;
        }

        /// <summary>
        /// 设置XMin
        /// </summary>
        public static Rect SetXMax(this ref Rect rect, float value)
        {
            rect.xMax = value;
            return rect;
        }

        /// <summary>
        /// 设置XMin
        /// </summary>
        public static Rect SetYMax(this ref Rect rect, float value)
        {
            rect.yMax = value;
            return rect;
        }

        /// <summary>
        /// 设置Min
        /// </summary>
        public static Rect SetMin(this ref Rect rect, Vector2 value)
        {
            rect.min = value;
            return rect;
        }

        /// <summary>
        /// 设置Min
        /// </summary>
        public static Rect SetMax(this ref Rect rect, Vector2 value)
        {
            rect.max = value;
            return rect;
        }

        /// <summary>
        /// 设置Center
        /// </summary>
        public static Rect SetCenter(this ref Rect rect, Vector2 value)
        {
            rect.center = value;
            return rect;
        }

        /// <summary>
        /// 设置Center
        /// </summary>
        public static Rect SetSize(this ref Rect rect, Vector2 value)
        {
            rect.size = value;
            return rect;
        }

        /// <summary>
        /// 设置Center
        /// </summary>
        public static Rect SetPosition(this ref Rect rect, Vector2 value)
        {
            rect.position = value;
            return rect;
        }
    }
}