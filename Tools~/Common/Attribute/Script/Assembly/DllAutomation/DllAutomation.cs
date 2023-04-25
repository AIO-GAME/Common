using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// DLL自动化处理
/// </summary>
public static class DllAutomation
{
    private static Dictionary<int, List<(DllAutomationAttribute, IDllAutomation)>> Dic;

    /// <summary>
    /// 存储Dic列表 从小到达排序
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

    #region Install

    /// <summary>
    /// 实现计时器
    /// </summary>
    public static async Task InstallTimer()
    {
        var watch = new Stopwatch();
        var allDic = new Dictionary<string, TimeSpan>();
        var sort = new Dictionary<int, string>();
        var num = 0;
        var allTimer = TimeSpan.Zero;

        for (int i = 0; i < Order.Count; i++)
        {
            foreach (var system in Dic[Order[i]])
            {
                var alltimelong = watch.Elapsed;
                try
                {
                    watch.Start();
                    await system.Item2.Install();
                    watch.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation Installation DLL : {0} -> Error : {1}", system.Item1.Name, ex.Message);
                    watch.Stop();
                }

                var timer = watch.Elapsed - alltimelong;
                var title = string.Format("{0} {1}", num, system.Item1.Name);
                allDic.Add(title, timer);
                sort.Add(num, title);
                allTimer += timer;
                num++;
            }
        }

        Console.WriteLine("Automation Installation DLL Time Total -> <color=#E47833><b>{0:G}</b></color>", allTimer);
        for (int i = 0; i < num; i++)
        {
            string key = sort[i];
            TimeSpan timer = allDic[key];
            var timeinfo = string.Format("[<color=#E47833><b>{1}% | {0:G}</b></color>]", timer, (timer.Ticks / (double)allTimer.Ticks * 100).ToString("00.0000")).PadRight(30);
            Console.WriteLine("Automation Installation DLL Time : {0} -> {1}", timeinfo, key);
        }

    }

    /// <summary>
    /// 实现计时器
    /// </summary>
    public static async Task Install()
    {
        for (int i = 0; i < Order.Count; i++)
        {
            foreach (var system in Dic[Order[i]])
            {
                try
                {
                    await system.Item2.Install();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation Installation DLL: {0} -> Error : {1}", system.Item1.Name, ex.Message);
                }
            }
        }
    }

    #endregion

    #region UnInstall

    /// <summary>
    /// 卸载 实现计时器
    /// </summary>
    public static async Task UnInstallTimer()
    {
        var watch = new Stopwatch();
        var allDic = new Dictionary<string, TimeSpan>();
        var sort = new Dictionary<int, string>();
        var num = 0;
        var allTimer = TimeSpan.Zero;

        for (int i = Order.Count - 1; i >= 0; i--)
        {
            foreach (var system in Dic[Order[i]])
            {
                var alltimelong = watch.Elapsed;
                try
                {
                    watch.Start();
                    await system.Item2.UnInstall();
                    watch.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation UnInstallation DLL : {0} -> Error : {1}", system.Item1.Name, ex.Message);
                    watch.Stop();
                }

                var timer = watch.Elapsed - alltimelong;
                var title = string.Format("{0} {1}", num, system.Item1.Name);
                allDic.Add(title, timer);
                sort.Add(num, title);
                allTimer += timer;
                num++;
            }
        }

        Console.WriteLine("Automation UnInstallation DLL Time Total -> <color=#E47833><b>{0:G}</b></color>", allTimer);
        for (int i = 0; i < num; i++)
        {
            string key = sort[i];
            TimeSpan timer = allDic[key];
            var timeinfo = string.Format("[<color=#E47833><b>{1}% | {0:G}</b></color>]", timer, (timer.Ticks / (double)allTimer.Ticks * 100).ToString("00.0000")).PadRight(30);
            Console.WriteLine("Automation UnInstallation DLL Time : {0} -> {1}", timeinfo, key);
        }

    }

    /// <summary>
    /// 实现计时器
    /// </summary>
    public static async Task Uninstall()
    {
        for (int i = Order.Count - 1; i >= 0; i--)
        {
            foreach (var system in Dic[Order[i]])
            {
                try
                {
                    await system.Item2.UnInstall();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation UnInstallation DLL: {0} -> Error : {1}", system.Item1.Name, ex.Message);
                }
            }
        }
    }

    #endregion
}