// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using UnityEditor;
// using UnityEngine;
// using Object = UnityEngine.Object;
//
// #pragma warning disable CS1591
// namespace AIO.UEditor
// {
//     internal partial class Plugins
//     {
//         internal static class Helper
//         {
//             public static T GetOrDefault<T>(IDictionary dic, in object key, in T defaultValue)
//             {
//                 if (dic == null)
//                     throw new ArgumentNullException(nameof(dic));
//                 if (key == null)
//                     throw new ArgumentNullException(nameof(key));
//                 return dic.Contains(key) && dic[key] is T obj ? obj : defaultValue;
//             }
//
//             /// <summary>
//             /// 获取指定文件夹下的预制件
//             /// </summary>
//             /// <param name="pattern">匹配模式</param>
//             /// <param name="folder">文件夹</param>
//             /// <returns>预制件数组</returns>
//             public static IEnumerable<T> GetAssetsRes<T>(in string pattern, params string[] folder)
//                 where T : Object
//             {
//                 if (string.IsNullOrEmpty(pattern)) return Array.Empty<T>();
//
//                 return AssetDatabase.FindAssets(pattern, folder)
//                     .Select(AssetDatabase.GUIDToAssetPath)
//                     .Select(AssetDatabase.LoadAssetAtPath<T>)
//                     .Where(value => value != null);
//             }
//
//             private static PluginsInfoWindow Window;
//
//             [MenuItem("Tools/Window/Plugins Manager #_F12")]
//             public static void Open()
//             {
//                 if (Window is null)
//                 {
//                     Window = ScriptableObject.CreateInstance<PluginsInfoWindow>();
//                     Window.titleContent = new GUIContent("插件管理界面", "Plugins Manager");
//                     Window.minSize = new Vector2(200, 600);
//                 }
//
//                 Window.Show(true);
//                 Window.Focus();
//                 Window.Repaint();
//             }
//
//
//             /// <summary>
//             /// 移除重复元素
//             /// </summary>
//             private static IList<T> RemoveRepeat<T>(IList<T> array)
//             {
//                 if (array is null) throw new ArgumentNullException(nameof(array));
//                 if (array.Count <= 1) return array;
//                 var hashSet = new HashSet<T>();
//                 for (var i = 0; i < array.Count; i++)
//                 {
//                     var num = array[i];
//                     if (hashSet.Count == 0 || !hashSet.Contains(num))
//                     {
//                         hashSet.Add(num);
//                     }
//                     else if (array is List<T> list)
//                     {
//                         list.RemoveAll(x => Equals(x, num));
//                     }
//                     else
//                     {
//                         array.RemoveAt(i--);
//                     }
//                 }
//
//                 return array;
//             }
//
//             private static string[] GetScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup)
//             {
//                 //获得当前平台已有的的宏定义
//                 var GetScriptingDefineSymbolsForGroup = typeof(PlayerSettings).GetMethod("GetScriptingDefineSymbols",
//                     BindingFlags.Static | BindingFlags.Public);
//                 string str = null;
//                 if (GetScriptingDefineSymbolsForGroup != null)
//                 {
//                     foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//                     {
//                         if (!assembly.GetName().Name.StartsWith("UnityEditor.Build")) continue;
//                         var namedBuildTargetType = assembly.GetType("UnityEditor.Build.NamedBuildTarget");
//                         var FromBuildTargetGroupMethod = namedBuildTargetType?.GetMethod("FromBuildTargetGroup",
//                             BindingFlags.Static | BindingFlags.Public);
//                         if (FromBuildTargetGroupMethod is null) continue;
//                         str =
//                             FromBuildTargetGroupMethod.Invoke(null, new object[] { buildTargetGroup }) as string;
//                         break;
//                     }
//                 }
//                 else str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
//
//                 return string.IsNullOrEmpty(str) ? Array.Empty<string>() : str.Split(';');
//             }
//
//             /// <summary>
//             /// 添加传入的宏定义
//             /// </summary>
//             public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
//             {
//                 if (value is null || value.Count == 0) return;
//
//                 if (str is null) str = string.Empty;
//
//                 var verify = new List<string>(str.Split(';'));
//                 foreach (var v in value)
//                 {
//                     if (string.IsNullOrEmpty(v) || verify.Contains(v)) continue;
//                     verify.Add(v);
//                 }
//
//                 RemoveRepeat(verify);
//                 //添加宏定义
// #if UNITY_2023_1_OR_NEWER
//                 PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), string.Join(";", verify));
// #else
//                 PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", verify));
// #endif
//             }
//         }
//     }
// }