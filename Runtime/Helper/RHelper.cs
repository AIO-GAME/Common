/*|============|*|
|*|Author:     |*| xi nan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using UnityEngine;
using UnityEngine.Rendering;

namespace AIO
{
    /// <summary>
    /// Utils Unity Engine
    /// </summary>
    public static partial class RHelper
    {
        /// <summary>
        /// 设置渲染帧间隔
        /// </summary>
        /// <param name="sync"> 垂直同步计数 </param>
        /// <param name="frame"> 帧间隔 </param>
        /// <c>讲解</c>
        /// <code>
        ///  if (Application.targetFrameRate = 60);
        ///      frame = 1; 代表 60fps
        ///      frame = 2; 代表 30fps
        ///      frame = 3; 代表 20fps
        ///      frame = 4; 代表 15fps
        ///      frame = 5; 代表 12fps
        ///      frame = 6; 代表 10fps
        /// </code>
        public static void SetFrame(int sync, int frame)
        {
            QualitySettings.vSyncCount            = sync;
            OnDemandRendering.renderFrameInterval = frame;
        }

        /// <summary>
        /// 设置物理模拟开关
        /// </summary>
        /// <param name="auto"> 是否自动模拟 </param>
        public static void PhysicsSimulation(bool auto) { Physics.autoSimulation = auto; }
    }
}
