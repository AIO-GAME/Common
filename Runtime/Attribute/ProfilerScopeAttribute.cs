using System;
using System.Diagnostics;

/// <summary>
/// 用于标记方法的性能分析器范围
/// </summary>
[AttributeUsage(
    AttributeTargets.Method |
    AttributeTargets.Constructor |
    AttributeTargets.Assembly |
    AttributeTargets.Property,
    Inherited = false)]
[Conditional("UNITY_EDITOR"), DebuggerNonUserCode]
public class ProfilerScopeAttribute : Attribute
{
}