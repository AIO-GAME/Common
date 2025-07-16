/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-07-01
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.IO;

namespace AIO
{
    partial class AHelper
    {
        partial class IO
        {
            /// <summary>
            /// 设置Windows文件夹图标
            /// </summary>
            /// <param name="exe"> exe文件路径 </param>
            /// <param name="absolute"> 是否使用绝对路径 </param>
            public static bool SetWinDirIcon(string exe, bool absolute = false)
            {
                var fileinfo      = new FileInfo(exe);
                var directoryInfo = fileinfo.Directory;
                return SetWinDirIcon(directoryInfo.FullName, exe, absolute);
            }

            /// <summary>
            /// 设置Windows文件夹图标
            /// </summary>
            /// <param name="dir"> 目标文件夹路径 </param>
            /// <param name="exe"> exe文件路径 </param>
            /// <param name="absolute"> 是否使用绝对路径 </param>
            public static bool SetWinDirIcon(string dir, string exe, bool absolute = false)
            {
                if (string.IsNullOrEmpty(dir) && Directory.Exists(dir)) return false;
                var fileinfo = new FileInfo(exe);
                if (!fileinfo.Exists) return false;
                var dirInfo = new DirectoryInfo(dir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)); // 将文件夹设置为只读
                var text = absolute
                    ? $"IconResource={fileinfo.FullName.Replace(dirInfo.FullName, "").TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)},0"
                    : $"IconResource={fileinfo.FullName},0";

                var desktopIniPath = Path.Combine(dir, "desktop.ini");
                try
                {
                    if (!File.Exists(desktopIniPath))
                        File.WriteAllText(desktopIniPath, $"[.ShellClassInfo]\n{text}");
                    else
                    {
                        File.SetAttributes(desktopIniPath, FileAttributes.Normal);
                        var content = File.ReadAllText(desktopIniPath);
                        if (!content.Contains("[.ShellClassInfo]"))
                        {
                            content = $"{content}\n[.ShellClassInfo]\n{text}";
                        }
                        else if (!content.Contains("IconResource="))
                        {
                            var index = content.IndexOf("[.ShellClassInfo]", StringComparison.CurrentCultureIgnoreCase);
                            if (index >= 0)
                                content = content.Insert(index + "[.ShellClassInfo]".Length, $"\n{text}\n");
                        }
                        else
                        {
                            var lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                            for (var i = 0; i < lines.Length; i++)
                            {
                                if (!lines[i].StartsWith("IconResource=", StringComparison.CurrentCultureIgnoreCase)) continue;
                                lines[i] = text;
                                break;
                            }

                            content = string.Join("\n", lines);
                        }

                        File.WriteAllText(desktopIniPath, content);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error setting icon for directory '{dirInfo.FullName}': {e.Message}");
                    return false;
                }

                File.SetAttributes(desktopIniPath, FileAttributes.Hidden | FileAttributes.System);

                if (!dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
                {
                    dirInfo.Attributes |= FileAttributes.ReadOnly;
                    dirInfo.Refresh();
                }

                return true;
            }
        }
    }
}