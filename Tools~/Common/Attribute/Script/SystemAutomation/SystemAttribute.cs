#if !UNITY
using System;

/// <summary>
/// 系统自动化注册属性
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public class SystemAutomationAttribute : Attribute
{
    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 系统类型
    /// </summary>
    public Type SystemType { get; }

    /// <summary>
    /// 初始化
    /// </summary>
    public SystemAutomationAttribute(Type system, string name, int priority)
    {
        Priority = priority;
        SystemType = system;
        Name = name;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public SystemAutomationAttribute(Type system, int priority)
    {
        SystemType = system;
        Priority = priority;
        Name = system.Name;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public SystemAutomationAttribute(Type system, string name)
    {
        SystemType = system;
        Priority = 0;
        Name = name;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public SystemAutomationAttribute(Type system)
    {
        SystemType = system;
        Priority = 0;
        Name = system.Name;
    }
}
#endif