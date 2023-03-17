using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace AIO.Package.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PluginsInfo))]
    internal class PluginsInfoEditor : UnityEditor.Editor
    {
        private PluginsInfo[] plugins;

        private DirectoryInfo Root;

        private void OnEnable()
        {
            Root = Directory.GetParent(Application.dataPath);
            plugins = new PluginsInfo[targets.Length];
            for (var i = 0; i < targets.Length; i++)
            {
                var path = AssetDatabase.GetAssetPath(targets[i]) + ".json";
                plugins[i] = (PluginsInfo)targets[i];
                if (File.Exists(path))
                {
                    var info = JsonUtility.FromJson<PluginsInfoJson>(File.ReadAllText(path)) ?? new PluginsInfoJson();
                    plugins[i].Name = info.Name;
                    plugins[i].MacroDefinition = info.MacroDefinition;
                    plugins[i].SourceRelativePath = info.SourceRelativePath;
                    plugins[i].TargetRelativePath = info.TargetRelativePath;
                }
            }

            Undo.RegisterCreatedObjectUndo(this, nameof(PluginsInfoEditor));
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
            serializedObject.Update();
            foreach (var setting in plugins)
            {
                EditorGUILayout.LabelField("插件安装管理配置", new GUIStyle("PreLabel"));
                EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));

                EditorGUILayout.PrefixLabel("插件名");
                setting.Name = EditorGUILayout.TextField(setting.Name);
                EditorGUILayout.Space();

                EditorGUILayout.PrefixLabel("宏定义");
                setting.MacroDefinition = EditorGUILayout.TextField(setting.MacroDefinition);
                EditorGUILayout.Space();

                EditorGUILayout.PrefixLabel("源文件相对路径");
                setting.SourceRelativePath = EditorGUILayout.TextField(setting.SourceRelativePath);
                EditorGUILayout.Space();

                EditorGUILayout.PrefixLabel("链接相对路径");
                setting.TargetRelativePath = EditorGUILayout.TextField(setting.TargetRelativePath);
                EditorGUILayout.Space();

                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();

                if (!string.IsNullOrEmpty(setting.SourceRelativePath) &&
                    !string.IsNullOrEmpty(setting.TargetRelativePath))
                {
                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button("UnInstall")) _ = UnInitialize(setting);
                    if (GUILayout.Button(" Install ")) _ = Initialize(setting);
                    // if (GetValidDir(Root.FullName, setting.TargetRelativePath).Exists)
                    // {
                    // }
                    // else
                    // {
                    // }

                    EditorGUILayout.EndHorizontal();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnValidate()
        {
            Undo.RecordObject(this, nameof(PluginsInfoEditor));
        }

        private void OnDisable()
        {
            for (var i = 0; i < targets.Length; i++)
            {
                var path = AssetDatabase.GetAssetPath(targets[i]);
                var info = new PluginsInfoJson();
                info.Name = plugins[i].Name;
                info.MacroDefinition = plugins[i].MacroDefinition;
                info.SourceRelativePath = plugins[i].SourceRelativePath;
                info.TargetRelativePath = plugins[i].TargetRelativePath;
                File.WriteAllText(path + ".json", JsonUtility.ToJson(info), Encoding.UTF8);
            }
        }

        internal static DirectoryInfo GetValidDir(string rootdir, string value)
        {
            var root = new DirectoryInfo(Path.Combine(rootdir, Path.GetPathRoot(value)));
            var name = Path.GetFileName(value);
            try
            {
                var regex = new Regex(value);
                foreach (var directory in root.GetDirectories("*.*", SearchOption.AllDirectories))
                {
                    if (directory.Name == name && regex.Match(directory.FullName).Success)
                        return directory;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new DirectoryInfo(value);
        }

        internal static async Task Initialize(PluginsInfo info)
        {
            await Initialize(new PluginsInfoJson
            {
                Name = info.Name,
                MacroDefinition = info.MacroDefinition,
                SourceRelativePath = info.SourceRelativePath,
                TargetRelativePath = info.TargetRelativePath
            });
        }

        internal static async Task Initialize(IEnumerable<PluginsInfoJson> infos)
        {
            var dataPath = Directory.GetParent(Application.dataPath).FullName;
            var list = new List<Task>();
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

                var parent = Directory.GetParent(target.FullName);
                if (!parent.Exists) parent.Create();

                var a = PrPlatform.Folder.Link(target.FullName, source.FullName).OnComplete(Result =>
                {
                    if (Result.ExitCode == 0)
                    {
                        Debug.LogFormat("链接成功 {0} : {1}", info.Name, target.FullName);
                        if (!string.IsNullOrEmpty(info.MacroDefinition)) AddScriptingDefineSymbols(info.MacroDefinition);
                    }
                    else Debug.LogErrorFormat("链接失败 {0} : {1} => {2}", info.Name, target.FullName, Result.StdALL);
                });
                list.Add(a.Async());
            }

            await Task.WhenAll(list);

            AssetDatabase.Refresh();
            AssetDatabase.RefreshSettings();
            CompilationPipeline.RequestScriptCompilation();
        }

        internal static async Task UnInitialize(IEnumerable<PluginsInfoJson> infos)
        {
            var dataPath = Directory.GetParent(Application.dataPath).FullName;
            var list = new List<Task>();
            foreach (var info in infos)
            {
                var target = GetValidDir(dataPath, info.TargetRelativePath);
                if (!target.Exists)
                {
                    Debug.LogErrorFormat("{0} -> 目标链接不存在:{1}", info.Name, target.FullName);
                    continue;
                }

                var parent = Directory.GetParent(target.FullName);
                if (!parent.Exists) parent.Create();

                var a = PrPlatform.File.Del(target.FullName + ".meta").Link(PrPlatform.Folder.Del(target.FullName)).OnComplete(Result =>
                {
                    if (Result.ExitCode == 0)
                    {
                        Debug.LogFormat("移除成功 {0} : {1}", info.Name, target.FullName);
                        if (!string.IsNullOrEmpty(info.MacroDefinition)) DelScriptingDefineSymbols(info.MacroDefinition);
                    }
                    else Debug.LogErrorFormat("移除失败 {0} : {1} \n {2}", info.Name, target.FullName, Result.StdALL);
                });

                list.Add(a.Async());
            }

            await Task.WhenAll(list);

            AssetDatabase.Refresh();
            AssetDatabase.RefreshSettings();
            CompilationPipeline.RequestScriptCompilation();
        }

        internal static async Task Initialize(PluginsInfoJson info)
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
                if (!string.IsNullOrEmpty(info.MacroDefinition)) AddScriptingDefineSymbols(info.MacroDefinition);
                AssetDatabase.Refresh();
                AssetDatabase.RefreshSettings();
                CompilationPipeline.RequestScriptCompilation();
            }
            else Debug.LogErrorFormat("链接失败 {0} : {1} => {2}", info.Name, target.FullName, Result.StdALL);
        }

        internal static async Task UnInitialize(PluginsInfoJson info)
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
                if (!string.IsNullOrEmpty(info.MacroDefinition)) DelScriptingDefineSymbols(info.MacroDefinition);
                AssetDatabase.Refresh();
                AssetDatabase.RefreshSettings();
                CompilationPipeline.RequestScriptCompilation();
            }
            else Debug.LogErrorFormat("移除失败 {0} : {1} \n {2}", info.Name, target.FullName, Result.StdALL);
        }

        internal static async Task UnInitialize(PluginsInfo info)
        {
            await UnInitialize(new PluginsInfoJson
            {
                Name = info.Name,
                MacroDefinition = info.MacroDefinition,
                SourceRelativePath = info.SourceRelativePath,
                TargetRelativePath = info.TargetRelativePath
            });
        }

        /// <summary>
        /// 禁止你想要的宏定义
        /// </summary>
        internal static void DelScriptingDefineSymbols(string value)
        {
            if (value.IsNullOrEmpty() || value.Length == 0) return;
            //获取当前是哪个平台
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            //获得当前平台已有的的宏定义
            var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!str.IsNullOrEmpty() && str.Length != 0 && str.Contains(value))
            {
                str = str.Replace(value, "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
            }
        }

        /// <summary>
        /// 添加你想要的宏定义
        /// </summary>
        internal static void AddScriptingDefineSymbols(string value)
        {
            //获取当前是哪个平台
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            //获得当前平台已有的的宏定义
            var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!str.Contains(value))
            {
                //添加宏定义
                str += ";" + value;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
            }
        }
    }
}