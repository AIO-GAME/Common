/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        /// <summary>
        /// 设置功能函数
        /// </summary>
        public static class Setting
        {
            private static ICollection<string> GetScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup)
            {
                //获得当前平台已有的的宏定义
                var GetScriptingDefineSymbols = typeof(PlayerSettings).GetMethod("GetScriptingDefineSymbolsInternal",
                    BindingFlags.Static | BindingFlags.NonPublic);
                string str = null;
                if (GetScriptingDefineSymbols != null)
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (!assembly.GetName().Name.StartsWith("UnityEditor.Build")) continue;
                        var namedBuildTargetType = assembly.GetType("UnityEditor.Build.NamedBuildTarget");
                        var FromBuildTargetGroupMethod = namedBuildTargetType?.GetMethod("FromBuildTargetGroup",
                            BindingFlags.Static | BindingFlags.Public);
                        if (FromBuildTargetGroupMethod is null) continue;
                        var symbols = FromBuildTargetGroupMethod.Invoke(null, new object[] { buildTargetGroup });
                        str = GetScriptingDefineSymbols.Invoke(null, new object[] { symbols }) as string;
                        break;
                    }
                }
                else str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

                return string.IsNullOrEmpty(str) ? Array.Empty<string>() : str.Split(';');
            }

            private static void SetScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup,
                IEnumerable<string> verify)
            {
                //获得当前平台已有的的宏定义
                MethodInfo SetScriptingDefineSymbols = null;
                foreach (var methodInfo in typeof(PlayerSettings).GetMethods(BindingFlags.Static | BindingFlags.Public))
                {
                    if (methodInfo.Name != "SetScriptingDefineSymbols") continue;
                    var parameters = methodInfo.GetParameters();
                    if (parameters.Length != 2) continue;
                    if (parameters[0].ParameterType != typeof(string)) continue;
                    if (parameters[1].ParameterType != typeof(string)) continue;
                    SetScriptingDefineSymbols = methodInfo;
                }

                var str = string.Join(";", verify);
                if (SetScriptingDefineSymbols != null)
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (!assembly.GetName().Name.StartsWith("UnityEditor.Build")) continue;
                        var namedBuildTargetType = assembly.GetType("UnityEditor.Build.NamedBuildTarget");
                        var FromBuildTargetGroupMethod = namedBuildTargetType?.GetMethod("FromBuildTargetGroup",
                            BindingFlags.Static | BindingFlags.Public);
                        if (FromBuildTargetGroupMethod is null) continue;
                        var Symbols = FromBuildTargetGroupMethod.Invoke(null, new object[] { buildTargetGroup });
                        SetScriptingDefineSymbols.Invoke(null, new object[] { Symbols, str });

                        break;
                    }
                }
                else PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
            }

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                Debug.Log($"Plugins Data Editor : AddScriptingDefine -> {buildTargetGroup}");
                var verify = new List<string>(GetScriptingDefineSymbolsForGroup(buildTargetGroup));
                foreach (var v in value)
                {
                    if (string.IsNullOrEmpty(v) || verify.Contains(v)) continue;
                    verify.Add(v);
                }

                SetScriptingDefineSymbolsForGroup(buildTargetGroup, verify.RemoveRepeat());
            }

            /// <summary>
            /// 禁止传入的宏定义
            /// </summary>
            public static void DelScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                Debug.Log($"Plugins Data Editor : DelScriptingDefine -> {buildTargetGroup}");
                var str = GetScriptingDefineSymbolsForGroup(buildTargetGroup);
                if (str.Count == 0) return;
                IList<string> verify = new List<string>(str);
                verify = verify.RemoveRepeat();
                foreach (var item in value) verify.Remove(item);
                SetScriptingDefineSymbolsForGroup(buildTargetGroup, verify);
            }
        }
    }
}