﻿#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 公共属性检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Property), Conditional("UNITY_EDITOR")]
    public sealed class PropertyDisplayAttribute : InspectorAttribute
    {
        /// <summary>
        /// 公共属性检视器
        /// </summary>
        /// <param name="text">显示名称</param>
        /// <param name="displayOnlyRuntime">是否仅在编辑器运行时显示</param>
        public PropertyDisplayAttribute(string text = null, bool displayOnlyRuntime = true)
        {
            Text               = text;
            DisplayOnlyRuntime = displayOnlyRuntime;
        }

        public string Text               { get; private set; }
        public bool   DisplayOnlyRuntime { get; private set; }
    }
}