/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
            if (filePath.StartsWith(".\\Packages\\")) FilePath = filePath.Substring(2);
            else FilePath = filePath.Replace('\\', '/').Substring(Project);
        }
    }
}