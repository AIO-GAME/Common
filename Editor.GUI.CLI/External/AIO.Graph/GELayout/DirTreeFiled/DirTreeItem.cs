#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    [Serializable]
    public class DirTreeItem : IEnumerable<DirTreeItem>
    {
        public string OptionSearchPatternFolder;

        public string OptionSearchPatternFile;

        public bool OptionSearchFile;

        /// <summary>
        /// 文件夹信息
        /// </summary>
        public string DirInfo;

        /// <summary>
        /// 子文件夹
        /// </summary>
        [SerializeField]
        private DirTreeItem Next;

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth;

        /// <summary>
        /// 最大深度
        /// </summary>
        public int MaxDepth;

        /// <summary>
        /// 路径
        /// </summary>
        public string[] Paths;

        /// <summary>
        /// 子文件夹
        /// </summary>
        public int SelectIndex;

        /// <summary>
        /// 当前深度下标
        /// </summary>
        [NonSerialized]
        private DirTreeItem Last;

        private DirTreeItem() { }

        protected DirTreeItem(string info, DirTreeItem last)
        {
            DirInfo                   = info;
            Directory                 = new List<string>();
            Files                     = new List<string>();
            Last                      = last;
            Depth                     = last.Depth + 1;
            MaxDepth                  = last.MaxDepth;
            OptionSearchFile          = last.OptionSearchFile;
            OptionSearchPatternFolder = last.OptionSearchPatternFolder;
            OptionSearchPatternFile   = last.OptionSearchPatternFile;
        }

        /// <summary>
        /// 文件夹
        /// </summary>
        public List<string> Directory { get; private set; }

        /// <summary>
        /// 文件
        /// </summary>
        public List<string> Files { get; private set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValidity => System.IO.Directory.Exists(DirInfo);

        public IEnumerator<DirTreeItem> GetEnumerator()
        {
            var temp = this;
            while (temp != null)
            {
                if (temp.Depth >= temp.MaxDepth) yield break;
                if (temp.Paths is null || temp.Paths.Length == 0) yield break;
                if (!temp.IsValidity) yield break;
                yield return temp;
                var path = Path.Combine(temp.DirInfo, temp.Paths[temp.SelectIndex]).TrimEnd('\\');
                if (!System.IO.Directory.Exists(path)) yield break;
                if (temp.Next is null || temp.Next.DirInfo != path)
                    temp.Next = CreateTree(path, temp);
                temp = temp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static DirTreeItem Create(string info, int maxDepth,
                                         bool   optionSearchFile,
                                         string optionSearchPatternFolder,
                                         string optionSearchPatternFile)
        {
            var item = new DirTreeItem
            {
                DirInfo                   = info,
                Depth                     = 0,
                MaxDepth                  = maxDepth,
                OptionSearchFile          = optionSearchFile,
                OptionSearchPatternFolder = optionSearchPatternFolder,
                OptionSearchPatternFile   = optionSearchPatternFile
            };

            if (!item.IsValidity) return item;
            item.Last = null;
            item.UpdatePaths();
            return item;
        }

        public void UpdatePaths()
        {
            if (!IsValidity) return;
            Directory = new List<string>();
            Files     = new List<string>();
            var info = new DirectoryInfo(DirInfo);
            foreach (var directory in info.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                if (!string.IsNullOrEmpty(OptionSearchPatternFolder))
                    if (!Regex.IsMatch(directory.Name, OptionSearchPatternFolder))
                        continue;

                Directory.Add(string.Concat(directory.Name, '\\'));
            }

            if (OptionSearchFile)
                foreach (var file in info.GetFiles("*", SearchOption.TopDirectoryOnly))
                {
                    if (!string.IsNullOrEmpty(OptionSearchPatternFile))
                        if (!Regex.IsMatch(file.Name, OptionSearchPatternFile))
                            continue;
                    Files.Add(file.Name);
                }

            Paths = new string[Directory.Count + Files.Count];
            Directory.CopyTo(0, Paths, 0, Directory.Count);
            Files.CopyTo(0, Paths, Directory.Count, Files.Count);
            SelectIndex = 0;
        }

        public void Dispose()
        {
            OptionSearchPatternFolder = null;
            OptionSearchPatternFile   = null;
            Directory                 = null;
            Files                     = null;
            Last                      = null;
            Next                      = null;
            Paths                     = null;
        }

        public sealed override string ToString()
        {
            if (Paths is null || Paths.Length == 0) return string.Empty;
            return Paths[SelectIndex];
        }

        private static DirTreeItem CreateTree(string info, DirTreeItem last)
        {
            var tree = new DirTreeItem(info, last);
            if (!tree.IsValidity) return tree;
            tree.UpdatePaths();
            return tree;
        }
    }
}