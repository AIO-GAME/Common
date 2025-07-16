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

namespace AIO.UEditor
{
    /// <summary>
    /// AInitializeOnLoad
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
#if UNITY_EDITOR
            Debug.Log(MethodsPath.TryGetValue(method.MethodHandle.Value, out var tuple)
                          ? $"<color=#F7DC6F>[初始化] {mode} : </color> {method.ReflectedType.ToDetails()}:{method.Name} () (at {tuple.Item1}:{tuple.Item2})"
                          : $"<color=#F7DC6F>[初始化] {mode} : </color> {method.ReflectedType.ToDetails()} : {method.Name} ()"
                     );
#endif
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
                    OrdersEditor.Add(attr.Order);
                    MethodsEditor[attr.Order] = queue = new Queue<MethodInfo>();
                }

                queue.Enqueue(method);
            }
#endif

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeAfterSceneLoad))
            {
                if (MethodsRuntimeAfterSceneLoad.TryGetValue(attr.Order, out var queue))
                {
                    queue.Enqueue(method);
                }
                else
                {
                    OrdersRuntimeAfterSceneLoad.Add(attr.Order);
                    MethodsRuntimeAfterSceneLoad.Add(attr.Order, new Queue<MethodInfo>());
                    MethodsRuntimeAfterSceneLoad[attr.Order].Enqueue(method);
                }
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeBeforeSceneLoad))
            {
                if (MethodsRuntimeBeforeSceneLoad.TryGetValue(attr.Order, out var queue))
                {
                    queue.Enqueue(method);
                }
                else
                {
                    OrdersRuntimeBeforeSceneLoad.Add(attr.Order);
                    MethodsRuntimeBeforeSceneLoad.Add(attr.Order, new Queue<MethodInfo>());
                    MethodsRuntimeBeforeSceneLoad[attr.Order].Enqueue(method);
                }
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeAfterAssembliesLoaded))
            {
                if (MethodsRuntimeAfterAssembliesLoaded.TryGetValue(attr.Order, out var queue))
                {
                    queue.Enqueue(method);
                }
                else
                {
                    OrdersRuntimeAfterAssembliesLoaded.Add(attr.Order);
                    MethodsRuntimeAfterAssembliesLoaded.Add(attr.Order, new Queue<MethodInfo>());
                    MethodsRuntimeAfterAssembliesLoaded[attr.Order].Enqueue(method);
                }
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeBeforeSplashScreen))
            {
                if (MethodsRuntimeBeforeSplashScreen.TryGetValue(attr.Order, out var queue))
                {
                    queue.Enqueue(method);
                }
                else
                {
                    OrdersRuntimeBeforeSplashScreen.Add(attr.Order);
                    MethodsRuntimeBeforeSplashScreen.Add(attr.Order, new Queue<MethodInfo>());
                    MethodsRuntimeBeforeSplashScreen[attr.Order].Enqueue(method);
                }
            }

            if (attr.Mode.HasFlag(EInitAttrMode.RuntimeSubsystemRegistration))
            {
                if (MethodsRuntimeSubsystemRegistration.TryGetValue(attr.Order, out var queue))
                {
                    queue.Enqueue(method);
                }
                else
                {
                    OrdersRuntimeSubsystemRegistration.Add(attr.Order);
                    MethodsRuntimeSubsystemRegistration.Add(attr.Order, new Queue<MethodInfo>());
                    MethodsRuntimeSubsystemRegistration[attr.Order].Enqueue(method);
                }
            }
        }

        static AInitializeOnLoad()
        {
#if UNITY_EDITOR
            MethodsPath   = new Dictionary<IntPtr, Tuple<string, int>>();
            OrdersEditor  = new SortedSet<int>();
            MethodsEditor = new Dictionary<int, Queue<MethodInfo>>();
#endif
            OrdersRuntimeBeforeSceneLoad        = new SortedSet<int>();
            MethodsRuntimeBeforeSceneLoad       = new Dictionary<int, Queue<MethodInfo>>();
            OrdersRuntimeAfterSceneLoad         = new SortedSet<int>();
            MethodsRuntimeAfterSceneLoad        = new Dictionary<int, Queue<MethodInfo>>();
            OrdersRuntimeAfterAssembliesLoaded  = new SortedSet<int>();
            MethodsRuntimeAfterAssembliesLoaded = new Dictionary<int, Queue<MethodInfo>>();
            OrdersRuntimeBeforeSplashScreen     = new SortedSet<int>();
            MethodsRuntimeBeforeSplashScreen    = new Dictionary<int, Queue<MethodInfo>>();
            OrdersRuntimeSubsystemRegistration  = new SortedSet<int>();
            MethodsRuntimeSubsystemRegistration = new Dictionary<int, Queue<MethodInfo>>();

            foreach (var type in AHelper.Assembly.GetAllType())
            {
                if (type.Value.IsEnum) continue;
                if (type.Value.IsInterface) continue;
                foreach (var method in type.Value.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (!method.IsStatic) continue;
                    if (method.IsAbstract) continue;
                    if (method.IsGenericMethod) continue;
                    if (method.GetParameters().Length > 0) continue;
                    var attribute = method.GetCustomAttribute<AInitAttribute>(false);
                    if (attribute is null) continue;
                    Processing(attribute, method);
                }
            }
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod, Conditional("UNITY_EDITOR")]
        public static void InitializeOnLoadMethod()
        {
            foreach (var method in OrdersEditor.SelectMany(item => MethodsEditor[item]))
                try
                {
                    method.Invoke(null, null);
                    DebugLog(EInitAttrMode.Editor, method);
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.Editor, method, e);
                }

            EditorApplication.quitting += quit;
            return;

            void quit()
            {
                EditorApplication.quitting -= quit;
                MethodsEditor.Clear();
                OrdersEditor.Clear();
            }
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInitializeOnLoadMethod()
        {
            foreach (var method in OrdersRuntimeBeforeSceneLoad.SelectMany(item => MethodsRuntimeBeforeSceneLoad[item]))
                try
                {
                    method.Invoke(null, null);
                    DebugLog(EInitAttrMode.RuntimeBeforeSceneLoad, method);
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeBeforeSceneLoad, method, e);
                }

            Application.quitting += quit;
