/*|=============================================↩|
|*|Author:          |XINAN                     |↩|
|*|Date:            |2022-11-23                |↩|
|*|Time:            |16:42:47                  |↩|
|*|E-Mail:          |1398581458@qq.com         |↩|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public class PackageData
    {
        public List<string> URL;

        [NonSerialized] public List<string> Names;

        public PackageData()
        {
            Names = new List<string>();
        }

        public void GetNames()
        {
            Names.Clear();
            foreach (var item in URL)
                Names.Add(Path.GetFileName(item).Replace(".git", ""));
        }

        public string GetURL(string key)
        {
            foreach (var item in URL)
            {
                if (Path.GetFileName(item).Replace(".git", "") == key)
                    return item;
            }

            throw new Exception("packagedata not find key");
        }
    }

    [GWindow("Package Manager", Group = "Tools", MinSizeWidth = 500, MinSizeHeight = 200)]
    public class PackageManagerWindow : GraphicWindow
    {
        public const string PACKAGE_CLONE_FILE = "AutoGit.ini";

        protected Vector2 Vector;

        protected PackageData Package;

        private bool PackageDataOnGUISwitch;

        private string PackagesPath;

        protected override void OnActivation()
        {
            PackagesPath = Application.dataPath.Replace("Assets", "Packages");
            var FilePath = Path.Combine(PackagesPath, PACKAGE_CLONE_FILE);
            if (File.Exists(FilePath))
            {
                Package = AHelper.IO.ReadJsonUTF8<PackageData>(FilePath);
                if (Package.URL == null) Package.URL = new List<string>();
            }
            else Package = new PackageData { URL = new List<string>() };

            Package.Names = new List<string>();
        }

        protected override void OnDraw()
        {
            EditorGUILayout.LabelField("Git安装包管理", new GUIStyle("PreLabel"));
            EditorGUILayout.Space();
            PackageDataOnGUISwitch = EditorGUILayout.ToggleLeft("URL List", PackageDataOnGUISwitch, "FoldoutHeader",
                GUILayout.ExpandWidth(true));
            if (PackageDataOnGUISwitch) PackageDataOnGUI();
            EditorGUILayout.Space();
            PackageFolderListOnGUI();
        }

        private void PackageDataOnGUI()
        {
            EditorGUILayout.BeginVertical("SelectionRect");
            for (var i = 0; i < Package.URL.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                Package.URL[i] = EditorGUILayout.TextField($"NO.{i + 1}", Package.URL[i]);
                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    Package.URL.RemoveAt(i);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                Package.URL.Add("");
            }

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                Package.URL.RemoveAt(Package.URL.Count - 1);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private string FolderTmep;

        private void PackageFolderListOnGUI()
        {
            Package.GetNames();
            EditorGUILayout.BeginVertical("SelectionRect");
            EditorGUILayout.LabelField("", "Package Folder List", "PreLabel");
            for (var i = 0; i < Package.Names.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel($"NO.{i + 1} {Package.Names[i]}");
                EditorGUILayout.Separator();
                FolderTmep = Path.Combine(PackagesPath, Package.Names[i]);
                if (Directory.Exists(FolderTmep))
                {
                    if (GUILayout.Button("Pull", GUILayout.Width(75)))
                    {
                        var path = Path.Combine(PackagesPath, Package.Names[i]);
                        PrPlatform.Git.Pull(path, false).Async();
                    }

                    if (GUILayout.Button("Del", GUILayout.Width(75)))
                    {
                        var path = Path.Combine(PackagesPath, Package.Names[i]);
                        Task.Factory.StartNew(() => PrPlatform.Folder.Del(path));
                    }
                }
                else
                {
                    if (GUILayout.Button("Clone", GUILayout.Width(75)))
                    {
                        var url = Package.GetURL(Package.Names[i]);
                        PrPlatform.Git.Clone(PackagesPath, url, false).Async();
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            var width = Mathf.FloorToInt(position.width / 5 - 5);
            if (GUILayout.Button("Clone All", GUILayout.Width(width), GUILayout.ExpandWidth(true)))
            {
                PrPlatform.Git.Clone(PackagesPath, Package.URL, false).Async();
            }

            if (GUILayout.Button("Pull  All", GUILayout.Width(width), GUILayout.ExpandWidth(true)))
            {
                var list = new List<string>();
                foreach (var item in Package.Names)
                {
                    var info = new DirectoryInfo(Path.Combine(PackagesPath, item));
                    if (info.Exists) list.Add(info.FullName);
                }

                PrPlatform.Git.Add(list, ".", false).Async();
            }

            if (GUILayout.Button("Del All", GUILayout.Width(width), GUILayout.ExpandWidth(true)))
            {
                async void Dels()
                {
                    await Task.Factory.StartNew(() =>
                    {
                        foreach (var item in Package.Names)
                        {
                            PrPlatform.Folder.Del(Path.Combine(PackagesPath, item));
                        }
                    });
                    AssetDatabase.Refresh();
                }

                Dels();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }

        protected override void OnDisable()
        {
            if (string.IsNullOrEmpty(PackagesPath)) return;
            if (Package is null) return;
            AHelper.IO.WriteJsonUTF8(Path.Combine(PackagesPath, PACKAGE_CLONE_FILE), Package);
        }
    }
}