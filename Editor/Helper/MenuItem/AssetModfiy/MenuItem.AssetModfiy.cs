/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-10
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using YamlDotNet.Serialization;
using Object = UnityEngine.Object;

namespace AIO.UEditor
{
    /// <summary>
    /// MenuItem_Assets
    /// </summary>
    public class MenuItem_AssetModfiy
    {
        [MenuItem("Tools/AssetModfiy/修复脚本GUID")]
        public static void Test()
        {
            var assemblies = new List<Assembly>
            {
                Assembly.LoadFile(@"G:\UnityProject\G201\proj\third-plugins-back\client-core\DBVC.dll"),
                Assembly.LoadFile(@"G:\UnityProject\G201\proj\third-plugins-back\client-core\ClientCore.dll"),
            };
            var dirs = new List<DirectoryInfo>
            {
                new DirectoryInfo(@"G:\UnityProject\G201\proj\third-plugins\client-core\ClientCore"),
                new DirectoryInfo(@"G:\UnityProject\G201\proj\third-plugins\client-core\DBVC"),
            };
            var md5 = new Dictionary<string, string>
            {
                { "356a8f05a6726e645ade74e1e74b6523", "ClientCore" },
                { "53d0d244ae5b5d343b19aced455b29ca", "DBVC" },
                { "543bd8adf2811e447b9b5dc5b8c7feb1", "DOTweenPro" },
            };
            Test2(assemblies, dirs, md5);
        }

        private struct ScriptDataInfo
        {
            public string GUID;

            public long FileID;

            public string RealPath;

            public string FileName;

            public string NameSpace;
        }

        private static void Test2(
            ICollection<Assembly> assemblies,
            ICollection<DirectoryInfo> dirs,
            IDictionary<string, string> md5)
        {
            //
            var fileidDic = new Dictionary<long, string>();


            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract) continue;
                    if (!type.IsSubclassOf(typeof(Object))) continue;

                    var fileid = AHelper.FileID.Compute(type);
                    fileidDic.Add(fileid, type.FullName);
                    Console.WriteLine("{0} [ fileid : {1} ]", type.FullName, fileid);
                }
            }

            var guidDic = new Dictionary<string, string>();
            foreach (var directory in dirs)
            {
                foreach (var file in directory.GetFiles("*.cs", SearchOption.AllDirectories))
                {
                    if (file.Name.StartsWith("AssemblyInfo")) continue;
                    if (file.Extension.Contains(".meta")) continue;
                    if (!file.Extension.Contains(".cs")) continue;
                    var meta = string.Concat(file.FullName, ".meta");
                    if (!File.Exists(string.Concat(file.FullName, ".meta"))) continue;
                    var metaData = AHelper.Yaml.Deserialize<Hashtable>(File.ReadAllText(meta));


                    var namespacename = "";
                    foreach (var line in File.ReadLines(file.FullName))
                    {
                        if (line.StartsWith("namespace"))
                        {
                            namespacename = line.Replace("namespace ", "").Replace("{", "").Trim();
                            namespacename = string.Concat(namespacename, ".");
                            break;
                        }
                    }

                    namespacename = string.Concat(namespacename, file.Name.Replace(file.Extension, ""));

                    if (guidDic.ContainsKey(namespacename))
                    {
                        Debug.LogError(string.Format("Error: {0} {1} {2}", namespacename, guidDic[namespacename], metaData["guid"]));
                    }
                    else guidDic.Add(namespacename, metaData["guid"].ToString());

                    Console.WriteLine("{0} [ fileid : {1} ]", namespacename, metaData["guid"]);
                }
            }

            var path = Application.dataPath.Replace("Assets", "");

            var assetList = new List<string>();
            foreach (var file in AssetDatabase.GetAllAssetPaths())
            {
                if (file.Contains("SRDebugger")) continue;
                if (file.Contains("Sirenix")) continue;

                var full = Path.Combine(path, file);
                if (!File.Exists(full)) continue;

                var Extension = Path.GetExtension(full).ToLower();
                if (string.IsNullOrEmpty(Extension)) continue;
                if (Extension.Contains("cs")) continue;
                if (Extension.Contains("dll")) continue;
                if (Extension.Contains("txt")) continue;
                if (Extension.Contains("json")) continue;
                if (Extension.Contains("lua")) continue;
                if (Extension.Contains("bytes")) continue;

                if (Extension.Contains("png")) continue;
                if (Extension.Contains("jpg")) continue;
                if (Extension.Contains("mat")) continue;
                if (Extension.Contains("shader")) continue;
                if (Extension.Contains("mp3")) continue;
                if (Extension.Contains("fbx")) continue;
                if (Extension.Contains("font")) continue;
                if (Extension.Contains("otf")) continue;
                if (Extension.Contains("ttf")) continue;
                // if (Extension.Contains("unity")) continue;
                if (Extension.Contains("so")) continue;
                if (Extension.Contains("asmdef")) continue;
                if (Extension.Contains("uss")) continue;
                if (Extension.Contains("xml")) continue;
                if (Extension.Contains("prefs")) continue;


                assetList.Add(full);
            }


            var builder = new StringBuilder();
            foreach (var file in assetList)
            {
                var lines = File.ReadLines(file);
                var changed = false;
                builder.Clear();
                foreach (var line in lines)
                {
                    if (!(line.Contains("m_Script") && line.Contains("fileID") && line.Contains("guid")))
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    var arr = line.Split(':').Trim();
                    var fileid = long.Parse(arr[2].Split(',')[0]);
                    if (fileid == 11500000)
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    var guid = arr[3].Split(',')[0];
                    if (!md5.ContainsKey(guid))
                    {
                        builder.AppendLine(line);
                        continue;
                    }


                    if (!fileidDic.TryGetValue(fileid, out var newguid))
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    if (!guidDic.TryGetValue(newguid, out newguid))
                    {
                        builder.AppendLine(line);
                        continue;
                    }

                    builder.AppendLine(line.Replace(fileid.ToString(), "11500000").Replace(guid, newguid));
                    changed = true;
                }

                if (changed)
                {
                    Console.WriteLine(file);
                    File.WriteAllText(file, builder.ToString());
                }
            }
        }


        [MenuItem("Tools/AssetModfiy/查询资源丢失脚本")]
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
