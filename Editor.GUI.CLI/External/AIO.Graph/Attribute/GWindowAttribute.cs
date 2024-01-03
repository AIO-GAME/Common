/*|✩ - - - - - |||
|||✩ Author:   ||| -> star fire
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GWindowAttribute : DisplayNameAttribute
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
        /// 菜单路径
        /// </summary>
        public string Menu;

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon;

        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int MenuPriority;

        /// <summary>
        /// 菜单验证函数 返回值为bool
        /// </summary>
        public MethodInfo MenuValidate;

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

        /// <summary>
        /// 运行时 窗口类型
        /// </summary>
        public Type RuntimeType;

        /// <inheritdoc />
        public GWindowAttribute(string title) : base(title)
        {
            Title = new GUIContent(title);
        }

        /// <inheritdoc />
        public GWindowAttribute(string title, string tooltip) : base(title)
        {
            Title = new GUIContent(title, tooltip);
        }
    }
}