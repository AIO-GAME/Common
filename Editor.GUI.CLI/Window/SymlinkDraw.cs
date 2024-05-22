#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR_WIN
using Microsoft.Win32.SafeHandles;
#endif

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 符号链接工具类
    ///	An editor utility for easily creating symlinks in your project.
    /// Adds a Menu item under `Assets/Create/Folder (Symlink)`, and
    /// draws a small indicator in the Project view for folders that are
    /// symlinks.
    /// </summary>
    internal class SymlinkDraw
    {
        // FileAttributes that match a junction folder.
        private const FileAttributes FOLDER_SYMLINK_ATTRIBS = FileAttributes.Directory | FileAttributes.ReparsePoint;

        // Style used to draw the symlink indicator in the project view.
        private static GUIStyle _SymlinkMarkerStyle;

        // Style used to draw the symlink indicator in the project view.
        private static GUIStyle _SymlinkErrorStyle;

        private static bool _ShowSymlink;

        //#A3E4D7 转化为 Color 类型 0.64f, 0.89f, 0.84f, 0.5f
        private static readonly Color BackgroundColor = new Color(0.64f, 0.89f, 0.84f, 0.0f);
        //#2ECC71 转化为 Color 类型 0.18f, 0.8f, 0.44f, 0.5f
        private static readonly Color ErrorColor      = new Color(0.18f, 0.8f, 0.44f, 0.3f);

        private static GUIStyle SymlinkMarkerStyle
        {
            get
            {
                if (_SymlinkMarkerStyle is null)
                    _SymlinkMarkerStyle = new GUIStyle(EditorStyles.label)
                    {
                        normal    = { textColor = BackgroundColor }, // #2ECC71
                        alignment = TextAnchor.MiddleRight
                    };

                return _SymlinkMarkerStyle;
            }
        }

        private static GUIStyle SymlinkErrorStyle
        {
            get
            {
                if (_SymlinkErrorStyle is null)
                    _SymlinkErrorStyle = new GUIStyle(EditorStyles.label)
                    {
                        normal    = { textColor = ErrorColor }, // #E91E63
                        alignment = TextAnchor.MiddleRight
                    };

                return _SymlinkErrorStyle;
            }
        }

        [SettingsProvider]
        protected static SettingsProvider SettingsProvider()
        {
            var provider = new GraphicSettingsProvider("AIO/Symlink", SettingsScope.User)
            {
                label = "Symlink Tool",
                guiHandler = delegate
                {
                    GUILayout.Label("General", EditorStyles.boldLabel);
                    GUILayout.BeginVertical();
                    GUILayout.Space(10);
                    if (GUILayout.Button(GetShowSymlink() ? "Hide Symlink" : "Show Symlink"))
                    {
                        SetShowSymlink(!GetShowSymlink());
                        _ShowSymlink = !_ShowSymlink;
                        if (_ShowSymlink)
                            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
                        else
                            EditorApplication.projectWindowItemOnGUI -= OnProjectWindowItemGUI;
                    }

                    GUILayout.Space(10);
                    GUILayout.EndVertical();

                    GUILayout.FlexibleSpace();
                    EditorGUILayout.LabelField($"Version {Setting.Version}", EditorStyles.centeredGreyMiniLabel);
                }
            };
            return provider;
        }

        /// <summary>
        /// Static constructor subscribes to projectWindowItemOnGUI delegate.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // 创建渐变 从左到右
            texture = new Texture2D(2, 1) { wrapMode = TextureWrapMode.Clamp, };
            texture.SetPixel(0, 0, BackgroundColor);
            texture.SetPixel(2, 0, ErrorColor);
            texture.Apply();

            _ShowSymlink = GetShowSymlink();
            if (_ShowSymlink) EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        /// <summary>
        /// 显示符号链接
        /// </summary>
        private static bool GetShowSymlink() { return EditorPrefs.GetBool(typeof(SymlinkDraw).FullName, true); }

        /// <summary>
        /// 显示符号链接
        /// </summary>
        private static void SetShowSymlink(bool value) { EditorPrefs.SetBool(typeof(SymlinkDraw).FullName, value); }

        /// <summary>
        /// Draw a little indicator if folder is a symlink
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="r"></param>
        private static void OnProjectWindowItemGUI(string guid, Rect r)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path)) return;
            if (!Directory.Exists(path)) return;
            var attribs = File.GetAttributes(path);
            if ((attribs & FOLDER_SYMLINK_ATTRIBS) != FOLDER_SYMLINK_ATTRIBS) return;
            r.x     += r.width - 15; //7
            r.width =  15;
            Draw(r, path);
        }

        private static async void Draw(Rect rect, string path)
        {
            // EditorGUI.DrawRect(rect, BackgroundColor);
            DrawGradientRect(rect);

            if (!GUI.Button(rect, GUIContent.none, SymlinkMarkerStyle)) return;
            var realPath = GetRealPath(path).Replace('\\', '/');
            try
            {
                var executor = PrPlatform.Open.Path(realPath);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var result = await executor.Async();
                executor.EnableOutput = temp;
                if (result.ExitCode != 0) EditorUtility.RevealInFinder(realPath);
            }
            catch
            {
                EditorUtility.RevealInFinder(realPath);
            }
        }

        private static Texture2D texture;

        private static void DrawGradientRect(Rect rect)
        {
            // 绘制渐变颜色的矩形
            Handles.BeginGUI();
            GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true); // 使用矩形工具绘制带有纹理的矩形
            Handles.EndGUI();
        }

        private static async void ErrorDraw(Rect rect, string path = null)
        {
            EditorGUI.DrawRect(rect, ErrorColor);
            if (!GUI.Button(rect, GUIContent.none, SymlinkErrorStyle)) return;
            if (string.IsNullOrEmpty(path)) return;
            var realPath = GetRealPath(path).Replace('\\', '/');
            try
            {
                var result = await PrPlatform.Open.Path(realPath).Async();
                if (result.ExitCode != 0) EditorUtility.RevealInFinder(realPath);
            }
            catch
            {
                EditorUtility.RevealInFinder(realPath);
            }
        }

        // Create an absolute symbolic link
        /// <summary>
        /// Add a menu item in the Assets/Create category to add symlinks to directories.
        /// </summary>
        [MenuItem("Assets/Create/Folder (Absolute Symlink)", false, 20)]
        private static void SymlinkAbsolute() { Symlink(true); }

        // Create a relative symbolic link
        [MenuItem("Assets/Create/Folder (Relative Symlink)", false, 21)]
        private static void SymlinkRelative() { Symlink(false); }

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

            var sourceFolderName = sourceFolderPath.Split('/', '\\').LastOrDefault();

            if (string.IsNullOrEmpty(sourceFolderName))
            {
                Debug.LogWarning("Couldn't deduce the folder name?");
                return;
            }

            var activeObject = Selection.activeObject;

            var targetPath = AssetDatabase.GetAssetPath(activeObject) ?? "Assets";

            var attribs = File.GetAttributes(targetPath);

            if ((attribs & FileAttributes.Directory) != FileAttributes.Directory)
                targetPath = Path.GetDirectoryName(targetPath);

            // Get path to project.
            var pathToProject = Application.dataPath.Split(new[] { "/Assets" }, StringSplitOptions.None)[0];

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
                    ExecuteBashCommand($"ln -s {sourcePath} {targetPath}");
                    break;
                }
                case RuntimePlatform.LinuxEditor:
                // Is Linux the same as OSX?
                default: break;
            }

            Debug.Log($"Created symlink: {targetPath} <=> {sourceFolderPath}");
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        private static string GetRelativePath(string sourcePath, string outputPath)
        {
            if (string.IsNullOrEmpty(outputPath))
                return sourcePath;
            if (sourcePath == null)
                sourcePath = string.Empty;

            var splitOutput =
                outputPath.Split(new[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var splitSource =
                sourcePath.Split(new[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);

            var max = Mathf.Min(splitOutput.Length, splitSource.Length);
            var i   = 0;
            while (i < max)
            {
                if (splitOutput[i] != splitSource[i]) break;
                ++i;
            }

            var hopUpCount                                = splitOutput.Length - i - 1;
            var newSplitCount                             = hopUpCount + splitSource.Length - i;
            var newSplitTarget                            = new string[newSplitCount];
            var j                                         = 0;
            for (; j < hopUpCount; ++j) newSplitTarget[j] = "..";

            for (max = newSplitTarget.Length; j < max; ++j, ++i) newSplitTarget[j] = splitSource[i];

            return string.Join(Path.PathSeparator.ToString(), newSplitTarget);
        }

        private static void ExecuteBashCommand(string command)
        {
            command = command.Replace("\"", "\"\"");

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName               = "/bin/bash",
                    Arguments              = "-c \"" + command + "\"",
                    UseShellExecute        = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    CreateNoWindow         = true
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

        public static string GetRealPath(string path)
        {
            if (!Directory.Exists(path) && !File.Exists(path))
            {
                Console.WriteLine(new IOException("Path not found"));
                return path;
            }

#if UNITY_EDITOR_WIN
            var directoryHandle = CreateFile(path, 0, 2, IntPtr.Zero,
                                             CREATION_DISPOSITION_OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero); //Handle file / folder

            if (directoryHandle.IsInvalid)
            {
                Console.WriteLine(new Win32Exception(Marshal.GetLastWin32Error()));
                return path;
            }

            var result  = new StringBuilder(512);
            var mResult = GetFinalPathNameByHandle(directoryHandle, result, result.Capacity, 0);

            if (mResult < 0)
            {
                Console.WriteLine(new Win32Exception(Marshal.GetLastWin32Error()));
                return path;
            }

            if (result.Length >= 4 && result[0] == '\\' && result[1] == '\\' && result[2] == '?' && result[3] == '\\')
                return result.ToString().Substring(4); // "\\?\" remove

            return result.ToString();

#elif UNITY_EDITOR_OSX
            // 在OSX上获取符号链接的真实路径
            return path;
#else
            return path;
#endif
        }

#if UNITY_EDITOR_WIN

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName,
                                                        int    dwDesiredAccess,
                                                        int    dwShareMode,
                                                        IntPtr securityAttributes,
                                                        int    dwCreationDisposition,
                                                        int    dwFlagsAndAttributes,
                                                        IntPtr hTemplateFile);

        [DllImport("kernel32.dll", EntryPoint = "GetFinalPathNameByHandleW", CharSet = CharSet.Unicode,
                      SetLastError = true)]
        private static extern int GetFinalPathNameByHandle([In]  SafeFileHandle hFile,
                                                           [Out] StringBuilder  lpszFilePath,
                                                           [In]  int            cchFilePath,
                                                           [In]  int            dwFlags);

        private const int CREATION_DISPOSITION_OPEN_EXISTING = 3;
        private const int FILE_FLAG_BACKUP_SEMANTICS         = 0x02000000;

#endif
    }
}