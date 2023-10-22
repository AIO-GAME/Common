// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using System.Reflection;
// using System.Text.RegularExpressions;
// using System.Threading.Tasks;
// using UnityEditor;
// using UnityEditor.Compilation;
// using UnityEditor.PackageManager;
// using UnityEngine;
// using Object = UnityEngine.Object;
//
// // ReSharper disable AssignNullToNotNullAttribute
//
// namespace AIO.UEditor
// {
//     /// <summary>
//     /// 插件管理
//     /// </summary>
//     [CanEditMultipleObjects]
//     [CustomEditor(typeof(PluginsInfo))]
//     internal class PluginsInfoEditor : Editor
//     {
//         private Dictionary<string, bool> InstallList;
//
//         private DirectoryInfo Root;
//
//         private PluginsInfoEditor()
//         {
//             InstallList = new Dictionary<string, bool>();
//         }
//
//         ~PluginsInfoEditor()
//         {
//             InstallList.Clear();
//             InstallList = null;
//         }
//
//         private void OnEnable()
//         {
//             Root = Directory.GetParent(Application.dataPath);
//             InstallList.Clear();
//
//             if (serializedObject.isEditingMultipleObjects)
//             {
//                 foreach (var o in serializedObject.targetObjects) UpdateInstallInfo(o);
//             }
//             else UpdateInstallInfo(serializedObject.targetObject);
//         }
//
//         private void UpdateInstallInfo(in Object obj)
//         {
//             if (obj is null) return;
//             var serialized = new SerializedObject(obj);
//             var Name = serialized.FindProperty("Name");
//             var TargetRelativePath = serialized.FindProperty("TargetRelativePath");
//             if (string.IsNullOrEmpty(Name.stringValue)) return;
//             if (string.IsNullOrEmpty(TargetRelativePath.stringValue)) return;
//             InstallList.Set(Name.stringValue, GetValidDir(Root.FullName, TargetRelativePath.stringValue).Exists);
//         }
//
//         internal static void PathIsRegex(string path)
//         {
//             if (string.IsNullOrEmpty(path))
//             {
//                 EditorGUILayout.HelpBox("请输入有效路径", MessageType.Warning);
//                 return;
//             }
//
//             if (!GetValidDir(Application.dataPath.Replace("Assets", ""), path).Exists)
//             {
//                 EditorGUILayout.HelpBox("当前路径不存在", MessageType.Error);
//             }
//         }
//
//         public override void OnInspectorGUI()
//         {
//             EditorGUILayout.BeginHorizontal();
//             EditorGUILayout.LabelField("插件安装管理配置", new GUIStyle("PreLabel"));
//             if (GUILayout.Button("管理", GUILayout.Width(60))) MenuItem_Tools.OpenPluginsManagerWindow();
//             EditorGUILayout.EndHorizontal();
//
//             foreach (var o in serializedObject.targetObjects)
//             {
//                 var serialized = new SerializedObject(o);
//                 var Name = serialized.FindProperty("Name");
//                 var SourceRelativePath = serialized.FindProperty("SourceRelativePath");
//                 var TargetRelativePath = serialized.FindProperty("TargetRelativePath");
//                 var MacroDefinition = serialized.FindProperty("MacroDefinition");
//                 var Introduction = serialized.FindProperty("Introduction");
//
//                 EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));
//
//                 EditorGUILayout.BeginHorizontal();
//                 EditorGUILayout.LabelField(Name.stringValue, new GUIStyle("PreLabel"), GUILayout.ExpandWidth(true));
//                 if (!string.IsNullOrEmpty(SourceRelativePath.stringValue) &&
//                     !string.IsNullOrEmpty(TargetRelativePath.stringValue))
//                 {
//                     if (InstallList.GetOrDefault<bool>(Name.stringValue, false))
//                     {
//                         if (GUILayout.Button("卸载", GUILayout.Width(57))) _ = UnInitialize((PluginsInfo)o);
//                     }
//                     else
//                     {
//                         if (GUILayout.Button("安装", GUILayout.Width(57))) _ = Initialize((PluginsInfo)o);
//                     }
//                 }
//
//                 EditorGUILayout.EndHorizontal();
//
//                 EditorGUILayout.Space();
//                 EditorGUILayout.Space();
//
//                 EditorGUILayout.BeginHorizontal();
//                 EditorGUILayout.PrefixLabel("插件名");
//                 Name.stringValue = EditorGUILayout.TextField(Name.stringValue);
//                 EditorGUILayout.EndHorizontal();
//
//                 EditorGUILayout.Space();
//
//                 EditorGUILayout.BeginHorizontal();
//                 EditorGUILayout.PrefixLabel("宏定义");
//                 MacroDefinition.stringValue = EditorGUILayout.TextField(MacroDefinition.stringValue);
//                 EditorGUILayout.EndHorizontal();
//                 EditorGUILayout.Space();
//
//                 EditorGUILayout.PrefixLabel("源文件相对路径");
//                 TargetRelativePath.stringValue = EditorGUILayout.TextField(TargetRelativePath.stringValue);
//                 EditorGUILayout.Space();
//
//                 EditorGUILayout.PrefixLabel("链接相对路径");
//                 SourceRelativePath.stringValue = EditorGUILayout.TextField(SourceRelativePath.stringValue);
//                 EditorGUILayout.Space();
//
//                 EditorGUILayout.PrefixLabel("简介");
//                 Introduction.stringValue = EditorGUILayout.TextArea(Introduction.stringValue, GUILayout.Height(50));
//                 EditorGUILayout.Space();
//
//                 EditorGUILayout.EndVertical();
//
//                 EditorGUILayout.Space();
//                 serialized.SetIsDifferentCacheDirty();
//                 serialized.ApplyModifiedPropertiesWithoutUndo();
//                 serialized.Update();
//             }
//
//             serializedObject.SetIsDifferentCacheDirty();
//             serializedObject.ApplyModifiedPropertiesWithoutUndo();
//             serializedObject.Update();
//         }
//
//         private void OnValidate()
//         {
//             if (serializedObject.hasModifiedProperties)
//             {
//                 if (serializedObject.isEditingMultipleObjects)
//                 {
//                     foreach (var o in serializedObject.targetObjects) UpdateInstallInfo(o);
//                 }
//                 else UpdateInstallInfo(serializedObject.targetObject);
//             }
//
//             serializedObject.ApplyModifiedPropertiesWithoutUndo();
//         }
//
//         private void OnDisable()
//         {
//             serializedObject.ApplyModifiedPropertiesWithoutUndo();
//         }
//
//         private void OnDestroy()
//         {
//             serializedObject.Dispose();
//         }
//
//         internal static DirectoryInfo GetValidDir(string rootDir, string value)
//         {
//             if (string.IsNullOrEmpty(value)) return null;
//             var dirInfo = new DirectoryInfo(value);
//             if (dirInfo.Exists) return dirInfo;
//             var root = new DirectoryInfo(Path.Combine(rootDir, Path.GetPathRoot(value)));
//
//             var name = dirInfo.Name;
//             try
//             {
//                 var regex = new Regex(value);
//                 foreach (var directory in root.GetDirectories("*.*",
//                              value.Contains("*") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
//                 {
//                     if (directory.Name == name && regex.Match(directory.FullName).Success)
//                         return directory;
//                 }
//             }
//             catch (Exception)
//             {
//                 // ignored
//             }
//
//             return dirInfo;
//         }
//
//         internal static async Task Initialize(IEnumerable<PluginsInfo> infos)
//         {
//             var dataPath = Directory.GetParent(Application.dataPath)?.FullName;
//             var list = new List<Task>();
//             var macroList = new List<string>();
//             foreach (var info in infos)
//             {
//                 var source = GetValidDir(dataPath, info.SourceRelativePath);
//                 if (!source.Exists)
//                 {
//                     Debug.LogErrorFormat("{0} -> 插件位置不存在:{1}", info.Name, source.FullName);
//                     continue;
//                 }
//
//                 var target = GetValidDir(dataPath, info.TargetRelativePath);
//                 if (target.Exists)
//                 {
//                     Debug.LogErrorFormat("{0} -> 目标链接位已存在:{1}", info.Name, target.FullName);
//                     continue;
//                 }
//
//                 if (!string.IsNullOrEmpty(info.MacroDefinition))
//                 {
//                     if (info.MacroDefinition.Contains(';'))
//                     {
//                         macroList.Add(info.MacroDefinition.Split(';'));
//                     }
//                     else macroList.Add(info.MacroDefinition);
//                 }
//
//                 var parent = Directory.GetParent(target.FullName);
//                 if (parent != null && !parent.Exists) parent.Create();
//
//                 var a = PrPlatform.Folder.Symbolic(target.FullName, source.FullName);
//                 list.Add(a.Async());
//             }
//
//             if (list.Count > 0)
//             {
//                 try
//                 {
//                     Helper.AddScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, macroList);
//                     EditorUtility.DisplayProgressBar("插件", "正在安装插件", 0);
//                     await Task.WhenAll(list);
//                     AssetDatabase.Refresh();
//                     Helper.RefreshSettings();
//                     Helper.CompilationPipelineCompilationStartedBegin();
//                     Helper.CompilationPipelineRequestScriptCompilation();
//                 }
//                 catch (Exception e)
//                 {
//                     Debug.LogException(e);
//                 }
//             }
//         }
//
//         internal static async Task UnInitialize(IEnumerable<PluginsInfo> infos)
//         {
//             var dataPath = Directory.GetParent(Application.dataPath)?.FullName;
//             var list = new List<Task>();
//             var macroList = new List<string>();
//             foreach (var info in infos)
//             {
//                 var target = GetValidDir(dataPath, info.TargetRelativePath);
//                 if (!target.Exists)
//                 {
//                     Debug.LogErrorFormat("{0} -> 目标链接不存在:{1}", info.Name, target.FullName);
//                     continue;
//                 }
//
//                 if (!string.IsNullOrEmpty(info.MacroDefinition))
//                 {
//                     if (info.MacroDefinition.Contains(';'))
//                     {
//                         macroList.Add(info.MacroDefinition.Split(';'));
//                     }
//                     else macroList.Add(info.MacroDefinition);
//                 }
//
//                 var parent = Directory.GetParent(target.FullName);
//                 if (parent != null && !parent.Exists) parent.Create();
//
//                 var a = PrPlatform.File.Del(target.FullName + ".meta").Link(PrPlatform.Folder.Del(target.FullName));
//                 list.Add(a.Async());
//             }
//
//             if (list.Count > 0)
//             {
//                 try
//                 {
//                     Helper.DelScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, macroList);
//                     EditorUtility.DisplayProgressBar("插件", "正在卸载插件", 0);
//                     await Task.WhenAll(list);
//                     AssetDatabase.Refresh();
//                     Helper.RefreshSettings();
//                     Helper.CompilationPipelineCompilationStartedBegin();
//                     Helper.CompilationPipelineRequestScriptCompilation();
//                 }
//                 catch (Exception e)
//                 {
//                     Debug.LogException(e);
//                 }
//             }
//         }
//
//         internal static Task Initialize(PluginsInfo info)
//         {
//             return Initialize(new[] { info });
//         }
//
//         internal static Task UnInitialize(PluginsInfo info)
//         {
//             return UnInitialize(new[] { info });
//         }
//
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
//             /// 移除重复元素
//             /// </summary>
//             private static IList<T> RemoveRepeat<T>(IList<T> array)
//             {
//                 if (array is null) return Array.Empty<T>();
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
//             private static ICollection<string> GetScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup)
//             {
//                 //获得当前平台已有的的宏定义
//                 var GetScriptingDefineSymbols = typeof(PlayerSettings).GetMethod("GetScriptingDefineSymbolsInternal",
//                     BindingFlags.Static | BindingFlags.NonPublic);
//                 string str = null;
//                 if (GetScriptingDefineSymbols != null)
//                 {
//                     foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//                     {
//                         if (!assembly.GetName().Name.StartsWith("UnityEditor.Build")) continue;
//                         var namedBuildTargetType = assembly.GetType("UnityEditor.Build.NamedBuildTarget");
//                         var FromBuildTargetGroupMethod = namedBuildTargetType?.GetMethod("FromBuildTargetGroup",
//                             BindingFlags.Static | BindingFlags.Public);
//                         if (FromBuildTargetGroupMethod is null) continue;
//                         var symbols = FromBuildTargetGroupMethod.Invoke(null, new object[] { buildTargetGroup });
//                         str = GetScriptingDefineSymbols.Invoke(null, new object[] { symbols }) as string;
//                         break;
//                     }
//                 }
//                 else str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
//
//                 return string.IsNullOrEmpty(str) ? Array.Empty<string>() : str.Split(';');
//             }
//
//             private static void SetScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup,
//                 IEnumerable<string> verify)
//             {
//                 //获得当前平台已有的的宏定义
//                 var SetScriptingDefineSymbols = typeof(PlayerSettings).GetMethod(
//                     "SetScriptingDefineSymbols",
//                     2,
//                     BindingFlags.Static | BindingFlags.Public,
//                     null,
//                     new Type[] { typeof(string), typeof(string) },
//                     null
//                 );
//                 var str = string.Join(";", verify);
//                 if (SetScriptingDefineSymbols != null)
//                 {
//                     foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//                     {
//                         if (!assembly.GetName().Name.StartsWith("UnityEditor.Build")) continue;
//                         var namedBuildTargetType = assembly.GetType("UnityEditor.Build.NamedBuildTarget");
//                         var FromBuildTargetGroupMethod = namedBuildTargetType?.GetMethod("FromBuildTargetGroup",
//                             BindingFlags.Static | BindingFlags.Public);
//                         if (FromBuildTargetGroupMethod is null) continue;
//                         var Symbols = FromBuildTargetGroupMethod.Invoke(null, new object[] { buildTargetGroup });
//                         SetScriptingDefineSymbols.Invoke(null, new object[] { Symbols, str });
//
//                         break;
//                     }
//                 }
//                 else PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
//             }
//
//             /// <summary>
//             /// 添加传入的宏定义
//             /// </summary>
//             public static void AddScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
//             {
//                 if (value is null || value.Count == 0) return;
//                 Debug.Log($"Plugins Data Editor : AddScriptingDefine -> {buildTargetGroup}");
//                 var verify = new List<string>(GetScriptingDefineSymbolsForGroup(buildTargetGroup));
//                 foreach (var v in value)
//                 {
//                     if (string.IsNullOrEmpty(v) || verify.Contains(v)) continue;
//                     verify.Add(v);
//                 }
//
//                 SetScriptingDefineSymbolsForGroup(buildTargetGroup, RemoveRepeat(verify));
//             }
//
//
//             /// <summary>
//             /// 禁止传入的宏定义
//             /// </summary>
//             public static void DelScriptingDefine(BuildTargetGroup buildTargetGroup, ICollection<string> value)
//             {
//                 if (value is null || value.Count == 0) return;
//                 Debug.Log($"Plugins Data Editor : DelScriptingDefine -> {buildTargetGroup}");
//                 var str = GetScriptingDefineSymbolsForGroup(buildTargetGroup);
//                 if (str.Count == 0) return;
//                 IList<string> verify = new List<string>(str);
//                 verify = RemoveRepeat(verify);
//                 foreach (var item in value) verify.Remove(item);
//                 SetScriptingDefineSymbolsForGroup(buildTargetGroup, verify);
//             }
//
//             /// <summary>
//             /// 刷新设置
//             /// </summary>
//             public static void RefreshSettings()
//             {
//                 var refreshSettingsMethodInfo = typeof(AssetDatabase).GetMethod("RefreshSettings",
//                     BindingFlags.Static | BindingFlags.Public);
//                 if (refreshSettingsMethodInfo != null)
//                 {
//                     Debug.Log("Plugins Data Editor : AssetDatabase RefreshSettings Start");
//                     refreshSettingsMethodInfo.Invoke(null, null);
//                 }
//             }
//
//             /// <summary>
//             /// 编译程序集
//             /// </summary>
//             public static void CompilationPipelineRequestScriptCompilation()
//             {
//                 var requestScriptCompilationMethodInfo = typeof(CompilationPipeline).GetMethod(
//                     "RequestScriptCompilation",
//                     0,
//                     BindingFlags.Static | BindingFlags.Public,
//                     null,
//                     new Type[] { },
//                     null
//                 );
//                 if (requestScriptCompilationMethodInfo != null)
//                 {
//                     Debug.Log("Plugins Data Editor : CompilationPipeline RequestScriptCompilation Start");
//                     requestScriptCompilationMethodInfo.Invoke(null, null);
//                 }
//             }
//
//             public static void CompilationPipelineCompilationStartedBegin()
//             {
//                 var compilationStarted = typeof(CompilationPipeline).GetEvent("compilationStarted", BindingFlags.Static | BindingFlags.Public);
//                 if (compilationStarted != null)
//                 {
//                     var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationStartedEnd), BindingFlags.Static | BindingFlags.NonPublic);
//                     var Events = Delegate.CreateDelegate(compilationStarted.EventHandlerType, null, methodInfo);
//                     Debug.Log("Plugins Data Editor : CompilationPipelineCompilationStartedBegin");
//                     compilationStarted.AddEventHandler(null, Events);
//                 }
//             }
//
//             private static void CompilationPipelineCompilationStartedEnd(object o)
//             {
//                 EditorUtility.DisplayProgressBar("插件", "正在编译", 0);
//                 var compilationStarted = typeof(CompilationPipeline).GetEvent("compilationStarted", BindingFlags.Static | BindingFlags.Public);
//                 if (compilationStarted != null)
//                 {
//                     var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationStartedEnd), BindingFlags.Static | BindingFlags.NonPublic);
//                     var Events = Delegate.CreateDelegate(compilationStarted.EventHandlerType, null, methodInfo);
//                     Debug.Log("Plugins Data Editor : CompilationPipelineCompilationStartedEnd");
//                     compilationStarted.RemoveEventHandler(null, Events);
//                 }
//
//                 var compilationFinished = typeof(CompilationPipeline).GetEvent("compilationFinished", BindingFlags.Static | BindingFlags.Public);
//                 if (compilationFinished != null)
//                 {
//                     var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationFinishedEnd), BindingFlags.Static | BindingFlags.NonPublic);
//                     var Events = Delegate.CreateDelegate(compilationFinished.EventHandlerType, null, methodInfo);
//                     compilationFinished.AddEventHandler(null, Events);
//                 }
//             }
//
//             private static void CompilationPipelineCompilationFinishedEnd(object o)
//             {
//                 var compilationFinished = typeof(CompilationPipeline).GetEvent("compilationFinished", BindingFlags.Static | BindingFlags.Public);
//                 if (compilationFinished != null)
//                 {
//                     var methodInfo = typeof(Helper).GetMethod(nameof(CompilationPipelineCompilationFinishedEnd), BindingFlags.Static | BindingFlags.NonPublic);
//                     var Events = Delegate.CreateDelegate(compilationFinished.EventHandlerType, null, methodInfo);
//                     Debug.Log("Plugins Data Editor : CompilationPipelineCompilationFinishedEnd");
//                     compilationFinished.RemoveEventHandler(null, Events);
//                 }
//                 //
//                 // Client.Resolve();
//                 EditorUtility.ClearProgressBar();
//                 EditorUtility.DisplayDialog("插件", "命令执行完毕", "OK");
//                 AssetDatabase.Refresh(
//                     ImportAssetOptions.ForceSynchronousImport |
//                     ImportAssetOptions.ForceUpdate |
//                     ImportAssetOptions.ImportRecursive |
//                     ImportAssetOptions.DontDownloadFromCacheServer |
//                     ImportAssetOptions.ForceUncompressedImport
//                 );
//
//                 CompilationPipelineRequestScriptCompilation();
//             }
//         }
//
//     }
// }