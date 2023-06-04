/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-06-04               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// 输出信息
    /// </summary>
    internal static partial class OutPutInfoMation
    {
        [MenuItem("System/Output/List Player Assemblies in Console")]
        public static void PrintAssemblyNames()
        {
            Debug.Log("== Player Assemblies ==");
            var playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);

            foreach (var assembly in playerAssemblies)
            {
                Debug.LogFormat("{0} -> {1}", assembly.name, assembly.outputPath);
            }
        }
    }
}