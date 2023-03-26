using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.Unity.Editor
{
    public partial class UtilsEditor
    {
        /// <summary>
        /// Unity Editor IO Utils
        /// </summary>
        public static partial class IO

        {
            /// <summary>
            /// 获取指定文件夹下的预制件
            /// </summary>
            /// <param name="pattern">匹配模式</param>
            /// <param name="folder">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<T> GetAssetsRes<T>(
                in string pattern,
                params string[] folder
            ) where T : Object
            {
                if (string.IsNullOrEmpty(pattern)) return Array.Empty<T>();

                return AssetDatabase.FindAssets(pattern, folder)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .Where(value => value != null);
            }

            /// <summary>
            /// 获取指定文件夹下的预制件
            /// </summary>
            /// <param name="folder">文件夹</param>
            /// <param name="pattern"></param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<T> GetAssetsRes<T>(
                in string pattern,
                Func<T, bool> filtration,
                params string[] folder
            ) where T : Object
            {
                if (string.IsNullOrEmpty(pattern)) return Array.Empty<T>();
                return AssetDatabase.FindAssets(pattern, folder)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .Where(value => value != null && !filtration(value));
            }

            /// <summary>
            /// 获取资源的相对路径
            /// </summary>
            /// <param name="pattern">匹配模式</param>
            /// <param name="folders">文件夹</param>
            /// <returns>路径数组</returns>
            public static IEnumerable<string> GetAssetsPath(
                in string pattern,
                params string[] folders)
            {
                if (string.IsNullOrEmpty(pattern)) return Array.Empty<string>();
                return AssetDatabase.FindAssets(pattern, folders).Select(AssetDatabase.GUIDToAssetPath);
            }

            /// <summary>
            /// 获取资源的相对路径
            /// </summary>
            /// <param name="pattern">匹配模式</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="folders">文件夹</param>
            /// <returns>路径数组</returns>
            public static IEnumerable<string> GetAssetsPath(
                in string pattern,
                Func<string, bool> filtration,
                params string[] folders)
            {
                if (string.IsNullOrEmpty(pattern)) return Array.Empty<string>();
                return AssetDatabase.FindAssets(pattern, folders)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Where(value => !filtration(value)).ToArray();
            }

            /// <summary>
            /// 获取指定文件夹下的预制件
            /// </summary>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="folder">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<GameObject> GetAssetPrefabs(
                Func<GameObject, bool> filtration,
                params string[] folder)
            {
                return GetAssetsRes("t:Prefab", filtration, folder);
            }

            /// <summary>
            /// 获取指定文件夹下的预制件
            /// </summary>
            /// <param name="folder">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<GameObject> GetAssetPrefabs(params string[] folder)
            {
                return GetAssetsRes<GameObject>("t:Prefab", folder);
            }

            /// <summary>
            /// 获取指定文件夹下的音频文件
            /// </summary>
            /// <param name="folder">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<AnimationClip> GetAssetClips(params string[] folder)
            {
                return GetAssetsRes<AnimationClip>("t:Animation", folder);
            }

            /// <summary>
            /// 获取指定文件夹下的音频文件
            /// </summary>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="folder">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<AnimationClip> GetAssetClips(
                Func<AnimationClip, bool> filtration,
                params string[] folder)
            {
                return GetAssetsRes("t:Animation", filtration, folder);
            }

            /// <summary>
            /// 获取预制件身上的组件
            /// </summary>
            public static IEnumerable<T> GetAssetPrefabs<T>(params string[] folders) where T : Component
            {
                var list = new List<T>();
                foreach (var asset in GetAssetsRes<GameObject>("t:Prefab", folders))
                    list.AddRange(asset.GetComponentsInChildren<T>(true));
                return list.ToArray();
            }

            /// <summary>
            /// 获取预制件资源路径
            /// </summary>
            /// <param name="folders">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<string> GetAssetPrefabsPath(params string[] folders)
            {
                return GetAssetsPath("t:Prefab", folders);
            }

            /// <summary>
            /// 获取预制件资源路径
            /// </summary>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="folders">文件夹</param>
            /// <returns>预制件数组</returns>
            public static IEnumerable<string> GetAssetPrefabsPath(
                Func<string, bool> filtration,
                params string[] folders)
            {
                return GetAssetsPath("t:Prefab", filtration, folders);
            }
        }
    }
}