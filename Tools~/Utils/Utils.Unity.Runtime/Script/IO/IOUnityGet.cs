using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace AIO.Unity
{
    /// <summary>
    /// Unity IOUtils
    /// </summary>
    public static partial class UIOUtils
    {
        /// <summary>
        /// Unity默认路径
        /// </summary>
        public static string UnityPath { get; }

        internal static int UnityPathLength { get; }

        static UIOUtils()
        {
            UnityPath = Application.dataPath.Replace("Assets", "");
            UnityPathLength = UnityPath.Length;
        }

        /// <summary>
        /// 获取资源文件夹下
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查找模式</param>
        /// <returns>以Assets路径为节点的路径数组</returns>
        public static string[] GetFilesRelativeAsset(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return new string[] { };
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return new string[] { };
            var array = new List<string>();
            foreach (var item in IOUtils.GetFilesInfo(value, pattern, option))
                array.Add(item.FullName.Substring(UnityPathLength).Replace('\\', '/'));
            return array.ToArray();
        }

        /// <summary>
        /// 获取资源文件夹下
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查找模式</param>
        /// <returns>以Assets路径为节点的路径数组</returns>
        public static string[] GetFilesRelativeAsset(string value, Func<FileInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return new string[] { };
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return new string[] { };
            var array = new List<string>();
            foreach (var item in IOUtils.GetFilesInfo(value, filtration, pattern, option))
                array.Add(item.FullName.Substring(UnityPathLength).Replace('\\', '/'));
            return array.ToArray();
        }

        /// <summary>
        /// 获取资源文件夹下 屏蔽meta文件
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查找模式</param>
        /// <returns>以Assets路径为节点的路径数组</returns>
        public static string[] GetFilesRelativeAssetNoMeta(string value, Func<FileInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return new string[] { };
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return new string[] { };
            var array = new List<string>();
            foreach (var item in IOUtils.GetFilesInfo(value, filtration, pattern, option))
                if (!item.Extension.Contains(".meta"))
                    array.Add(item.FullName.Substring(UnityPathLength).Replace('\\', '/'));
            return array.ToArray();
        }

        /// <summary>
        /// 获取资源文件夹下 屏蔽meta文件
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查找模式</param>
        /// <returns>以Assets路径为节点的路径数组</returns>
        public static string[] GetFilesRelativeAssetNoMeta(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return new string[] { };
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return new string[] { };
            var array = new List<string>();
            foreach (var item in IOUtils.GetFilesInfo(value, pattern, option))
                if (!item.Extension.Contains(".meta"))
                    array.Add(item.FullName.Substring(UnityPathLength).Replace('\\', '/'));
            return array.ToArray();
        }
    }
}