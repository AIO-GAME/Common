﻿#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 只读检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class ReadOnlyAttribute : InspectorAttribute { }
}