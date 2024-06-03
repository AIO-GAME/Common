#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AIO.UEditor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

#pragma warning disable CS1591
namespace AIO
{
    internal partial class Plugins
    {
        #region Nested type: PluginDataEditor

        /// <summary>
        /// 插件管理
        /// </summary>
        [CanEditMultipleObjects, CustomEditor(typeof(PluginData))]
        private class PluginDataEditor : Editor
        {
            private Dictionary<string, bool> InstallList;

            private DirectoryInfo Root;

            private PluginDataEditor() { InstallList = new Dictionary<string, bool>(); }

            private void OnEnable()
            {
                Root = Directory.GetParent(Application.dataPath);
                InstallList.Clear();

                if (serializedObject.isEditingMultipleObjects)
                    foreach (var o in serializedObject.targetObjects)
                        UpdateInstallInfo(o);
                else UpdateInstallInfo(serializedObject.targetObject);
            }

            private void OnDisable()
            {
                if (!serializedObject.targetObject) return;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }

            private void OnValidate()
            {
                if (serializedObject.hasModifiedProperties)
                {
                    if (serializedObject.isEditingMultipleObjects)
                        foreach (var o in serializedObject.targetObjects)
                            UpdateInstallInfo(o);
                    else UpdateInstallInfo(serializedObject.targetObject);
                }

                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }

            ~PluginDataEditor()
            {
                InstallList.Clear();
                InstallList = null;
            }

            private void UpdateInstallInfo(in Object obj)
            {
                if (!obj) return;
                var serialized         = new SerializedObject(obj);
                var Name               = serialized.FindProperty("Name");
                var TargetRelativePath = serialized.FindProperty("TargetRelativePath");
                if (string.IsNullOrEmpty(Name.stringValue)) return;
                if (string.IsNullOrEmpty(TargetRelativePath.stringValue)) return;
                InstallList[Name.stringValue] = GetValidDir(Root.FullName, TargetRelativePath.stringValue).Exists;
            }

            internal static void PathIsRegex(string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    EditorGUILayout.HelpBox("请输入有效路径", MessageType.Warning);
                    return;
                }

                if (!GetValidDir(Application.dataPath.Replace("Assets", ""), path).Exists) EditorGUILayout.HelpBox("当前路径不存在", MessageType.Error);
            }

            public override void OnInspectorGUI()
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("插件安装管理配置", new GUIStyle("PreLabel"));
                if (GUILayout.Button("管理", GUILayout.Width(60))) Open();
                EditorGUILayout.EndHorizontal();

                foreach (var o in serializedObject.targetObjects)
                {
                    var serialized         = new SerializedObject(o);
                    var Name               = serialized.FindProperty("Name");
                    var SourceRelativePath = serialized.FindProperty("SourceRelativePath");
                    var TargetRelativePath = serialized.FindProperty("TargetRelativePath");
                    var MacroDefinition    = serialized.FindProperty("MacroDefinition");
                    var Introduction       = serialized.FindProperty("Introduction");

                    EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(Name.stringValue, new GUIStyle("PreLabel"), GUILayout.ExpandWidth(true));
                    if (!string.IsNullOrEmpty(SourceRelativePath.stringValue) &&
                        !string.IsNullOrEmpty(TargetRelativePath.stringValue))
                    {
                        if (InstallList.GetOrDefault(Name.stringValue, false))
                        {
                            if (GUILayout.Button("卸载", GUILayout.Width(57))) _ = UnInitialize((PluginData)o);
                        }
                        else
                        {
                            if (GUILayout.Button("安装", GUILayout.Width(57))) _ = Initialize((PluginData)o);
                        }
                    }

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("插件名");
                    Name.stringValue = EditorGUILayout.TextField(Name.stringValue);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("宏定义");
                    MacroDefinition.stringValue = EditorGUILayout.TextField(MacroDefinition.stringValue);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.PrefixLabel("源文件相对路径");
                    TargetRelativePath.stringValue = EditorGUILayout.TextField(TargetRelativePath.stringValue);
                    EditorGUILayout.Space();

                    EditorGUILayout.PrefixLabel("链接相对路径");
                    SourceRelativePath.stringValue = EditorGUILayout.TextField(SourceRelativePath.stringValue);
                    EditorGUILayout.Space();

                    EditorGUILayout.PrefixLabel("简介");
                    Introduction.stringValue = EditorGUILayout.TextArea(Introduction.stringValue, GUILayout.Height(50));
                    EditorGUILayout.Space();

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.Space();
                    serialized.SetIsDifferentCacheDirty();
                    serialized.ApplyModifiedPropertiesWithoutUndo();
                    serialized.Update();
                }

                serializedObject.SetIsDifferentCacheDirty();
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                serializedObject.Update();
            }

