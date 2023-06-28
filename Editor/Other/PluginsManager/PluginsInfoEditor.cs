using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AIO;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// 插件管理
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PluginsInfo))]
    internal class PluginsInfoEditor : UnityEditor.Editor
    {
        private Dictionary<string, bool> InstallList;

        private DirectoryInfo Root;

        private PluginsInfoEditor()
        {
            InstallList = new Dictionary<string, bool>();
        }

        private void OnEnable()
        {
            Root = Directory.GetParent(Application.dataPath);
            InstallList.Clear();

            if (serializedObject.isEditingMultipleObjects)
            {
                foreach (var o in serializedObject.targetObjects) UpdateInstallInfo(o);
            }
            else UpdateInstallInfo(serializedObject.targetObject);
        }

        private void UpdateInstallInfo(in Object obj)
        {
            if (obj is null) return;
            var serialized = new SerializedObject(obj);
            var Name = serialized.FindProperty("Name");
            var TargetRelativePath = serialized.FindProperty("TargetRelativePath");
            if (string.IsNullOrEmpty(Name.stringValue)) return;
            if (string.IsNullOrEmpty(TargetRelativePath.stringValue)) return;
            InstallList.Set(Name.stringValue, GetValidDir(Root.FullName, TargetRelativePath.stringValue).Exists);
        }

        internal static void PathIsRegex(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                EditorGUILayout.HelpBox("请输入有效路径", MessageType.Warning);
                return;
            }

            if (!GetValidDir(Application.dataPath.Replace("Assets", ""), path).Exists)
            {
                EditorGUILayout.HelpBox("当前路径不存在", MessageType.Error);
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("插件安装管理配置", new GUIStyle("PreLabel"));
            if (GUILayout.Button("管理", GUILayout.Width(60))) EditorMenu.PluginsWindow();
            EditorGUILayout.EndHorizontal();

            foreach (var o in serializedObject.targetObjects)
            {
                var serialized = new SerializedObject(o);
                var Name = serialized.FindProperty("Name");
                var SourceRelativePath = serialized.FindProperty("SourceRelativePath");
                var TargetRelativePath = serialized.FindProperty("TargetRelativePath");
                var MacroDefinition = serialized.FindProperty("MacroDefinition");
                var Introduction = serialized.FindProperty("Introduction");

                EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Name.stringValue, new GUIStyle("PreLabel"), GUILayout.ExpandWidth(true));
                if (!string.IsNullOrEmpty(SourceRelativePath.stringValue) &&
                    !string.IsNullOrEmpty(TargetRelativePath.stringValue))
                {
                    if (InstallList.GetOrDefault<bool>(Name.stringValue, false))
                    {
                        if (GUILayout.Button("卸载", GUILayout.Width(57))) _ = UnInitialize((PluginsInfo)o);
                    }
                    else
                    {
                        if (GUILayout.Button("安装", GUILayout.Width(57))) _ = Initialize((PluginsInfo)o);
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

        private void OnValidate()
        {
            if (serializedObject.hasModifiedProperties)
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    foreach (var o in serializedObject.targetObjects) UpdateInstallInfo(o);
                }
                else UpdateInstallInfo(serializedObject.targetObject);
            }

            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void OnDisable()
        {
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void OnDestroy()
        {
            serializedObject.Dispose();
        }

        internal static DirectoryInfo GetValidDir(string rootdir, string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            var dirinfo = new DirectoryInfo(value);
            if (dirinfo.Exists) return dirinfo;
            var root = new DirectoryInfo(Path.Combine(rootdir, Path.GetPathRoot(value)));

            var name = dirinfo.Name;
            try
            {
                var regex = new Regex(value);
                foreach (var directory in root.GetDirectories("*.*", value.Contains("*") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                {
                    if (directory.Name == name && regex.Match(directory.FullName).Success)
                        return directory;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return dirinfo;
        }

        internal static async Task Initialize(IEnumerable<PluginsInfo> infos)
        {
            var dataPath = Directory.GetParent(Application.dataPath).FullName;
            var list = new List<Task>();
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
                    if (info.MacroDefinition.Contains(';'))
                    {
                        macroList.Add(info.MacroDefinition.Split(';'));
                    }
                    else macroList.Add(info.MacroDefinition);
                }

                var parent = Directory.GetParent(target.FullName);
                if (!parent.Exists) parent.Create();

                var a = PrPlatform.Folder.Link(target.FullName, source.FullName);
                list.Add(a.Async());
            }

            if (list.Count > 0)
            {
                await Task.WhenAll(list);
                AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                AssetDatabase.RefreshSettings();
#endif

                CompilationPipeline.compilationFinished += compilationFinished;
                if (macroList.Count != 0) UtilsEditor.Symbols.AddScriptingDefine(macroList.ToArray());
                CompilationPipeline.RequestScriptCompilation();
            }
        }

        internal static async Task UnInitialize(IEnumerable<PluginsInfo> infos)
        {
            var dataPath = Directory.GetParent(Application.dataPath).FullName;
            var list = new List<Task>();
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
                    {
                        macroList.Add(info.MacroDefinition.Split(';'));
                    }
                    else macroList.Add(info.MacroDefinition);
                }

                var parent = Directory.GetParent(target.FullName);
                if (!parent.Exists) parent.Create();

                var a = PrPlatform.File.Del(target.FullName + ".meta").Link(PrPlatform.Folder.Del(target.FullName));
                list.Add(a.Async());
            }

            if (list.Count > 0)
            {
                await Task.WhenAll(list);

                AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                AssetDatabase.RefreshSettings();
#endif
                CompilationPipeline.compilationFinished += compilationFinished;
                if (macroList.Count != 0) UtilsEditor.Symbols.DelScriptingDefine(macroList.ToArray());
                CompilationPipeline.RequestScriptCompilation();
            }
        }

        internal static async Task Initialize(PluginsInfo info)
        {
            var dataPath = Directory.GetParent(Application.dataPath).FullName;
            var source = GetValidDir(dataPath, info.SourceRelativePath);
            if (!source.Exists)
            {
                Debug.LogErrorFormat("插件位置不存在:{0}", source.FullName);
                return;
            }

            var target = GetValidDir(dataPath, info.TargetRelativePath);
            if (target.Exists)
            {
                Debug.LogErrorFormat("目标链接位已存在:{0}", target.FullName);
                return;
            }

            var parent = Directory.GetParent(target.FullName);
            if (!parent.Exists) parent.Create();
            var Result = await PrPlatform.Folder.Link(target.FullName, source.FullName);
            if (Result.ExitCode == 0)
            {
                Debug.LogFormat("链接成功 {0} : {1}", info.Name, target.FullName);
                AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                AssetDatabase.RefreshSettings();
#endif
                if (!string.IsNullOrEmpty(info.MacroDefinition)) UtilsEditor.Symbols.AddScriptingDefine(info.MacroDefinition.Split(';'));
                CompilationPipeline.compilationFinished += compilationFinished;
                CompilationPipeline.RequestScriptCompilation();
            }
            else Debug.LogErrorFormat("链接失败 {0} : {1} => {2}", info.Name, target.FullName, Result.StdALL);
        }

        internal static async Task UnInitialize(PluginsInfo info)
        {
            var target = GetValidDir(Application.dataPath, info.TargetRelativePath);
            if (!target.Exists)
            {
                Debug.LogErrorFormat("目标链接不存在:{0}", target.FullName);
                return;
            }

            var Result = await PrPlatform.File.Del(target.FullName + ".meta").Link(PrPlatform.Folder.Del(target.FullName));
            if (Result.ExitCode == 0)
            {
                Debug.LogFormat("移除成功 {0} : {1}", info.Name, target.FullName);
                AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                AssetDatabase.RefreshSettings();
#endif
                if (!string.IsNullOrEmpty(info.MacroDefinition)) UtilsEditor.Symbols.DelScriptingDefine(info.MacroDefinition.Split(';'));
                CompilationPipeline.compilationFinished += compilationFinished;
                CompilationPipeline.RequestScriptCompilation();
            }
            else Debug.LogErrorFormat("移除失败 {0} : {1} \n {2}", info.Name, target.FullName, Result.StdALL);
        }

        private static void compilationFinished(object o)
        {
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("插件", "命令执行完毕", "OK");
            CompilationPipeline.compilationFinished -= compilationFinished;
        }
    }
}