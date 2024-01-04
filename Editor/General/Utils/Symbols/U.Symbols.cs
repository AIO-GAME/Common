/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan 
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        /// <summary>
        /// Symbols
        /// </summary>
        public static class Symbols
        {
            #region Get Scripting Define

            /// <summary>
            /// 获得当前平台已有的的宏定义
            /// </summary>
            public static ICollection<string> GetScriptingDefine()
            {
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                //获得当前平台已有的的宏定义
#if UNITY_2023_1_OR_NEWER
                var str =
 PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup)).Split(';');
#else
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
#endif
                if (str.Length == 0) return Array.Empty<string>();
                var verify = new List<string>(str);
                verify.RemoveRepeat();
                return verify;
            }

            /// <summary>
            /// 获得当前平台已有的的宏定义
            /// </summary>
            public static ICollection<string> GetScriptingDefine(BuildTargetGroup buildTargetGroup)
            {
                //获得当前平台已有的的宏定义
#if UNITY_2023_1_OR_NEWER
                var str =
 PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup)).Split(';');
#else
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
#endif
                if (str.Length == 0) return Array.Empty<string>();
                var verify = new List<string>(str);
                verify.RemoveRepeat();
                return verify;
            }

            #endregion

            #region Del Scripting Define

            /// <summary>
            /// 禁止传入的宏定义
            /// </summary>
            public static void DelScriptingDefine(string value)
            {
                if (string.IsNullOrEmpty(value)) return;
                DelScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, new string[] { value });
            }

            /// <summary>
            /// 禁止传入的宏定义
            /// </summary>
            public static void DelScriptingDefine(BuildTargetGroup buildTargetGroup, string value)
            {
                if (string.IsNullOrEmpty(value)) return;
                DelScriptingDefine(buildTargetGroup, new string[] { value });
            }

            /// <summary>
            /// 禁止传入的宏定义
            /// </summary>
            public static void DelScriptingDefine(params string[] value)
            {
                if (value is null || value.Length == 0) return;
                DelScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, value.ToList());
            }

            /// <summary>
            /// 禁止传入的宏定义
            /// </summary>
            public static void DelScriptingDefine(BuildTargetGroup buildTargetGroup, params string[] value)
            {
                if (value is null || value.Length == 0) return;
                DelScriptingDefine(buildTargetGroup, value.ToList());
            }

            /// <summary>
            /// 禁止传入的宏定义
            /// </summary>
            public static void DelScriptingDefine(ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                DelScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, value);
            }

            /// <summary>
            /// 禁止传入的宏定义
            /// </summary>
            public static void DelScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                //获得当前平台已有的的宏定义
#if UNITY_2023_1_OR_NEWER
                var str =
 PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup)).Split(';');
#else
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
#endif

                if (str.Length == 0) return;

                var verify = new List<string>(str);
                verify.RemoveRepeat();
                foreach (var item in value) verify.Remove(item);

                var content = "";
                if (verify.Count > 0) content = string.Join(";", verify);
#if UNITY_2023_1_OR_NEWER
                PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), content);
#else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, content);
#endif
            }

            #endregion

            #region Add Scripting Define

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(string value)
            {
                if (string.IsNullOrEmpty(value)) return;
                AddScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, new string[] { value });
            }

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, string value)
            {
                if (string.IsNullOrEmpty(value)) return;
                AddScriptingDefine(buildTargetGroup, new string[] { value });
            }

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(params string[] value)
            {
                if (value is null || value.Length == 0) return;
                AddScriptingDefine(value.ToList());
            }

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, params string[] value)
            {
                if (value is null || value.Length == 0) return;
                AddScriptingDefine(buildTargetGroup, value.ToList());
            }

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                AddScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, value);
            }

            /// <summary>
            /// 添加传入的宏定义
            /// </summary>
            public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                //获得当前平台已有的的宏定义
#if UNITY_2023_1_OR_NEWER
                var str =
 PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup)).Split(';');
#else
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
#endif

                var verify = new List<string>(str);
                foreach (var v in value)
                {
                    if (string.IsNullOrEmpty(v) || verify.Contains(v)) continue;
                    verify.Add(v);
                }

                verify.RemoveRepeat();
                //添加宏定义
#if UNITY_2023_1_OR_NEWER
                PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), string.Join(";", verify));
#else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", verify));
#endif
            }

            #endregion
        }
    }
}