/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_2023_1_OR_NEWER
using UnityEditor.Build;
#endif

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
                var str =
#if UNITY_2023_1_OR_NEWER
                    PlayerSettings.GetScriptingDefineSymbols(
                        NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));
#else
                    PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
#endif

                return string.IsNullOrEmpty(str) ? Array.Empty<string>() : str.Split(';');
            }

            private static void SetScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup,
                IEnumerable<string> verify)
            {
                var str = string.Join(";", verify);
#if UNITY_2023_1_OR_NEWER
                PlayerSettings.SetScriptingDefineSymbols(
                    NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), str);
#else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
#endif
            }

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
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
