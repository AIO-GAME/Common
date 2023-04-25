using System;
using System.Reflection;

/// <summary>
/// DLL 注入属性
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public class DllAutomationAttribute : Attribute
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
    public DllAutomationAttribute(Type system, string name, int priority = 0)
    {
        Priority = priority;
        SystemType = system;
        Name = name;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public DllAutomationAttribute(Type system, int priority = 0)
    {
        SystemType = system;
        Priority = priority;
        Name = Assembly.GetAssembly(system).GetName().Name;
    }
}