            internal static DirectoryInfo GetValidDir(string rootDir, string value)
            {
                if (string.IsNullOrEmpty(value)) return null;
                var dirInfo = new DirectoryInfo(value);
                if (dirInfo.Exists) return dirInfo;
                var root = new DirectoryInfo(Path.Combine(rootDir, Path.GetPathRoot(value)));

                var name = dirInfo.Name;
                try
                {
                    var regex = new Regex(value);
                    foreach (var directory in root.GetDirectories("*.*", value.Contains("*")
                                                                      ? SearchOption.AllDirectories
                                                                      : SearchOption.TopDirectoryOnly)
                                                  .Where(directory => directory.Name == name && regex.Match(directory.FullName).Success)
                            ) return directory;
                }
                catch (Exception)
                {
                    // ignored
                }

                return dirInfo;
            }

            internal static async Task Initialize(IEnumerable<PluginData> infos)
            {
                var dataPath  = Directory.GetParent(Application.dataPath)?.FullName;
                var list      = new List<Task>();
                var macroList = new List<string>();
                foreach (var info in infos)
                {
                    var source = GetValidDir(dataPath, info.SourceRelativePath);
                    if (!source.Exists)
                    {
                        Debug.LogErrorFormat("{0} -> 插件位置不存在:{1}", info.Name, source.FullName);
                        continue;
                    }

                    var target = GetValidDir(dataPath, info.TargetRelativePath);
                    if (target.Exists)
                    {
                        Debug.LogErrorFormat("{0} -> 目标链接位已存在:{1}", info.Name, target.FullName);
                        continue;
                    }

                    if (!string.IsNullOrEmpty(info.MacroDefinition))
                    {
                        if (info.MacroDefinition.Contains(";"))
                            macroList.AddRange(info.MacroDefinition.Split(';'));
                        else macroList.Add(info.MacroDefinition);
                    }

                    var parent = Directory.GetParent(target.FullName);
                    if (parent != null && !parent.Exists) parent.Create();
                    list.Add(PrPlatform.Folder.Symbolic(target.FullName, source.FullName).Async());
                }

                if (list.Count > 0)
                {
                    EditorUtility.DisplayProgressBar("插件", "正在安装插件", 0);
                    await Task.WhenAll(list);
                    EditorUtility.ClearProgressBar();
                    EHelper.Symbols.AddScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, macroList);
                    AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                    AssetDatabase.RefreshSettings();
#endif
                    compilationStarted();
                }
            }

            internal static async Task UnInitialize(IEnumerable<PluginData> infos)
            {
                var dataPath  = Directory.GetParent(Application.dataPath)?.FullName;
                var list      = new List<Task>();
                var macroList = new List<string>();
                foreach (var info in infos)
                {
                    var target = GetValidDir(dataPath, info.TargetRelativePath);
                    if (!target.Exists)
                    {
                        Debug.LogErrorFormat("{0} -> 目标链接不存在:{1}", info.Name, target.FullName);
                        continue;
                    }

                    if (!string.IsNullOrEmpty(info.MacroDefinition))
                    {
                        if (info.MacroDefinition.Contains(';'))
                            macroList.AddRange(info.MacroDefinition.Split(';'));
                        else macroList.Add(info.MacroDefinition);
                    }

                    var parent = Directory.GetParent(target.FullName);
                    if (parent != null && !parent.Exists) parent.Create();

                    var a = PrPlatform.File.Del(target.FullName + ".meta").Link(PrPlatform.Folder.Del(target.FullName));
                    list.Add(a.Async());
                }

                if (list.Count > 0)
                {
                    EditorUtility.DisplayProgressBar("插件", "正在卸载插件", 0);
                    await Task.WhenAll(list);
                    EditorUtility.ClearProgressBar();
                    EHelper.Symbols.DelScriptingDefine(EditorUserBuildSettings.selectedBuildTargetGroup, macroList);
                    AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                    AssetDatabase.RefreshSettings();
#endif
                    compilationStarted();
                }
            }

            internal static Task Initialize(PluginData info) { return Initialize(new[] { info }); }

            internal static Task UnInitialize(PluginData info) { return UnInitialize(new[] { info }); }

            private static void compilationStarted()
            {
                CompilationPipeline.compilationStarted += compilationStarted;
                CompilationPipeline.RequestScriptCompilation();
            }

            private static void compilationStarted(object o)
            {
                EditorUtility.DisplayProgressBar("插件", "正在编译", 0);
                CompilationPipeline.compilationStarted  -= compilationStarted;
                CompilationPipeline.compilationFinished += compilationFinished;
            }

            private static void compilationFinished(object o)
            {
                CompilationPipeline.compilationFinished -= compilationFinished;

                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("插件", "命令执行完毕", "OK");
                AssetDatabase.Refresh();

                CompilationPipeline.RequestScriptCompilation();
            }
        }

        #endregion
    }
}