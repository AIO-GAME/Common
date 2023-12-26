/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-22
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.IO;
using System.Text;

namespace AIO.UEditor
{
    /// <summary>
    /// 路径选择器
    /// </summary>
    [Serializable]
    public class DirTree
    {
        #region Option

        /// <summary>
        /// 目录深度
        /// </summary>
        public int OptionDirDepth = 1;

        /// <summary>
        /// 是否支持搜索文件
        /// </summary>
        public bool OptionSearchFiles;

        /// <summary>
        /// 搜索文件的搜索模式 正则表达式
        /// </summary>
        public string OptionSearchPatternFile = ".";

        /// <summary>
        /// 搜索文件夹的搜索模式 正则表达式
        /// </summary>
        public string OptionSearchPatternFolder = ".";

        public void UpdateOption()
        {
            if (!Directory.Exists(DirPath)) return;
            foreach (var item in Root)
            {
                item.MaxDepth = OptionDirDepth;
                item.OptionSearchFile = OptionSearchFiles;
                item.OptionSearchPatternFile = OptionSearchPatternFile;
                item.OptionSearchPatternFolder = OptionSearchPatternFolder;
                item.UpdatePaths();
            }

            OnUpdateOption();
        }

        protected virtual void OnUpdateOption()
        {
        }

        #endregion

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= Root.MaxDepth) return string.Empty;
                var i = 0;
                foreach (var item in Root)
                {
                    if (i++ != index) continue;
                    return item.Paths[item.SelectIndex].Replace("\\", "");
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 目录
        /// </summary>
        public string DirPath;

        public DirTreeItem Root;

        public int Count => Root.MaxDepth;

        public DirTree()
        {
        }

        public DirTree(string directoryPath, int optionDirDepth = 1)
        {
            CreateTree(
                DirPath = directoryPath,
                OptionDirDepth = optionDirDepth
            );
        }

        protected void CreateTree(
            string info,
            int maxDepth
        )
        {
            if (string.IsNullOrEmpty(info)) info = nameof(string.Empty);
            Root = DirTreeItem.Create(info, maxDepth,
                OptionSearchFiles,
                OptionSearchPatternFolder,
                OptionSearchPatternFile);
            UpdateOption();
        }

        /// <summary>
        /// 获取完整路径
        /// </summary>
        public string GetFullPath()
        {
            if (string.IsNullOrEmpty(DirPath)) return string.Empty;
            var builder = new StringBuilder(DirPath).Append('/');
            foreach (var item in Root) builder.Append(item.Paths[item.SelectIndex]);
            return builder.ToString().Replace("\\", "/");
        }

        public string GetAbsolutePath()
        {
            if (string.IsNullOrEmpty(DirPath)) return string.Empty;
            var builder = new StringBuilder();
            foreach (var item in Root) builder.Append(item.Paths[item.SelectIndex]);
            return builder.ToString().Replace("\\", "/");
        }

        /// <summary>
        /// 路径是否有效
        /// </summary>
        public bool IsValidity()
        {
            return Directory.Exists(GetFullPath());
        }

        /// <summary>
        /// 获取根目录
        /// </summary>
        public string GetRootPath()
        {
            return DirPath;
        }
    }
}