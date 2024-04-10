#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 密码检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class PasswordAttribute : InspectorAttribute { }
}