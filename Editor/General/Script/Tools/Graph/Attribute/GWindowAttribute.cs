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
    /// 窗口信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GWindowAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public GUIContent Title { get; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public uint MaxSizeWidth = 0;

        /// <summary>
        /// 最大高度
        /// </summary>
        public uint MaxSizeHeight = 0;

        /// <summary>
        /// 最大宽高
        /// </summary>
        public Vector2 MaxSize => new Vector2(MaxSizeWidth, MaxSizeHeight);

        /// <summary>
        /// 最小宽度
        /// </summary>
        public uint MinSizeWidth = 0;

        /// <summary>
        /// 最小高度
        /// </summary>
        public uint MinSizeHeight = 0;

        /// <summary>
        /// 最小宽高
        /// </summary>
        public Vector2 MinSize => new Vector2(MinSizeWidth, MinSizeHeight);

        /// <summary>
        /// 组
        /// </summary>
        public string Group;

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order;

        /// <inheritdoc />
        public GWindowAttribute(string title)
        {
            Title = new GUIContent(title);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, Texture texture)
        {
            Title = new GUIContent(title, texture);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, Texture image, string tooltip)
        {
            Title = new GUIContent(title, image, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(Texture texture, string tooltip)
        {
            Title = new GUIContent(texture, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, string tooltip)
        {
            Title = new GUIContent(title, tooltip);
        }

        /// <inheritdoc />
        public GWindowAttribute(GUIContent title)
        {
            Title = title;
        }
    }
}