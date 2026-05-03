#region namespace

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 快捷工具，仅可标记静态函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class LnkToolsAttribute : Attribute, IGUIContentInfo
    {
        /// <summary>
        /// 快捷工具，仅可标记静态函数
        /// </summary>
        /// <param name="runtimeMode">Unity 启动模式</param>
        /// <param name="priority">工具优先级（越小越靠前）</param>
        public LnkToolsAttribute(
            ELnkToolsMode runtimeMode = ELnkToolsMode.AllMode,
            int           priority    = int.MaxValue)
        {
            RuntimeMode = runtimeMode;
            Priority    = priority;
        }

        /// <summary>
        /// 快捷工具，仅可标记静态函数
        /// </summary>
        /// <param name="text">工具名称</param>
        /// <param name="runtimeMode">快捷工具触发类型</param>
        /// <param name="priority">工具优先级（越小越靠前）</param>
        public LnkToolsAttribute(string        text,
                                 ELnkToolsMode runtimeMode = ELnkToolsMode.AllMode,
                                 int           priority    = int.MaxValue)
        {
            Text        = text;
            RuntimeMode = runtimeMode;
            Priority    = priority;
        }

        /// <summary>
        /// 快捷工具，仅可标记静态函数
        /// </summary>
        /// <param name="tooltip">工具提示</param>
        /// <param name="text">工具名称</param>
        /// <param name="runtimeMode">快捷工具触发类型</param>
        /// <param name="priority">工具优先级（越小越靠前）</param>
        public LnkToolsAttribute(string        text,
                                 string        tooltip,
                                 ELnkToolsMode runtimeMode = ELnkToolsMode.AllMode,
                                 int           priority    = int.MaxValue)
        {
            Text        = text;
            Tooltip     = tooltip;
            RuntimeMode = runtimeMode;
            Priority    = priority;
        }

        public string Tooltip      { get; set; }
        public string IconBuiltin  { get; set; }
        public string IconRelative { get; set; }
        public string IconResource { get; set; }
        public string Text         { get; set; }

        /// <summary>
        /// 运行快捷工具触发类型
        /// </summary>
        public ELnkToolsMode RuntimeMode { get; set; }

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
        [InspectorName("Editor ➡️ bar")]
        ToolbarRight,

        [InspectorName("Editor ⬅️ Bar ")]
        ToolbarLeft,

        [InspectorName("Scene 窗口")]
        SceneView,

        // GameView,
    }

    /// <summary>
    /// 快捷工具触发类型
    /// </summary>
    [Flags]
    public enum ELnkToolsMode : byte
    {
        [InspectorName("无模式")]
        NoMode = 0,

        /// <summary>
        /// 仅在运行时
        /// </summary>
        [InspectorName("运行模式")]
        OnlyRuntime = 1,

        /// <summary>
        /// 仅在编辑时
        /// </summary>
        [InspectorName("编辑模式")]
        OnlyEditor = 2,

        /// <summary>
        /// 所有模式
        /// </summary>
        [InspectorName("全部模式")]
        AllMode = OnlyRuntime | OnlyEditor,
    }
}