/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-06-04               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;
using UnityEngine;

namespace UnityEditor
{
    public static partial class UtilsEditor
    {
        /// <summary>
        /// Profiler Editor
        /// </summary>
        public static class Profiler
        {
            /// <summary>
            /// 获取 Texture 磁盘占用大小
            /// </summary>
            /// <returns>占用空间</returns>
            public static long GetStorageMemoryTexture<T>(T obj) where T : Texture
            {
                var method = UtilsGen.Assembly.GetMethodInfo(
                    "UnityEditor.dll",
                    "UnityEditor.TextureUtil",
                    "GetStorageMemorySize");
                return Convert.ToInt64(method.Invoke(null, new object[] { obj }));
            }
        }
    }
}