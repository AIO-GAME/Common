/*|============|*|
|*|Author:     |*| xi nan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using UnityEngine;
using UnityEngine.Rendering;

namespace AIO.UEditor
{
    /// <summary>
    /// Utils Unity Editor
    /// </summary>
    public static partial class EHelper
    {
        /// <summary>
        /// 是否为命令行模式
        /// </summary>
        /// <returns>
        /// Ture: 是
        /// False: 否
        /// </returns>
        public static bool IsCMD()
        {
            return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
        }
    }
}