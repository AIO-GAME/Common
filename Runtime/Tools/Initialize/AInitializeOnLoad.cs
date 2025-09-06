#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

namespace AIO
{
    /// <summary>
    /// 标记 <see cref="AInitAttribute"/> 的方法会在编辑器加载和运行时初始化时调用
    /// 可多次标记，按Order顺序执行
    /// </summary>
    internal static class AInitializeOnLoad
    {
        private class Error : Exception
        {
            public Error(EInitAttrMode mode, MemberInfo method, Exception exception)
                : base($"{nameof(AInitializeOnLoad)} {mode} : {method.Name} Error: {exception.Message}", exception.InnerException) { }
        }

#if UNITY_2022_1_OR_NEWER
        [HideInCallstack]
#endif
        [Conditional("UNITY_EDITOR")]
        private static void DebugLog(EInitAttrMode mode, MethodBase method)
        {
            if (method.ReflectedType is null) throw new NullReferenceException();
            Debug.Log(MethodsPath.TryGetValue(method.MethodHandle.Value, out var tuple)
                          ? $"<color=#F7DC6F><b>[初始化] {mode}</b> : </color> {method.ReflectedType.ToDetails()} : {method.Name} () (at {tuple.Item1}:{tuple.Item2})"
                          : $"<color=#F7DC6F><b>[初始化] {mode}</b> : </color> {method.ReflectedType.ToDetails()} : {method.Name} ()"
                     );
        }

        private static void DebugError(EInitAttrMode mode, MemberInfo method, Exception e) { Debug.LogException(new Error(mode, method, e)); }

#if UNITY_EDITOR
        private static readonly SortedSet<int>                         OrdersEditor;
        private static readonly Dictionary<IntPtr, Tuple<string, int>> MethodsPath;
        private static readonly Dictionary<int, Queue<MethodInfo>>     MethodsEditor;
#endif

        private static readonly SortedSet<int> OrdersRuntimeBeforeSceneLoad;
        private static readonly SortedSet<int> OrdersRuntimeAfterSceneLoad;
        private static readonly SortedSet<int> OrdersRuntimeAfterAssembliesLoaded;
        private static readonly SortedSet<int> OrdersRuntimeBeforeSplashScreen;
        private static readonly SortedSet<int> OrdersRuntimeSubsystemRegistration;

        private static readonly Dictionary<int, Queue<MethodInfo>> MethodsRuntimeBeforeSceneLoad;
        private static readonly Dictionary<int, Queue<MethodInfo>> MethodsRuntimeAfterSceneLoad;
        private static readonly Dictionary<int, Queue<MethodInfo>> MethodsRuntimeAfterAssembliesLoaded;
        private static readonly Dictionary<int, Queue<MethodInfo>> MethodsRuntimeBeforeSplashScreen;
        private static readonly Dictionary<int, Queue<MethodInfo>> MethodsRuntimeSubsystemRegistration;

