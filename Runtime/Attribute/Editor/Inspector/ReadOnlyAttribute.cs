﻿using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 只读检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ReadOnlyAttribute : InspectorAttribute
    {
    }
}