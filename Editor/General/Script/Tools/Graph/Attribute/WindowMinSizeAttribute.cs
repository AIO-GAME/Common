/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 最小的窗口大小
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WindowMinSizeAttribute : Attribute
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public float Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        public float Height { get; }

        /// <inheritdoc />
        public WindowMinSizeAttribute(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <inheritdoc />
        public WindowMinSizeAttribute(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <inheritdoc />
        public WindowMinSizeAttribute(Vector2 height)
        {
            Width = height.x;
            Height = height.y;
        }
    }
}
