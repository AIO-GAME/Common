﻿using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 标签检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public sealed class LabelAttribute : InspectorAttribute
    {
        public string Name { get; private set; }

        /// <summary>
        /// 标签检视器
        /// </summary>
        /// <param name="name">标签</param>
        public LabelAttribute(string name)
        {
            Name = name;
        }
    }
}