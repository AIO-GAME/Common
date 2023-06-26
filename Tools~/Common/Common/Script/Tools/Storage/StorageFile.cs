﻿using System;
using System.IO;

namespace AIO
{
    /// <summary>
    /// 数据文件
    /// </summary>
    public abstract class StorageFile : Storage,
        ISave,
        ILoad
    {
        /// <summary>
        /// 保存读取路径
        /// </summary>
        private readonly string Path;

        /// <summary>
        /// 数据存储
        /// </summary>
        /// <param name="path">存储读取路径</param>
        protected StorageFile(in string path) : base()
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            Path = path;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        public void Save()
        {
            if (Count == 0) return;
            UtilsGen.IO.Write(Path, Data, 0, Data.Length, false);
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            Buffer.Clear();
            if (File.Exists(Path)) Buffer.Write(UtilsGen.IO.Read(Path));
        }
    }
}