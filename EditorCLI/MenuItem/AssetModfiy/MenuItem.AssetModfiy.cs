#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// MenuItem_Assets
    /// </summary>
    public static class MenuItem_AssetModify
    {
        [MenuItem("AIO/Tools/AssetModify/查询资源丢失脚本")]
        private static void SelectGameObjects()
        {
            //Get the current scene and all top-level GameObjects in the scene hierarchy
            var path = Application.dataPath.Replace("Assets", "");

            var rootObjects = new Dictionary<string, GameObject>();
            foreach (var file in AssetDatabase.GetAllAssetPaths())
            {
                var extension = Path.GetExtension(file).ToLower();
                if (!extension.Contains("prefab")) continue;

                var full = Path.Combine(path, file);
                var a    = AssetDatabase.LoadAssetAtPath<GameObject>(file);
                if (!a) continue;
                rootObjects.Add(file, a);
            }


            var objectsWithDeadLinks = new List<Object>();
            foreach (var g in rootObjects)
            {
                var trans = g.Value.GetComponentsInChildren<Transform>();
                foreach (var obj in from tran in trans
                                    let components = tran.GetComponents<Component>()
                                    where !components.All(currentComponent => currentComponent)
                                    select tran.gameObject)
                {
                    objectsWithDeadLinks.Add(obj);
                    Selection.activeGameObject = obj;
                    Debug.LogFormat("{0} -> has a missing script! \n{1}", g.Key, obj); //Console中输出
                }
                //Get all components on the GameObject, then loop through them
            }

            if (objectsWithDeadLinks.Count > 0)
                //Set the selection in the editor
                Selection.objects = objectsWithDeadLinks.ToArray();
            else
                Debug.Log("No GameObjects missing scripts! Yay!");
        }
    }
}