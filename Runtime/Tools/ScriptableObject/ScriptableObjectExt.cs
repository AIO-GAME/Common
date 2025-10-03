using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO
{
    [Serializable]
    public class ScriptableObject<T> : ScriptableObject
    where T : ScriptableObject<T>
    {
#if UNITY_EDITOR
        private static T instance;
#endif

        private static T GetResource() { return Resources.LoadAll<T>(typeof(T).Name).FirstOrDefault(item => item); }

        public static T GetOrCreate()
        {
#if UNITY_EDITOR
            if (!instance)
            {
                var path    = Application.dataPath;
                var dirinfo = new DirectoryInfo(path);
                foreach (var assetPath in dirinfo.
                                          GetFiles($"{typeof(T).Name}.asset", SearchOption.AllDirectories).
                                          Select(info => info.FullName.Replace(dirinfo.FullName, "Assets")))
                {
                    instance = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (instance != null) break;
                }

                if (instance == null)
                {
                    foreach (var item in AssetDatabase.
                                         FindAssets($"t:{typeof(T).Name}", new[] { "Assets", }).
                                         Select(AssetDatabase.GUIDToAssetPath).
                                         Select(AssetDatabase.LoadAssetAtPath<T>).
                                         Where(item => item))
                    {
                        instance = item;
                        if (instance != null) break;
                    }
                }

                if (instance == null)
                {
                    instance = CreateInstance<T>();
                    var resourcesDir = Path.Combine(Application.dataPath, "Resources");
                    if (!Directory.Exists(resourcesDir)) Directory.CreateDirectory(resourcesDir);
                    try
                    {
                        AssetDatabase.CreateAsset(instance, $"Assets/Resources/{typeof(T).Name}.asset");
                        AssetDatabase.SaveAssets();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            if (instance == null) throw new Exception($"Not found {typeof(T).Name}.asset ! Please create it !");
            return instance;
#else
            return GetResource();
#endif
        }
    }
}