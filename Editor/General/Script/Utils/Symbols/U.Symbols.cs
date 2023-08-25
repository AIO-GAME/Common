/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using AIO;
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
            /// <summary>
            /// 禁止你想要的宏定义
            /// </summary>
            public static void DelScriptingDefine(params string[] value)
            {
                if (value is null || value.Length == 0) return;
                //获取当前是哪个平台
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                //获得当前平台已有的的宏定义
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                if (str.Length == 0) return;

                var verify = new List<string>(str);
                verify.RemoveRepeat();
                foreach (var item in value) verify.Remove(item);

                if (verify.Count > 0)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", verify));
                }
                else PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, "");
            }

            /// <summary>
            /// 获得当前平台已有的的宏定义
            /// </summary>
            public static ICollection<string> GetScriptingDefine()
            {
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                //获得当前平台已有的的宏定义
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                if (str.Length == 0) return Array.Empty<string>();
                var verify = new List<string>(str);
                verify.RemoveRepeat();
                return verify;
            }

            /// <summary>
            /// 禁止你想要的宏定义
            /// </summary>
            public static void DelScriptingDefine(ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                //获取当前是哪个平台
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                //获得当前平台已有的的宏定义
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                if (str.Length == 0) return;

                var verify = new List<string>(str);
                verify.RemoveRepeat();
                foreach (var item in value) verify.Remove(item);

                if (verify.Count > 0)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", verify));
                }
                else PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, "");
            }

            /// <summary>
            /// 添加你想要的宏定义
            /// </summary>
            public static void AddScriptingDefine(params string[] value)
            {
                if (value is null || value.Length == 0) return;
                //获取当前是哪个平台
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                //获得当前平台已有的的宏定义
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                var verify = new List<string>(str);
                foreach (var v in value)
                {
                    if (string.IsNullOrEmpty(v) || verify.Contains(v)) continue;
                    verify.Add(v);
                }

                verify.RemoveRepeat();
                //添加宏定义
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", verify));
            }

            /// <summary>
            /// 添加你想要的宏定义
            /// </summary>
            public static void AddScriptingDefine(ICollection<string> value)
            {
                if (value is null || value.Count == 0) return;
                //获取当前是哪个平台
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                //获得当前平台已有的的宏定义
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                var verify = new List<string>(str);
                foreach (var v in value)
                {
                    if (string.IsNullOrEmpty(v) || verify.Contains(v)) continue;
                    verify.Add(v);
                }

                verify.RemoveRepeat();
                //添加宏定义
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", verify));
            }
        }
    }
}
