/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 快捷工具，仅可标记静态函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class LnkToolsAttribute : Attribute
    {
        /// <summary>
        /// 工具名称
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// 工具使用的内置图标 Unity内置图标
        /// </summary>
        public string IconBuiltin { get; set; }

        /// <summary>
        /// 相对路径图标 使用 AssetDatabase.LoadAssetAtPath 加载
        /// </summary>
        public string IconRelative { get; set; }

        /// <summary>
        /// 资源路径图标 使用 Resources.Load 加载
        /// </summary>
        public string IconResource { get; set; }

        /// <summary>
        /// 工具使用的自定义标题
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 快捷工具触发类型
        /// </summary>
        public ELnkToolsMode Mode { get; set; }

        /// <summary>
        /// 工具优先级（越小越靠前）
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 前景色
        /// </summary>
        public string ForegroundColor { get; set; }

        /// <summary>
        /// 显示模式
        /// </summary>
        public ELnkShowMode ShowMode { get; set; } = ELnkShowMode.SceneView;

        public LnkToolsAttribute(
            ELnkToolsMode mode = ELnkToolsMode.AllMode,
            int priority = int.MaxValue)
        {
            Mode = mode;
            Priority = priority;
        }

        public LnkToolsAttribute(string text,
            ELnkToolsMode mode = ELnkToolsMode.AllMode,
            int priority = int.MaxValue)
        {
            Text = text;
            Mode = mode;
            Priority = priority;
        }

        public LnkToolsAttribute(string text, string tooltip,
            ELnkToolsMode mode = ELnkToolsMode.AllMode,
            int priority = int.MaxValue)
        {
            Text = text;
            Tooltip = tooltip;
            Mode = mode;
            Priority = priority;
        }
    }

    public enum ELnkShowMode
    {
        [InspectorName("Editor bar")] Toolbar,
        [InspectorName("Scene 窗口")] SceneView,
        // GameView,
    }

    /// <summary>
    /// 快捷工具触发类型
    /// </summary>
    public enum ELnkToolsMode
    {
        /// <summary>
        /// 所有模式
        /// </summary>
        [InspectorName("全部模式")] AllMode,

        /// <summary>
        /// 仅在运行时
        /// </summary>
        [InspectorName("运行时")] OnlyRuntime,

        /// <summary>
        /// 仅在编辑时
        /// </summary>
        [InspectorName("编辑器")] OnlyEditor
    }
}