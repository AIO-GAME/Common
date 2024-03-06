using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIO
{
    /// <summary>
    /// 脚本图标 请不要直接传入参数 请使用字段赋值
    /// </summary>
    /// <remarks>
    /// 用于标记脚本图标
    /// 如果使用局部类,则需要在局部类中标记
    /// </remarks>
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Enum |
        AttributeTargets.Struct |
        AttributeTargets.Interface |
        AttributeTargets.Delegate |
        AttributeTargets.Field |
        AttributeTargets.Event,
        AllowMultiple = true, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ScriptIconAttribute : Attribute
    {
        /// <summary>
        /// 相对路径图标 使用 AssetDatabase.LoadAssetAtPath 加载
        /// </summary>
        public string IconRelative { get; set; }

        /// <summary>
        /// 资源路径图标 使用 Resources.Load 加载
        /// </summary>
        public string IconResource { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        internal string FilePath { get; set; }

        /// <summary>
        /// 是否自动生成 持续更新
        /// </summary>
        public static bool AutoGenerate
        {
            get => PlayerPrefs.GetInt("ScriptIconAttribute.AutoGenerate", 1) == 1;
            set => PlayerPrefs.SetInt("ScriptIconAttribute.AutoGenerate", value ? 1 : 0);
        }

        private static int Project
        {
            get
            {
                if (_project == 0)
                    _project = Application.dataPath.LastIndexOf("/", StringComparison.CurrentCulture) + 1;
                return _project;
            }
        }

        private static int _project;

        public ScriptIconAttribute([CallerFilePath] string filePath = "")
        {
            FilePath = filePath.StartsWith(@".\Packages\")
                ? filePath.Substring(2)
                : filePath.Replace('\\', '/').Substring(Project);
        }
    }
}