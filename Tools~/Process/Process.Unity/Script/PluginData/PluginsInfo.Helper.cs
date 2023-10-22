using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using Object = UnityEngine.Object;


#pragma warning disable CS1591
namespace AIO
{
    internal partial class Plugins
    {
        internal static class Helper
        {
            internal static Action CB;

            private static PluginDataWindow Window;

            [MenuItem("Tools/Window/Plugin Data Manager #_F12")]
            public static void Open()
            {
                if (Window is null)
                {
                    Window = ScriptableObject.CreateInstance<PluginDataWindow>();
                    Window.titleContent = new GUIContent("插件管理界面", "Plugin Data Manager");
                    Window.minSize = new Vector2(200, 600);
                }

                Window.Show(true);
                Window.Focus();
                Window.Repaint();
            }

            public static void Close()
            {
                if (Window != null) Window.Close();
                Window = null;
            }

            /// <summary>
            /// 获取指定文件夹下的预制件
            /// </summary>
            /// <param name="pattern">匹配模式</param>
            /// <param name="folder">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<T> GetAssetsRes<T>(in string pattern, params string[] folder)
                where T : Object
            {
                if (string.IsNullOrEmpty(pattern)) return Array.Empty<T>();

                return AssetDatabase.FindAssets(pattern, folder)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .Where(value => value != null);
            }

            public static T GetOrDefault<T>(IDictionary dic, in object key, in T defaultValue)
            {
                if (dic == null)
                    throw new ArgumentNullException(nameof(dic));
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                return dic.Contains(key) && dic[key] is T obj ? obj : defaultValue;
            }

            /// <summary>
            /// 移除重复元素
            /// </summary>
            private static IList<T> RemoveRepeat<T>(IList<T> array)
            {
                if (array is null) return Array.Empty<T>();
                if (array.Count <= 1) return array;
                var hashSet = new HashSet<T>();
                for (var i = 0; i < array.Count; i++)
                {
                    var num = array[i];
                    if (hashSet.Count == 0 || !hashSet.Contains(num))
                    {
                        hashSet.Add(num);
                    }
                    else if (array is List<T> list)
                    {
                        list.RemoveAll(x => Equals(x, num));
                    }
                    else
                    {
                        array.RemoveAt(i--);
                    }
                }

                return array;
            }

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

                SetScriptingDefineSymbolsForGroup(buildTargetGroup, RemoveRepeat(verify));
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
                verify = RemoveRepeat(verify);
                foreach (var item in value) verify.Remove(item);
                SetScriptingDefineSymbolsForGroup(buildTargetGroup, verify);
            }

            /// <summary>
            /// 刷新设置
            /// </summary>
            public static void RefreshSettings()
            {
                var refreshSettingsMethodInfo = typeof(AssetDatabase).GetMethod("RefreshSettings",
                    BindingFlags.Static | BindingFlags.Public);
                if (refreshSettingsMethodInfo != null)
                {
                    Debug.Log("Plugins Data Editor : AssetDatabase RefreshSettings Start");
                    refreshSettingsMethodInfo.Invoke(null, null);
                }
            }

            /// <summary>
            /// 编译程序集
            /// </summary>
            public static void CompilationPipelineRequestScriptCompilation()
            {
                var requestScriptCompilationMethodInfo = typeof(CompilationPipeline)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(method => method.Name == "RequestScriptCompilation")
                    .FirstOrDefault(method => method.GetParameters().Length <= 0);
                if (requestScriptCompilationMethodInfo != null)
                {
                    Debug.Log("Plugins Data Editor : CompilationPipeline RequestScriptCompilation Start");
                    requestScriptCompilationMethodInfo.Invoke(null, null);
                }
            }

            public static void CompilationPipelineCompilationStartedBegin()
            {
                var compilationStarted = typeof(CompilationPipeline).GetEvent("compilationStarted", BindingFlags.Static | BindingFlags.Public);
                if (compilationStarted != null)
                {
                    var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationStartedEnd), BindingFlags.Static | BindingFlags.NonPublic);
                    var Events = Delegate.CreateDelegate(compilationStarted.EventHandlerType, null, methodInfo);
                    Debug.Log("Plugins Data Editor : CompilationPipelineCompilationStartedBegin");
                    compilationStarted.AddEventHandler(null, Events);
                }
            }

            private static void CompilationPipelineCompilationStartedEnd(object o)
            {
                EditorUtility.DisplayProgressBar("插件", "正在编译", 0);
                var compilationStarted = typeof(CompilationPipeline).GetEvent("compilationStarted", BindingFlags.Static | BindingFlags.Public);
                if (compilationStarted != null)
                {
                    var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationStartedEnd), BindingFlags.Static | BindingFlags.NonPublic);
                    var Events = Delegate.CreateDelegate(compilationStarted.EventHandlerType, null, methodInfo);
                    Debug.Log("Plugins Data Editor : CompilationPipelineCompilationStartedEnd");
                    compilationStarted.RemoveEventHandler(null, Events);
                }

                var compilationFinished = typeof(CompilationPipeline).GetEvent("compilationFinished", BindingFlags.Static | BindingFlags.Public);
                if (compilationFinished != null)
                {
                    var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationFinishedEnd), BindingFlags.Static | BindingFlags.NonPublic);
                    var Events = Delegate.CreateDelegate(compilationFinished.EventHandlerType, null, methodInfo);
                    compilationFinished.AddEventHandler(null, Events);
                }
            }

            private static void CompilationPipelineCompilationFinishedEnd(object o)
            {
                var compilationFinished = typeof(CompilationPipeline).GetEvent("compilationFinished", BindingFlags.Static | BindingFlags.Public);
                if (compilationFinished != null)
                {
                    var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationFinishedEnd), BindingFlags.Static | BindingFlags.NonPublic);
                    var Events = Delegate.CreateDelegate(compilationFinished.EventHandlerType, null, methodInfo);
                    Debug.Log("Plugins Data Editor : CompilationPipelineCompilationFinishedEnd");
                    compilationFinished.RemoveEventHandler(null, Events);
                }

                //
                // Client.Resolve();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("插件", "命令执行完毕", "OK");
                AssetDatabase.Refresh(
                    ImportAssetOptions.ForceSynchronousImport |
                    ImportAssetOptions.ForceUpdate |
                    ImportAssetOptions.ImportRecursive |
                    ImportAssetOptions.DontDownloadFromCacheServer |
                    ImportAssetOptions.ForceUncompressedImport
                );

                CompilationPipelineRequestScriptCompilation();
                CB?.Invoke();
                CB = null;
            }
        }
    }
}