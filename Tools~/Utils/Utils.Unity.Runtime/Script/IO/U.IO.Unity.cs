using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

public partial class UtilsEngine
{
    /// <summary>
    /// Unity IO
    /// </summary>
    public static partial class IO
    {
        /// <summary>
        /// Unity默认路径
        /// </summary>
        public static string UnityPath { get; }

        internal static int UnityPathLength { get; }

        static IO()
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
        public static IEnumerable<string> GetFilesRelativeAsset(
            string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<string>();
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return Array.Empty<string>();
            return Utils.IO.GetFilesInfo(value, pattern, option)
                .Select(item => item.FullName.Substring(UnityPathLength));
        }

        /// <summary>
        /// 获取资源文件夹下
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查找模式</param>
        /// <returns>以Assets路径为节点的路径数组</returns>
        public static IEnumerable<string> GetFilesRelativeAsset(
            string value,
            in Func<FileInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<string>();
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return Array.Empty<string>();
            return Utils.IO.GetFilesInfo(value, filtration, pattern, option)
                .Select(item => item.FullName.Substring(UnityPathLength));
        }

        /// <summary>
        /// 获取资源文件夹下 屏蔽meta文件
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查找模式</param>
        /// <returns>以Assets路径为节点的路径数组</returns>
        public static IEnumerable<string> GetFilesRelativeAssetNoMeta(string value, Func<FileInfo, bool> filtration,
            string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<string>();
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return Array.Empty<string>();
            return
                from item in Utils.IO.GetFilesInfo(value, filtration, pattern, option)
                where !item.Extension.Contains(".meta")
                select item.FullName.Substring(UnityPathLength);
        }

        /// <summary>
        /// 获取资源文件夹下 屏蔽meta文件
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查找模式</param>
        /// <returns>以Assets路径为节点的路径数组</returns>
        public static IEnumerable<string> GetFilesRelativeAssetNoMeta(
            string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<string>();
            value = Path.GetFullPath(value);
            if (!value.Contains(UnityPath)) return Array.Empty<string>();
            return
                from item in Utils.IO.GetFilesInfo(value, pattern, option)
                where !item.Extension.Contains(".meta")
                select item.FullName.Substring(UnityPathLength);
        }
    }
}
