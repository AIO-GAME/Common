#region namespace

using System;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 快捷工具，仅可标记静态函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class LnkToolsAttribute : Attribute
    {
        /// <summary>
        /// 快捷工具，仅可标记静态函数
        /// </summary>
        /// <param name="mode">Unity 启动模式</param>
        /// <param name="priority">工具优先级（越小越靠前）</param>
        public LnkToolsAttribute(
            ELnkToolsMode mode     = ELnkToolsMode.AllMode,
            int           priority = int.MaxValue)
        {
            Mode     = mode;
            Priority = priority;
        }

        /// <summary>
        /// 快捷工具，仅可标记静态函数
        /// </summary>
        /// <param name="text">工具名称</param>
        /// <param name="mode">快捷工具触发类型</param>
        /// <param name="priority">工具优先级（越小越靠前）</param>
        public LnkToolsAttribute(string        text,
                                 ELnkToolsMode mode     = ELnkToolsMode.AllMode,
                                 int           priority = int.MaxValue)
        {
            Text     = text;
            Mode     = mode;
            Priority = priority;
        }

        /// <summary>
        /// 快捷工具，仅可标记静态函数
        /// </summary>
        /// <param name="tooltip">工具提示</param>
        /// <param name="text">工具名称</param>
        /// <param name="mode">快捷工具触发类型</param>
        /// <param name="priority">工具优先级（越小越靠前）</param>
        public LnkToolsAttribute(string        text,
                                 string        tooltip,
                                 ELnkToolsMode mode     = ELnkToolsMode.AllMode,
                                 int           priority = int.MaxValue)
        {
            Text     = text;
            Tooltip  = tooltip;
            Mode     = mode;
            Priority = priority;
        }

        /// <summary>
        /// 工具名称
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// 工具使用的内置图标 Unity内置图标
        /// </summary>
        public string IconBuiltin { get; set; }

        /// <summary>
        /// 相对路径图标 使用 <see cref="UnityEditor.AssetDatabase.LoadAssetAtPath"/> 加载
        /// </summary>
        /// Assets/..
        /// Packages/..
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
    }

    public enum ELnkShowMode
    {
        [InspectorName("Editor bar")]
        Toolbar,

        [InspectorName("Scene 窗口")]
        SceneView,

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
        [InspectorName("全部模式")]
        AllMode,

        /// <summary>
        /// 仅在运行时
        /// </summary>
        [InspectorName("运行时")]
        OnlyRuntime,

        /// <summary>
        /// 仅在编辑时
        /// </summary>
        [InspectorName("编辑器")]
        OnlyEditor
    }
}