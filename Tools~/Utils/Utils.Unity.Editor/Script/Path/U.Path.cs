namespace AIO.Unity.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    using UnityEditor;

    using UnityEngine;

    using Debug = UnityEngine.Debug;

    public static partial class UtilsEditor
    {
        /// <summary>
        /// 提供了一些与路径相关的实用方法。
        /// 包含与程序集有关的实用方法和属性的静态类
        /// </summary>
        public static class Paths
        {
            static Paths()
            {
                Assets = Application.dataPath;
                Editor = EditorApplication.applicationPath;
                EditorContents = EditorApplication.applicationContentsPath;
                StreamingAssets = Application.streamingAssetsPath;
                PersistentData = Application.persistentDataPath;
                Project = Directory.GetParent(Assets).FullName;
                Logs = Path.Combine(Project, "Logs");
                Packages = Path.Combine(Project, "Packages");
                UserSettings = Path.Combine(Project, "UserSettings");

                ProjectSettings = Path.Combine(Project, "ProjectSettings");
                Backups = Path.Combine(Project, "Editor Default Resources");
                EditorDefaultResources = Path.Combine(Project, "Backups");

                try
                {
                    SyncVS = typeof(Editor).Assembly.GetType("UnityEditor.SyncVS", true);
                    SyncVS_SyncSolution = SyncVS.GetMethod("SyncSolution", BindingFlags.Static | BindingFlags.Public);

                    if (SyncVS_SyncSolution == null) throw new MissingMemberException(SyncVS.ToString(), "SyncSolution");
                }
                catch (Exception ex)
                {
                    throw new UnityEditorInternalException(ex);
                }
            }

            /// <summary>
            /// 获取当前项目的 ProjectSettings 文件夹的完整路径。
            /// </summary>
            public static string ProjectSettings { get; }

            /// <summary>
            /// 获取当前项目的 Editor Default Resources 文件夹的完整路径。
            /// </summary>
            public static string EditorDefaultResources { get; }

            /// <summary>
            /// 获取当前项目的 Backups 文件夹的完整路径。
            /// </summary>
            public static string Backups { get; }

            /// <summary>
            /// 用户自定义设置
            /// </summary>
            public static string UserSettings { get; }

            /// <summary>
            /// 项目日志文件夹路径
            /// </summary>
            public static string Packages { get; }

            /// <summary>
            /// 项目日志文件夹路径
            /// </summary>
            public static string Logs { get; }

            /// <summary>
            /// 获取当前项目 Streaming Assets 文件夹的完整路径。
            /// </summary>
            public static string StreamingAssets { get; }

            /// <summary>
            /// 获取当前项目 Persistent Assets 文件夹的完整路径。
            /// </summary>
            public static string PersistentData { get; }

            /// <summary>
            /// 获取当前项目 Assets 文件夹的完整路径。
            /// </summary>
            public static string Assets { get; }

            /// <summary>
            /// 获取 Unity 编辑器的可执行文件的完整路径。
            /// </summary>
            public static string Editor { get; }

            /// <summary>
            /// 获取 Unity 编辑器的安装目录的完整路径。
            /// </summary>
            public static string EditorContents { get; }

            /// <summary>
            /// 获取当前项目所在文件夹的完整路径。
            /// </summary>
            public static string Project { get; }

            /// <summary>
            /// 获取当前项目名称。
            /// </summary>
            public static string ProjectName => Path.GetFileName(Project.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));

            #region Assembly Projects

            /// <summary>
            /// Unity编辑器内部的SyncVS类型
            /// </summary>
            private static Type SyncVS;

            /// <summary>
            /// SyncVS.SyncSolution()方法的MethodInfo对象
            /// </summary>
            private static MethodInfo SyncVS_SyncSolution;

            /// <summary>
            /// 同步Unity工程的解决方案文件
            /// </summary>
            public static void SyncUnitySolution()
            {
                try
                {
                    SyncVS_SyncSolution.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    throw new UnityEditorInternalException(ex);
                }
            }

            /// <summary>
            /// 获取程序集的第一编译阶段所对应的项目文件的完整路径
            /// </summary>
            public static string RuntimeAssemblyFirstPassProject =>
                PreferredProjectPath
                (
                    Path.Combine(Project, ProjectName + ".Plugins.csproj"),
                    Path.Combine(Project, "Assembly-CSharp-firstpass.csproj")
                );

            /// <summary>
            /// 获取程序集的第二编译阶段所对应的项目文件的完整路径
            /// </summary>
            public static string RuntimeAssemblySecondPassProject =>
                PreferredProjectPath
                (
                    Path.Combine(Project, ProjectName + ".csproj"),
                    Path.Combine(Project, "Assembly-CSharp.csproj")
                );

            /// <summary>
            /// 获取编辑器程序集的第一编译阶段所对应的项目文件的完整路径
            /// </summary>
            public static string EditorAssemblyFirstPassProject =>
                PreferredProjectPath
                (
                    Path.Combine(Project, ProjectName + ".Editor.csproj"),
                    Path.Combine(Project, "Assembly-CSharp-Editor-firstpass.csproj")
                );

            /// <summary>
            /// 获取编辑器程序集的第二编译阶段所对应的项目文件的完整路径
            /// </summary>
            public static string EditorAssemblySecondPassProject =>
                PreferredProjectPath
                (
                    Path.Combine(Project, ProjectName + ".Editor.Plugins.csproj"),
                    Path.Combine(Project, "Assembly-CSharp-Editor.csproj")
                );

            /// <summary>
            /// 获取所有与程序集相关的项目文件的完整路径
            /// </summary>
            public static IEnumerable<string> AssemblyProjects
            {
                get
                {
                    var firstPass = RuntimeAssemblyFirstPassProject;
                    var secondPass = RuntimeAssemblySecondPassProject;
                    var editorFirstPass = EditorAssemblyFirstPassProject;
                    var editorSecondPass = EditorAssemblySecondPassProject;

                    if (firstPass != null)
                    {
                        yield return firstPass;
                    }

                    if (secondPass != null)
                    {
                        yield return secondPass;
                    }

                    if (editorFirstPass != null)
                    {
                        yield return editorFirstPass;
                    }

                    if (editorSecondPass != null)
                    {
                        yield return editorSecondPass;
                    }
                }
            }

            /// <summary>
            /// 比较两个项目文件的最后写入时间，选择其中一个作为当前项目的项目文件
            /// </summary>
            private static string PreferredProjectPath(string path1, string path2)
            {
                if (!File.Exists(path1) && !File.Exists(path2))
                {
                    return null;
                }

                if (!File.Exists(path1))
                {
                    return path2;
                }

                if (!File.Exists(path2))
                {
                    return path1;
                }

                var timestamp1 = File.GetLastWriteTime(path1);
                var timestamp2 = File.GetLastWriteTime(path2);

                if (timestamp1 >= timestamp2)
                {
                    return path1;
                }

                return path2;
            }

            #endregion


            #region .NET

            /// <summary>
            /// MSBuild的下载链接
            /// </summary>
            public const string MsBuildDownloadLink = "https://aka.ms/vs/15/release/vs_buildtools.exe";

            /// <summary>
            /// 获取系统环境变量PATH并分割成字符串数组
            /// </summary>
            private static IEnumerable<string> environmentPaths
            {
                get
                {
                    if (Application.platform == RuntimePlatform.WindowsEditor)
                    {
                        return Environment.GetEnvironmentVariable("PATH").Split(';');
                    }

                    // http://stackoverflow.com/a/41318134/154502
                    var start = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = "-l -c \"echo $PATH\"", // -l = 'login shell' to execute /etc/profile
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    var process = Process.Start(start);
                    process.WaitForExit();
                    var path = process.StandardOutput.ReadToEnd().Trim();
                    return path.Split(':');
                }
            }

            // ProgramFilesx86 is not available until .NET 4
            // https://stackoverflow.com/questions/194157/
            /// <summary>
            /// 获取x86程序文件夹路径，兼容32位和64位操作系统
            /// </summary>
            private static string ProgramFilesx86
            {
                get
                {
                    if (IntPtr.Size == 8 || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
                    {
                        return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                    }

                    return Environment.GetEnvironmentVariable("ProgramFiles");
                }
            }

            /// <summary>
            /// 获取MSBuild.exe文件的完整路径
            /// </summary>
            public static string MSBuild
            {
                get
                {
                    if (Application.platform != RuntimePlatform.WindowsEditor)
                    {
                        return null;
                    }

                    try
                    {
                        var startInfo = new ProcessStartInfo
                        {
                            FileName = Path.Combine(ProgramFilesx86, @"Microsoft Visual Studio\Installer\vswhere.exe"),
                            Arguments = @"-latest -prerelease -products * -requires Microsoft.Component.MSBuild -find **\Bin\MSBuild.exe",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        };

                        using (var vsWhere = Process.Start(startInfo))
                        {
                            var firstPath = vsWhere.StandardOutput.ReadLine();
                            vsWhere.WaitForExit();
                            return firstPath;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"Failed to find MSBuild path via VSWhere utility.\n{ex}");
                        return null;
                    }
                }
            }

            /// <summary>
            /// 获取xbuild命令的完整路径
            /// </summary>
            public static string XBuild
            {
                get
                {
                    if (Application.platform == RuntimePlatform.WindowsEditor)
                    {
                        return null;
                    }

                    var path = Utils.IO.TryPathsForFile("xbuild", environmentPaths);

                    return path;
                }
            }

            /// <summary>
            /// Roslyn编译器csc.exe的完整路径
            /// </summary>
            public static string RoslynCompiler => Path.Combine(Path.GetDirectoryName(Editor), "Data/tools/Roslyn/csc.exe");

            /// <summary>
            /// 项目构建工具的完整路径
            /// </summary>
            public static string ProjectBuilder => Application.platform == RuntimePlatform.WindowsEditor ? MSBuild : XBuild;

            #endregion
        }
    }
}