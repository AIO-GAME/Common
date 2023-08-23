﻿#if SUPPORT_YOOASSET
using System;
using System.IO;
using UnityEngine;
using YooAsset;

namespace AIO.UEngine
{
    /// <summary>
    /// 内置文件查询服务类
    /// </summary>
    public class ResolverQueryServices : IQueryServices
    {
        public bool QueryStreamingAssets(string packageName, string fileName)
        {
#if UNITY_EDITOR
            Console.WriteLine("-> QueryStreamingAssets: " + string.Concat(packageName, '/', fileName));
#endif
#if UNITY_WEBGL
            var path = Path.Combine(Application.persistentDataPath, "BuildinFiles", packageName, fileName);
#else
            var path = Path.Combine(Application.streamingAssetsPath, "BuildinFiles", packageName, fileName);
#endif

#if UNITY_EDITOR
            Debug.LogFormat("{0} -> {1}", nameof(QueryStreamingAssets), path);
#endif
            return File.Exists(path);
        }

        public bool QueryDeliveryFiles(string packageName, string fileName)
        {
            return false;
#if UNITY_EDITOR
            // System.Console.WriteLine("-> QueryDeliveryFiles: " + string.Concat(packageName, '/', fileName));
#endif
            // var package = YooAssets.TryGetPackage(packageName);
            // if (package is null) return false;
            // return package.CheckLocationValid(fileName);
        }

        public DeliveryFileInfo GetDeliveryFileInfo(string packageName, string fileName)
        {
#if UNITY_EDITOR
            Console.WriteLine("-> GetDeliveryFileInfo: " + string.Concat(packageName, '/', fileName));
#endif

            return new DeliveryFileInfo
            {
                DeliveryFilePath = string.Concat(packageName, '/', fileName),
                DeliveryFileOffset = 0,
            };
        }
    }
}
#endif