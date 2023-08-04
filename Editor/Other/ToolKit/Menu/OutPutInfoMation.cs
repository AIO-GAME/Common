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
        [MenuItem("Tools/Output/List Player Assemblies in Console")]
        public static void PrintAssemblyNames()
        {
            Debug.Log("== Player Assemblies ==");
            var playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);

            foreach (var assembly in playerAssemblies)
            {
                Debug.LogFormat("{0} -> {1}", assembly.name, assembly.outputPath);
            }
        }

        [MenuItem("Tools/Scripts/GameObjects With Missing Scripts")]
        static void SelectGameObjects()
        {
            //Get the current scene and all top-level GameObjects in the scene hierarchy
            var path = Application.dataPath.Replace("Assets", "");

            var rootObjects = new Dictionary<string, GameObject>();
            foreach (var file in AssetDatabase.GetAllAssetPaths())
            {
                var extension = Path.GetExtension(file).ToLower();
                if (!extension.Contains("prefab")) continue;

                var full = Path.Combine(path, file);
                var a = AssetDatabase.LoadAssetAtPath<GameObject>(file);
                if (a is null) continue;
                rootObjects.Add(file, a);
            }


            var objectsWithDeadLinks = new List<Object>();
            foreach (var g in rootObjects)
            {
                var root = "root";
                var trans = g.Value.GetComponentsInChildren<Transform>();
                foreach (var tran in trans)
                {
                    var components = tran.GetComponents<Component>();
                    for (var i = 0; i < components.Length; i++)
                    {
                        var currentComponent = components[i];

                        //If the component is null, that means it's a missing script!
                        if (currentComponent == null)
                        {
                            //Add the sinner to our naughty-list
                            objectsWithDeadLinks.Add(tran.gameObject);
                            Selection.activeGameObject = tran.gameObject;
                            Debug.LogFormat("{0} -> has a missing script! \n{1}", g.Key, tran.gameObject); //Console中输出
                            break;
                        }
                    }
                }
                //Get all components on the GameObject, then loop through them
            }

            if (objectsWithDeadLinks.Count > 0)
            {
                //Set the selection in the editor
                Selection.objects = objectsWithDeadLinks.ToArray();
            }
            else
            {
                Debug.Log("No GameObjects missing scripts! Yay!");
            }
        }
    }
}
