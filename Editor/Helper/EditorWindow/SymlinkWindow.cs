/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIO.UEditor
{
    /// <summary>
    /// 符号链接工具类
    ///	An editor utility for easily creating symlinks in your project.
    /// Adds a Menu item under `Assets/Create/Folder (Symlink)`, and
    /// draws a small indicator in the Project view for folders that are
    /// symlinks.
    /// </summary>
    [InitializeOnLoad]
    [GWindow("符号链接", "Symlink Window", Group = "Tools",
        MinSizeWidth = 600, MinSizeHeight = 600,
        MaxSizeWidth = 600, MaxSizeHeight = 600
    )]
    internal class SymlinkWindow : GraphicWindow
    {
        // FileAttributes that match a junction folder.
        private const FileAttributes FOLDER_SYMLINK_ATTRIBS = FileAttributes.Directory | FileAttributes.ReparsePoint;

        // Style used to draw the symlink indicator in the project view.
        private static GUIStyle _SymlinkMarkerStyle;

        private static GUIStyle SymlinkMarkerStyle
        {
            get
            {
                if (_SymlinkMarkerStyle is null)
                {
                    _SymlinkMarkerStyle = new GUIStyle(EditorStyles.label)
                    {
                        normal = { textColor = new Color(.2f, .8f, .2f, .8f) },
                        alignment = TextAnchor.MiddleRight
                    };
                }

                return _SymlinkMarkerStyle;
            }
        }

        [SettingsProvider]
        protected static SettingsProvider SettingsProvider()
        {
            var provider = CreateSettingsProvider("Symlink");
            provider.label = "Symlink Tool";
            provider.guiHandler = delegate
            {
                GELayout.Label("General", EditorStyles.boldLabel);
                GELayout.BeginVertical();
                GELayout.Space();
                if (GELayout.Button(ShowSymlink ? "Hide Symlink" : "Show Symlink"))
                {
                    ShowSymlink = _ShowSymlink = !_ShowSymlink;
                }

                GELayout.Space();
                GELayout.EndVertical();
            };
            return provider;
        }

        /// <summary>
        /// Static constructor subscribes to projectWindowItemOnGUI delegate.
        /// </summary>
        static SymlinkWindow()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        /// <summary>
        /// 显示符号链接
        /// </summary>
        private static bool _ShowSymlink
        {
            get => EditorPrefs.GetBool("AIO.Symlink.ShowSymlink", true);
            set => EditorPrefs.SetBool("AIO.Symlink.ShowSymlink", value);
        }

        private static bool ShowSymlink = _ShowSymlink;

        /// <summary>
        /// Draw a little indicator if folder is a symlink
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="r"></param>
        private static void OnProjectWindowItemGUI(string guid, Rect r)
        {
            if (!ShowSymlink) return;
            try
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                if (!string.IsNullOrEmpty(path))
                {
                    var attribs = File.GetAttributes(path);

                    if ((attribs & FOLDER_SYMLINK_ATTRIBS) == FOLDER_SYMLINK_ATTRIBS)
                        GUI.Label(r, "<=>", SymlinkMarkerStyle);
                }
            }
            catch
            {
                // ignored
            }
        }

        /**
             *	Add a menu item in the Assets/Create category to add symlinks to directories.
             */
        // Create an absolute symbolic link
        [MenuItem("Assets/Create/Folder (Absolute Symlink)", false, 20)]
        private static void SymlinkAbsolute()
        {
            Symlink(true);
        }

        // Create a relative symbolic link
        [MenuItem("Assets/Create/Folder (Relative Symlink)", false, 21)]
        private static void SymlinkRelative()
        {
            Symlink(false);
        }

        private static void Symlink(bool absolute)
        {
            var sourceFolderPath = EditorUtility.OpenFolderPanel("Select Folder Source", "", "");

            // Cancelled dialog
            if (string.IsNullOrEmpty(sourceFolderPath)) return;

            if (sourceFolderPath.Contains(Application.dataPath))
            {
                Debug.LogWarning("Cannot create a symlink to folder in your project!");
                return;
            }

            var sourceFolderName = sourceFolderPath.Split(new char[] { '/', '\\' }).LastOrDefault();

            if (string.IsNullOrEmpty(sourceFolderName))
            {
                Debug.LogWarning("Couldn't deduce the folder name?");
                return;
            }

            var activeObject = Selection.activeObject;

            var targetPath = activeObject != null ? AssetDatabase.GetAssetPath(activeObject) : null;

            if (string.IsNullOrEmpty(targetPath)) targetPath = "Assets";

            var attribs = File.GetAttributes(targetPath);

            if ((attribs & FileAttributes.Directory) != FileAttributes.Directory)
                targetPath = Path.GetDirectoryName(targetPath);

            // Get path to project.
            var pathToProject = Application.dataPath.Split(new string[] { "/Assets" }, StringSplitOptions.None)[0];

            targetPath = $"{pathToProject}/{targetPath}/{sourceFolderName}";

            if (Directory.Exists(targetPath))
            {
                Debug.LogWarning(
                    $"A folder already exists at this location, aborting link.\n{sourceFolderPath} -> {targetPath}");
                return;
            }

            // Use absolute path or relative path?
            var sourcePath = absolute ? sourceFolderPath : GetRelativePath(sourceFolderPath, targetPath);
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                {
                    using (var cmd = Process.Start("CMD.exe", $"/C mklink /J \"{targetPath}\" \"{sourcePath}\""))
                    {
                        cmd?.WaitForExit();
                    }

                    break;
                }
                case RuntimePlatform.OSXEditor:
                {
                    // For some reason, OSX doesn't want to create a symlink with quotes around the paths, so escape the spaces instead.
                    sourcePath = sourcePath.Replace(" ", "\\ ");
                    targetPath = targetPath.Replace(" ", "\\ ");
                    var command = $"ln -s {sourcePath} {targetPath}";
                    ExecuteBashCommand(command);
                    break;
                }
                case RuntimePlatform.LinuxEditor:
                // Is Linux the same as OSX?
                default:
                    break;
            }

            Debug.Log(string.Format("Created symlink: {0} <=> {1}", targetPath, sourceFolderPath));
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        private static string GetRelativePath(string sourcePath, string outputPath)
        {
            if (string.IsNullOrEmpty(outputPath))
                return sourcePath;
            if (sourcePath == null)
                sourcePath = string.Empty;

            var splitOutput =
                outputPath.Split(new char[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var splitSource =
                sourcePath.Split(new char[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);

            var max = Mathf.Min(splitOutput.Length, splitSource.Length);
            var i = 0;
            while (i < max)
            {
                if (splitOutput[i] != splitSource[i]) break;
                ++i;
            }

            var hopUpCount = splitOutput.Length - i - 1;
            var newSplitCount = hopUpCount + splitSource.Length - i;
            var newSplitTarget = new string[newSplitCount];
            var j = 0;
            for (; j < hopUpCount; ++j)
            {
                newSplitTarget[j] = "..";
            }

            for (max = newSplitTarget.Length; j < max; ++j, ++i)
            {
                newSplitTarget[j] = splitSource[i];
            }

            return string.Join(Path.PathSeparator.ToString(), newSplitTarget);
        }

        private static void ExecuteBashCommand(string command)
        {
            command = command.Replace("\"", "\"\"");

            var proc = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            using (proc)
            {
                proc.Start();
                proc.WaitForExit();

                if (!proc.StandardError.EndOfStream)
                    Debug.LogError(proc.StandardError.ReadToEnd());
            }
        }
    }
}