        private static void Processing(AInitAttribute attr, MethodInfo method)
        {
#if UNITY_EDITOR
            var root = Application.dataPath.Replace("Assets", "");
            MethodsPath[method.MethodHandle.Value] = new Tuple<string, int>(attr.FilePath.Replace(root, ""), attr.LineNumber);
            if (attr.Mode.HasFlag(EInitAttrMode.Editor))
            {
                if (!MethodsEditor.TryGetValue(attr.Order, out var queue))
                {
                    MethodsEditor[attr.Order] = queue = new Queue<MethodInfo>();
                    OrdersEditor.Add(attr.Order);
                }

                queue.Enqueue(method);
            }
#endif

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeAfterSceneLoad))
            {
                if (!MethodsRuntimeAfterSceneLoad.TryGetValue(attr.Order, out var queue))
                {
                    OrdersRuntimeAfterSceneLoad.Add(attr.Order);
                    queue = MethodsRuntimeAfterSceneLoad[attr.Order] = new Queue<MethodInfo>();
                }

                queue.Enqueue(method);
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeBeforeSceneLoad))
            {
                if (!MethodsRuntimeBeforeSceneLoad.TryGetValue(attr.Order, out var queue))
                {
                    OrdersRuntimeBeforeSceneLoad.Add(attr.Order);
                    queue = MethodsRuntimeBeforeSceneLoad[attr.Order] = new Queue<MethodInfo>();
                }

                queue.Enqueue(method);
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeAfterAssembliesLoaded))
            {
                if (!MethodsRuntimeAfterAssembliesLoaded.TryGetValue(attr.Order, out var queue))
                {
                    OrdersRuntimeAfterAssembliesLoaded.Add(attr.Order);
                    MethodsRuntimeAfterAssembliesLoaded[attr.Order] = new Queue<MethodInfo>();
                }

                queue.Enqueue(method);
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeBeforeSplashScreen))
            {
                if (!MethodsRuntimeBeforeSplashScreen.TryGetValue(attr.Order, out var queue))
                {
                    OrdersRuntimeBeforeSplashScreen.Add(attr.Order);
                    MethodsRuntimeBeforeSplashScreen[attr.Order] = new Queue<MethodInfo>();
                }

                queue.Enqueue(method);
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeSubsystemRegistration))
            {
                if (!MethodsRuntimeSubsystemRegistration.TryGetValue(attr.Order, out var queue))
                {
                    OrdersRuntimeSubsystemRegistration.Add(attr.Order);
                    MethodsRuntimeSubsystemRegistration[attr.Order] = new Queue<MethodInfo>();
                }

                queue.Enqueue(method);
            }
        }

        static AInitializeOnLoad()
        {
#if UNITY_EDITOR
            OrdersEditor  = new SortedSet<int>();
            MethodsPath   = new Dictionary<IntPtr, Tuple<string, int>>();
            MethodsEditor = new Dictionary<int, Queue<MethodInfo>>();
#endif

            OrdersRuntimeBeforeSceneLoad       = new SortedSet<int>();
            OrdersRuntimeAfterSceneLoad        = new SortedSet<int>();
            OrdersRuntimeAfterAssembliesLoaded = new SortedSet<int>();
            OrdersRuntimeBeforeSplashScreen    = new SortedSet<int>();
            OrdersRuntimeSubsystemRegistration = new SortedSet<int>();

            MethodsRuntimeBeforeSceneLoad       = new Dictionary<int, Queue<MethodInfo>>();
            MethodsRuntimeAfterSceneLoad        = new Dictionary<int, Queue<MethodInfo>>();
            MethodsRuntimeAfterAssembliesLoaded = new Dictionary<int, Queue<MethodInfo>>();
            MethodsRuntimeBeforeSplashScreen    = new Dictionary<int, Queue<MethodInfo>>();
            MethodsRuntimeSubsystemRegistration = new Dictionary<int, Queue<MethodInfo>>();

            var dict = new Dictionary<MethodInfo, AInitAttribute>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsEnum) continue;
                    if (type.IsInterface) continue;
                    foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (!method.IsStatic) continue;
                        if (method.IsAbstract) continue;
                        if (method.IsGenericMethod) continue;
                        if (method.GetParameters().Length > 0) continue;
                        var attribute = method.GetCustomAttribute<AInitAttribute>(false);
                        if (attribute is null) continue;
                        dict[method] = attribute;
                    }
                }
            }

            foreach (var kvp in dict)
            {
                try { Processing(kvp.Value, kvp.Key); }
                catch (Exception e)
                {
                    DebugError(kvp.Value.Mode, kvp.Key, e);
                }
            }
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod, Conditional("UNITY_EDITOR")]
        public static void InitializeOnLoadMethod()
        {
            foreach (var method in OrdersEditor.SelectMany(item => MethodsEditor[item]))
            {
                EditorUtility.DisplayProgressBar("初始化", $"{method.DeclaringType?.FullName}:{method.Name} ()", 0f);
                try
                {
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.Editor, method, e);
                }

                EditorUtility.DisplayProgressBar("初始化", $"{method.DeclaringType?.FullName}:{method.Name} ()", 1f);
            }

            EditorUtility.ClearProgressBar();
            Application.quitting       += InitializeOnLoadMethodQuit;
            EditorApplication.quitting += InitializeOnLoadMethodQuit;
        }

        private static void InitializeOnLoadMethodQuit()
        {
            Application.quitting       -= InitializeOnLoadMethodQuit;
            EditorApplication.quitting -= InitializeOnLoadMethodQuit;
            MethodsEditor.Clear();
            OrdersEditor.Clear();
            OrdersRuntimeSubsystemRegistration.Clear();
            MethodsRuntimeSubsystemRegistration.Clear();
            OrdersRuntimeBeforeSplashScreen.Clear();
            MethodsRuntimeBeforeSplashScreen.Clear();
            OrdersRuntimeAfterAssembliesLoaded.Clear();
            MethodsRuntimeAfterAssembliesLoaded.Clear();
            OrdersRuntimeBeforeSceneLoad.Clear();
            MethodsRuntimeBeforeSceneLoad.Clear();
            OrdersRuntimeAfterSceneLoad.Clear();
            MethodsRuntimeAfterSceneLoad.Clear();
        }

