#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 通用表格绘制器（支持自定义复杂类型的数组、集合）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class GenericTableAttribute : InspectorAttribute { }
}