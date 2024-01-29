/*|============|*|
|*|Author:     |*| star fire
|*|Date:       |*| 2024-01-29
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// AInitializeOnLoad
    /// </summary>
    [InitializeOnLoad]
    internal static class AInitializeOnLoad
    {
        private static readonly Dictionary<int, Queue<MethodInfo>> MethodsEditor =
            new Dictionary<int, Queue<MethodInfo>>();

        private static readonly List<int> OrdersEditor = new List<int>();

        private static readonly Dictionary<int, Queue<MethodInfo>> MethodsRuntime =
            new Dictionary<int, Queue<MethodInfo>>();

        private static readonly List<int> OrdersRuntime = new List<int>();

        static AInitializeOnLoad()
        {
            foreach (var type in AHelper.Assembly.GetAllType())
            {
                if (type.Value.IsEnum) continue;
                if (type.Value.IsInterface) continue;
                foreach (var method in
                         type.Value.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (!method.IsStatic) continue;
                    if (method.IsAbstract) continue;
                    if (method.IsGenericMethod) continue;
                    if (method.GetParameters().Length > 0) continue;
                    var attribute = method.GetCustomAttribute<AInitAttribute>(false);
                    if (attribute is null) continue;

                    if (attribute.Mode == EInitAttrMode.Editor ||
                        attribute.Mode == EInitAttrMode.Both)
                    {
                        if (MethodsEditor.ContainsKey(attribute.Order))
                            MethodsEditor[attribute.Order].Enqueue(method);
                        else
                        {
                            MethodsEditor.Add(attribute.Order, new Queue<MethodInfo>());
                            MethodsEditor[attribute.Order].Enqueue(method);
                            OrdersEditor.Add(attribute.Order);
                        }
                    }

                    if (attribute.Mode == EInitAttrMode.Runtime ||
                        attribute.Mode == EInitAttrMode.Both)
                    {
                        if (MethodsRuntime.ContainsKey(attribute.Order))
                            MethodsRuntime[attribute.Order].Enqueue(method);
                        else
                        {
                            MethodsRuntime.Add(attribute.Order, new Queue<MethodInfo>());
                            MethodsRuntime[attribute.Order].Enqueue(method);
                            OrdersRuntime.Add(attribute.Order);
                        }
                    }
                }
            }

            OrdersEditor.Sort((a, b) => b.CompareTo(a));
            OrdersRuntime.Sort((a, b) => b.CompareTo(a));
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            foreach (var method in OrdersEditor.SelectMany(item => MethodsEditor[item]))
            {
                try
                {
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    throw new Exception($"AInitializeOnLoad: {method.Name} Error: {e.Message}");
                }
            }
        }

        [InitializeOnEnterPlayMode]
        [RuntimeInitializeOnLoadMethod]
        private static void RuntimeInitializeOnLoadMethod()
        {
            foreach (var method in OrdersRuntime.SelectMany(t => MethodsRuntime[t]))
            {
                try
                {
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    throw new Exception($"AInitializeOnLoad: {method.Name} Error: {e.Message}");
                }
            }
        }
    }
}