#if !UNITY_EDITOR && UNITY_2020_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.bbbirder;
using com.bbbirder.injection;
using UnityEngine;

namespace AIO.UEditor
{
    internal static class ProfilerHookTask
    {
        [AInit(EInitAttrMode.RuntimeAfterAssembliesLoaded, int.MinValue)]
        public static void Init()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var assemblyName = assembly.GetName().Name;
                if (FixHelper.fixedAssemblies.Contains(assemblyName)) continue;
                var injections = GetAllInjections(new[] { assembly });
                if (injections.Count <= 0) continue;
                foreach (var injection in injections) FixHelper.FixMethod(injection);
                FixHelper.fixedAssemblies.Add(assemblyName);
            }

            Debug.Log($"fixed {FixHelper.allInjections.Length} injections successfully!");
        }

        /// <summary>
        /// Get all injections in current domain.
        /// </summary>
        /// <returns></returns>
        private static ICollection<InjectionInfo> GetAllInjections(Assembly[] assemblies)
        {
            var injections = assemblies.
                    SelectMany(Retriever.GetAllAttributes<ProfilerScopeAttribute>).
                    Where(attr =>
                    {
                        if (attr.targetMember is not MethodInfo info) return false;
                        if (info.IsAbstract) return false;

                        return true;
                    }).
                    SelectMany(attr => attr.ProvideInjections())
                ;

            return injections.ToArray();
        }
    }
}
#endif