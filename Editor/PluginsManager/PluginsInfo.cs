using System;
using UnityEngine;

namespace AIO.Package.Editor
{
    [Serializable]
    internal class PluginsInfoJson
    {
        /// <summary>
        /// 插件相对路径
        /// </summary>
        [SerializeField] public string SourceRelativePath = "";

        /// <summary>
        /// 插件名
        /// </summary>
        [SerializeField] public string Name = "";

        /// <summary>
        /// 目标位置
        /// </summary>
        [SerializeField] public string TargetRelativePath = "";

        /// <summary>
        /// 宏定义
        /// </summary>
        [SerializeField] public string MacroDefinition = "";
    }

    [CreateAssetMenu(menuName = "Plugins/Info", fileName = "PluginsInfo")]
    internal class PluginsInfo : ScriptableObject
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
    }
}