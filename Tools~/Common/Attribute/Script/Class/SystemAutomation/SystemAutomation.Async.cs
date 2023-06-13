/*|==========|*|
|*|Author:   |*| -> SAM
|*|Date:     |*| -> 2023-06-12
|*|==========|*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class SystemAutomation
{
    /// <summary>
    /// 安装 
    /// </summary>
    /// <param name="hasTimer">输出时间</param>
    public static Task LoadAsync(bool hasTimer = false)
    {
        if (hasTimer) return InstallAsyncTimer();
        return InstallAsync();
    }

    /// <summary>
    /// 卸载 
    /// </summary>
    /// <param name="hasTimer">输出时间</param>
    public static Task UnLoadAsync(bool hasTimer = false)
    {
        if (hasTimer) return UnInstallAsync();
        return UnInstallAsyncTimer();
    }

    /// <summary>
    /// 异常信息
    /// </summary>
    public static event Action<SystemAutomationAttribute, Exception> OnException;

    /// <summary>
    /// 成功信息
    /// </summary>
    public static event Action<SystemAutomationAttribute> OnSuccessful;

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
                    if (OnSuccessful != null) OnSuccessful.Invoke(attribute);
                    else Console.WriteLine($"Automation {nameof(InstallAsyncTimer)} System : {attribute.Name} -> Successful");
                }
                catch (Exception ex)
                {
                    if (OnException != null) OnException.Invoke(attribute, ex);
                    else Console.WriteLine($"Automation {nameof(InstallAsyncTimer)} System : {attribute.Name} -> Error : {ex}");
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

        Console.WriteLine("Automation Installation System Time Total -> <color=#E47833><b>{0:G}</b></color>", allTimer);
        for (var i = 0; i < num; i++)
        {
            var key = sort[i];
            var timer = allDic[key];
            var timeinfo = string.Format("[<color=#E47833><b>{1:00.0000}% | {0:G}</b></color>]",
                timer, (timer.Ticks / (double)allTimer.Ticks * 100)).PadRight(30);
            Console.WriteLine("Automation Installation System Time : {0} -> {1}", timeinfo, key);
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
                    if (OnSuccessful != null) OnSuccessful.Invoke(attribute);
                    else Console.WriteLine($"Automation {nameof(InstallAsync)} System : {attribute.Name} -> Successful");
                }
                catch (Exception ex)
                {
                    if (OnException != null) OnException.Invoke(attribute, ex);
                    else Console.WriteLine($"Automation {nameof(InstallAsync)} System : {attribute.Name} -> Error : {ex}");
                }
            }
        }
    }

    #endregion

    #region Uninstall

    private static async Task UnInstallAsync()
    {
        for (var i = Order.Count - 1; i >= 0; i--)
        {
            foreach (var (attribute, automation) in Dic[Order[i]])
            {
                try
                {
                    await automation.UnInstallAsync();
                    if (OnSuccessful != null) OnSuccessful.Invoke(attribute);
                    else Console.WriteLine($"Automation {nameof(UnInstallAsync)} System : {attribute.Name} -> Successful");
                }
                catch (Exception ex)
                {
                    if (OnException != null) OnException.Invoke(attribute, ex);
                    else Console.WriteLine($"Automation {nameof(UnInstallAsync)} System : {attribute.Name} -> Error : {ex}");
                }
            }
        }
    }

    private static async Task UnInstallAsyncTimer()
    {
        var watch = new Stopwatch();
        var allDic = new Dictionary<string, TimeSpan>();
        var sort = new Dictionary<int, string>();
        var num = 0;
        var allTimer = TimeSpan.Zero;

        for (var i = Order.Count - 1; i >= 0; i--)
        {
            foreach (var (attr, auto) in Dic[Order[i]])
            {
                var alltimelong = watch.Elapsed;
                try
                {
                    watch.Start();
                    await auto.UnInstallAsync();
                    if (OnSuccessful != null) OnSuccessful.Invoke(attr);
                    else Console.WriteLine($"Automation {nameof(UnInstallAsyncTimer)} System : {attr.Name} -> Successful");
                }
                catch (Exception ex)
                {
                    if (OnException != null) OnException.Invoke(attr, ex);
                    else Console.WriteLine($"Automation {nameof(UnInstallAsyncTimer)} System : {attr.Name} -> Error : {ex}");
                }
                finally
                {
                    watch.Stop();
                    var timer = watch.Elapsed - alltimelong;
                    var title = string.Format("{0} {1}", num, attr.Name);
                    allDic.Add(title, timer);
                    sort.Add(num, title);
                    allTimer += timer;
                    num++;
                }
            }
        }

        Console.WriteLine("Automation UnInstallation System Time Total -> <color=#E47833><b>{0:G}</b></color>", allTimer);
        for (var i = 0; i < num; i++)
        {
            var key = sort[i];
            var timer = allDic[key];
            var timeinfo = string.Format("[<color=#E47833><b>{1:00.0000}% | {0:G}</b></color>]",
                timer, (timer.Ticks / (double)allTimer.Ticks * 100)).PadRight(30);
            Console.WriteLine("Automation UnInstallation System Time : {0} -> {1}", timeinfo, key);
        }
    }

    #endregion
}