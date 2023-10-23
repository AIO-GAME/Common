using UnityEngine;

#pragma warning disable CS1591
namespace AIO
{
    /// <summary>
    /// 插件信息
    /// </summary>
    [CreateAssetMenu(menuName = "Plugins/AIO Data", fileName = nameof(PluginData))]
    public class PluginData : ScriptableObject
    {
        /// <summary>
        /// 插件相对路径
        /// </summary>
        public string SourceRelativePath;

        /// <summary>
        /// 插件名
        /// </summary>
        public string Name;

        /// <summary>
        /// 目标位置
        /// </summary>
        public string TargetRelativePath;

        /// <summary>
        /// 宏定义
        /// </summary>
        public string MacroDefinition;

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction;
    }

    internal partial class Plugins
    {
    }
}