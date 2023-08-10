/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 最大的窗口大小
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WindowMaxSizeAttribute : Attribute
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
        public WindowMaxSizeAttribute(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <inheritdoc />
        public WindowMaxSizeAttribute(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <inheritdoc />
        public WindowMaxSizeAttribute(Vector2 height)
        {
            Width = height.x;
            Height = height.y;
        }
    }
}