/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 只读检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ReadOnlyAttribute : InspectorAttribute
    {
    }
}