#if UNITY_EDITOR
            EditorApplication.quitting += quit;
#endif
            return;

            void quit()
            {
                Application.quitting -= quit;
#if UNITY_EDITOR
                EditorApplication.quitting -= quit;
#endif
                OrdersRuntimeBeforeSceneLoad.Clear();
                MethodsRuntimeBeforeSceneLoad.Clear();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void RuntimeInitializeAfterSceneLoadMethod()
        {
            foreach (var method in OrdersRuntimeAfterSceneLoad.SelectMany(item => MethodsRuntimeAfterSceneLoad[item]))
                try
                {
                    DebugLog(EInitAttrMode.RuntimeAfterSceneLoad, method);
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeAfterSceneLoad, method, e);
                }

            Application.quitting += quit;
#if UNITY_EDITOR
            EditorApplication.quitting += quit;
#endif
            return;

            void quit()
            {
                Application.quitting -= quit;
#if UNITY_EDITOR
                EditorApplication.quitting -= quit;
#endif
                OrdersRuntimeAfterSceneLoad.Clear();
                MethodsRuntimeAfterSceneLoad.Clear();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void RuntimeInitializeAfterAssembliesLoadedMethod()
        {
            foreach (var method in OrdersRuntimeAfterAssembliesLoaded.SelectMany(item => MethodsRuntimeAfterAssembliesLoaded[item]))
                try
                {
                    DebugLog(EInitAttrMode.RuntimeAfterAssembliesLoaded, method);
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeAfterAssembliesLoaded, method, e);
                }

            Application.quitting += quit;
#if UNITY_EDITOR
            EditorApplication.quitting += quit;
#endif
            return;

            void quit()
            {
                Application.quitting -= quit;
#if UNITY_EDITOR
                EditorApplication.quitting -= quit;
#endif
                OrdersRuntimeAfterAssembliesLoaded.Clear();
                MethodsRuntimeAfterAssembliesLoaded.Clear();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void RuntimeInitializeBeforeSplashScreenMethod()
        {
            foreach (var method in OrdersRuntimeBeforeSplashScreen.SelectMany(item => MethodsRuntimeBeforeSplashScreen[item]))
                try
                {
                    DebugLog(EInitAttrMode.RuntimeBeforeSplashScreen, method);
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeBeforeSplashScreen, method, e);
                }

            Application.quitting += quit;
#if UNITY_EDITOR
            EditorApplication.quitting += quit;
#endif
            return;

            void quit()
            {
                Application.quitting -= quit;
#if UNITY_EDITOR
                EditorApplication.quitting -= quit;
#endif
                OrdersRuntimeBeforeSplashScreen.Clear();
                MethodsRuntimeBeforeSplashScreen.Clear();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RuntimeInitializeSubsystemRegistrationMethod()
        {
            foreach (var method in OrdersRuntimeSubsystemRegistration.SelectMany(item => MethodsRuntimeSubsystemRegistration[item]))
                try
                {
                    DebugLog(EInitAttrMode.RuntimeSubsystemRegistration, method);
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    DebugError(EInitAttrMode.RuntimeSubsystemRegistration, method, e);
                }

            Application.quitting += quit;
#if UNITY_EDITOR
            EditorApplication.quitting += quit;
#endif
            return;

            void quit()
            {
                Application.quitting -= quit;
#if UNITY_EDITOR
                EditorApplication.quitting -= quit;
#endif
                OrdersRuntimeSubsystemRegistration.Clear();
                MethodsRuntimeSubsystemRegistration.Clear();
            }
        }
    }
}