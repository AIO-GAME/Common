/*|==========|*|
|*|Author:   |*| -> XINAN
|*|Date:     |*| -> 2023-06-12
|*|==========|*/

#if !UNITY
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

/// <summary>
/// DLL自动化处理
/// </summary>
public static partial class DllAutomation
{
    private static Dictionary<int, List<(DllAutomationAttribute, IDllAutomation)>> Dic;

    /// <summary>
    /// 存储 Dic 列表 从小到达排序
    /// </summary>
    private static List<int> Order;

    /// <summary>
    /// 接口名
    /// </summary>
    private const string InterfaceName = nameof(IDllAutomation);

    /// <summary>
    /// 初始化
    /// </summary>
    public static void Initialize(in Assembly assembly)
    {
        Dic = new Dictionary<int, List<(DllAutomationAttribute, IDllAutomation)>>();
        Order = new List<int>();

        foreach (var type in assembly.GetCustomAttributes<DllAutomationAttribute>())
        {
            if (type.SystemType.GetInterface(InterfaceName) != null)
            {
                if (Dic.ContainsKey(type.Priority))
                {
                    Dic[type.Priority].Add((type, (IDllAutomation)Activator.CreateInstance(type.SystemType)));
                }
                else
                {
                    Order.Add(type.Priority);
                    Dic.Add(type.Priority, new List<(DllAutomationAttribute, IDllAutomation)>());
                    Dic[type.Priority].Add((type, (IDllAutomation)Activator.CreateInstance(type.SystemType)));
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
        Dic = new Dictionary<int, List<(DllAutomationAttribute, IDllAutomation)>>();
        Order = new List<int>();
        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetCustomAttributes<DllAutomationAttribute>())
            {
                if (type.SystemType.GetInterface(InterfaceName) != null)
                {
                    if (Dic.ContainsKey(type.Priority))
                    {
                        Dic[type.Priority].Add((type, (IDllAutomation)Activator.CreateInstance(type.SystemType)));
                    }
                    else
                    {
                        Order.Add(type.Priority);
                        Dic.Add(type.Priority, new List<(DllAutomationAttribute, IDllAutomation)>());
                        Dic[type.Priority].Add((type, (IDllAutomation)Activator.CreateInstance(type.SystemType)));
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
            foreach (var (attr, automation) in Dic[index])
            {
                str.AppendLine(string.Format("Priority : {0} -> {1} -> {2}",
                    attr.Priority, attr.Name, attr.SystemType.FullName));
            }
        }

        return str.ToString();
    }

    /// <summary>
    /// 运行
    /// </summary>
    public static void Run()
    {
        foreach (var index in Order)
        {
            foreach (var (attribute, automation) in Dic[index])
            {
                try
                {
                    automation.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation Installation DLL: {0} -> Error : {1}", attribute.Name, ex);
                }
            }
        }
    }
}

#endif