#if UNITY_EDITOR
using System.Diagnostics;
using System.Threading.Tasks;
using AIO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Timer_Test : MonoBehaviour
{
    public static int Count = 0;

    private void Awake()
    {
        UnityAsync.Initialize();
        TimerSystem.Initialize(10, 128);
    }

    public Stopwatch Watch;

    private void Start()
    {
        
    }

    private void OnGUI()
    {
        if (GULayout.Button("添加10000定时器"))
        {
            Watch = Stopwatch.StartNew();
            Task.Factory.StartNew(() =>
            {
                Debug.Log("-> 加入定时任务");
                for (var i = 1; i <= 100; i++)
                {
                    for (var j = 0; j < 10; j++)
                    {
                        TimerSystem.Push(10 * i, 10, Callback);
                    }
                }
            });
        }

        if (GULayout.Button("添加循环定时器"))
        {
            Watch = Stopwatch.StartNew();
            TimerSystem.PushLoop(1000, 1000, CallbackLoop);
        }

        if (GULayout.Button("移除循环定时器"))
        {
            TimerSystem.Pop(1000);
        }

        if (GULayout.Button("清空计数"))
        {
            Count = 0;
        }
    }

    private static long TotalValue;

    private void Callback100()
    {
        Debug.LogFormat("-> 任务完成 Duration {0} : Number -> {1} ", 1000, 1);
    }

    private void Callback200()
    {
        Debug.LogFormat("-> 任务完成 Duration {0} : Number -> {1} ", 500, 2);
    }

    private void CallbackLoop()
    {
        lock (this)
        {
            Debug.LogFormat("-> 任务完成 Duration {0} : Number -> {1} : Time -> {2}", 1000, ++Count, Watch.ElapsedMilliseconds);
        }
    }

    private void Callback()
    {
        // var wucha = time - ++Count * 1000;
        // TotalValue += wucha;
        // lock (this)
        {
            lock (this)
            {
                Count = Count + 1;
                if (Count >= 10000 * 0.9990f)
                {
                    Debug.Log("-> 任务完成 => " + Watch.ElapsedMilliseconds + "ms -> " + Count);
                }
            }
        }

        // Watch.Stop();
    }

    private void OnApplicationQuit()
    {
        if (Watch != null)
        {
            Watch.Stop();
            Debug.Log("-> 任务完成 => " + Watch.ElapsedMilliseconds + "ms -> " + Count);
        }


        TimerSystem.Dispose();
        UnityAsync.Dispose();
    }
}
#endif