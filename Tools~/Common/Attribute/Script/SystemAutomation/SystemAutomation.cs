#if !UNITY
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

/// <summary>
/// 系统自动化注册
/// </summary>
public static partial class SystemAutomation
{
    private static Dictionary<int, List<(SystemAutomationAttribute, ISystemAutomation)>> Dic;

    /// <summary>
    /// 存储Dic列表 从小到达排序
    /// </summary>
    private static List<int> Order;

    /// <summary>
    /// 接口名
    /// </summary>
    private const string InterfaceName = nameof(ISystemAutomation);

    /// <summary>
    /// 初始化
    /// </summary>
    public static void Initialize(in Assembly assembly)
    {
        Dic = new Dictionary<int, List<(SystemAutomationAttribute, ISystemAutomation)>>();
        Order = new List<int>();

        foreach (var type in assembly.GetCustomAttributes<SystemAutomationAttribute>())
        {
            if (type.SystemType.GetInterface(InterfaceName) != null)
            {
                if (Dic.ContainsKey(type.Priority))
                {
                    Dic[type.Priority].Add((type, (ISystemAutomation)Activator.CreateInstance(type.SystemType)));
                }
                else
                {
                    Order.Add(type.Priority);
                    Dic.Add(type.Priority, new List<(SystemAutomationAttribute, ISystemAutomation)>());
                    Dic[type.Priority].Add((type, (ISystemAutomation)Activator.CreateInstance(type.SystemType)));
                }
            }
        }

        Order.Sort((x, y) =>
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public static void Initialize(in IEnumerable<Assembly> assemblies)
    {
        Dic = new Dictionary<int, List<(SystemAutomationAttribute, ISystemAutomation)>>();
        Order = new List<int>();
        foreach (var assembly in assemblies)
        {
            foreach (var attribute in assembly.GetCustomAttributes<SystemAutomationAttribute>())
            {
                if (attribute.SystemType.GetInterface(InterfaceName) != null)
                {
                    if (Dic.ContainsKey(attribute.Priority))
                    {
                        Dic[attribute.Priority].Add((attribute, (ISystemAutomation)Activator.CreateInstance(attribute.SystemType)));
                    }
                    else
                    {
                        Order.Add(attribute.Priority);
                        Dic.Add(attribute.Priority, new List<(SystemAutomationAttribute, ISystemAutomation)>());
                        Dic[attribute.Priority].Add((attribute, (ISystemAutomation)Activator.CreateInstance(attribute.SystemType)));
                    }
                }
            }
        }

        Order.Sort((x, y) =>
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        });
    }

    /// <summary>
    /// 输出信息
    /// </summary>
    public static string OutPut()
    {
        var str = new StringBuilder();
        foreach (var index in Order)
        {
            str.AppendLine("Order : " + index + " -> ");
            foreach (var (attribute, automation) in Dic[index])
            {
                str.AppendLine(string.Format("Priority : {0} -> {1} -> {2}",
                    attribute.Priority, attribute.Name, attribute.SystemType.FullName));
            }
        }

        return str.ToString();
    }
}
#endif