
#if !NO_UNITY


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class DllAutomation
{
    /// <summary>
    /// 安装 dll
    /// </summary>
    /// <param name="hasTimer">输出时间</param>
    public static Task LoadAsync(bool hasTimer = false)
    {
        if (hasTimer) return InstallAsyncTimer();
        return InstallAsync();
    }

    /// <summary>
    /// 卸载 dll
    /// </summary>
    /// <param name="hasTimer">输出时间</param>
    public static Task UnLoadAsync(bool hasTimer = false)
    {
        if (hasTimer) return UnInstallAsyncTimer();
        return UninstallAsync();
    }

    #region Install

    private static async Task InstallAsyncTimer()
    {
        var watch = new Stopwatch();
        var allDic = new Dictionary<string, TimeSpan>();
        var sort = new Dictionary<int, string>();
        var num = 0;
        var allTimer = TimeSpan.Zero;

        foreach (var index in Order)
        {
            foreach (var (attribute, automation) in Dic[index])
            {
                var alltimelong = watch.Elapsed;
                try
                {
                    watch.Start();
                    await automation.InstallAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation Installation DLL : {0} -> Error : {1}", attribute.Name, ex);
                }
                finally
                {
                    watch.Stop();
                    var timer = watch.Elapsed - alltimelong;
                    var title = string.Format("{0} {1}", num, attribute.Name);
                    allDic.Add(title, timer);
                    sort.Add(num, title);
                    allTimer += timer;
                    num++;
                }
            }
        }

        Console.WriteLine("Automation Installation DLL Time Total -> <color=#E47833><b>{0:G}</b></color>", allTimer);
        for (var i = 0; i < num; i++)
        {
            var key = sort[i];
            var timer = allDic[key];
            var timeinfo = string.Format("[<color=#E47833><b>{1:00.0000}% | {0:G}</b></color>]", timer, (timer.Ticks / (double)allTimer.Ticks * 100)).PadRight(30);
            Console.WriteLine("Automation Installation DLL Time : {0} -> {1}", timeinfo, key);
        }
    }

    private static async Task InstallAsync()
    {
        foreach (var index in Order)
        {
            foreach (var (attribute, automation) in Dic[index])
            {
                try
                {
                    await automation.InstallAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation Installation DLL: {0} -> Error : {1}", attribute.Name, ex);
                }
            }
        }
    }

    #endregion

    #region UnInstall

    private static async Task UnInstallAsyncTimer()
    {
        var watch = new Stopwatch();
        var allDic = new Dictionary<string, TimeSpan>();
        var sort = new Dictionary<int, string>();
        var num = 0;
        var allTimer = TimeSpan.Zero;

        for (var i = Order.Count - 1; i >= 0; i--)
        {
            foreach (var (attribute, automation) in Dic[Order[i]])
            {
                var alltimelong = watch.Elapsed;
                try
                {
                    watch.Start();
                    await automation.UnInstallAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation UnInstallation DLL : {0} -> Error : {1}", attribute.Name, ex);
                }
                finally
                {
                    watch.Stop();
                    var timer = watch.Elapsed - alltimelong;
                    var title = string.Format("{0} {1}", num, attribute.Name);
                    allDic.Add(title, timer);
                    sort.Add(num, title);
                    allTimer += timer;
                    num++;
                }
            }
        }

        Console.WriteLine("Automation UnInstallation DLL Time Total -> <color=#E47833><b>{0:G}</b></color>", allTimer);
        for (var i = 0; i < num; i++)
        {
            var key = sort[i];
            var timer = allDic[key];
            var timeinfo = string.Format("[<color=#E47833><b>{1:00.0000}% | {0:G}</b></color>]", timer, (timer.Ticks / (double)allTimer.Ticks * 100)).PadRight(30);
            Console.WriteLine("Automation UnInstallation DLL Time : {0} -> {1}", timeinfo, key);
        }
    }

    private static async Task UninstallAsync()
    {
        for (var i = Order.Count - 1; i >= 0; i--)
        {
            foreach (var (attribute, automation) in Dic[Order[i]])
            {
                try
                {
                    await automation.UnInstallAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Automation UnInstallation DLL: {0} -> Error : {1}", attribute.Name, ex);
                }
            }
        }
    }

    #endregion
}
#endif