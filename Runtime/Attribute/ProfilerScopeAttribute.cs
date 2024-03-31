using System;
using System.Diagnostics;
#if !UNITY_EDITOR && UNITY_2020_1_OR_NEWER
using System.Reflection;
using AIO;
using com.bbbirder.injection;
using UnityEngine.Profiling;
#endif

/// <summary>
/// 用于标记方法的性能分析器范围
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
[Conditional("UNITY_EDITOR"), DebuggerNonUserCode]
public class ProfilerScopeAttribute
#if !UNITY_EDITOR && UNITY_2020_1_OR_NEWER
    : DecoratorAttribute
#else
    : Attribute
#endif
{
#if !UNITY_EDITOR && UNITY_2020_1_OR_NEWER
    public ProfilerScopeAttribute()
    {
    }

    private string title;

    private void OnCompleted()
    {
        Profiler.EndSample();
    }

    protected override R Decorate<R>(InvocationInfo<R> invocation)
    {
        if (string.IsNullOrEmpty(title))
        {
            if (targetMember is MethodInfo method)
            {
                var details = method.ToDetails();
                // 需要考虑泛型函数 将泛型函数的名称转换为泛型函数的名称
                title = string.Format("{0}.{1}({2}) - {3}",
                    method.DeclaringType.ToDetails(),
                    method.Name,
                    details.FullParameter,
                    details.ReturnType);
            }
        }

        Profiler.BeginSample(title);
        var r = invocation.FastInvoke(); // invoke original method
        if (IsAsyncMethod) // delay on async method
        {
            var awaiter = invocation.GetAwaiter(r);
            if (awaiter is null) OnCompleted();
            else awaiter.OnCompleted(OnCompleted);
        }
        else OnCompleted();

        return r;
    }
#endif
}