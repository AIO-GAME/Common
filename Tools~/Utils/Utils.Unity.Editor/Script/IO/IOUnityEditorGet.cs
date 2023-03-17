using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

using Object = UnityEngine.Object;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// Unity Editor IO Utils
    /// </summary>
    public static partial class UEIOUtils
    {
        /// <summary>
        /// 获取指定文件夹下的预制件
        /// </summary>
        /// <param name="pattern">匹配模式</param>
        /// <param name="folder">文件夹</param>
        /// <returns>预制件数组</returns>
        public static T[] GetAssetsRes<T>(string pattern, params string[] folder) where T : Object
        {
            if (string.IsNullOrEmpty(pattern)) return new T[] { };
            var list = new List<T>();
            foreach (var guid in AssetDatabase.FindAssets(pattern, folder))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var value = AssetDatabase.LoadAssetAtPath<T>(path);
                if (value != null) list.Add(value);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取指定文件夹下的预制件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="pattern"></param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <returns>预制件数组</returns>
        public static T[] GetAssetsRes<T>(string pattern, Func<T, bool> filtration, params string[] folder) where T : Object
        {
            if (string.IsNullOrEmpty(pattern)) return new T[] { };
            var list = new List<T>();
            foreach (var guid in AssetDatabase.FindAssets(pattern, folder))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var value = AssetDatabase.LoadAssetAtPath<T>(path);
                if (value != null && !filtration(value)) list.Add(value);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取资源的相对路径
        /// </summary>
        /// <param name="pattern">匹配模式</param>
        /// <param name="folders">文件夹</param>
        /// <returns>路径数组</returns>
        public static string[] GetAssetsPath(string pattern, params string[] folders)
        {
            if (string.IsNullOrEmpty(pattern)) return new string[] { };
            var guids = AssetDatabase.FindAssets(pattern, folders);
            var r = new List<string>();
            foreach (var guid in guids)
                r.Add(AssetDatabase.GUIDToAssetPath(guid));
            return r.ToArray();
        }

        /// <summary>
        /// 获取资源的相对路径
        /// </summary>
        /// <param name="pattern">匹配模式</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="folders">文件夹</param>
        /// <returns>路径数组</returns>
        public static string[] GetAssetsPath(string pattern, Func<string, bool> filtration, params string[] folders)
        {
            if (string.IsNullOrEmpty(pattern)) return new string[] { };
            var guids = AssetDatabase.FindAssets(pattern, folders);
            var r = new List<string>();
            foreach (var guid in guids)
            {
                var value = AssetDatabase.GUIDToAssetPath(guid);
                if (!filtration(value)) r.Add(value);
            }

            return r.ToArray();
        }

        /// <summary>
        /// 获取指定文件夹下的预制件
        /// </summary>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="folder">文件夹</param>
        /// <returns>预制件数组</returns>
        public static GameObject[] GetAssetPrefabs(Func<GameObject, bool> filtration, params string[] folder)
        {
            return GetAssetsRes("t:Prefab", filtration, folder);
        }

        /// <summary>
        /// 获取指定文件夹下的预制件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <returns>预制件数组</returns>
        public static GameObject[] GetAssetPrefabs(params string[] folder)
        {
            return GetAssetsRes<GameObject>("t:Prefab", folder);
        }

        /// <summary>
        /// 获取指定文件夹下的音频文件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <returns>预制件数组</returns>
        public static AnimationClip[] GetAssetClips(params string[] folder)
        {
            return GetAssetsRes<AnimationClip>("t:Animation", folder);
        }

        /// <summary>
        /// 获取指定文件夹下的音频文件
        /// </summary>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="folder">文件夹</param>
        /// <returns>预制件数组</returns>
        public static AnimationClip[] GetAssetClips(Func<AnimationClip, bool> filtration, params string[] folder)
        {
            return GetAssetsRes("t:Animation", filtration, folder);
        }

        /// <summary>
        /// 获取预制件身上的组件
        /// </summary>
        public static T[] GetAssetPrefabs<T>(params string[] folders) where T : Component
        {
            var gameobjects = GetAssetsRes<GameObject>("t:Prefab", folders);
            var list = new List<T>();
            foreach (var asset in gameobjects)
                list.AddRange(asset.GetComponentsInChildren<T>(true));
            return list.ToArray();
        }

        /// <summary>
        /// 获取预制件资源路径
        /// </summary>
        /// <param name="folders">文件夹</param>
        /// <returns>预制件数组</returns>
        public static string[] GetAssetPrefabsPath(params string[] folders)
        {
            return GetAssetsPath("t:Prefab", folders);
        }

        /// <summary>
        /// 获取预制件资源路径
        /// </summary>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="folders">文件夹</param>
        /// <returns>预制件数组</returns>
        public static string[] GetAssetPrefabsPath(Func<string, bool> filtration, params string[] folders)
        {
            return GetAssetsPath("t:Prefab", filtration, folders);
        }
    }
}