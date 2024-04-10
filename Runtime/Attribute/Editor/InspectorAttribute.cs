#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 类成员检视器特性
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    public abstract class InspectorAttribute : Attribute { }
}