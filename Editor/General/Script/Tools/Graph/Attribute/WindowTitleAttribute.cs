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
    /// 窗口名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WindowTitleAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public GUIContent Title { get; }

        /// <inheritdoc />
        public WindowTitleAttribute(string title)
        {
            Title = new GUIContent(title);
        }

        /// <inheritdoc />
        public WindowTitleAttribute(string title, Texture texture)
        {
            Title = new GUIContent(title, texture);
        }

        /// <inheritdoc />
        public WindowTitleAttribute(string title, Texture image, string tooltip)
        {
            Title = new GUIContent(title, image, tooltip);
        }

        /// <inheritdoc />
        public WindowTitleAttribute(Texture texture, string tooltip)
        {
            Title = new GUIContent(texture, tooltip);
        }

        /// <inheritdoc />
        public WindowTitleAttribute(string title, string tooltip)
        {
            Title = new GUIContent(title, tooltip);
        }

        /// <inheritdoc />
        public WindowTitleAttribute(GUIContent title)
        {
            Title = title;
        }
    }
}
