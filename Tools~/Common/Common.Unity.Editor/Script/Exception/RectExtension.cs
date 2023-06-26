using UnityEngine;

namespace UnityEditor
{
    /// <summary>
    /// 矩形扩展
    /// </summary>
    public static class RectExtension
    {
        /// <summary>
        /// GUI绘制矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="fillColor">填充颜色</param>
        /// <param name="outlineColor">描边颜色</param>
        public static void DrawGUI(this Rect rect, in Color fillColor, in Color outlineColor)
        {
            if (rect.size == Vector2.zero) return;
            Handles.DrawSolidRectangleWithOutline(rect, fillColor, outlineColor);
        }

        /// <summary>
        /// GUI绘制矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="fillColor">填充颜色</param>
        public static void DrawGUI(this Rect rect, in Color fillColor)
        {
            if (rect.size == Vector2.zero) return;
            var outlineColor = Color.red;
            Handles.DrawSolidRectangleWithOutline(rect, fillColor, outlineColor);
        }

        /// <summary>
        /// GUI绘制矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        public static void DrawGUI(this Rect rect)
        {
            if (rect.size == Vector2.zero) return;
            var fillColor = new Color(1, 0, 0, 0.5f);
            var outlineColor = Color.red;
            Handles.DrawSolidRectangleWithOutline(rect, fillColor, outlineColor);
        }
    }
}