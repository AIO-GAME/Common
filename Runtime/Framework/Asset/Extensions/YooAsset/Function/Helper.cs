﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-21
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

#if SUPPORT_YOOASSET
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace AIO.UEngine
{
    internal static partial class YAssetSystem
    {
        private static IEnumerator GetAutoPakcageCO(AssetInfo location, Action<YAssetPackage> cb)
        {
            yield return GetAutoPakcageCO(location.Address, cb);
        }

        private static IEnumerator GetAutoPakcageCO(string location, Action<YAssetPackage> cb)
        {
            Print.LogFormat("Load Assets Coroutine : [auto : {0}]", location);
            foreach (var package in Dic.Values.Where(package => package.CheckLocationValid(location)))
            {
                if (package.IsNeedDownloadFromRemote(location))
                {
                    var info = package.GetAssetInfo(location);
                    if (info is null) throw new SystemException(string.Format("无法获取资源信息 [{0} : {1}]", package.PackageName, location));
                    var operation = package.CreateBundleDownloader(info, 10, 1, 60);
                    RegisterEvent(location, operation);
                    operation.BeginDownload();
                    yield return operation;
                    switch (operation.Status)
                    {
                        case EOperationStatus.Succeed: break;
                        default: throw new SystemException(string.Format("资源获取失败 [{0} : {1}] {2} -> {3}", package.PackageName, package.GetPackageVersion(), location, operation.Error));
                    }
                }

                cb?.Invoke(package);
                yield break;
            }
#if UNITY_EDITOR
            throw new SystemException(string.Format("资源查找失败 [auto : {0}]", location));
#endif
        }

        private static IEnumerator GetAutoPakcageCO(string packagename, AssetInfo location, Action<YAssetPackage> cb)
        {
            yield return GetAutoPakcageCO(packagename, location.Address, cb);
        }

        private static IEnumerator GetAutoPakcageCO(string packagename, string location, Action<YAssetPackage> cb)
        {
            Print.LogFormat("Load Assets Coroutine : [{0} : {1}]", packagename, location);

            if (!Dic.TryGetValue(packagename, out var package))
                throw new SystemException(string.Format("目标资源包不存在 [{0} : {1}]", packagename, location));

            if (package.IsNeedDownloadFromRemote(location))
            {
                var info = package.GetAssetInfo(location);
                if (info is null) throw new SystemException(string.Format("无法获取资源信息 [{0} : {1}]", packagename, location));
                var operation = package.CreateBundleDownloader(info, 10, 1, 60);
                RegisterEvent(location, operation);
                operation.BeginDownload();
                yield return operation;
                switch (operation.Status)
                {
                    case EOperationStatus.Succeed: break;
                    default:
                        throw new SystemException(string.Format("资源获取失败 [{0} : {1}] {2} -> {3}",
                            package.PackageName,
                            package.GetPackageVersion(),
                            location,
                            operation.Error));
                }
            }

            if (!package.CheckLocationValid(location))
                throw new SystemException(string.Format("[{0} : {1}] 传入地址验证无效 {2}",
                    package.PackageName,
                    package.GetPackageVersion(),
                    location));

            cb?.Invoke(package);
        }

        private static YAssetPackage GetAutoPakcageSync(AssetInfo location)
        {
            return GetAutoPakcageSync(location.Address);
        }

        private static YAssetPackage GetAutoPakcageSync(string location)
        {
            Print.LogFormat("Load Assets Sync : [auto : {0}]", location);

            foreach (var package in Dic.Values.Where(package => package.CheckLocationValid(location)))
            {
                if (package.IsNeedDownloadFromRemote(location))
                    throw new SystemException(string.Format("不支持同步加载远程资源 [{0} : {1}]", package.PackageName, location));

                return package;
            }
#if UNITY_EDITOR
            throw new SystemException(string.Format("资源查找失败 [auto : {0}]", location));
#endif
            return null;
        }

        private static YAssetPackage GetAutoPakcageSync(string packagename, AssetInfo location)
        {
            return GetAutoPakcageSync(packagename, location.Address);
        }

        private static YAssetPackage GetAutoPakcageSync(string packagename, string location)
        {
            Print.LogFormat("Load Assets Sync : [{0} : {1}]", packagename, location);

            if (!Dic.TryGetValue(packagename, out var package))
                throw new SystemException(string.Format("目标资源包不存在 [{0} : {1}]", packagename, location));

            if (package.IsNeedDownloadFromRemote(location))
                throw new SystemException(string.Format("不支持同步加载远程资源 [{0} : {1}]", package.PackageName, location));

            if (!package.CheckLocationValid(location))
                throw new SystemException(string.Format("[{0} : {1}] 传入地址验证无效 {2}", package.PackageName, package.GetPackageVersion(), location));

            return package;
        }

        private static Task<YAssetPackage> GetAutoPakcageTask(AssetInfo location)
        {
            return GetAutoPakcageTask(location.Address);
        }

        private static async Task<YAssetPackage> GetAutoPakcageTask(string location)
        {
            Print.LogFormat("Load Assets Async : [auto : {0}]", location);

            foreach (var package in Dic.Values.Where(package => package.CheckLocationValid(location)))
            {
                if (package.IsNeedDownloadFromRemote(location))
                {
                    var info = package.GetAssetInfo(location);
                    if (info is null) throw new SystemException(string.Format("无法获取资源信息 {0}", location));
                    var operation = package.CreateBundleDownloader(info, 10, 1, 60);
                    RegisterEvent(location, operation);
                    operation.BeginDownload();
                    await operation.Task;
                    switch (operation.Status)
                    {
                        case EOperationStatus.Succeed: break;
                        default:
                            throw new SystemException(string.Format("资源获取失败 [{0} : {1}] {2} -> {3}",
                                package.PackageName,
                                package.GetPackageVersion(),
                                location,
                                operation.Error));
                    }
                }

                return package;
            }
#if UNITY_EDITOR
            throw new SystemException(string.Format("资源查找失败 [auto : {0}]", location));
#endif
            return null;
        }

        private static Task<YAssetPackage> GetAutoPakcageTask(string packagename, AssetInfo location)
        {
            return GetAutoPakcageTask(packagename, location.Address);
        }

        private static async Task<YAssetPackage> GetAutoPakcageTask(string packagename, string location)
        {
            Print.LogFormat("Load Assets Async : [{0} : {1}]", packagename, location);

            if (!Dic.TryGetValue(packagename, out var package))
                throw new SystemException(string.Format("目标资源包不存在 [{0} : {1}]", packagename, location));

            if (package.IsNeedDownloadFromRemote(location))
            {
                var info = package.GetAssetInfo(location);
                if (info is null) throw new SystemException(string.Format("无法获取资源信息 [{0} : {1}]", packagename, location));
                var operation = package.CreateBundleDownloader(info, 10, 1, 60);
                RegisterEvent(location, operation);
                operation.BeginDownload();
                await operation.Task;
                switch (operation.Status)
                {
                    case EOperationStatus.Succeed: break;
                    default:
                        throw new SystemException(string.Format("资源获取失败 [{0} : {1}] {2} -> {3}",
                            package.PackageName,
                            package.GetPackageVersion(),
                            location,
                            operation.Error));
                }
            }

            if (!package.CheckLocationValid(location))
                throw new SystemException(string.Format("[{0} : {1}] 传入地址验证无效 {2}",
                    package.PackageName,
                    package.GetPackageVersion(),
                    location));

            return package;
        }

        private static Dictionary<string, OperationHandleBase> ReferenceOPHandle { get; set; } = new Dictionary<string, OperationHandleBase>();

        private static T GetHandle<T>(string location) where T : OperationHandleBase
        {
            if (ReferenceOPHandle.ContainsKey(location)) return (T)ReferenceOPHandle[location];
            return null;
        }

        private static T GetHandle<T>(AssetInfo location) where T : OperationHandleBase
        {
            return GetHandle<T>(location.Address);
        }

        private static void AddHandle<T>(string location, T operation) where T : OperationHandleBase
        {
            if (!ReferenceOPHandle.ContainsKey(location)) ReferenceOPHandle.Add(location, operation);
        }

        private static void AddHandle<T>(AssetInfo location, T operation) where T : OperationHandleBase
        {
            AddHandle(location.Address, operation);
        }

        public static void FreeHandle(string location)
        {
            if (ReferenceOPHandle.TryGetValue(location, out var value))
            {
                value.ReleaseInternal();
                ReferenceOPHandle.Remove(location);
            }
        }

        public static void FreeHandle(IEnumerable<string> locations)
        {
            foreach (var location in locations)
            {
                if (ReferenceOPHandle.TryGetValue(location, out var value))
                {
                    value.ReleaseInternal();
                    ReferenceOPHandle.Remove(location);
                }
            }
        }

        public static void FreeHandle(IEnumerable<AssetInfo> locations)
        {
            foreach (var location in locations) FreeHandle(location.Address);
        }

        public static void FreeHandle(AssetInfo location)
        {
            FreeHandle(location.Address);
        }


        public static void Destroy()
        {
            if (isInitialize)
            {
                foreach (var item in ReferenceOPHandle.Keys.ToArray())
                    ReferenceOPHandle[item].ReleaseInternal();
                ReferenceOPHandle.Clear();

                YooAssets.Destroy();
                isInitialize = false;
            }
        }
    }
}
#endif