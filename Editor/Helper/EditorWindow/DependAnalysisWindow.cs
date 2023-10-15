using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 查询引用关系
    /// </summary>
    [GWindow("查询资源引用", Group = "Tools",
        MinSizeWidth = 600, MinSizeHeight = 600
    )]
    public class DependAnalysisGraphWindow : GraphicWindow
    {
        private static Object[] targetObjects;
        private static int targetCount;

        [MenuItem("Assets/Query Reference", false, 19)]
        private static void FindReferences()
        {
            targetObjects = Selection.GetFiltered<Object>(SelectionMode.Assets);
            targetCount = targetObjects?.Length ?? 0;
            if (targetCount == 0) return;
            EHelper.Window.Open<DependAnalysisGraphWindow>(MenuItem_Tools.DockedWindowTypes);
        }

        private bool[] foldoutArr;
        private Object[][] beDependArr;
        private Vector2 scrollPos;
        private string[] withoutExtensions = new string[] { ".prefab", ".unity", ".mat", ".asset", ".controller" };

        protected override void OnActivation()
        {
            beDependArr = new Object[targetCount][];
            foldoutArr = new bool[targetCount];
            for (var i = 0; i < targetCount; i++) beDependArr[i] = GetBeDepend(targetObjects[i]);
            EditorStyles.foldout.richText = true;
        }

        protected override void OnGUI()
        {
            if (beDependArr.Length != targetCount) return;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            for (int i = 0, count = 0; i < targetCount; i++)
            {
                count = beDependArr[i] == null ? 0 : beDependArr[i].Length;
                var objName = Path.GetFileName(AssetDatabase.GetAssetPath(targetObjects[i]));
                var info = count == 0
                    ? $"<color=yellow>{objName}【{count}】</color>"
                    : $"{objName}【{count}】";
                foldoutArr[i] = EditorGUILayout.Foldout(foldoutArr[i], info);
                if (foldoutArr[i])
                {
                    if (count > 0)
                    {
                        foreach (var obj in beDependArr[i])
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(15);
                            EditorGUILayout.ObjectField(obj, typeof(Object), true);
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(15);
                        EditorGUILayout.LabelField("【Null】");
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// 查找所有引用目标资源的物体
        /// </summary>
        /// <param name="target">目标资源</param>
        /// <returns></returns>
        private Object[] GetBeDepend(Object target)
        {
            if (target == null) return null;
            var path = AssetDatabase.GetAssetPath(target);
            if (string.IsNullOrEmpty(path)) return null;
            var guid = AssetDatabase.AssetPathToGUID(path);
            var files = Directory.GetFiles(Application.dataPath, "*",
                SearchOption.AllDirectories).Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
            var objects = new List<Object>();
            foreach (var file in files)
            {
                var assetPath = "Assets" + file.Replace(Application.dataPath, "");
                var readText = File.ReadAllText(file);

                if (!readText.StartsWith("%YAML"))
                {
                    var depends = AssetDatabase.GetDependencies(assetPath, false);
                    if (depends != null)
                    {
                        foreach (var dep in depends)
                        {
                            if (dep == path)
                            {
                                objects.Add(AssetDatabase.LoadAssetAtPath<Object>(assetPath));
                                break;
                            }
                        }
                    }
                }
                else if (Regex.IsMatch(readText, guid)) objects.Add(AssetDatabase.LoadAssetAtPath<Object>(assetPath));
            }

            return objects.ToArray();
        }

        protected override void OnDispose()
        {
            targetObjects = null;
            beDependArr = null;
            foldoutArr = null;
        }
    }
}