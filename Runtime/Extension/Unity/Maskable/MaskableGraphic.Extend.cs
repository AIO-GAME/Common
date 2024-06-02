#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// MaskableGraphic扩展类
    /// </summary>
    public static class MaskableGraphicKit
    {
        /// <summary>
        /// 设置材质
        /// </summary>
        public static void SetMat<T>(this T graphic, Material material)
        where T : MaskableGraphic
        {
            graphic.material = material;
            graphic.SetMaterialDirty();
        }

        // /// <summary>
        // /// 设置材质
        // /// </summary>
        // /// <param name="path">材质文件路径</param>
        //public static void SetMat<T>(this T grap, string path) where T : MaskableGraphic
        //{
        //    grap.material = UIMaterialKit.GetMaterial(path);
        //    grap.SetMaterialDirty();
        //}

        /// <summary>
        /// 材质清空
        /// </summary>
        public static void MatClear<T>(this T grap)
        where T : MaskableGraphic
        {
            grap.material = null;
            grap.SetMaterialDirty();
        }
    }
}