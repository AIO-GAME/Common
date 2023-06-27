/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-27
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.IO;
using System.Reflection;

namespace UnityEngine
{
    /// <summary>
    /// 程序集扩展
    /// </summary>
    public static class AssemblyExtension
    {
        /// <summary>
        /// Path slash for AssetDatabase format
        /// </summary>
        private const string ADBPathSlash = "/";

        /// <summary>
        /// Path slash to replace for AssetDatabase format
        /// </summary>
        private const string ADBPathSlashToReplace = "\\";

        /// <summary>
        /// AssetDatabase path to the assembly directory, without final slash
        /// </summary>
        public static string ADBDir(this Assembly assembly)
        {
            var path = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
            return (path.Substring(path.Length - 3) != "dll"
                    ? Path.GetDirectoryName(assembly.Location)
                    : Path.GetDirectoryName(path)).Substring(Application.dataPath.Length - 6)
                .Replace(ADBPathSlashToReplace, ADBPathSlash);
        }
    }
}