/*|==========|*|
|*|Author:   |*| -> SAM
|*|Date:     |*| -> 2023-06-12
|*|==========|*/

#if NO_UNITY

using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class DllAutomation
{
    /// <summary>
    /// 安装 dll
    /// </summary>
    /// <param name="hasTimer">输出时间</param>
    public static IEnumerator LoadSync(bool hasTimer = false)
    {
        if (hasTimer) yield return InstallSyncTimer();
        else yield return InstallSync();
    }

    /// <summary>
    /// 卸载 dll
    /// </summary>
    /// <param name="hasTimer">输出时间</param>
    public static IEnumerator UnLoadSync(bool hasTimer = false)
    {
        if (hasTimer) yield return UnInstallSyncTimer();
        else yield return UninstallSync();
    }

    #region Install

    private static IEnumerator InstallSyncTimer()
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
                watch.Start();
                yield return automation.InstallSync();
                watch.Stop();
                var timer = watch.Elapsed - alltimelong;
                var title = string.Format("{0} {1}", num, attribute.Name);
                allDic.Add(title, timer);
                sort.Add(num, title);
                allTimer += timer;
                num++;
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

        yield break;
    }

    private static IEnumerator InstallSync()
    {
        foreach (var index in Order)
        {
            foreach (var (attribute, automation) in Dic[index])
            {
                yield return automation.InstallSync();
            }
        }

        yield break;
    }

    #endregion

    #region UnInstall

    private static IEnumerator UnInstallSyncTimer()
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
                watch.Start();
                yield return automation.UnInstallSync();
                watch.Stop();
                var timer = watch.Elapsed - alltimelong;
                var title = string.Format("{0} {1}", num, attribute.Name);
                allDic.Add(title, timer);
                sort.Add(num, title);
                allTimer += timer;
                num++;
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

        yield break;
    }

    private static IEnumerator UninstallSync()
    {
        for (var i = Order.Count - 1; i >= 0; i--)
        {
            foreach (var (attribute, automation) in Dic[Order[i]])
            {
                yield return automation.UnInstallSync();
            }
        }

        yield break;
    }

    #endregion
}
#endif