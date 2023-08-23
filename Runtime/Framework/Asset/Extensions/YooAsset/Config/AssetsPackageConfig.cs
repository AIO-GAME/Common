﻿namespace AIO.UEngine
{
    using System;

    [Serializable]
    public class AssetsPackageConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 版本
        /// </summary>
        public string Version;

        /// <summary>
        /// 是否为默认包
        /// </summary>
        public bool IsDefault;

        public override string ToString()
        {
            return string.Format("Name : {0} , Version : {1} , IsDefault : {2}", Name, Version, IsDefault);
        }
    }
}