/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-21
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.Collections.Generic;
using UnityEngine;

namespace AIO
{
    public static class MaterialExtend
    {
        /// <summary>
        /// 将材质列表中的材质替换为复制的材质
        /// </summary>
        /// <param name="materials"> 材质列表 </param>
        /// <returns> 是否有材质被替换 </returns>
        public static bool ConvertToCopiedMaterials(this IList<Material> materials)
        {
            if (materials == null) return false;
            bool changed = false;
            for (int i = 0; i < materials.Count; ++i)
            {
                if (!GetCopiedMaterial(materials[i], out var copiedMaterial)) continue;
                changed      = true;
                materials[i] = copiedMaterial;
            }

            return changed;
        }

        /// <summary>
        /// 获取材质的复制品
        /// </summary>
        /// <param name="material"> 原始材质 </param>
        /// <param name="copiedMaterial"> 复制的材质 </param>
        /// <returns> 是否创建了新的复制材质 </returns>
        public static bool GetCopiedMaterial(this Material material, out Material copiedMaterial)
        {
            if (material == null)
            {
                copiedMaterial = null;
                return false;
            }

            if (material.name.EndsWith("__copied"))
            {
                copiedMaterial = material;
                return false;
            }

            copiedMaterial      = Object.Instantiate(material);
            copiedMaterial.name = $"{material.name}__copied";
            return true;
        }
    }
}