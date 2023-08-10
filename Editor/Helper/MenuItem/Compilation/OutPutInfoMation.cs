/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIO.UEditor
{
    /// <summary>
    /// 输出信息
    /// </summary>
    internal static partial class OutPutInfoMation
    {
        [MenuItem("Tools/Compilation/输出 Player 编译程序集")]
        public static void PrintAssemblyNamesPlayer()
        {
            var playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);
            foreach (var assembly in playerAssemblies)
            {
                Debug.LogFormat("{0} -> {1}", assembly.name, assembly.outputPath);
            }
        }

        [MenuItem("Tools/Compilation/输出 Editor 编译程序集")]
        public static void PrintAssemblyNamesEditor()
        {
            var playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.Editor);
            foreach (var assembly in playerAssemblies)
            {
                Debug.LogFormat("{0} -> {1}", assembly.name, assembly.outputPath);
            }
        }

        [MenuItem("Tools/Compilation/输出 Player 和 Test 编译程序集")]
        public static void PrintAssemblyNamesPlayerWithoutTestAssemblies()
        {
            var playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.PlayerWithoutTestAssemblies);
            foreach (var assembly in playerAssemblies)
            {
                Debug.LogFormat("{0} -> {1}", assembly.name, assembly.outputPath);
            }
        }
    }
}