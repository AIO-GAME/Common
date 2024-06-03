#region

using System;
using System.IO;
using System.Reflection;

#endregion

namespace AIO
{
    /// <summary>
    ///     程序集扩展
    /// </summary>
    public static class ExtendAssembly
    {
        /// <summary>
        ///     程序集目录的完整路径，没有最后的斜杠 |
        ///     Full path to the assembly directory, without final slash
        /// </summary>
        public static string Directory(this Assembly assembly)
        {
            var path = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
            return path.Substring(path.Length - 3) != "dll"
                ? Path.GetDirectoryName(assembly.Location)
                : Path.GetDirectoryName(path);
        }
    }
}