#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInitializeOnLoadMethod()
        {
            foreach (var method in OrdersRuntimeBeforeSceneLoad.SelectMany(item => MethodsRuntimeBeforeSceneLoad[item]))
            {
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("场景加载前", $"{method.DeclaringType?.FullName}:{method.Name} ()", 0f);
#endif
                try
                {
                    method.Invoke(null, null);
#if !UNITY_EDITOR
                    DebugLog(EInitAttrMode.RuntimeBeforeSceneLoad, method);
#endif
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeBeforeSceneLoad, method, e);
                }
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("场景加载前", $"{method.DeclaringType?.FullName}:{method.Name} ()", 1f);
#endif
            }

#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
#endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void RuntimeInitializeAfterSceneLoadMethod()
        {
            foreach (var method in OrdersRuntimeAfterSceneLoad.SelectMany(item => MethodsRuntimeAfterSceneLoad[item]))
            {
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("场景加载后", $"{method.DeclaringType?.FullName}:{method.Name} ()", 0f);
#endif
                try
                {
                    method.Invoke(null, null);
#if !UNITY_EDITOR
                    DebugLog(EInitAttrMode.RuntimeAfterSceneLoad, method);
#endif
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeAfterSceneLoad, method, e);
                }
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("场景加载后", $"{method.DeclaringType?.FullName}:{method.Name} ()", 1f);
#endif
            }

#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
#endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void RuntimeInitializeAfterAssembliesLoadedMethod()
        {
            foreach (var method in OrdersRuntimeAfterAssembliesLoaded.SelectMany(item => MethodsRuntimeAfterAssembliesLoaded[item]))
            {
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("程序加载完毕后", $"{method.DeclaringType?.FullName}:{method.Name} ()", 0f);
#endif
                try
                {
                    method.Invoke(null, null);
#if !UNITY_EDITOR
                    DebugLog(EInitAttrMode.RuntimeAfterAssembliesLoaded, method);
#endif
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeAfterAssembliesLoaded, method, e);
                }
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("程序加载完毕后", $"{method.DeclaringType?.FullName}:{method.Name} ()", 1f);
#endif
            }
#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
#endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void RuntimeInitializeBeforeSplashScreenMethod()
        {
            foreach (var method in OrdersRuntimeBeforeSplashScreen.SelectMany(item => MethodsRuntimeBeforeSplashScreen[item]))
            {
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("启动画面之前", $"{method.DeclaringType?.FullName}:{method.Name} ()", 0f);
#endif
                try
                {
                    method.Invoke(null, null);
#if !UNITY_EDITOR
                    DebugLog(EInitAttrMode.RuntimeBeforeSplashScreen, method);
#endif
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeBeforeSplashScreen, method, e);
                }
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("启动画面之前", $"{method.DeclaringType?.FullName}:{method.Name} ()", 1f);
#endif
            }
#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
#endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RuntimeInitializeSubsystemRegistrationMethod()
        {
            foreach (var method in OrdersRuntimeSubsystemRegistration.SelectMany(item => MethodsRuntimeSubsystemRegistration[item]))
            {
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("子系统注册", $"{method.DeclaringType?.FullName}:{method.Name} ()", 0f);
#endif
                try
                {
                    method.Invoke(null, null);
#if !UNITY_EDITOR
                    DebugLog(EInitAttrMode.RuntimeSubsystemRegistration, method);
#endif
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeSubsystemRegistration, method, e);
                }
#if UNITY_EDITOR
                EditorUtility.DisplayProgressBar("子系统注册", $"{method.DeclaringType?.FullName}:{method.Name} ()", 1f);
#endif
            }


#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
#endif
        }
    }
}