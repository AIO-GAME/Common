using UnityEngine;
using UnityEditor;

namespace AIO.UEditor
{
    partial class GraphicRect
    {
        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="fillColor">填充颜色</param>
        /// <param name="outlineColor">描边颜色</param>
        public static void DrawRect(in Rect rect, in Color fillColor, in Color outlineColor)
        {
            var corners = new Vector3[4];
            corners[0] = new Vector3(rect.x, rect.y, 0);
            corners[1] = new Vector3(rect.x, rect.y + rect.width, 0);
            corners[2] = new Vector3(rect.x + rect.width, rect.y + rect.width, 0);
            corners[3] = new Vector3(rect.x + rect.width, rect.y, 0);
            Handles.DrawSolidRectangleWithOutline(corners, fillColor, outlineColor);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="fillColor">填充颜色</param>
        public static void DrawRect(in Rect rect, in Color fillColor)
        {
            var corners = new Vector3[4];
            corners[0] = new Vector3(rect.x, rect.y, 0);
            corners[1] = new Vector3(rect.x, rect.y + rect.width, 0);
            corners[2] = new Vector3(rect.x + rect.width, rect.y + rect.width, 0);
            corners[3] = new Vector3(rect.x + rect.width, rect.y, 0);
            var outlineColor = Color.red;
            Handles.DrawSolidRectangleWithOutline(corners, fillColor, outlineColor);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        public static void DrawRect(in Rect rect)
        {
            var corners = new Vector3[4];
            corners[0] = new Vector3(rect.x, rect.y, 0);
            corners[1] = new Vector3(rect.x, rect.y + rect.width, 0);
            corners[2] = new Vector3(rect.x + rect.width, rect.y + rect.width, 0);
            corners[3] = new Vector3(rect.x + rect.width, rect.y, 0);
            var fillColor = new Color(1, 0, 0, 0.5f);
            var outlineColor = Color.red;
            Handles.DrawSolidRectangleWithOutline(corners, fillColor, outlineColor);
        }


        /// <summary>
        /// 添加Cursor
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="cursor">鼠标样式</param>
        public static void AddCursor(in Rect rect, MouseCursor cursor)
        {
            EditorGUIUtility.AddCursorRect(rect, MouseCursor.SplitResizeLeftRight);
        }
    }
}